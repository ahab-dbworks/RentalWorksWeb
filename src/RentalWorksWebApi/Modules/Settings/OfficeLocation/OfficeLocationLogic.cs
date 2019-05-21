using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.OfficeLocation
{
    [FwLogic(Id:"SsvsPskrsl1v")]
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
        [FwLogicProperty(Id:"0Hj1fA4EoP7P", IsPrimaryKey:true)]
        public string LocationId { get { return location.LocationId; } set { location.LocationId = value; } }

        [FwLogicProperty(Id:"0Hj1fA4EoP7P", IsRecordTitle:true)]
        public string Location { get { return location.Location; } set { location.Location = value; } }

        [FwLogicProperty(Id:"s72vwwq6Bvd")]
        public string LocationCode { get { return location.LocationCode; } set { location.LocationCode = value; } }

        [FwLogicProperty(Id: "ZeOmhYbh0kY6D")]
        public string CompanyName { get { return location.CompanyName; } set { location.CompanyName = value; } }

        [FwLogicProperty(Id: "lGRLyD6Zq20")]
        public string RateType { get { return location.RateType; } set { location.RateType = value; } }

        [FwLogicProperty(Id: "17YN2fTfnBCZH")]
        public string Color { get { return location.Color; } set { location.Color = value; } }

        [FwLogicProperty(Id:"iU02kRoiKzq")]
        public string DefaultPurchasePoTypeId { get { return location.DefaultPurchasePoTypeId; } set { location.DefaultPurchasePoTypeId = value; } }

        [FwLogicProperty(Id:"BuPhPT7D3XD")]
        public string DefaultPurchasePoType { get; set; }

        [FwLogicProperty(Id:"GDXu3A4umCh")]
        public bool? Inactive { get { return location.Inactive; } set { location.Inactive = value; } }

        [FwLogicProperty(Id:"TWUrrSVl5Ll")]
        public string DateStamp { get { return location.DateStamp; } set { location.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
