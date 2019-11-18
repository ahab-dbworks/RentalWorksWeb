using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.SpaceRate
{
    [FwLogic(Id:"MBZE43UrSuKEs")]
    public class SpaceRateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SpaceRateRecord spaceRate = new SpaceRateRecord();
        SpaceRateLoader spaceRateLoader = new SpaceRateLoader();
        public SpaceRateLogic()
        {
            dataRecords.Add(spaceRate);
            dataLoader = spaceRateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"gtglfgcowWii4", IsPrimaryKey:true)]
        public string SpaceRateId { get { return spaceRate.SpaceRateId; } set { spaceRate.SpaceRateId = value; } }

        [FwLogicProperty(Id:"GU1x8WWnvn2Zx", IsReadOnly:true)]
        public string BuildingId { get; set; }

        [FwLogicProperty(Id:"plRnVK7Q3PjhJ", IsReadOnly:true)]
        public string FloorId { get; set; }

        [FwLogicProperty(Id:"UUu6qsPYK2Cp")]
        public string SpaceId { get { return spaceRate.SpaceId; } set { spaceRate.SpaceId = value; } }

        [FwLogicProperty(Id:"mCQY7gm4GkIi")]
        public string FacilityTypeId { get { return spaceRate.FacilityTypeId; } set { spaceRate.FacilityTypeId = value; } }

        [FwLogicProperty(Id:"oFI3JmxphBUFm", IsReadOnly:true)]
        public string FacilityType { get; set; }

        [FwLogicProperty(Id:"Rkw9oUQVfuHV")]
        public string SpaceTypeId { get { return spaceRate.SpaceTypeId; } set { spaceRate.SpaceTypeId = value; } }

        [FwLogicProperty(Id:"HUqYYtwM6nOF6", IsReadOnly:true)]
        public string SpaceType { get; set; }

        [FwLogicProperty(Id:"8hVzEjbOcekE")]
        public string RateId { get { return spaceRate.RateId; } set { spaceRate.RateId = value; } }

        //[FwLogicProperty(Id:"U4aBddaArtPo")]
        //public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"rfBfV69HjVUzH", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"aGqltNZXxDNNh", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"UXzoJDAIqrzuV", IsReadOnly:true)]
        public decimal? Price { get; set; }

        [FwLogicProperty(Id:"dCl6CYBB5vJOZ", IsReadOnly:true)]
        public decimal? HourlyRate { get; set; }

        [FwLogicProperty(Id:"EXQ1mbKXXMAzD", IsReadOnly:true)]
        public decimal? DailyRate { get; set; }

        [FwLogicProperty(Id:"qk1loAJw4h1Pw", IsReadOnly:true)]
        public decimal? WeeklyRate { get; set; }

        [FwLogicProperty(Id:"P06JR5HLfymYH", IsReadOnly:true)]
        public decimal? Week2Rate { get; set; }

        [FwLogicProperty(Id:"fZNKSb1sZGgFK", IsReadOnly:true)]
        public decimal? Week3Rate { get; set; }

        [FwLogicProperty(Id:"cpI78hrjA4RRp", IsReadOnly:true)]
        public decimal? Week4Rate { get; set; }

        [FwLogicProperty(Id:"qMIcIINynHlon", IsReadOnly:true)]
        public decimal? Week5Rate { get; set; }

        [FwLogicProperty(Id:"Ueac7AVduZXmX", IsReadOnly:true)]
        public decimal? MonthlyRate { get; set; }

        [FwLogicProperty(Id:"M2KEhSHUOYGDl", IsReadOnly:true)]
        public bool? StageScheduling { get; set; }

        [FwLogicProperty(Id:"uHlhrvC6a3nSk", IsReadOnly:true)]
        public string UnitId { get; set; }

        [FwLogicProperty(Id:"HUqYYtwM6nOF6", IsReadOnly:true)]
        public string SpaceTypeClassification { get; set; }

        [FwLogicProperty(Id:"rZHWVUXwlXUn")]
        public int? OrderBy { get { return spaceRate.OrderBy; } set { spaceRate.OrderBy = value; } }

        [FwLogicProperty(Id:"1TCi2YiBJNgU")]
        public string DateStamp { get { return spaceRate.DateStamp; } set { spaceRate.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
