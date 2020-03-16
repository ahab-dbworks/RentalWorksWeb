using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [FwLogic(Id: "SsvsPskrsl1v")]
    public class OfficeLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        OfficeLocationRecord location = new OfficeLocationRecord();
        OfficeLocationLoader locationLoader = new OfficeLocationLoader();

        public OfficeLocationLogic()
        {
            dataRecords.Add(location);
            dataLoader = locationLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "0Hj1fA4EoP7P", IsPrimaryKey: true)]
        public string LocationId { get { return location.LocationId; } set { location.LocationId = value; } }

        [FwLogicProperty(Id: "0Hj1fA4EoP7P", IsRecordTitle: true)]
        public string Location { get { return location.Location; } set { location.Location = value; } }

        [FwLogicProperty(Id: "s72vwwq6Bvd")]
        public string LocationCode { get { return location.LocationCode; } set { location.LocationCode = value; } }

        [FwLogicProperty(Id: "ZeOmhYbh0kY6D")]
        public string CompanyName { get { return location.CompanyName; } set { location.CompanyName = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string Address1 { get { return location.Address1; } set { location.Address1 = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string Address2 { get { return location.Address2; } set { location.Address2 = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string City { get { return location.City; } set { location.City = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string Zip { get { return location.Zip; } set { location.Zip = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string State { get { return location.State; } set { location.State = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string CountryId { get { return location.CountryId; } set { location.CountryId = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx", IsReadOnly: true)]
        public string Country { get; set; }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string Phone { get { return location.Phone; } set { location.Phone = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string Fax { get { return location.Fax; } set { location.Fax = value; } }

        [FwLogicProperty(Id: "vkQn6TgUTnDWo")]
        public string Email { get { return location.Email; } set { location.Email = value; } }

        [FwLogicProperty(Id: "U5purbrsmbnUJ")]
        public string WebAddress { get { return location.WebAddress; } set { location.WebAddress = value; } }

        [FwLogicProperty(Id: "lGRLyD6Zq20")]
        public string RateType { get { return location.RateType; } set { location.RateType = value; } }

        [FwLogicProperty(Id: "17YN2fTfnBCZH")]
        public string Color { get { return location.Color; } set { location.Color = value; } }

        [FwLogicProperty(Id: "iU02kRoiKzq")]
        public string DefaultPurchasePoTypeId { get { return location.DefaultPurchasePoTypeId; } set { location.DefaultPurchasePoTypeId = value; } }

        [FwLogicProperty(Id: "BuPhPT7D3XD", IsReadOnly: true)]
        public string DefaultPurchasePoType { get; set; }

        [FwLogicProperty(Id: "KUV4zO00aBt0v")]
        public string GlPrefix { get { return location.GlPrefix; } set { location.GlPrefix = value; } }

        [FwLogicProperty(Id: "kQSgwE143S0sR")]
        public string GlSuffix { get { return location.GlSuffix; } set { location.GlSuffix = value; } }

        [FwLogicProperty(Id: "7sJFv7oKrwuXx")]
        public bool? UseNumberPrefix { get { return location.UseNumberPrefix; } set { location.UseNumberPrefix = value; } }

        [FwLogicProperty(Id: "Zb3GsDaZZLFJq")]
        public string NumberPrefix { get { return location.NumberPrefix; } set { location.NumberPrefix = value; } }

        [FwLogicProperty(Id: "m5RhkDjMvklnT")]
        public bool? UseRequisitionNumbers { get { return location.UseRequisitionNumbers; } set { location.UseRequisitionNumbers = value; } }

        [FwLogicProperty(Id: "2iDk3wdztJqYE")]
        public bool? UseSameNumberForQuoteAndOrder { get { return location.UseSameNumberForQuoteAndOrder; } set { location.UseSameNumberForQuoteAndOrder = value; } }

        [FwLogicProperty(Id: "XyYSpJdYXeBWY")]
        public bool? UseSameNumberForAllExportBatches { get { return location.UseSameNumberForAllExportBatches; } set { location.UseSameNumberForAllExportBatches = value; } }

        [FwLogicProperty(Id: "FaYkG70UZGgIc")]
        public bool? UserOrderNumberAndSuffixForInvoice { get { return location.UserOrderNumberAndSuffixForInvoice; } set { location.UserOrderNumberAndSuffixForInvoice = value; } }

        [FwLogicProperty(Id: "oUX9anIe2VxyM")]
        public bool? UseHInHiatusInvoiceNumbers { get { return location.UseHInHiatusInvoiceNumbers; } set { location.UseHInHiatusInvoiceNumbers = value; } }

        [FwLogicProperty(Id: "vOrKm2KiBUh1p")]
        public string DefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "Lr66u6QGlFEH0", IsReadOnly: true)]
        public string DefaultCurrencyCode { get; set; }

        [FwLogicProperty(Id: "TNVs0dD8Wk87t", IsReadOnly: true)]
        public string DefaultCurrency { get; set; }

        [FwLogicProperty(Id: "GDXu3A4umCh")]
        public bool? Inactive { get { return location.Inactive; } set { location.Inactive = value; } }

        [FwLogicProperty(Id: "TWUrrSVl5Ll")]
        public string DateStamp { get { return location.DateStamp; } set { location.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
