using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class BrowseRequestDto
    {
        //public enum OrderByDirections { asc, desc }
        public Dictionary<string, object> miscfields { get; set; } = new Dictionary<string, object>();
        public string module { get; set; } = string.Empty;
        public Dictionary<string, object> options { get; set; } = new Dictionary<string, object>();
        public string orderby { get; set; } = string.Empty;
        public string orderbydirection { get; set; } = string.Empty;
        public int pageno { get; set; } = 0;
        public int pagesize { get; set; } = 0;
        public string[] searchfieldoperators { get; set; } = new string[0];
        public string[] searchfields { get; set; } = new string[0];
        public string[] searchfieldvalues { get; set; } = new string[0];

        public BrowseRequestDto()
        {

        }

        public int GetOffsetRows()
        {
            return (pageno - 1) * pagesize;
        }

        public int GetFetchSize()
        {
            return pagesize;
        }
    }
}
