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

namespace RentalWorksWeb.Integration
{
    public class QBOIntegrationData
    {
        //---------------------------------------------------------------------------------------------
        public static dynamic qbokeys, qbosettings;
        public static List<dynamic> _Items                  = new List<dynamic>();
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
        //---------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------
        public static ExportInvoicesToQBOReturn ExportInvoicesToQBO(string batchno, string batchfrom, string batchto, string locationid, string usersid)
        {
            dynamic invoiceids, invoiceinfo;
            ExportInvoicesToQBOReturn result = new ExportInvoicesToQBOReturn();

            try
            {
                qbosettings     = GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys         = GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
                invoiceids      = ExportInvoices(FwSqlConnection.RentalWorks, batchno, batchfrom, batchto, locationid);

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
                LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, batchno, ex.Message, "", ex.StackTrace);
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------
        public static ExportReceiptsToQBOReturn ExportReceiptsToQBO(string batchno, string batchfrom, string batchto, string locationid)
        {
            dynamic arids, payment;
            ExportReceiptsToQBOReturn result = new ExportReceiptsToQBOReturn();

            try
            {
                qbosettings     = GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys         = GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
                arids           = ExportRecepits(FwSqlConnection.RentalWorks, batchno, batchfrom, batchto, locationid);

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
                LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, batchno, ex.Message, "", ex.StackTrace);
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------
        public static ExportVendorInvoicesToQBOReturn ExportVendorInvoicesToQBO(string batchno, string locationid)
        {
            dynamic vendorinvoiceids, vendorinvoiceinfo;
            ExportVendorInvoicesToQBOReturn result = new ExportVendorInvoicesToQBOReturn();

            try
            {
                qbosettings      = GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys          = GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
                vendorinvoiceids = ExportVendorInvoices(FwSqlConnection.RentalWorks, batchno);

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
                LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, batchno, ex.Message, "", ex.StackTrace);
            }

            return result;
        }
        //------------------------------------------------------------------------------
        private static dynamic QueryToJsonObject(string query)
        {
            dynamic obj = new ExpandoObject();
            JsonConvert.PopulateObject(Get(query), obj);
            return obj;
        }
        //------------------------------------------------------------------------------
        private static dynamic PostToJsonObject(string entity, string poststr)
        {
            dynamic obj = new ExpandoObject();
            JsonConvert.PopulateObject(Post(entity, poststr), obj);
            return obj;
        }
        //------------------------------------------------------------------------------
        private static string Get(string query)
        {
            string encodedQuery           = System.Net.WebUtility.UrlEncode(query);
            string uri                    = string.Format("{0}/company/{1}/query?query={2}", qbosettings.qbobaseurl, qbokeys.companyid, encodedQuery);
            HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
            string responseFromServer     = string.Empty;
            httpWebRequest.Method         = "GET";
            httpWebRequest.Accept         = "application/json";
            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(qbosettings.qboconsumerkey, qbosettings.qboconsumersecret, qbokeys.accesstoken, qbokeys.accesstokensecret, httpWebRequest, null));

            try
            {
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                using (Stream data = httpWebResponse.GetResponseStream())
                {
                    responseFromServer = new StreamReader(data).ReadToEnd();
                }
            }
            catch (Exception e)
            {
                responseFromServer = "ERROR: " + e.Message + "  ----  " + "Query: " + query;
            }
            return responseFromServer;
        }
        //------------------------------------------------------------------------------
        private static string Post(string entity, string postStr)
        {
            string uri                    = string.Format("{0}/company/{1}/{2}", qbosettings.qbobaseurl, qbokeys.companyid, entity);
            HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
            string responseFromServer     = string.Empty;
            httpWebRequest.Method         = "POST";
            httpWebRequest.Accept         = "application/json";
            httpWebRequest.ContentType    = "application/json";

            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(qbosettings.qboconsumerkey, qbosettings.qboconsumersecret, qbokeys.accesstoken, qbokeys.accesstokensecret, httpWebRequest, null));

