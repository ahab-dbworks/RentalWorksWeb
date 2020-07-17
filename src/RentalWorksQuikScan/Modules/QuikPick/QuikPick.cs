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
    class QuikPick
    {
        [FwJsonServiceMethod]
        public void QuoteSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "QuoteSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentto",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("statusdate",  false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("orderdate",   false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = request.pageno;
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
                            string[] searchValues = request.searchvalue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < searchValues.Length; i++)
                            {
                                select.Add($"and orderdesc like @searchvalue{i}");
                                select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                            }
                        }
                        break;
                }
            }
            select.Add("order by orderdate desc, orderno desc");
            select.AddParameter("@locationid", userLocation.locationId);

            response.searchresults = qry.QueryToFwJsonTable(select, true);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetDeals(dynamic request, dynamic response, dynamic session)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select dealid, deal");
            qry.Add("from   dealview");
            qry.Add("where  statustype <> 'I'");
            qry.Add("order by deal");
            result = qry.QueryToDynamicList2();

            response.deals = result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void NewQuote(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);
            
            response.newQuote = QSSaveQuote(conn:        FwSqlConnection.RentalWorks          ,
                                            orderdesc:   request.orderdesc                    ,
                                            location:    userLocation.locationId              ,
                                            estrentfrom: request.estrentfrom                  ,
                                            estfromtime: request.estfromtime                  ,
                                            estrentto:   request.estrentto                    ,
                                            esttotime:   request.esttotime                    ,
                                            webusersid:  session.security.webUser.webusersid  ,
                                            dealid:      request.dealid                       ,
                                            pono:        ""                                   ,
                                            ratetype:    request.ratetype                     ,
                                            ordertype:   request.ordertype);

            response.order = GetDealOrderByOrderId(conn:    FwSqlConnection.RentalWorks,
                                                   orderid: response.newQuote.orderid);
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QSSaveQuote(FwSqlConnection conn, string orderdesc, string location, string estrentfrom, string estfromtime, string estrentto, string esttotime, string webusersid, string dealid, string pono, string ratetype, string ordertype)
        {
            dynamic result;
            FwSqlCommand sp;
            dynamic appoptions;
            
            appoptions = FwSqlData.GetApplicationOptions(conn);
            orderdesc =  ((FwValidate.IsPropertyDefined(appoptions, "mixedcase")) && (appoptions.mixedcase.enabled)) ? orderdesc : orderdesc.ToUpper();

            sp = new FwSqlCommand(conn, "dbo.qssavequote");
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
            sp.Execute();
            result = new ExpandoObject();
            result.orderid = sp.GetParameter("@orderid").ToString();
            result.errno   = sp.GetParameter("@errno").ToString();
            result.errmsg  = sp.GetParameter("@errmsg").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic GetDealOrderByOrderId(FwSqlConnection conn, string orderid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from   qsdealorderview");
            qry.Add("where  orderid = @orderid");
            qry.AddParameter("@orderid", orderid);
            result = qry.QueryToDynamicObject2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
