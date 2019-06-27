using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Item
    {
        [DataType(DataType.Text)]
        public string masterid              { get; set; }

        [DataType(DataType.Text)]
        public string masterno              { get; set; }

        [DataType(DataType.Text)]
        public string master                { get; set; }

        public bool inactive                { get; set; }

        [DataType(DataType.Text)]
        public string availfor              { get; set; }

        [DataType(DataType.Text)]
        public string availfrom             { get; set; }

        [DataType(DataType.Text)]
        public string manufacturer          { get; set; }

        [DataType(DataType.Text)]
        public string category              { get; set; }

        [DataType(DataType.Text)]
        public string inventorydepartmentid { get; set; }

        [DataType(DataType.Text)]
        public string inventorydepartment   { get; set; }

        [DataType(DataType.Text)]
        public string unit                  { get; set; }

        [DataType(DataType.Text)]
        public string unitid                { get; set; }

        [DataType(DataType.Text)]
        public string itemclass             { get; set; }

        [DataType(DataType.Text)]
        public string subcategory           { get; set; }

        [DataType(DataType.Text)]
        public string rank                  { get; set; }

        [DataType(DataType.Text)]
        public string defaultcost           { get; set; }

        [DataType(DataType.Text)]
        public string notes                 { get; set; }

        [DataType(DataType.Text)]
        public string cost                  { get; set; }

        [DataType(DataType.Text)]
        public string price                 { get; set; }

        [DataType(DataType.Text)]
        public string originalicode         { get; set; }

        [DataType(DataType.Text)]
        public string manifestvalue         { get; set; }

        [DataType(DataType.Text)]
        public string shipweightlbs         { get; set; }

        [DataType(DataType.Text)]
        public string shipweightoz          { get; set; }

        [DataType(DataType.Text)]
        public string weightwcaselbs        { get; set; }

        [DataType(DataType.Text)]
        public string weightwcaseoz         { get; set; }

        [DataType(DataType.Text)]
        public string widthft               { get; set; }

        [DataType(DataType.Text)]
        public string widthin               { get; set; }

        [DataType(DataType.Text)]
        public string heightft              { get; set; }

        [DataType(DataType.Text)]
        public string heightin              { get; set; }

        [DataType(DataType.Text)]
        public string lengthft              { get; set; }

        [DataType(DataType.Text)]
        public string lengthin              { get; set; }

        [DataType(DataType.Text)]
        public string shipweightkg          { get; set; }

        [DataType(DataType.Text)]
        public string shipweightg           { get; set; }

        [DataType(DataType.Text)]
        public string weightwcasekg         { get; set; }

        [DataType(DataType.Text)]
        public string weightwcaseg          { get; set; }

        [DataType(DataType.Text)]
        public string widthm                { get; set; }

        [DataType(DataType.Text)]
        public string widthcm               { get; set; }

        [DataType(DataType.Text)]
        public string heightm               { get; set; }

        [DataType(DataType.Text)]
        public string heightcm              { get; set; }

        [DataType(DataType.Text)]
        public string lengthm               { get; set; }

        [DataType(DataType.Text)]
        public string lengthcm              { get; set; }

        [DataType(DataType.Text)]
        public string categoryid            { get; set; }

        [DataType(DataType.Text)]
        public string qty                   { get; set; }

        [DataType(DataType.Text)]
        public string datestamp             { get; set; }

        [DataType(DataType.Text)]
        public string warehouseid           { get; set; }

        [DataType(DataType.Text)]
        public string[] itemaka             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class RentalItem : Item
    {
        public string dailyrate                  { get; set; }

        [DataType(DataType.Text)]
        public string weeklyrate                 { get; set; }

        [DataType(DataType.Text)]
        public string week2rate                  { get; set; }

        [DataType(DataType.Text)]
        public string week3rate                  { get; set; }

        [DataType(DataType.Text)]
        public string week4rate                  { get; set; }

        [DataType(DataType.Text)]
        public string week5rate                  { get; set; }

        [DataType(DataType.Text)]
        public string monthlyrate                { get; set; }

        [DataType(DataType.Text)]
        public string partnumber                 { get; set; }

        [DataType(DataType.Text)]
        public string aisleloc                   { get; set; }

        [DataType(DataType.Text)]
        public string shelfloc                   { get; set; }

        [DataType(DataType.Text)]
        public string nodiscount                 { get; set; }

        [DataType(DataType.Text)]
        public string replacementcost            { get; set; }

        [DataType(DataType.Text)]
        public string trackedby                  { get; set; }

        [DataType(DataType.Text)]
        public string dailycost                  { get; set; }

        [DataType(DataType.Text)]
        public string weeklycost                 { get; set; }

        [DataType(DataType.Text)]
        public string monthlycost                { get; set; }

        [DataType(DataType.Text)]
        public string qtyin                      { get; set; }

        [DataType(DataType.Text)]
        public string dw                         { get; set; }

        [DataType(DataType.Text)]
        public string metered                    { get; set; }

        [DataType(DataType.Text)]
        public string inventorydepartmentorderby { get; set; }

        [DataType(DataType.Text)]
        public string categoryorderby            { get; set; }

        [DataType(DataType.Text)]
        public string subcategoryorderby         { get; set; }

        [DataType(DataType.Text)]
        public string orderby                    { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class SaleItem : Item
    {
        public string inventorydepartmentorderby { get; set; }

        [DataType(DataType.Text)]
        public string categoryorderby            { get; set; }

        [DataType(DataType.Text)]
        public string subcategoryorderby         { get; set; }

        [DataType(DataType.Text)]
        public string orderby                    { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderItem
    {
        [DataType(DataType.Text)]
        public string masteritemid        { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string masterid            { get; set; }

        [DataType(DataType.Text)]
        public string description         { get; set; }

        [DataType(DataType.Text)]
        public string rentfromdate        { get; set; }

        [DataType(DataType.Text)]
        public string rentfromtime        { get; set; }

        [DataType(DataType.Text)]
        public string renttodate          { get; set; }

        [DataType(DataType.Text)]
        public string renttotime          { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string qtyordered          { get; set; }

        [DataType(DataType.Text)]
        public string unit                { get; set; }

        [DataType(DataType.Text)]
        public string price               { get; set; }

        [DataType(DataType.Text)]
        public string daysinwk            { get; set; }

        [DataType(DataType.Text)]
        public string notes               { get; set; }

        [DataType(DataType.Text)]
        public string parentid            { get; set; }

        [DataType(DataType.Text)]
        public string unitextended        { get; set; }

        [DataType(DataType.Text)]
        public string periodextended      { get; set; }

        [DataType(DataType.Text)]
        public string weeklyextended      { get; set; }

        [DataType(DataType.Text)]
        public string taxable             { get; set; }

        [DataType(DataType.Text)]
        public string inactive            { get; set; }

        [DataType(DataType.Text)]
        public string itemorder           { get; set; }

        [DataType(DataType.Text)]
        public string packageitemid       { get; set; }

        [DataType(DataType.Text)]
        public string nestedmasteritemid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class HighlyUsedItem
    {
        [Required]
        [DataType(DataType.Text)]
        public string dealid       { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string departmentid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CompletesAndKits
    {
        [DataType(DataType.Text)]
        public string packageid   { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string warehouseid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CompleteKit
    {
        public string masterno         { get; set; }

        [DataType(DataType.Text)]
        public string description      { get; set; }

        [DataType(DataType.Text)]
        public string masterid         { get; set; }

        [DataType(DataType.Text)]
        public string packageitemid    { get; set; }

        [DataType(DataType.Text)]
        public string packageid        { get; set; }

        [DataType(DataType.Text)]
        public string primaryflg       { get; set; }

        [DataType(DataType.Text)]
        public string defaultqty       { get; set; }

        [DataType(DataType.Text)]
        public string isoption         { get; set; }

        [DataType(DataType.Text)]
        public string charge           { get; set; }

        [DataType(DataType.Text)]
        public string datestamp        { get; set; }

        [DataType(DataType.Text)]
        public string required         { get; set; }

        [DataType(DataType.Text)]
        public string optioncolor      { get; set; }

        [DataType(DataType.Text)]
        public string itemclass        { get; set; }

        [DataType(DataType.Text)]
        public string availfor         { get; set; }

        [DataType(DataType.Text)]
        public string availfrom        { get; set; }

        [DataType(DataType.Text)]
        public string categoryorderby  { get; set; }

        [DataType(DataType.Text)]
        public string orderby          { get; set; }

        [DataType(DataType.Text)]
        public string itemcolor        { get; set; }

        [DataType(DataType.Text)]
        public string isnestedcomplete { get; set; }

        [DataType(DataType.Text)]
        public string iteminactive     { get; set; }

        [DataType(DataType.Text)]
        public string warehouseid      { get; set; }

        [DataType(DataType.Text)]
        public string parentid         { get; set; }

        [DataType(DataType.Text)]
        public string packageitemclass { get; set; }

        public string[] itemaka        { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}