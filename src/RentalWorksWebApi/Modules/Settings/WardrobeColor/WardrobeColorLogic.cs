using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.Color;

namespace RentalWorksWebApi.Modules.Settings.WardrobeColor
{
    public class WardrobeColorLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ColorRecord wardrobeColor = new ColorRecord();
        WardrobeColorLoader wardrobeColorLoader = new WardrobeColorLoader();
        public WardrobeColorLogic()
        {
            dataRecords.Add(wardrobeColor);
            dataLoader = wardrobeColorLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeColorId { get { return wardrobeColor.ColorId; } set { wardrobeColor.ColorId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeColor { get { return wardrobeColor.Color; } set { wardrobeColor.Color = value; } }
        public string ColorType { get { return wardrobeColor.ColorType; } set { wardrobeColor.ColorType = value; } }
        public bool? Inactive { get { return wardrobeColor.Inactive; } set { wardrobeColor.Inactive = value; } }
        public string DateStamp { get { return wardrobeColor.DateStamp; } set { wardrobeColor.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            ColorType = "W";
        }
        //------------------------------------------------------------------------------------

    }

}
