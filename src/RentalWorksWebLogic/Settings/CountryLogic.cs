using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class CountryLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CountryRecord country = new CountryRecord();
        public CountryLogic()
        {
            dataRecords.Add(country);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CountryId { get { return country.CountryId; } set { country.CountryId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Country { get { return country.Country; } set { country.Country = value; } }
        public string CountryCode { get { return country.CountryCode; } set { country.CountryCode = value; } }
        public bool IsUSA { get { return country.IsUSA; } set { country.IsUSA = value; } }
        public bool Metric { get { return country.Metric; } set { country.Metric = value; } }
        public bool Inactive { get { return country.Inactive; } set { country.Inactive = value; } }
        public string DateStamp { get { return country.DateStamp; } set { country.DateStamp = value; } }
        //------------------------------------------------------------------------------------



    }




}
