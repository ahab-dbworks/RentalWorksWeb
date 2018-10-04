using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Order
{
    public class OrderLogic : OrderBaseLogic
    {
        OrderLoader orderLoader = new OrderLoader();
        OrderBrowseLoader orderBrowseLoader = new OrderBrowseLoader();
        //------------------------------------------------------------------------------------
        public OrderLogic()
        {
            dataLoader = orderLoader;
            browseLoader = orderBrowseLoader;
            Type = RwConstants.ORDER_TYPE_ORDER;
            BeforeSave += OnBeforeSave;
            dealOrder.AfterSave += OnAfterSaveDealOrder;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        //------------------------------------------------------------------------------------
        public string OrderNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        //------------------------------------------------------------------------------------
        public string OrderDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = RwConstants.ORDER_STATUS_CONFIRMED;
                OrderDate = FwConvert.ToString(DateTime.Today);
            }
        }
        //------------------------------------------------------------------------------------ 
        public override void OnAfterSaveDealOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            base.OnAfterSaveDealOrder(sender, e);
            OrderLogic l2 = new OrderLogic();
            l2.SetDependencies(this.AppConfig, this.UserSession);
            object[] pk = GetPrimaryKeys();
            bool b = l2.LoadAsync<OrderLogic>(pk).Result;
            BillToAddressId = l2.BillToAddressId;
            TaxId = l2.TaxId;

            if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)) && (TaxId != null) && (!TaxId.Equals(string.Empty)))
            {
                b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
            }
            b = dealOrder.UpdateOrderTotal().Result;
        }
        //------------------------------------------------------------------------------------    
        public async Task<OrderLogic> CancelOrderASync()
        {
            await dealOrder.CancelOrder();
            await LoadAsync<OrderLogic>();
            return this;
        }
        //------------------------------------------------------------------------------------
        public async Task<OrderLogic> UncancelOrderASync()
        {
            await dealOrder.UncancelOrder();
            await LoadAsync<OrderLogic>();
            return this;
        }
        //------------------------------------------------------------------------------------    
        public async Task<OrderLogic> CreateSnapshotASync()
        {
            string newOrderId = await dealOrder.CreateSnapshot();

            string[] keys = { newOrderId };
            OrderLogic l = new OrderLogic();
            l.SetDependencies(AppConfig, UserSession);
            bool x = await l.LoadAsync<OrderLogic>(keys);

            return l;
        }
        //------------------------------------------------------------------------------------
    }
}
