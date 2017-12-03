﻿using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Region
{
    public class RegionLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RegionRecord region = new RegionRecord();
        public RegionLogic()
        {
            dataRecords.Add(region);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RegionId { get { return region.RegionId; } set { region.RegionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Region { get { return region.Region; } set { region.Region = value; } }
        public bool? Inactive { get { return region.Inactive; } set { region.Inactive = value; } }
        public string DateStamp { get { return region.DateStamp; } set { region.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
