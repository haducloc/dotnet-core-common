using System.Data;

namespace NetCore.Common.DataAccess
{
    public delegate T RowMapper<T>(IDataReader dataReader);
    public delegate T RowNewer<T>();
}
