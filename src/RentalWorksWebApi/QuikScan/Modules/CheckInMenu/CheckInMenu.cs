using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class CheckInMenu : MobileModule
    {
        RwAppData AppData;
        private Regex isNumeric = new Regex("^[0-9]*$");
        //----------------------------------------------------------------------------------------------------
        public CheckInMenu(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task DealSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "DealSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select *");
                select.Add("from   dbo.funcdeal(@locationid)");
                select.Add("where  dealinactive <> 'T'");
                if (!request.showalllocation)
                {
                    select.Add("  and locationid = @locationid"); //MY + EG 2017-08-25: QS needs to support location filter on the user level.
                }
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    switch ((string)request.searchmode)
                    {
                        case "DEALNO":
                            select.Add("and dealno like @dealno");
                            select.AddParameter("@dealno", request.searchvalue + "%");
                            break;
                        case "NAME":
                            select.Add("and deal like @deal");
                            select.AddParameter("@deal", "%" + request.searchvalue + "%");
                            break;
                    }
                }
                select.Add("order by deal");
                select.AddParameter("@locationid", userLocation.locationId);

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SessionSearch(dynamic request, dynamic response, dynamic session)
        {
            if (FwValidate.IsPropertyDefined(request, "searchvalue"))
            {
                string searchvalue = request.searchvalue.ToString();
                if (searchvalue.Length > 0)
                {
                    if (!isNumeric.IsMatch(searchvalue))
                    {
                        throw new FwBadRequestException("Session No must be an integer");
                    }
                }
            }
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;

                    select.Add("select *");
                    select.Add("from suspendview with (nolock)");
                    select.Add("where contracttype = 'IN'");
                    select.Add("  and ordertype    = 'O'");
                    if (!request.showalllocation)
                    {
                        select.Add("  and locationid   = @locationid");
                    }
                    if (!string.IsNullOrEmpty(request.searchvalue))
                    {
                        select.Add("  and sessionno = @sessionno");
                        select.AddParameter("@sessionno", request.searchvalue);
                    }
                    if (!string.IsNullOrEmpty(request.orderid))
                    {
                        select.Add("  and orderid = @orderid");
                        select.AddParameter("@orderid", request.orderid);
                    }
                    select.Parse();
                    select.AddOrderBy("statusdate desc");
                    select.AddParameter("@locationid", userLocation.locationId);
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task OrderSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "OrderSearch";
                dynamic userLocation;
                FwSqlCommand qry;
                FwSqlSelect select = new FwSqlSelect();

                FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                qry.AddColumn("orderdate", false, FwDataTypes.Date);
                qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                qry.AddColumn("estrentto", false, FwDataTypes.Date);
                qry.AddColumn("statusdate", false, FwDataTypes.Date);
                select.Add("select o.orderid, o.orderdesc, o.status, o.orderno, o.orderdate, o.deal, o.estrentfrom, o.estrentto, o.statusdate, o.dealid, o.departmentid");
                select.Add("from  dbo.funcorder('O','F') o");
                select.Add("where o.ordertype = 'O'");
                if (!request.showalllocation)
                {
                    select.Add("  and o.locationid = @locationid"); //MY + EG 2017-08-25: QS needs to support location filter on the user level.
                }
                select.Add("  and o.rentalsale <> 'T'");
                select.Add("  and o.status not in ('CANCELLED', 'SNAPSHOT')");
                select.Add("  and o.hasrentalout = 'T'");
                select.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    switch ((string)request.searchmode)
                    {
                        case "ORDERNO":
                            select.Add("and orderno like @orderno");
                            select.AddParameter("@orderno", request.searchvalue + "%");
                            break;
                        case "DESCRIPTION":
                            {
                                string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < searchValues.Length; i++)
                                {
                                    select.Add($"and orderdesc like @orderdesc{i}");
                                    select.AddParameter($"@orderdesc{i}", $"%{searchValues[i]}%");
                                }
                            }
                            break;
                        case "DEAL":
                            {
                                string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < searchValues.Length; i++)
                                {
                                    select.Add($"and deal like @deal{i}");
                                    select.AddParameter($"@deal{i}", $"%{searchValues[i]}%");
                                }
                            }
                            break;
                    }
                }
                select.Add("order by orderdate desc, orderno desc");
                select.AddParameter("@locationid", userLocation.locationId);

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "")]
        public async Task CreateSuspendedSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.createincontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@orderid", request.orderid);
                    sp.AddParameter("@dealid", request.dealid);
                    sp.AddParameter("@departmentid", request.departmentid);
                    sp.AddParameter("@usersid", session.security.webUser.usersid);
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
        [FwJsonServiceMethod]
        public async Task EnableShowAllLocations(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                //const string METHOD_NAME = "ToggleShowAllLocations";
                bool result;

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select warehousecount = count(*)");
                    qry.Add("  from warehouse with (nolock)");
                    qry.Add(" where inactive <> 'T'");
                    await qry.ExecuteAsync();
                    result = qry.GetField("warehousecount").ToInt32() > 1;
                }
                response.enable = result; 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}