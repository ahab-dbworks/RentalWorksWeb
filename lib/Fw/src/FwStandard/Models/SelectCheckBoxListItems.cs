using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{

    //------------------------------------------------------------------------------------ 
    public class SelectedCheckBoxListItem
    {
        public string value;
        public SelectedCheckBoxListItem(string value)
        {
            this.value = value;
        }
    }
    //------------------------------------------------------------------------------------ 
    public class SelectedCheckBoxListItems : List<SelectedCheckBoxListItem>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SelectedCheckBoxListItem item in this)
            {
                if (!sb.Length.Equals(0))
                {
                    sb.Append(",");
                }
                sb.Append(item.value);
            }
            return sb.ToString();
        }
    }
    //------------------------------------------------------------------------------------ 
}
