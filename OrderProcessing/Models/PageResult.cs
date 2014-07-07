using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models
{
    public class PageResult<T>
    {
        public PageResult(IEnumerable<T> items, int totalItems, string previousLink = "", string nextLink = "")
        {
            TotalItems = totalItems;
            Items = items;
            PreviousLink = previousLink;
            NextLink = nextLink;
        }

        public int TotalItems { get; private set; }
        public IEnumerable<T> Items { get; private set; }

        public string PreviousLink { get; private set; }
        public string NextLink { get; private set; }
    }
}