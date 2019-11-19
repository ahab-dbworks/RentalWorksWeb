using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.RegionSettings.Region
{
    [FwLogic(Id:"SV7CVgc2K0JeX")]
    public class RegionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RegionRecord region = new RegionRecord();
        public RegionLogic()
        {
            dataRecords.Add(region);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"xs4WeWc2wc60K", IsPrimaryKey:true)]
        public string RegionId { get { return region.RegionId; } set { region.RegionId = value; } }

        [FwLogicProperty(Id:"xs4WeWc2wc60K", IsRecordTitle:true)]
        public string Region { get { return region.Region; } set { region.Region = value; } }

        [FwLogicProperty(Id:"6ZOnzOgIznDN")]
        public bool? Inactive { get { return region.Inactive; } set { region.Inactive = value; } }

        [FwLogicProperty(Id:"nHxLc75NFgEw")]
        public string DateStamp { get { return region.DateStamp; } set { region.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
