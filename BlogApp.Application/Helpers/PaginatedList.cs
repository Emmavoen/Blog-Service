using System;
using System.Collections.Generic;

namespace BlogApp.Application.Helpers
{
    public class PaginatedList<T>
        {
            public List<T> Items { get; }
            public int TotalCount { get; }
            public int PageNumber { get; }
            public int PageSize { get; }
            public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

            public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
            {
                Items = items;
                TotalCount = count;
                PageNumber = pageNumber;
                PageSize = pageSize;
            }
        }
}