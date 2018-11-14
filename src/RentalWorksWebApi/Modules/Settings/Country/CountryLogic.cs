using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Country
{
    [FwLogic(Id:"s366HPMxvrtP")]
    public class CountryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CountryRecord country = new CountryRecord();
        public CountryLogic()
        {
            dataRecords.Add(country);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"YJ3w4sv0Um92", IsPrimaryKey:true)]
        public string CountryId { get { return country.CountryId; } set { country.CountryId = value; } }

        [FwLogicProperty(Id:"YJ3w4sv0Um92", IsRecordTitle:true)]
        public string Country { get { return country.Country; } set { country.Country = value; } }

        [FwLogicProperty(Id:"ORHzkjem4wQk")]
        public string CountryCode { get { return country.CountryCode; } set { country.CountryCode = value; } }

        [FwLogicProperty(Id:"NsaHZVM8nSU4")]
        public bool? IsUSA { get { return country.IsUSA; } set { country.IsUSA = value; } }

        [FwLogicProperty(Id:"lR3RaR26pl5w")]
        public bool? Metric { get { return country.Metric; } set { country.Metric = value; } }

        [FwLogicProperty(Id:"jnjD2LTLEl3e")]
        public bool? Inactive { get { return country.Inactive; } set { country.Inactive = value; } }

        [FwLogicProperty(Id:"LXdoMM3YfcI8")]
        public string DateStamp { get { return country.DateStamp; } set { country.DateStamp = value; } }

        //------------------------------------------------------------------------------------



    }




}
