using System.Data.Common;

namespace NetCore.Common.DataAccess
{
    public class NonUniqueSqlException : DbException
    {
        public NonUniqueSqlException(string message) : base(message)
        {
        }
    }
}
