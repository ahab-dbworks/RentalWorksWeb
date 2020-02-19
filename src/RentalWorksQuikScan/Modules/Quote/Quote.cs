using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using RentalWorksQuikScan.Source;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    public class Quote
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void LoadItems(dynamic request, dynamic response, dynamic session)
        {
            FwSqlSelect select = new FwSqlSelect();
            FwSqlCommand qry   = new FwSqlCommand(FwSqlConnection.RentalWorks);
            decimal grandtotal = 0.0M;

            select.PageNo   = request.pageno;
            select.PageSize = request.pagesize;
            qry.AddColumn("qtyordered",         false, FwJsonDataTableColumn.DataTypes.Integer);
            qry.AddColumn("price",              false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("periodextended",     false, FwJsonDataTableColumn.DataTypes.Decimal);
            select.Add("select *");
            select.Add("  from dbo.qsmasteritem(@orderid, @masteritemid)");
            select.Add("order by orderby");
            select.AddParameter("@orderid",        request.orderid);
            select.AddParameter("@masteritemid",   string.Empty);

            response.searchresults = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < response.searchresults.Rows.Count; i++)
            {
                grandtotal += response.searchresults.Rows[i][response.searchresults.ColumnIndex["periodextended"]];
            }
            response.grandtotal = grandtotal;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ScanItem(dynamic request, dynamic response, dynamic session)
        {
            response.iteminfo = RwAppData.WebGetItemStatus(FwSqlConnection.RentalWorks, session.security.webUser.usersid, request.enteredvalue);

            if (response.iteminfo.trackedby != "QUANTITY")
            {
                request.qty      = 1;
                request.masterno = request.enteredvalue;
                AddItem(request, response, session);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void AddItem(dynamic request, dynamic response, dynamic session)
        {
            response.insert = QSInsertMasterItem(orderid:      request.orderid,
                                                 barcode:      request.masterno,
                                                 qtyordered:   request.qty,
                                                 webusersid:   session.security.webUser.webusersid,
                                                 masteritemid: "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void UpdateItem(dynamic request, dynamic response, dynamic session)
        {
            response.update = QSInsertMasterItem(orderid:      request.orderid,
                                                 barcode:      request.masterno,
                                                 qtyordered:   request.qty,
                                                 webusersid:   session.security.webUser.webusersid,
                                                 masteritemid: request.masteritemid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void DeleteItem(dynamic request, dynamic response, dynamic session)
        {
            response.deleteitem = QSDeleteMasterItem(orderid:      request.orderid,
                                                     masteritemid: request.masteritemid,
                                                     rentalitemid: request.rentalitemid,
                                                     qtyremoved:   request.qtyremoved,
                                                     webusersid:   session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void SubmitQuote(dynamic request, dynamic response, dynamic session)
        {
            response.submit = QSSubmitQuote(orderid:    request.orderid,
                                            webusersid: session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CancelQuote(dynamic request, dynamic response, dynamic session)
        {
            response.cancel = QSCancelQuote(orderid:    request.orderid,
                                            webusersid: session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void SearchItems(dynamic request, dynamic response, dynamic session)
        {
            response.items = SearchQuantityItems(request.searchvalue, request.warehouseid);
        }
        //---------------------------------------------------------------------------------------------
        public static List<dynamic> QSMasterItem(string orderid, string masteritemid)
        {
            List<dynamic> result;

            FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from dbo.qsmasteritem(@orderid, @masteritemid)");
            qry.Add("order by orderby");
            qry.AddParameter("@orderid",      orderid);
            qry.AddParameter("@masteritemid", masteritemid);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static dynamic QSInsertMasterItem(string orderid, string barcode, int qtyordered, string masteritemid, string webusersid)
        {
            FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.qsinsertmasteritem");
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@barcode",      barcode);
            sp.AddParameter("@qtyordered",   qtyordered);
            sp.AddParameter("@webusersid",   webusersid);
            sp.AddParameter("@masteritemid", SqlDbType.Char,    ParameterDirection.InputOutput, masteritemid);
            sp.AddParameter("@errno",        SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@errmsg",       SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();

            dynamic result      = new ExpandoObject();
            result.masteritemid = sp.GetParameter("@masteritemid").ToString();
            result.errno        = sp.GetParameter("@errno").ToString();
            result.errmsg       = sp.GetParameter("@errmsg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static dynamic QSDeleteMasterItem(string orderid, string masteritemid, string rentalitemid, int qtyremoved, string webusersid)
        {
            FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.qsdeletemasteritem");
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@rentalitemid", rentalitemid);
            sp.AddParameter("@webusersid",   webusersid);
            sp.AddParameter("@qty",          qtyremoved);
            sp.AddParameter("@errno",        SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",       SqlDbType.VarChar,  ParameterDirection.Output);
            sp.Execute();

            dynamic result = new ExpandoObject();
            result.errno   = sp.GetParameter("@errno").ToString();
            result.errmsg  = sp.GetParameter("@errmsg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static dynamic QSSubmitQuote(string orderid, string webusersid)
        {
            FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.qssubmitquote");
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@webusersid",   webusersid);
            sp.AddParameter("@errno",        SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",       SqlDbType.VarChar,  ParameterDirection.Output);
            sp.Execute();

            dynamic result = new ExpandoObject();
            result.errno   = sp.GetParameter("@errno").ToString();
            result.errmsg  = sp.GetParameter("@errmsg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static dynamic QSCancelQuote(string orderid, string webusersid)
        {
            FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.qscancelquote");
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@webusersid",   webusersid);
            sp.AddParameter("@errno",        SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",       SqlDbType.VarChar,  ParameterDirection.Output);
            sp.Execute();

            dynamic result = new ExpandoObject();
            result.errno   = sp.GetParameter("@errno").ToString();
            result.errmsg  = sp.GetParameter("@errmsg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static List<dynamic> SearchQuantityItems(string searchvalue, string warehouseid)
        {
            List<dynamic> result;

            FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from masterview with (nolock)");
            qry.Add(" where warehouseid = @warehouseid");
            qry.Add("   and availfor in ('R')");
            qry.Add("   and availfrom in ('W')");
            qry.Add("   and inactive <> 'T'");
            qry.Add("   and hasqty = 'T'");
            qry.Add("   and trackedby = 'QUANTITY'");
            qry.Add("   and class = 'I'");
            qry.Add("   and master like '%' + @searchvalue + '%'");
            qry.Add("order by master");
            qry.AddParameter("@searchvalue", searchvalue);
            qry.AddParameter("@warehouseid", warehouseid);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}