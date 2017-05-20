using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using System.Collections.Generic;
using System.Dynamic;

namespace RentalWorksAPI.api.v2.Data
{
    public class OrderData
    {
        //----------------------------------------------------------------------------------------------------
        public static Csrs GetCsrs(string locationid, string csrid)
        {
            FwSqlCommand qry;
            Csrs result  = new Csrs();
            dynamic qryresult = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select top 1 *");
            qry.Add("  from apirest_csrdeal");
            qry.Add(" where csrid = @csrid");
            qry.AddParameter("@csrid", csrid);

            qryresult = qry.QueryToDynamicObject2();

            if (qryresult.csrid != "")
            {
                result.csrid = qryresult.csrid;
                result.deals = GetCsrsDeals(csrid);
            }

            return result;
        }
        //------------------------------------------------------------------------------
        public static List<Deal> GetCsrsDeals(string csrid)
        {
            FwSqlCommand qry;
            List<Deal> deals = new List<Deal>();
            dynamic qryresult;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_csrdeal");
            qry.Add(" where csrid = @csrid");
            qry.AddParameter("@csrid", csrid);
            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                Deal deal = new Deal();

                deal.dealid   = qryresult[i].dealid;
                deal.dealname = qryresult[i].deal;

                deals.Add(deal);
            }

            return deals;
        }
        //------------------------------------------------------------------------------
    }
}