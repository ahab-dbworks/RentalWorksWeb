using System.Collections.Generic;
using System.Dynamic;

namespace FwStandard.Models
{
    public class BrowseRequestDto
    {
        public dynamic miscfields { get; set; } = new ExpandoObject();
        public string module { get; set; } = string.Empty;
        public dynamic options { get; set; } = new ExpandoObject();
        public string orderby { get; set; } = string.Empty;
        public string orderbydirection { get; set; } = string.Empty;
        public int pageno { get; set; } = 0;
        public int pagesize { get; set; } = 0;
        public string[] searchfieldoperators { get; set; } = new string[0];
        public string[] searchfields { get; set; } = new string[0];
        public string[] searchfieldvalues { get; set; } = new string[0];
        public dynamic uniqueids { get; set; } = new ExpandoObject();
        public dynamic boundids { get; set; } = new ExpandoObject();
        public Dictionary<string, string> filterfields { get; set; } = new Dictionary<string, string>();

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
