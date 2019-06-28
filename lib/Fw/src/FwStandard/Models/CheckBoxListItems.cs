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
        //------------------------------------------------------------------------------------ 
        public CheckBoxListItem(string value, string text, bool? selected)
        {
            this.value = value;
            this.text = text;
            this.selected = selected;
        }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 
    public class CheckBoxListItems : List<CheckBoxListItem>
    {
        //------------------------------------------------------------------------------------ 
        public CheckBoxListItems GetSelectedItems()
        {
            CheckBoxListItems selectedItems = new CheckBoxListItems();

            foreach (CheckBoxListItem item in this)
            {
                if (item.selected.GetValueOrDefault(false))
                {
                    selectedItems.Add(item);
                }
            }

            return selectedItems;
        }
        //------------------------------------------------------------------------------------ 
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
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
}
