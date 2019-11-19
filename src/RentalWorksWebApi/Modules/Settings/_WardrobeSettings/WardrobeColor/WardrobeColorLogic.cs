using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Settings.Color;
using WebLibrary;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeColor
{
    [FwLogic(Id:"Zi11fAucSuwI0")]
    public class WardrobeColorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ColorRecord wardrobeColor = new ColorRecord();
        WardrobeColorLoader wardrobeColorLoader = new WardrobeColorLoader();
        public WardrobeColorLogic()
        {
            dataRecords.Add(wardrobeColor);
            dataLoader = wardrobeColorLoader;
            ColorType = RwConstants.COLOR_TYPE_WARDROBE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"7YshxUROXu9Ob", IsPrimaryKey:true)]
        public string WardrobeColorId { get { return wardrobeColor.ColorId; } set { wardrobeColor.ColorId = value; } }

        [FwLogicProperty(Id:"7YshxUROXu9Ob", IsRecordTitle:true)]
        public string WardrobeColor { get { return wardrobeColor.Color; } set { wardrobeColor.Color = value; } }

        [FwLogicProperty(Id:"gNuzmtKqq")]
        public string ColorType { get { return wardrobeColor.ColorType; } set { wardrobeColor.ColorType = value; } }

        [FwLogicProperty(Id:"9QiXGp4mpB")]
        public bool? Inactive { get { return wardrobeColor.Inactive; } set { wardrobeColor.Inactive = value; } }

        [FwLogicProperty(Id:"uSnoTuyrMP")]
        public string DateStamp { get { return wardrobeColor.DateStamp; } set { wardrobeColor.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
