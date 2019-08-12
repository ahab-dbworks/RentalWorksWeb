using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using System.Collections.Generic;
using System.Dynamic;

namespace RentalWorksAPI.api.v2.Data
{
    public class DealData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<Deal> GetDeal(string dealid, string dealno)
        {
            FwSqlCommand qry;
            FwSqlSelect select  = new FwSqlSelect();
            List<Deal> result   = new List<Deal>();
            FwJsonDataTable dt  = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select *");
            select.Add("  from api_dealview");
            if (!string.IsNullOrEmpty(dealid))
            {
                select.Add(" where dealid = @dealid");
                select.AddParameter("@dealid", dealid);
            }
            if (!string.IsNullOrEmpty(dealno))
            {
                select.Add(" where dealno = @dealno");
                select.AddParameter("@dealno", dealno);
            }
            select.Add("order by deal");

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Deal deal = new Deal();

                deal.customer       = dt.Rows[i][dt.ColumnIndex["customer"]].ToString().TrimEnd();
                deal.dealno         = dt.Rows[i][dt.ColumnIndex["dealno"]].ToString().TrimEnd();
                deal.customerno     = dt.Rows[i][dt.ColumnIndex["custno"]].ToString().TrimEnd();
                deal.dealname       = dt.Rows[i][dt.ColumnIndex["deal"]].ToString().TrimEnd();
                deal.dealtype       = dt.Rows[i][dt.ColumnIndex["dealtype"]].ToString().TrimEnd();
                deal.dealstatus     = dt.Rows[i][dt.ColumnIndex["dealstatus"]].ToString().TrimEnd();
                deal.phone          = dt.Rows[i][dt.ColumnIndex["phone"]].ToString().TrimEnd();
                deal.fax            = dt.Rows[i][dt.ColumnIndex["fax"]].ToString().TrimEnd();
                deal.phonetollfree  = dt.Rows[i][dt.ColumnIndex["phone800"]].ToString().TrimEnd();
                deal.phoneother     = dt.Rows[i][dt.ColumnIndex["otherphone"]].ToString().TrimEnd();
                deal.billperiod     = dt.Rows[i][dt.ColumnIndex["billperiod"]].ToString().TrimEnd();
                deal.paymenttype    = dt.Rows[i][dt.ColumnIndex["paytype"]].ToString().TrimEnd();
                deal.creditstatus   = dt.Rows[i][dt.ColumnIndex["creditstatus"]].ToString().TrimEnd();
                deal.paymentterms   = dt.Rows[i][dt.ColumnIndex["payterms"]].ToString().TrimEnd();
                deal.porequired     = dt.Rows[i][dt.ColumnIndex["porequired"]].ToString().TrimEnd();
                deal.officelocation = dt.Rows[i][dt.ColumnIndex["officelocation"]].ToString().TrimEnd();
                deal.dealid         = dt.Rows[i][dt.ColumnIndex["dealid"]].ToString().TrimEnd();
                deal.customerid     = dt.Rows[i][dt.ColumnIndex["customerid"]].ToString().TrimEnd();
                deal.department     = dt.Rows[i][dt.ColumnIndex["department"]].ToString().TrimEnd();
                deal.taxfedno       = dt.Rows[i][dt.ColumnIndex["taxfedno"]].ToString().TrimEnd();
                deal.datestamp      = dt.Rows[i][dt.ColumnIndex["datestamp"]].ToString().TrimEnd();

                deal.address          = new Address();
                deal.address.address1 = dt.Rows[i][dt.ColumnIndex["address1"]].ToString().TrimEnd();
                deal.address.address2 = dt.Rows[i][dt.ColumnIndex["address2"]].ToString().TrimEnd();
                deal.address.city     = dt.Rows[i][dt.ColumnIndex["city"]].ToString().TrimEnd();
                deal.address.state    = dt.Rows[i][dt.ColumnIndex["state"]].ToString().TrimEnd();
                deal.address.zipcode  = dt.Rows[i][dt.ColumnIndex["zip"]].ToString().TrimEnd();
                deal.address.country  = dt.Rows[i][dt.ColumnIndex["country"]].ToString().TrimEnd();

                result.Add(deal);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ProcessDeal(Deal deal)
        {
            FwSqlCommand sp;
            dynamic result = new ExpandoObject();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "api_processdeal");
            sp.AddParameter("@customer",          deal.customer);
            sp.AddParameter("@customerid",        deal.customerid);
            sp.AddParameter("@dealno",            deal.dealno);
            sp.AddParameter("@deal",              deal.dealname);
            sp.AddParameter("@dealtype",          deal.dealtype);
            if (deal.address != null)
            {
                sp.AddParameter("@address1", deal.address.address1);
                sp.AddParameter("@address2", deal.address.address2);
                sp.AddParameter("@city",     deal.address.city);
                sp.AddParameter("@state",    deal.address.state);
                sp.AddParameter("@zip",      deal.address.zipcode);
                sp.AddParameter("@country",  deal.address.country);
            }
            sp.AddParameter("@dealstatus",        deal.dealstatus);
            sp.AddParameter("@mainphone",         deal.phone);
            sp.AddParameter("@fax",               deal.fax);
            sp.AddParameter("@phone800",          deal.phonetollfree);
            sp.AddParameter("@otherphone",        deal.phoneother);
            sp.AddParameter("@billperiod",        deal.billperiod);
            sp.AddParameter("@paytype",           deal.paymenttype);
            sp.AddParameter("@creditstatus",      deal.creditstatus);
            sp.AddParameter("@taxoption",         "");
            sp.AddParameter("@payterms",          deal.paymentterms);
            sp.AddParameter("@porequired",        deal.porequired);
            sp.AddParameter("@officelocation",    deal.officelocation);
            sp.AddParameter("@department",        deal.department);
            sp.AddParameter("@taxfedno",          deal.taxfedno);
            sp.AddParameter("@datestamp",         System.Data.SqlDbType.DateTime, System.Data.ParameterDirection.InputOutput, deal.datestamp);
            sp.AddParameter("@dealid",            System.Data.SqlDbType.Char,     System.Data.ParameterDirection.InputOutput, deal.dealid);
            sp.AddParameter("@errno",             System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output,      0);
            sp.AddParameter("@errmsg",            System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output,      255);
            sp.Execute();

            result.dealid    = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.errno     = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg    = sp.GetParameter("@errmsg").ToString().TrimEnd();
            result.datestamp = sp.GetParameter("@datestamp").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}