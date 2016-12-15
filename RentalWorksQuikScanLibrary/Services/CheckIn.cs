using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RentalWorksQuikScanLibrary.Services
{
    class CheckIn
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetShowCreateContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select showcreatecontract = (case when exists (select *");
                qry.Add("                                               from ordertran with (nolock)");
                qry.Add("                                               where inreturncontractid = @contractid)");
                qry.Add("                                  then 'T'");
                qry.Add("                                  else 'F'");
                qry.Add("                             end)");
                qry.AddParameter("@contractid", request.contractid);
                qry.Execute();
                response.showcreatecontract = qry.GetField("showcreatecontract").ToBoolean();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetSerialInfo(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            response.serial = CheckIn.FuncSerialFrm(conn:         FwSqlConnection.RentalWorks,
                                                    orderid:      request.orderid,
                                                    warehouseid:  userLocation.warehouseId,
                                                    contractid:   request.contractid,
                                                    masterid:     request.masterid);

            response.serialitems = CheckIn.FuncSerialMeterIn2(conn:        FwSqlConnection.RentalWorks,
                                                              contractid:  request.contractid,
                                                              orderid:     request.orderid,
                                                              masterid:    request.masterid,
                                                              warehouseid: userLocation.warehouseId);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetSerialItems(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            response.serialitems = CheckIn.FuncSerialMeterIn2(conn:        FwSqlConnection.RentalWorks,
                                                              contractid:  request.contractid,
                                                              orderid:     request.orderid,
                                                              masterid:    request.masterid,
                                                              warehouseid: userLocation.warehouseId);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void SerialSessionIn(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            CheckIn.InsertSerialSession(conn:         FwSqlConnection.RentalWorks,
                                        contractid:   request.contractid,
                                        orderid:      request.orderid,
                                        masteritemid: request.masteritemid,
                                        rentalitemid: request.rentalitemid,
                                        activitytype: "I",
                                        usersid:      session.security.webUser.usersid,
                                        meter:        request.meter,
                                        toggledelete: request.toggledelete);

            response.serial = CheckIn.FuncSerialFrm(conn:         FwSqlConnection.RentalWorks,
                                                    orderid:      request.orderid,
                                                    warehouseid:  userLocation.warehouseId,
                                                    contractid:   request.contractid,
                                                    masterid:     request.masterid);
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic FuncSerialFrm(FwSqlConnection conn, string orderid, string warehouseid, string contractid, string masterid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from dbo.funcserialfrm(@orderid, @warehouseid, @contractid)");
            qry.Add(" where masterid = @masterid");
            qry.AddParameter("@orderid",      orderid);
            qry.AddParameter("@warehouseid",  warehouseid);
            qry.AddParameter("@contractid",   contractid);
            qry.AddParameter("@masterid",     masterid);
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic FuncSerialMeterIn2(FwSqlConnection conn, string contractid, string orderid, string masterid, string warehouseid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from dbo.funcserialmeterin2(@contractid, @orderid, @masterid, @warehouseid, '')");
            qry.AddParameter("@orderid",      orderid);
            qry.AddParameter("@warehouseid",  warehouseid);
            qry.AddParameter("@contractid",   contractid);
            qry.AddParameter("@masterid",     masterid);
            result = qry.QueryToDynamicList();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static void InsertSerialSession(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string rentalitemid, string activitytype, string usersid, string meter, string toggledelete)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("exec dbo.insertserialsession @contractid, @orderid, @masteritemid, @rentalitemid, @activitytype, @internalchar, @usersid, @meter, @toggledelete, @containeritemid, @containeroutcontractid");
            qry.AddParameter("@contractid",             contractid);
            qry.AddParameter("@orderid",                orderid);
            qry.AddParameter("@masteritemid",           masteritemid);
            qry.AddParameter("@rentalitemid",           rentalitemid);
            qry.AddParameter("@activitytype",           activitytype);
            qry.AddParameter("@internalchar",           "");
            qry.AddParameter("@usersid",                usersid);
            qry.AddParameter("@meter",                  meter);
            qry.AddParameter("@toggledelete",           toggledelete);
            qry.AddParameter("@containeritemid",        "");
            qry.AddParameter("@containeroutcontractid", "");
            qry.Execute();
        }
        //---------------------------------------------------------------------------------------------
    }
}
