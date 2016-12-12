using System;
using System.Collections.Generic;
using System.Web;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using RentalWorksWebLibrary;
using Fw.Json.SqlServer;
using System.Web.Services;
using Fw.Json.Utilities;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;

namespace RentalWorksWeb.Integration
{
    public partial class QBOIntegration : System.Web.UI.Page
    {
        public String REQUEST_TOKEN_URL        = string.Empty;
        public String ACCESS_TOKEN_URL         = string.Empty;
        public String AUTHORIZE_URL            = string.Empty;
        public String OAUTH_URL                = string.Empty;
        public String consumerKey              = string.Empty;
        public String consumerSecret           = string.Empty;
        public string strrequestToken          = string.Empty;
        public string tokenSecret              = string.Empty;
        public string GrantUrl                 = string.Empty;
        public string oauth_callback_url       = string.Empty;
        public dynamic QBOKeys;
        
        public static string location_id               = string.Empty;
        public static dynamic qbokeys, qbosettings;
        public static List<dynamic> _Items                  = new List<dynamic>();
        public static List<dynamic> Invoices                = new List<dynamic>();
        public static List<dynamic> CreditMemos             = new List<dynamic>();
        public static List<dynamic> Classes                 = new List<dynamic>();
        public static List<dynamic> Terms                   = new List<dynamic>();
        public static List<dynamic> Customers               = new List<dynamic>();
        public static List<dynamic> PaymentMethods          = new List<dynamic>();
        public static List<dynamic> TaxItems /*taxcode*/    = new List<dynamic>();
        public static List<dynamic> TaxRates                = new List<dynamic>();
        public static List<dynamic> Receipts /*payment*/    = new List<dynamic>();
        public static List<dynamic> Vendors                 = new List<dynamic>();
        public static List<dynamic> TaxAgencies             = new List<dynamic>();
        public static List<dynamic> VendorInvoices /*bill*/ = new List<dynamic>();
        public static List<dynamic> Accounts                = new List<dynamic>();
        //---------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            dynamic settings;
            settings           = RwAppData.GetQBOSettings(FwSqlConnection.RentalWorks);
            GrantUrl           = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Integration/QBOIntegration/QBOIntegration.aspx?connect=true";
            oauth_callback_url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Integration/QBOIntegration/QBOIntegration.aspx?";

            consumerKey       = settings.qboconsumerkey;
            consumerSecret    = settings.qboconsumersecret;
            REQUEST_TOKEN_URL = (settings.qborequesttokenurl != "") ? settings.qborequesttokenurl : "https://oauth.intuit.com/oauth/v1/get_request_token";
            ACCESS_TOKEN_URL  = (settings.qboaccesstokenurl != "")  ? settings.qboaccesstokenurl  : "https://oauth.intuit.com/oauth/v1/get_access_token";
            AUTHORIZE_URL     = (settings.qboauthorizeurl != "")    ? settings.qboauthorizeurl    : "https://workplace.intuit.com/Connect/Begin";
            OAUTH_URL         = (settings.qbooauthurl != "")        ? settings.qbooauthurl        : "https://oauth.intuit.com/oauth/v1";

