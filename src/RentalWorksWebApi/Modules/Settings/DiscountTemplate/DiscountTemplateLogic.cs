using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.DiscountTemplate
{
    [FwLogic(Id:"RNWyfHOA9ZF0")]
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
        [FwLogicProperty(Id:"hpMRbT9Uy0m8", IsPrimaryKey:true)]
        public string DiscountTemplateId { get { return discountTemplate.DiscountTemplateId; } set { discountTemplate.DiscountTemplateId = value; } }

        [FwLogicProperty(Id:"hpMRbT9Uy0m8", IsRecordTitle:true)]
        public string DiscountTemplate { get { return discountTemplate.DiscountTemplate; } set { discountTemplate.DiscountTemplate = value; } }

        [FwLogicProperty(Id:"ArUtQy8HB0cE")]
        public string OfficeLocationId { get { return discountTemplate.OfficeLocationId; } set { discountTemplate.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"3gPd8bD7CGmQ", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"YY8obcU6REQV")]
        public bool? IsCompany { get { return discountTemplate.IsCompany; } set { discountTemplate.IsCompany = value; } }

        [FwLogicProperty(Id:"4CIuehdaOWF5")]
        public bool? Rental { get { return discountTemplate.Rental; } set { discountTemplate.Rental = value; } }

        [FwLogicProperty(Id:"3KBjnB9psAyq")]
        public bool? Sales { get { return discountTemplate.Sales; } set { discountTemplate.Sales = value; } }

        [FwLogicProperty(Id:"S76WHaUtPCVC")]
        public bool? Labor { get { return discountTemplate.Labor; } set { discountTemplate.Labor = value; } }

        [FwLogicProperty(Id:"oBDGLkUtEtKZ")]
        public bool? Misc { get { return discountTemplate.Misc; } set { discountTemplate.Misc = value; } }

        [FwLogicProperty(Id:"l0MaE07aRn8N")]
        public bool? Space { get { return discountTemplate.Space; } set { discountTemplate.Space = value; } }

        [FwLogicProperty(Id:"e2sRs6g12ZCo")]
        public decimal? RentalDiscountPercent { get { return discountTemplate.RentalDiscountPercent; } set { discountTemplate.RentalDiscountPercent = value; } }

        [FwLogicProperty(Id:"JJfKc328SumZ")]
        public decimal? RentalDaysPerWeek { get { return discountTemplate.RentalDaysPerWeek; } set { discountTemplate.RentalDaysPerWeek = value; } }

        [FwLogicProperty(Id:"6oaWEiiT03hu")]
        public decimal? SalesDiscountPercent { get { return discountTemplate.SalesDiscountPercent; } set { discountTemplate.SalesDiscountPercent = value; } }

        [FwLogicProperty(Id:"G8DTeozORJvY")]
        public decimal? SpaceDiscountPercent { get { return discountTemplate.SpaceDiscountPercent; } set { discountTemplate.SpaceDiscountPercent = value; } }

        [FwLogicProperty(Id:"bzKnjfNGL8X2")]
        public string RentalAsOf { get { return discountTemplate.RentalAsOf; } set { discountTemplate.RentalAsOf = value; } }

        [FwLogicProperty(Id:"FFnULuK0juwu")]
        public string SalesAsOf { get { return discountTemplate.SalesAsOf; } set { discountTemplate.SalesAsOf = value; } }

        [FwLogicProperty(Id:"JD1EeYYkphj9")]
        public string LaborAsOf { get { return discountTemplate.LaborAsOf; } set { discountTemplate.LaborAsOf = value; } }

        [FwLogicProperty(Id:"exQNJqGHCATG")]
        public string MiscAsOf { get { return discountTemplate.MiscAsOf; } set { discountTemplate.MiscAsOf = value; } }

        [FwLogicProperty(Id:"isBuHnEuupHD")]
        public string SpaceAsOf { get { return discountTemplate.SpaceAsOf; } set { discountTemplate.SpaceAsOf = value; } }

        [FwLogicProperty(Id:"mB7bPWS8HgvA")]
        public decimal? SpaceDaysPerWeek { get { return discountTemplate.SpaceDaysPerWeek; } set { discountTemplate.SpaceDaysPerWeek = value; } }

        [FwLogicProperty(Id:"MSuSAvDN0nvM")]
        public string CompanyId { get { return discountTemplate.CompanyId; } set { discountTemplate.CompanyId = value; } }

        [FwLogicProperty(Id:"cksyNfrwDWi4")]
        public bool? ApplyDiscountToCustomRate { get { return discountTemplate.ApplyDiscountToCustomRate; } set { discountTemplate.ApplyDiscountToCustomRate = value; } }

        [FwLogicProperty(Id:"LdF5QstRBjKl")]
        public bool? Inactive { get { return discountTemplate.Inactive; } set { discountTemplate.Inactive = value; } }

        [FwLogicProperty(Id:"7zRXPQ57Nxo0")]
        public string DateStamp { get { return discountTemplate.DateStamp; } set { discountTemplate.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
