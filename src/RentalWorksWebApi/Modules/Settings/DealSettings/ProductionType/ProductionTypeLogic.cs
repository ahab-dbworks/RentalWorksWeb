using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DealSettings.ProductionType
{
    [FwLogic(Id:"MKT1DNQpoiJ1g")]
    public class ProductionTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ProductionTypeRecord productionType = new ProductionTypeRecord();
        public ProductionTypeLogic()
        {
            dataRecords.Add(productionType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"ZzGz3qqFhPrfY", IsPrimaryKey:true)]
        public string ProductionTypeId { get { return productionType.ProductionTypeId; } set { productionType.ProductionTypeId = value; } }

        [FwLogicProperty(Id:"ZzGz3qqFhPrfY", IsRecordTitle:true)]
        public string ProductionType { get { return productionType.ProductionType; } set { productionType.ProductionType = value; } }

        [FwLogicProperty(Id:"WNZ6xQY8XNed")]
        public string ProductionTypeCode { get { return productionType.ProductionTypeCode; } set { productionType.ProductionTypeCode = value; } }

        [FwLogicProperty(Id:"rg9QacWGan9d")]
        public bool? Inactive { get { return productionType.Inactive; } set { productionType.Inactive = value; } }

        [FwLogicProperty(Id:"li2NanS9W53n")]
        public string DateStamp { get { return productionType.DateStamp; } set { productionType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
