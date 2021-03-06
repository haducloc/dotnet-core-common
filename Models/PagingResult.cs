﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Common.Models
{
    public class PagingResult<T>
    {
        // >= 1
        public int PageIndex { get; private set; }

        public int PageCount { get; private set; }

        public int RecordCount { get; private set; }

        public IList<T> Data { get; private set; }

        public PagingResult(IList<T> data, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageCount = (int)Math.Ceiling(count / (double)pageSize);

            RecordCount = count;
            Data = data;
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
                return (PageIndex < PageCount);
            }
        }

        public static async Task<PagingResult<T>> CreateAsync(IQueryable<T> source, int? count, int? pageIndex, int pageSize)
        {
            var countVal = count ?? await source.CountAsync();
            var indexVal = pageIndex ?? 1;
            var items = await source.Skip((indexVal - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagingResult<T>(items, countVal, indexVal, pageSize);
        }
    }
}
