using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.DiscountTemplate
{
    public class DiscountTemplateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DiscountTemplateRecord discountTemplate = new DiscountTemplateRecord();
        DiscountTemplateLoader discountTemplateLoader = new DiscountTemplateLoader();
        public DiscountTemplateLogic()
        {
            dataRecords.Add(discountTemplate);
            dataLoader = discountTemplateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DiscountTemplateId { get { return discountTemplate.DiscountTemplateId; } set { discountTemplate.DiscountTemplateId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string DiscountTemplate { get { return discountTemplate.DiscountTemplate; } set { discountTemplate.DiscountTemplate = value; } }
        public string LocationId { get { return discountTemplate.LocationId; } set { discountTemplate.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public bool? IsCompany { get { return discountTemplate.IsCompany; } set { discountTemplate.IsCompany = value; } }
        public bool? Rental { get { return discountTemplate.Rental; } set { discountTemplate.Rental = value; } }
        public bool? Sales { get { return discountTemplate.Sales; } set { discountTemplate.Sales = value; } }
        public bool? Labor { get { return discountTemplate.Labor; } set { discountTemplate.Labor = value; } }
        public bool? Misc { get { return discountTemplate.Misc; } set { discountTemplate.Misc = value; } }
        public bool? Space { get { return discountTemplate.Space; } set { discountTemplate.Space = value; } }
        public decimal? RentalDiscountPercent { get { return discountTemplate.RentalDiscountPercent; } set { discountTemplate.RentalDiscountPercent = value; } }
        public decimal? RentalDaysPerWeek { get { return discountTemplate.RentalDaysPerWeek; } set { discountTemplate.RentalDaysPerWeek = value; } }
        public decimal? SalesDiscountPercent { get { return discountTemplate.SalesDiscountPercent; } set { discountTemplate.SalesDiscountPercent = value; } }
        public decimal? SpaceDiscountPercent { get { return discountTemplate.SpaceDiscountPercent; } set { discountTemplate.SpaceDiscountPercent = value; } }
        public string RentalAsOf { get { return discountTemplate.RentalAsOf; } set { discountTemplate.RentalAsOf = value; } }
        public string SalesAsOf { get { return discountTemplate.SalesAsOf; } set { discountTemplate.SalesAsOf = value; } }
        public string LaborAsOf { get { return discountTemplate.LaborAsOf; } set { discountTemplate.LaborAsOf = value; } }
        public string MiscAsOf { get { return discountTemplate.MiscAsOf; } set { discountTemplate.MiscAsOf = value; } }
        public string SpaceAsOf { get { return discountTemplate.SpaceAsOf; } set { discountTemplate.SpaceAsOf = value; } }
        public decimal? SpaceDaysPerWeek { get { return discountTemplate.SpaceDaysPerWeek; } set { discountTemplate.SpaceDaysPerWeek = value; } }
        public string CompanyId { get { return discountTemplate.CompanyId; } set { discountTemplate.CompanyId = value; } }
        public bool? ApplyDiscountToCustomRate { get { return discountTemplate.ApplyDiscountToCustomRate; } set { discountTemplate.ApplyDiscountToCustomRate = value; } }
        public bool? Inactive { get { return discountTemplate.Inactive; } set { discountTemplate.Inactive = value; } }
        public string DateStamp { get { return discountTemplate.DateStamp; } set { discountTemplate.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}