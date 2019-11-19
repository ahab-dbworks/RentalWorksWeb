using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DealSettings.DealType
{
    [FwLogic(Id:"CutJroZ4lNKc")]
    public class DealTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealTypeRecord dealType = new DealTypeRecord();
        public DealTypeLogic()
        {
            dataRecords.Add(dealType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"hpl4N4ctvvLk", IsPrimaryKey:true)]
        public string DealTypeId { get { return dealType.DealTypeId; } set { dealType.DealTypeId = value; } }

        [FwLogicProperty(Id:"hpl4N4ctvvLk", IsRecordTitle:true)]
        public string DealType { get { return dealType.DealType; } set { dealType.DealType = value; } }

        [FwLogicProperty(Id:"xOHp8xIT0qEb")]
        public string Color { get { return dealType.Color;  } set { dealType.Color = value; } }

        [FwLogicProperty(Id:"Ib4L2thiNO4k")]
        public bool? WhiteText { get { return dealType.WhiteText; } set { dealType.WhiteText = value; } }

        [FwLogicProperty(Id:"2aF0mDozn2dL")]
        public string GlPrefix { get { return dealType.GlPrefix; } set { dealType.GlPrefix = value; } }

        [FwLogicProperty(Id:"HKF0neqfA2EC")]
        public string GlSuffix { get { return dealType.GlSuffix; } set { dealType.GlSuffix = value; } }

        [FwLogicProperty(Id:"Ocg1xjw7P4M9")]
        public bool? TheatricalProduction { get { return dealType.TheatricalProduction; } set { dealType.TheatricalProduction = value; } }

        [FwLogicProperty(Id:"7hseHexJAoRC")]
        public bool? Inactive { get { return dealType.Inactive; } set { dealType.Inactive = value; } }

        [FwLogicProperty(Id:"t0zzPu3ccOAA")]
        public string DateStamp { get { return dealType.DateStamp; } set { dealType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
