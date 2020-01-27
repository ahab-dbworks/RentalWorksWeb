using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using RentalWorksQuikScan.Source;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    public class AssignItem
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void ItemSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "ItemSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = request.pageno;
            select.PageSize = request.pagesize;
            select.Add("select *");
            select.Add("from   dbo.funcassignasset2(@warehouseid)");
            if (!string.IsNullOrEmpty(request.searchvalue))
            {
                switch ((string)request.searchmode)
                {
                    case "ICODE":
                        select.Add("where masterno like @masterno");
                        select.AddParameter("@masterno", request.searchvalue + "%");
                        break;
                    case "DESCRIPTION":
                        select.Add("where master like @master");
                        select.AddParameter("@master", "%" + request.searchvalue + "%");
                        break;
                    case "TRACKEDBY":
                        select.Add("where trackedby like @trackedby");
                        select.AddParameter("@trackedby", request.searchvalue + "%");
                        break;
                    case "PONO":
                        select.Add("where orderno like @orderno");
                        select.AddParameter("@orderno", request.searchvalue + "%");
                        break;
                }
            }
            switch ((string)request.searchmode)
            {
                case "ICODE":
                    select.Add("order by masterno");
                    break;
                case "DESCRIPTION":
                    select.Add("order by master");
                    break;
                case "TRACKEDBY":
                    select.Add("order by masterno");
                    break;
                case "PONO":
                    select.Add("order by orderno, masterno");
                    break;
            }
            select.AddParameter("@warehouseid", userLocation.warehouseId);

            response.searchresults = qry.QueryToFwJsonTable(select, true);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetAssignableItemAssets(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetAssignableItemAssets";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            select.PageNo   = request.pageno;
            select.PageSize = request.pagesize;
            if (request.selectedrecord.rowtype == "I-CODE")
            {
                select.Add("select *");
                select.Add("  from dbo.funcassignassetrentalitem(@masterid, @warehouseid, @orderid, @statustype)");
                select.AddParameter("@orderid",    request.selectedstatus.orderid);
                select.AddParameter("@statustype", request.selectedstatus.statustype);
            }
            else if (request.selectedrecord.rowtype == "PO")
            {
                select.Add("select *");
                select.Add("  from dbo.funcassignporentalitem(@orderid, @masterid)");
                select.AddParameter("@orderid", request.selectedrecord.orderid);
            }
            if (!string.IsNullOrEmpty(request.searchvalue))
            {
                switch ((string)request.searchmode)
                {
                    case "BARCODE":
                        select.Add("where barcode like @barcode");
                        select.AddParameter("@barcode", request.searchvalue + "%");
                        break;
                    case "RFID":
                        select.Add("where rfid like @rfid");
                        select.AddParameter("@rfid", request.searchvalue + "%");
                        break;
                    case "SERIALNO":
                        select.Add("where mfgserial like @mfgserial");
                        select.AddParameter("@mfgserial", request.searchvalue + "%");
                        break;
                }
            }
            select.Add("order by barcode");
            select.AddParameter("@masterid",    request.selectedrecord.masterid);
            select.AddParameter("@warehouseid", userLocation.warehouseId);

            response.searchresults = qry.QueryToFwJsonTable(select, true);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void UpdateAssignableItemAsset(dynamic request, dynamic response, dynamic session)
        {
            string barcode, mfgserial, rfid;

            barcode   = (request.recorddata.barcode   != request.barcode)   ? request.barcode   : "";
            mfgserial = (request.recorddata.mfgserial != request.mfgserial) ? request.mfgserial : "";
            rfid      = (request.recorddata.rfid      != request.rfid)      ? request.rfid      : "";

            barcode   = barcode.ToUpper();
            mfgserial = (request.recorddata.mixedcaseserialno == "T") ? mfgserial : mfgserial.ToUpper();

            if (request.selectedrecord.rowtype == "I-CODE")
            {
                response.updateitem = UpdateAssignableItemAsset(request.recorddata.rentalitemid, request.selectedrecord.masterid, barcode, mfgserial, rfid, request.mfgdate);
            }
            else if (request.selectedrecord.rowtype == "PO")
            {
                response.updateitem = UpdatePOAssignableItemAsset(request.recorddata, request.selectedrecord.masterid, barcode, mfgserial, rfid, request.mfgdate, session.security.webUser.webusersid);
            }
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic UpdateAssignableItemAsset(string rentalitemid, string masterid, string barcode, string mfgserial, string rfid, FwDateTime mfgdate)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webupdaterentalitem");
            sp.AddParameter("@rentalitemid", rentalitemid);
            sp.AddParameter("@masterid",     masterid);
            sp.AddParameter("@barcode",      barcode);
            sp.AddParameter("@mfgserial",    mfgserial);
            sp.AddParameter("@rfid",         rfid);
            sp.AddParameter("@mfgdate",      ((mfgdate==null) ? System.DBNull.Value : mfgdate.GetSqlDate()));
            sp.AddParameter("@status",       SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();

            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic UpdatePOAssignableItemAsset(dynamic data, string masterid, string barcode, string mfgserial, string rfid, FwDateTime mfgdate, string webusersid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand sp;
            
            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webassignrentalitem");
            sp.AddParameter("@orderid",           data.orderid);
            sp.AddParameter("@receivecontractid", data.receivecontractid);
            sp.AddParameter("@webusersid",        webusersid);
            sp.AddParameter("@internalchar",      data.internalchar);
            sp.AddParameter("@barcodeholdingid",  data.barcodeholdingid);
            sp.AddParameter("@masterid",          masterid);
            sp.AddParameter("@barcode",           barcode);
            sp.AddParameter("@mfgserial",         mfgserial);
            sp.AddParameter("@rfid",              rfid);
            sp.AddParameter("@mfgdate",           ((mfgdate==null) ? System.DBNull.Value : mfgdate.GetSqlDate()));
            sp.AddParameter("@status",            SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",               SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();

            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void MultiScanTags(dynamic request, dynamic response, dynamic session)
        {
            FwSqlCommand qry, qry2, qry3, qry4;
            dynamic userLocation, recordstoupdate, validatetagsresult, record, updatedrecordinfo = new ExpandoObject(), selectedrecordupdate, selectedstatusupdate = null;
            List<dynamic> returnrecords = new List<dynamic>();
            int assignedcount = 0, exceptioncount = 0;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select * from dbo.funcvalidaterfidtag(@epcs)");
            qry.AddParameter("@epcs", request.epcs);
            validatetagsresult = qry.QueryToDynamicList2();

            qry2 = new FwSqlCommand(FwSqlConnection.RentalWorks);
            if (request.selectedrecord.rowtype == "I-CODE")
            {
                qry2.Add("select *");
                qry2.Add("  from dbo.funcassignassetrentalitem(@masterid, @warehouseid, @orderid, @statustype)");
                qry2.AddParameter("@orderid",    request.selectedstatus.orderid);
                qry2.AddParameter("@statustype", request.selectedstatus.statustype);
            }
            else if (request.selectedrecord.rowtype == "PO")
            {
                qry2.Add("select *");
                qry2.Add("  from dbo.funcassignporentalitem(@orderid, @masterid)");
                qry2.AddParameter("@orderid", request.selectedrecord.orderid);
            }
            qry2.AddParameter("@masterid",    request.selectedrecord.masterid);
            qry2.AddParameter("@warehouseid", userLocation.warehouseId);
            recordstoupdate = qry2.QueryToDynamicList2();

            for (int i = 0; i < validatetagsresult.Count; i++)
            {
                record      = new ExpandoObject();
                record.rfid = validatetagsresult[i].rfid;
                if ((validatetagsresult[i].status == "OK") && (recordstoupdate.Count > 0))
                {
                    if (request.selectedrecord.rowtype == "I-CODE")
                    {
                        updatedrecordinfo = UpdateAssignableItemAsset(recordstoupdate[0].rentalitemid, request.selectedrecord.masterid, "", "", record.rfid, null);
                    }
                    else if (request.selectedrecord.rowtype == "PO")
                    {
                        updatedrecordinfo = UpdatePOAssignableItemAsset(recordstoupdate[0], request.selectedrecord.masterid, "", "", record.rfid, null, session.security.webUser.webusersid);
                    }

                    if (updatedrecordinfo.status == 0)
                    {
                        record.status    = "assigned";
                        record.statusmsg = "Assigned";
                        assignedcount++;
                        recordstoupdate.RemoveAt(0);
                    }
                    else
                    {
                        record.status    = "exception";
                        record.statusmsg = "Exception";
                        record.msg       = updatedrecordinfo.msg;
                        exceptioncount++;
                    }
                }
                else
                {
                    if (validatetagsresult[i].masterid == request.selectedrecord.masterid)
                    {
                        record.status    = "processed";
                        record.statusmsg = "Already Assigned";
                    }
                    else if ((validatetagsresult[i].masterid != "") && (validatetagsresult[i].masterid != request.selectedrecord.masterid))
                    {
                        record.status    = "exception";
                        record.statusmsg = "Exception";
                        record.msg       = validatetagsresult[i].statusmessage;
                        exceptioncount++;
                    }
                    else if (recordstoupdate.Count == 0)
                    {
                        record.status    = "exception";
                        record.statusmsg = "Exception";
                        record.msg       = "No pending items to assign tag to.";
                        exceptioncount++;
                    }
                }
                returnrecords.Add(record);
            }

            response.records        = returnrecords;
            response.assignedcount  = assignedcount;
            response.exceptioncount = exceptioncount;

            if (request.selectedrecord.rowtype == "I-CODE")
            {
                qry4 = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qry4.Add("select *");
                qry4.Add("  from dbo.funcassignassetdetail(@warehouseid, @masterid)");
                qry4.Add(" where orderid    = @orderid");
                qry4.Add("   and statustype = @statustype");
                qry4.AddParameter("@masterid",    request.selectedrecord.masterid);
                qry4.AddParameter("@warehouseid", userLocation.warehouseId);
                qry4.AddParameter("@orderid",     request.selectedstatus.orderid);
                qry4.AddParameter("@statustype",  request.selectedstatus.statustype);
                selectedstatusupdate = qry4.QueryToDynamicObject2();
                response.selectedstatusupdate = selectedstatusupdate;
            }

            qry3 = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry3.Add("select top 1 *");
            qry3.Add("  from dbo.funcassignasset2(@warehouseid)");
            qry3.Add(" where masterid = @masterid");
            qry3.Add("   and rowtype  = @rowtype");
            qry3.AddParameter("@masterid",    request.selectedrecord.masterid);
            qry3.AddParameter("@rowtype",     request.selectedrecord.rowtype);
            qry3.AddParameter("@warehouseid", userLocation.warehouseId);
            selectedrecordupdate = qry3.QueryToDynamicObject2();

            response.selectedrecordupdate = selectedrecordupdate;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetBarcodeRFIDItem(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;
            FwSqlCommand qry;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("mfgdate", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from dbo.funcgetrentalitem(@code, @warehouseid, @webusersid)");
            qry.AddParameter("@code",        request.code);
            qry.AddParameter("@warehouseid", userLocation.warehouseId);
            qry.AddParameter("@webusersid",  session.security.webUser.webusersid);

            response.recorddata = qry.QueryToDynamicObject2();
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetAssignAssetDetails(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;
            FwSqlCommand qry;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.Add("select *");
            //qry.Add("  from dbo.funcassignassetdetail(@warehouseid, @masterid)");
            qry.Add("WITH Assignment_CTE(masterid, masterno, master, trackedby, qty, orderid, orderno, orderdesc, statustype, allowmassrfidassignment, orderby)");
            qry.Add("AS");
            qry.Add("(select *, orderby = 1");
            qry.Add("from funcassignassetdetail('0000000H', 'A0000949')");
            qry.Add("where statustype = 'IN'");
            qry.Add("union");
            qry.Add("select *, orderby = 2");
            qry.Add("from funcassignassetdetail('0000000H', 'A0000949') ");
            qry.Add("where statustype <> 'IN')");

            qry.Add("select masterid, masterno, master, trackedby, qty, orderid, orderno, orderdesc, statustype, allowmassrfidassignment");
            qry.Add("from Assignment_CTE");
            qry.Add("order by orderby, statustype");
            qry.AddParameter("@warehouseid", userLocation.warehouseId);
            qry.AddParameter("@masterid",    request.selectedrecord.masterid);

            response.results = qry.QueryToDynamicList2();
        }
        //---------------------------------------------------------------------------------------------
    }
}
