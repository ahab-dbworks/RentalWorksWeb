using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class BrowseRequestDto
    {
        public enum OrderByDirections { asc, desc }
        public string miscfields { get; set; }
        public string module { get; set; }
        public string[] options { get; set; }
        public string orderby { get; set; }
        public OrderByDirections orderbydirection { get; set; }
        public int pageno { get; set; }
        public int pagesize { get; set; }
        public string[] searchfieldoperators { get; set; }
        public string[] searchfields { get; set; }
        public string[] searchfieldvalues { get; set; }

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