            string decodedPostStr        = HttpUtility.HtmlDecode(postStr);
            byte[] byteArray             = Encoding.UTF8.GetBytes(decodedPostStr);
            httpWebRequest.ContentLength = byteArray.Length;

            try
            {
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);

                WebResponse response = httpWebRequest.GetResponse();
                dataStream           = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                responseFromServer  = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (WebException e)
            {
                responseFromServer = "ERROR: " + e.Message + "  ----  " + "Entity: " + entity + "  ---  Post: " + postStr;
                if (e.Response != null) {
                    using (var errorResponse = (HttpWebResponse)e.Response) {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream())) {
                            LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, entity, e.Message, "", "Response: " + reader.ReadToEnd() + " Request: " + postStr);
                        }
                    }
                }
            } 
            return responseFromServer;
        }
        //------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------
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
                _customer = QueryToJsonObject("select * from customer where fullyqualifiedname = '" + customer + "'").QueryResponse.Customer;
                if (_customer != null)
                {
                    _customer = _customer[0];
                    Customers.Add(_customer);
                }
            }

            if (_customer == null)
            {
                dynamic newCustomer = new ExpandoObject(), customerinfo;
                string newCustomerJson;

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

                newCustomerJson = JsonConvert.SerializeObject(newCustomer);
                _customer       = PostToJsonObject("customer", newCustomerJson).Customer;
                Customers.Add(_customer);
            }

            return _customer;
        }
        //---------------------------------------------------------------------------------------------
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
                _class = QueryToJsonObject("select * from class where fullyqualifiedname = '" + classname + "'").QueryResponse.Class;
                if (_class != null)
                {
                    _class = _class[0];
                    Classes.Add(_class);
                }
            }

            if (_class == null)
            {
                dynamic newClass = new ExpandoObject();
                string newClassJson;

                newClass.Name = classname;

                newClassJson = JsonConvert.SerializeObject(newClass);
                _class       = PostToJsonObject("class", newClassJson).Class;
                Classes.Add(_class);
            }

            return _class;
        }
        //---------------------------------------------------------------------------------------------
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
                term = QueryToJsonObject("select * from term where name = '" + payterms + "'").QueryResponse.Term;
                if (term != null)
                {
                    term = term[0];
                    Terms.Add(term);
                }
            }

            if (term == null)
            {
                dynamic newTerm = new ExpandoObject();
                string newTermJson, duedays;

                duedays = GetPayTermsDueDate(FwSqlConnection.RentalWorks, payterms);

                newTerm.Name    = payterms;
                newTerm.Type    = "STANDARD";
                newTerm.DueDays = duedays;

                newTermJson = JsonConvert.SerializeObject(newTerm);
                term        = PostToJsonObject("term", newTermJson).Term;
                Terms.Add(term);
            }

            return term;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateTaxCode(dynamic invoice)
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
                taxcode = QueryToJsonObject("select * from taxcode where name = '" + invoice.taxitemcode + "'").QueryResponse.TaxCode;
                if (taxcode != null)
                {
                    taxcode = taxcode[0];
                    TaxItems.Add(taxcode);
                }
            }

            if (taxcode == null)
            {
                dynamic newTaxService = new ExpandoObject(), taxrate, taxagency;
                string newTaxServiceJson;

                newTaxService.TaxCode = invoice.taxitemcode;

                taxrate                      = ValidateTaxRate(invoice);
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

                newTaxServiceJson = JsonConvert.SerializeObject(newTaxService);
                taxcode           = PostToJsonObject("taxservice/taxcode", newTaxServiceJson);
                taxcode           = QueryToJsonObject("select * from taxcode where name = '" + invoice.taxitemcode + "'").QueryResponse.TaxCode;
                taxcode           = taxcode[0];
                TaxItems.Add(taxcode);
            }

            return taxcode;
        }
        //---------------------------------------------------------------------------------------------
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
                taxagency = QueryToJsonObject("select * from taxagency where name = '" + invoice.taxvendor + "'").QueryResponse.TaxAgency;
                if (taxagency != null)
                {
                    taxagency = taxagency[0];
                    TaxAgencies.Add(taxagency);
                }
            }

            if (taxagency == null)
            {
                dynamic newTaxAgency = new ExpandoObject();
                string newTaxAgencyJson;

                newTaxAgency.DisplayName = invoice.taxvendor;

                newTaxAgencyJson = JsonConvert.SerializeObject(newTaxAgency);
                taxagency        = PostToJsonObject("taxagency", newTaxAgencyJson).TaxAgency;
                TaxAgencies.Add(taxagency);
            }

            return taxagency;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateTaxRate(dynamic invoice)
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
                taxrate = QueryToJsonObject("select * from taxrate where name = '" + invoice.taxitemcode + "'").QueryResponse.TaxRate;
                if (taxrate != null)
                {
                    taxrate = taxrate[0];
                    TaxRates.Add(taxrate);
                }
            }

            return taxrate;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateItem(dynamic item)
        {
            dynamic _item = null;

            if (_Items.Count != 0)
            {
                for (int i = 0; i < _Items.Count; i++)
                {
                    if (_Items[i].Name == item.description)
                    {
                        _item = _Items[i];
                        break;
                    }
                }
            }

            if (_item == null)
            {
                string itemdescription = item.description;
                _item = QueryToJsonObject("select * from item where name = '" + itemdescription.Replace("'", @"\'") + "'").QueryResponse.Item;
                if (_item != null)
                {
                    _item = _item[0];
                    _Items.Add(_item);
                }
            }

            if (_item == null)
            {
                dynamic newItem = new ExpandoObject();
                string newItemJson = string.Empty;

                string itemdescrription         = item.description;
                newItem.Name                    = itemdescrription.Replace(":", "");
                newItem.Type                    = "Service";
                newItem.IncomeAccountRef        = new ExpandoObject();
                newItem.IncomeAccountRef.value  = ValidateAccount(item.incomeaccountid, "", "Income").Id.Value;
                newItem.ExpenseAccountRef       = new ExpandoObject();
                newItem.ExpenseAccountRef.value = ValidateAccount(item.expenseaccountid, "", "Expense").Id.Value;

                newItemJson = JsonConvert.SerializeObject(newItem);
                _item       = PostToJsonObject("item", newItemJson).Item;
                _Items.Add(_item);
            }

            return _item;
        }
        //---------------------------------------------------------------------------------------------
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
                    //if (Accounts[i].Name == accountinfo.glacctdesc)
                    if (Accounts[i].FullyQualifiedName == accountinfo.glacctdesc)   //jh 06/12/2017 CAS-20384-ZPXQ
                    {
                        account = Accounts[i];
                        break;
                    }
                }
            }

            if (account == null)
            {
                //account = QueryToJsonObject("select * from account where name = '" + accountinfo.glacctdesc + "'").QueryResponse.Account;
                account = QueryToJsonObject("select * from account where FullyQualifiedName = '" + accountinfo.glacctdesc + "'").QueryResponse.Account;  //jh 06/12/2017 CAS-20384-ZPXQ
                if (account != null)
                {
                    account = account[0];
                    Accounts.Add(account);
                }
            }

            if (account == null)
            {
                dynamic newAccount = new ExpandoObject();
                string newAccountJson;

                newAccount.Name = accountinfo.glacctdesc;

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

                newAccountJson = JsonConvert.SerializeObject(newAccount);
                account        = PostToJsonObject("account", newAccountJson).Account;
                Accounts.Add(account);
            }

            return account;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateInvoice(dynamic invoice, ExportInvoicesToQBOReturn result)
        {
            dynamic _invoice = null;

            if (Invoices.Count != 0)
            {
                for (int i = 0; i < Invoices.Count; i++)
                {
                    if (Invoices[i].DocNumber == invoice.invoiceno)
                    {
                        _invoice = Invoices[i];
                        break;
                    }
                }
            }

            if (_invoice == null)
            {
                _invoice = QueryToJsonObject("select * from invoice where docnumber = '" + invoice.invoiceno + "'").QueryResponse.Invoice;
                if (_invoice != null)
                {
                    _invoice = _invoice[0];
                    Invoices.Add(_invoice);
                }
            }

            if (_invoice != null)
            {
                result.addInvoice(invoice.invoiceno, "Already exported to Quickbooks");
            }

            if (_invoice == null)
            {
                dynamic newInvoice = new ExpandoObject();
                string newInvoiceJson;
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
                    newInvoice.CustomerMemo = invoice.printnotes;
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
                    else if (invoice.taxcountry == "C")
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

                newInvoiceJson = JsonConvert.SerializeObject(newInvoice);
                _invoice       = PostToJsonObject("invoice", newInvoiceJson).Invoice;
                Invoices.Add(_invoice);

                //jh 06/22/2017 CAS-20810-L6T5
                if (invoice.taxcountry == "C") // if Canada
                {
                    decimal taxAmount = _invoice.TxnTaxDetail.TotalTax;    // determine the tax amount that QBO calculated for this invoice
                    if (invoice.invoicetax != taxAmount) // if RW tax amuont is different than QBO tax for this invoice, force the RW tax amount up to QBO
                    {
                        dynamic newInvoice2 = new ExpandoObject();
                        dynamic _invoice2 = new ExpandoObject();
                        newInvoice2 = _invoice;
                        newInvoice2.TxnTaxDetail.TotalTax = invoice.invoicetax; // need to change it here
                        newInvoice2.TxnTaxDetail.TaxLine[0].Amount = invoice.invoicetax;  // and here, too (both)
                        newInvoiceJson = JsonConvert.SerializeObject(newInvoice2);
                        _invoice2 = PostToJsonObject("invoice", newInvoiceJson).Invoice;
                    }
                }

                result.addInvoice(invoice.invoiceno, "Exported to Quickbooks");
            }

            return _invoice;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateCreditMemo(dynamic invoice, ExportInvoicesToQBOReturn result)
        {
            dynamic creditmemo = null;

            if (CreditMemos.Count != 0)
            {
                for (int i = 0; i < CreditMemos.Count; i++)
                {
                    if (CreditMemos[i].DocNumber == invoice.invoiceno)
                    {
                        creditmemo = CreditMemos[i];
                        break;
                    }
                }
            }

            if (creditmemo == null)
            {
                creditmemo = QueryToJsonObject("select * from creditmemo where docnumber = '" + invoice.invoiceno + "'").QueryResponse.CreditMemo;
                if (creditmemo != null)
                {
                    creditmemo = creditmemo[0];
                    CreditMemos.Add(creditmemo);
                }
            }

            if (creditmemo != null)
            {
                result.addInvoice(invoice.invoiceno, "Already exported to Quickbooks");
            }

            if (creditmemo == null)
            {
                dynamic newCreditMemo = new ExpandoObject();
                string newCreditMemoJson;
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
                    newCreditMemo.CustomerMemo = invoice.printnotes;
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
                    else if (invoice.taxcountry == "C")
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

                newCreditMemoJson = JsonConvert.SerializeObject(newCreditMemo);
                creditmemo        = PostToJsonObject("creditmemo", newCreditMemoJson).CreditMemo;
                CreditMemos.Add(creditmemo);

                //jh 07/05/2017 CAS-20810-L6T5
                if (invoice.taxcountry == "C") // if Canada
                {
                    decimal taxAmount = creditmemo.TxnTaxDetail.TotalTax;    // determine the tax amount that QBO calculated for this invoice
                    if (invoice.invoicetax != taxAmount) // if RW tax amuont is different than QBO tax for this invoice, force the RW tax amount up to QBO
                    {
                        dynamic newCreditMemo2 = new ExpandoObject();
                        dynamic creditmemo2 = new ExpandoObject();
                        newCreditMemo2 = creditmemo;
                        newCreditMemo2.TxnTaxDetail.TotalTax = invoice.invoicetax; // need to change it here
                        newCreditMemo2.TxnTaxDetail.TaxLine[0].Amount = invoice.invoicetax;  // and here, too (both)
                        newCreditMemoJson = JsonConvert.SerializeObject(newCreditMemo2);
                        creditmemo2 = PostToJsonObject("creditmemo", newCreditMemoJson).CreditMemo;
                    }
                }


                result.addInvoice(invoice.invoiceno, "Exported to Quickbooks");
            }

            return creditmemo;
        }
        //---------------------------------------------------------------------------------------------
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
                _vendor = QueryToJsonObject("select * from vendor where displayname = '" + vendor + "'").QueryResponse.Vendor;
                if (_vendor != null)
                {
                    _vendor = _vendor[0];
                    Vendors.Add(_vendor);
                }
            }

            if (_vendor == null)
            {
                dynamic newVendor = new ExpandoObject(), vendorinfo;
                string newVendorJson;

                vendorinfo = GetVendorInfo(FwSqlConnection.RentalWorks, vendor);

                newVendor.DisplayName                     = vendor;
                newVendor.BillAddr                        = new ExpandoObject();
                newVendor.BillAddr.Line1                  = vendorinfo.add1;
                newVendor.BillAddr.Line2                  = vendorinfo.add2;
                newVendor.BillAddr.City                   = vendorinfo.city;
                newVendor.BillAddr.CountrySubDivisionCode = vendorinfo.state;
                newVendor.BillAddr.PostalCode             = vendorinfo.zip;
                //newVendor.BillAddr.Country                = vendorinfo.remitcountry;
                newVendor.PrimaryPhone                    = new ExpandoObject();
                newVendor.PrimaryPhone.FreeFormNumber     = vendorinfo.phone;

                newVendorJson = JsonConvert.SerializeObject(newVendor);
                _vendor        = PostToJsonObject("vendor", newVendorJson).Vendor;
                Vendors.Add(_vendor);
            }

            return _vendor;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateVendorInvoice(dynamic vendorinvoice, ExportVendorInvoicesToQBOReturn result)
        {
            dynamic _bill = null;

            if (VendorInvoices.Count != 0)
            {
                for (int i = 0; i < VendorInvoices.Count; i++)
                {
                    if (VendorInvoices[i].DocNumber == vendorinvoice.invno)
                    {
                        _bill = VendorInvoices[i];
                        break;
                    }
                }
            }

            if (_bill == null)
            {
                _bill = QueryToJsonObject("select * from bill where docnumber = '" + vendorinvoice.invno + "'").QueryResponse.Bill;
                if (_bill != null)
                {
                    _bill = _bill[0];
                    VendorInvoices.Add(_bill);
                }
            }

            if (_bill != null)
            {
                result.addVendorInvoice(vendorinvoice.invno, "Already exported to Quickbooks");
            }

            if (_bill == null)
            {
                dynamic newBill = new ExpandoObject();
                string newBillJson;

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

                newBillJson = JsonConvert.SerializeObject(newBill);
                _bill       = PostToJsonObject("bill", newBillJson).Bill;
                VendorInvoices.Add(_bill);

                result.addVendorInvoice(vendorinvoice.invno, "Exported to Quickbooks");
            }

            return _bill;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateVendorCredit(dynamic vendorinvoice, ExportVendorInvoicesToQBOReturn result)
        {
            dynamic _vendorcredit = null;

            if (VendorCredit.Count != 0)
            {
                for (int i = 0; i < VendorCredit.Count; i++)
                {
                    if (VendorCredit[i].DocNumber == vendorinvoice.invno)
                    {
                        _vendorcredit = VendorCredit[i];
                        break;
                    }
                }
            }

            if (_vendorcredit == null)
            {
                _vendorcredit = QueryToJsonObject("select * from vendorcredit where docnumber = '" + vendorinvoice.invno + "'").QueryResponse.VendorCredit;
                if (_vendorcredit != null)
                {
                    _vendorcredit = _vendorcredit[0];
                    VendorCredit.Add(_vendorcredit);
                }
            }

            if (_vendorcredit != null)
            {
                result.addVendorInvoice(vendorinvoice.invno, "Already exported to Quickbooks");
            }

            if (_vendorcredit == null)
            {
                dynamic newVendorCredit = new ExpandoObject();
                string newVendorCreditJson;

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

                newVendorCreditJson = JsonConvert.SerializeObject(newVendorCredit);
                _vendorcredit       = PostToJsonObject("vendorcredit", newVendorCreditJson).VendorCredit;
                VendorCredit.Add(_vendorcredit);

                result.addVendorInvoice(vendorinvoice.invno, "Exported to Quickbooks");
            }

            return _vendorcredit;
        }
        //---------------------------------------------------------------------------------------------
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
                _paymentmethod = QueryToJsonObject("select * from paymentmethod where name = '" + paymentmethod + "'").QueryResponse.PaymentMethod;
                if (_paymentmethod != null)
                {
                    _paymentmethod = _paymentmethod[0];
                    PaymentMethods.Add(_paymentmethod);
                }
            }

            if (_paymentmethod == null)
            {
                dynamic newPaymentMethod = new ExpandoObject();
                string newPaymentMethodJson;

                newPaymentMethod.Name = paymentmethod;

                newPaymentMethodJson = JsonConvert.SerializeObject(newPaymentMethod);
                _paymentmethod       = PostToJsonObject("paymentmethod", newPaymentMethodJson).Name;
                PaymentMethods.Add(_paymentmethod);
            }

            return _paymentmethod;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateReceipt(dynamic payment, ExportReceiptsToQBOReturn result)
        {
            dynamic _payment = null;

            if ((Receipts.Count != 0) && (payment.externalid != ""))
            {
                for (int i = 0; i < Receipts.Count; i++)
                {
                    if (Receipts[i].Id == payment.externalid)
                    {
                        _payment = Receipts[i];
                        break;
                    }
                }
            }

            if ((_payment == null) && (payment.externalid != ""))
            {
                _payment = QueryToJsonObject("select * from payment where id = '" + payment.externalid + "'").QueryResponse.Payment;
                if (_payment != null)
                {
                    _payment = _payment[0];
                    Receipts.Add(_payment);
                }
            }

            if (_payment != null)
            {
                result.addReceipt(_payment.Id.Value, "Already exported to Quickbooks");
            }

            if (_payment == null)
            {
                dynamic newPayment = new ExpandoObject();
                string newPaymentJson;

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
                    newPayment.DepositToAccountRef = new ExpandoObject();
                    newPayment.DepositToAccountRef.value = ValidateAccount("", payment.depositglacctdesc, "Liability").Id.Value;
                }
                else
                {
                    newPayment.Line = new List<dynamic>();
                    for (int j = 0; j < payment.invoicespaid.Count; j++)
                    {
                        dynamic newInvoice   = new ExpandoObject();
                        dynamic newLinkedTxn = new ExpandoObject();

                        newInvoice.Amount    = payment.invoicespaid[j].amount;
                        newInvoice.LinkedTxn = new List<dynamic>();
                        if (payment.invoicespaid[j].fromcredit == true)
                        {
                            dynamic creditmemo = QueryToJsonObject("select * from creditmemo where docnumber = '" + payment.invoicespaid[j].invoiceno + "'").QueryResponse.CreditMemo;
                            newLinkedTxn.TxnId   = creditmemo[0].Id;
                            newLinkedTxn.TxnType = "CreditMemo";
                            newInvoice.LinkedTxn.Add(newLinkedTxn);
                        }
                        else
                        {
                            dynamic invoice = QueryToJsonObject("select * from invoice where docnumber = '" + payment.invoicespaid[j].invoiceno + "'").QueryResponse.Invoice;
                            newLinkedTxn.TxnId   = invoice[0].Id;
                            newLinkedTxn.TxnType = "Invoice";
                            newInvoice.LinkedTxn.Add(newLinkedTxn);
                        }

                        newPayment.Line.Add(newInvoice);
                    }
                }

                newPaymentJson = JsonConvert.SerializeObject(newPayment);
                _payment = PostToJsonObject("payment", newPaymentJson).Payment;
                Receipts.Add(_payment);

                result.addReceipt(_payment.Id.Value, "Exported to Quickbooks");

                UpdateARExternalId(FwSqlConnection.RentalWorks, payment.arid, _payment.Id.Value);
            }

            return _payment;
        }
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
        //---------------------------------------------------------------------------------------------
    }
}