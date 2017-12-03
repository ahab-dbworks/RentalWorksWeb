using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.DealType
{
    public class DealTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealTypeRecord dealType = new DealTypeRecord();
        public DealTypeLogic()
        {
            dataRecords.Add(dealType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealTypeId { get { return dealType.DealTypeId; } set { dealType.DealTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string DealType { get { return dealType.DealType; } set { dealType.DealType = value; } }
        public string Color { get { return dealType.Color;  } set { dealType.Color = value; } }
        public bool? WhiteText { get { return dealType.WhiteText; } set { dealType.WhiteText = value; } }
        public string GlPrefix { get { return dealType.GlPrefix; } set { dealType.GlPrefix = value; } }
        public string GlSuffix { get { return dealType.GlSuffix; } set { dealType.GlSuffix = value; } }
        public bool? TheatricalProduction { get { return dealType.TheatricalProduction; } set { dealType.TheatricalProduction = value; } }
        public bool? Inactive { get { return dealType.Inactive; } set { dealType.Inactive = value; } }
        public string DateStamp { get { return dealType.DateStamp; } set { dealType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
