using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
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
        [FwBusinessLogicField(isTitle: true)]
        public string DealType { get { return dealType.DealType; } set { dealType.DealType = value; } }
        public decimal Color { get { return dealType.Color;  } set { dealType.Color = value; } }
        public string WhiteText { get { return dealType.WhiteText; } set { dealType.WhiteText = value; } }
        public string GlPrefix { get { return dealType.GlPrefix; } set { dealType.GlPrefix = value; } }
        public string GlSuffix { get { return dealType.GlSuffix; } set { dealType.GlSuffix = value; } }
        public string TheatricalProduction { get { return dealType.TheatricalProduction; } set { dealType.TheatricalProduction = value; } }
        public string Inactive { get { return dealType.Inactive; } set { dealType.Inactive = value; } }
        public DateTime? DateStamp { get { return dealType.DateStamp; } set { dealType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
