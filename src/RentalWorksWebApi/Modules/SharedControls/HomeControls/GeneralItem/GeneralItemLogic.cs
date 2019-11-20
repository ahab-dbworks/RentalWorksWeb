using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.GeneralItem
{
    [FwLogic(Id:"RjtQOZGRynn4")]
    public class GeneralItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GeneralItemLoader generalItemLoader = new GeneralItemLoader();
        public GeneralItemLogic()
        {
            dataLoader = generalItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"OllTRNIFsTDM", IsPrimaryKey:true)]
        public string ItemId { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"hYeEdnycZDZd")]
        public string ICode { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"8EMfqweWdvRj")]
        public string Description { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"Gg1vZrs1TQta")]
        public string AvailFor { get; set; }

        [FwLogicProperty(Id:"9PCGZk3k3emm")]
        public string TypeId { get; set; }

        [FwLogicProperty(Id:"nDIyu4R2hIua")]
        public string Type { get; set; }

        [FwLogicProperty(Id:"vPvU2hYVRNYc")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"bSza07B4Oppg")]
        public string Category { get; set; }

        [FwLogicProperty(Id:"lAEWSeZqM5g2")]
        public string SubCategoryId { get; set; }

        [FwLogicProperty(Id:"204vmiNyUA1A")]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id:"92OQROO2pEDl")]
        public string Classification { get; set; }

        [FwLogicProperty(Id:"C3xz5mN1JBQ9")]
        public string ClassificationDescription { get; set; }

        [FwLogicProperty(Id:"J5ZlFM5eTzXS")]
        public string ClassificationColor { get; set; }

        [FwLogicProperty(Id:"iJuWFAqvTAeo")]
        public string UnitId { get; set; }

        [FwLogicProperty(Id:"0yzhRmXMlp8h")]
        public string Unit { get; set; }

        [FwLogicProperty(Id:"U2lOVay5ywSJ")]
        public string UnitType { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
