using System;
using System.Data;
using Fw.Json.Utilities;
using Fw.Json.SqlServer.Entities;
using System.Dynamic;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SqlClient;
using Fw.Json.ValueTypes;
using System.IO;
using System.Text;

namespace Fw.Json.SqlServer
{
    public class FwSqlData
    {
        //-----------------------------------------------------------------------------
        static public String Decrypt(FwSqlConnection conn, string data)
        {
           string value = string.Empty;

            FwSqlCommand qry = new FwSqlCommand(conn);
            qry.Add("select value = dbo.decrypt(@data)");
            qry.AddParameter("@data", data);
            qry.Execute();
            value = qry.GetField("value").ToString().TrimEnd();

            return value;
        }
        //-----------------------------------------------------------------------------
        static public String Encrypt(FwSqlConnection conn, string data)
        {
            string value;
            FwSqlCommand qry;
            
            value = string.Empty;
            qry = new FwSqlCommand(conn);
            qry.Add("select value = dbo.encrypt(@data)");
            qry.AddParameter("@data", data);
            qry.Execute();
            value = qry.GetField("value").ToString().Trim();
            
            return value;
        }
        //-----------------------------------------------------------------------------
        public static FwControl GetControl(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            DataTable dt;
            FwControl control;
            
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from control with(nolock)");
            qry.Add("where controlid = 1");
            dt = qry.QueryToTable();
            control = null;
            if (dt.Rows.Count > 0)
            {
                control = new FwControl();
                control.ControlId   = dt.Rows[0]["controlid"].ToString().TrimEnd();
                control.Company     = dt.Rows[0]["company"].ToString().TrimEnd();
                control.System      = dt.Rows[0]["system"].ToString().TrimEnd();
                control.ImagePath   = dt.Rows[0]["imagepath"].ToString().TrimEnd();
                control.DateStamp   = dt.Rows[0]["datestamp"].ToString().TrimEnd();
                control.LoadSettings(dt.Rows[0]["settings"].ToString().TrimEnd());
            }
            
            return control;
        }
        //-----------------------------------------------------------------------------
        public static dynamic GetWebControl(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            dynamic result;
            
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from webcontrol with(nolock)");
            qry.Add("where webcontrolid = 1");
            result = qry.QueryToDynamicObject2();
            
            return result;
        }
        //-----------------------------------------------------------------------------
        static public String GetClientCode(FwSqlConnection conn) 
        {
            string value;
            FwSqlCommand qry;

            value = string.Empty;
            qry   = new FwSqlCommand(conn);
            qry.Add("select value = dbo.getclientcode()");
            qry.Execute();
            value = qry.GetField("value").ToString().TrimEnd().ToUpper();
            
            return value;
        }
        //-----------------------------------------------------------------------------
        static public String GetNextId(FwSqlConnection conn)
        {
            string msg, id;
            FwSqlCommand sp;
           
            msg = string.Empty;
            id  = string.Empty;
            sp  = new FwSqlCommand(conn, "getnextid");
            sp.AddParameter("@id", SqlDbType.Char, ParameterDirection.Output);
            sp.Execute();
            id = sp.GetParameter("@id").ToString().TrimEnd();
            
            return id;
        }
        //-----------------------------------------------------------------------------
        static public String GetNextId(FwSqlConnection conn, SqlTransaction transaction)
        {
            string msg, id;
           
            msg = string.Empty;
            id  = string.Empty;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "getnextid"))
            {
                sp.Transaction = transaction;
                sp.AddParameter("@id", SqlDbType.Char, ParameterDirection.Output);
                sp.Execute(false);
                id = sp.GetParameter("@id").ToString().TrimEnd();
            }
            
