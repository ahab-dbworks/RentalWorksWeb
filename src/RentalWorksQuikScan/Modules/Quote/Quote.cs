﻿using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    public class Quote
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void LoadModuleProperties(dynamic request, dynamic response, dynamic session)
        {
            response.syscontrol = LoadSysControlValues();
        }
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
            response.insert = QSInsertMasterItem(orderid:          request.orderid,
                                                 barcode:          request.masterno,
                                                 qtyordered:       request.qty,
                                                 webusersid:       session.security.webUser.webusersid,
                                                 masteritemid:     "",
                                                 spaceid:          (request.locationdata != null) ? request.locationdata.spaceid : "",
                                                 spacetypeid:      (request.locationdata != null) ? request.locationdata.spacetypeid : "",
                                                 facilitiestypeid: (request.locationdata != null) ? request.locationdata.facilitiestypeid : "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void UpdateItem(dynamic request, dynamic response, dynamic session)
        {
            response.update = QSInsertMasterItem(orderid:          request.orderid,
                                                 barcode:          request.masterno,
                                                 qtyordered:       request.qty,
                                                 webusersid:       session.security.webUser.webusersid,
                                                 masteritemid:     request.masteritemid,
                                                 spaceid:          (request.locationdata != null) ? request.locationdata.spaceid : "",
                                                 spacetypeid:      (request.locationdata != null) ? request.locationdata.spacetypeid : "",
                                                 facilitiestypeid: (request.locationdata != null) ? request.locationdata.facilitiestypeid : "");
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
            response.submit = QSSubmitQuote(orderid:        request.orderid,
                                            webusersid:     session.security.webUser.webusersid,
                                            createcontract: request.createcontract);
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
        public static dynamic QSInsertMasterItem(string orderid, string barcode, int qtyordered, string masteritemid, string webusersid, string spaceid, string spacetypeid, string facilitiestypeid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.qsinsertmasteritem");
            sp.AddParameter("@orderid",          orderid);
            sp.AddParameter("@barcode",          barcode);
            sp.AddParameter("@qtyordered",       qtyordered);
            sp.AddParameter("@webusersid",       webusersid);
            sp.AddParameter("@spaceid",          spaceid);
            sp.AddParameter("@spacetypeid",      spacetypeid);
            sp.AddParameter("@facilitiestypeid", facilitiestypeid);
            sp.AddParameter("@masteritemid",     SqlDbType.Char,    ParameterDirection.InputOutput, masteritemid);
            sp.AddParameter("@errno",            SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@errmsg",           SqlDbType.VarChar, ParameterDirection.Output);
            try
            {
                sp.Execute();
                result.masteritemid = sp.GetParameter("@masteritemid").ToString();
                result.errno        = sp.GetParameter("@errno").ToString();
                result.errmsg       = sp.GetParameter("@errmsg").ToString();
            }
            catch (Exception ex)
            {
                int indexofBEGINTRANSACTION = ex.Message.IndexOf("BEGIN TRANSACTION");
                string error                = ex.Message.Remove(0, indexofBEGINTRANSACTION+17);
                result.errno                = "1";
                result.errmsg               = error;
            }
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
        public static dynamic QSSubmitQuote(string orderid, string webusersid, string createcontract)
        {
            FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.qssubmitquote");
            sp.AddParameter("@orderid",        orderid);
            sp.AddParameter("@webusersid",     webusersid);
            sp.AddParameter("@createcontract", createcontract);
            sp.AddParameter("@errno",          SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",         SqlDbType.VarChar,  ParameterDirection.Output);
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
            string[] searchvalues;

            searchvalues = searchvalue.Split(' ');

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

            if (searchvalues.Length == 1)
            {
                qry.Add("   and master like '%' + @searchvalue + '%'");
                qry.AddParameter("@searchvalue", searchvalues[0]);
            }
            else
            {
                qry.Add("   and (");
                for (int i = 0; i < searchvalues.Length; i++)
                {
                    if (i > 0)
                    {
                        qry.Add(" and ");
                    }
                    qry.Add("(master like '%' + @searchvalue" + i + " + '%')");
                    qry.AddParameter("@searchvalue" + i, searchvalues[i]);
                }
                qry.Add(")");
            }

            qry.Add("order by master");
            qry.AddParameter("@warehouseid", warehouseid);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void SearchLocations(dynamic request, dynamic response, dynamic session)
        {
            response.locations = SearchOrderLocations(orderid:     request.orderid,
                                                      searchvalue: request.searchvalue);
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic SearchOrderLocations(string orderid, string searchvalue)
        {
            dynamic result               = new ExpandoObject();
            FwSqlCommand qry             = new FwSqlCommand(FwSqlConnection.RentalWorks);
            string includefacilitiestype = "F";
            dynamic applicationoptions   = FwSqlData.GetApplicationOptions(FwSqlConnection.RentalWorks);
            string[] searchvalues        = searchvalue.Split(' ');

            if (applicationoptions.facilities.enabled)
            {
                dynamic controlresult = LoadSysControlValues();

                if ((controlresult.itemsinrooms == "T") && (controlresult.facilitytypeincurrentlocation == "T"))
                {
                    includefacilitiestype = "T";
                }
            }

            qry.Add("select *");
            qry.Add("  from dbo.funcorderspacelocation(@orderid, @includefacilitiestype)");
            qry.Add(" where orderid = orderid");

            if (searchvalues.Length == 1)
            {
                qry.Add("   and location like '%' + @searchvalue + '%'");
                qry.AddParameter("@searchvalue", searchvalues[0]);
            }
            else
            {
                qry.Add("   and (");
                for (int i = 0; i < searchvalues.Length; i++)
                {
                    if (i > 0)
                    {
                        qry.Add(" and ");
                    }
                    qry.Add("(location like '%' + @searchvalue" + i + " + '%')");
                    qry.AddParameter("@searchvalue" + i, searchvalues[i]);
                }
                qry.Add(")");
            }

            qry.AddParameter("@orderid",               orderid);
            qry.AddParameter("@includefacilitiestype", includefacilitiestype);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic LoadSysControlValues()
        {
            FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select top 1 itemsinrooms, facilitytypeincurrentlocation");
            qry.Add("  from syscontrol with (nolock)");
            qry.Add(" where controlid = '1'");
            dynamic result = qry.QueryToDynamicObject2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}