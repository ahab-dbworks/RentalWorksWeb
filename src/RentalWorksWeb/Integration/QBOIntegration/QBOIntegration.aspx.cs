using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Web.Integration
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
        
        public static string location_id = string.Empty;
        public static dynamic qbokeys, qbosettings;
        //---------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            dynamic settings;
            settings           = QBOIntegrationData.GetQBOSettings(FwSqlConnection.RentalWorks);
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

            QBOIntegrationData.StoreQBOKeys(FwSqlConnection.RentalWorks, location_id, accessToken.Token, accessToken.TokenSecret, Request.QueryString["realmId"].ToString());
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

                QBOIntegrationData.DeleteQBOKeys(FwSqlConnection.RentalWorks, location_id);
            }
            catch// (Exception ex)
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
            qbokeys     = QBOIntegrationData.GetQBOKeys(FwSqlConnection.RentalWorks, location_id);
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
    }
}