using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;

namespace NetCore.Common.DataAccess
{
    public static class AdoNetUtils
    {
        public static DbCommand CreateCommand(this DbTransaction tran, string commandText, CommandType? type = null)
        {
            var cmd = tran.Connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.Transaction = tran;

            if (type != null)
            {
                cmd.CommandType = type.Value;
            }
            return cmd;
        }

        public static DbCommand CreateCommand(this DbConnection conn, string commandText, CommandType? type = null)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            if (type != null)
            {
                cmd.CommandType = type.Value;
            }
            return cmd;
        }

        public static DbParameter RegisterParameter(this DbCommand cmd, string name, object value, DbType? dbType = null, ParameterDirection? direction = null)
        {
            var par = cmd.CreateParameter();
            par.ParameterName = name;
            par.Value = value ?? DBNull.Value;

            if (dbType != null)
            {
                par.DbType = dbType.Value;
            }
            if (direction != null)
            {
                par.Direction = direction.Value;
            }

            cmd.Parameters.Add(par);
            return par;
        }

        public static DbParameter RegisterContains(this DbCommand cmd, string name, string value)
        {
            return cmd.RegisterParameter(name, value != null ? "%" + value + "%" : null, DbType.String);
        }

        public static DbParameter RegisterStartsWith(this DbCommand cmd, string name, string value)
        {
            return cmd.RegisterParameter(name, value != null ? value + "%" : null, DbType.String);
        }

        public static DbParameter RegisterEndsWith(this DbCommand cmd, string name, string value)
        {
            return cmd.RegisterParameter(name, value != null ? "%" + value : null, DbType.String);
        }

        public static int ExecuteUpdate(this DbCommand cmd)
        {
            cmd.Connection.TryOpen();
            return cmd.ExecuteNonQuery();
        }

        public static async Task<int> ExecuteUpdateAsync(this DbCommand cmd)
        {
            await cmd.Connection.TryOpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public static T ExecuteScalar<T>(this DbCommand cmd)
        {
            cmd.Connection.TryOpen();
            return (T)cmd.ExecuteScalar();
        }

        public static async Task<T> ExecuteScalarAsync<T>(this DbCommand cmd)
        {
            await cmd.Connection.TryOpenAsync();
            return await cmd.ExecuteScalarAsync<T>();
        }

        public static void ExecuteHandler(this DbCommand cmd, RowHandler handler)
        {
            cmd.Connection.TryOpen();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    handler(reader);
                }
            }
        }

        public static async Task ExecuteHandlerAsync(this DbCommand cmd, RowHandler handler)
        {
            await cmd.Connection.TryOpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    handler(reader);
                }
            }
        }

        public static IList<T> ExecuteList<T>(this DbCommand cmd, RowMapper<T> mapper)
        {
            cmd.Connection.TryOpen();
            using (var reader = cmd.ExecuteReader())
            {
                IList<T> list = new List<T>();
                while (reader.Read())
                {
                    T obj = mapper(reader);
                    list.Add(obj);
                }
                return list;
            }
        }

        public static async Task<IList<T>> ExecuteListAsync<T>(this DbCommand cmd, RowMapper<T> mapper)
        {
            await cmd.Connection.TryOpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                IList<T> list = new List<T>();
                while (await reader.ReadAsync())
                {
                    T obj = mapper(reader);
                    list.Add(obj);
                }
                return list;
            }
        }

        public static T ExecuteSingle<T>(this DbCommand cmd, RowMapper<T> mapper)
        {
            cmd.Connection.TryOpen();
            using (var reader = cmd.ExecuteReader())
            {
                T t = default(T);
                bool found = false;
                while (reader.Read())
                {
                    if (found)
                    {
                        throw new NonUniqueSqlException(cmd.CommandText);
                    }
                    t = mapper(reader);
                    found = true;
                }
                return t;
            }
        }

        public static async Task<T> ExecuteSingleAsync<T>(this DbCommand cmd, RowMapper<T> mapper)
        {
            await cmd.Connection.TryOpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                T t = default(T);
                bool found = false;
                while (await reader.ReadAsync())
                {
                    if (found)
                    {
                        throw new NonUniqueSqlException(cmd.CommandText);
                    }
                    t = mapper(reader);
                    found = true;
                }
                return t;
            }
        }

        public static void ExecuteStream(this DbCommand cmd, Stream outStream, Action<DbDataReader> consumer)
        {
            cmd.Connection.TryOpen();
            using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                long dataOffset = 0L;
                byte[] buffer = new byte[4096];
                long bytesRead;

                while (reader.Read())
                {
                    if (consumer != null)
                    {
                        consumer.Invoke(reader);
                    }
                    int streamIndex = reader.FieldCount - 1;
                    do
                    {
                        bytesRead = reader.GetBytes(streamIndex, dataOffset, buffer, 0, buffer.Length);
                        outStream.Write(buffer, 0, (int)bytesRead);
                        dataOffset += bytesRead;
                    }
                    while (bytesRead > 0L);
                }
            }
        }

        public static async Task ExecuteStreamAsync(this DbCommand cmd, Stream outStream, Action<DbDataReader> consumer)
        {
            await cmd.Connection.TryOpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
            {
                long dataOffset = 0L;
                byte[] buffer = new byte[4096];
                long bytesRead;

                while (await reader.ReadAsync())
                {
                    if (consumer != null)
                    {
                        consumer.Invoke(reader);
                    }
                    int streamIndex = reader.FieldCount - 1;
                    do
                    {
                        bytesRead = reader.GetBytes(streamIndex, dataOffset, buffer, 0, buffer.Length);
                        await outStream.WriteAsync(buffer, 0, (int)bytesRead);
                        dataOffset += bytesRead;
                    }
                    while (bytesRead > 0L);
                }
            }
        }

        public static T GetValue<T>(this IDataReader reader, string name)
        {
            object val = reader[name];
            return Convert.IsDBNull(val) ? default(T) : (T)val;
        }

        public static DbTransaction OpenTransaction(this DbConnection conn)
        {
            conn.TryOpen();
            return conn.BeginTransaction();
        }

        public static void TryRollback(this DbTransaction tran)
        {
            if (tran != null)
            {
                tran.Rollback();
            }
        }

        public static void TryOpen(this DbConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public static async Task TryOpenAsync(this DbConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
        }

        public static void TryClose(this DbConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public static void TryDispose(this IDisposable obj)
        {
            if (obj != null)
            {
                obj.Dispose();
            }
        }
    }
}