            if (Request.QueryString.Count > 0)
            {
                List<string> queryKeys = new List<string>(Request.QueryString.AllKeys);
                if (queryKeys.Contains("connect"))
                {
                    FireAuth();
                }
                if (queryKeys.Contains("oauth_token"))
                {
                    ReadToken();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        private void FireAuth()
        {
            HttpContext.Current.Session["consumerKey"]    = consumerKey;
            HttpContext.Current.Session["consumerSecret"] = consumerSecret;
            CreateAuthorization();
            IToken token    = (IToken)HttpContext.Current.Session["requestToken"];
            tokenSecret     = token.TokenSecret;
            strrequestToken = token.Token;
        }
        //---------------------------------------------------------------------------------------------
        private void ReadToken()
        {
            HttpContext.Current.Session["oauthToken"]    = Request.QueryString["oauth_token"].ToString();
            HttpContext.Current.Session["oauthVerifyer"] = Request.QueryString["oauth_verifier"].ToString();
            HttpContext.Current.Session["realm"]         = Request.QueryString["realmId"].ToString();
            HttpContext.Current.Session["dataSource"]    = Request.QueryString["dataSource"].ToString();
            //Stored in a session for demo purposes.
            //Production applications should securely store the Access Token
            getAccessToken();
        }
        //---------------------------------------------------------------------------------------------
        #region <<Routines>>
        //---------------------------------------------------------------------------------------------
        protected IOAuthSession CreateSession()
        {
            var consumerContext = new OAuthConsumerContext { ConsumerKey     = HttpContext.Current.Session["consumerKey"].ToString(),
                                                             ConsumerSecret  = HttpContext.Current.Session["consumerSecret"].ToString(),
                                                             SignatureMethod = SignatureMethod.HmacSha1 };
            return new OAuthSession(consumerContext, REQUEST_TOKEN_URL, HttpContext.Current.Session["oauthLink"].ToString(), ACCESS_TOKEN_URL);
        }
        //---------------------------------------------------------------------------------------------
        private void getAccessToken()
        {
            IOAuthSession clientSession                      = CreateSession();
            IToken accessToken                               = clientSession.ExchangeRequestTokenForAccessToken((IToken)HttpContext.Current.Session["requestToken"], HttpContext.Current.Session["oauthVerifyer"].ToString());
            HttpContext.Current.Session["accessToken"]       = accessToken.Token;
            HttpContext.Current.Session["accessTokenSecret"] = accessToken.TokenSecret;

            RwAppData.StoreQBOKeys(FwSqlConnection.RentalWorks, location_id, accessToken.Token, accessToken.TokenSecret, Request.QueryString["realmId"].ToString());
        }
        //---------------------------------------------------------------------------------------------
        protected void CreateAuthorization()
        {
            //Remember these for later.
            HttpContext.Current.Session["consumerKey"]    = consumerKey;
            HttpContext.Current.Session["consumerSecret"] = consumerSecret;
            HttpContext.Current.Session["oauthLink"]      = OAUTH_URL;
            //
            IOAuthSession session                       = CreateSession();
            IToken requestToken                         = session.GetRequestToken();
            HttpContext.Current.Session["requestToken"] = requestToken;
            tokenSecret                                 = requestToken.TokenSecret;
            var authUrl                                 = string.Format("{0}?oauth_token={1}&oauth_callback={2}", AUTHORIZE_URL, requestToken.Token, UriUtility.UrlEncode(oauth_callback_url));
            HttpContext.Current.Session["oauthLink"]    = authUrl;
            Response.Redirect(authUrl);
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
        [WebMethod]
        public static void Disconnect()
        {
            try
            {
                HttpContext.Current.Session["accessToken"]       = null;
                HttpContext.Current.Session["accessTokenSecret"] = null;
                HttpContext.Current.Session["realm"]             = null;
                HttpContext.Current.Session["dataSource"]        = null;

                RwAppData.DeleteQBOKeys(FwSqlConnection.RentalWorks, location_id);
            }
            catch (Exception ex)
            {
                
            }
        }
        //---------------------------------------------------------------------------------------------
        public class LoadScreenReturn
        {
            public bool   connected = false;
            public string dateconnected = string.Empty;
            public string expiresindays = string.Empty;
        }
        //---------------------------------------------------------------------------------------------
        [WebMethod]
        public static LoadScreenReturn LoadScreen(string locationid)
        {
            LoadScreenReturn result = new LoadScreenReturn();

            location_id = FwCryptography.AjaxDecrypt(locationid);
            qbokeys     = RwAppData.GetQBOKeys(FwSqlConnection.RentalWorks, location_id);
            if ((qbokeys != null) && (qbokeys.accesstoken != ""))
            {
                DateTime expiredt = FwConvert.ToDateTime(qbokeys.accesstokendate);
                int expiresindays = (expiredt.AddDays(180) - DateTime.Now.Date).Days;

                if (expiresindays > 0)
                {
                    result.connected = true;
                    result.dateconnected = FwConvert.ToUSShortDate(qbokeys.accesstokendate);
                    result.expiresindays = expiresindays.ToString();
                }
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public class ExportToQBOReturn
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
        [WebMethod]
        public static ExportToQBOReturn ExportToQBO(dynamic formdata)
        {
            dynamic invoiceids, invoiceinfo;
            ExportToQBOReturn result = new ExportToQBOReturn();
            try
            {
                qbosettings = RwAppData.GetQBOSettings(FwSqlConnection.RentalWorks);
                qbokeys     = RwAppData.GetQBOKeys(FwSqlConnection.RentalWorks, location_id);
                invoiceids  = RwAppData.ExportInvoices(FwSqlConnection.RentalWorks, formdata["batchno"], formdata["batchfrom"], formdata["batchto"], location_id);

                for (int i = 0; i < invoiceids.Count; i++)
                {
                    invoiceinfo       = RwAppData.InvoiceView(FwSqlConnection.RentalWorks, invoiceids[i].invoiceid);
                    invoiceinfo.items = RwAppData.InvoiceItemView(FwSqlConnection.RentalWorks, invoiceids[i].invoiceid);

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
                RwAppData.LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, formdata["batchno"], ex.Message, "", ex.StackTrace);
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
                            RwAppData.LogChgBatchExportRecordError(FwSqlConnection.RentalWorks, entity, e.Message, "", "Response: " + reader.ReadToEnd() + " Request: " + postStr);
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
        private static dynamic ValidateCustomer(dynamic invoice)
        {
            dynamic customer = null;

            if (Customers.Count != 0)
            {
                for (int i = 0; i < Customers.Count; i++)
                {
                    if (Customers[i].DisplayName == invoice.customer)
                    {
                        customer = Customers[i];
                        break;
                    }
                }
            }

            if (customer == null)
            {
                customer = QueryToJsonObject("select * from customer where fullyqualifiedname = '" + invoice.customer + "'").QueryResponse.Customer;
                if (customer != null)
                {
                    customer = customer[0];
                    Customers.Add(customer);
                }
            }

            if (customer == null)
            {
                dynamic newCustomer = new ExpandoObject();
                string newCustomerJson;

                newCustomer.DisplayName                     = invoice.customer;
                newCustomer.BillAddr                        = new ExpandoObject();
                newCustomer.BillAddr.Line1                  = invoice.billtoadd1;
                newCustomer.BillAddr.Line2                  = invoice.billtoadd2;
                newCustomer.BillAddr.City                   = invoice.billtocity;
                newCustomer.BillAddr.CountrySubDivisionCode = invoice.billtostate;
                newCustomer.BillAddr.PostalCode             = invoice.billtozip;
                newCustomer.BillAddr.Country                = invoice.billtocountry;

                newCustomerJson = JsonConvert.SerializeObject(newCustomer);
                customer        = PostToJsonObject("customer", newCustomerJson).Customer;
                Customers.Add(customer);
            }

            return customer;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateClass(dynamic invoice)
        {
            dynamic _class = null;

            if (Classes.Count != 0)
            {
                for (int i = 0; i < Classes.Count; i++)
                {
                    if (Classes[i].Name == invoice.invoiceclass)
                    {
                        _class = Classes[i];
                        break;
                    }
                }
            }

            if (_class == null)
            {
                _class = QueryToJsonObject("select * from class where fullyqualifiedname = '" + invoice.invoiceclass + "'").QueryResponse.Class;
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

                newClass.Name = invoice.invoiceclass;

                newClassJson = JsonConvert.SerializeObject(newClass);
                _class       = PostToJsonObject("class", newClassJson).Class;
                Classes.Add(_class);
            }

            return _class;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateTerm(dynamic invoice)
        {
            dynamic term = null;

            if (Terms.Count != 0)
            {
                for (int i = 0; i < Terms.Count; i++)
                {
                    if (Terms[i].Name == invoice.payterms)
                    {
                        term = Terms[i];
                        break;
                    }
                }
            }

            if (term == null)
            {
                term = QueryToJsonObject("select * from term where name = '" + invoice.payterms + "'").QueryResponse.Term;
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

                duedays = RwAppData.GetPayTermsDueDate(FwSqlConnection.RentalWorks, invoice.payterms);

                newTerm.Name    = invoice.payterms;
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
                    dynamic rates    = RwAppData.GetTaxRatePercent(FwSqlConnection.RentalWorks, invoice.taxitemcode);
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
        private static dynamic ValidateItem(dynamic invoiceitem)
        {
            dynamic item = null;

            if (_Items.Count != 0)
            {
                for (int i = 0; i < _Items.Count; i++)
                {
                    if (_Items[i].Name == invoiceitem.description)
                    {
                        item = _Items[i];
                        break;
                    }
                }
            }

            if (item == null)
            {
                string itemdescription = invoiceitem.description;
                item = QueryToJsonObject("select * from item where name = '" + itemdescription.Replace("'", @"\'") + "'").QueryResponse.Item;
                if (item != null)
                {
                    item = item[0];
                    _Items.Add(item);
                }
            }

            if (item == null)
            {
                dynamic newItem = new ExpandoObject();
                string newItemJson = string.Empty;

                newItem.Name                    = invoiceitem.description;
                newItem.Type                    = "Service";
                newItem.IncomeAccountRef        = new ExpandoObject();
                newItem.IncomeAccountRef.value  = ValidateAccount(invoiceitem, "Income").Id.Value;
                newItem.ExpenseAccountRef       = new ExpandoObject();
                newItem.ExpenseAccountRef.value = ValidateAccount(invoiceitem, "Expense").Id.Value;

                newItemJson = JsonConvert.SerializeObject(newItem);
                item        = PostToJsonObject("item", newItemJson).Item;
                _Items.Add(item);
            }

            return item;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateAccount(dynamic invoiceitem, string accounttype)
        {
            dynamic account     = null;
            dynamic accountinfo = null;

            if (accounttype == "Income")
            {
                accountinfo = RwAppData.GetAccountInfo(FwSqlConnection.RentalWorks, invoiceitem.incomeaccountid);
            }
            else if (accounttype == "Expense")
            {
                accountinfo = RwAppData.GetAccountInfo(FwSqlConnection.RentalWorks, invoiceitem.expenseaccountid);
            }

            if (Accounts.Count != 0)
            {
                for (int i = 0; i < Accounts.Count; i++)
                {
                    if (Accounts[i].Name == accountinfo.glacctdesc)
                    {
                        account = Accounts[i];
                        break;
                    }
                }
            }

            if (account == null)
            {
                account = QueryToJsonObject("select * from account where name = '" + accountinfo.glacctdesc + "'").QueryResponse.Account;
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
                    //newAccount.AccountType = "CostOfGoodsSold";
                    newAccount.AccountSubType = "EquipmentRentalCos";
                }
                else if (accounttype == "Income")
                {
                    newAccount.AccountSubType = "SalesOfProductIncome";
                }

                newAccountJson = JsonConvert.SerializeObject(newAccount);
                account        = PostToJsonObject("account", newAccountJson).Account;
                Accounts.Add(account);
            }

            return account;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateInvoice(dynamic invoice, ExportToQBOReturn result)
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

                newInvoice.DocNumber                       = invoice.invoiceno;
                newInvoice.TxnDate                         = FwConvert.ToDateTime(invoice.invoicedate).ToString("yyyy-MM-dd");
                newInvoice.CustomerRef                     = new ExpandoObject();
                newInvoice.CustomerRef.value               = ValidateCustomer(invoice).Id.Value;
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
                    newInvoice.ClassRef.value = ValidateClass(invoice).Id.Value;
                }

                if (invoice.payterms != "")
                {
                    newInvoice.SalesTermRef       = new ExpandoObject();
                    newInvoice.SalesTermRef.value = ValidateTerm(invoice).Id.Value;
                }

                if (invoice.taxitemcode != "")
                {
                    newInvoice.TxnTaxDetail                     = new ExpandoObject();
                    newInvoice.TxnTaxDetail.TxnTaxCodeRef       = new ExpandoObject();
                    newInvoice.TxnTaxDetail.TxnTaxCodeRef.value = ValidateTaxCode(invoice).Id.Value;
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

                    newItem.Id                                   = j+1;
                    newItem.DetailType                           = "SalesItemLineDetail";
                    newItem.SalesItemLineDetail                  = new ExpandoObject();
                    newItem.SalesItemLineDetail.ItemRef          = new ExpandoObject();
                    newItem.SalesItemLineDetail.ItemRef.value    = ValidateItem(invoice.items[j]).Id.Value;
                    newItem.SalesItemLineDetail.Qty              = invoice.items[j].qty;
                    newItem.SalesItemLineDetail.UnitPrice        = (invoice.items[j].qty != 0) ? (invoice.items[j].linetotal / invoice.items[j].qty) : "0";
                    newItem.SalesItemLineDetail.TaxCodeRef       = new ExpandoObject();
                    newItem.SalesItemLineDetail.TaxCodeRef.value = (invoice.items[j].taxable == "T") ? "TAX" : "NON";
                    newItem.Amount                               = invoice.items[j].linetotal;

                    newInvoice.Line.Add(newItem);
                }

                newInvoiceJson = JsonConvert.SerializeObject(newInvoice);
                _invoice       = PostToJsonObject("invoice", newInvoiceJson).Invoice;
                Invoices.Add(_invoice);

                result.addInvoice(invoice.invoiceno, "Exported to Quickbooks");
            }

            return _invoice;
        }
        //---------------------------------------------------------------------------------------------
        private static dynamic ValidateCreditMemo(dynamic invoice, ExportToQBOReturn result)
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

                newCreditMemo.DocNumber                       = invoice.invoiceno;
                newCreditMemo.TxnDate                         = FwConvert.ToDateTime(invoice.invoicedate).ToString("yyyy-MM-dd");
                newCreditMemo.CustomerRef                     = new ExpandoObject();
                newCreditMemo.CustomerRef.value               = ValidateCustomer(invoice).Id.Value;
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
                    newCreditMemo.ClassRef.value = ValidateClass(invoice).Id.Value;
                }

                if (invoice.payterms != "")
                {
                    newCreditMemo.SalesTermRef       = new ExpandoObject();
                    newCreditMemo.SalesTermRef.value = ValidateTerm(invoice).Id.Value;
                }

                if (invoice.taxitemcode != "")
                {
                    newCreditMemo.TxnTaxDetail                     = new ExpandoObject();
                    newCreditMemo.TxnTaxDetail.TxnTaxCodeRef       = new ExpandoObject();
                    newCreditMemo.TxnTaxDetail.TxnTaxCodeRef.value = ValidateTaxCode(invoice).Id.Value;
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

                    newItem.Id                                = j+1;
                    newItem.DetailType                        = "SalesItemLineDetail";
                    newItem.SalesItemLineDetail               = new ExpandoObject();
                    newItem.SalesItemLineDetail.ItemRef       = new ExpandoObject();
                    newItem.SalesItemLineDetail.ItemRef.value = ValidateItem(invoice.items[j]).Id.Value;
                    newItem.SalesItemLineDetail.Qty           = invoice.items[j].qty;
                    newItem.Amount                            = (invoice.items[j].qty * invoice.items[j].rate);

                    newCreditMemo.Line.Add(newItem);
                }

                newCreditMemoJson = JsonConvert.SerializeObject(newCreditMemo);
                creditmemo        = PostToJsonObject("creditmemo", newCreditMemoJson).CreditMemo;
                CreditMemos.Add(creditmemo);

                result.addInvoice(invoice.invoiceno, "Exported to Quickbooks");
            }

            return creditmemo;
        }
        //---------------------------------------------------------------------------------------------
    }
}