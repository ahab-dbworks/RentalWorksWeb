using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksWeb.Source
{
    public class RwAppData
    {
        //----------------------------------------------------------------------------------------------------
        public static string GetWebUsersId(FwSqlConnection conn, string userid)
        {
            string webUsersId;
            FwSqlCommand qry;

            webUsersId = string.Empty;
            qry = new FwSqlCommand(conn);
            qry.Add("select webusersid = dbo.getwebusersidbylogin(@usersid)");
            qry.AddParameter("@usersid", userid);
            qry.Execute();
            webUsersId = qry.GetField("webusersid").ToString().TrimEnd();
            
            return webUsersId;
        }
        //-----------------------------------------------------------------------------
        public static dynamic GetWebUsersView(FwSqlConnection conn, string webusersid)
        {
            FwSqlCommand qry;
            dynamic result;
            DataTable dt;
            object cellValue;
            IDictionary<String, object> webUser;
            
            result = null;
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from   webusersview with (nolock)");
            qry.Add("where  webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            dt = qry.QueryToTable();
            if (dt.Rows.Count == 1)
            {
                result = new ExpandoObject();
                webUser = result as IDictionary<String, object>;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cellValue = dt.Rows[0][i];
                    if (cellValue is string) {
                        cellValue = cellValue.ToString().TrimEnd();
                    } 
                    webUser[dt.Columns[i].ColumnName] = cellValue;
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetUser(FwSqlConnection conn, string usersId)
        {
            FwSqlCommand qry;
            List<dynamic> dataSet;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from users with(nolock)");
            qry.Add("where usersid = @usersid");
            qry.AddParameter("@usersid", usersId);
            dataSet = qry.QueryToDynamicList();
            if (dataSet.Count == 0)
            {
                throw new Exception("Can't find user.");
            }
            result = dataSet[0];

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetContact(FwSqlConnection conn, string contactId)
        {
            FwSqlCommand qry;
            List<dynamic> rows;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from contact with(nolock)");
            qry.Add("where contactid = @contactid");
            qry.AddParameter("@contactid", contactId);
            rows = qry.QueryToDynamicList();
            if (rows.Count == 0)
            {
                throw new Exception("Can't find contact.");
            }
            result = rows[0];

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetQBOSettings(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select qboconsumerkey, qboconsumersecret, qborequesttokenurl, qboaccesstokenurl, qboauthorizeurl, qbooauthurl, qbobaseurl");
            qry.Add("from chgbatchcontrol with (nolock)");
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic StoreQBOKeys(FwSqlConnection conn, string locationid, string accessKey, string accessSecret, string companyId)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.saveqbokey");
            sp.AddParameter("@locationid",        locationid);
            sp.AddParameter("@accesstoken",       accessKey);
            sp.AddParameter("@accesstokensecret", accessSecret);
            sp.AddParameter("@companyid",         companyId);
            sp.AddParameter("@status",            SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",               SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetQBOKeys(FwSqlConnection conn, string locationid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select accesstoken, accesstokensecret, accesstokendate, companyid");
            qry.Add("from dataexport with (nolock)");
            qry.Add("where dataexportname = 'CHARGE PROCESSING'");
            qry.Add("  and exporttype     = 'QBO'");
            qry.Add("  and locationid     = @locationid");
            qry.AddParameter("@locationid", locationid);
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic DeleteQBOKeys(FwSqlConnection conn, string locationid)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.deleteqbokey");
            sp.AddParameter("@locationid", locationid);
            sp.AddParameter("@status",     SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",        SqlDbType.VarChar, ParameterDirection.Output);
            sp.Execute();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ExportInvoices(FwSqlConnection conn, string batchno, FwDateTime batchfrom, FwDateTime batchto, string locationid)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.exportinvoices");
            sp.AddParameter("@chgbatchid", batchno);
            sp.AddParameter("@fromdate",   batchfrom.GetSqlValue());
            sp.AddParameter("@todate",     batchto.GetSqlValue());
            sp.AddParameter("@locationid", locationid);
            result = sp.QueryToDynamicList();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic InvoiceView(FwSqlConnection conn, string invoiceid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select invoiceno, invoicedate, customer, billtoadd1, billtoadd2, billtocity, billtostate, billtozip, billtocountry, invoiceclass, pono, payterms, invoiceduedate, printnotes, taxitemcode, taxvendor, chgbatchno, invoicetotal");
            qry.Add("from invoiceview with (nolock)");
            qry.Add("where invoiceid = @invoiceid");
            qry.AddParameter("@invoiceid", invoiceid);
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic InvoiceItemView(FwSqlConnection conn, string invoiceid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from invoiceitemview with (nolock)");
            qry.Add("where invoiceid = @invoiceid");
            qry.Add("  and itemclass <> 'ST'");
            qry.AddParameter("@invoiceid", invoiceid);
            result = qry.QueryToDynamicList();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static string GetPayTermsDueDate(FwSqlConnection conn, string payterms)
        {
            FwSqlCommand qry;
            string result;

            qry = new FwSqlCommand(conn);
            qry.Add("select days");
            qry.Add("from payterms with (nolock)");
            qry.Add("where payterms = @payterms");
            qry.AddParameter("@payterms", payterms);
            qry.Execute();
            result = qry.GetField("days").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetTaxRatePercent(FwSqlConnection conn, string taxitemcode)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select rentaltaxrate1, salestaxrate1, labortaxrate1");
            qry.Add("from taxoption with (nolock)");
            qry.Add("where taxitemcode = @taxitemcode");
            qry.AddParameter("@taxitemcode", taxitemcode);
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetAccountInfo(FwSqlConnection conn, string accountid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from glaccount with (nolock)");
            qry.Add("where glaccountid = @accountid");
            qry.AddParameter("@accountid", accountid);
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static void LogChgBatchExportRecordError(FwSqlConnection conn, string responsevalue01, string responsevalue02, string responsevalue03, string msg)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("insert into chgbatchexportrecord (datestamp, chgbatchexportrecordid, recordtype,  responsevalue01,  responsevalue02,  responsevalue03,  msg)");
            qry.Add("                          values (getdate(), @nextid,                @recordtype, @responsevalue01, @responsevalue02, @responsevalue03, @msg)");
            qry.AddParameter("@nextid",          FwSqlData.GetNextId(FwSqlConnection.RentalWorks));
            qry.AddParameter("@recordtype",      "QBO Error");
            qry.AddParameter("@responsevalue01", responsevalue01);
            qry.AddParameter("@responsevalue02", responsevalue02);
            qry.AddParameter("@responsevalue03", responsevalue03);
            qry.AddParameter("@msg",             msg);
            qry.Execute();
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetLocationInfo(FwSqlConnection conn, string locationid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select locationid, location, locationcolor");
            qry.Add("  from location with (nolock)");
            qry.Add(" where locationid = @locationid");
            qry.AddParameter("@locationid", locationid);
            result = qry.QueryToDynamicObject();

            result.locationid    = FwCryptography.AjaxEncrypt(result.locationid);
            result.locationcolor = FwConvert.OleToHex((int)result.locationcolor);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}