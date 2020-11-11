using FwStandard.Models;

namespace FwStandard.Data
{
    //------------------------------------------------------------------------------------
    public class FwReportRequest
    {
        public bool? IsSummary { get; set; } = false;
        public bool IncludeSubHeadingsAndSubTotals { get; set; } = true;
        public bool IncludeIdColumns { get; set; } = true;
        public string Locale { get; set; }
        public CheckBoxListItems excelfields { get; set; } = new CheckBoxListItems();
    }
    //------------------------------------------------------------------------------------
}
