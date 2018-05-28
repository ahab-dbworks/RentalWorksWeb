using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Modules.Home.Order;
using System;
using WebLibrary;
using WebApi.Logic;
using FwStandard.SqlServer;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.Quote
{
    public class QuoteLogic : OrderBaseLogic
    {
        QuoteLoader quoteLoader = new QuoteLoader();
        QuoteBrowseLoader quoteBrowseLoader = new QuoteBrowseLoader();
        //------------------------------------------------------------------------------------
        public QuoteLogic()
        {
            dataLoader = quoteLoader;
            browseLoader = quoteBrowseLoader;
            Type = RwConstants.ORDER_TYPE_QUOTE;
            BeforeSave += OnBeforeSave;
            dealOrder.AfterSave += OnAfterSaveDealOrder;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string QuoteId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string QuoteNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        public string QuoteDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
        public int VersionNumber { get { return dealOrder.VersionNumber; } set { dealOrder.VersionNumber = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                StatusDate = FwConvert.ToString(DateTime.Today);
                QuoteDate = FwConvert.ToString(DateTime.Today);
                if ((DealId == null) || (DealId.Equals(string.Empty)))
                {
                    Status = RwConstants.QUOTE_STATUS_PROSPECT;
                }
                else
                {
                    Status = RwConstants.QUOTE_STATUS_ACTIVE;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public override void OnAfterSaveDealOrder(object sender, AfterSaveEventArgs e)
        {
            base.OnAfterSaveDealOrder(sender, e);
            if (e.SavePerformed)
            {
                QuoteLogic l2 = new QuoteLogic();
                l2.SetDependencies(this.AppConfig, this.UserSession);
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<QuoteLogic>(pk).Result;
                BillToAddressId = l2.BillToAddressId;
                TaxId = l2.TaxId;


                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)) && (TaxId != null) && (!TaxId.Equals(string.Empty)))
                {
                    b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------    
        public async Task<OrderLogic> QuoteToOrderASync<T>()
        {
            string orderId = await dealOrder.QuoteToOrder();
            string[] keys = { orderId };

            OrderLogic l = new OrderLogic();
            l.AppConfig = AppConfig;
            l.UserSession = UserSession;
            bool x = await l.LoadAsync<OrderLogic>(keys);

            return l;
        }
        //------------------------------------------------------------------------------------
    }
}