            return id;
        }
        //-----------------------------------------------------------------------------
        public enum SQLVersions {NotLoaded, Unknown, SQL2000, SQL2005, SQL2008}
        static SQLVersions GetSqlVersion(FwSqlConnection conn)
        {
            bool ismssql2000, ismssql2005, ismssql2008;
            FwSqlCommand qry;
            SQLVersions sqlVersion;
            
            ismssql2000 = false;
            ismssql2005 = false;
            ismssql2008 = false;
            sqlVersion = SQLVersions.NotLoaded;
            qry = new FwSqlCommand(conn);
            qry.Add("select ismssql2000 = dbo.fw_funcismssql2000(),");
            qry.Add("       ismssql2005 = dbo.fw_funcismssql2005(),");
            qry.Add("       ismssql2008 = dbo.fw_funcismssql2008()");
            qry.Execute();
            ismssql2000 = qry.GetField("ismssql2000").ToBoolean();
            ismssql2005 = qry.GetField("ismssql2005").ToBoolean();
            ismssql2008 = qry.GetField("ismssql2008").ToBoolean();
            if (ismssql2008)
            {
                sqlVersion = SQLVersions.SQL2008;
            } else if (ismssql2005)
            {
                sqlVersion = SQLVersions.SQL2005;
            } else if (ismssql2000)
            {
                sqlVersion = SQLVersions.SQL2000;
            }
            else
            {
                sqlVersion = SQLVersions.Unknown;
            }

            return sqlVersion;
        }
        //----------------------------------------------------------------------------------------------------
        public static string GetUsersId(FwSqlConnection conn, string webUsersId)
        {
            string usersId;
            FwSqlCommand qry;
            
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 usersid");
            qry.Add("from webusers with(nolock)");
            qry.Add("where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webUsersId);
            qry.Execute();
            usersId = qry.GetField("usersid").ToString().TrimEnd();
            
            return usersId;
        }
        //------------------------------------------------------------------------------
        public static string GetWebUsersId(FwSqlConnection conn, string email)
        {
            string webUsersId;
            FwSqlCommand qry;

            webUsersId = string.Empty;
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 webusersid");
            qry.Add("from webusersview with(nolock)");
            qry.Add("where email = @email");
            qry.AddParameter("@email", email);
            qry.Execute();
            webUsersId = qry.GetField("webusersid").ToString().TrimEnd();
            
            return webUsersId;
        }
        //-----------------------------------------------------------------------------
        //public static dynamic GetWebUsersView(FwSqlConnection conn, string email)
        //{
        //    FwSqlCommand qry;
        //    dynamic result;
        //    DataTable dt;
        //    object cellValue;
        //    IDictionary<String, object> webUser;
            
        //    result = null;
        //    qry = new FwSqlCommand(conn);
        //    qry.Add("select top 1 *");
        //    qry.Add("from   webusersview with(nolock)");
        //    qry.Add("where  email         = @email");
        //    qry.Add("   or  userloginname = @email");
        //    qry.AddParameter("@email", email);
        //    dt = qry.QueryToTable();
        //    if (dt.Rows.Count == 1)
        //    {
        //        result = new ExpandoObject();
        //        webUser = result as IDictionary<String, object>;
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            cellValue = dt.Rows[0][i];
        //            if (cellValue is string) {
        //                cellValue = cellValue.ToString().TrimEnd();
        //            } 
        //            webUser[dt.Columns[i].ColumnName] = cellValue;
        //        }
        //    }

        //    return result;
        //}
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
            qry.Add("from   webusersview with(nolock)");
            qry.Add("where  webusersid = @webusersid");
            qry.Add("order by usertype desc"); //2016-12-07 MY: This is a hack fix to make Usertype: user show up first. Need a better solution.
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
        //-----------------------------------------------------------------------------
        public static string InsertAppImage(FwSqlConnection conn, string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype, string extension, byte[] image)
        {
            int width, height;
            string appImageId;
            FwSqlCommand sp;

            width = 0;
            height = 0;
            sp = new FwSqlCommand(conn, "insertappimage");
            sp.AddParameter("@uniqueid1",   uniqueid1);
            sp.AddParameter("@uniqueid2",   uniqueid2);
            sp.AddParameter("@uniqueid3",   uniqueid3);
            sp.AddParameter("@description", description);
            sp.AddParameter("@rectype",     rectype);
            sp.AddParameter("@extension",   extension);
            sp.AddParameter("@image",       FwGraphics.ConvertToJpg(image, ref width, ref height));
            sp.AddParameter("@thumbnail",   FwGraphics.GetJpgThumbnail(image));
            sp.AddParameter("@width",       width);
            sp.AddParameter("@height",      height);
            sp.AddParameter("@appimageid",  System.Data.SqlDbType.Char, System.Data.ParameterDirection.Output);
            sp.Execute();
            appImageId = sp.GetParameter("@appimageid").ToString().TrimEnd();

            return appImageId;
        }
        //-----------------------------------------------------------------------------
        public static string InsertAppImage(FwSqlConnection conn, string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype, string extension, string base64Image)
        {
            byte[] image;
            string appImageId;

            image = Convert.FromBase64String(base64Image);
            appImageId = FwSqlData.InsertAppImage(conn, uniqueid1, uniqueid2, uniqueid3, description, rectype, extension, image);

            return appImageId;
        }
        //-----------------------------------------------------------------------------
        public static Bitmap GetAppImage(FwSqlConnection conn, string rowGuid)
        {
            FwSqlCommand qry;
            Bitmap image;
            
            qry= new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from appimage with(nolock)");
            qry.Add("where rowguid = @rowguid");
            qry.AddParameter("@rowguid", rowGuid);
            qry.Execute();
            if (qry.RowCount > 0)
            {
                image = qry.GetField("image").ToBitmap();
            }
            else
            {
                image = null;
            }
            return image;
        }
        //-----------------------------------------------------------------------------
        public static string InsertSecurity(FwSqlConnection conn, string secDesc, string parentId, int itemType)
        {
            string secId;
            FwSqlCommand sp;

            secId = string.Empty;
            sp = new FwSqlCommand(conn, "insertsecurity");
            sp.AddParameter("@secdesc",  secDesc);
            sp.AddParameter("@parentid", parentId);
            sp.AddParameter("@itemtype", itemType);
            sp.AddParameter("@secid",    System.Data.SqlDbType.Char, System.Data.ParameterDirection.Output);
            sp.Execute();
            secId = sp.GetParameter("@secid").ToString().TrimEnd();

            return secId;
        }
        //-----------------------------------------------------------------------------
        public static bool IsMSSQL2000(FwSqlConnection conn)
        {
            bool result;
            result = (GetSqlVersion(conn) == SQLVersions.SQL2000);
            return result;
        }
        //-----------------------------------------------------------------------------
        public static bool IsMSSQL2005(FwSqlConnection conn)
        {
            bool result;
            result = (GetSqlVersion(conn) == SQLVersions.SQL2005);
            return result;
        }
        //-----------------------------------------------------------------------------
        public static bool IsMSSQL2008(FwSqlConnection conn)
        {
            bool result = false;
            result = (GetSqlVersion(conn) == SQLVersions.SQL2008);
            return result;
        }
        //-----------------------------------------------------------------------------
        //public static bool LoginWebUsers(FwSecurity.FwLoginSSO loginSSO)
        //{
        //    FwSqlCommand sp;
        //    loginSSO.WebAccess = false;
        //    loginSSO.ErrNo = 0;
        //    loginSSO.ErrMsg = string.Empty;
        //    sp = new FwSqlCommand("loginwebusers");
        //    sp.AddParameter("@email", loginSSO.Email);
        //    sp.AddParameter("@sourceid1", loginSSO.SourceId1);
        //    sp.AddParameter("@webaccess",  SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@webusersid", SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@errno",      SqlDbType.Int,     ParameterDirection.Output);
        //    sp.AddParameter("@errmsg",     SqlDbType.VarChar, ParameterDirection.Output);
        //    try
        //    {
        //        sp.Open();
        //        loginSSO.WebAccess  = sp.GetParameterValue("@webaccess").ToString().TrimEnd().Equals("T");
        //        loginSSO.ErrNo      = sp.GetParameterValue("@errno").ToInt32();
        //        loginSSO.ErrMsg     = sp.GetParameterValue("@errmsg").ToInt32();
        //        loginSSO.WebUsersId = sp.GetParameterValue("@webusersid").ToString().TrimEnd();
        //        if (loginSSO.ErrNo != 0)
        //        {
        //            loginSSO.WebAccess = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loginSSO.WebAccess = false;
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sp.Close();
        //    }
        //    return loginSSO.WebAccess;
        //}
        //-----------------------------------------------------------------------------
        public static void LockOutWebUser(FwSqlConnection conn, string webUsersId) 
        {
            FwSqlCommand qry;
            
            if (!webUsersId.Equals(string.Empty))
            {
                qry = new FwSqlCommand(conn);
                qry.Add("update webusers");
                qry.Add("  set  lockaccount = @lockaccount");
                qry.Add("where  webusersid = @webusersid");
                qry.AddParameter("@lockaccount", "T");
                qry.AddParameter("@webusersid", webUsersId);
                qry.Execute();
            }
        }
        //------------------------------------------------------------------------------
        public static bool WebEmailRegistered(FwSqlConnection conn, string email)
        {
            bool registered = false;
            FwSqlCommand sp;

            registered = false;
            email = email.ToUpper().Trim();
            sp = new FwSqlCommand(conn, "webemailregistered");
            sp.AddParameter("@email", email);
            sp.AddParameter("@registered", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            registered = (sp.GetParameter("@registered").ToString().Trim() == "T");
            
            return registered;
        }
        //------------------------------------------------------------------------------
        public static string WebAuthenticate(FwSqlConnection conn, string email, string password, ref int errNo, ref string errMsg)
        {
            string webUsersId, webpassword, encryptedwebpassword;
            FwSqlCommand sp;
            //dynamic appoptions;
            
            webUsersId = "";
            //appoptions = FwSqlData.GetApplicationOptions(conn);     //MY 6/26/2015: Why are we loading app options in WebGetUsers?
            //webpassword =  ((FwValidate.IsPropertyDefined(appoptions, "mixedcase")) && (appoptions.mixedcase.enabled)) ? password : password.ToUpper();
            //AG 02/11/2015 need to support mixcase on passwords
            webpassword =  password.ToUpper();
            encryptedwebpassword = FwSqlData.Encrypt(conn, webpassword);
            //sp = new FwSqlCommand(conn, "webgetusers2"); //MY + AG 10/23/2017: Changed method to remove web access check and not effect old .net systems.
            sp = new FwSqlCommand(conn, "webauthenticate");
            sp.AddParameter("@userlogin",         email);
            sp.AddParameter("@userloginpassword", encryptedwebpassword);
            sp.AddParameter("@webusersid",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@errno",             SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",            SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            webUsersId = sp.GetParameter("@webusersid").ToString().TrimEnd();
            errNo      = sp.GetParameter("@errno").ToInt32();
            errMsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return webUsersId;
        }
        //------------------------------------------------------------------------------
        public static string WebGetUserTmpPassword(FwSqlConnection conn, string usersEmail, string webPassword)
        {
            string webUsersId;
            FwSqlCommand sp;
            
            webUsersId = "";
            webPassword = webPassword.ToUpper().Trim();
            sp = new FwSqlCommand(conn, "webgetuserstmppassword");
            sp.AddParameter("@usersemail",  usersEmail);
            sp.AddParameter("@webpassword", webPassword);
            sp.AddParameter("@webusersid",  SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            webUsersId = sp.GetParameter("@webusersid").ToString().TrimEnd();

            return webUsersId;
        }
        //------------------------------------------------------------------------------
        public static void WebRegisterUser(FwSqlConnection conn, string usersEmail, ref int errNo, ref string errMsg)
        {
            FwSqlCommand sp;
            
            if (errNo == 0)
            {
                sp = new FwSqlCommand(conn, "webregisteruser");
                sp.AddParameter("@usersemail", SqlDbType.NVarChar, ParameterDirection.Input,  usersEmail);
                sp.AddParameter("@errno",      SqlDbType.Int,      ParameterDirection.Output);
                sp.AddParameter("@errmsg",     SqlDbType.NVarChar, ParameterDirection.Output);
                sp.Execute();
                errNo  = sp.GetParameter("@errno").ToInt32();
                errMsg = sp.GetParameter("@errmsg").ToString().TrimEnd();
            }
        }
        //------------------------------------------------------------------------------
        public class WebUserResetPasswordResult
        {
            public int ErrNo {get;set;}
            public string ErrMsg {get;set;}

            public WebUserResetPasswordResult()
            {
                ErrNo = 0;
                ErrMsg = string.Empty;
            }
        }
        public static WebUserResetPasswordResult WebUserResetPassword(FwSqlConnection conn, string usersEmail)
        {
            FwSqlCommand sp;
            WebUserResetPasswordResult result;

            result = new WebUserResetPasswordResult();
            sp = new FwSqlCommand(conn, "webuserresetpassword");
            sp.AddParameter("@usersemail", SqlDbType.NVarChar, ParameterDirection.Input, usersEmail);
            sp.AddParameter("@errno",      SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            result.ErrNo  = sp.GetParameter("@errno").ToInt32();
            result.ErrMsg = sp.GetParameter("@errmsg").ToString().TrimEnd();
            
            return result;
        }
        //------------------------------------------------------------------------------
        public static string WebUsersSetPassword(FwSqlConnection conn, string webUsersId, string webPassword, string clearTmpPassword)
        {
            int errNo;
            string errMsg;
            FwSqlCommand sp;
            
            errNo = 0;
            errMsg = "";
            webPassword = webPassword.ToUpper().Trim();
            webPassword = FwCryptography.DbwEncrypt(conn, webPassword);
            sp = new FwSqlCommand(conn, "webuserssetpassword");
            sp.AddParameter("@webusersid",       SqlDbType.NVarChar, ParameterDirection.Input, webUsersId);
            sp.AddParameter("@webpassword",      SqlDbType.NVarChar, ParameterDirection.Input, webPassword);
            sp.AddParameter("@cleartmppassword", SqlDbType.NVarChar, ParameterDirection.Input, clearTmpPassword);
            sp.AddParameter("@errno",            SqlDbType.Int,      ParameterDirection.Output, "");
            sp.AddParameter("@errmsg",           SqlDbType.NVarChar, ParameterDirection.Output, "");
            sp.Execute();
            errNo  = sp.GetParameter("@errno").ToInt32();
            errMsg = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return errMsg;
        }
        //------------------------------------------------------------------------------
        public static bool WebValidatePassword(FwSqlConnection conn, string webPassword, ref int errNo, ref string errMsg)
        {
            bool validPassword;
            FwSqlCommand sp;
            validPassword = false;
            errNo = 0;
            errMsg = string.Empty;
            webPassword = webPassword.ToUpper().Trim();
            sp = new FwSqlCommand(conn, "webvalidatepassword");
            sp.AddParameter("@webpassword",   SqlDbType.Char,    ParameterDirection.Input, webPassword);
            sp.AddParameter("@validpassword", SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@errno",         SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@errmsg",        SqlDbType.VarChar, ParameterDirection.Output);
            try
            {
                sp.ExecuteReader();
                validPassword = sp.GetParameter("@validpassword").ToBoolean();
                errNo         = sp.GetParameter("@errno").ToInt32();
                errMsg        = sp.GetParameter("@errmsg").ToString().TrimEnd();
            }
            catch(Exception ex)
            {
                errNo = 1004;
                errMsg = ex.Message;
            }
            finally
            {
                sp.Close();
            }
            return validPassword;
        }
        //-----------------------------------------------------------------------------
        public static void UpdateAppNote(FwSqlConnection conn, string uniqueid1, string uniqueid2, string uniqueid3, string note)
        {
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "updateappnote");
            sp.AddParameter("uniqueid1", uniqueid1);
            sp.AddParameter("uniqueid2", uniqueid2);
            sp.AddParameter("uniqueid3", uniqueid3);
            sp.AddParameter("note",      note);
            sp.Execute();
        }
        //-----------------------------------------------------------------------------
        public static dynamic GetApplicationOptions(FwSqlConnection conn)
        {
            dynamic result, option;
            FwSqlCommand qry;
            List<string> encryptedOptions;
            string optionsStr, decryptedOption, description, key;
            bool enabled;
            int value;
            IDictionary<String, object> resultDic;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 options");
            qry.Add("from controlclient with(nolock)");
            qry.Add("where controlid = '1'");
            qry.Execute();
            optionsStr = qry.GetField("options").ToString();

            encryptedOptions = new List<string>();
            encryptedOptions.AddRange(optionsStr.Trim().Split(new char[]{'~'}, StringSplitOptions.RemoveEmptyEntries));
            result = new ExpandoObject();
            resultDic = (IDictionary<String, object>)result;
            for (int i = 0; i < encryptedOptions.Count; i++)
            {                    
                decryptedOption        = FwSqlData.Decrypt(conn, encryptedOptions[i]);
                description            = decryptedOption.Substring(0, decryptedOption.Length - 4).ToUpper();
                key = description.Replace("-", "").Replace("_", "").ToLower();
                enabled                = decryptedOption.Substring(decryptedOption.Length - 4, 1).Equals("T");
                value                  = FwConvert.ToInt32(decryptedOption.Substring(decryptedOption.Length - 3, 3));                    
                option = new ExpandoObject();
                option.description = description;
                option.enabled     = enabled;
                option.value       = value;
                resultDic[key]     = option;
            }
            return result;
        }
        //-----------------------------------------------------------------------------
        public class ApplicationOption
        {
            public string Description { get;set; } = string.Empty;
            public bool Enabled { get;set; } = false;
            public  int Value { get;set; } = 0; 
        }
        public static Dictionary<string, ApplicationOption> GetApplicationOptions2(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            List<string> encryptedOptions;
            string optionsStr, decryptedOption, key;
            Dictionary<string, ApplicationOption> options;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 options");
            qry.Add("from controlclient with(nolock)");
            qry.Add("where controlid = '1'");
            qry.Execute();
            optionsStr = qry.GetField("options").ToString();

            encryptedOptions = new List<string>();
            encryptedOptions.AddRange(optionsStr.Trim().Split(new char[]{'~'}, StringSplitOptions.RemoveEmptyEntries));
            options = new Dictionary<string, ApplicationOption>();
            for (int i = 0; i < encryptedOptions.Count; i++)
            {                    
                decryptedOption = FwSqlData.Decrypt(conn, encryptedOptions[i]);
                ApplicationOption option = new ApplicationOption();
                option.Description = decryptedOption.Substring(0, decryptedOption.Length - 4).ToUpper();;
                option.Enabled     = decryptedOption.Substring(decryptedOption.Length - 4, 1).Equals("T");;
                option.Value       = FwConvert.ToInt32(decryptedOption.Substring(decryptedOption.Length - 3, 3));;
                key                = option.Description.Replace("-", "").Replace("_", "").ToLower();                
                options[key]       = option;
            }
            return options;
        }
        //-----------------------------------------------------------------------------
        public class WebInsertAppDocumentResponse
        {
            public string AppDocumentId = string.Empty;
            public string AppImageId    = string.Empty;
        }
        public static WebInsertAppDocumentResponse WebInsertAppDocument(FwSqlConnection conn, string uniqueid1, string uniqueid2, string description, string documenttypeid, string inputbyusersid, byte[] file, string extension)
        {
            WebInsertAppDocumentResponse response = new WebInsertAppDocumentResponse();
            int width = 0, height = 0;
            
            FwSqlCommand sp = new FwSqlCommand(conn, "dbo.webinsertappdocument");
            sp.AddParameter("@uniqueid1",      uniqueid1);
            sp.AddParameter("@uniqueid2",      uniqueid2);
            sp.AddParameter("@description",    description);
            sp.AddParameter("@documenttypeid", documenttypeid);
            sp.AddParameter("@inputbyusersid", inputbyusersid);
            switch (extension.ToUpper())
            {
                case "BMP":
                case "GIF":
                case "JPG":
                case "JPEG":
                case "PNG":
                case "TIF":
                case "TIFF":
                    sp.AddParameter("@image",     FwGraphics.ConvertToJpg(file, ref width, ref height));
                    sp.AddParameter("@thumbnail", FwGraphics.GetJpgThumbnail(file));
                    sp.AddParameter("@extension", "");
                    break;
                case "PDF":
                    sp.AddParameter("@image",     file);
                    sp.AddParameter("@extension", extension.ToUpper());
                    break;
            }
            sp.AddParameter("@appdocumentid", SqlDbType.Char, ParameterDirection.Output, 8);
            sp.AddParameter("@appimageid",    SqlDbType.Char, ParameterDirection.Output, 8);
            sp.Execute();
            response.AppDocumentId  = sp.GetParameter("@appdocumentid").ToString().TrimEnd();
            response.AppImageId     = sp.GetParameter("@appimageid").ToString().TrimEnd();

            return response;
        }
        //-----------------------------------------------------------------------------
        public static string GetDocumentTypeId(FwSqlConnection conn, string documenttype)
        {
            FwSqlCommand qry;
            string documenttypeid = null;
            
            qry= new FwSqlCommand(conn);
            qry.Add("select top 1 documenttypeid");
            qry.Add("from documenttype with(nolock)");
            qry.Add("where documenttype = @documenttype");
            qry.AddParameter("@documenttype", documenttype);
            qry.Execute();
            if (qry.RowCount > 0)
            {
                documenttypeid = qry.GetField("documenttypeid").ToString().TrimEnd();
            }
            return documenttypeid;
        }
        //-----------------------------------------------------------------------------
        public static void InsertDocumentType(FwSqlConnection conn, string documenttypeid, string documenttype, string rowtype, bool floorplan, bool videos, bool panoramic, bool autoattachtoemail)
        {
            FwSqlCommand qry;
            
            qry= new FwSqlCommand(conn);
            qry.Add("insert into documenttype(documenttypeid, documenttype, floorplan, rowtype, videos, panoramic, autoattachtoemail)");
            qry.Add("values(@documenttypeid, @documenttype, @floorplan, @rowtype, @videos, @panoramic, @autoattachtoemail)");
            qry.AddParameter("@documenttypeid",    documenttypeid);
            qry.AddParameter("@documenttype",      documenttype);
            qry.AddParameter("@rowtype",           rowtype);
            qry.AddParameter("@floorplan",         FwConvert.LogicalToCharacter(floorplan));
            qry.AddParameter("@videos",            FwConvert.LogicalToCharacter(videos));
            qry.AddParameter("@panoramic",         FwConvert.LogicalToCharacter(panoramic));
            qry.AddParameter("@autoattachtoemail", FwConvert.LogicalToCharacter(autoattachtoemail));
            qry.Execute();
        }
        //----------------------------------------------------------------------------------------------------
        public static void InsertWebAudit(FwSqlConnection conn, string uniqueid, string webusersid, string createdflag)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("exec insertwebaudit @uniqueid, @webusersid, @createdflag");
            qry.AddParameter("@uniqueid", uniqueid);
            qry.AddParameter("@webusersid", webusersid);
            qry.AddParameter("@createdflag", createdflag);
            qry.Execute();
        }
        //-----------------------------------------------------------------------------
        public static void SetCompanyContactStatus(FwSqlConnection conn, string compcontactid, bool active)
        {
            FwSqlCommand qry;
            string inactive;
            object activedate=null, inactivedate;

            inactive     = active ? "F" : "T";
            if (active)
            {
                inactivedate = DBNull.Value;
                activedate = FwConvert.ToUSShortDate(DateTime.Now);
            }
            else 
            {
                inactivedate = FwConvert.ToUSShortDate(DateTime.Now);
            }
            qry = new FwSqlCommand(conn);
            qry.Add("update compcontact");
            qry.Add("set inactive     = @inactive,");
            qry.Add("    inactivedate = @inactivedate");
            if (active)
            {
                qry.Add(", activedate = @activedate");
                qry.AddParameter("@activedate", activedate);
            }
            qry.Add("where compcontactid = @compcontactid");
            qry.AddParameter("@inactive", inactive);
            qry.AddParameter("@inactivedate", inactivedate);
            qry.AddParameter("@compcontactid", compcontactid);
            qry.Execute();
        }
        //-----------------------------------------------------------------------------
        public static List<dynamic> GetHolidayEvents(FwSqlConnection conn, FwDateTime fromDate, double days, string countryid, string resourceId)
        {
            FwDateTime toDate;
            FwSqlCommand qry;
            DataTable dt;
            List<dynamic> holidays;
            dynamic holiday;
            
            holidays = new List<dynamic>();
            toDate = new FwDateTime(fromDate.ToDateTime().AddDays(days));  
            qry= new FwSqlCommand(conn);
            qry.Add("select holidaydt, description");
            qry.Add("from holiday with(nolock)");
            qry.Add("where holidaydt >= @start");
            qry.Add("  and holidaydt <= @end");
            qry.AddParameter("@start", fromDate.GetSqlValue());
            qry.AddParameter("@end",   toDate.GetSqlValue());
            dt = qry.QueryToTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id, text, start, end;
                    DateTime dtStart, dtEnd;
                    
                    id       = Guid.NewGuid().ToString();
                    text     = (!string.IsNullOrEmpty(dt.Rows[i]["description"].ToString())) ? "Holiday" : dt.Rows[i]["description"].ToString();
                    dtStart  = new FwDatabaseField(dt.Rows[i]["holidaydt"]).ToDateTime();
                    start    = FwConvert.ToUtcIso8601DateTime(dtStart);
                    dtEnd    = dtStart.AddDays(1).AddSeconds(-1);
                    end      = FwConvert.ToUtcIso8601DateTime(dtEnd);

                    holiday = new ExpandoObject();
                    holiday.id       = id;
                    holiday.text     = text;
                    holiday.start    = start;
                    holiday.end      = end;
                    holiday.resource = resourceId;
                    holidays.Add(holiday);
                }
            }

            //qry= new FwSqlCommand(conn);
            //qry.Add("select holidaydate, holiday, observed, countryid, country, holidayid, inactive, custom");
            //qry.Add("from getholidays(@startdate, @enddate, @holidayid, @countryid)");
            //qry.Add("where inactive <> 'T'");;
            //qry.AddParameter("@startdate", fromDate.GetSqlValue());
            //qry.AddParameter("@enddate",   toDate.GetSqlValue());
            //qry.AddParameter("@holidayid", "");
            //qry.AddParameter("@countryid", countryid);
            //dt = qry.QueryToTable();
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string id, text, start, end;
            //        DateTime dtStart, dtEnd;
                    
            //        id       = new Guid().ToString();
            //        text     = (!string.IsNullOrEmpty(dt.Rows[i]["holiday"].ToString())) ? "Holiday" : dt.Rows[i]["description"].ToString();
            //        dtStart  = new FwDatabaseField(dt.Rows[i]["holidaydate"]).ToDateTime();
            //        start    = FwConvert.ToUtcIso8601DateTime(dtStart);
            //        dtEnd    = dtStart.AddDays(1).AddSeconds(-1);
            //        end      = FwConvert.ToUtcIso8601DateTime(dtEnd);

            //        holiday = new ExpandoObject();
            //        holiday.id       = id;
            //        holiday.text     = text;
            //        holiday.start    = start;
            //        holiday.end      = end;
            //        holiday.resource = resourceId;
            //        holidays.Add(holiday);
            //    }
            //}

            return holidays;
        }
        //-----------------------------------------------------------------------------
        public class GetEmailReportControlResponse
        {
            public string accountname {get;set;}
            public string accountpassword {get;set;}
            public string authtype {get;set;}
            public int deletedays {get;set;}
            public string host {get;set;}
            public string pdfpath {get;set;}
            public int port {get;set;}
        }
        public static GetEmailReportControlResponse GetEmailReportControl(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            GetEmailReportControlResponse result;
            
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from emailreportcontrol");
            qry.Execute();
            result = new GetEmailReportControlResponse();
            result.accountname     = qry.GetField("accountname").ToString().TrimEnd();
            result.accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
            result.authtype        = qry.GetField("authtype").ToString().TrimEnd();
            result.deletedays      = qry.GetField("deletedays").ToInt32();
            result.host            = qry.GetField("host").ToString().TrimEnd();
            result.pdfpath         = qry.GetField("pdfpath").ToString().TrimEnd();
            result.port            = qry.GetField("port").ToInt32();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        // filepath is just the fake filepath you get if you take the value attribute on input[type="file"], using it here to get the extension
        public static void UpdateAppDocumentImage(string appdocumentid, string documenttypeid, string uniqueid1, string uniqueid2, 
                                                  int uniqueid1int, string filedataurl, string filepath, FwDateTime received, FwDateTime reviewed, 
                                                  FwDateTime expiration, string inputbyusersid, string contactid, string projectid, string description, 
                                                  bool uploadpending, bool insertNewVersion, string notes)
        {
            FwDateTime datestamp, attachDateTime;
            string appimageid, extension, base64file, parentappdocumentid;
            int width, height, version;
            byte[] fileBuffer, thumbnailBuffer=null;

            //if (documenttypeid == string.Empty) throw new ArgumentException("Document Type is required.");
            datestamp       = DateTime.UtcNow;
            attachDateTime  = DateTime.Now;
            parentappdocumentid = string.Empty;
            version             = 1;
            using(FwSqlConnection conn = new FwSqlConnection(FwSqlConnection.AppDatabase))
            {
                conn.Open();
                using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                {
                    if (string.IsNullOrEmpty(appdocumentid) || (insertNewVersion))
                    {
                        if (insertNewVersion && !string.IsNullOrEmpty(appdocumentid))
                        {
                            using (FwSqlCommand qry = new FwSqlCommand(conn))
                            {
                                qry.Transaction = transaction;
                                qry.Add("select top 1 parentappdocumentid, version");
                                qry.Add("from appdocument with (nolock)");
                                qry.Add("where appdocumentid = @appdocumentid");
                                qry.AddParameter("@appdocumentid", appdocumentid);
                                qry.Execute(false);
                                if (qry.RowCount > 0) {
                                    parentappdocumentid = qry.GetField("parentappdocumentid").ToString().TrimEnd();
                                    if (string.IsNullOrEmpty(parentappdocumentid))
                                    {
                                        parentappdocumentid = appdocumentid;
                                    }
                                    version = qry.GetField("version").ToInt32() + 1;
                                    if (version == 1) version++;
                                }
                            }
                        }
                        appdocumentid = FwSqlData.GetNextId(conn, transaction);
                        //if (string.IsNullOrEmpty(filedataurl)) throw new Exception("File is required.");
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Add("insert appdocument(");
                            cmd.Add("  appdocumentid");
                            if (documenttypeid != null) cmd.Add(", documenttypeid");
                            cmd.Add(", uniqueid1");
                            cmd.Add(", uniqueid2");
                            cmd.Add(", uniqueid1int");
                            cmd.Add(", attachdate");
                            cmd.Add(", attachtime");
                            if (received   != null) cmd.Add(", received");
                            if (reviewed   != null) cmd.Add(", reviewed");
                            if (expiration != null) cmd.Add(", expiration");
                            cmd.Add(", datestamp");
                            if (inputbyusersid != null) cmd.Add(", inputbyusersid");
                            if (contactid      != null) cmd.Add(", contactid");
                            if (projectid      != null) cmd.Add(", projectid");
                            if (description    != null) cmd.Add(", description");
                            if (uploadpending) cmd.Add(", uploadpending");
                            if (notes          != null) cmd.Add(", notes");
                            if (insertNewVersion) cmd.Add(", parentappdocumentid");
                            if (insertNewVersion) cmd.Add(", version");
                            cmd.Add(")");
                            
                            cmd.Add("values(@appdocumentid");
                            cmd.Add(", @documenttypeid");
                            cmd.Add(", @uniqueid1");
                            cmd.Add(", @uniqueid2");
                            cmd.Add(", @uniqueid1int");
                            cmd.Add(", @attachdate");
                            cmd.Add(", @attachtime");
                            if (received   != null) cmd.Add(", @received");
                            if (reviewed   != null) cmd.Add(", @reviewed");
                            if (expiration != null) cmd.Add(", @expiration");
                            cmd.Add(", @datestamp");
                            if (inputbyusersid != null) cmd.Add(", @inputbyusersid");
                            if (contactid      != null) cmd.Add(", @contactid");
                            if (projectid      != null) cmd.Add(", @projectid");
                            if (description    != null) cmd.Add(", @description");
                            if (uploadpending)          cmd.Add(", @uploadpending");
                            if (notes          != null) cmd.Add(", @notes");
                            if (insertNewVersion) cmd.Add(", @parentappdocumentid");
                            if (insertNewVersion) cmd.Add(", @version");
                            cmd.Add(")");
                            
                            cmd.AddParameter("@appdocumentid",  appdocumentid);
                            if (documenttypeid != null) cmd.AddParameter("@documenttypeid", documenttypeid);
                            cmd.AddParameter("@uniqueid1",      uniqueid1);
                            cmd.AddParameter("@uniqueid2",      uniqueid2);
                            cmd.AddParameter("@uniqueid1int",   uniqueid1int);
                            cmd.AddParameter("@attachdate",     attachDateTime.GetSqlDate());
                            cmd.AddParameter("@attachtime",     attachDateTime.GetSqlTime());
                            if (received   != null) cmd.AddParameter("@received",   received.GetSqlDate());
                            if (reviewed   != null) cmd.AddParameter("@reviewed",   reviewed.GetSqlDate());
                            if (expiration != null) cmd.AddParameter("@expiration", expiration.GetSqlDate());
                            cmd.AddParameter("@datestamp",      datestamp.GetSqlValue());
                            if (inputbyusersid != null) cmd.AddParameter("@inputbyusersid", inputbyusersid);
                            if (contactid      != null) cmd.AddParameter("@contactid",      contactid);
                            if (projectid      != null) cmd.AddParameter("@projectid",      projectid);
                            if (description    != null) cmd.AddParameter("@description",    description);
                            if (uploadpending)          cmd.AddParameter("@uploadpending",  "T");
                            if (notes          != null) cmd.AddParameter("@notes",          notes);
                            if (insertNewVersion) cmd.AddParameter("@parentappdocumentid", parentappdocumentid);
                            if (insertNewVersion) cmd.AddParameter("@version", version);
                            cmd.ExecuteNonQuery(false);
                        }
                    }
                    else
                    {
                        // update the attachdate/time on the appdocument
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Add("update appdocument");
                            cmd.Add("set attachdate = @attachdate");
                            cmd.Add("  , attachtime = @attachtime");
                            if (documenttypeid != null) cmd.Add("  , documenttypeid = @documenttypeid");
                            if (received       != null) cmd.Add("  , received       = @received");
                            if (reviewed       != null) cmd.Add("  , reviewed       = @reviewed");
                            if (expiration     != null) cmd.Add("  , expiration     = @expiration");
                            if (description    != null) cmd.Add("  , description    = @description");
                            if (uploadpending)          cmd.Add("  , uploadpending  = @uploadpending");
                            if (notes          != null) cmd.Add("  , notes          = @notes");
                            cmd.Add("where appdocumentid = @appdocumentid");
                            cmd.AddParameter("@appdocumentid",  appdocumentid);
                            if (documenttypeid != null) cmd.AddParameter("@documenttypeid", documenttypeid);
                            cmd.AddParameter("@attachdate",     attachDateTime.GetSqlDate());
                            cmd.AddParameter("@attachtime",     attachDateTime.GetSqlTime());
                            if (received    != null) cmd.AddParameter("@received", received.GetSqlDate());
                            if (reviewed    != null) cmd.AddParameter("@reviewed", reviewed.GetSqlDate());
                            if (expiration  != null) cmd.AddParameter("@expiration", expiration.GetSqlDate());
                            if (description != null) cmd.AddParameter("@description", description);
                            if (uploadpending)       cmd.AddParameter("@uploadpending", "T");
                            if (notes       != null) cmd.AddParameter("@notes", notes);
                            cmd.ExecuteNonQuery(false);
                        }
                    }
                    
                    // if the user didn't update the file no need to mess with appimage
                    if ((filedataurl != null) && (filepath != null))
                    {
                        appimageid = FwSqlData.GetNextId(conn, transaction);
                        // convert filedataurl to binary file
                        width           = 0;
                        height          = 0;
                        extension =  Path.GetExtension(filepath).ToLower();
                        if (extension.StartsWith(".")) 
                        {
                            extension = extension.Substring(1);
                        }
                        base64file = filedataurl.Substring(filedataurl.IndexOf(',') + 1);
                        fileBuffer = Convert.FromBase64String(base64file);
                        switch(extension)
                        {
                            case "jpg":
                            case "jpeg":
                            case "gif":
                            case "png":
                            case "tif":
                            case "tiff":
                            case "bmp":
                                thumbnailBuffer = FwGraphics.GetJpgThumbnail(fileBuffer);
                                using(MemoryStream ms = new MemoryStream(fileBuffer))
                                {
                                    Image image = Image.FromStream(ms);
                                    width  = image.Width;
                                    height = image.Height;
                                }
                                break;
                        }
                        
                        // delete any existing appimages
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Add("delete appimage");
                            cmd.Add("where uniqueid1 = @uniqueid1");
                            cmd.AddParameter("@uniqueid1",  appdocumentid);
                            cmd.ExecuteNonQuery(false);
                        }
                    
                        // insert appimage
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Add("insert into appimage(appimageid, uniqueid1, uniqueid2, uniqueid3, description, extension, rectype, height, width" + ((thumbnailBuffer != null) ? ", thumbnail" : "") + ", image, datestamp)");
                            cmd.Add("values(@appimageid, @uniqueid1, @uniqueid2, @uniqueid3, @description, @extension, @rectype, @height, @width" + ((thumbnailBuffer != null) ? ", @thumbnail" : "") + ", @image, @datestamp)");
                            cmd.AddParameter("@appimageid",  appimageid);
                            cmd.AddParameter("@uniqueid1",   appdocumentid);
                            cmd.AddParameter("@uniqueid2",   string.Empty);
                            cmd.AddParameter("@uniqueid3",   string.Empty);
                            cmd.AddParameter("@description", string.Empty);
                            cmd.AddParameter("@extension",   extension);
                            cmd.AddParameter("@rectype",     "F");
                            cmd.AddParameter("@height",      height);
                            cmd.AddParameter("@width",       width);
                            if (thumbnailBuffer != null)
                            {
                                cmd.AddParameter("@thumbnail",   thumbnailBuffer);
                            }
                            cmd.AddParameter("@image",       fileBuffer);
                            cmd.AddParameter("@datestamp",   datestamp.GetSqlDate());
                            cmd.ExecuteNonQuery(false);
                        }
                    }
                    transaction.Commit();
                }
            }
        }
        //-----------------------------------------------------------------------------
        public static void ToggleAppdocumentInactive(string appdocumentid)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.AppConnection, "dbo.toggleappdocumentinactive");
            sp.AddParameter("@appdocumentid", appdocumentid);
            sp.Execute();
        }
        //-----------------------------------------------------------------------------
        public static void ToggleAppNoteInactive(string appnoteid)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.AppConnection, "dbo.toggleappnoteinactive");
            sp.AddParameter("@appnoteid", appnoteid);
            sp.Execute();
        }
        //-----------------------------------------------------------------------------
        public static FwWebUserSettings GetWebUserSettings(FwSqlConnection conn, string webusersid)
        {
            FwSqlCommand qry;
            DataTable dt;
            FwWebUserSettings control;
            
            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 settings");
            qry.Add("from webusers with(nolock)");
            qry.Add("where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            dt = qry.QueryToTable();
            control = null;
            if (dt.Rows.Count > 0)
            {
                control = new FwWebUserSettings();
                control.LoadSettings(dt.Rows[0]["settings"].ToString().TrimEnd());
            }
            
            return control;
        }
        //-----------------------------------------------------------------------------
        //justin 04/05/2019 RentalWorksWeb#308. query fields from webusers table instead of xml settings
        public static void GetWebUserSettings2019(FwSqlConnection conn, string webusersid, ref string applicationTheme, ref int browseDefaultRows)
        {
            FwSqlCommand qry;
            DataTable dt;

            // default values
            applicationTheme = "theme-material";
            browseDefaultRows = 15; 

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 applicationtheme, browsedefaultrows");
            qry.Add(" from  webusers with (nolock)");
            qry.Add(" where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            dt = qry.QueryToTable();
            if (dt.Rows.Count > 0)
            {
                applicationTheme = dt.Rows[0]["applicationtheme"].ToString().TrimEnd();
                browseDefaultRows = FwConvert.ToInt32(dt.Rows[0]["browsedefaultrows"].ToString().TrimEnd());
            }

            if (!string.IsNullOrEmpty(applicationTheme))
            {
                applicationTheme = "theme-material";
            }
            if (browseDefaultRows == 0)
            {
                browseDefaultRows = 15;
            }

        }
        //-----------------------------------------------------------------------------
        public static string CheckDatabaseVersion(FwSqlConnection conn, string requireddbversion, string requiredhotfixfilename)
        {
            FwSqlCommand qry;
            string dbversion;
            bool hasrequireddbversion, hasrequiredhotfix;
            StringBuilder result;

            result = new StringBuilder();
            qry = new FwSqlCommand(conn);
            qry.Add("select dbversion         = (select rtrim(dbversion)");
            qry.Add("                            from control with (nolock)),");
            qry.Add("       hasrequiredhotfix = case when exists (select *");
            qry.Add("                                             from hotfix with (nolock)");
            qry.Add("                                             where filename = @filename)");
            qry.Add("                                then 'T'");
            qry.Add("                                else 'F'");
            qry.Add("                           end");
            qry.AddParameter("@filename", requiredhotfixfilename);
            qry.Execute();
            dbversion            = qry.GetField("dbversion").ToString();
            hasrequireddbversion = (dbversion == requireddbversion);
            hasrequiredhotfix    = qry.GetField("hasrequiredhotfix").ToBoolean();
            if (!hasrequireddbversion && !string.IsNullOrEmpty(requireddbversion))
            {
                result.Append("<div>Web application and database versions do not match!</div>");
                result.Append("<table style=\"margin:10px 0 0 0;\">");
                result.Append("<tr><td style=\"padding:2px 5px;\">Web Application:</td><td style=\"padding:2px 5px;\">");
                result.Append(requireddbversion);
                result.Append("</td></tr>");
                result.Append("<tr><td style=\"padding:2px 5px;\">Database:</td>");
                result.Append("<td style=\"padding:2px 5px;\">");
                result.Append(dbversion);
                result.Append("</td></tr>");
                result.Append("</table>");
            }
            else if (!hasrequiredhotfix && !string.IsNullOrEmpty(requiredhotfixfilename))
            {
                result.Append("<div>Please update your database hotfixes!</div>");
                result.Append("<div style=\"margin:10px 0 0 0;\">Missing Hotfix: " + requiredhotfixfilename + "</div>");
            }

            return result.ToString();
        }
        //-----------------------------------------------------------------------------
    }
}