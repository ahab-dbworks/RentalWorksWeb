using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebLibrary;
using static WebApi.Modules.Home.DealOrder.DealOrderRecord;

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
        [FwLogicProperty(Id:"t2iGW7Twvavm", IsPrimaryKey:true)]
        public string OrderId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"LDW97FNr8Vrz", DisableDirectAssign: true, DisableDirectModify: true)]
        public string OrderNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"ntviBtfhLqsd", DisableDirectAssign: true, DisableDirectModify: true)]
        public string OrderDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }

        //------------------------------------------------------------------------------------
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
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
        public async Task<OrderOnHoldResponse> OnHoldOrderASync()
        {
            OrderOnHoldResponse response = await dealOrder.OnHoldOrder();
            if (response.success)
            {
                await LoadAsync<OrderLogic>();
                response.order = this;
            }
            return response;
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
