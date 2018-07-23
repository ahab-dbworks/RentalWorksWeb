using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.OfficeLocation
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string LocationId { get { return location.LocationId; } set { location.LocationId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Location { get { return location.Location; } set { location.Location = value; } }
        public string LocationCode { get { return location.LocationCode; } set { location.LocationCode = value; } }
        public string DefaultPurchasePoTypeId { get { return location.DefaultPurchasePoTypeId; } set { location.DefaultPurchasePoTypeId = value; } }
        public string DefaultPurchasePoType { get; set; }
        public bool? Inactive { get { return location.Inactive; } set { location.Inactive = value; } }
        public string DateStamp { get { return location.DateStamp; } set { location.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
