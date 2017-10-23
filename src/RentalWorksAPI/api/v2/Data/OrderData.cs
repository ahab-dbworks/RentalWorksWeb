﻿using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Newtonsoft.Json;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.OrderModels.CsrsDeals;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace RentalWorksAPI.api.v2.Data
{
    public class OrderData
    {
        //----------------------------------------------------------------------------------------------------
        public static Csrs GetCsrs(string locationid, string csrid)
        {
            FwSqlCommand qry;
            Csrs result       = new Csrs();
            dynamic qryresult = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select top 1 *");
            qry.Add("  from apirest_csrdeal");
            qry.Add(" where csrid      = @csrid");
            qry.Add("   and locationid = @locationid");
            qry.AddParameter("@csrid",      csrid);
            qry.AddParameter("@locationid", locationid);

            qryresult = qry.QueryToDynamicObject2();

            if ((qryresult != null) && (qryresult.csrid != ""))
            {
                result.csrid = qryresult.csrid;
                result.deals = GetCsrsDeals(csrid);
            }

            return result;
        }
        //------------------------------------------------------------------------------
        public static List<Deal> GetCsrsDeals(string csrid)
        {
            FwSqlCommand qry;
            List<Deal> deals = new List<Deal>();
            dynamic qryresult;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_csrdeal");
            qry.Add(" where csrid = @csrid");
            qry.AddParameter("@csrid", csrid);
            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                Deal deal = new Deal();

                deal.dealid   = qryresult[i].dealid;
                deal.dealname = qryresult[i].deal;

                deals.Add(deal);
            }

            return deals;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrdersAndItemsResponse> GetOrdersAndItems(string locationid, string departmentid, string lastmodifiedfromdate, string lastmodifiedtodate,
                                                                     string orderid, List<string> agentid, List<string> status, List<string> dealid)
        {
            FwSqlCommand qry;
            List<OrdersAndItemsResponse> result = new List<OrdersAndItemsResponse>();
            dynamic qryresult                   = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select distinct dealid, deal");
            qry.Add("  from apirest_ordersidfunc(@agentids, @dealids, @departmentid, @locationid, @status, @lastmodifiedfromdate, @lastmodifiedtodate, @orderid)");
            qry.AddParameter("@agentids",               string.Join(",", agentid));
            qry.AddParameter("@dealids",                string.Join(",", dealid));
            qry.AddParameter("@departmentid",           departmentid);
            qry.AddParameter("@locationid",             locationid);
            qry.AddParameter("@status",                 string.Join(",", status));
            qry.AddParameter("@lastmodifiedfromdate",   lastmodifiedfromdate);
            qry.AddParameter("@lastmodifiedtodate",     lastmodifiedtodate);
            qry.AddParameter("@orderid",                orderid);

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                OrdersAndItemsResponse ordersanditems = new OrdersAndItemsResponse();

                ordersanditems.dealid = qryresult[i].dealid;
                ordersanditems.deal   = qryresult[i].deal;
                ordersanditems.orders = GetOAIOrders(locationid, departmentid, lastmodifiedfromdate, lastmodifiedtodate, orderid, agentid, status, qryresult[i].dealid);

                result.Add(ordersanditems);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Order> GetOAIOrders(string locationid, string departmentid, string lastmodifiedfromdate, string lastmodifiedtodate,
                                                  string orderid, List<string> agentid, List<string> status, string dealid)
        {
            FwSqlCommand qry;
            List<Order> result = new List<Order>();
            dynamic qryresult  = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("orderdate",        false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("pickdate",         false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentfrom",      false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentto",        false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("createddate",      false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("lastmodifieddate", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from apirest_ordersfunc(@agentids, @dealid, @departmentid, @locationid, @status, @lastmodifiedfromdate, @lastmodifiedtodate, @orderid)");
            qry.AddParameter("@agentids",               string.Join(",", agentid));
            qry.AddParameter("@dealid",                 dealid);
            qry.AddParameter("@departmentid",           departmentid);
            qry.AddParameter("@locationid",             locationid);
            qry.AddParameter("@status",                 string.Join(",", status));
            qry.AddParameter("@lastmodifiedfromdate",   lastmodifiedfromdate);
            qry.AddParameter("@lastmodifiedtodate",     lastmodifiedtodate);
            qry.AddParameter("@orderid",                orderid);

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                Order order = new Order();

                order.orderid          = qryresult[i].orderid;
                order.orderno          = qryresult[i].orderno;
                order.orderdesc        = qryresult[i].orderdesc;
                order.orderdate        = qryresult[i].orderdate;
                order.csrid            = qryresult[i].csrid;
                order.csr              = qryresult[i].csr;
                order.pickdate         = qryresult[i].pickdate;
                order.picktime         = qryresult[i].picktime;
                order.estrentfrom      = qryresult[i].estrentfrom;
                order.estfromtime      = qryresult[i].estfromtime;
                order.estrentto        = qryresult[i].estrentto;
                order.esttotime        = qryresult[i].esttotime;
                order.ordertypeid      = qryresult[i].ordertypeid;
                order.ordertype        = qryresult[i].ordertype;
                order.departmentid     = qryresult[i].departmentid;
                order.department       = qryresult[i].department;
                order.rental           = qryresult[i].rental;
                order.sales            = qryresult[i].sales;
                order.labor            = qryresult[i].labor;
                order.misc             = qryresult[i].misc;
                order.orderedbycontact = qryresult[i].orderedbycontact;
                order.createdbyuserid  = qryresult[i].createdbyusersid;
                order.createdby        = qryresult[i].createdby;
                order.createddate      = qryresult[i].createddate;
                order.lastmodifieddate = qryresult[i].lastmodifieddate;
                order.status           = qryresult[i].status;
                order.pono             = qryresult[i].pono;
                order.orderunitid      = qryresult[i].orderunitid;
                order.orderunit        = qryresult[i].orderunit;

                order.outgoingdelivership           = new Delivery();
                order.outgoingdelivership.type      = qryresult[i].outdeliverydelivertype;
                order.outgoingdelivership.contact   = qryresult[i].outdeliverycontact;
                order.outgoingdelivership.attention = qryresult[i].outdeliveryattention;
                order.outgoingdelivership.phone     = qryresult[i].outdeliverycontactphone;
                order.outgoingdelivership.location  = qryresult[i].outdeliverylocation;
                order.outgoingdelivership.address1  = qryresult[i].outdeliveryadd1;
                order.outgoingdelivership.address2  = qryresult[i].outdeliveryadd2;
                order.outgoingdelivership.city      = qryresult[i].outdeliverycity;
                order.outgoingdelivership.state     = qryresult[i].outdeliverystate;
                order.outgoingdelivership.zipcode   = qryresult[i].outdeliveryzip;
                order.outgoingdelivership.country   = qryresult[i].outdeliverycountry;

                order.incomingdelivership           = new Delivery();
                order.incomingdelivership.type      = qryresult[i].indeliverydelivertype;
                order.incomingdelivership.contact   = qryresult[i].indeliverycontact;
                order.incomingdelivership.attention = qryresult[i].indeliveryattention;
                order.incomingdelivership.phone     = qryresult[i].indeliverycontactphone;
                order.incomingdelivership.location  = qryresult[i].indeliverylocation;
                order.incomingdelivership.address1  = qryresult[i].indeliveryadd1;
                order.incomingdelivership.address2  = qryresult[i].indeliveryadd2;
                order.incomingdelivership.city      = qryresult[i].indeliverycity;
                order.incomingdelivership.state     = qryresult[i].indeliverystate;
                order.incomingdelivership.zipcode   = qryresult[i].indeliveryzip;
                order.incomingdelivership.country   = qryresult[i].indeliverycountry;

                order.ordernotes = GetOAIOrderNotes(qryresult[i].orderid);

                order.items = GetOAIItems(qryresult[i].orderid);

                result.Add(order);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrderNote> GetOAIOrderNotes(string orderid)
        {
            FwSqlCommand qry;
            List<OrderNote> result = new List<OrderNote>();
            dynamic qryresult      = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("notedate",  false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("datestamp", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.UTCDateTime);
            qry.Add("select *");
            qry.Add("  from apirest_ordernoteview");
            qry.Add(" where orderid = @orderid");
            qry.Add("order by notedate");
            qry.AddParameter("@orderid", orderid);

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                OrderNote ordernote = new OrderNote();

                ordernote.ordernoteid = qryresult[i].ordernoteid;
                ordernote.notes       = qryresult[i].notes;
                ordernote.notedate    = qryresult[i].notedate;
                ordernote.notesdesc   = qryresult[i].notesdesc;
                ordernote.datestamp   = qryresult[i].datestamp;
                ordernote.name        = qryresult[i].name;

                result.Add(ordernote);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Item> GetOAIItems(string orderid)
        {
            FwSqlCommand qry, qry2;
            List<Item> result = new List<Item>();
            dynamic qryresult = new ExpandoObject();

            qry2 = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry2.Add("exec apirest_availmakecurrent @orderid");
            qry2.AddParameter("@orderid", orderid);
            qry2.Execute();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("rentfromdate",   false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("renttodate",     false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("price",          false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.AddColumn("unitextended",   false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.AddColumn("weeklyextended", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.AddColumn("periodextended", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.Add("select *");
            qry.Add("  from apirest_ordersanditemsfunc(@orderid)");
            qry.Add("order by orderby");
            qry.AddParameter("@orderid", orderid);

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                Item item = new Item();

                item.masteritemid          = qryresult[i].masteritemid;
                item.masterid              = qryresult[i].masterid;
                item.masterno              = qryresult[i].masterno;
                item.description           = qryresult[i].description;
                item.rectype               = qryresult[i].rectype;
                item.itemorder             = qryresult[i].itemorder;
                item.inventorydepartmentid = qryresult[i].inventorydepartmentid;
                item.inventorydepartment   = qryresult[i].inventorydepartment;
                item.itemclass             = qryresult[i].itemclass;
                item.rentfromdate          = qryresult[i].rentfromdate;
                item.rentfromtime          = qryresult[i].rentfromtime;
                item.renttodate            = qryresult[i].renttodate;
                item.renttotime            = qryresult[i].renttotime;
                item.qtyordered            = FwConvert.ToString(qryresult[i].qtyordered);
                item.subqty                = FwConvert.ToString(qryresult[i].subqty);
                item.subvendor             = qryresult[i].subvendor;
                item.unit                  = qryresult[i].unit;
                item.price                 = qryresult[i].price;
                item.daysinwk              = FwConvert.ToString(qryresult[i].daysinweeks);
                item.notes                 = qryresult[i].notes;
                item.parentid              = qryresult[i].parentid;
                item.unitextended          = qryresult[i].unitextended;
                item.weeklyextended        = qryresult[i].weeklyextended;
                item.periodextended        = qryresult[i].periodextended;
                item.taxable               = qryresult[i].taxable;
                item.warehouseid           = qryresult[i].warehouseid;
                item.qtystaged             = FwConvert.ToString(qryresult[i].qtystaged);
                item.qtyout                = FwConvert.ToString(qryresult[i].qtyout);
                item.qtyin                 = FwConvert.ToString(qryresult[i].qtyin);
                item.qtyremaining          = FwConvert.ToString(qryresult[i].qtyremaining);
                item.qtyconflict           = FwConvert.ToString(qryresult[i].qtyconflict);
                item.availabletofulfillqty = FwConvert.ToString(qryresult[i].availabletofullfillqty);
                item.trackedby             = qryresult[i].trackedby;

                result.Add(item);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrderStatusItems> GetOrderStatus(string orderid)
        {
            List<OrderStatusItems> result            = new List<OrderStatusItems>();
            List<OrderStatusItemDetail> detailresult = new List<OrderStatusItemDetail>();
            dynamic qryresult                        = new ExpandoObject();

            detailresult = GetOrderStatusDetail(orderid);

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from apirest_orderstatus(@orderid)");
                qry.Add("order by orderby");
                qry.AddParameter("@orderid", orderid);
                qryresult = qry.QueryToDynamicList2();

                for (int i = 0; i < qryresult.Count; i++)
                {
                    OrderStatusItems item = new OrderStatusItems();

                    item.rectype         = qryresult[i].rectype;
                    item.masterid        = qryresult[i].masterid;
                    item.masteritemid    = qryresult[i].masteritemid;
                    item.masterno        = qryresult[i].masterno;
                    item.description     = qryresult[i].description;
                    item.qtyordered      = qryresult[i].qtyordered;
                    item.qtystaged       = qryresult[i].qtystaged;
                    item.qtyout          = qryresult[i].qtyout;
                    item.qtyin           = qryresult[i].qtyin;
                    item.qtystillout     = qryresult[i].qtystillout;
                    item.itemorder       = qryresult[i].itemorder;
                    item.trackedby       = qryresult[i].trackedby;

                    item.physicalassets  = detailresult.Where(x => x.masteritemid == item.masteritemid).ToList();

                    result.Add(item);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrderStatusItemDetail> GetOrderStatusDetail(string orderid)
        {
            List<OrderStatusItemDetail> result = new List<OrderStatusItemDetail>();;
            dynamic qryresult                  = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("outdatetime", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.DateTime);
                qry.AddColumn("indatetime",  false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.DateTime);
                qry.Add("select *");
                qry.Add("from apirest_orderstatusdetail(@orderid)");
                qry.Add("order by orderby");
                qry.AddParameter("@orderid", orderid);
                qryresult = qry.QueryToDynamicList2();

                for (int i = 0; i < qryresult.Count; i++)
                {
                    OrderStatusItemDetail item = new OrderStatusItemDetail();

                    item.rentalitemid    = qryresult[i].rentalitemid;
                    item.rectype         = qryresult[i].rectype;
                    item.masterid        = qryresult[i].masterid;
                    item.masteritemid    = qryresult[i].masteritemid;
                    item.masterno        = qryresult[i].masterno;
                    item.description     = qryresult[i].description;
                    item.trackedby       = qryresult[i].trackedby;
                    item.barcode         = qryresult[i].barcode;
                    item.serialno        = qryresult[i].mfgserial;
                    item.rfid            = qryresult[i].rfid;
                    item.vendor          = qryresult[i].vendor;
                    item.outdatetime     = qryresult[i].outdatetime;
                    item.indatetime      = qryresult[i].indatetime;

                    result.Add(item);
                }
            }

            return result;
        }
        //------------------------------------------------------------------------------
    }
}