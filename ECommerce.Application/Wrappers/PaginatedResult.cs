using System;
using System.Collections.Generic;

namespace ECommerce.Application.Wrappers
{
    public class PaginatedResult<T> where T : class
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        //Önceki veya sonraki sayfa var mı mantıksal kontrolleri
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        //Asıl verilerin tutulduğu liste
        public IReadOnlyList<T> Items { get; private set; }

        public PaginatedResult(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }
    }
}