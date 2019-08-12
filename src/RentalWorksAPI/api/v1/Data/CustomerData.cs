using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v1.Models;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace RentalWorksAPI.api.v1.Data
{
    public class CustomerData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<Customer> GetCustomer(string customerid, string customerno)
        {
            FwSqlCommand qry;
            FwSqlSelect select    = new FwSqlSelect();
            List<Customer> result = new List<Customer>();
            FwJsonDataTable dt    = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("insvalidthru",      false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("creditthroughdate", false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select *");
            select.Add("  from api_customerview");
            select.Parse();
            if (!string.IsNullOrEmpty(customerid))
            {
                select.AddWhere("customerid = @customerid");
                select.AddParameter("@customerid", customerid);
            }
            if (!string.IsNullOrEmpty(customerno))
            {
                select.AddWhere("custno = @custno");
                select.AddParameter("@custno", customerno);
            }
            select.Add("order by customer");

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Customer newCustomer = new Customer();

                newCustomer.customerid             = dt.Rows[i][dt.ColumnIndex["customerid"]].ToString().TrimEnd();
                newCustomer.customerno             = dt.Rows[i][dt.ColumnIndex["custno"]].ToString().TrimEnd();
                newCustomer.customername           = dt.Rows[i][dt.ColumnIndex["customer"]].ToString().TrimEnd();
                newCustomer.location               = dt.Rows[i][dt.ColumnIndex["location"]].ToString().TrimEnd();
                newCustomer.customertype           = dt.Rows[i][dt.ColumnIndex["custtype"]].ToString().TrimEnd();
                newCustomer.customercategory       = dt.Rows[i][dt.ColumnIndex["custcatdesc"]].ToString().TrimEnd();
                newCustomer.phone                  = dt.Rows[i][dt.ColumnIndex["phone"]].ToString().TrimEnd();
                newCustomer.fax                    = dt.Rows[i][dt.ColumnIndex["faxno"]].ToString().TrimEnd();
                newCustomer.phonetollfree          = dt.Rows[i][dt.ColumnIndex["phone800"]].ToString().TrimEnd();
                newCustomer.phoneother             = dt.Rows[i][dt.ColumnIndex["phoneother"]].ToString().TrimEnd();
                newCustomer.creditstatus           = dt.Rows[i][dt.ColumnIndex["creditstatus"]].ToString().TrimEnd();
                newCustomer.creditthroughdate      = dt.Rows[i][dt.ColumnIndex["creditthroughdate"]].ToString().TrimEnd();
                newCustomer.creditlimit            = dt.Rows[i][dt.ColumnIndex["creditlimit"]].ToString().TrimEnd();
                newCustomer.website                = dt.Rows[i][dt.ColumnIndex["webaddress"]].ToString().TrimEnd();
                newCustomer.certificateofinsurance = dt.Rows[i][dt.ColumnIndex["certofins"]].ToString().TrimEnd();
                newCustomer.insurancevalidthru     = dt.Rows[i][dt.ColumnIndex["insvalidthru"]].ToString().TrimEnd();
                newCustomer.taxable                = dt.Rows[i][dt.ColumnIndex["taxable"]].ToString().TrimEnd();
                newCustomer.billtoaddress          = dt.Rows[i][dt.ColumnIndex["billtoadd"]].ToString().TrimEnd();
                newCustomer.billtoattention        = dt.Rows[i][dt.ColumnIndex["billtoatt"]].ToString().TrimEnd();
                newCustomer.customerstatus         = dt.Rows[i][dt.ColumnIndex["custstatus"]].ToString().TrimEnd();
                newCustomer.taxfedno               = dt.Rows[i][dt.ColumnIndex["taxfedno"]].ToString().TrimEnd();
                newCustomer.department             = dt.Rows[i][dt.ColumnIndex["department"]].ToString().TrimEnd();
                newCustomer.datestamp              = dt.Rows[i][dt.ColumnIndex["datestamp"]].ToString().TrimEnd();

                newCustomer.address                = new Address();
                newCustomer.address.address1       = dt.Rows[i][dt.ColumnIndex["add1"]].ToString().TrimEnd();
                newCustomer.address.address2       = dt.Rows[i][dt.ColumnIndex["add2"]].ToString().TrimEnd();
                newCustomer.address.city           = dt.Rows[i][dt.ColumnIndex["city"]].ToString().TrimEnd();
                newCustomer.address.state          = dt.Rows[i][dt.ColumnIndex["state"]].ToString().TrimEnd();
                newCustomer.address.zipcode        = dt.Rows[i][dt.ColumnIndex["zip"]].ToString().TrimEnd();
                newCustomer.address.country        = dt.Rows[i][dt.ColumnIndex["countrycode"]].ToString().TrimEnd();

                result.Add(newCustomer);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Order> GetCustomerOrders(string customerid, OrderParameters request)
        {
            FwSqlCommand qry;
            List<Order> result = new List<Order>();
            DataTable dt       = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_orderview");
            qry.Add(" where customerid = @customerid");
            if (request.statuses != null)
            {
                string parameterizedstatusstring;
                parameterizedstatusstring = qry.GetParameterizedIn("status", request.statuses.ToList<object>());
                qry.Add("  and status in (" + parameterizedstatusstring + ")");
            }
            if ((request.rental == "true") && ((request.sales == null) || (request.sales == "false")))
            {
                qry.Add("  and rental = @rental");
                qry.AddParameter("@rental", ((request.rental == "true") ? "T" : "F"));
            }
            if ((request.sales == "true") && ((request.rental == null) || (request.rental == "false")))
            {
                qry.Add("  and sales = @sales");
                qry.AddParameter("@sales", ((request.sales == "true") ? "T" : "F"));
            }
            qry.AddParameter("@customerid", customerid);
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Order newOrder = new Order();

                newOrder.orderno                   = dt.Rows[i]["orderno"].ToString().TrimEnd();
                newOrder.orderdesc                 = dt.Rows[i]["orderdesc"].ToString().TrimEnd();
                newOrder.status                    = dt.Rows[i]["status"].ToString().TrimEnd();
                newOrder.location                  = dt.Rows[i]["location"].ToString().TrimEnd();
                newOrder.orderid                   = dt.Rows[i]["orderid"].ToString().TrimEnd();
                newOrder.ordertype                 = dt.Rows[i]["ordertype"].ToString().TrimEnd();
                newOrder.rental                    = dt.Rows[i]["rental"].ToString().TrimEnd();
                newOrder.sales                     = dt.Rows[i]["sales"].ToString().TrimEnd();
                newOrder.pono                      = dt.Rows[i]["pono"].ToString().TrimEnd();
                newOrder.ratetype                  = dt.Rows[i]["ratetype"].ToString().TrimEnd();
                newOrder.estrentfrom               = dt.Rows[i]["estrentfrom"].ToString().TrimEnd();
                newOrder.estrentto                 = dt.Rows[i]["estrentto"].ToString().TrimEnd();
                newOrder.estfromtime               = dt.Rows[i]["estfromtime"].ToString().TrimEnd();
                newOrder.esttotime                 = dt.Rows[i]["esttotime"].ToString().TrimEnd();
                newOrder.billperiodstart           = dt.Rows[i]["billperiodstart"].ToString().TrimEnd();
                newOrder.billperiodend             = dt.Rows[i]["billperiodend"].ToString().TrimEnd();
                newOrder.webusersid                = dt.Rows[i]["webusersid"].ToString().TrimEnd();
                newOrder.datestamp                 = dt.Rows[i]["datestamp"].ToString().TrimEnd();
                newOrder.agent                     = dt.Rows[i]["agent"].ToString().TrimEnd();
                newOrder.projectmanager            = dt.Rows[i]["projectmanager"].ToString().TrimEnd();
                newOrder.dealtype                  = dt.Rows[i]["dealtype"].ToString().TrimEnd();
                newOrder.department                = dt.Rows[i]["department"].ToString().TrimEnd();
                newOrder.orderlocation             = dt.Rows[i]["orderlocation"].ToString().TrimEnd();
                newOrder.refno                     = dt.Rows[i]["refno"].ToString().TrimEnd();
                newOrder.taxoption                 = dt.Rows[i]["taxoption"].ToString().TrimEnd();
                newOrder.requiredbydate            = dt.Rows[i]["requiredbydate"].ToString().TrimEnd();
                newOrder.requiredbytime            = dt.Rows[i]["requiredbytime"].ToString().TrimEnd();
                newOrder.deliverondate             = dt.Rows[i]["deliverondate"].ToString().TrimEnd();
                newOrder.pickdate                  = dt.Rows[i]["pickdate"].ToString().TrimEnd();
                newOrder.loadindate                = dt.Rows[i]["loadindate"].ToString().TrimEnd();
                newOrder.testdate                  = dt.Rows[i]["testdate"].ToString().TrimEnd();
                newOrder.strikedate                = dt.Rows[i]["strikedate"].ToString().TrimEnd();
                newOrder.rentaltaxrate1            = dt.Rows[i]["rentaltaxrate1"].ToString().TrimEnd();
                newOrder.rentaltaxrate2            = dt.Rows[i]["rentaltaxrate2"].ToString().TrimEnd();
                newOrder.agentid                   = dt.Rows[i]["agentid"].ToString().TrimEnd();
                newOrder.departmentid              = dt.Rows[i]["departmentid"].ToString().TrimEnd();
                newOrder.projectmanagerid          = dt.Rows[i]["projectmanagerid"].ToString().TrimEnd();
                newOrder.dealtypeid                = dt.Rows[i]["dealtypeid"].ToString().TrimEnd();
                newOrder.orderunitid               = dt.Rows[i]["orderunitid"].ToString().TrimEnd();
                newOrder.orderunit                 = dt.Rows[i]["orderunit"].ToString().TrimEnd();
                newOrder.ordertotal                = dt.Rows[i]["ordertotal"].ToString().TrimEnd();
                newOrder.ordergrosstotal           = dt.Rows[i]["ordergrosstotal"].ToString().TrimEnd();
                newOrder.ordertypedescription      = dt.Rows[i]["orderunit"].ToString().TrimEnd();
                newOrder.onlineorderno             = dt.Rows[i]["onlineorderno"].ToString().TrimEnd();

                newOrder.deal                      = new OrderDeal();
                newOrder.deal.dealid               = dt.Rows[i]["dealid"].ToString().TrimEnd();
                newOrder.deal.dealname             = dt.Rows[i]["deal"].ToString().TrimEnd();
                newOrder.deal.dealno               = dt.Rows[i]["dealno"].ToString().TrimEnd();

                newOrder.customer                  = new Customer();
                newOrder.customer.customerid       = dt.Rows[i]["customerid"].ToString().TrimEnd();
                newOrder.customer.customername     = dt.Rows[i]["customer"].ToString().TrimEnd();
                newOrder.customer.customerno       = dt.Rows[i]["custno"].ToString().TrimEnd();

                newOrder.ordernotes                = OrderData.GetOrderNotes(dt.Rows[i]["orderid"].ToString().TrimEnd());

                newOrder.outgoingdelivership           = new Delivery();
                newOrder.outgoingdelivership.type      = dt.Rows[i]["outdeliverydelivertype"].ToString().TrimEnd();
                newOrder.outgoingdelivership.contact   = dt.Rows[i]["outdeliverycontact"].ToString().TrimEnd();
                newOrder.outgoingdelivership.attention = dt.Rows[i]["outdeliveryattention"].ToString().TrimEnd();
                newOrder.outgoingdelivership.phone     = dt.Rows[i]["outdeliverycontactphone"].ToString().TrimEnd();
                newOrder.outgoingdelivership.location  = dt.Rows[i]["outdeliverylocation"].ToString().TrimEnd();
                newOrder.outgoingdelivership.address1  = dt.Rows[i]["outdeliveryadd1"].ToString().TrimEnd();
                newOrder.outgoingdelivership.address2  = dt.Rows[i]["outdeliveryadd2"].ToString().TrimEnd();
                newOrder.outgoingdelivership.city      = dt.Rows[i]["outdeliverycity"].ToString().TrimEnd();
                newOrder.outgoingdelivership.state     = dt.Rows[i]["outdeliverystate"].ToString().TrimEnd();
                newOrder.outgoingdelivership.zipcode   = dt.Rows[i]["outdeliveryzip"].ToString().TrimEnd();
                newOrder.outgoingdelivership.country   = dt.Rows[i]["outdeliverycountry"].ToString().TrimEnd();

                result.Add(newOrder);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ProcessCustomer(Customer customerdata)
        {
            FwSqlCommand sp, updateqry;
            dynamic result = new ExpandoObject();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "api_processcustomer");
            sp.AddParameter("@custno",            customerdata.customerno);
            sp.AddParameter("@customer",          customerdata.customername);
            sp.AddParameter("@location",          customerdata.location);
            sp.AddParameter("@custtype",          customerdata.customertype);
            sp.AddParameter("@custcatdesc",       customerdata.customercategory);
            if (customerdata.address != null)
            {
                sp.AddParameter("@add1",              customerdata.address.address1);
                sp.AddParameter("@add2",              customerdata.address.address2);
                sp.AddParameter("@city",              customerdata.address.city);
                sp.AddParameter("@state",             customerdata.address.state);
                sp.AddParameter("@zip",               customerdata.address.zipcode);
                sp.AddParameter("@countrycode",       customerdata.address.country);
            }
            sp.AddParameter("@phone",             customerdata.phone);
            sp.AddParameter("@faxno",             customerdata.fax);
            sp.AddParameter("@phone800",          customerdata.phonetollfree);
            sp.AddParameter("@phoneother",        customerdata.phoneother);
            sp.AddParameter("@billtoadd",         customerdata.billtoaddress);
            sp.AddParameter("@creditstatus",      customerdata.creditstatus);
            sp.AddParameter("@custstatus",        customerdata.customerstatus);
            //sp.AddParameter("@creditthroughdate", customerdata.creditthroughdate);
            if ((customerdata.creditthroughdate == "") && (!string.IsNullOrEmpty(customerdata.customerid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update customer");
                updateqry.Add("   set creditthroughdate = null");
                updateqry.Add(" where customerid        = @customerid");
                updateqry.AddParameter("@customerid", customerdata.customerid);
                updateqry.Execute();
            }
            else if ((customerdata.creditthroughdate != "") && (customerdata.creditthroughdate != null))
            {
                sp.AddParameter("@creditthroughdate", customerdata.creditthroughdate);
            }
            sp.AddParameter("@taxoption",         "");
            sp.AddParameter("@webaddress",        customerdata.website);
            sp.AddParameter("@certofins",         customerdata.certificateofinsurance);
            //sp.AddParameter("@insvalidthru",      customerdata.insurancevalidthru);
            if ((customerdata.insurancevalidthru == "") && (!string.IsNullOrEmpty(customerdata.customerid)))
            {
                updateqry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                updateqry.Add("update customer");
                updateqry.Add("   set insvalidthru = null");
                updateqry.Add(" where customerid   = @customerid");
                updateqry.AddParameter("@customerid", customerdata.customerid);
                updateqry.Execute();
            }
            else if ((customerdata.insurancevalidthru != "") && (customerdata.insurancevalidthru != null))
            {
                sp.AddParameter("@insvalidthru", customerdata.insurancevalidthru);
            }
            sp.AddParameter("@billtoatt",         customerdata.billtoattention);
            sp.AddParameter("@taxable",           customerdata.taxable);
            sp.AddParameter("@creditlimit",       customerdata.creditlimit);
            sp.AddParameter("@taxfedno",          customerdata.taxfedno);
            sp.AddParameter("@department",        customerdata.department);
            sp.AddParameter("@datestamp",         System.Data.SqlDbType.DateTime, System.Data.ParameterDirection.InputOutput, customerdata.datestamp);
            sp.AddParameter("@customerid",        System.Data.SqlDbType.Char,     System.Data.ParameterDirection.InputOutput, customerdata.customerid);
            sp.AddParameter("@errno",             System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output,      0);
            sp.AddParameter("@errmsg",            System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output,      255);
            sp.Execute();

            result.customerid = sp.GetParameter("@customerid").ToString().TrimEnd();
            result.errno      = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();
            result.datestamp  = sp.GetParameter("@datestamp").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}