using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.OfficeLocation
{
    public class OfficeLocationLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        OfficeLocationRecord location = new OfficeLocationRecord();
        public OfficeLocationLogic()
        {
            dataRecords.Add(location);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string LocationId { get { return location.LocationId; } set { location.LocationId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Location { get { return location.Location; } set { location.Location = value; } }
        public string LocationCode { get { return location.LocationCode; } set { location.LocationCode = value; } }
        public bool Inactive { get { return location.Inactive; } set { location.Inactive = value; } }
        //------------------------------------------------------------------------------------
    }

}
