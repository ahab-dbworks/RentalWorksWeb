using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using Newtonsoft.Json.Linq;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class Staging : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public Staging(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadModuleProperties(dynamic request, dynamic response, dynamic session)
        {
            response.syscontrol = await LoadSysControlValuesAsync();
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetSearchResults(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string searchmode = request.searchmode;
                var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                string locationid = userLocation.locationId;
                string moduletype = request.moduletype;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;

                    switch (moduletype)
                    {
                        case RwConstants.ModuleTypes.Order:
                            switch (searchmode)
                            {
                                case "orderno":
                                    select.PageNo = request.pageno;
                                    select.PageSize = request.pagesize;
                                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                                    qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                                    qry.AddColumn("estrentto", false, FwDataTypes.Date);
                                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                                    qry.AddColumn("pickdate", false, FwDataTypes.Date);
                                    select.Add("select *");
                                    select.Add("from dbo.funcorderstaging(@warehouseid)");
                                    select.Add("where orderno = orderno");
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.Parse();
                                    select.AddOrderBy("orderdate desc, orderno desc");
                                    select.AddWhere("orderno like @orderno");
                                    select.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                                    select.AddParameter("@orderno", request.searchvalue + "%");
                                    break;
                                case "orderdesc":
                                    select.PageNo = request.pageno;
                                    select.PageSize = request.pagesize;
                                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                                    qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                                    qry.AddColumn("estrentto", false, FwDataTypes.Date);
                                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                                    qry.AddColumn("pickdate", false, FwDataTypes.Date);
                                    select.Add("select *");
                                    select.Add("from dbo.funcorderstaging(@warehouseid)");
                                    select.Add("where orderno = orderno");
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.Parse();
                                    //select.AddWhere("orderdesc like @searchvalue");
                                    //select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                                    {
                                        string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < searchValues.Length; i++)
                                        {
                                            select.AddWhere($"orderdesc like @orderdesc{i}");
                                            select.AddParameter($"@orderdesc{i}", $"%{searchValues[i]}%");
                                        }
                                    }
                                    select.AddOrderBy("orderdate desc, orderno desc");
                                    select.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                                    break;
                                case "deal":
                                    select.PageNo = request.pageno;
                                    select.PageSize = request.pagesize;
                                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                                    qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                                    qry.AddColumn("estrentto", false, FwDataTypes.Date);
                                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                                    qry.AddColumn("pickdate", false, FwDataTypes.Date);
                                    select.Add("select *");
                                    select.Add("from dbo.funcorderstaging(@warehouseid) o");
                                    select.Add("where orderno = orderno");
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.Parse();
                                    //select.AddWhere("deal like @searchvalue");
                                    {
                                        string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < searchValues.Length; i++)
                                        {
                                            select.AddWhere($"deal like @deal{i}");
                                            select.AddParameter($"@deal{i}", $"%{searchValues[i]}%");
                                        }
                                    }
                                    select.AddOrderBy("orderdate desc, orderno desc");
                                    select.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                                    select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                                    break;
                                case "sessionno":
                                    select.PageNo = request.pageno;
                                    select.PageSize = request.pagesize;
                                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                                    select.Add("select orderid, orderdesc, orderno, status, statusdate, sessionno, dealno, deal, usersid, username, contractid");
                                    select.Add("from suspendview v with (nolock)");
                                    select.Add("where v.contracttype = 'OUT'");
                                    select.Add("  and v.ordertype    = 'O'");
                                    select.Add("  and v.locationid   = @locationid");
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.AddParameter("@locationid", userLocation.locationId);
                                    select.Parse();
                                    select.AddWhere("sessionno like @sessionno");
                                    select.AddOrderBy("statusdate desc, sessionno desc");
                                    select.AddParameter("@sessionno", request.searchvalue + "%");
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
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
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
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.Parse();
                                    //select.AddWhere("orderdesc like @searchvalue");
                                    //select.AddParameter("@searchvalue", request.searchvalue + "%");
                                    {
                                        string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < searchValues.Length; i++)
                                        {
                                            select.AddWhere($"orderdesc like @searchvalue{i}");
                                            select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                                        }
                                    }
                                    select.AddOrderBy("orderno");
                                    select.AddParameter("@locationid", userLocation.locationId);
                                    break;
                                case "sessionno":
                                    select.PageNo = request.pageno;
                                    select.PageSize = request.pagesize;
                                    qry.AddColumn("contractdate", false, FwDataTypes.Date);
                                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                                    select.Add("select contractdate,contracttime, orderid, orderdesc, orderno, status, statusdate, sessionno, dealno, deal, usersid, username, contractid");
                                    select.Add("from suspendview v with (nolock)");
                                    select.Add("where v.contracttype = 'OUT'");
                                    select.Add("  and v.locationid   = @locationid");
                                    select.Add("  and v.dealid       = ''");
                                    select.Add("  and v.ordertype    = 'P'");
                                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
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
                                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                                    qry.AddColumn("pickdate", false, FwDataTypes.Date);
                                    qry.AddColumn("shipdate", false, FwDataTypes.Date);
                                    qry.AddColumn("receivedate", false, FwDataTypes.Date);
                                    select.Add("select orderid, orderdesc, status, orderno, orderdate, fromwarehouse, towarehouse, department, origorderno, pickdate, shipdate, receivedate");
                                    select.Add("from transferview tv with (nolock)");
                                    select.Add("where status in ('ACTIVE','CONFIRMED')");
                                    select.Add("  and fromwarehouseid = @warehouseid");
                                    DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
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
                                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                                    qry.AddColumn("pickdate", false, FwDataTypes.Date);
                                    qry.AddColumn("shipdate", false, FwDataTypes.Date);
                                    qry.AddColumn("receivedate", false, FwDataTypes.Date);
                                    select.Add("select orderid, orderdesc, status, orderno, orderdate, fromwarehouse, towarehouse, department, origorderno, pickdate, shipdate, receivedate");
                                    select.Add("from transferview tv with (nolock)");
                                    select.Add("where status in ('ACTIVE','CONFIRMED')");
                                    select.Add("  and fromwarehouseid = @warehouseid");
                                    DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.Parse();
                                    //select.AddWhere("orderdesc like @searchvalue");
                                    //select.AddParameter("@searchvalue", request.searchvalue + "%");
                                    {
                                        string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < searchValues.Length; i++)
                                        {
                                            select.AddWhere($"orderdesc like @searchvalue{i}");
                                            select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                                        }
                                    }
                                    select.AddOrderBy("orderno desc");
                                    select.AddParameter("@locationid", userLocation.locationId);
                                    select.AddParameter("@warehouseid", userLocation.warehouseId);
                                    break;
                                case "sessionno":
                                    select.Add("select contractdate, contracttime, orderid, orderdesc, orderno, status, statusdate, sessionno, usersid, username, contractid, warehouse");
                                    select.Add("from  suspendview v with (nolock)");
                                    select.Add("where v.contracttype = 'MANIFEST'");
                                    select.Add("  and   v.locationid   = @locationid");
                                    select.Add("  and   v.dealid       = ''");
                                    select.Add("  and  v.ordertype = 'T'");
                                    DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, select);
                                    select.Parse();
                                    select.AddWhere("sessionno like @searchvalue");
                                    select.AddOrderBy("contractdate desc, contracttime desc");
                                    select.AddParameter("@locationid", userLocation.locationId);
                                    select.AddParameter("@searchvalue", request.searchvalue + "%");

                                    break;
                            }
                            break;
                    }
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                    //response.searchresults = JToken.FromObject(await qry.QueryToFwJsonTableAsync(select, true));
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "activitytype,orderid")]
        public async Task GetResponsiblePerson(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.getresponsibleperson", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@orderid", request.orderid);
                    sp.AddParameter("@showresponsibleperson", SqlDbType.Char, ParameterDirection.Output);
                    sp.AddParameter("@responsibleperson", SqlDbType.VarChar, ParameterDirection.Output);
                    sp.AddParameter("@responsiblepersonid", SqlDbType.Char, ParameterDirection.Output);
                    await sp.ExecuteAsync();
                    response.responsibleperson = new ExpandoObject();
                    response.responsibleperson.showresponsibleperson = sp.GetParameter("@showresponsibleperson").ToString().Trim();
                    response.responsibleperson.responsibleperson = sp.GetParameter("@responsibleperson").ToString().Trim();
                    response.responsibleperson.responsiblepersonid = sp.GetParameter("@responsiblepersonid").ToString().Trim();
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("person", false);
                    qry.AddColumn("contactid", false);
                    qry.Add("select text=person, value=contactid");
                    qry.Add("from inventorycontactview");
                    qry.Add("where responsibleperson = 'T'");
                    qry.Add("order by person");
                    response.responsibleperson.responsiblepersons = await qry.QueryToDynamicList2Async();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<bool> IsSuspendedSessionsEnabledAsync()
        {
            bool isenabled = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select isenabled = dbo.quikscanstagebysession()");
                    await qry.ExecuteAsync();
                    isenabled = qry.GetField("isenabled").ToBoolean();
                } 
            }
            return isenabled;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid")]
        public async Task GetOrderSuspendedSessions(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string usersid = session.security.webUser.usersid;
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, usersid);
                string locationid = userLocation.locationId;
                string orderid = request.orderid;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
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
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "")]
        public async Task CreateSuspendedSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.createoutcontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@orderid", request.orderid);
                    sp.AddParameter("@usersid", session.security.webUser.usersid);
                    sp.AddParameter("@notes", string.Empty);
                    sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await sp.ExecuteAsync();
                    response.contractid = sp.GetParameter("@contractid").ToString().Trim();
                }
                response.sessionno = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "contract", "contractid", response.contractid, "sessionno");
                if (!string.IsNullOrEmpty(response.contractid))
                {
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Add("update contract");
                        cmd.Add("set forcedsuspend = 'G'");
                        cmd.Add("where contractid = @contractid");
                        cmd.AddParameter("@contractid", response.contractid);
                        await cmd.ExecuteAsync();
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public async Task GetPendingItems(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "GetPendingItems";
                FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
                session.userLocation = await this.AppData.GetUserLocationAsync(conn: conn
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
                response.searchresults = await this.AppData.GetStagingPendingItemsAsync(conn: conn,
                                                                          orderId: request.orderid,
                                                                          warehouseId: session.userLocation.warehouseId,
                                                                          contractId: request.contractid,
                                                                          searchMode: searchMode,
                                                                          searchValue: searchValue,
                                                                          pageNo: pageNo,
                                                                          pageSize: pageSize); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> funcstagedAsync(FwSqlConnection conn, string orderid, string warehouseid, bool summary, string searchMode, string searchValue, int pageNo, int pageSize)
        {
            using (var qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddColumn("rectype", false);
                qry.AddColumn("masteritemid", false);
                qry.AddColumn("description", false);
                qry.AddColumn("masterid", false);
                qry.AddColumn("masterno", false);
                qry.AddColumn("barcode", false);
                qry.AddColumn("quantity", false, FwDataTypes.Decimal);
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
                select.Parse();
                if (searchMode == "description" && searchValue != null && searchValue.Length > 0)
                {
                    string[] searchValues = searchValue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < searchValues.Length; i++)
                    {
                        select.AddWhere($"description like @searchvalue{i}");
                        select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                    }
                }
                select.AddOrderBy("orderby");
                select.AddParameter("@orderid", orderid);
                select.AddParameter("@summary", FwConvert.LogicalToCharacter(summary));
                select.AddParameter("@warehouseid", warehouseid);
                dynamic result = new ExpandoObject();
                result = await qry.QueryToFwJsonTableAsync(select, false);
                return result;
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> funccheckedoutAsync(FwSqlConnection conn, string contractid, string searchMode, string searchValue, int pageNo, int pageSize)
        {
            using (var qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddColumn("rectype", false);
                qry.AddColumn("masteritemid", false);
                qry.AddColumn("description", false);
                qry.AddColumn("masterid", false);
                qry.AddColumn("masterno", false);
                qry.AddColumn("barcode", false);
                qry.AddColumn("quantity", false, FwDataTypes.Decimal);
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
                select.Parse();
                if (searchMode == "description" && searchValue != null && searchValue.Length > 0)
                {
                    string[] searchValues = searchValue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < searchValues.Length; i++)
                    {
                        select.AddWhere($"description like @searchvalue{i}");
                        select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                    }
                }
                select.AddOrderBy("orderby");
                select.AddParameter("@contractid", contractid);
                dynamic result = new ExpandoObject();
                result = await qry.QueryToFwJsonTableAsync(select, false);
                return result;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public async Task GetStagedItems(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "GetStagedItems";
                session.userLocation = await this.AppData.GetUserLocationAsync(conn: conn
                                                               , usersId: session.security.webUser.usersid);
                FwValidate.TestIsNullOrEmpty(METHOD_NAME, "Your user account requires a warehouse to peform this action.", session.userLocation.warehouseId);

                string searchMode = string.Empty;
                string searchValue = string.Empty;
                int pageNo = 0;
                int pageSize = 0;
                if (FwValidate.IsPropertyDefined(request, "searchmode"))
                {
                    searchMode = request.searchmode;
                }
                if (FwValidate.IsPropertyDefined(request, "searchvalue"))
                {
                    searchValue = request.searchvalue;
                }
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
                    response.searchresults = await this.funcstagedAsync(conn, request.orderid, session.userLocation.warehouseId, false, searchMode, searchValue, pageNo, pageSize);
                }
                else
                {
                    response.searchresults = await this.funccheckedoutAsync(conn, request.contractid, searchMode, searchValue, pageNo, pageSize);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public async Task OrdertranExists(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if (!(string.IsNullOrEmpty(request.contractid)))
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select ordertranexists = case when exists (select *");
                        qry.Add("                                           from dbo.funccheckedout(@contractid))");
                        qry.Add("                              then 'T'");
                        qry.Add("                              else 'F'");
                        qry.Add("                         end");
                        qry.AddParameter("@contractid", request.contractid);
                        await qry.ExecuteAsync();
                        response.ordertranExists = qry.GetField("ordertranexists").ToBoolean();
                    }
                }
                else
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select ordertranexists = case when exists (select *");
                        qry.Add("                                           from dbo.funcstaged(@orderid, 'F'))");
                        qry.Add("                              then 'T'");
                        qry.Add("                              else 'F'");
                        qry.Add("                         end");
                        qry.AddParameter("@orderid", request.orderid);
                        await qry.ExecuteAsync();
                        response.ordertranExists = qry.GetField("ordertranexists").ToBoolean();
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid")]
        public async Task CancelContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await this.AppData.CancelContractAsync(conn, request.contractid, session.security.webUser.usersid, true); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,masterid,masteritemid")]
        public async Task LoadSerialNumbers(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
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
                    response.funcserialmeterout = await qry.QueryToDynamicList2Async();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,masterid,masteritemid")]
        public async Task funcserialmeterout(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
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
                    response.funcserialmeterout = await qry.QueryToDynamicList2Async();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid,orderid,masteritemid,rentalitemid,internalchar,meter,toggledelete,containeritemid,containeroutcontractid")]
        public async Task InsertSerialSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "insertserialsession", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@contractid", request.contractid);
                    sp.AddParameter("@orderid", request.orderid);
                    sp.AddParameter("@masteritemid", request.masteritemid);
                    sp.AddParameter("@rentalitemid", request.rentalitemid);
                    sp.AddParameter("@activitytype", "O");
                    sp.AddParameter("@internalchar", request.internalchar);
                    sp.AddParameter("@usersid", session.security.webUser.usersid);
                    sp.AddParameter("@meter", FwConvert.ToDecimal(request.meter));
                    sp.AddParameter("@toggledelete", FwConvert.LogicalToCharacter(request.toggledelete));
                    sp.AddParameter("@containeritemid", request.containeritemid);
                    sp.AddParameter("@containeroutcontractid", request.containeroutcontractid);
                    sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                    await sp.ExecuteAsync();

                    response.status = sp.GetParameter("@status").ToInt32();
                    response.statusmessage = sp.GetParameter("@msg").ToString();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid,masteritemid")]
        public async Task funcserialfrm(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 *");
                    qry.Add("from  dbo.funcserialfrm (@orderid, @warehouseid, @contractid) si");
                    qry.Add("where masteritemid = @masteritemid");
                    qry.AddParameter("@orderid", request.orderid);
                    qry.AddParameter("@warehouseid", userLocation.warehouseId);
                    qry.AddParameter("@contractid", request.contractid);
                    qry.AddParameter("@masteritemid", request.masteritemid);
                    response.funcserialfrm = qry.QueryToDynamicObject2Async();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RFIDScan(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string batchid;
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);

                batchid = await this.AppData.LogRFIDTagsAsync(conn: conn,
                                                portal: request.portal,
                                                sessionid: request.sessionid,
                                                tags: request.tags,
                                                usersid: session.security.webUser.usersid,
                                                rfidmode: request.rfidmode);

                await this.AppData.ProcessScannedTagsAsync(conn: conn,
                                             portal: request.portal,
                                             sessionid: request.sessionid,
                                             batchid: batchid,
                                             usersid: session.security.webUser.usersid,
                                             rfidmode: request.rfidmode);

                response.processed = await GetTagsAsync(sessionid: request.sessionid,
                                             usersid: session.security.webUser.usersid,
                                             portal: request.portal,
                                             batchid: batchid);

                response.exceptions = await this.AppData.GetRFIDExceptionsAsync(conn: conn,
                                                                    sessionid: request.sessionid,
                                                                    portal: request.portal,
                                                                    usersid: session.security.webUser.usersid);

                response.pending = await this.AppData.CheckoutExceptionRFIDAsync(conn: conn,
                                                                    orderid: request.sessionid,
                                                                    warehouseid: userLocation.warehouseId); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetTagsAsync(string sessionid, string usersid, string portal, string batchid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
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
                qry.AddParameter("@orderid", "");
                qry.AddParameter("@usersid", usersid);
                qry.AddParameter("@portal", portal);
                qry.AddParameter("@batchid", batchid);
                qry.AddParameter("@rfidmode", "STAGING");
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CreateContract(dynamic request, dynamic response, dynamic session)   //Only called directly from staging when moduletype == transfer
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
            response.createcontract = await WebCreateContractAsync(usersid, contracttype, contractid, orderid, responsiblepersonid, printname);

            // insert the signature image
            //FwSqlData.InsertAppImage(conn, contractid, string.Empty, string.Empty, "CONTRACT_SIGNATURE", string.Empty, "JPG", request.signatureImage);

            //if ((FwValidate.IsPropertyDefined(request, "images")) && (request.images.Length > 0))
            //{
            //    byte[] image;
            //    for (int i = 0; i < request.images.Length; i++)
            //    {
            //        image = Convert.FromBase64String(request.images[i]);
            //        FwSqlData.InsertAppImage(conn, contractid, string.Empty, string.Empty, "CONTRACT_IMAGE", string.Empty, "JPG", image);
            //    }
            //}

            //if (contracttype == RwAppData.CONTRACT_TYPE_OUT)
            //{
            //    FwSqlCommand qry = new FwSqlCommand(conn);
            //    qry.Add("select orderno, orderdesc");
            //    qry.Add("  from dealorder with (nolock)");
            //    qry.Add(" where orderid = @orderid");
            //    qry.AddParameter("@orderid", orderid);
            //    qry.Execute();

            //    response.subject = qry.GetField("@orderno").ToString().TrimEnd() + " - " + qry.GetField("@orderdesc").ToString().TrimEnd() + " - Out Contract";
            //}
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebCreateContractAsync(string usersid, string contracttype, string contractid, string orderId, string responsiblePersonId, string printname)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand sp, qryUpdateDealOrderDetail, qryUpdateContract;

                FwSqlCommand qryInputByUser;
                string inputbyusersid, namefml;
                if (!string.IsNullOrEmpty(contractid))
                {
                    qryInputByUser = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryInputByUser.Add("select c.inputbyusersid, u.namefml");
                    qryInputByUser.Add("from contract c join usersview u on (c.inputbyusersid = u.usersid)");
                    qryInputByUser.Add("where contractid = @contractid");
                    qryInputByUser.AddParameter("@contractid", contractid);
                    await qryInputByUser.ExecuteAsync();
                    inputbyusersid = qryInputByUser.GetField("inputbyusersid").ToString().TrimEnd();
                    namefml = qryInputByUser.GetField("namefml").ToString().TrimEnd();
                    if (usersid != inputbyusersid)
                    {
                        throw new Exception("Only the session owner " + namefml + " can create a contract.");
                    }
                }

                sp = new FwSqlCommand(conn, "dbo.webcreatecontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@contracttype", contracttype);
                sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.InputOutput, contractid);
                sp.AddParameter("@orderid", orderId);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@personprintname", printname);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                result = new ExpandoObject();
                result.contractId = sp.GetParameter("@contractid").ToString().TrimEnd();
                result.status = sp.GetParameter("@status").ToInt32();
                result.msg = sp.GetParameter("@msg").ToString().TrimEnd();

                if ((contracttype == RwAppData.CONTRACT_TYPE_OUT) && (!string.IsNullOrEmpty(responsiblePersonId)))
                {
                    qryUpdateDealOrderDetail = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryUpdateDealOrderDetail.Add("update dealorderdetail");
                    qryUpdateDealOrderDetail.Add("set responsiblepersonid = @responsiblepersonid");
                    qryUpdateDealOrderDetail.Add("where orderid           = @orderid");
                    qryUpdateDealOrderDetail.AddParameter("@responsiblepersonid", responsiblePersonId);
                    qryUpdateDealOrderDetail.AddParameter("@orderid", orderId);
                    await qryUpdateDealOrderDetail.ExecuteNonQueryAsync();

                    qryUpdateContract = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryUpdateContract.Add("update contract");
                    qryUpdateContract.Add("   set responsiblepersonid = @responsiblepersonid,");
                    qryUpdateContract.Add(" where contractId          = @contractId");
                    qryUpdateContract.AddParameter("@responsiblepersonid", responsiblePersonId);
                    qryUpdateContract.AddParameter("@contractId", result.contractId);
                    await qryUpdateContract.ExecuteNonQueryAsync();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SearchLocations(dynamic request, dynamic response, dynamic session)
        {
            response.locations = await SearchOrderLocationsAsync(orderid:     request.orderid,
                                                      searchvalue: request.searchvalue);
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> SearchOrderLocationsAsync(string orderid, string searchvalue)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result = new ExpandoObject();
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                string includefacilitiestype = "F";
                dynamic applicationoptions = await FwSqlData.GetApplicationOptionsAsync(this.ApplicationConfig.DatabaseSettings);
                string[] searchvalues = searchvalue.Split(' ');

                if (applicationoptions.facilities.enabled)
                {
                    dynamic controlresult = await LoadSysControlValuesAsync();

                    if ((controlresult.itemsinrooms) && (controlresult.facilitytypeincurrentlocation))
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

                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@includefacilitiestype", includefacilitiestype);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> LoadSysControlValuesAsync()
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("itemsinrooms", false, FwDataTypes.Boolean);
                qry.AddColumn("facilitytypeincurrentlocation", false, FwDataTypes.Boolean);
                qry.Add("select top 1 itemsinrooms, facilitytypeincurrentlocation");
                qry.Add("  from syscontrol with (nolock)");
                qry.Add(" where controlid = '1'");
                dynamic result = await qry.QueryToDynamicObject2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task StartSubstituteSession(dynamic request, dynamic response, dynamic session)
        {
            response.sessionid = await CreateNewSubstituteSessionAsync(orderid:     request.OrderId,
                                                            orderitemid: request.OrderItemId);
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> CreateNewSubstituteSessionAsync(string orderid, string orderitemid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string sessionid = await FwSqlData.GetNextIdAsync(conn, this.ApplicationConfig.DatabaseSettings);

                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("insert into stagingsubstitutesession(sessionid,  orderid,  masteritemid, datestamp)");
                qry.Add("                             values (@SessionId, @OrderId, @OrderItemId, getutcdate())");
                qry.AddParameter("@SessionId", sessionid);
                qry.AddParameter("@OrderId", orderid);
                qry.AddParameter("@OrderItemId", orderitemid);
                await qry.ExecuteAsync();

                return sessionid;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task StageSubstituteItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand sp = new FwSqlCommand(conn, "dbo.stageaddsubstituteitemtosession", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@sessionid", request.SessionId);
                sp.AddParameter("@code", request.Code);
                sp.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                sp.AddParameter("@qty", request.Qty);
                sp.AddParameter("@usersid", session.security.webUser.usersid);
                sp.AddParameter("@masterno", SqlDbType.VarChar, ParameterDirection.Output);
                sp.AddParameter("@description", SqlDbType.VarChar, ParameterDirection.Output);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                dynamic result = new ExpandoObject();
                result.masterno = sp.GetParameter("@masterno").ToString().TrimEnd();
                result.description = sp.GetParameter("@description").ToString().TrimEnd();
                result.status = sp.GetParameter("@status").ToInt32();
                result.msg = sp.GetParameter("@msg").ToString().TrimEnd();

                response.stagesubstituteitem = result;

                if (result.status == 0)
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select id, sessionid, masterid, rentalitemid, masterno, master, barcode, mfgserial, qty, rentalstatusid, rentalstatus, rentalstatuscolor");
                    qry.Add("  from stagingsubstitutesessionview with (nolock)");
                    qry.Add(" where sessionid = @sessionid");
                    qry.AddParameter("@sessionid", request.SessionId);
                    response.items = await qry.QueryToDynamicList2Async();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
