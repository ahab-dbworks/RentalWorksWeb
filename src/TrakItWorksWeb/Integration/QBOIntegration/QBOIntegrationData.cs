using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace TrakItWorksWeb.Integration
{
    public class QBOIntegrationData
    {
        //----------------------------------------------------------------------------------------------------
        public static dynamic qbokeys, qbosettings;
        public static List<dynamic> Items                   = new List<dynamic>();
        public static List<dynamic> Invoices                = new List<dynamic>();
        public static List<dynamic> CreditMemos             = new List<dynamic>();
        public static List<dynamic> Classes                 = new List<dynamic>();
        public static List<dynamic> Terms                   = new List<dynamic>();
        public static List<dynamic> Customers               = new List<dynamic>();
        public static List<dynamic> PaymentMethods          = new List<dynamic>();
        public static List<dynamic> TaxItems                = new List<dynamic>(); /*taxcode*/
        public static List<dynamic> TaxRates                = new List<dynamic>();
        public static List<dynamic> Receipts                = new List<dynamic>(); /*payment*/
        public static List<dynamic> Vendors                 = new List<dynamic>();
        public static List<dynamic> TaxAgencies             = new List<dynamic>();
        public static List<dynamic> VendorInvoices          = new List<dynamic>(); /*bill*/
        public static List<dynamic> VendorCredit            = new List<dynamic>();
        public static List<dynamic> Accounts                = new List<dynamic>();
        public static string exportbatchid                  = string.Empty;
        public static string chgbatchexportid               = string.Empty;
        //----------------------------------------------------------------------------------------------------
        //====================================================================================================
        //----------------------------------------------------------------------------------------------------
        private const int RECEIPT_INVOICE_ERROR             = 200;
        //----------------------------------------------------------------------------------------------------
        //====================================================================================================
        //----------------------------------------------------------------------------------------------------
        public class ExportInvoicesToQBOReturn
        {
            public string status  = string.Empty;
            public string message = string.Empty;
            public List<Dictionary<string, string>> invoices = new List<Dictionary<string, string>>();

            public void addInvoice(string invoiceno, string message)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();

                item.Add("invoiceno", invoiceno);
                item.Add("message",   message);

                invoices.Add(item);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static ExportInvoicesToQBOReturn ExportInvoicesToQBO(string batchid, string batchfrom, string batchto, string locationid, string usersid)
        {
            dynamic invoiceids, invoiceinfo;
            ExportInvoicesToQBOReturn result = new ExportInvoicesToQBOReturn();

            try
            {
                exportbatchid   = batchid;
                qbosettings     = GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys         = GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
                invoiceids      = ExportInvoices(FwSqlConnection.RentalWorks, batchid, batchfrom, batchto, locationid);

                for (int i = 0; i < invoiceids.Count; i++)
                {
                    invoiceinfo       = InvoiceView(FwSqlConnection.RentalWorks, invoiceids[i].invoiceid);

                    if (invoiceinfo.invoicetotal >= 0)
                    {
                        ValidateInvoice(invoiceinfo, result);
                    }
                    else if (invoiceinfo.invoicetotal < 0)
                    {
                        ValidateCreditMemo(invoiceinfo, result);
                    }
                }
                result.status  = "0";
                result.message = "All invoices exported to Quickbooks successfully.";
            }
            catch (Exception ex)
            {
                result.status  = "100";
                result.message = ex.Message + "<br/> " + ex.StackTrace;
                LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, batchid, "", "", "", ex.Message  + ex.StackTrace);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public class ExportReceiptsToQBOReturn
        {
            public string status = string.Empty;
            public string message = string.Empty;
            public List<Dictionary<string, string>> receipts = new List<Dictionary<string, string>>();

            public void addReceipt(string receiptno, string message)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();

                item.Add("receiptno", receiptno);
                item.Add("message", message);

                receipts.Add(item);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static ExportReceiptsToQBOReturn ExportReceiptsToQBO(string batchid, string batchfrom, string batchto, string locationid)
        {
            dynamic arids, payment;
            ExportReceiptsToQBOReturn result = new ExportReceiptsToQBOReturn();

            try
            {
                exportbatchid   = batchid;
                qbosettings     = GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys         = GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
                arids           = ExportRecepits(FwSqlConnection.RentalWorks, batchid, batchfrom, batchto, locationid);

                for (int i = 0; i < arids.Count; i++)
                {
                    payment              = ReceiptView(FwSqlConnection.RentalWorks, arids[i].arid);

                    ValidateReceipt(payment, result);
                }
                result.status  = "0";
                result.message = "All invoices exported to Quickbooks successfully.";
            }
            catch (Exception ex)
            {
                result.status  = "100";
                result.message = ex.Message + "<br/> " + ex.StackTrace;
                LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, batchid, "", "", "", ex.Message  + ex.StackTrace);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public class ExportVendorInvoicesToQBOReturn
        {
            public string status = string.Empty;
            public string message = string.Empty;
            public List<Dictionary<string, string>> vendorinvoices = new List<Dictionary<string, string>>();

            public void addVendorInvoice(string invoiceno, string message)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();

                item.Add("invoiceno", invoiceno);
                item.Add("message", message);

                vendorinvoices.Add(item);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static ExportVendorInvoicesToQBOReturn ExportVendorInvoicesToQBO(string batchid, string locationid)
        {
            dynamic vendorinvoiceids, vendorinvoiceinfo;
            ExportVendorInvoicesToQBOReturn result = new ExportVendorInvoicesToQBOReturn();

            try
            {
                exportbatchid    = batchid;
                qbosettings      = GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys          = GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
                vendorinvoiceids = ExportVendorInvoices(FwSqlConnection.RentalWorks, batchid);

                for (int i = 0; i < vendorinvoiceids.Count; i++)
                {
                    vendorinvoiceinfo       = VendorInvoiceView(FwSqlConnection.RentalWorks, vendorinvoiceids[i].vendorinvoiceid);

                    if (vendorinvoiceinfo.invoicetotal >= 0)
                    {
                        ValidateVendorInvoice(vendorinvoiceinfo, result);
                    }
                    else if (vendorinvoiceinfo.invoicetotal < 0)
                    {
                        ValidateVendorCredit(vendorinvoiceinfo, result);
                    }
                }
                result.status  = "0";
                result.message = "All invoices exported to Quickbooks successfully.";
            }
            catch (Exception ex)
            {
                result.status  = "100";
                result.message = ex.Message + "<br/> " + ex.StackTrace;
                LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, batchid, "", "", "", ex.Message  + ex.StackTrace);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        //====================================================================================================
        //----------------------------------------------------------------------------------------------------
        private class QBOQueryResponse
        {
            public int     StatusCode   = 0;
            public dynamic JSONResponse = new ExpandoObject();
        }
        //------------------------------------------------------------------------------
        private static QBOQueryResponse QueryQBO(string query)
        {
            string encodedQuery            = System.Net.WebUtility.UrlEncode(query);
            string uri                     = string.Format("{0}/company/{1}/query?query={2}", qbosettings.qbobaseurl, qbokeys.companyid, encodedQuery);
            HttpWebRequest httpWebRequest  = WebRequest.Create(uri) as HttpWebRequest;
            string responseFromServer      = string.Empty;
            QBOQueryResponse queryresponse = new QBOQueryResponse();

            httpWebRequest.Method         = "GET";
            httpWebRequest.Accept         = "application/json";
            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(qbosettings.qboconsumerkey, qbosettings.qboconsumersecret, qbokeys.accesstoken, qbokeys.accesstokensecret, httpWebRequest, null));

            try
            {
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                using (Stream data = httpWebResponse.GetResponseStream())
                {
                    responseFromServer = new StreamReader(data).ReadToEnd();

                    queryresponse.StatusCode = (int)httpWebResponse.StatusCode;
                    JsonConvert.PopulateObject(responseFromServer, queryresponse.JSONResponse);
                }
            }
            catch (WebException e)
            {
                if (e.Response != null) {
                    using (var errorResponse = (HttpWebResponse)e.Response) {
                        queryresponse.StatusCode = (int)errorResponse.StatusCode;
                        using (var reader = new StreamReader(errorResponse.GetResponseStream())) {
                            responseFromServer = reader.ReadToEnd();
                            LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, exportbatchid, "", responseFromServer, query, e.Message);
                            JsonConvert.PopulateObject(responseFromServer, queryresponse.JSONResponse);
                        }
                    }
                }
            }

            return queryresponse;
        }
        //----------------------------------------------------------------------------------------------------
        private class QBOPostResponse
        {
            public int     StatusCode   = 0;
            public dynamic JSONResponse = new ExpandoObject();
        }
        //----------------------------------------------------------------------------------------------------
        private static QBOPostResponse PostToQBO(string entity, dynamic postObject)
        {
            string uri                    = string.Format("{0}/company/{1}/{2}", qbosettings.qbobaseurl, qbokeys.companyid, entity);
            HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
            string responseFromServer     = string.Empty;
            QBOPostResponse postresponse  = new QBOPostResponse();
            string postStr                = JsonConvert.SerializeObject(postObject);
            string decodedPostStr         = HttpUtility.HtmlDecode(postStr);
            byte[] byteArray              = Encoding.UTF8.GetBytes(decodedPostStr);

            httpWebRequest.Method         = "POST";
            httpWebRequest.Accept         = "application/json";
            httpWebRequest.ContentType    = "application/json";
            httpWebRequest.ContentLength  = byteArray.Length;
            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(qbosettings.qboconsumerkey, qbosettings.qboconsumersecret, qbokeys.accesstoken, qbokeys.accesstokensecret, httpWebRequest, null));

            try
            {
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);

                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                dataStream                      = httpWebResponse.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                responseFromServer  = reader.ReadToEnd();

                postresponse.StatusCode = (int)httpWebResponse.StatusCode;
                JsonConvert.PopulateObject(responseFromServer, postresponse.JSONResponse);

                reader.Close();
                dataStream.Close();
                httpWebResponse.Close();
            }
            catch (WebException e)
            {
                if (e.Response != null) {
                    using (var errorResponse = (HttpWebResponse)e.Response) {
                        postresponse.StatusCode = (int)errorResponse.StatusCode;
                        using (var reader = new StreamReader(errorResponse.GetResponseStream())) {
                            responseFromServer = reader.ReadToEnd();
                            LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, exportbatchid, entity, responseFromServer, postStr, e.Message);
                            JsonConvert.PopulateObject(responseFromServer, postresponse.JSONResponse);
                        }
                    }
                }
            } 
            return postresponse;
        }
        //----------------------------------------------------------------------------------------------------
        private static string GetDevDefinedOAuthHeader(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, HttpWebRequest webRequest, string requestBody)
        {

            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey                 = consumerKey,
                SignatureMethod             = SignatureMethod.HmacSha1,
                ConsumerSecret              = consumerSecret,
                UseHeaderForOAuthParameters = true
            };

            //We already have OAuth tokens, so OAuth URIs below are not used - set to example.com
            OAuthSession oSession = new OAuthSession(consumerContext, "https://www.example.com", "https://www.example.com", "https://www.example.com");

            oSession.AccessToken = new TokenBase
            {
                Token       = accessToken,
                ConsumerKey = consumerKey,
                TokenSecret = accessTokenSecret
            };

            IConsumerRequest consumerRequest = oSession.Request();
            consumerRequest = ConsumerRequestExtensions.ForMethod(consumerRequest, webRequest.Method);
            if (requestBody != null)
            {
                consumerRequest = consumerRequest.Post().WithRawContentType(webRequest.ContentType).WithRawContent(System.Text.Encoding.ASCII.GetBytes(requestBody));
            }
            consumerRequest = ConsumerRequestExtensions.ForUri(consumerRequest, webRequest.RequestUri);
            consumerRequest = consumerRequest.SignWithToken();
            return consumerRequest.Context.GenerateOAuthParametersForHeader();
        }
        //----------------------------------------------------------------------------------------------------
        //====================================================================================================
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateCustomer(dynamic customer)
        {
            dynamic _customer = null;

            if (Customers.Count != 0)
            {
                for (int i = 0; i < Customers.Count; i++)
                {
                    if (Customers[i].DisplayName == customer)
                    {
                        _customer = Customers[i];
                        break;
                    }
                }
            }

            if (_customer == null)
            {
                string querystring           = customer.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from customer where fullyqualifiedname = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Customer != null)
                {
                    _customer = qboresponse.JSONResponse.QueryResponse.Customer[0];
                    Customers.Add(_customer);
                }
            }

            if (_customer == null)
            {
                dynamic newCustomer = new ExpandoObject(), customerinfo;
                QBOPostResponse qbopost;

                customerinfo = GetCustomerInfo(FwSqlConnection.RentalWorks, customer);

                string customername                         = customer;
                newCustomer.DisplayName                     = customername.Replace(":", "");
                newCustomer.BillAddr                        = new ExpandoObject();
                if (customerinfo.billtoadd == "CUSTOMER") {
                    newCustomer.BillAddr.Line1                  = customerinfo.add1;
                    newCustomer.BillAddr.Line2                  = customerinfo.add2;
                    newCustomer.BillAddr.City                   = customerinfo.city;
                    newCustomer.BillAddr.CountrySubDivisionCode = customerinfo.state;
                    newCustomer.BillAddr.PostalCode             = customerinfo.zip;
                    //newCustomer.BillAddr.Country                = customerinfo.country;
                } else if (customerinfo.billtoadd == "OTHER") {
                    newCustomer.BillAddr.Line1                  = customerinfo.billtoadd1;
                    newCustomer.BillAddr.Line2                  = customerinfo.billtoadd2;
                    newCustomer.BillAddr.City                   = customerinfo.billtocity;
                    newCustomer.BillAddr.CountrySubDivisionCode = customerinfo.billtostate;
                    newCustomer.BillAddr.PostalCode             = customerinfo.billtozip;
                    //newCustomer.BillAddr.Country                = customerinfo.billtocountry;
                }

                qbopost   = PostToQBO("customer", newCustomer);
                _customer = qbopost.JSONResponse.Customer;
                Customers.Add(_customer);
            }

            return _customer;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateClass(dynamic classname)
        {
            dynamic _class = null;

            if (Classes.Count != 0)
            {
                for (int i = 0; i < Classes.Count; i++)
                {
                    if (Classes[i].Name == classname)
                    {
                        _class = Classes[i];
                        break;
                    }
                }
            }

            if (_class == null)
            {
                string querystring           = classname.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from class where fullyqualifiedname = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Class != null)
                {
                    _class = qboresponse.JSONResponse.QueryResponse.Class[0];
                    Classes.Add(_class);
                }
            }

            if (_class == null)
            {
                dynamic newClass = new ExpandoObject();
                QBOPostResponse qbopost;

                newClass.Name = classname.Replace(":", "");

                qbopost = PostToQBO("class", newClass);
                _class  = qbopost.JSONResponse.Class;
                Classes.Add(_class);
            }

            return _class;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateTerm(dynamic payterms)
        {
            dynamic term = null;

            if (Terms.Count != 0)
            {
                for (int i = 0; i < Terms.Count; i++)
                {
                    if (Terms[i].Name == payterms)
                    {
                        term = Terms[i];
                        break;
                    }
                }
            }

            if (term == null)
            {
                string querystring           = payterms.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from term where name = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Term != null)
                {
                    term = qboresponse.JSONResponse.QueryResponse.Term[0];
                    Terms.Add(term);
                }
            }

            if (term == null)
            {
                dynamic newTerm = new ExpandoObject();
                QBOPostResponse qbopost;
                string duedays;

                duedays = GetPayTermsDueDate(FwSqlConnection.RentalWorks, payterms);

                newTerm.Name    = payterms;
                newTerm.Type    = "STANDARD";
                newTerm.DueDays = duedays;

                qbopost = PostToQBO("term", newTerm);
                term    = qbopost.JSONResponse.Term;
                Terms.Add(term);
            }

            return term;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryTaxCode(dynamic invoice)
        {
            dynamic taxcode = null;

            if (TaxItems.Count != 0)
            {
                for (int i = 0; i < TaxItems.Count; i++)
                {
                    if (TaxItems[i].Name == invoice.taxitemcode)
                    {
                        taxcode = TaxItems[i];
                        break;
                    }
                }
            }

            if (taxcode == null)
            {
                string querystring           = invoice.taxitemcode.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from taxcode where name = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.TaxCode != null)
                {
                    taxcode = qboresponse.JSONResponse.QueryResponse.TaxCode[0];
                    TaxItems.Add(taxcode);
                }
            }

            return taxcode;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateTaxCode(dynamic invoice)
        {
            dynamic taxcode = null;

            taxcode = QueryTaxCode(invoice);

            if (taxcode == null)
            {
                dynamic newTaxService = new ExpandoObject(), taxrate, taxagency;
                QBOPostResponse qbopost;

                newTaxService.TaxCode = invoice.taxitemcode;

                taxrate                      = QueryTaxRate(invoice);
                newTaxService.TaxRateDetails = new List<dynamic>();
                dynamic rate                 = new ExpandoObject();
                if (taxrate != null)
                {
                    rate.TaxRateId = taxrate.Id.Value;
                    newTaxService.TaxRateDetails.Add(rate);
                }
                else
                {
                    dynamic rates    = GetTaxRatePercent(FwSqlConnection.RentalWorks, invoice.taxitemcode);
                    taxagency        = ValidateTaxAgency(invoice);
                    rate.TaxRateName = invoice.taxitemcode;
                    rate.RateValue   = Math.Max(Math.Max(rates.rentaltaxrate1, rates.salestaxrate1), rates.labortaxrate1).ToString();
                    rate.TaxAgencyId = taxagency.Id.Value;
                    newTaxService.TaxRateDetails.Add(rate);
                }

                qbopost = PostToQBO("taxservice/taxcode", newTaxService);
                taxcode = qbopost.JSONResponse.TaxCode;
                taxcode = QueryTaxCode(invoice);
                TaxItems.Add(taxcode);
            }

            return taxcode;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateTaxAgency(dynamic invoice)
        {
            dynamic taxagency = null;

            if (TaxAgencies.Count != 0)
            {
                for (int i = 0; i < TaxAgencies.Count; i++)
                {
                    if (TaxAgencies[i].Name == invoice.taxvendor)
                    {
                        taxagency = TaxAgencies[i];
                        break;
                    }
                }
            }

            if (taxagency == null)
            {
                string querystring           = invoice.taxvendor.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from taxagency where name = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.TaxAgency != null)
                {
                    taxagency = qboresponse.JSONResponse.QueryResponse.TaxAgency[0];
                    TaxAgencies.Add(taxagency);
                }
            }

            if (taxagency == null)
            {
                dynamic newTaxAgency = new ExpandoObject();
                QBOPostResponse qbopost;

                newTaxAgency.DisplayName = invoice.taxvendor;

                qbopost   = PostToQBO("taxagency", newTaxAgency);
                taxagency = qbopost.JSONResponse.TaxAgency;
                TaxAgencies.Add(taxagency);
            }

            return taxagency;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryTaxRate(dynamic invoice)
        {
            dynamic taxrate = null;

            if (TaxRates.Count != 0)
            {
                for (int i = 0; i < TaxRates.Count; i++)
                {
                    if (TaxRates[i].Name == invoice.taxitemcode)
                    {
                        taxrate = TaxRates[i];
                        break;
                    }
                }
            }

            if (taxrate == null)
            {
                string querystring           = invoice.taxitemcode.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from taxrate where name = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.TaxRate != null)
                {
                    taxrate = qboresponse.JSONResponse.QueryResponse.TaxRate[0];
                    TaxRates.Add(taxrate);
                }
            }

            return taxrate;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateItem(dynamic item)
        {
            dynamic _item = null;

            if (Items.Count != 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Name == item.description)
                    {
                        _item = Items[i];
                        break;
                    }
                }
            }

            if (_item == null)
            {
                string queryname             = item.description.Replace(":", "").Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from item where name = '" + queryname + "'");
                if (qboresponse.JSONResponse.QueryResponse.Item != null)
                {
                    _item = qboresponse.JSONResponse.QueryResponse.Item[0];
                    Items.Add(_item);
                }
            }

            if (_item == null)
            {
                dynamic newItem = new ExpandoObject();
                QBOPostResponse qbopost;

                string itemdescription          = (item.description != "") ? item.description : "No Description";
                newItem.Name                    = itemdescription.Replace(":", "");
                newItem.Type                    = "Service";
                newItem.IncomeAccountRef        = new ExpandoObject();
                newItem.IncomeAccountRef.value  = ValidateAccount(item.incomeaccountid, "", "Income").Id.Value;
                newItem.ExpenseAccountRef       = new ExpandoObject();
                newItem.ExpenseAccountRef.value = ValidateAccount(item.expenseaccountid, "", "Expense").Id.Value;

                qbopost = PostToQBO("item", newItem);
                _item   = qbopost.JSONResponse.Item;
                Items.Add(_item);
            }

            return _item;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateAccount(string accountid, string accountname, string accounttype)
        {
            dynamic account     = null;
            dynamic accountinfo = new ExpandoObject();

            if ((accountid != "") && (accountname == ""))
            {
                accountinfo = GetAccountInfo(FwSqlConnection.RentalWorks, accountid);
            }
            else
            {
                accountinfo.glacctdesc = accountname;
            }

            if (Accounts.Count != 0)
            {
                for (int i = 0; i < Accounts.Count; i++)
                {
                    if (Accounts[i].FullyQualifiedName == accountinfo.glacctdesc)
                    {
                        account = Accounts[i];
                        break;
                    }
                }
            }

            if (account == null)
            {
                string queryname             = accountinfo.glacctdesc.Replace(":", "").Replace("\"", "").Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from account where FullyQualifiedName = '" + queryname + "'");
                if (qboresponse.JSONResponse.QueryResponse.Account != null)
                {
                    account = qboresponse.JSONResponse.QueryResponse.Account[0];
                    Accounts.Add(account);
                }
            }

            if (account == null)
            {
                dynamic newAccount = new ExpandoObject();
                QBOPostResponse qbopost;

                newAccount.Name = accountinfo.glacctdesc.Replace(":", "").Replace("\"", "");

                if (accounttype == "Expense")
                {
                    newAccount.AccountSubType = "EquipmentRentalCos";
                }
                else if (accounttype == "Income")
                {
                    newAccount.AccountSubType = "SalesOfProductIncome";
                }
                else if (accounttype == "Liability")
                {
                    newAccount.AccountSubType = "OtherCurrentLiabilities";
                }

                qbopost = PostToQBO("account", newAccount);
                account = qbopost.JSONResponse.Account;
                Accounts.Add(account);
            }

            return account;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateVendor(dynamic vendor)
        {
            dynamic _vendor = null;

            if (Vendors.Count != 0)
            {
                for (int i = 0; i < Vendors.Count; i++)
                {
                    if (Vendors[i].DisplayName == vendor)
                    {
                        _vendor = Vendors[i];
                        break;
                    }
                }
            }

            if (_vendor == null)
            {
                string querystring           = vendor.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from vendor where displayname = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Vendor != null)
                {
                    _vendor = qboresponse.JSONResponse.QueryResponse.Vendor[0];
                    Vendors.Add(_vendor);
                }
            }

            if (_vendor == null)
            {
                dynamic newVendor = new ExpandoObject(), vendorinfo;
                QBOPostResponse qbopost;

                vendorinfo = GetVendorInfo(FwSqlConnection.RentalWorks, vendor);

                newVendor.DisplayName                     = vendor.Replace(":", "");
                newVendor.BillAddr                        = new ExpandoObject();
                newVendor.BillAddr.Line1                  = vendorinfo.add1;
                newVendor.BillAddr.Line2                  = vendorinfo.add2;
                newVendor.BillAddr.City                   = vendorinfo.city;
                newVendor.BillAddr.CountrySubDivisionCode = vendorinfo.state;
                newVendor.BillAddr.PostalCode             = vendorinfo.zip;
                //newVendor.BillAddr.Country                = vendorinfo.remitcountry;
                newVendor.PrimaryPhone                    = new ExpandoObject();
                newVendor.PrimaryPhone.FreeFormNumber     = vendorinfo.phone;

                qbopost = PostToQBO("vendor", newVendor);
                _vendor = qbopost.JSONResponse.Vendor;
                Vendors.Add(_vendor);
            }

            return _vendor;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidatePaymentMethod(dynamic paymentmethod)
        {
            dynamic _paymentmethod = null;

            if (PaymentMethods.Count != 0)
            {
                for (int i = 0; i < PaymentMethods.Count; i++)
                {
                    if (PaymentMethods[i].Name == paymentmethod)
                    {
                        _paymentmethod = PaymentMethods[i];
                        break;
                    }
                }
            }

            if (_paymentmethod == null)
            {
                string querystring           = paymentmethod.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from paymentmethod where name = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.PaymentMethod != null)
                {
                    _paymentmethod = qboresponse.JSONResponse.QueryResponse.PaymentMethod[0];
                    PaymentMethods.Add(_paymentmethod);
                }
            }

            if (_paymentmethod == null)
            {
                dynamic newPaymentMethod = new ExpandoObject();
                QBOPostResponse qbopost;

                newPaymentMethod.Name = paymentmethod;

                qbopost        = PostToQBO("paymentmethod", newPaymentMethod);
                _paymentmethod = qbopost.JSONResponse.Name;
                PaymentMethods.Add(_paymentmethod);
            }

            return _paymentmethod;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateInvoice(dynamic invoice, ExportInvoicesToQBOReturn result)
        {
            dynamic _invoice = null;

            _invoice = QueryInvoice(invoice.invoiceno);

            if (_invoice != null)
            {
                result.addInvoice(invoice.invoiceno, "Already exported to Quickbooks");
            }

            if (_invoice == null)
            {
                _invoice = PostInvoice(invoice);

                result.addInvoice(invoice.invoiceno, "Exported to Quickbooks");
            }

            return _invoice;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryInvoice(dynamic invoiceno)
        {
            dynamic invoice = null;

            if (Invoices.Count != 0)
            {
                for (int i = 0; i < Invoices.Count; i++)
                {
                    if (Invoices[i].DocNumber == invoiceno)
                    {
                        invoice = Invoices[i];
                        break;
                    }
                }
            }

            if (invoice == null)
            {
                string querystring           = invoiceno.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from invoice where docnumber = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Invoice != null)
                {
                    invoice = qboresponse.JSONResponse.QueryResponse.Invoice[0];
                    Invoices.Add(invoice);
                }
            }

            return invoice;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic PostInvoice(dynamic invoice)
        {
            dynamic _invoice = null;

            dynamic newInvoice = new ExpandoObject();
            QBOPostResponse qbopost;
            string TxnTaxCodeRefValue = string.Empty;

            newInvoice.DocNumber                       = invoice.invoiceno;
            newInvoice.TxnDate                         = FwConvert.ToDateTime(invoice.invoicedate).ToString("yyyy-MM-dd");
            newInvoice.CustomerRef                     = new ExpandoObject();
            newInvoice.CustomerRef.value               = ValidateCustomer(invoice.customer).Id.Value;
            newInvoice.DueDate                         = FwConvert.ToDateTime(invoice.invoiceduedate).ToString("yyyy-MM-dd");
            newInvoice.BillAddr                        = new ExpandoObject();
            newInvoice.BillAddr.Line1                  = invoice.billtoadd1;
            newInvoice.BillAddr.Line2                  = invoice.billtoadd2;
            newInvoice.BillAddr.City                   = invoice.billtocity;
            newInvoice.BillAddr.CountrySubDivisionCode = invoice.billtostate;
            newInvoice.BillAddr.PostalCode             = invoice.billtozip;
            newInvoice.BillAddr.Country                = invoice.billtocountry;

            if (invoice.invoiceclass != "")
            {
                newInvoice.ClassRef       = new ExpandoObject();
                newInvoice.ClassRef.value = ValidateClass(invoice.invoiceclass).Id.Value;
            }

            if (invoice.payterms != "")
            {
                newInvoice.SalesTermRef       = new ExpandoObject();
                newInvoice.SalesTermRef.value = ValidateTerm(invoice.payterms).Id.Value;
            }

            if (invoice.taxitemcode != "")
            {
                TxnTaxCodeRefValue = ValidateTaxCode(invoice).Id.Value;

                newInvoice.TxnTaxDetail                     = new ExpandoObject();
                newInvoice.TxnTaxDetail.TxnTaxCodeRef       = new ExpandoObject();
                newInvoice.TxnTaxDetail.TxnTaxCodeRef.value = TxnTaxCodeRefValue;
            }

            if (invoice.printnotes != "")
            {
                newInvoice.CustomerMemo       = new ExpandoObject();
                newInvoice.CustomerMemo.value = invoice.printnotes;
            }

            if (invoice.chgbatchno != "")
            {
                newInvoice.PrivateNote = invoice.chgbatchno;
            }

            newInvoice.Line = new List<dynamic>();
            for (int j = 0; j < invoice.items.Count; j++)
            {
                dynamic newItem = new ExpandoObject();
                string  taxCodeRefValue = string.Empty;
                if (invoice.taxcountry == "U")
                {
                    taxCodeRefValue = (invoice.items[j].taxable == "T") ? "TAX" : "NON";
                }
                //else if (invoice.taxcountry == "C")
                else if ((invoice.taxcountry == "C") || (invoice.taxcountry == "UK"))  //jh 08/16/2017 CAS-21271-VIGE
                {
                    taxCodeRefValue = TxnTaxCodeRefValue;
                }

                newItem.Id                                   = j+1;
                newItem.DetailType                           = "SalesItemLineDetail";
                newItem.SalesItemLineDetail                  = new ExpandoObject();
                newItem.SalesItemLineDetail.ItemRef          = new ExpandoObject();
                newItem.SalesItemLineDetail.ItemRef.value    = ValidateItem(invoice.items[j]).Id.Value;
                newItem.SalesItemLineDetail.Qty              = invoice.items[j].qty;
                newItem.SalesItemLineDetail.UnitPrice        = (invoice.items[j].qty != 0) ? (invoice.items[j].linetotal / invoice.items[j].qty) : "0";
                newItem.SalesItemLineDetail.TaxCodeRef       = new ExpandoObject();
                newItem.SalesItemLineDetail.TaxCodeRef.value = taxCodeRefValue;
                newItem.Amount                               = invoice.items[j].linetotal;

                newInvoice.Line.Add(newItem);
            }

            qbopost  = PostToQBO("invoice", newInvoice);
            _invoice = qbopost.JSONResponse.Invoice;
            Invoices.Add(_invoice);

            //jh 06/22/2017 CAS-20810-L6T5
            //if (invoice.taxcountry == "C") // if Canada
            //if ((invoice.taxcountry == "C") || (invoice.taxcountry == "UK"))  //jh 08/16/2017 CAS-21271-VIGE Canada or UK
            //jh 11/16/2017 CAS-21973-PUHU (needs to do this check for all invoices)
            //{
            decimal taxAmount = _invoice.TxnTaxDetail.TotalTax; // determine the tax amount that QBO calculated for this invoice
            if (invoice.invoicetax != taxAmount)                // if RW tax amuont is different than QBO tax for this invoice, force the RW tax amount up to QBO
            {
                dynamic newInvoice2 = new ExpandoObject();
                dynamic _invoice2 = new ExpandoObject();
                newInvoice2 = _invoice;
                newInvoice2.TxnTaxDetail.TotalTax = invoice.invoicetax; // need to change it here
                newInvoice2.TxnTaxDetail.TaxLine[0].Amount = invoice.invoicetax;  // and here, too (both)
                qbopost = PostToQBO("invoice", newInvoice2);
                _invoice2 = qbopost.JSONResponse.Invoice;
            }
            //}

            return _invoice;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateCreditMemo(dynamic invoice, ExportInvoicesToQBOReturn result)
        {
            dynamic creditmemo = null;

            creditmemo = QueryCreditMemo(invoice.invoiceno);

            if (creditmemo != null)
            {
                result.addInvoice(invoice.invoiceno, "Already exported to Quickbooks");
            }

            if (creditmemo == null)
            {
                creditmemo = PostCreditMemo(invoice);

                result.addInvoice(invoice.invoiceno, "Exported to Quickbooks");
            }

            return creditmemo;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryCreditMemo(dynamic invoiceno)
        {
            dynamic creditmemo = null;

            if (CreditMemos.Count != 0)
            {
                for (int i = 0; i < CreditMemos.Count; i++)
                {
                    if (CreditMemos[i].DocNumber == invoiceno)
                    {
                        creditmemo = CreditMemos[i];
                        break;
                    }
                }
            }

            if (creditmemo == null)
            {
                string querystring           = invoiceno.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from creditmemo where docnumber = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.CreditMemo != null)
                {
                    creditmemo = qboresponse.JSONResponse.QueryResponse.CreditMemo[0];
                    CreditMemos.Add(creditmemo);
                }
            }

            return creditmemo;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic PostCreditMemo(dynamic invoice)
        {
            dynamic creditmemo = null;
            dynamic newCreditMemo = new ExpandoObject();
            QBOPostResponse qbopost;
            string TxnTaxCodeRefValue = string.Empty;

            newCreditMemo.DocNumber                       = invoice.invoiceno;
            newCreditMemo.TxnDate                         = FwConvert.ToDateTime(invoice.invoicedate).ToString("yyyy-MM-dd");
            newCreditMemo.CustomerRef                     = new ExpandoObject();
            newCreditMemo.CustomerRef.value               = ValidateCustomer(invoice.customer).Id.Value;
            newCreditMemo.DueDate                         = FwConvert.ToDateTime(invoice.invoiceduedate).ToString("yyyy-MM-dd");
            newCreditMemo.BillAddr                        = new ExpandoObject();
            newCreditMemo.BillAddr.Line1                  = invoice.billtoadd1;
            newCreditMemo.BillAddr.Line2                  = invoice.billtoadd2;
            newCreditMemo.BillAddr.City                   = invoice.billtocity;
            newCreditMemo.BillAddr.CountrySubDivisionCode = invoice.billtostate;
            newCreditMemo.BillAddr.PostalCode             = invoice.billtozip;
            newCreditMemo.BillAddr.Country                = invoice.billtocountry;

            if (invoice.invoiceclass != "")
            {
                newCreditMemo.ClassRef       = new ExpandoObject();
                newCreditMemo.ClassRef.value = ValidateClass(invoice.invoiceclass).Id.Value;
            }

            if (invoice.payterms != "")
            {
                newCreditMemo.SalesTermRef       = new ExpandoObject();
                newCreditMemo.SalesTermRef.value = ValidateTerm(invoice.payterms).Id.Value;
            }

            if (invoice.taxitemcode != "")
            {

                TxnTaxCodeRefValue = ValidateTaxCode(invoice).Id.Value;

                newCreditMemo.TxnTaxDetail                     = new ExpandoObject();
                newCreditMemo.TxnTaxDetail.TxnTaxCodeRef       = new ExpandoObject();
                //newCreditMemo.TxnTaxDetail.TxnTaxCodeRef.value = ValidateTaxCode(invoice).Id.Value;
                newCreditMemo.TxnTaxDetail.TxnTaxCodeRef.value = TxnTaxCodeRefValue;  //jh 07/05/2017 CAS-20755-XOPL
            }

            if (invoice.printnotes != "")
            {
                newCreditMemo.CustomerMemo       = new ExpandoObject();
                newCreditMemo.CustomerMemo.value = invoice.printnotes.Replace("'", @"\'");
            }

            if (invoice.chgbatchno != "")
            {
                newCreditMemo.PrivateNote = invoice.chgbatchno;
            }

            newCreditMemo.Line = new List<dynamic>();
            for (int j = 0; j < invoice.items.Count; j++)
            {
                dynamic newItem = new ExpandoObject();

                //jh 07/05/2017 CAS-20755-XOPL
                string taxCodeRefValue = string.Empty;
                if (invoice.taxcountry == "U")
                {
                    taxCodeRefValue = (invoice.items[j].taxable == "T") ? "TAX" : "NON";
                }
                //else if (invoice.taxcountry == "C")
                else if ((invoice.taxcountry == "C") || (invoice.taxcountry == "UK"))  //jh 08/16/2017 CAS-21271-VIGE
                {
                    taxCodeRefValue = TxnTaxCodeRefValue;
                }


                newItem.Id                                   = j+1;
                newItem.DetailType                           = "SalesItemLineDetail";
                newItem.SalesItemLineDetail                  = new ExpandoObject();
                newItem.SalesItemLineDetail.ItemRef          = new ExpandoObject();
                newItem.SalesItemLineDetail.ItemRef.value    = ValidateItem(invoice.items[j]).Id.Value;
                newItem.SalesItemLineDetail.TaxCodeRef       = new ExpandoObject();
                //newItem.SalesItemLineDetail.TaxCodeRef.value = (invoice.items[j].taxable == "T") ? "TAX" : "NON";
                newItem.SalesItemLineDetail.TaxCodeRef.value = taxCodeRefValue;  //jh 07/05/2017 CAS-20755-XOPL
                newItem.SalesItemLineDetail.Qty              = invoice.items[j].qty;
                newItem.Amount                               = invoice.items[j].linetotal*(-1);

                newCreditMemo.Line.Add(newItem);
            }

            qbopost    = PostToQBO("creditmemo", newCreditMemo);
            creditmemo = qbopost.JSONResponse.CreditMemo;
            CreditMemos.Add(creditmemo);

            //jh 07/05/2017 CAS-20810-L6T5
            //if (invoice.taxcountry == "C") // if Canada
            //if ((invoice.taxcountry == "C") || (invoice.taxcountry == "UK"))  //jh 08/16/2017 CAS-21271-VIGE Canada or UK 
            //jh 11/16/2017 CAS-21973-PUHU (needs to do this check for all invoices)
            //{
            decimal taxAmount = creditmemo.TxnTaxDetail.TotalTax;    // determine the tax amount that QBO calculated for this invoice
                                                                     //if (invoice.invoicetax != taxAmount) // if RW tax amuont is different than QBO tax for this invoice, force the RW tax amount up to QBO
            if (((-1) * invoice.invoicetax) != taxAmount) // jh 11/16/2017 CAS-21999-UITX
            {
                dynamic newCreditMemo2 = new ExpandoObject();
                dynamic creditmemo2 = new ExpandoObject();
                newCreditMemo2 = creditmemo;
                //newCreditMemo2.TxnTaxDetail.TotalTax          = invoice.invoicetax; // need to change it here
                //newCreditMemo2.TxnTaxDetail.TaxLine[0].Amount = invoice.invoicetax;  // and here, too (both)
                newCreditMemo2.TxnTaxDetail.TotalTax = ((-1) * invoice.invoicetax); // jh 11/16/2017 CAS-21999-UITX
                newCreditMemo2.TxnTaxDetail.TaxLine[0].Amount = ((-1) * invoice.invoicetax);  // jh 11/16/2017 CAS-21999-UITX
                qbopost = PostToQBO("creditmemo", newCreditMemo2);
                creditmemo2 = qbopost.JSONResponse.CreditMemo;
            }
            //}

            return creditmemo;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateVendorInvoice(dynamic vendorinvoice, ExportVendorInvoicesToQBOReturn result)
        {
            dynamic _bill = null;

            _bill = QueryVendorInvoice(vendorinvoice.invno);

            if (_bill != null)
            {
                result.addVendorInvoice(vendorinvoice.invno, "Already exported to Quickbooks");
            }

            if (_bill == null)
            {
                _bill = PostVendorInvoice(vendorinvoice);

                result.addVendorInvoice(vendorinvoice.invno, "Exported to Quickbooks");
            }

            return _bill;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryVendorInvoice(dynamic invno)
        {
            dynamic bill = null;

            if (VendorInvoices.Count != 0)
            {
                for (int i = 0; i < VendorInvoices.Count; i++)
                {
                    if (VendorInvoices[i].DocNumber == invno)
                    {
                        bill = VendorInvoices[i];
                        break;
                    }
                }
            }

            if (bill == null)
            {
                string querystring           = invno.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from bill where docnumber = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Bill != null)
                {
                    bill = qboresponse.JSONResponse.QueryResponse.Bill[0];
                    VendorInvoices.Add(bill);
                }
            }

            return bill;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic PostVendorInvoice(dynamic vendorinvoice)
        {
            dynamic bill    = null;
            dynamic newBill = new ExpandoObject();
            QBOPostResponse qbopost;

            newBill.VendorRef                     = new ExpandoObject();
            newBill.VendorRef.value               = ValidateVendor(vendorinvoice.vendor).Id.Value;
            newBill.TxnDate                       = FwConvert.ToDateTime(vendorinvoice.invdate).ToString("yyyy-MM-dd");
            newBill.DueDate                       = FwConvert.ToDateTime(vendorinvoice.invoiceduedate).ToString("yyyy-MM-dd");
            newBill.DocNumber                     = vendorinvoice.invno;

            if (vendorinvoice.payterms != "")
            {
                newBill.SalesTermRef       = new ExpandoObject();
                newBill.SalesTermRef.value = ValidateTerm(vendorinvoice.payterms).Id.Value;
            }

            //2017/02/01 MY: Not required as QBO does not use any tax information on vendor invoices.
            //if (vendorinvoice.taxitemcode != "")
            //{
            //    newBill.TxnTaxDetail                     = new ExpandoObject();
            //    newBill.TxnTaxDetail.TxnTaxCodeRef       = new ExpandoObject();
            //    newBill.TxnTaxDetail.TxnTaxCodeRef.value = ValidateTaxCode(vendorinvoice).Id.Value;
            //}

            newBill.Line = new List<dynamic>();
            for (int j = 0; j < vendorinvoice.items.Count; j++)
            {
                dynamic newItem = new ExpandoObject();

                newItem.Id                                          = j+1;
                newItem.DetailType                                  = "ItemBasedExpenseLineDetail";
                newItem.ItemBasedExpenseLineDetail                  = new ExpandoObject();
                newItem.ItemBasedExpenseLineDetail.ItemRef          = new ExpandoObject();
                newItem.ItemBasedExpenseLineDetail.ItemRef.value    = ValidateItem(vendorinvoice.items[j]).Id.Value;
                newItem.ItemBasedExpenseLineDetail.Qty              = vendorinvoice.items[j].qty;
                newItem.ItemBasedExpenseLineDetail.UnitPrice        = (vendorinvoice.items[j].taxable == "T") ? (vendorinvoice.items[j].extendedwtax / vendorinvoice.items[j].qty) : vendorinvoice.items[j].cost;
                newItem.ItemBasedExpenseLineDetail.TaxCodeRef       = new ExpandoObject();
                newItem.ItemBasedExpenseLineDetail.TaxCodeRef.value = (vendorinvoice.items[j].taxable == "T") ? "TAX" : "NON";
                newItem.Amount                                      = (vendorinvoice.items[j].taxable == "T") ? vendorinvoice.items[j].extendedwtax : vendorinvoice.items[j].linetotal;

                newBill.Line.Add(newItem);
            }

            qbopost = PostToQBO("bill", newBill);
            bill    = qbopost.JSONResponse.Bill;
            VendorInvoices.Add(bill);

            return bill;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic ValidateVendorCredit(dynamic vendorinvoice, ExportVendorInvoicesToQBOReturn result)
        {
            dynamic _vendorcredit = null;

            _vendorcredit = QueryVendorCredit(vendorinvoice.invno);

            if (_vendorcredit != null)
            {
                result.addVendorInvoice(vendorinvoice.invno, "Already exported to Quickbooks");
            }

            if (_vendorcredit == null)
            {
                _vendorcredit = PostVendorCredit(vendorinvoice);

                result.addVendorInvoice(vendorinvoice.invno, "Exported to Quickbooks");
            }

            return _vendorcredit;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryVendorCredit(dynamic invno)
        {
            dynamic vendorcredit = null;

            if (VendorCredit.Count != 0)
            {
                for (int i = 0; i < VendorCredit.Count; i++)
                {
                    if (VendorCredit[i].DocNumber == invno)
                    {
                        vendorcredit = VendorCredit[i];
                        break;
                    }
                }
            }

            if (vendorcredit == null)
            {
                string querystring           = invno.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from vendorcredit where docnumber = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.VendorCredit != null)
                {
                    vendorcredit = qboresponse.JSONResponse.QueryResponse.VendorCredit[0];
                    VendorCredit.Add(vendorcredit);
                }
            }

            return vendorcredit;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic PostVendorCredit(dynamic vendorinvoice)
        {
            dynamic vendorcredit   = null;
            dynamic newVendorCredit = new ExpandoObject();
            QBOPostResponse qbopost;

            newVendorCredit.VendorRef                     = new ExpandoObject();
            newVendorCredit.VendorRef.value               = ValidateVendor(vendorinvoice.vendor).Id.Value;
            newVendorCredit.TxnDate                       = FwConvert.ToDateTime(vendorinvoice.invdate).ToString("yyyy-MM-dd");
            newVendorCredit.DocNumber                     = vendorinvoice.invno;

            newVendorCredit.Line = new List<dynamic>();
            for (int j = 0; j < vendorinvoice.items.Count; j++)
            {
                dynamic newItem = new ExpandoObject();

                newItem.Id                                          = j+1;
                newItem.DetailType                                  = "ItemBasedExpenseLineDetail";
                newItem.ItemBasedExpenseLineDetail                  = new ExpandoObject();
                newItem.ItemBasedExpenseLineDetail.ItemRef          = new ExpandoObject();
                newItem.ItemBasedExpenseLineDetail.ItemRef.value    = ValidateItem(vendorinvoice.items[j]).Id.Value;
                newItem.ItemBasedExpenseLineDetail.Qty              = vendorinvoice.items[j].qty;
                newItem.ItemBasedExpenseLineDetail.TaxCodeRef       = new ExpandoObject();
                newItem.ItemBasedExpenseLineDetail.TaxCodeRef.value = (vendorinvoice.items[j].taxable == "T") ? "TAX" : "NON";
                newItem.Amount                                      = ((vendorinvoice.items[j].taxable == "T") ? vendorinvoice.items[j].extendedwtax : vendorinvoice.items[j].linetotal) * (-1);

                newVendorCredit.Line.Add(newItem);
            }

            qbopost      = PostToQBO("vendorcredit", newVendorCredit);
            vendorcredit = qbopost.JSONResponse.VendorCredit;
            VendorCredit.Add(vendorcredit);

            return vendorcredit;
        }
        //----------------------------------------------------------------------------------------------------
        private static void ValidateReceipt(dynamic payment, ExportReceiptsToQBOReturn result)
        {
            dynamic _payment = null;

            _payment = QueryReceipt(payment.externalid);

            if (_payment != null)
            {
                result.addReceipt(_payment.Id.Value, "Already exported to Quickbooks");
            }

            if (_payment == null)
            {
                _payment = PostReceipt(payment);

                if (_payment.status == 0)
                {
                    result.addReceipt(_payment.data.Id.Value, "Exported to Quickbooks");
                    UpdateARExternalId(FwSqlConnection.RentalWorks, payment.arid, _payment.data.Id.Value);
                }
                else if (_payment.status == RECEIPT_INVOICE_ERROR)
                {
                    result.addReceipt("", _payment.message);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic QueryReceipt(dynamic externalid)
        {
            dynamic payment = null;

            if ((Receipts.Count != 0) && (externalid != ""))
            {
                for (int i = 0; i < Receipts.Count; i++)
                {
                    if (Receipts[i].Id == externalid)
                    {
                        payment = Receipts[i];
                        break;
                    }
                }
            }

            if ((payment == null) && (externalid != ""))
            {
                string querystring           = externalid.Replace("'", @"\'");
                QBOQueryResponse qboresponse = QueryQBO("select * from payment where id = '" + querystring + "'");
                if (qboresponse.JSONResponse.QueryResponse.Payment != null)
                {
                    payment = qboresponse.JSONResponse.QueryResponse.Payment[0];
                    Receipts.Add(payment);
                }
            }

            return payment;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic PostReceipt(dynamic payment)
        {
            dynamic paymentreturn = new ExpandoObject();
            dynamic newPayment    = new ExpandoObject();
            QBOPostResponse qbopost;

            paymentreturn.status = 0;

            newPayment.CustomerRef       = new ExpandoObject();
            newPayment.CustomerRef.value = ValidateCustomer(payment.customer).Id.Value;
            newPayment.TxnDate           = FwConvert.ToDateTime(payment.ardate).ToString("yyyy-MM-dd");
            newPayment.PaymentRefNum     = payment.checkno;
            newPayment.TotalAmt          = payment.pmtamt;

            if (payment.paymentmethod != "")
            {
                newPayment.PaymentMethodRef       = new ExpandoObject();
                newPayment.PaymentMethodRef.value = ValidatePaymentMethod(payment.paymentmethod).Id.Value;
            }

            if (payment.invoicespaid.Count == 0)
            {
                newPayment.DepositToAccountRef       = new ExpandoObject();
                newPayment.DepositToAccountRef.value = ValidateAccount("", payment.depositglacctdesc, "Liability").Id.Value;
            }
            else
            {
                newPayment.Line = new List<dynamic>();
                for (int j = 0; j < payment.invoicespaid.Count; j++)
                {
                    try
                    {
                        dynamic newInvoice   = new ExpandoObject();
                        dynamic newLinkedTxn = new ExpandoObject();

                        newInvoice.Amount    = payment.invoicespaid[j].amount;
                        newInvoice.LinkedTxn = new List<dynamic>();
                        if (payment.invoicespaid[j].fromcredit == true)
                        {
                            dynamic creditmemo   = QueryCreditMemo(payment.invoicespaid[j].invoiceno);
                            newLinkedTxn.TxnId   = creditmemo.Id;
                            newLinkedTxn.TxnType = "CreditMemo";
                            newInvoice.LinkedTxn.Add(newLinkedTxn);
                        }
                        else
                        {
                            dynamic invoice      = QueryInvoice(payment.invoicespaid[j].invoiceno);
                            newLinkedTxn.TxnId   = invoice.Id;
                            newLinkedTxn.TxnType = "Invoice";
                            newInvoice.LinkedTxn.Add(newLinkedTxn);
                        }

                        newPayment.Line.Add(newInvoice);
                    }
                    catch //(Exception ex)
                    {
                        paymentreturn.status  = RECEIPT_INVOICE_ERROR;
                        paymentreturn.message = "Invoice " + payment.invoicespaid[j].invoiceno + " does not exist in QBO.";
                    }
                }
                
            }

            if (paymentreturn.status == 0)
            {
                qbopost            = PostToQBO("payment", newPayment);
                paymentreturn.data = qbopost.JSONResponse.Payment;
                Receipts.Add(paymentreturn.data);
            }

            return paymentreturn;
        }
        //----------------------------------------------------------------------------------------------------
        //====================================================================================================
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetQBOSettings(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select qboconsumerkey, qboconsumersecret, qborequesttokenurl, qboaccesstokenurl, qboauthorizeurl, qbooauthurl, qbobaseurl");
            qry.Add("from chgbatchcontrol with (nolock)");
            result = qry.QueryToDynamicObject2();

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
            result = qry.QueryToDynamicObject2();

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
            result = sp.QueryToDynamicList2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic InvoiceView(FwSqlConnection conn, string invoiceid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            //qry.Add("select invoiceid, invoiceno, invoicedate, customer, billtoadd1, billtoadd2, billtocity, billtostate, billtozip, billtocountry, invoiceclass, pono, payterms, invoiceduedate, printnotes, taxitemcode, taxvendor, taxcountry, chgbatchno, invoicetotal");
            qry.Add("select invoiceid, invoiceno, invoicedate, customer, billtoadd1, billtoadd2, billtocity, billtostate, billtozip, billtocountry, invoiceclass, pono, payterms, invoiceduedate, printnotes, taxitemcode, taxvendor, taxcountry, chgbatchno, invoicetotal, invoicetax");  //jh 06/22/2017 CAS-20810-L6T5
            qry.Add("from invoiceview with (nolock)");
            qry.Add("where invoiceid = @invoiceid");
            qry.AddParameter("@invoiceid", invoiceid);
            result = qry.QueryToDynamicObject2();

            result.items = InvoiceItemView(conn, result.invoiceid);

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
            result = qry.QueryToDynamicList2();

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
            result = qry.QueryToDynamicObject2();

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
            result = qry.QueryToDynamicObject2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetCustomerInfo(FwSqlConnection conn, string customer)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from customer with (nolock)");
            qry.Add("where customer = @customer");
            qry.AddParameter("@customer", customer);
            result = qry.QueryToDynamicObject2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetVendorInfo(FwSqlConnection conn, string vendor)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from vendor with (nolock)");
            qry.Add("where vendor = @vendor");
            qry.AddParameter("@vendor", vendor);
            result = qry.QueryToDynamicObject2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static void LogChgBatchExportRecordError(FwSqlConnection conn, string chgbatchid, string responsevalue01, string responsevalue02, string responsevalue03, string msg)
        {
            FwSqlCommand qry;

            if (string.IsNullOrEmpty(chgbatchexportid))
            {
                chgbatchexportid = FwSqlData.GetNextId(FwSqlConnection.RentalWorks);
            }

            qry = new FwSqlCommand(conn);
            qry.Add("insert into chgbatchexportrecord (datestamp, chgbatchexportrecordid, recordtype,  chgbatchid,  chgbatchexportid,  responsevalue01,  responsevalue02,  responsevalue03,  msg)");
            qry.Add("                          values (getdate(), @nextid,                @recordtype, @chgbatchid, @chgbatchexportid, @responsevalue01, @responsevalue02, @responsevalue03, @msg)");
            qry.AddParameter("@nextid",           FwSqlData.GetNextId(FwSqlConnection.RentalWorks));
            qry.AddParameter("@recordtype",       "QBO Error");
            qry.AddParameter("@chgbatchid",       chgbatchid);
            qry.AddParameter("@chgbatchexportid", chgbatchexportid);
            qry.AddParameter("@responsevalue01",  responsevalue01);
            qry.AddParameter("@responsevalue02",  responsevalue02);
            qry.AddParameter("@responsevalue03",  responsevalue03);
            qry.AddParameter("@msg",              msg);
            qry.Execute();
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ExportVendorInvoices(FwSqlConnection conn, string batchno)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.exportvendorinvoices");
            sp.AddParameter("@chgbatchid", batchno);
            result = sp.QueryToDynamicList2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic VendorInvoiceView(FwSqlConnection conn, string vendorinvoiceid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select viv.vendorinvoiceid, viv.invno, viv.invdate, viv.vendor, viv.invoiceclass, viv.pono, viv.payterms, viv.invoiceduedate, viv.printnotes, viv.taxitemcode, viv.invoicetotal");
            qry.Add("  from vendorinvoiceview viv with (nolock)");
            qry.Add("where viv.vendorinvoiceid = @vendorinvoiceid");
            qry.AddParameter("@vendorinvoiceid ", vendorinvoiceid);
            result = qry.QueryToDynamicObject2();

            result.items = VendorInvoiceItemView(conn, result.vendorinvoiceid);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic VendorInvoiceItemView(FwSqlConnection conn, string vendorinvoiceid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from funcvendorinvoiceitemqbo(@vendorinvoiceid) viiv");
            qry.Add("order by viiv.itemorder, viiv.masterno");
            qry.AddParameter("@vendorinvoiceid", vendorinvoiceid);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetVendorInvoiceTax(FwSqlConnection conn, string vendorinvoiceid, string vendorinvoiceitemid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from dbo.funcgetvendorinvoicetax(@vendorinvoiceid, @vendorinvoiceitemid, 'F', '', 'T')");
            qry.AddParameter("@vendorinvoiceid",     vendorinvoiceid);
            qry.AddParameter("@vendorinvoiceitemid", vendorinvoiceitemid);
            result = qry.QueryToDynamicObject2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ExportRecepits(FwSqlConnection conn, string batchno, FwDateTime batchfrom, FwDateTime batchto, string locationid)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.exportreceipts");
            sp.AddParameter("@chgbatchid", batchno);
            sp.AddParameter("@fromdate",   batchfrom.GetSqlValue());
            sp.AddParameter("@todate",     batchto.GetSqlValue());
            sp.AddParameter("@locationid", locationid);
            result = sp.QueryToDynamicList2();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ReceiptView(FwSqlConnection conn, string arid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from dbo.funcarqbo(@arid)");
            qry.AddParameter("@arid", arid);
            result = qry.QueryToDynamicObject2();

            if (result.rectype == "P")
            {
                GetInvoicesPaid(conn, result);
            }
            else if (result.rectype == "D")
            {

            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetInvoicesPaid(FwSqlConnection conn, dynamic payment)
        {
            FwSqlCommand qry, qry2;
            string depositid;

            qry = new FwSqlCommand(conn);
            qry.Add("select depositid");
            qry.Add("  from ardepositpmt with (nolock)");
            qry.Add(" where paymentid =  @arid");
            qry.Add("   and applied   <> 0");
            qry.AddParameter("@arid", payment.arid);
            qry.Execute();
            depositid = qry.GetField("depositid").ToString().TrimEnd();

            qry2 = new FwSqlCommand(conn);
            qry2.Add("select i.invoiceno, arp.amount");
            qry2.Add("  from arpayment arp with (nolock) left outer join invoice i with (nolock) on (arp.invoiceid = i.invoiceid)");
            qry2.Add(" where arp.arid   =  @arid");
            qry2.Add("   and arp.amount <> 0");
            qry2.AddParameter("@arid", payment.arid);
            payment.invoicespaid = qry2.QueryToDynamicList2();

            for (int i = 0; i < payment.invoicespaid.Count; i++)
            {
                payment.invoicespaid[i].fromcredit = !string.IsNullOrEmpty(depositid);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static void UpdateARExternalId(FwSqlConnection conn, string arid, string externalid)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("update ar");
            qry.Add("   set externalid = @externalid");
            qry.Add(" where arid = @arid");
            qry.AddParameter("@arid",       arid);
            qry.AddParameter("@externalid", externalid);
            qry.Execute();
        }
        //----------------------------------------------------------------------------------------------------
    }
}