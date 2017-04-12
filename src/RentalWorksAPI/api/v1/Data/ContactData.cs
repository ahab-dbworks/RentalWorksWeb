using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace RentalWorksAPI.api.v1.Data
{
    public class ContactData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<Contact> GetContact(string contactid)
        {
            FwSqlCommand qry;
            FwSqlSelect select  = new FwSqlSelect();
            List<Contact> result = new List<Contact>();
            FwJsonDataTable dt  = new FwJsonDataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            select.PageNo   = 0;
            select.PageSize = 0;
            select.Add("select *");
            select.Add("  from api_contactview");
            if (!string.IsNullOrEmpty(contactid))
            {
                select.Add(" where contactid = @contactid");
                select.AddParameter("@contactid", contactid);
            }
            select.Add("order by lastname");

            dt = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Contact contact = new Contact();

                contact.contactid      = dt.Rows[i][dt.ColumnIndex["contactid"]].ToString().TrimEnd();
                contact.firstname      = dt.Rows[i][dt.ColumnIndex["firstname"]].ToString().TrimEnd();
                contact.middlename     = dt.Rows[i][dt.ColumnIndex["mi"]].ToString().TrimEnd();
                contact.lastname       = dt.Rows[i][dt.ColumnIndex["lastname"]].ToString().TrimEnd();
                contact.email          = dt.Rows[i][dt.ColumnIndex["email"]].ToString().TrimEnd();
                contact.officephone    = dt.Rows[i][dt.ColumnIndex["officephone"]].ToString().TrimEnd();
                contact.officeext      = dt.Rows[i][dt.ColumnIndex["ext"]].ToString().TrimEnd();
                contact.homephone      = dt.Rows[i][dt.ColumnIndex["phone"]].ToString().TrimEnd();
                contact.fax            = dt.Rows[i][dt.ColumnIndex["fax"]].ToString().TrimEnd();
                contact.faxext         = dt.Rows[i][dt.ColumnIndex["faxext"]].ToString().TrimEnd();
                contact.cellular       = dt.Rows[i][dt.ColumnIndex["cellular"]].ToString().TrimEnd();
                contact.directphone    = dt.Rows[i][dt.ColumnIndex["directphone"]].ToString().TrimEnd();
                contact.directext      = dt.Rows[i][dt.ColumnIndex["directext"]].ToString().TrimEnd();
                contact.pager          = dt.Rows[i][dt.ColumnIndex["pager"]].ToString().TrimEnd();
                contact.pagerpin       = dt.Rows[i][dt.ColumnIndex["pagerbin"]].ToString().TrimEnd();
                contact.employedby     = dt.Rows[i][dt.ColumnIndex["employedby"]].ToString().TrimEnd();
                contact.employedbyno   = dt.Rows[i][dt.ColumnIndex["employedbyno"]].ToString().TrimEnd();
                contact.employedbyid   = dt.Rows[i][dt.ColumnIndex["employedbyid"]].ToString().TrimEnd();
                contact.companytype    = dt.Rows[i][dt.ColumnIndex["companytype"]].ToString().TrimEnd();
                contact.contacttitle   = dt.Rows[i][dt.ColumnIndex["contacttitle"]].ToString().TrimEnd();
                contact.jobtitle       = dt.Rows[i][dt.ColumnIndex["jobtitle"]].ToString().TrimEnd();
                contact.primarycontact = dt.Rows[i][dt.ColumnIndex["primarycontact"]].ToString().TrimEnd();
                contact.authorized     = dt.Rows[i][dt.ColumnIndex["authorized"]].ToString().TrimEnd();
                contact.barcode        = dt.Rows[i][dt.ColumnIndex["barcode"]].ToString().TrimEnd();
                contact.sourceid       = dt.Rows[i][dt.ColumnIndex["sourceid"]].ToString().TrimEnd();
                contact.datestamp      = dt.Rows[i][dt.ColumnIndex["datestamp"]].ToString().TrimEnd();

                contact.address          = new Address();
                contact.address.address1 = dt.Rows[i][dt.ColumnIndex["address1"]].ToString().TrimEnd();
                contact.address.address2 = dt.Rows[i][dt.ColumnIndex["address2"]].ToString().TrimEnd();
                contact.address.city     = dt.Rows[i][dt.ColumnIndex["city"]].ToString().TrimEnd();
                contact.address.state    = dt.Rows[i][dt.ColumnIndex["state"]].ToString().TrimEnd();
                contact.address.zipcode  = dt.Rows[i][dt.ColumnIndex["zip"]].ToString().TrimEnd();
                contact.address.country  = dt.Rows[i][dt.ColumnIndex["countrycode"]].ToString().TrimEnd();

                result.Add(contact);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ProcessContact(Contact contact)
        {
            FwSqlCommand sp;
            dynamic result = new ExpandoObject();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "api_processcontact");
            sp.AddParameter("@firstname",         contact.firstname);
            sp.AddParameter("@mi",                contact.middlename);
            sp.AddParameter("@lastname",          contact.lastname);
            sp.AddParameter("@email",             contact.email);
            if (contact.address != null)
            {
                sp.AddParameter("@address1",          contact.address.address1);
                sp.AddParameter("@address2",          contact.address.address2);
                sp.AddParameter("@city",              contact.address.city);
                sp.AddParameter("@state",             contact.address.state);
                sp.AddParameter("@zip",               contact.address.zipcode);
                sp.AddParameter("@countrycode",       contact.address.country);
            }
            sp.AddParameter("@officephone",       contact.officephone);
            sp.AddParameter("@ext",               contact.officeext);
            sp.AddParameter("@homephone",         contact.homephone);
            sp.AddParameter("@fax",               contact.fax);
            sp.AddParameter("@faxext",            contact.faxext);
            sp.AddParameter("@cellular",          contact.cellular);
            sp.AddParameter("@directphone",       contact.directphone);
            sp.AddParameter("@directext",         contact.directext);
            sp.AddParameter("@pager",             contact.pager);
            sp.AddParameter("@pagerbin",          contact.pagerpin);
            sp.AddParameter("@employedbyid",      contact.employedbyid);
            sp.AddParameter("@employedby",        contact.employedby);
            sp.AddParameter("@employedbyno",      contact.employedbyno);
            sp.AddParameter("@contacttitle",      contact.contacttitle);
            sp.AddParameter("@companytype",       contact.companytype);
            sp.AddParameter("@jobtitle",          contact.jobtitle);
            sp.AddParameter("@primarycontact",    contact.primarycontact);
            sp.AddParameter("@barcode",           contact.barcode);
            sp.AddParameter("@sourceid",          contact.sourceid);
            sp.AddParameter("@datestamp",         System.Data.SqlDbType.DateTime, System.Data.ParameterDirection.InputOutput, contact.datestamp);
            sp.AddParameter("@contactid",         System.Data.SqlDbType.Char,     System.Data.ParameterDirection.InputOutput, contact.contactid);
            sp.AddParameter("@errno",             System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output,      0);
            sp.AddParameter("@errmsg",            System.Data.SqlDbType.VarChar,  System.Data.ParameterDirection.Output,      255);
            sp.Execute();

            result.contactid = sp.GetParameter("@contactid").ToString().TrimEnd();
            result.errno     = sp.GetParameter("@errno").ToString().TrimEnd();
            result.errmsg    = sp.GetParameter("@errmsg").ToString().TrimEnd();
            result.datestamp = sp.GetParameter("@datestamp").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Order> GetContactOrders(string webusersid, OrderParameters request)
        {
            FwSqlCommand qry;
            List<Order> result = new List<Order>();
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_funcgetcontactorder(@webusersid, @dealid)");
            qry.Add(" where orderid = orderid");
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
            qry.AddParameter("@webusersid", webusersid);
            if (request.dealid == null)
            {
                qry.AddParameter("@dealid", DBNull.Value);
            }
            else
            {
                qry.AddParameter("@dealid", request.dealid);
            }
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
                newOrder.orderdate                 = dt.Rows[i]["orderdate"].ToString().TrimEnd();
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
                newOrder.picktime                  = dt.Rows[i]["picktime"].ToString().TrimEnd();
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
                newOrder.ordertypedescription      = dt.Rows[i]["ordertypedescription"].ToString().TrimEnd();
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
        public static List<Deal> GetContactDeals(string webusersid)
        {
            FwSqlCommand qry;
            List<Deal> result = new List<Deal>();
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_funcgetcontactdeal(@webusersid)");
            qry.AddParameter("@webusersid", webusersid);
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Deal deal = new Deal();

                deal.dealid   = dt.Rows[i]["dealid"].ToString().TrimEnd();
                deal.dealname = dt.Rows[i]["deal"].ToString().TrimEnd();

                result.Add(deal);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static WebUsers GetWebUser(string webusersid)
        {
            FwSqlCommand qry;
            WebUsers result = new WebUsers();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from webusersview");
            qry.Add(" where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            qry.Execute();

            result.webusersid              = qry.GetField("webusersid").ToString().TrimEnd();
            result.usersid                 = qry.GetField("usersid").ToString().TrimEnd();
            result.contactid               = qry.GetField("contactid").ToString().TrimEnd();
            result.dealid                  = qry.GetField("dealid").ToString().TrimEnd();
            result.name                    = qry.GetField("name").ToString().TrimEnd();
            result.username                = qry.GetField("username").ToString().TrimEnd();
            result.fullname                = qry.GetField("fullname").ToString().TrimEnd();
            result.email                   = qry.GetField("email").ToString().TrimEnd();
            result.changepasswordatlogin   = qry.GetField("changepasswordatlogin").ToString().TrimEnd();
            result.primarydepartmentid     = qry.GetField("primarydepartmentid").ToString().TrimEnd();
            result.rentaldepartmentid      = qry.GetField("rentaldepartmentid").ToString().TrimEnd();
            result.salesdepartmentid       = qry.GetField("salesdepartmentid").ToString().TrimEnd();
            result.rentalagentusersid      = qry.GetField("rentalagentusersid").ToString().TrimEnd();
            result.salesagentusersid       = qry.GetField("salesagentusersid").ToString().TrimEnd();
            result.partsdepartmentid       = qry.GetField("partsdepartmentid").ToString().TrimEnd();
            result.labordepartmentid       = qry.GetField("labordepartmentid").ToString().TrimEnd();
            result.miscdepartmentid        = qry.GetField("miscdepartmentid").ToString().TrimEnd();
            result.spacedepartmentid       = qry.GetField("spacedepartmentid").ToString().TrimEnd();
            result.titletype               = qry.GetField("titletype").ToString().TrimEnd();
            result.title                   = qry.GetField("title").ToString().TrimEnd();
            result.department              = qry.GetField("department").ToString().TrimEnd();
            result.departmentid            = qry.GetField("departmentid").ToString().TrimEnd();
            result.locationid              = qry.GetField("locationid").ToString().TrimEnd();
            result.location                = qry.GetField("location").ToString().TrimEnd();
            result.warehouseid             = qry.GetField("warehouseid").ToString().TrimEnd();
            result.warehouse               = qry.GetField("warehouse").ToString().TrimEnd();
            result.office                  = qry.GetField("office").ToString().TrimEnd();
            result.phoneextension          = qry.GetField("phoneextension").ToString().TrimEnd();
            result.fax                     = qry.GetField("fax").ToString().TrimEnd();
            result.usertype                = qry.GetField("usertype").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}