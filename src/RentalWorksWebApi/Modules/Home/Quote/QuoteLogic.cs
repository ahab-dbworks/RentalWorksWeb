using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.Home.Order;
using System;
using WebLibrary;
using WebApi.Logic;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using WebApi.Modules.Home.DealOrder;
using static WebApi.Modules.Home.DealOrder.DealOrderRecord;

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
        [FwLogicProperty(Id: "PZaTT296KqnzM", IsPrimaryKey: true)]
        public string QuoteId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }

        [FwLogicProperty(Id: "kwFYukQcQp9o", DisableDirectAssign: true, DisableDirectModify: true)]
        public string QuoteNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }

        [FwLogicProperty(Id: "aN8TGE6a4YFv", DisableDirectAssign: true, DisableDirectModify: true)]
        public string QuoteDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }

        [FwLogicProperty(Id: "cVKvrBbjaH2B", DisableDirectModify: true)]
        public int? VersionNumber { get { return dealOrder.VersionNumber; } set { dealOrder.VersionNumber = value; } }

        //------------------------------------------------------------------------------------
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                StatusDate = FwConvert.ToString(DateTime.Today);
                QuoteDate = FwConvert.ToString(DateTime.Today);
                Status = ((string.IsNullOrEmpty(DealId)) ? RwConstants.QUOTE_STATUS_PROSPECT : RwConstants.QUOTE_STATUS_ACTIVE);
                VersionNumber = 1;
                if (this.UserSession.UserType == "CONTACT")
                {
                    Status = RwConstants.QUOTE_STATUS_NEW;
                }
            }
            else // (updating)
            {
                if (DealId != null) // user has modified the Deal value in this update request
                {
                    if (DealId.Equals(string.Empty))
                    {
                        Status = RwConstants.QUOTE_STATUS_PROSPECT;
                    }
                    else
                    {
                        Status = RwConstants.QUOTE_STATUS_ACTIVE;
                    }
                }

                if (Status != null)
                {
                    if ((e.Original != null) && (!Status.Equals(((QuoteLogic)e.Original).Status)))
                    {
                        StatusDate = FwConvert.ToString(DateTime.Today);
                    }
                }
            }

        }
        //------------------------------------------------------------------------------------ 
        public override void OnAfterSaveDealOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            base.OnAfterSaveDealOrder(sender, e);
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    TaxId = ((DealOrderRecord)e.Original).TaxId;
                }

                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)) && (TaxId != null) && (!TaxId.Equals(string.Empty)))
                {
                    bool b1 = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                }
            }
            bool b2 = dealOrder.UpdateOrderTotal(e.SqlConnection).Result;
        }
        //------------------------------------------------------------------------------------    
        public async Task<OrderLogic> QuoteToOrderASync<T>()
        {
            string orderId = await dealOrder.QuoteToOrder();
            string[] keys = { orderId };

            OrderLogic l = new OrderLogic();
            l.SetDependencies(AppConfig, UserSession);
            bool x = await l.LoadAsync<OrderLogic>(keys);

            return l;
        }
        //------------------------------------------------------------------------------------
        public async Task<QuoteLogic> CancelQuoteASync()
        {
            await dealOrder.CancelQuote();
            await LoadAsync<QuoteLogic>();
            return this;
        }
        //------------------------------------------------------------------------------------
        public async Task<QuoteLogic> UncancelQuoteASync()
        {
            await dealOrder.UncancelQuote();
            await LoadAsync<QuoteLogic>();
            return this;
        }
        //------------------------------------------------------------------------------------    
        public async Task<QuoteLogic> CreateNewVersionASync()
        {
            string newQuoteId = await dealOrder.CreateNewVersion();

            string[] keys = { newQuoteId };
            QuoteLogic l = new QuoteLogic();
            l.SetDependencies(AppConfig, UserSession);
            bool x = await l.LoadAsync<QuoteLogic>(keys);

            return l;
        }
        //------------------------------------------------------------------------------------    
        public async Task<TSpStatusResponse> MakeQuoteActiveAsync()
        {
            return await dealOrder.MakeQuoteActive();
        }
        //------------------------------------------------------------------------------------
        public async Task<QuoteLogic> SubmitQuoteASync()
        {
            bool success = await dealOrder.SubmitQuote();

            if (success)
            {
                await LoadAsync<QuoteLogic>();
            }
            return this;
        }
        //------------------------------------------------------------------------------------
        public async Task<ReserveQuoteResponse> Reserve()
        {
            return await dealOrder.Reserve();
        }
        //------------------------------------------------------------------------------------
        public async Task<QuoteLogic> ActivateQuoteRequestASync()
        {
            string newOrderId = await dealOrder.ActivateQuoteRequest();

            string[] keys = { newOrderId };
            QuoteLogic q = new QuoteLogic();
            q.SetDependencies(AppConfig, UserSession);
            bool x = await q.LoadAsync<QuoteLogic>(keys);

            return q;
        }
        //------------------------------------------------------------------------------------    
        public async Task<ChangeOrderOfficeLocationResponse> ChangeOfficeLocationASync(ChangeOrderOfficeLocationRequest request)
        {
            QuoteLogic orig = (QuoteLogic)this.MemberwiseClone();
            ChangeOrderOfficeLocationResponse response = await dealOrder.ChangeOfficeLocationASync(request);
            if (response.success)
            {
                await LoadAsync<QuoteLogic>();
                response.quoteOrOrder = this;
                AddAudit(orig);
            }
            return response;
        }
        //------------------------------------------------------------------------------------
    }
}
