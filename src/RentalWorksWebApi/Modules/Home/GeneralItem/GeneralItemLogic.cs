using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.Home.GeneralItem
{
    public class GeneralItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GeneralItemLoader generalItemLoader = new GeneralItemLoader();
        public GeneralItemLogic()
        {
            dataLoader = generalItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        public string Description { get; set; }
        [JsonIgnore]
        public string AvailFor { get; set; }
        public string TypeId { get; set; }
        public string Type { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public string Classification { get; set; }
        public string ClassificationDescription { get; set; }
        public string ClassificationColor { get; set; }
        public string UnitId { get; set; }
        public string Unit { get; set; }
        public string UnitType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
