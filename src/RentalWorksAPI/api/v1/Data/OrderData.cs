using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v1.Models;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksAPI.api.v1.Data
{
    public class OrderData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<Order> GetOrder(string orderid, string ordertype, List<string> statuses, string rental, string sales, string datestamp)
        {
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();
            List<Order> result = new List<Order>();
            FwJsonDataTable dt = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("estrentfrom",     false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentto",       false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("billperiodstart", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("billperiodend",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("requiredbydate",  false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("deliverondate",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("pickdate",        false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("loadindate",      false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("testdate",        false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("strikedate",      false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("datestamp",       false, FwJsonDataTableColumn.DataTypes.UTCDateTime);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select distinct *");
            select.Add("  from apirest_orderview");
            select.Parse();
            select.AddWhere("ordertype = @ordertype");
            select.AddParameter("@ordertype", ordertype);
            if (!string.IsNullOrEmpty(orderid))
            {
                select.AddWhere("orderid = @orderid");
                select.AddParameter("@orderid", orderid);
            }
            if ((rental == "true") && ((sales == null) || (sales == "false")))
            {
                select.AddWhere("rental = @rental");
                select.AddParameter("@rental", ((rental == "true") ? "T" : "F"));
            }
            if ((sales == "true") && ((rental == null) || (rental == "false")))
            {
                select.AddWhere("sales = @sales");
                select.AddParameter("@sales", ((sales == "true") ? "T" : "F"));
            }
            if (!string.IsNullOrEmpty(datestamp))
            {
                select.AddWhere("datestamp > @datestamp");
                select.AddParameter("@datestamp", datestamp);
            }
            if (statuses != null)
            {
                select.AddWhereIn("and", "status", string.Join(",", statuses), false);
            }
            select.AddOrderBy("orderdesc");

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Order newOrder = new Order();

                newOrder.orderno                   = dt.Rows[i][dt.ColumnIndex["orderno"]].ToString().TrimEnd();
                newOrder.orderdesc                 = dt.Rows[i][dt.ColumnIndex["orderdesc"]].ToString().TrimEnd();
                newOrder.status                    = dt.Rows[i][dt.ColumnIndex["status"]].ToString().TrimEnd();
                newOrder.location                  = dt.Rows[i][dt.ColumnIndex["location"]].ToString().TrimEnd();
                newOrder.orderid                   = dt.Rows[i][dt.ColumnIndex["orderid"]].ToString().TrimEnd();
                newOrder.ordertype                 = dt.Rows[i][dt.ColumnIndex["ordertype"]].ToString().TrimEnd();
                newOrder.rental                    = dt.Rows[i][dt.ColumnIndex["rental"]].ToString().TrimEnd();
                newOrder.sales                     = dt.Rows[i][dt.ColumnIndex["sales"]].ToString().TrimEnd();
                newOrder.pono                      = dt.Rows[i][dt.ColumnIndex["pono"]].ToString().TrimEnd();
                newOrder.ratetype                  = dt.Rows[i][dt.ColumnIndex["ratetype"]].ToString().TrimEnd();
                newOrder.estrentfrom               = dt.Rows[i][dt.ColumnIndex["estrentfrom"]].ToString().TrimEnd();
                newOrder.estrentto                 = dt.Rows[i][dt.ColumnIndex["estrentto"]].ToString().TrimEnd();
                newOrder.estfromtime               = dt.Rows[i][dt.ColumnIndex["estfromtime"]].ToString().TrimEnd();
                newOrder.esttotime                 = dt.Rows[i][dt.ColumnIndex["esttotime"]].ToString().TrimEnd();
                newOrder.billperiodstart           = dt.Rows[i][dt.ColumnIndex["billperiodstart"]].ToString().TrimEnd();
                newOrder.billperiodend             = dt.Rows[i][dt.ColumnIndex["billperiodend"]].ToString().TrimEnd();
                newOrder.webusersid                = dt.Rows[i][dt.ColumnIndex["webusersid"]].ToString().TrimEnd();
                newOrder.datestamp                 = dt.Rows[i][dt.ColumnIndex["datestamp"]].ToString().TrimEnd();
                newOrder.agent                     = dt.Rows[i][dt.ColumnIndex["agent"]].ToString().TrimEnd();
                newOrder.projectmanager            = dt.Rows[i][dt.ColumnIndex["projectmanager"]].ToString().TrimEnd();
                newOrder.dealtype                  = dt.Rows[i][dt.ColumnIndex["dealtype"]].ToString().TrimEnd();
                newOrder.department                = dt.Rows[i][dt.ColumnIndex["department"]].ToString().TrimEnd();
                newOrder.orderlocation             = dt.Rows[i][dt.ColumnIndex["orderlocation"]].ToString().TrimEnd();
                newOrder.refno                     = dt.Rows[i][dt.ColumnIndex["refno"]].ToString().TrimEnd();
                newOrder.taxoption                 = dt.Rows[i][dt.ColumnIndex["taxoption"]].ToString().TrimEnd();
                newOrder.requiredbydate            = dt.Rows[i][dt.ColumnIndex["requiredbydate"]].ToString().TrimEnd();
                newOrder.requiredbytime            = dt.Rows[i][dt.ColumnIndex["requiredbytime"]].ToString().TrimEnd();
                newOrder.deliverondate             = dt.Rows[i][dt.ColumnIndex["deliverondate"]].ToString().TrimEnd();
                newOrder.pickdate                  = dt.Rows[i][dt.ColumnIndex["pickdate"]].ToString().TrimEnd();
                newOrder.picktime                  = dt.Rows[i][dt.ColumnIndex["picktime"]].ToString().TrimEnd();
                newOrder.loadindate                = dt.Rows[i][dt.ColumnIndex["loadindate"]].ToString().TrimEnd();
                newOrder.testdate                  = dt.Rows[i][dt.ColumnIndex["testdate"]].ToString().TrimEnd();
                newOrder.strikedate                = dt.Rows[i][dt.ColumnIndex["strikedate"]].ToString().TrimEnd();
                newOrder.rentaltaxrate1            = dt.Rows[i][dt.ColumnIndex["rentaltaxrate1"]].ToString().TrimEnd();
                newOrder.rentaltaxrate2            = dt.Rows[i][dt.ColumnIndex["rentaltaxrate2"]].ToString().TrimEnd();
                newOrder.agentid                   = dt.Rows[i][dt.ColumnIndex["agentid"]].ToString().TrimEnd();
                newOrder.departmentid              = dt.Rows[i][dt.ColumnIndex["departmentid"]].ToString().TrimEnd();
                newOrder.projectmanagerid          = dt.Rows[i][dt.ColumnIndex["projectmanagerid"]].ToString().TrimEnd();
                newOrder.dealtypeid                = dt.Rows[i][dt.ColumnIndex["dealtypeid"]].ToString().TrimEnd();
                newOrder.orderunitid               = dt.Rows[i][dt.ColumnIndex["orderunitid"]].ToString().TrimEnd();
                newOrder.orderunit                 = dt.Rows[i][dt.ColumnIndex["orderunit"]].ToString().TrimEnd();
                newOrder.ordertotal                = dt.Rows[i][dt.ColumnIndex["ordertotal"]].ToString().TrimEnd();
                newOrder.ordergrosstotal           = dt.Rows[i][dt.ColumnIndex["ordergrosstotal"]].ToString().TrimEnd();
                newOrder.ordertypedescription      = dt.Rows[i][dt.ColumnIndex["orderunit"]].ToString().TrimEnd();
                newOrder.onlineorderno             = dt.Rows[i][dt.ColumnIndex["onlineorderno"]].ToString().TrimEnd();
                newOrder.warehouse                 = dt.Rows[i][dt.ColumnIndex["warehouse"]].ToString().TrimEnd();

                newOrder.deal                      = new OrderDeal();
                newOrder.deal.dealid               = dt.Rows[i][dt.ColumnIndex["dealid"]].ToString().TrimEnd();
                newOrder.deal.dealname             = dt.Rows[i][dt.ColumnIndex["deal"]].ToString().TrimEnd();
                newOrder.deal.dealno               = dt.Rows[i][dt.ColumnIndex["dealno"]].ToString().TrimEnd();

                newOrder.customer                  = new Customer();
                newOrder.customer.customerid       = dt.Rows[i][dt.ColumnIndex["customerid"]].ToString().TrimEnd();
                newOrder.customer.customername     = dt.Rows[i][dt.ColumnIndex["customer"]].ToString().TrimEnd();
                newOrder.customer.customerno       = dt.Rows[i][dt.ColumnIndex["custno"]].ToString().TrimEnd();

                newOrder.ordernotes                = GetOrderNotes(dt.Rows[i][dt.ColumnIndex["orderid"]].ToString().TrimEnd());

                newOrder.outgoingdelivership           = new Delivery();
                newOrder.outgoingdelivership.type      = dt.Rows[i][dt.ColumnIndex["outdeliverydelivertype"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.contact   = dt.Rows[i][dt.ColumnIndex["outdeliverycontact"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.attention = dt.Rows[i][dt.ColumnIndex["outdeliveryattention"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.phone     = dt.Rows[i][dt.ColumnIndex["outdeliverycontactphone"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.location  = dt.Rows[i][dt.ColumnIndex["outdeliverylocation"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.address1  = dt.Rows[i][dt.ColumnIndex["outdeliveryadd1"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.address2  = dt.Rows[i][dt.ColumnIndex["outdeliveryadd2"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.city      = dt.Rows[i][dt.ColumnIndex["outdeliverycity"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.state     = dt.Rows[i][dt.ColumnIndex["outdeliverystate"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.zipcode   = dt.Rows[i][dt.ColumnIndex["outdeliveryzip"]].ToString().TrimEnd();
                newOrder.outgoingdelivership.country   = dt.Rows[i][dt.ColumnIndex["outdeliverycountry"]].ToString().TrimEnd();

                result.Add(newOrder);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ProcessQuote(Order orderdata)
        {
            FwSqlCommand sp_processquote, sp_processoutgoingdelivership, updateqry;
            dynamic result = new ExpandoObject();

            sp_processquote = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_processquote");
            sp_processquote.AddParameter("@orderdesc",          orderdata.orderdesc);
            sp_processquote.AddParameter("@location",           orderdata.location);
            sp_processquote.AddParameter("@warehouse",          orderdata.warehouse);
            sp_processquote.AddParameter("@rental",             orderdata.rental);
            sp_processquote.AddParameter("@sales",              orderdata.sales);
            sp_processquote.AddParameter("@estrentfrom",        orderdata.estrentfrom);
            sp_processquote.AddParameter("@estfromtime",        orderdata.estfromtime);
            sp_processquote.AddParameter("@estrentto",          orderdata.estrentto);
            sp_processquote.AddParameter("@esttotime",          orderdata.esttotime);
            if ((orderdata.billperiodstart == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update dealorder");
                updateqry.Add("   set billperiodstart = null");
                updateqry.Add(" where orderid         = @orderid");
                updateqry.AddParameter("@orderid", orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.billperiodstart != "") && (orderdata.billperiodstart != null))
            {
                sp_processquote.AddParameter("@billperiodstart", orderdata.billperiodstart);
            }
            if ((orderdata.billperiodend == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update dealorder");
                updateqry.Add("   set billperiodend = null");
                updateqry.Add(" where orderid       = @orderid");
                updateqry.AddParameter("@orderid", orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.billperiodend != "") && (orderdata.billperiodend != null))
            {
                sp_processquote.AddParameter("@billperiodend", orderdata.billperiodend);
            }
            sp_processquote.AddParameter("@webusersid",         orderdata.webusersid);
            sp_processquote.AddParameter("@agentid",            orderdata.agentid);
            sp_processquote.AddParameter("@projectmanagerid",   orderdata.projectmanagerid);
            if (orderdata.deal != null)
            {
                sp_processquote.AddParameter("@dealid",         orderdata.deal.dealid);
            }
            else
            {
                sp_processquote.AddParameter("@dealid",         "");
            }
            sp_processquote.AddParameter("@pono",               orderdata.pono);
            sp_processquote.AddParameter("@status",             orderdata.status);
            sp_processquote.AddParameter("@ordertype",          orderdata.ordertype);
            sp_processquote.AddParameter("@ratetype",           orderdata.ratetype);
            sp_processquote.AddParameter("@orderunit",          orderdata.orderunit);
            if ((orderdata.requiredbydate == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update delivery");
                updateqry.Add("   set requiredbydate = @estrentfrom");
                updateqry.Add(" where deliveryid = (select outdeliveryid");
                updateqry.Add("                       from dealorder");
                updateqry.Add("                      where orderid = @orderid)");
                updateqry.AddParameter("@orderid",     orderdata.orderid);
                updateqry.AddParameter("@estrentfrom", orderdata.estrentfrom);
                updateqry.Execute();
            }
            else if ((orderdata.requiredbydate != "") && (orderdata.requiredbydate != null))
            {
                sp_processquote.AddParameter("@requiredbydate", orderdata.requiredbydate);
            }
            sp_processquote.AddParameter("@requiredbytime",     orderdata.requiredbytime);
            if ((orderdata.deliverondate == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update delivery");
                updateqry.Add("   set ondate = null");
                updateqry.Add(" where deliveryid = (select outdeliveryid");
                updateqry.Add("                       from dealorder");
                updateqry.Add("                      where orderid = @orderid)");
                updateqry.AddParameter("@orderid",     orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.deliverondate != "") && (orderdata.deliverondate != null))
            {
                sp_processquote.AddParameter("@ondate",             orderdata.deliverondate);
            }
            if ((orderdata.pickdate == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update dealorder");
                updateqry.Add("   set pickdate = null");
                updateqry.Add(" where orderid  = @orderid");
                updateqry.AddParameter("@orderid", orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.pickdate != "") && (orderdata.pickdate != null))
            {
                sp_processquote.AddParameter("@pickdate",             orderdata.pickdate);
            }
            sp_processquote.AddParameter("@picktime",           orderdata.picktime);
            if ((orderdata.loadindate == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update dealorder");
                updateqry.Add("   set loadindate = null");
                updateqry.Add(" where orderid    = @orderid");
                updateqry.AddParameter("@orderid", orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.loadindate != "") && (orderdata.loadindate != null))
            {
                sp_processquote.AddParameter("@loadindate",             orderdata.loadindate);
            }
            if ((orderdata.testdate == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update dealorder");
                updateqry.Add("   set testdate = null");
                updateqry.Add(" where orderid  = @orderid");
                updateqry.AddParameter("@orderid", orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.testdate != "") && (orderdata.testdate != null))
            {
                sp_processquote.AddParameter("@testdate",             orderdata.testdate);
            }
            if ((orderdata.strikedate == "") && (!string.IsNullOrEmpty(orderdata.orderid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update dealorder");
                updateqry.Add("   set strikedate = null");
                updateqry.Add(" where orderid    = @orderid");
                updateqry.AddParameter("@orderid", orderdata.orderid);
                updateqry.Execute();
            }
            else if ((orderdata.strikedate != "") && (orderdata.strikedate != null))
            {
                sp_processquote.AddParameter("@strikedate",             orderdata.strikedate);
            }
            sp_processquote.AddParameter("@refno",              orderdata.refno);
            sp_processquote.AddParameter("@orderid",            System.Data.SqlDbType.Char,     System.Data.ParameterDirection.InputOutput, orderdata.orderid);
            sp_processquote.AddParameter("@errno",              System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output, 0);
            sp_processquote.AddParameter("@errmsg",             System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output, 255);
            sp_processquote.Execute();

            result.orderid = sp_processquote.GetParameter("@orderid").ToString().TrimEnd();
            result.errno   = sp_processquote.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg  = sp_processquote.GetParameter("@errmsg").ToString().TrimEnd();

            if ((result.errno == "0") && (orderdata.outgoingdelivership != null))
            {
                sp_processoutgoingdelivership = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_processquoteoutgoingdeliveryship");
                sp_processoutgoingdelivership.AddParameter("@orderid",      result.orderid);
                sp_processoutgoingdelivership.AddParameter("@delivertype",  orderdata.outgoingdelivership.type);
                sp_processoutgoingdelivership.AddParameter("@contact",      orderdata.outgoingdelivership.contact);
                sp_processoutgoingdelivership.AddParameter("@attention",    orderdata.outgoingdelivership.attention);
                sp_processoutgoingdelivership.AddParameter("@contactphone", orderdata.outgoingdelivership.phone);
                sp_processoutgoingdelivership.AddParameter("@location",     orderdata.outgoingdelivership.location);
                sp_processoutgoingdelivership.AddParameter("@add1",         orderdata.outgoingdelivership.address1);
                sp_processoutgoingdelivership.AddParameter("@add2",         orderdata.outgoingdelivership.address2);
                sp_processoutgoingdelivership.AddParameter("@city",         orderdata.outgoingdelivership.city);
                sp_processoutgoingdelivership.AddParameter("@state",        orderdata.outgoingdelivership.state);
                sp_processoutgoingdelivership.AddParameter("@zip",          orderdata.outgoingdelivership.zipcode);
                sp_processoutgoingdelivership.AddParameter("@country",      orderdata.outgoingdelivership.country);
                sp_processoutgoingdelivership.AddParameter("@errno",        System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output, 0);
                sp_processoutgoingdelivership.AddParameter("@errmsg",       System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output, 255);
                sp_processoutgoingdelivership.Execute();

                result.errno  = sp_processoutgoingdelivership.GetParameter("@errno").ToString().TrimEnd();
                result.errmsg = sp_processoutgoingdelivership.GetParameter("@errmsg").ToString().TrimEnd();
            }

            if ((result.errno == "0") && (orderdata.ordernotes != null))
            {
                for (int i = 0; i < orderdata.ordernotes.Count; i++)
                {
                    ProcessOrderNote(orderdata.ordernotes[i], result.orderid);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrderItem> GetOrderItems(string orderid)
        {
            FwSqlCommand qry;
            FwSqlSelect select     = new FwSqlSelect();
            List<OrderItem> result = new List<OrderItem>();
            FwJsonDataTable dt     = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("rentfromdate", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("renttodate",   false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select *");
            select.Add("  from apirest_masteritemview");
            select.Add(" where orderid = @orderid");
            select.Add("order by itemorder");
            select.AddParameter("@orderid", orderid);

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OrderItem orderitem = new OrderItem();

                orderitem.masteritemid         = dt.Rows[i][dt.ColumnIndex["masteritemid"]].ToString().TrimEnd();
                orderitem.masterid             = dt.Rows[i][dt.ColumnIndex["masterid"]].ToString().TrimEnd();
                orderitem.description          = dt.Rows[i][dt.ColumnIndex["description"]].ToString().TrimEnd();
                orderitem.rentfromdate         = dt.Rows[i][dt.ColumnIndex["rentfromdate"]].ToString().TrimEnd();
                orderitem.rentfromtime         = dt.Rows[i][dt.ColumnIndex["rentfromtime"]].ToString().TrimEnd();
                orderitem.renttodate           = dt.Rows[i][dt.ColumnIndex["renttodate"]].ToString().TrimEnd();
                orderitem.renttotime           = dt.Rows[i][dt.ColumnIndex["renttotime"]].ToString().TrimEnd();
                orderitem.qtyordered           = dt.Rows[i][dt.ColumnIndex["qtyordered"]].ToString().TrimEnd();
                orderitem.unit                 = dt.Rows[i][dt.ColumnIndex["unit"]].ToString().TrimEnd();
                orderitem.price                = dt.Rows[i][dt.ColumnIndex["price"]].ToString().TrimEnd();
                orderitem.daysinwk             = dt.Rows[i][dt.ColumnIndex["daysinwk"]].ToString().TrimEnd();
                orderitem.notes                = dt.Rows[i][dt.ColumnIndex["notes"]].ToString().TrimEnd();
                orderitem.parentid             = dt.Rows[i][dt.ColumnIndex["parentid"]].ToString().TrimEnd();
                orderitem.nestedmasteritemid   = dt.Rows[i][dt.ColumnIndex["nestedmasteritemid"]].ToString().TrimEnd();

                orderitem.unitextended         = dt.Rows[i][dt.ColumnIndex["unitextended"]].ToString().TrimEnd();
                orderitem.periodextended       = dt.Rows[i][dt.ColumnIndex["periodextended"]].ToString().TrimEnd();
                orderitem.weeklyextended       = dt.Rows[i][dt.ColumnIndex["weeklyextended"]].ToString().TrimEnd();
                orderitem.taxable              = dt.Rows[i][dt.ColumnIndex["taxable"]].ToString().TrimEnd();
                orderitem.inactive             = dt.Rows[i][dt.ColumnIndex["inactive"]].ToString().TrimEnd();
                orderitem.itemorder            = dt.Rows[i][dt.ColumnIndex["itemorder"]].ToString().TrimEnd();

                result.Add(orderitem);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrderItem> GetOrderItems(string orderid, string[] items)
        {
            FwSqlCommand qry;
            FwSqlSelect select     = new FwSqlSelect();
            List<OrderItem> result = new List<OrderItem>();
            FwJsonDataTable dt     = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("rentfromdate", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("renttodate",   false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select *");
            select.Add("  from apirest_masteritemview");
            select.Parse();
            select.AddWhere("orderid = @orderid");
            select.AddWhereIn("and", "masteritemid", string.Join(",", items), false);
            select.AddOrderBy("itemorder");
            select.AddParameter("@orderid", orderid);

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OrderItem orderitem = new OrderItem();

                orderitem.masteritemid         = dt.Rows[i][dt.ColumnIndex["masteritemid"]].ToString().TrimEnd();
                orderitem.masterid             = dt.Rows[i][dt.ColumnIndex["masterid"]].ToString().TrimEnd();
                orderitem.description          = dt.Rows[i][dt.ColumnIndex["description"]].ToString().TrimEnd();
                orderitem.rentfromdate         = dt.Rows[i][dt.ColumnIndex["rentfromdate"]].ToString().TrimEnd();
                orderitem.rentfromtime         = dt.Rows[i][dt.ColumnIndex["rentfromtime"]].ToString().TrimEnd();
                orderitem.renttodate           = dt.Rows[i][dt.ColumnIndex["renttodate"]].ToString().TrimEnd();
                orderitem.renttotime           = dt.Rows[i][dt.ColumnIndex["renttotime"]].ToString().TrimEnd();
                orderitem.qtyordered           = dt.Rows[i][dt.ColumnIndex["qtyordered"]].ToString().TrimEnd();
                orderitem.unit                 = dt.Rows[i][dt.ColumnIndex["unit"]].ToString().TrimEnd();
                orderitem.price                = dt.Rows[i][dt.ColumnIndex["price"]].ToString().TrimEnd();
                orderitem.daysinwk             = dt.Rows[i][dt.ColumnIndex["daysinwk"]].ToString().TrimEnd();
                orderitem.notes                = dt.Rows[i][dt.ColumnIndex["notes"]].ToString().TrimEnd();
                orderitem.parentid             = dt.Rows[i][dt.ColumnIndex["parentid"]].ToString().TrimEnd();
                orderitem.nestedmasteritemid   = dt.Rows[i][dt.ColumnIndex["nestedmasteritemid"]].ToString().TrimEnd();

                orderitem.unitextended         = dt.Rows[i][dt.ColumnIndex["unitextended"]].ToString().TrimEnd();
                orderitem.periodextended       = dt.Rows[i][dt.ColumnIndex["periodextended"]].ToString().TrimEnd();
                orderitem.weeklyextended       = dt.Rows[i][dt.ColumnIndex["weeklyextended"]].ToString().TrimEnd();
                orderitem.taxable              = dt.Rows[i][dt.ColumnIndex["taxable"]].ToString().TrimEnd();
                orderitem.inactive             = dt.Rows[i][dt.ColumnIndex["inactive"]].ToString().TrimEnd();
                orderitem.itemorder            = dt.Rows[i][dt.ColumnIndex["itemorder"]].ToString().TrimEnd();

                result.Add(orderitem);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ProcessOrderItem(OrderItem orderitem, string orderid)
        {
            FwSqlCommand sp;
            dynamic result = new ExpandoObject();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_processmasteritem");
            sp.AddParameter("@orderid",       orderid);
            sp.AddParameter("@masterid",      orderitem.masterid);
            sp.AddParameter("@qtyordered",    orderitem.qtyordered);
            sp.AddParameter("@note",          orderitem.notes);
            sp.AddParameter("@parentid",      orderitem.parentid);
            sp.AddParameter("@packageitemid", orderitem.packageitemid);
            sp.AddParameter("@masteritemid",  System.Data.SqlDbType.Char,     System.Data.ParameterDirection.InputOutput, orderitem.masteritemid);
            sp.AddParameter("@errno",         System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output, 0);
            sp.AddParameter("@errmsg",        System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output, 255);
            sp.Execute();

            result.masteritemid = sp.GetParameter("@masteritemid").ToString().TrimEnd();
            result.errno        = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg       = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static OrderNote GetOrderNote(string ordernoteid)
        {
            FwSqlCommand qry;
            OrderNote result = new OrderNote();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_ordernoteview");
            qry.Add(" where ordernoteid = @ordernoteid");
            qry.AddParameter("@ordernoteid", ordernoteid);
            qry.Execute();

            result.ordernoteid = qry.GetField("ordernoteid").ToString().TrimEnd();
            result.notes       = qry.GetField("notes").ToString().TrimEnd();
            result.notedate    = qry.GetField("notedate").ToString().TrimEnd();
            result.notesdesc   = qry.GetField("notesdesc").ToString().TrimEnd();
            result.datestamp   = qry.GetField("datestamp").ToString().TrimEnd();
            result.name        = qry.GetField("name").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<OrderNote> GetOrderNotes(string orderid)
        {
            FwSqlCommand qry;
            FwSqlSelect select     = new FwSqlSelect();
            List<OrderNote> result = new List<OrderNote>();
            FwJsonDataTable dt     = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("notedate",  false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.UTCDateTime);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select *");
            select.Add("  from apirest_ordernoteview");
            select.Add(" where orderid = @orderid");
            select.Add("order by notedate");
            select.AddParameter("@orderid", SqlDbType.VarChar, ParameterDirection.Input, 8, orderid);

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OrderNote ordernote = new OrderNote();
    
                ordernote.ordernoteid = dt.Rows[i][dt.ColumnIndex["ordernoteid"]].ToString().TrimEnd();
                ordernote.notes       = dt.Rows[i][dt.ColumnIndex["notes"]].ToString().TrimEnd();
                ordernote.notedate    = dt.Rows[i][dt.ColumnIndex["notedate"]].ToString().TrimEnd();
                ordernote.notesdesc   = dt.Rows[i][dt.ColumnIndex["notesdesc"]].ToString().TrimEnd();
                ordernote.datestamp   = dt.Rows[i][dt.ColumnIndex["datestamp"]].ToString().TrimEnd();
                ordernote.name        = dt.Rows[i][dt.ColumnIndex["name"]].ToString().TrimEnd();

                result.Add(ordernote);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ProcessOrderNote(OrderNote ordernote, string orderid)
        {
            FwSqlCommand sp;
            dynamic result = new ExpandoObject();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_processordernote");
            sp.AddParameter("@orderid",     orderid);
            sp.AddParameter("@notedate",    ordernote.notedate);
            sp.AddParameter("@webusersid",  ordernote.webusersid);
            sp.AddParameter("@notedesc",    ordernote.notesdesc);
            sp.AddParameter("@note",        ordernote.notes);
            sp.AddParameter("@ordernoteid", System.Data.SqlDbType.Char,     System.Data.ParameterDirection.InputOutput, ordernote.ordernoteid);
            sp.AddParameter("@errno",       System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output, 0);
            sp.AddParameter("@errmsg",      System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output, 255);
            sp.Execute();

            result.ordernoteid = sp.GetParameter("@ordernoteid").ToString().TrimEnd();
            result.errno       = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg      = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic WebSubmitQuote(string orderid, string webusersid)
        {
            FwSqlCommand sp;
            dynamic result = new ExpandoObject();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "apiwebsubmitquote");
            sp.AddParameter("@orderid",    orderid);
            sp.AddParameter("@webusersid", webusersid);
            sp.AddParameter("@errno",      System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output, 0);
            sp.AddParameter("@errmsg",     System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output, 255);
            sp.Execute();

            result.errno        = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg       = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static NewQuoteVersionResult ProcessQuoteNewVersion(string orderid, string webusersid)
        {
            FwSqlCommand sp;
            NewQuoteVersionResult result = new NewQuoteVersionResult();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_processquotenewversion");
            sp.AddParameter("@orderid",    orderid);
            sp.AddParameter("@webusersid", webusersid);
            sp.AddParameter("@neworderid", System.Data.SqlDbType.Char,     System.Data.ParameterDirection.Output, 8);
            sp.AddParameter("@errno",      System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output, 0);
            sp.AddParameter("@errmsg",     System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output, 255);
            sp.Execute();

            result.neworderid   = sp.GetParameter("@neworderid").ToString().TrimEnd();
            result.errno        = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg       = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return result;
        }
        //------------------------------------------------------------------------------
        public static Error APIWebDeleteMasterItem(string orderid, string masteritemid)
        {
            Error response = new Error();
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.apirest_processmasteritemdelete");
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@errno",        SqlDbType.Int,     ParameterDirection.Output, 4);
            sp.AddParameter("@errmsg",       SqlDbType.VarChar, ParameterDirection.Output, 255);
            sp.Execute();

            response.errno        = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg       = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        
        //------------------------------------------------------------------------------
        public static QuoteToOrderResult QuoteToOrder(string orderid, string webusersid)
        {
            FwSqlCommand sp;
            QuoteToOrderResult response = new QuoteToOrderResult();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webquotetoorder");
            sp.AddParameter("@orderid",       orderid);
            sp.AddParameter("@webusersid",    webusersid);
            sp.AddParameter("@neworderid",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@errno",         SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.ExecuteNonQuery();

            response.neworderid = sp.GetParameter("@neworderid").ToString().TrimEnd();
            response.errno      = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
        public static Error WebClearQuote(string orderid)
        {
            FwSqlCommand sp;
            Error response = new Error();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webclearquote");
            sp.AddParameter("@orderid",       orderid);
            sp.AddParameter("@errno",         SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.ExecuteNonQuery();

            response.errno      = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
        public static Error CancelDealOrderWeb(string orderid, string webusersid)
        {
            FwSqlCommand sp;
            Error response = new Error();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.canceldealorderweb");
            sp.AddParameter("@orderid",       orderid);
            sp.AddParameter("@webusersid",    webusersid);
            sp.AddParameter("@errno",         SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.ExecuteNonQuery();

            response.errno      = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
        public static CopyOrderResult CopyQuoteOrder(CopyOrderParameters request)
        {
            FwSqlCommand sp;
            CopyOrderResult response = new CopyOrderResult();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.apirest_copyquoteorder");
            sp.AddParameter("@fromorderid",        request.orderid);
            sp.AddParameter("@webusersid",         request.webusersid);
            sp.AddParameter("@newordertype",       request.ordertype);
            sp.AddParameter("@copyquoterates",     request.copyquoterates);
            sp.AddParameter("@copyinventoryrates", request.copyinventoryrates);
            sp.AddParameter("@copyquotedates",     request.copyquotedates);
            sp.AddParameter("@usecurrentdate",     request.usecurrentdate);
            sp.AddParameter("@copylineitemnotes",  request.copylineitemnotes);
            sp.AddParameter("@combinesubs",        request.combinesubs);
            sp.AddParameter("@copydocuments",      request.copydocuments);
            sp.AddParameter("@neworderid",         SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@errno",              SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",             SqlDbType.NVarChar, ParameterDirection.Output);
            sp.ExecuteNonQuery();

            response.neworderid = sp.GetParameter("@neworderid").ToString().TrimEnd();
            response.errno      = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //----------------------------------------------------------------------------------------------------
        public static void UpdateOrderTimeStamp(string orderid)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("update dealorder");
            qry.Add("set datestamp = getutcdate()");
            qry.Add("where orderid = @orderid");
            qry.AddParameter("@orderid", orderid);

            qry.Execute();
        }
        //----------------------------------------------------------------------------------------------------
    }
}