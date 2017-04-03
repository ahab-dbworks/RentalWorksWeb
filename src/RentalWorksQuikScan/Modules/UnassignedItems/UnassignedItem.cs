using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;

namespace RentalWorksQuikScan.Modules
{
    public class UnassignedItem
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetAssignableItems(dynamic request, dynamic response, dynamic session)
        {
            FwSqlCommand qry;
            DataTable dt         = new DataTable();
            dynamic result       = new ExpandoObject();
            string mode          = request.searchmode;
            string searchvalue   = request.searchvalue;
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select * from dbo.funcassignasset(@warehouseid)");
            if (mode == "PO")
            {
                qry.Add("where rowtype = 'PO'");
                if (!string.IsNullOrEmpty(searchvalue))
                {
                    qry.Add("and orderno like @orderno");
                    qry.AddParameter("@orderno", searchvalue + "%");
                }
            }
            else if (mode == "ITEM")
            {
                qry.Add("where rowtype = 'I-CODE'");
                if (!string.IsNullOrEmpty(searchvalue))
                {
                    qry.Add("and masterno like @masterno");
                    qry.AddParameter("@masterno", searchvalue + "%");
                }
            }
            qry.Add("order by statusdate desc");
            qry.AddParameter("@warehouseid", userLocation.warehouseId);
            dt = qry.QueryToTable();
            result = new ExpandoObject[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result[i]                   = new ExpandoObject();
                result[i].masterid          = new FwDatabaseField(dt.Rows[i]["masterid"]).ToString().TrimEnd();
                result[i].masterno          = new FwDatabaseField(dt.Rows[i]["masterno"]).ToString().TrimEnd();
                result[i].master            = new FwDatabaseField(dt.Rows[i]["master"]).ToString().TrimEnd();
                result[i].trackedby         = new FwDatabaseField(dt.Rows[i]["trackedby"]).ToString().TrimEnd();
                result[i].qty               = new FwDatabaseField(dt.Rows[i]["qty"]).ToString().TrimEnd();
                result[i].orderid           = new FwDatabaseField(dt.Rows[i]["orderid"]).ToString().TrimEnd();
                result[i].receivecontractid = new FwDatabaseField(dt.Rows[i]["receivecontractid"]).ToString().TrimEnd();
                result[i].orderno           = new FwDatabaseField(dt.Rows[i]["orderno"]).ToString().TrimEnd();
                result[i].orderdesc         = new FwDatabaseField(dt.Rows[i]["orderdesc"]).ToString().TrimEnd();
                result[i].vendor            = new FwDatabaseField(dt.Rows[i]["vendor"]).ToString().TrimEnd();
                result[i].statusdate        = new FwDatabaseField(dt.Rows[i]["statusdate"]).ToShortDateString();
                result[i].rowtype           = new FwDatabaseField(dt.Rows[i]["rowtype"]).ToString().TrimEnd();
            }

            response.assignableitems = result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetPOAssignableItems(dynamic request, dynamic response, dynamic session)
        {
            FwSqlCommand qry;
            dynamic result = new ExpandoObject();
            string orderid = request.orderid;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select * from dbo.funcassignpoitems(@orderid)");
            qry.AddParameter("@orderid", orderid);
            result = qry.QueryToDynamicList();

            response.assignableitems = result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetAssignableItemAssets(dynamic request, dynamic response, dynamic session)
        {
            if (request.mode == "ITEM")
            {
                response.assignableitemassets = GetItemAssets(request.masterid, session.security.webUser.usersid);
            }
            else if (request.mode == "PO")
            {
                response.assignableitemassets = GetPOItemAssets(request.orderid, masterid: request.masterid);
            }
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic GetItemAssets(string masterid, string usersid)
        {
            FwSqlCommand qry;
            dynamic result       = new ExpandoObject();
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select * from dbo.funcassignrentalitem(@masterid, @warehouseid)");
            qry.AddParameter("@masterid",    masterid);
            qry.AddParameter("@warehouseid", userLocation.warehouseId);
            result = qry.QueryToDynamicList();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic GetPOItemAssets(string orderid, string masterid)
        {
            FwSqlCommand qry;
            dynamic result = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select * from dbo.funcassignporentalitem(@orderid, @masterid)");
            qry.AddParameter("@orderid",  orderid);
            qry.AddParameter("@masterid", masterid);
            result = qry.QueryToDynamicList();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void UpdateAssignableItemAsset(dynamic request, dynamic response, dynamic session)
        {
            if (request.mode == "ITEM")
            {
                response.updateitem           = UpdateAssignableItemAsset(request.assetdata, request.itemdata.masterid, request.barcode, request.mfgserial, request.rfid, request.mfgdate);
                response.assignableitemassets = GetItemAssets(request.itemdata.masterid, session.security.webUser.usersid);
            }
            else if (request.mode == "PO")
            {
                response.updateitem           = UpdatePOAssignableItemAsset(request.assetdata, request.itemdata.masterid, request.barcode, request.mfgserial, request.rfid, request.mfgdate, session.security.webUser.webusersid);
                response.assignableitemassets = GetPOItemAssets(request.itemdata.orderid, masterid: request.itemdata.masterid);
            }
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic UpdateAssignableItemAsset(dynamic data, string masterid, string barcode, string mfgserial, string rfid, string mfgdate)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand sp;
            string barcodevalue, mfgserialvalue, rfidvalue;

            barcodevalue   = (data.barcode   != barcode)   ? barcode   : "";
            mfgserialvalue = (data.mfgserial != mfgserial) ? mfgserial : "";
            rfidvalue      = (data.rfid      != rfid)      ? rfid      : "";
            
            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webupdaterentalitem");
            sp.AddParameter("@rentalitemid", data.rentalitemid);
            sp.AddParameter("@masterid",     masterid);
            sp.AddParameter("@barcode",      barcodevalue);
            sp.AddParameter("@mfgserial",    mfgserialvalue);
            sp.AddParameter("@rfid",         rfidvalue);
            sp.AddParameter("@mfgdate",      mfgdate);
            sp.AddParameter("@status",       SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();

            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic UpdatePOAssignableItemAsset(dynamic data, string masterid, string barcode, string mfgserial, string rfid, string mfgdate, string webusersid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand sp;
            string barcodevalue, mfgserialvalue, rfidvalue;

            barcodevalue   = (data.barcode   != barcode)   ? barcode   : "";
            mfgserialvalue = (data.mfgserial != mfgserial) ? mfgserial : "";
            rfidvalue      = (data.rfid      != rfid)      ? rfid      : "";
            
            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webassignrentalitem");
            sp.AddParameter("@orderid",           data.orderid);
            sp.AddParameter("@receivecontractid", data.receivecontractid);
            sp.AddParameter("@webusersid",        webusersid);
            sp.AddParameter("@internalchar",      data.internalchar);
            sp.AddParameter("@barcodeholdingid",  data.barcodeholdingid);
            sp.AddParameter("@masterid",          masterid);
            sp.AddParameter("@barcode",           barcodevalue);
            sp.AddParameter("@mfgserial",         mfgserialvalue);
            sp.AddParameter("@rfid",              rfidvalue);
            sp.AddParameter("@mfgdate",           mfgdate);
            sp.AddParameter("@status",            SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",               SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();

            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
