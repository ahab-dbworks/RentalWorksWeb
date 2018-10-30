using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{

    //------------------------------------------------------------------------------------ 
    public class CheckBoxListItem
    {
        public string value { get; set; }
        public string text { get; set; }
        public bool? selected { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class CheckBoxListItems : List<CheckBoxListItem>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (CheckBoxListItem item in this)
            {
                if (item.selected.GetValueOrDefault(false))
                {
                    if (!sb.Length.Equals(0))
                    {
                        sb.Append(",");
                    }
                    sb.Append(item.value);
                }
            }
            return sb.ToString();
        }
    }
    //------------------------------------------------------------------------------------ 
}
