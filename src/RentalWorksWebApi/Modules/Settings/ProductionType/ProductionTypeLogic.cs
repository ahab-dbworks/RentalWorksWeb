using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.ProductionType
{
    public class ProductionTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ProductionTypeRecord productionType = new ProductionTypeRecord();
        public ProductionTypeLogic()
        {
            dataRecords.Add(productionType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProductionTypeId { get { return productionType.ProductionTypeId; } set { productionType.ProductionTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProductionType { get { return productionType.ProductionType; } set { productionType.ProductionType = value; } }
        public string ProductionTypeCode { get { return productionType.ProductionTypeCode; } set { productionType.ProductionTypeCode = value; } }
        public bool? Inactive { get { return productionType.Inactive; } set { productionType.Inactive = value; } }
        public string DateStamp { get { return productionType.DateStamp; } set { productionType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
