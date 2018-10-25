using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Common.DataAccess
{
    public class PaginatedList<T>
    {
        // >= 1
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public IList<T> Data { get; private set; }

        public PaginatedList(IList<T> data, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.Data = data;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int? count, int? pageIndex, int pageSize)
        {
            var countVal = count ?? await source.CountAsync();
            var indexVal = pageIndex ?? 1;
            var items = await source.Skip((indexVal - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, countVal, indexVal, pageSize);
        }
    }
}
