using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Item
    {
        public string masterid              { get; set; }
        public string masterno              { get; set; }
        public string master                { get; set; }
        public bool inactive                { get; set; }
        public string availfor              { get; set; }
        public string availfrom             { get; set; }
        public string manufacturer          { get; set; }
        public string category              { get; set; }
        public string inventorydepartmentid { get; set; }
        public string inventorydepartment   { get; set; }
        public string unit                  { get; set; }
        public string unitid                { get; set; }
        public string itemclass             { get; set; }
        public string subcategory           { get; set; }
        public string rank                  { get; set; }
        public string defaultcost           { get; set; }
        public string notes                 { get; set; }
        public string cost                  { get; set; }
        public string price                 { get; set; }
        public string originalicode         { get; set; }
        public string manifestvalue         { get; set; }
        public string shipweightlbs         { get; set; }
        public string shipweightoz          { get; set; }
        public string weightwcaselbs        { get; set; }
        public string weightwcaseoz         { get; set; }
        public string widthft               { get; set; }
        public string widthin               { get; set; }
        public string heightft              { get; set; }
        public string heightin              { get; set; }
        public string lengthft              { get; set; }
        public string lengthin              { get; set; }
        public string shipweightkg          { get; set; }
        public string shipweightg           { get; set; }
        public string weightwcasekg         { get; set; }
        public string weightwcaseg          { get; set; }
        public string widthm                { get; set; }
        public string widthcm               { get; set; }
        public string heightm               { get; set; }
        public string heightcm              { get; set; }
        public string lengthm               { get; set; }
        public string lengthcm              { get; set; }
        public string categoryid            { get; set; }
        public string qty                   { get; set; }
        public string datestamp             { get; set; }
        public string warehouseid           { get; set; }
        public string[] itemaka             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class RentalItem : Item
    {
        public string dailyrate                  { get; set; }
        public string weeklyrate                 { get; set; }
        public string week2rate                  { get; set; }
        public string week3rate                  { get; set; }
        public string week4rate                  { get; set; }
        public string week5rate                  { get; set; }
        public string monthlyrate                { get; set; }
        public string partnumber                 { get; set; }
        public string aisleloc                   { get; set; }
        public string shelfloc                   { get; set; }
        public string nodiscount                 { get; set; }
        public string replacementcost            { get; set; }
        public string trackedby                  { get; set; }
        public string dailycost                  { get; set; }
        public string weeklycost                 { get; set; }
        public string monthlycost                { get; set; }
        public string qtyin                      { get; set; }
        public string dw                         { get; set; }
        public string metered                    { get; set; }
        public string inventorydepartmentorderby { get; set; }
        public string categoryorderby            { get; set; }
        public string subcategoryorderby         { get; set; }
        public string orderby                    { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class SaleItem : Item
    {
        public string inventorydepartmentorderby { get; set; }
        public string categoryorderby            { get; set; }
        public string subcategoryorderby         { get; set; }
        public string orderby                    { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderItem
    {
        public string masteritemid        { get; set; }
        [Required]
        public string masterid            { get; set; }
        public string description         { get; set; }
        public string rentfromdate        { get; set; }
        public string rentfromtime        { get; set; }
        public string renttodate          { get; set; }
        public string renttotime          { get; set; }
        [Required]
        public string qtyordered          { get; set; }
        public string unit                { get; set; }
        public string price               { get; set; }
        public string daysinwk            { get; set; }
        public string notes               { get; set; }
        public string parentid            { get; set; }
        public string unitextended        { get; set; }
        public string periodextended      { get; set; }
        public string weeklyextended      { get; set; }
        public string taxable             { get; set; }
        public string inactive            { get; set; }
        public string itemorder           { get; set; }
        public string packageitemid       { get; set; }
        public string nestedmasteritemid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class HighlyUsedItem
    {
        [Required]
        public string dealid       { get; set; }
        [Required]
        public string departmentid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CompletesAndKits
    {
        public string packageid   { get; set; }
        [Required]
        public string warehouseid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CompleteKit
    {
        public string masterno         { get; set; }
        public string description      { get; set; }
        public string masterid         { get; set; }
        public string packageitemid    { get; set; }
        public string packageid        { get; set; }
        public string primaryflg       { get; set; }
        public string defaultqty       { get; set; }
        public string isoption         { get; set; }
        public string charge           { get; set; }
        public string datestamp        { get; set; }
        public string required         { get; set; }
        public string optioncolor      { get; set; }
        public string itemclass        { get; set; }
        public string availfor         { get; set; }
        public string availfrom        { get; set; }
        public string categoryorderby  { get; set; }
        public string orderby          { get; set; }
        public string itemcolor        { get; set; }
        public string isnestedcomplete { get; set; }
        public string iteminactive     { get; set; }
        public string warehouseid      { get; set; }
        public string parentid         { get; set; }
        public string packageitemclass { get; set; }
        public string[] itemaka        { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}