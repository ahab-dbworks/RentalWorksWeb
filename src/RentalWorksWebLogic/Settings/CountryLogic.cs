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
        [FwBusinessLogicField(isTitle: true)]
        public string Country { get { return country.Country; } set { country.Country = value; } }
        public string CountryCode { get { return country.CountryCode; } set { country.CountryCode = value; } }
        public string IsUSA { get { return country.IsUSA; } set { country.IsUSA = value; } }
        public string Metric { get { return country.Metric; } set { country.Metric = value; } }
        public string Inactive { get { return country.Inactive; } set { country.Inactive = value; } }
        public DateTime? DateStamp { get { return country.DateStamp; } set { country.DateStamp = value; } }
        //------------------------------------------------------------------------------------



    }




}
