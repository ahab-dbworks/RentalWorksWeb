using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class QuikPick : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public QuikPick(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task QuoteSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "QuoteSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                                 usersId: session.security.webUser.usersid);

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                qry.AddColumn("estrentto", false, FwDataTypes.Date);
                qry.AddColumn("statusdate", false, FwDataTypes.Date);
                qry.AddColumn("orderdate", false, FwDataTypes.Date);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select *");
                select.Add("from   qsdealorderview");
                select.Add(" where locationid = @locationid");
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    switch ((string)request.searchmode)
                    {
                        case "QUOTE":
                            select.Add("and orderno like @orderno");
                            select.AddParameter("@orderno", request.searchvalue + "%");
                            break;
                        case "DESCRIPTION":
                            //select.Add("and orderdesc like @orderdesc");
                            //select.AddParameter("@orderdesc", "%" + request.searchvalue + "%");
                            {
                                string[] searchValues = request.searchvalue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < searchValues.Length; i++)
                                {
                                    select.Add($"and orderdesc like @orderdesc{i}");
                                    select.AddParameter($"@orderdesc{i}", $"%{searchValues[i]}%");
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
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetDeals(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;
                dynamic result;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select dealid, deal");
                qry.Add("from   dealview");
                qry.Add("where  statustype <> 'I'");
                qry.Add("order by deal");
                result = await qry.QueryToDynamicList2Async();

                response.deals = result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task NewQuote(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation;

                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                response.newQuote = await QSSaveQuoteAsync(conn: conn,
                                                orderdesc: request.orderdesc,
                                                location: userLocation.locationId,
                                                estrentfrom: request.estrentfrom,
                                                estfromtime: request.estfromtime,
                                                estrentto: request.estrentto,
                                                esttotime: request.esttotime,
                                                webusersid: session.security.webUser.webusersid,
                                                dealid: request.dealid,
                                                pono: "",
                                                ratetype: request.ratetype,
                                                ordertype: request.ordertype);

                response.order = await GetDealOrderByOrderIdAsync(conn: conn,
                                                       orderid: response.newQuote.orderid); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        private async Task<dynamic> QSSaveQuoteAsync(FwSqlConnection conn, string orderdesc, string location, string estrentfrom, string estfromtime, string estrentto, string esttotime, string webusersid, string dealid, string pono, string ratetype, string ordertype)
        {
            dynamic result;
            FwSqlCommand sp;
            dynamic appoptions;
            
            appoptions = await FwSqlData.GetApplicationOptionsAsync(this.ApplicationConfig.DatabaseSettings);
            orderdesc =  ((FwValidate.IsPropertyDefined(appoptions, "mixedcase")) && (appoptions.mixedcase.enabled)) ? orderdesc : orderdesc.ToUpper();

            sp = new FwSqlCommand(conn, "dbo.qssavequote", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",     SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@orderdesc",   orderdesc);
            sp.AddParameter("@estrentfrom", estrentfrom);
            sp.AddParameter("@estrentto",   estrentto);
            sp.AddParameter("@webusersid",  webusersid);
            sp.AddParameter("@dealid",      dealid);
            sp.AddParameter("@ratetype",    ratetype);
            sp.AddParameter("@ordertype",   ordertype);
            sp.AddParameter("@errno",       SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@errmsg",      SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.orderid = sp.GetParameter("@orderid").ToString();
            result.errno   = sp.GetParameter("@errno").ToString();
            result.errmsg  = sp.GetParameter("@errmsg").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        private async Task<dynamic> GetDealOrderByOrderIdAsync(FwSqlConnection conn, string orderid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   qsdealorderview");
            qry.Add("where  orderid = @orderid");
            qry.AddParameter("@orderid", orderid);
            result = await qry.QueryToDynamicObject2Async();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
