using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.HomeControls.ItemDimension;
using WebApi.Modules.HomeControls.Master;

namespace WebApi.Modules.Settings.Space
{
    [FwLogic(Id:"aDT4UhktuYRZR")]
    public class SpaceLogic : AppBusinessLogic
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
            BeforeValidate += OnBeforeValidate;
        }
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id:"mjmdglazt8ON")]
        //public string LocationId { get; set; }

        [FwLogicProperty(Id:"zQ37HDeuluPmi", IsPrimaryKey:true)]
        public string SpaceId { get { return space.MasterId; } set { space.MasterId = value; } }

        [FwLogicProperty(Id:"lWckKy7QRCQL")]
        public string BuildingId { get { return space.BuildingId; } set { space.BuildingId = value; } }

        [FwLogicProperty(Id:"29lLg77Mxt0LC", IsReadOnly:true)]
        public string Building { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"EU3Zrr4kdZDv")]
        public string BuildingType { get; set; }

        [FwLogicProperty(Id:"H742wg6DygUJA", IsReadOnly:true)]
        public string FloorId { get { return space.FloorId; } set { space.FloorId = value; } }

        [FwLogicProperty(Id:"H742wg6DygUJA", IsReadOnly:true)]
        public string Floor { get; set; }

        [FwLogicProperty(Id:"zQ37HDeuluPmi", IsReadOnly:true)]
        public string Space { get { return space.Description; } set { space.Description = value; } }

        [FwLogicProperty(Id:"3gha3cuXVUJM")]
        public int? OrderBy { get { return space.OrderBy; } set { space.OrderBy = value; } }

        [FwLogicProperty(Id:"bEy07igAy6Ht")]
        public string BuildingSpace { get; set; }

        [FwLogicProperty(Id:"29lLg77Mxt0LC", IsReadOnly:true)]
        public string BuildingFloorSpace { get; set; }

        [FwLogicProperty(Id:"OCstCrqSFO3k")]
        public decimal? SquareFeet { get { return space.SquareFeet; } set { space.SquareFeet = value; } }

        [FwLogicProperty(Id:"Zlru0E6xFewN")]
        public string SpaceFromDate { get { return space.SpaceFromDate; } set { space.SpaceFromDate = value; } }

        [FwLogicProperty(Id:"4NVdqQuUI6KQ")]
        public string SpaceToDate { get { return space.SpaceToDate; } set { space.SpaceToDate = value; } }

        [FwLogicProperty(Id:"oNMbZc3ksrcn")]
        public bool? CommonSquareFeet { get { return space.CommonSquareFeet; } set { space.CommonSquareFeet = value; } }

        [FwLogicProperty(Id:"Yn51Ota6FXYl")]
        public string PrimaryDimensionId { get { return space.PrimaryDimensionId; } set { space.PrimaryDimensionId = value; } }

        [FwLogicProperty(Id:"Df5qAvNeqNoV")]
        public int? WidthFt { get { return primaryDimension.WidthFt; } set { primaryDimension.WidthFt = value; } }

        [FwLogicProperty(Id:"dxA57W4IhJVg")]
        public int? HeightFt { get { return primaryDimension.HeightFt; } set { primaryDimension.HeightFt = value; } }

        [FwLogicProperty(Id:"4ynGBbcOwYkc")]
        public int? LengthFt { get { return primaryDimension.LengthFt; } set { primaryDimension.LengthFt = value; } }

        [FwLogicProperty(Id:"esu3jqzoohrO")]
        public int? Occupancy { get { return space.Occupancy; } set { space.Occupancy = value; } }

        //[FwLogicProperty(Id:"zwlWAkApMeoo")]
        //public string Chg1 { get; set; }

        //[FwLogicProperty(Id:"AisUt7zxHrn9")]
        //public string Chg2 { get; set; }

        //[FwLogicProperty(Id:"w1jqoEq4CSsG")]
        //public string Chg3 { get; set; }

        //[FwLogicProperty(Id:"3TCtUAkPpf3m")]
        //public string Chg4 { get; set; }

        //[FwLogicProperty(Id:"RvpJauulRe3T")]
        //public string Chg5 { get; set; }

        //[FwLogicProperty(Id:"DKTP1amovOCH")]
        //public string Chg6 { get; set; }

        //[FwLogicProperty(Id:"PBJZwbHsioZ8")]
        //public string Chg7 { get; set; }

        //[FwLogicProperty(Id:"U7ANtpDAolZF")]
        //public string Chg8 { get; set; }

        //[FwLogicProperty(Id:"HHJY90fQQN4Q")]
        //public string Chg9 { get; set; }

        //[FwLogicProperty(Id:"AicXBm6LbXEq")]
        //public string Chg10 { get; set; }

        //[FwLogicProperty(Id:"dAdiuBeke5lK")]
        //public int? Orderbybuilding { get; set; }

        //[FwLogicProperty(Id:"Mfepo1XAKtxV")]
        //public int? Orderbyfloor { get; set; }

        //[FwLogicProperty(Id:"jW5nYlqJT010")]
        //public int? Orderbyroom { get; set; }

        [FwLogicProperty(Id:"HCP3RPU2WDt2")]
        public bool? Inactive { get { return space.Inactive; } set { space.Inactive = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeValidate(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // FloorId should really not be blank. But I'm adding this line to prevent a failure on the Duplicate Check
                if (string.IsNullOrEmpty(FloorId))
                {
                    FloorId = RwConstants.NONE;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    if ((string.IsNullOrEmpty(FloorId)) || (FloorId.Equals(RwConstants.NONE)))
                    {
                        isValid = false;
                        validateMsg = "Select a Floor for this Space to belong to.";
                    }
                }
                else //smUpdate
                {
                    SpaceLogic orig = null;
                    string floorId = FloorId;
                    if (original != null)
                    {
                        orig = (SpaceLogic)original;
                        floorId = floorId ?? orig.FloorId;
                    }
                    if (string.IsNullOrEmpty(floorId))
                    {
                        isValid = false;
                        validateMsg = "Select a Floor for this Space to belong to.";
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
    }
}
