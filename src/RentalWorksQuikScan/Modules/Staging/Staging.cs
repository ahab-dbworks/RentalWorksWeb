using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    class Staging
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void LoadModuleProperties(dynamic request, dynamic response, dynamic session)
        {
            response.syscontrol = LoadSysControlValues();
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetSearchResults(dynamic request, dynamic response, dynamic session)
        {
            string searchmode = request.searchmode;
            var userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            string locationid = userLocation.locationId;
            string moduletype = request.moduletype;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;

                switch(moduletype)
                {
                    case RwConstants.ModuleTypes.Order:
                        switch (searchmode)
                        {
                            case "orderno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentto", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select *");
                                select.Add("from dbo.funcorderstaging(@warehouseid)");
                                select.Add("where orderno = orderno");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddOrderBy("orderdate desc, orderno desc");
                                select.AddWhere("orderno like @searchvalue");
                                select.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "orderdesc":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentto", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select *");
                                select.Add("from dbo.funcorderstaging(@warehouseid)");
                                select.Add("where orderno = orderno");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("orderdesc like @searchvalue");
                                select.AddOrderBy("orderdate desc, orderno desc");
                                select.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                                break;
                            case "deal":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentto", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select *");
                                select.Add("from dbo.funcorderstaging(@warehouseid) o");
                                select.Add("where orderno = orderno");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("deal like @searchvalue");
                                select.AddOrderBy("orderdate desc, orderno desc");
                                select.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                                break;
                            case "sessionno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, orderno, status, statusdate, sessionno, dealno, deal, usersid, username, contractid");
                                select.Add("from suspendview v with (nolock)");
                                select.Add("where v.contracttype = 'OUT'");
                                select.Add("  and v.ordertype    = 'O'");
                                select.Add("  and v.locationid   = @locationid");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.Parse();
                                select.AddWhere("sessionno like @searchvalue");
                                select.AddOrderBy("statusdate desc, sessionno desc");
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                        }
                        break;
                    case RwConstants.ModuleTypes.Truck:
                        switch (searchmode)
                        {
                            case "orderno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                select.Add("select orderid, orderdesc, status, orderno");
                                select.Add("from dbo.functruck() o");
                                select.Add("where o.locationid = @locationid");
                                select.Add("  and o.status not in ('INACTIVE')");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddOrderBy("orderno desc");
                                select.AddWhere("orderno like @searchvalue");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "orderdesc":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                select.Add("select orderid, orderdesc, status, orderno");
                                select.Add("from dbo.functruck() o");
                                select.Add("where o.locationid = @locationid");
                                select.Add("  and o.status not in ('INACTIVE')");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("orderdesc like @searchvalue");
                                select.AddOrderBy("orderno");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "sessionno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("contractdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select contractdate,contracttime, orderid, orderdesc, orderno, status, statusdate, sessionno, dealno, deal, usersid, username, contractid");
                                select.Add("from suspendview v with (nolock)");
                                select.Add("where v.contracttype = 'OUT'");
                                select.Add("  and v.locationid   = @locationid");
                                select.Add("  and v.dealid       = ''");
                                select.Add("  and v.ordertype    = 'P'");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("sessionno like @searchvalue");
                                //select.AddOrderBy("statusdate desc, sessionno desc");
                                select.AddOrderBy("contractdate desc, contracttime desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                        }
                        break;
                    case RwConstants.ModuleTypes.Transfer:
                        switch (searchmode)
                        {
                            case "orderno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("shipdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("receivedate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, fromwarehouse, towarehouse, department, origorderno, pickdate, shipdate, receivedate");
                                select.Add("from transferview tv with (nolock)");
                                select.Add("where status in ('ACTIVE','CONFIRMED')");
                                select.Add("  and fromwarehouseid = @warehouseid");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddOrderBy("orderno desc");
                                select.AddWhere("orderno like @searchvalue");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@warehouseid", userLocation.warehouseId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "orderdesc":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("shipdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("receivedate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, fromwarehouse, towarehouse, department, origorderno, pickdate, shipdate, receivedate");
                                select.Add("from transferview tv with (nolock)");
                                select.Add("where status in ('ACTIVE','CONFIRMED')");
                                select.Add("  and fromwarehouseid = @warehouseid");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("orderdesc like @searchvalue");
                                select.AddOrderBy("orderno desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@warehouseid", userLocation.warehouseId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "sessionno":
                                select.Add("select contractdate, contracttime, orderid, orderdesc, orderno, status, statusdate, sessionno, usersid, username, contractid, warehouse");
                                select.Add("from  suspendview v with (nolock)");
                                select.Add("where v.contracttype = 'MANIFEST'");
                                select.Add("  and   v.locationid   = @locationid");
                                select.Add("  and   v.dealid       = ''");
                                select.Add("  and  v.ordertype = 'T'");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("sessionno like @searchvalue");
                                select.AddOrderBy("contractdate desc, contracttime desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");

                                break;
                        }
                        break;
                }
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }

        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "activitytype,orderid")]
        public static void GetResponsiblePerson(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.getresponsibleperson"))
            {
                sp.AddParameter("@orderid", request.orderid);
                sp.AddParameter("@showresponsibleperson", SqlDbType.Char,    ParameterDirection.Output);
                sp.AddParameter("@responsibleperson",     SqlDbType.VarChar, ParameterDirection.Output);
                sp.AddParameter("@responsiblepersonid",   SqlDbType.Char,    ParameterDirection.Output);
                sp.Execute();
                response.responsibleperson = new ExpandoObject();
                response.responsibleperson.showresponsibleperson = sp.GetParameter("@showresponsibleperson").ToString().Trim();
                response.responsibleperson.responsibleperson     = sp.GetParameter("@responsibleperson").ToString().Trim();
                response.responsibleperson.responsiblepersonid   = sp.GetParameter("@responsiblepersonid").ToString().Trim();
            }
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("person", false);
                qry.AddColumn("contactid", false);
                qry.Add("select text=person, value=contactid");
                qry.Add("from inventorycontactview");
                qry.Add("where responsibleperson = 'T'");
                qry.Add("order by person");
                response.responsibleperson.responsiblepersons    = qry.QueryToDynamicList2();
            }
        }
        //---------------------------------------------------------------------------------------------
        public static bool IsSuspendedSessionsEnabled()
        {
            bool isenabled = false;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select isenabled = dbo.quikscanstagebysession()");
                qry.Execute();
                isenabled = qry.GetField("isenabled").ToBoolean();
            }
            return isenabled;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid")]
        public static void GetOrderSuspendedSessions(dynamic request, dynamic response, dynamic session)
        {
            string usersid = session.security.webUser.usersid;
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, usersid);
            string locationid = userLocation.locationId;
            string orderid = request.orderid;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;

                select.Add("select v.contractid, v.status, v.statusdate, v.sessionno, v.dealno, v.deal, v.orderno, v.orderdesc, v.usersid, v.username, oc.orderid");
                select.Add("from suspendview v with (nolock) join ordercontract oc with (nolock) on (v.contractid = oc.contractid)");
                select.Add("where v.contracttype = 'OUT'");
                select.Add("  and v.ordertype    = 'O'");
                select.Add("  and v.locationid   = @locationid");
                select.Add("  and oc.orderid     = @orderid");
                select.AddParameter("@locationid", locationid);
                select.AddParameter("@orderid", orderid);
                select.Parse();
                select.AddOrderBy("statusdate desc");
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "")]
        public static void CreateSuspendedSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.createoutcontract"))
            {
                sp.AddParameter("@orderid", request.orderid);
                sp.AddParameter("@usersid", session.security.webUser.usersid);
                sp.AddParameter("@notes", string.Empty);
                sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                sp.Execute();
                response.contractid = sp.GetParameter("@contractid").ToString().Trim();
            }
            response.sessionno = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "contract", "contractid", response.contractid, "sessionno");
            if (!string.IsNullOrEmpty(response.contractid))
            {
                using (FwSqlCommand cmd = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    cmd.Add("update contract");
                    cmd.Add("set forcedsuspend = 'G'");
                    cmd.Add("where contractid = @contractid");
                    cmd.AddParameter("@contractid", response.contractid);
                    cmd.Execute();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public static void GetPendingItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetPendingItems";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            session.userLocation = RwAppData.GetUserLocation(conn: FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "Your user account requires a warehouse to peform this action.", session.userLocation.warehouseId);
            string searchMode = request.searchmode;
            string searchValue = request.searchvalue;
            int pageNo = 0;
            int pageSize = 0;
            if (FwValidate.IsPropertyDefined(request, "pageno"))
            {
                pageNo = request.pageno;
            }
            if (FwValidate.IsPropertyDefined(request, "pagesize"))
            {
                pageSize = request.pagesize;
            }
            response.searchresults = RwAppData.GetStagingPendingItems(conn:        FwSqlConnection.RentalWorks,
                                                                      orderId:     request.orderid,
                                                                      warehouseId: session.userLocation.warehouseId,
                                                                      contractId:  request.contractid,
                                                                      searchMode:  searchMode,
                                                                      searchValue: searchValue,
                                                                      pageNo:      pageNo,
                                                                      pageSize:    pageSize);
        }
        //----------------------------------------------------------------------------------------------------
        public static FwJsonDataTable funcstaged(FwSqlConnection conn, string orderid, string warehouseid, bool summary, string searchMode, string searchValue, int pageNo, int pageSize)
        {
            using (var qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("rectype", false);
                qry.AddColumn("masteritemid", false);
                qry.AddColumn("description", false);
                qry.AddColumn("masterid", false);
                qry.AddColumn("masterno", false);
                qry.AddColumn("barcode", false);
                qry.AddColumn("quantity", false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("vendorid", false);
                qry.AddColumn("vendor", false);
                qry.AddColumn("itemclass", false);
                qry.AddColumn("trackedby", false);
                qry.AddColumn("consignorid", false);
                qry.AddColumn("consignoragreementid", false);
                var select = new FwSqlSelect();
                select.PageNo = pageNo;
                select.PageSize = pageSize;
                select.Add("select rectype, masteritemid, description, masterid, masterno, barcode, quantity, vendorid, vendor, itemclass, trackedby, consignorid, consignoragreementid, orderby");
                select.Add("from dbo.funcstaged(@orderid, @summary)");
                select.Add("where warehouseid = @warehouseid");
                if (searchMode == "description")
                {
                    select.AddWhere("description like @searchvalue");
                    select.AddParameter("@searchvalue", "%" + searchValue + "%");
                }
                select.Add("order by orderby");
                select.AddParameter("@orderid", orderid);
                select.AddParameter("@summary", FwConvert.LogicalToCharacter(summary));
                select.AddParameter("@warehouseid", warehouseid);
                dynamic result = new ExpandoObject();
                result = qry.QueryToFwJsonTable(select, false);
                return result;
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static FwJsonDataTable funccheckedout(FwSqlConnection conn, string contractid, string searchMode, string searchValue, int pageNo, int pageSize)
        {
            using (var qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("rectype", false);
                qry.AddColumn("masteritemid", false);
                qry.AddColumn("description", false);
                qry.AddColumn("masterid", false);
                qry.AddColumn("masterno", false);
                qry.AddColumn("barcode", false);
                qry.AddColumn("quantity", false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("vendorid", false);
                qry.AddColumn("vendor", false);
                qry.AddColumn("itemclass", false);
                qry.AddColumn("trackedby", false);
                qry.AddColumn("consignorid", false);
                qry.AddColumn("consignoragreementid", false);
                var select = new FwSqlSelect();
                select.PageNo = pageNo;
                select.PageSize = pageSize;
                select.Add("select rectype, masteritemid, description, masterid, masterno, barcode, quantity, vendorid, vendor, itemclass, trackedby, consignorid, consignoragreementid, orderby");
                select.Add("from dbo.funccheckedout(@contractid)");
                if (searchMode == "description")
                {
                    select.AddWhere("description like @searchvalue");
                    select.AddParameter("@searchvalue", "%" + searchValue + "%");
                }
                select.Add("order by orderby");
                select.AddParameter("@contractid", contractid);
                dynamic result = new ExpandoObject();
                result = qry.QueryToFwJsonTable(select, false);
                return result;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public static void GetStagedItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetStagedItems";
            session.userLocation = RwAppData.GetUserLocation(conn: FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "Your user account requires a warehouse to peform this action.", session.userLocation.warehouseId);

            string searchMode = string.Empty;
            string searchValue = string.Empty;
            int pageNo = 0;
            int pageSize = 0;
            if (FwValidate.IsPropertyDefined(request, "pageno"))
            {
                pageNo = request.pageno;
            }
            if (FwValidate.IsPropertyDefined(request, "pagesize"))
            {
                pageSize = request.pagesize;
            }

            if (string.IsNullOrEmpty(request.contractid))
            {
                response.searchresults = Staging.funcstaged(FwSqlConnection.RentalWorks, request.orderid, session.userLocation.warehouseId, false, searchMode, searchValue, pageNo, pageSize);
            }
            else
            {
                response.searchresults = Staging.funccheckedout(FwSqlConnection.RentalWorks, request.contractid, searchMode, searchValue, pageNo, pageSize);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public static void OrdertranExists(dynamic request, dynamic response, dynamic session)
        {
            if (!(string.IsNullOrEmpty(request.contractid)))
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("select ordertranexists = case when exists (select *");
                    qry.Add("                                           from dbo.funccheckedout(@contractid))");
                    qry.Add("                              then 'T'");
                    qry.Add("                              else 'F'");
                    qry.Add("                         end");
                    qry.AddParameter("@contractid", request.contractid);
                    qry.Execute();
                    response.ordertranExists = qry.GetField("ordertranexists").ToBoolean();
                }
            }
            else
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("select ordertranexists = case when exists (select *");
                    qry.Add("                                           from dbo.funcstaged(@orderid, 'F'))");
                    qry.Add("                              then 'T'");
                    qry.Add("                              else 'F'");
                    qry.Add("                         end");
                    qry.AddParameter("@orderid", request.orderid);
                    qry.Execute();
                    response.ordertranExists = qry.GetField("ordertranexists").ToBoolean();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid")]
        public static void CancelContract(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.CancelContract(FwSqlConnection.RentalWorks, request.contractid, session.security.webUser.usersid, true);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,masterid,masteritemid")]
        public static void LoadSerialNumbers(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from funcserialmeterout(@orderid, @masterid, @warehouseid)");
                qry.Add("where masteritemid = @masteritemid");
                qry.Add("  and itemstatus <> 'O'");
                qry.Add("order by mfgserial");
                qry.AddParameter("@orderid", request.orderid);
                qry.AddParameter("@masterid", request.masterid);
                qry.AddParameter("@warehouseid", userLocation.warehouseId);
                qry.AddParameter("@masteritemid", request.masteritemid);
                response.funcserialmeterout = qry.QueryToDynamicList2();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,masterid,masteritemid")]
        public static void funcserialmeterout(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from funcserialmeterout(@orderid, @masterid, @warehouseid)");
                qry.Add("where masteritemid = @masteritemid");
                if (FwValidate.IsPropertyDefined(request, "onlystagedorout") && request.onlystagedorout == true)
                {
                    qry.Add("  and itemstatus in ('S', 'O')");
                }
                qry.Add("order by mfgserial");
                qry.AddParameter("@orderid", request.orderid);
                qry.AddParameter("@masterid", request.masterid);
                qry.AddParameter("@warehouseid", userLocation.warehouseId);
                qry.AddParameter("@masteritemid", request.masteritemid);
                response.funcserialmeterout = qry.QueryToDynamicList2();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid,orderid,masteritemid,rentalitemid,internalchar,meter,toggledelete,containeritemid,containeroutcontractid")]
        public static void InsertSerialSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "insertserialsession"))
            {
                sp.AddParameter("@contractid",             request.contractid);
                sp.AddParameter("@orderid",                request.orderid);
                sp.AddParameter("@masteritemid",           request.masteritemid);
                sp.AddParameter("@rentalitemid",           request.rentalitemid);
                sp.AddParameter("@activitytype",           "O");
                sp.AddParameter("@internalchar",           request.internalchar);
                sp.AddParameter("@usersid",                session.security.webUser.usersid);
                sp.AddParameter("@meter",                  FwConvert.ToDecimal(request.meter));
                sp.AddParameter("@toggledelete",           FwConvert.LogicalToCharacter(request.toggledelete));
                sp.AddParameter("@containeritemid",        request.containeritemid);
                sp.AddParameter("@containeroutcontractid", request.containeroutcontractid);
                sp.AddParameter("@status",                 SqlDbType.Int,     ParameterDirection.Output);
                sp.AddParameter("@msg",                    SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();

                response.status        = sp.GetParameter("@status").ToInt32();
                response.statusmessage = sp.GetParameter("@msg").ToString();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid,masteritemid")]
        public static void funcserialfrm(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 *");
                qry.Add("from  dbo.funcserialfrm (@orderid, @warehouseid, @contractid) si");
                qry.Add("where masteritemid = @masteritemid");
                qry.AddParameter("@orderid", request.orderid);
                qry.AddParameter("@warehouseid", userLocation.warehouseId);
                qry.AddParameter("@contractid", request.contractid);
                qry.AddParameter("@masteritemid", request.masteritemid);
                response.funcserialfrm = qry.QueryToDynamicObject2();
             }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void RFIDScan(dynamic request, dynamic response, dynamic session)
        {
            string batchid;
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);

            batchid = RwAppData.LogRFIDTags(conn:      FwSqlConnection.RentalWorks,
                                            portal:    request.portal,
                                            sessionid: request.sessionid,
                                            tags:      request.tags,
                                            usersid:   session.security.webUser.usersid,
                                            rfidmode:  request.rfidmode);

            RwAppData.ProcessScannedTags(conn:      FwSqlConnection.RentalWorks,
                                         portal:    request.portal,
                                         sessionid: request.sessionid,
                                         batchid:   batchid,
                                         usersid:   session.security.webUser.usersid,
                                         rfidmode:  request.rfidmode);

            response.processed = GetTags(sessionid: request.sessionid,
                                         usersid:   session.security.webUser.usersid,
                                         portal:    request.portal,
                                         batchid:   batchid);

            response.exceptions = RwAppData.GetRFIDExceptions(conn:      FwSqlConnection.RentalWorks,
                                                                sessionid: request.sessionid,
                                                                portal:    request.portal,
                                                                usersid:   session.security.webUser.usersid);

            response.pending = RwAppData.CheckoutExceptionRFID(conn:        FwSqlConnection.RentalWorks,
                                                                orderid:     request.sessionid,
                                                                warehouseid: userLocation.warehouseId);
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetTags(string sessionid, string usersid, string portal, string batchid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("from   dbo.funcscannedtag(@sessionid, @orderid, @usersid, @portal, @batchid, @rfidmode)");
            if (string.IsNullOrEmpty(batchid))
            {
                qry.Add(" where status = 'EXCEPTION'");
            }
            else //MY 2017/08/02: Excludes exceptions because staging loads them seperately. Change when RFID is improved in staging.
            {
                qry.Add("where status <> 'EXCEPTION'");
            }
            qry.AddParameter("@sessionid", sessionid);
            qry.AddParameter("@orderid",   "");
            qry.AddParameter("@usersid",   usersid);
            qry.AddParameter("@portal",    portal);
            qry.AddParameter("@batchid",   batchid);
            qry.AddParameter("@rfidmode",  "STAGING");
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CreateContract(dynamic request, dynamic response, dynamic session)   //Only called directly from staging when moduletype == transfer
        {
            const string METHOD_NAME = "CreateContract";
            string usersid, contracttype, contractid, orderid, responsiblepersonid;
            string printname = string.Empty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "responsiblePersonId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "signatureImage");
            usersid             = session.security.webUser.usersid;
            contracttype        = request.contractType;
            contractid          = request.contractId;
            orderid             = request.orderId;
            responsiblepersonid = request.responsiblePersonId;
            if (FwValidate.IsPropertyDefined(request, "printname"))
            {
                printname = request.printname;
            }

            // Create the contract
            response.createcontract = WebCreateContract(usersid, contracttype, contractid, orderid, responsiblepersonid, printname);

            // insert the signature image
            //FwSqlData.InsertAppImage(FwSqlConnection.RentalWorks, contractid, string.Empty, string.Empty, "CONTRACT_SIGNATURE", string.Empty, "JPG", request.signatureImage);

            //if ((FwValidate.IsPropertyDefined(request, "images")) && (request.images.Length > 0))
            //{
            //    byte[] image;
            //    for (int i = 0; i < request.images.Length; i++)
            //    {
            //        image = Convert.FromBase64String(request.images[i]);
            //        FwSqlData.InsertAppImage(FwSqlConnection.RentalWorks, contractid, string.Empty, string.Empty, "CONTRACT_IMAGE", string.Empty, "JPG", image);
            //    }
            //}

            //if (contracttype == RwAppData.CONTRACT_TYPE_OUT)
            //{
            //    FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //    qry.Add("select orderno, orderdesc");
            //    qry.Add("  from dealorder with (nolock)");
            //    qry.Add(" where orderid = @orderid");
            //    qry.AddParameter("@orderid", orderid);
            //    qry.Execute();

            //    response.subject = qry.GetField("@orderno").ToString().TrimEnd() + " - " + qry.GetField("@orderdesc").ToString().TrimEnd() + " - Out Contract";
            //}
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic WebCreateContract(string usersid, string contracttype, string contractid, string orderId, string responsiblePersonId, string printname)
        {
            dynamic result;
            FwSqlCommand sp, qryUpdateDealOrderDetail, qryUpdateContract;

            FwSqlCommand qryInputByUser;
            string inputbyusersid, namefml;
            if (!string.IsNullOrEmpty(contractid)) {
                qryInputByUser = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qryInputByUser.Add("select c.inputbyusersid, u.namefml");
                qryInputByUser.Add("from contract c join usersview u on (c.inputbyusersid = u.usersid)");
                qryInputByUser.Add("where contractid = @contractid");
                qryInputByUser.AddParameter("@contractid", contractid);
                qryInputByUser.Execute();
                inputbyusersid = qryInputByUser.GetField("inputbyusersid").ToString().TrimEnd();
                namefml = qryInputByUser.GetField("namefml").ToString().TrimEnd();
                if (usersid != inputbyusersid) {
                    throw new Exception("Only the session owner " + namefml + " can create a contract.");
                }
            }

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.webcreatecontract");
            sp.AddParameter("@contracttype",    contracttype);
            sp.AddParameter("@contractid",      SqlDbType.NVarChar, ParameterDirection.InputOutput, contractid);
            sp.AddParameter("@orderid",         orderId);
            sp.AddParameter("@usersid",         usersid);
            sp.AddParameter("@personprintname", printname);
            sp.AddParameter("@status",          SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",             SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            result            = new ExpandoObject();
            result.contractId = sp.GetParameter("@contractid").ToString().TrimEnd();
            result.status     = sp.GetParameter("@status").ToInt32();
            result.msg        = sp.GetParameter("@msg").ToString().TrimEnd();

            if ((contracttype == RwAppData.CONTRACT_TYPE_OUT) && (!string.IsNullOrEmpty(responsiblePersonId)))
            {
                qryUpdateDealOrderDetail = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qryUpdateDealOrderDetail.Add("update dealorderdetail");
                qryUpdateDealOrderDetail.Add("set responsiblepersonid = @responsiblepersonid");
                qryUpdateDealOrderDetail.Add("where orderid           = @orderid");
                qryUpdateDealOrderDetail.AddParameter("@responsiblepersonid", responsiblePersonId);
                qryUpdateDealOrderDetail.AddParameter("@orderid", orderId);
                qryUpdateDealOrderDetail.ExecuteNonQuery();

                qryUpdateContract = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qryUpdateContract.Add("update contract");
                qryUpdateContract.Add("   set responsiblepersonid = @responsiblepersonid,");
                qryUpdateContract.Add(" where contractId          = @contractId");
                qryUpdateContract.AddParameter("@responsiblepersonid", responsiblePersonId);
                qryUpdateContract.AddParameter("@contractId",          result.contractId);
                qryUpdateContract.ExecuteNonQuery();
            }

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
