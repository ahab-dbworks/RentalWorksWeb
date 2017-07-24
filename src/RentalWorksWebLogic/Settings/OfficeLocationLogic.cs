using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
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
        public string Location { get { return location.Location; } set { location.Location = value; } }
        public string LocationCode { get { return location.LocationCode; } set { location.LocationCode = value; } }
        public bool Inactive { get { return location.Inactive; } set { location.Inactive = value; } }
        //------------------------------------------------------------------------------------
    }

}
