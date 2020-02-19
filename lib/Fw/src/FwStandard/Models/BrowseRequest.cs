using System.Collections.Generic;
using System.Dynamic;

namespace FwStandard.Models
{
    public class BrowseRequest
    {
        public dynamic miscfields { get; set; } = new ExpandoObject();
        public string module { get; set; } = string.Empty;
        public dynamic options { get; set; } = new ExpandoObject();
        public string orderby { get; set; } = string.Empty;
        public string orderbydirection { get; set; } = string.Empty;
        public int top { get; set; } = 0;
        public int pageno { get; set; } = 0;
        public int pagesize { get; set; } = 0;
        public List<string> searchfieldoperators { get; set; } = new List<string>();
        public List<string> searchfields { get; set; } = new List<string>();
        public List<string> searchfieldvalues { get; set; } = new List<string>();
        public List<string> searchfieldtypes { get; set; } = new List<string>();
        public List<string> searchseparators { get; set; } = new List<string>();
        public List<string> searchcondition { get; set; } = new List<string>();
        public List<string> searchconjunctions { get; set; } = new List<string>();
        public dynamic uniqueids { get; set; } = new ExpandoObject();
        public dynamic boundids { get; set; } = new ExpandoObject();
        public Dictionary<string, string> filterfields { get; set; } = new Dictionary<string, string>();
        public string activeview { get; set; } = string.Empty;
        public bool emptyobject { get; set; }  // send emptyobject=true to get a browse response with structure only, no data
        public bool forexcel { get; set; } = false;
        public bool includeallcolumns { get; set; } = true;
        public bool includecolorcolumns { get; set; } = true;
        public bool includeidcolumns { get; set; } = true;
        public CheckBoxListItems excelfields { get; set; } = new CheckBoxListItems();
        public List<string> totalfields { get; set; } = new List<string>();
        public Dictionary<string, List<string>> activeviewfields { get; set; } = new Dictionary<string, List<string>>();
        public BrowseRequest()
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
