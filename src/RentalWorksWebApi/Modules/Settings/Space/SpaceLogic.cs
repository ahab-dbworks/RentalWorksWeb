using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.ItemDimension;
using WebApi.Modules.Home.Master;

namespace WebApi.Modules.Settings.Space
{
    public class SpaceLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterRecord space = new MasterRecord();
        ItemDimensionRecord primaryDimension = new ItemDimensionRecord();
        SpaceLoader spaceLoader = new SpaceLoader();
        public SpaceLogic()
        {
            dataRecords.Add(space);
            dataRecords.Add(primaryDimension);
            dataLoader = spaceLoader;
        }
        //------------------------------------------------------------------------------------ 
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string LocationId { get; set; }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SpaceId { get { return space.MasterId; } set { space.MasterId = value; } }
        public string BuildingId { get { return space.BuildingId; } set { space.BuildingId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Building { get; set; }
        [JsonIgnore]
        public string BuildingType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FloorId { get { return space.FloorId; } set { space.FloorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Floor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Space { get { return space.Description; } set { space.Description = value; } }
        public int? OrderBy { get { return space.OrderBy; } set { space.OrderBy = value; } }
        public string BuildingSpace { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BuildingFloorSpace { get; set; }
        public decimal? SquareFeet { get { return space.SquareFeet; } set { space.SquareFeet = value; } }
        public string SpaceFromDate { get { return space.SpaceFromDate; } set { space.SpaceFromDate = value; } }
        public string SpaceToDate { get { return space.SpaceToDate; } set { space.SpaceToDate = value; } }
        public bool? CommonSquareFeet { get { return space.CommonSquareFeet; } set { space.CommonSquareFeet = value; } }
        public string PrimaryDimensionId { get { return space.PrimaryDimensionId; } set { space.PrimaryDimensionId = value; } }
        public int? WidthFt { get { return primaryDimension.WidthFt; } set { primaryDimension.WidthFt = value; } }
        public int? HeightFt { get { return primaryDimension.HeightFt; } set { primaryDimension.HeightFt = value; } }
        public int? LengthFt { get { return primaryDimension.LengthFt; } set { primaryDimension.LengthFt = value; } }
        public int? Occupancy { get { return space.Occupancy; } set { space.Occupancy = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        //public string Chg1 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg2 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg3 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg4 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg5 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg6 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg7 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg8 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg9 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Chg10 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Orderbybuilding { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Orderbyfloor { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Orderbyroom { get; set; }
        public bool? Inactive { get { return space.Inactive; } set { space.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
    }
}