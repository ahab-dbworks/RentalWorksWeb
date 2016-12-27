using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    class QuoteMenu
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void FindQuote(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);
            
            response.dealorder = GetDealOrderByBarcode(conn:       FwSqlConnection.RentalWorks,
                                                       barcode:    request.barcode,
                                                       usersid:    session.security.webUser.usersid,
                                                       locationid: userLocation.locationId);
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic GetDealOrderByBarcode(FwSqlConnection conn, string barcode, string usersid, string locationid)
        {
            dynamic result;
            DataTable dt = new DataTable();
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("from qsdealorderview");
            qry.Add("where locationid = @locationid");
            if (barcode != "")
            {
                qry.Add("  and  orderno   = @barcode");
                qry.AddParameter("@barcode", barcode);
            }
            DepartmentFilter.SetDepartmentFilter(usersid, qry);
            qry.Add("order by orderdate desc, orderno desc");
            qry.AddParameter("@locationid", locationid);
            dt = qry.QueryToTable();
            result = new ExpandoObject[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result[i]             = new ExpandoObject();
                result[i].orderid     = new FwDatabaseField(dt.Rows[i]["orderid"]).ToString().TrimEnd();
                result[i].orderno     = new FwDatabaseField(dt.Rows[i]["orderno"]).ToString().TrimEnd();
                result[i].dealno      = new FwDatabaseField(dt.Rows[i]["dealno"]).ToString().TrimEnd();
                result[i].deal        = new FwDatabaseField(dt.Rows[i]["deal"]).ToString().TrimEnd();
                result[i].orderdesc   = new FwDatabaseField(dt.Rows[i]["orderdesc"]).ToString().TrimEnd();
                result[i].estrentfrom = new FwDatabaseField(dt.Rows[i]["estrentfrom"]).ToShortDateString();
                result[i].estfromtime = new FwDatabaseField(dt.Rows[i]["estfromtime"]).ToString().TrimEnd();
                result[i].estrentto   = new FwDatabaseField(dt.Rows[i]["estrentto"]).ToShortDateString();
                result[i].esttotime   = new FwDatabaseField(dt.Rows[i]["esttotime"]).ToString().TrimEnd();
                result[i].ordertype   = new FwDatabaseField(dt.Rows[i]["ordertype"]).ToString().TrimEnd();
                result[i].status      = new FwDatabaseField(dt.Rows[i]["status"]).ToString().TrimEnd();
                result[i].statusdate  = new FwDatabaseField(dt.Rows[i]["statusdate"]).ToShortDateString();
                result[i].inputbyid   = new FwDatabaseField(dt.Rows[i]["inputbyid"]).ToString().TrimEnd();
                result[i].orderdate   = new FwDatabaseField(dt.Rows[i]["orderdate"]).ToShortDateString();
                result[i].locationid  = new FwDatabaseField(dt.Rows[i]["locationid"]).ToString().TrimEnd();
            }

            return result;
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
            result = qry.QueryToDynamicList();

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
            result = qry.QueryToDynamicObject();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
