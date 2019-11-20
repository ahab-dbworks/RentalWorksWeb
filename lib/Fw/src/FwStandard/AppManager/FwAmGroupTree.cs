using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.AppManager
{
    public class FwAmGroupTree
    {
        public string GroupsId { get; set; } = string.Empty;
        public FwAmSecurityTreeNode RootNode { get; set; } = null;
        public DateTime DateStamp { get; set; } = DateTime.MinValue;
    }
}
