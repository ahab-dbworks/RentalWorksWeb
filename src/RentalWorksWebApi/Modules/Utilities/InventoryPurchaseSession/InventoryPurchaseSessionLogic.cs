using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.Utilities.InventoryPurchaseUtility;

namespace WebApi.Modules.Utilities.InventoryPurchaseSession
{
    [FwLogic(Id: "TvWTInyM36Hfe")]
    public class InventoryPurchaseSessionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryPurchaseSessionLoader temporaryBarCodeSessionLoader = new InventoryPurchaseSessionLoader();
        public InventoryPurchaseSessionLogic()
        {
            dataLoader = temporaryBarCodeSessionLoader;
            InsteadOfDelete += OnInsteadOfDelete;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "TWQRFAqTFJNXN", IsReadOnly: true, IsPrimaryKey: true)]
        public string SessionId { get; set; }
        [FwLogicProperty(Id: "tX65CY5pqEUGE", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "TX6OvinEbCOx8", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "TXHRU5fhySRrO", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "TY74oM0i4Z70o", IsReadOnly: true)]
        public string Barcodes { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnInsteadOfDelete(object sender, InsteadOfDeleteEventArgs e)
        {
            e.Success = InventoryPurchaseUtilityFunc.DeleteSession(AppConfig, UserSession, SessionId).Result;
        }
        //------------------------------------------------------------------------------------
    }
}
