using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class GetManyResponse<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNo { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int TotalRows { get; set; } = 0;
    }
}
