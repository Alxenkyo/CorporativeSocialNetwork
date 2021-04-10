using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Data
{
    public class PagedResult<TItem>
    {
        public IEnumerable<TItem> Items { get; set; }
        public long Total { get; set; }
    }
}
