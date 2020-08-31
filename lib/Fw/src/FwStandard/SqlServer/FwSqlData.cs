using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.SqlServer
{
    public class FwSqlData
    {
        //-----------------------------------------------------------------------------
        static public async Task<dynamic> UserAuthenticationAsync(SqlServerConfig dbConfig, string username, string password)
        {
            dynamic response = new ExpandoObject();
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                dynamic userauthdata = null;

                //Find User by email or loginname
                using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                {
                    qry.Add("select top 1 webusersid, usersid, password, failedlogins, usertype");
                    qry.Add("  from webauthenticateview with (nolock)");
                    qry.Add(" where inactive    <> 'T'");
                    qry.Add("   and webaccess   =  'T'");
                    qry.Add("   and lockaccount <> 'T'");
                    qry.Add("   and ((email     = @username) or");
                    qry.Add("        (loginname = @username))");
                    qry.AddParameter("@username", username);
                    userauthdata = await qry.QueryToDynamicObject2Async();
                }

                if (userauthdata != null)
                {
                    bool passwordmatch = false;
                    using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                    {
                        qry.Add("select match = (case when (@password = dbo.encrypt(@userpassword)) then 1");
                        qry.Add("                                                                   else 0");
                        qry.Add("               end)");
                        qry.AddParameter("@password",     userauthdata.password);
                        qry.AddParameter("@userpassword", password.ToUpper()); //2020-08-27 MY: Need to figure out a way to not toupper passwords for comparison
                        await qry.ExecuteAsync();
                        passwordmatch = qry.GetField("match").ToBoolean();
                    }

                    if (passwordmatch)
                    {
                        if (userauthdata.failedlogins > 0)
                        {
                            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                            {
                                qry.Add("update users");
                                qry.Add("   set failedloginattempts = 0");
                                qry.Add(" where usersid = @usersid");
                                qry.AddParameter("@usersid",   userauthdata.usersid);
                                await qry.ExecuteAsync();
                            }
                        }

                        response.Status     = 0;
                        response.WebUsersId = userauthdata.webusersid;
                        response.UsersId    = userauthdata.usersid;
                    }
                    else
                    {
                        response.Status = 100;

                        //On failed password increment the failedloginattempts on the account
                        using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                        {
                            qry.Add("update users");
                            qry.Add("   set failedloginattempts = @failedlogins");
                            qry.Add(" where usersid = @usersid");
                            qry.AddParameter("@failedlogins", userauthdata.failedlogins + 1);
                            qry.AddParameter("@usersid",      userauthdata.usersid);
                            await qry.ExecuteAsync();
                        }
                    }
                }
                else
                {
                    response.Status = 100;
                }
            }
            return response;
        }
        //-----------------------------------------------------------------------------
        static public async Task<Boolean> CheckPasswordExpirationAsync(SqlServerConfig dbConfig, string UsersId)
        {
            bool response = false;
            bool changepassword;
            bool expireflag;
            int  expireindays;
            DateTime passwordlastupdated;

            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                {
                    qry.Add("select mustchangepwflg, expireflg, expiredays, pwupdated");
                    qry.Add("  from users u with (nolock)");
                    qry.Add(" where usersid = @usersid");
                    qry.AddParameter("@usersid", UsersId);
                    await qry.ExecuteAsync();
                    changepassword      = qry.GetField("mustchangepwflg").ToBoolean();
                    expireflag          = qry.GetField("expireflg").ToBoolean();
                    expireindays        = qry.GetField("expiredays").ToInt32();
                    passwordlastupdated = qry.GetField("pwupdated").ToDateTime();
                }
            }

            if (changepassword)
            {
                response = true;
            }
            else if (expireflag)
            {
                if (DateTime.Compare(DateTime.Today, passwordlastupdated.AddDays(expireindays)) > 0)
                {
                    response = true;
                }
            }

            return response;
        }
        //-----------------------------------------------------------------------------
        //static public async Task<String> DecryptAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string data)
        //{
        //    using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
        //    {
        //        qry.Add("select value = dbo.decrypt(@data)");
        //        qry.AddParameter("@data", data);
        //        await qry.ExecuteAsync();
        //        string result = qry.GetField("value").ToString().TrimEnd();
        //        return result;
        //    }
        //}
        //-----------------------------------------------------------------------------
        static public async Task<String> EncryptAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string data)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select value = dbo.encrypt(@data)");
                qry.AddParameter("@data", data);
                await qry.ExecuteAsync();
                string value = qry.GetField("value").ToString().Trim();
                return value;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<dynamic> GetWebControlAsync(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select top 1 *");
                qry.Add("from webcontrol with(nolock)");
                qry.Add("where webcontrolid = 1");
                var result = await qry.QueryToDynamicObject2Async();
                return result;
            }
        }
        static public async Task<String> GetClientCodeAsync(SqlServerConfig dbConfig)
        {
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                return await GetClientCodeAsync(conn, dbConfig);
            }
        }
        //-----------------------------------------------------------------------------
        static public async Task<String> GetClientCodeAsync(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select value = dbo.getclientcode()");
                await qry.ExecuteAsync();
                string value = qry.GetField("value").ToString().TrimEnd().ToUpper();
                return value;
            }
        }
        //-----------------------------------------------------------------------------
        static public async Task<String> GetNextIdAsync(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "getnextid", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@id", SqlDbType.Char, ParameterDirection.Output);
                await sp.ExecuteAsync();
                string result = sp.GetParameter("@id").ToString().TrimEnd();
                return result;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<String> GetInternalChar(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "getinternalchar", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@internalchar", SqlDbType.Char, ParameterDirection.Output);
                await sp.ExecuteAsync();
                string result = sp.GetParameter("@internalchar").ToString().TrimEnd();
                return result;
            }
        }
        //-----------------------------------------------------------------------------
        //jh 03/15/2019 commenting this overloaded version of the method. This behavior is now automatic whenever "conn" has an active transaction already started.  see conn.GetActiveTransaction()
        //static public async Task<String> GetNextIdAsync(FwSqlConnection conn, SqlTransaction transaction, SqlServerConfig dbConfig)
        //{
        //    using (FwSqlCommand sp = new FwSqlCommand(conn, "getnextid", dbConfig.QueryTimeout))
        //    {
        //        sp.Transaction = transaction;
        //        sp.AddParameter("@id", SqlDbType.Char, ParameterDirection.Output);
        //        await sp.ExecuteAsync(false);
        //        string result = sp.GetParameter("@id").ToString().TrimEnd();
        //        return result;
        //    }
        //}
        ////-----------------------------------------------------------------------------
        public enum SQLVersions {NotLoaded, Unknown, SQL2000, SQL2005, SQL2008}
        static async Task<SQLVersions> GetSqlVersionAsync(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select ismssql2000 = dbo.fw_funcismssql2000(),");
                qry.Add("       ismssql2005 = dbo.fw_funcismssql2005(),");
                qry.Add("       ismssql2008 = dbo.fw_funcismssql2008()");
                await qry.ExecuteAsync();
                bool ismssql2000 = qry.GetField("ismssql2000").ToBoolean();
                bool ismssql2005 = qry.GetField("ismssql2005").ToBoolean();
                bool ismssql2008 = qry.GetField("ismssql2008").ToBoolean();
                SQLVersions sqlVersion = SQLVersions.NotLoaded;
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
        }
        //-----------------------------------------------------------------------------
        public static async Task<bool> IsSqlVersionGreaterThanOrEqualTo(FwSqlConnection conn, SqlServerConfig dbConfig, int version)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select version = @@version");
                await qry.ExecuteAsync();
                string versionString = qry.GetField("version").ToString().TrimEnd();
                string[] versionParts = versionString.Split(' ');
                int sqlVersion = FwConvert.ToInt32(versionParts[3]);
                bool result = version >= sqlVersion;
                return result;
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<string> GetUsersIdAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string webUsersId)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
                qry.Add("select top 1 usersid");
                qry.Add("from webusers with(nolock)");
                qry.Add("where webusersid = @webusersid");
                qry.AddParameter("@webusersid", webUsersId);
                await qry.ExecuteAsync();
                string usersId = qry.GetField("usersid").ToString().TrimEnd();
                return usersId;
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<string> GetWebUsersIdAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string email)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
                qry.Add("select top 1 webusersid");
                qry.Add("from webusersview with(nolock)");
                qry.Add("where email = @email");
                qry.AddParameter("@email", email);
                await qry.ExecuteAsync();
                string webUsersId = qry.GetField("webusersid").ToString().TrimEnd();
                return webUsersId;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<byte[]> GetAppImageAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string rowGuid)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
                qry.Add("select *");
                qry.Add("from appimage with(nolock)");
                qry.Add("where rowguid = @rowguid");
                qry.AddParameter("@rowguid", rowGuid);
                await qry.ExecuteAsync();
                byte[] image = null;
                if (qry.RowCount > 0)
                {
                    image = qry.GetField("image").ToByteArray();
                }
                return image;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<string> InsertSecurityAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string secDesc, string parentId, int itemType)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "insertsecurity", dbConfig.QueryTimeout))
            { 
                sp.AddParameter("@secdesc", secDesc);
                sp.AddParameter("@parentid", parentId);
                sp.AddParameter("@itemtype", itemType);
                sp.AddParameter("@secid",    System.Data.SqlDbType.Char, System.Data.ParameterDirection.Output);
                await sp.ExecuteAsync();
                string secId = sp.GetParameter("@secid").ToString().TrimEnd();
                return secId;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<bool> IsMSSQL2000Async(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            SQLVersions version = await GetSqlVersionAsync(conn, dbConfig);
            bool result = (version == SQLVersions.SQL2000);
            return result;
        }
        //-----------------------------------------------------------------------------
        public static async Task<bool> IsMSSQL2005Async(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            SQLVersions version = await GetSqlVersionAsync(conn, dbConfig);
            bool result = (version == SQLVersions.SQL2005);
            return result;
        }
        //-----------------------------------------------------------------------------
        public static async Task<bool> IsMSSQL2008Async(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            bool result = false;
            var version = await GetSqlVersionAsync(conn, dbConfig);
            result = (version == SQLVersions.SQL2008);
            return result;
        }
        //-----------------------------------------------------------------------------
        public static async Task LockOutWebUserAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string webUsersId) 
        {
            FwSqlCommand qry;
            
            if (!webUsersId.Equals(string.Empty))
            {
                qry = new FwSqlCommand(conn, dbConfig.QueryTimeout);
                qry.Add("update webusers");
                qry.Add("  set  lockaccount = @lockaccount");
                qry.Add("where  webusersid = @webusersid");
                qry.AddParameter("@lockaccount", "T");
                qry.AddParameter("@webusersid", webUsersId);
                await qry.ExecuteAsync();
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<bool> WebEmailRegisteredAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string email)
        {
            bool registered = false;
            FwSqlCommand sp;

            registered = false;
            email = email.ToUpper().Trim();
            sp = new FwSqlCommand(conn, "webemailregistered", dbConfig.QueryTimeout);
            sp.AddParameter("@email", email);
            sp.AddParameter("@registered", SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            registered = (sp.GetParameter("@registered").ToString().Trim() == "T");
            
            return registered;
        }
        //------------------------------------------------------------------------------
        public class WebGetUsersResult
        {
            public int ErrorNo { get; set; } = 0;
            public string ErrorMessage { get; set; } = string.Empty;
            public string WebUsersId = string.Empty;
        }
        public static async Task<WebGetUsersResult> WebGetUsersAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string email, string password)
        {
            string webpassword, encryptedwebpassword;
            webpassword =  password.ToUpper();
            encryptedwebpassword = await FwSqlData.EncryptAsync(conn, dbConfig, webpassword);
            using (FwSqlCommand sp = new FwSqlCommand(conn, "webgetusers2", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@userlogin", email);
                sp.AddParameter("@userloginpassword", encryptedwebpassword);
                sp.AddParameter("@webusersid",        SqlDbType.NVarChar, ParameterDirection.Output);
                sp.AddParameter("@errno",             SqlDbType.Int,      ParameterDirection.Output);
                sp.AddParameter("@errmsg",            SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                WebGetUsersResult result = new WebGetUsersResult();
                result.WebUsersId   = sp.GetParameter("@webusersid").ToString().TrimEnd();
                result.ErrorNo      = sp.GetParameter("@errno").ToInt32();
                result.ErrorMessage = sp.GetParameter("@errmsg").ToString().TrimEnd();
                return result;
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<string> WebGetUserTmpPasswordAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string usersEmail, string webPassword)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "webgetuserstmppassword", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@usersemail", usersEmail);
                sp.AddParameter("@webpassword", webPassword.ToUpper().Trim());
                sp.AddParameter("@webusersid",  SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                string result = sp.GetParameter("@webusersid").ToString().TrimEnd();
                return result;
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<string> WebRegisterUserAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string usersEmail)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "webregisteruser", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@usersemail", SqlDbType.NVarChar, ParameterDirection.Input, usersEmail);
                sp.AddParameter("@errno",      SqlDbType.Int,      ParameterDirection.Output);
                sp.AddParameter("@errmsg",     SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                string result = sp.GetParameter("@errmsg").ToString().TrimEnd();
                return result;
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<string> WebUserResetPasswordAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string usersEmail)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "webuserresetpassword", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@usersemail", SqlDbType.NVarChar, ParameterDirection.Input, usersEmail);
                sp.AddParameter("@errno",      SqlDbType.Int,      ParameterDirection.Output);
                sp.AddParameter("@errmsg",     SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                string result = sp.GetParameter("@errmsg").ToString().TrimEnd();
                return result;
            }
        }
        //------------------------------------------------------------------------------
        public static async Task<string> WebUsersSetPasswordAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string webUsersId, string webPassword, string clearTmpPassword)
        {
            webPassword = webPassword.ToUpper().Trim();
            webPassword = await FwSqlData.EncryptAsync(conn, dbConfig, webPassword);
            using (FwSqlCommand sp = new FwSqlCommand(conn, "webuserssetpassword", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@webusersid", SqlDbType.NVarChar, ParameterDirection.Input, webUsersId);
                sp.AddParameter("@webpassword",      SqlDbType.NVarChar, ParameterDirection.Input, webPassword);
                sp.AddParameter("@cleartmppassword", SqlDbType.NVarChar, ParameterDirection.Input, clearTmpPassword);
                sp.AddParameter("@errno",            SqlDbType.Int,      ParameterDirection.Output, "");
                sp.AddParameter("@errmsg",           SqlDbType.NVarChar, ParameterDirection.Output, "");
                await sp.ExecuteAsync();
                string result = sp.GetParameter("@errmsg").ToString().TrimEnd();
                return result;
            }
        }
        //------------------------------------------------------------------------------
        public class WebValidatePasswordResult
        {
            public int ErrorNo { get; set; } = 0;
            public string ErrorMessage { get; set; } = string.Empty;
            public bool IsValid { get; set; } = false;
        }
        //------------------------------------------------------------------------------
        public static async Task<WebValidatePasswordResult> WebValidatePasswordAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string webPassword)
        {
            WebValidatePasswordResult result = new WebValidatePasswordResult();
            webPassword = webPassword.ToUpper().Trim();
            using (FwSqlCommand sp = new FwSqlCommand(conn, "webvalidatepassword", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@webpassword", SqlDbType.Char, ParameterDirection.Input, webPassword);
                sp.AddParameter("@validpassword", SqlDbType.Char,    ParameterDirection.Output);
                sp.AddParameter("@errno",         SqlDbType.Int,     ParameterDirection.Output);
                sp.AddParameter("@errmsg",        SqlDbType.VarChar, ParameterDirection.Output);
                try
                {
                    await sp.ExecuteNonQueryAsync();
                    result.IsValid = sp.GetParameter("@validpassword").ToBoolean();
                    result.ErrorNo   = sp.GetParameter("@errno").ToInt32();
                    result.ErrorMessage  = sp.GetParameter("@errmsg").ToString().TrimEnd();
                }
                catch(Exception ex)
                {
                    result.ErrorNo = 1004;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task UpdateAppNoteAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string uniqueid1, string uniqueid2, string uniqueid3, string note)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "updateappnote", dbConfig.QueryTimeout))
            {
                sp.AddParameter("uniqueid1", uniqueid1);
                sp.AddParameter("uniqueid2", uniqueid2);
                sp.AddParameter("uniqueid3", uniqueid3);
                sp.AddParameter("note",      note);
                await sp.ExecuteAsync();
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<dynamic> GetApplicationOptionsAsync(SqlServerConfig dbConfig)
        {
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                return await GetApplicationOptionsAsync(conn, dbConfig);
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<dynamic> GetApplicationOptionsAsync(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            dynamic result, option;
            List<string> encryptedOptions, decryptedOptions = new List<string>();
            string optionsStr, decryptedOption, description, key;
            bool enabled;
            int value;
            IDictionary<String, object> resultDic;

            result = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select top 1 options");
                qry.Add("from controlclient with(nolock)");
                qry.Add("where controlid = '1'");
                await qry.ExecuteAsync();
                optionsStr = qry.GetField("options").ToString();

                if (!optionsStr.Equals(string.Empty))
                {

                    encryptedOptions = new List<string>();
                    encryptedOptions.AddRange(optionsStr.Trim().Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries));

                    using (FwSqlCommand qry2 = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                    {
                        qry2.Add("select");
                        for (int i = 0; i < encryptedOptions.Count; i++)
                        {
                            string separator = (i == 0) ? "  " : " ,";
                            qry2.Add($"{separator}option{i} = dbo.decrypt(@option{i})");
                            qry2.AddParameter($"@option{i}", encryptedOptions[i]);
                            qry2.Parameters[i].SqlDbType = SqlDbType.NVarChar;
                        }
                        await qry2.ExecuteAsync();
                        for (int i = 0; i < encryptedOptions.Count; i++)
                        {
                            decryptedOptions.Add(qry2.GetField($"option{i}").ToString().TrimEnd());
                        }
                    }

                    resultDic = (IDictionary<String, object>)result;
                    for (int i = 0; i < encryptedOptions.Count; i++)
                    {
                        //decryptedOption        = await FwSqlData.DecryptAsync(conn, dbConfig, encryptedOptions[i]);
                        decryptedOption = decryptedOptions[i];
                        description = decryptedOption.Substring(0, decryptedOption.Length - 4).ToUpper();
                        //description = description.Replace("3", "THREE");  //justin 07/19/2019 temporary 3weekpricing --> threeweekpricing  
                        key = description.Replace("-", "").Replace("_", "").ToLower();
                        enabled = decryptedOption.Substring(decryptedOption.Length - 4, 1).Equals("T");
                        value = FwConvert.ToInt32(decryptedOption.Substring(decryptedOption.Length - 3, 3));
                        option = new ExpandoObject();
                        option.description = description;
                        option.enabled = enabled;
                        option.value = value;
                        resultDic[key] = option;
                    }
                }
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
        public static async Task<Dictionary<string, ApplicationOption>> GetApplicationOptions2Async(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            List<string> encryptedOptions, decryptedOptions = new List<string>();
            string optionsStr, decryptedOption, key;
            Dictionary<string, ApplicationOption> options = new Dictionary<string, ApplicationOption>();
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select top 1 options");
                qry.Add("from controlclient with(nolock)");
                qry.Add("where controlid = '1'");
                await qry.ExecuteAsync();
                optionsStr = qry.GetField("options").ToString();
                if (!optionsStr.Equals(string.Empty))
                {
                    encryptedOptions = new List<string>();
                    encryptedOptions.AddRange(optionsStr.Trim().Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries));
                    //options = new Dictionary<string, ApplicationOption>();
                    if (encryptedOptions.Count > 0)
                    {
                        using (FwSqlCommand qry2 = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                        {
                            qry2.Add("select");
                            for (int i = 0; i < encryptedOptions.Count; i++)
                            {
                                string separator = (i == 0) ? "  " : " ,";
                                qry2.Add($"{separator}option{i} = dbo.decrypt(@option{i})");
                                qry2.AddParameter($"@option{i}", encryptedOptions[i]);
                                qry2.Parameters[i].SqlDbType = SqlDbType.NVarChar;
                            }
                            await qry2.ExecuteAsync();
                            for (int i = 0; i < encryptedOptions.Count; i++)
                            {
                                decryptedOptions.Add(qry2.GetField($"option{i}").ToString().TrimEnd());
                            }
                        }
                        for (int i = 0; i < encryptedOptions.Count; i++)
                        {
                            //decryptedOption = await FwSqlData.DecryptAsync(conn, dbConfig, encryptedOptions[i]);
                            decryptedOption = decryptedOptions[i];
                            ApplicationOption option = new ApplicationOption();
                            option.Description = decryptedOption.Substring(0, decryptedOption.Length - 4).ToUpper(); ;
                            option.Enabled = decryptedOption.Substring(decryptedOption.Length - 4, 1).Equals("T"); ;
                            option.Value = FwConvert.ToInt32(decryptedOption.Substring(decryptedOption.Length - 3, 3)); ;
                            key = option.Description.Replace("-", "").Replace("_", "").ToLower();
                            options[key] = option;
                        }
                    }
                }
                return options;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<string> GetDocumentTypeIdAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string documenttype)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("select top 1 documenttypeid");
                qry.Add("from documenttype with(nolock)");
                qry.Add("where documenttype = @documenttype");
                qry.AddParameter("@documenttype", documenttype);
                await qry.ExecuteAsync();
                string documenttypeid = null;
                if (qry.RowCount > 0)
                {
                    documenttypeid = qry.GetField("documenttypeid").ToString().TrimEnd();
                }
                return documenttypeid;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task InsertDocumentTypeAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string documenttypeid, string documenttype, string rowtype, bool floorplan, bool videos, bool panoramic, bool autoattachtoemail)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            {
                qry.Add("insert into documenttype(documenttypeid, documenttype, floorplan, rowtype, videos, panoramic, autoattachtoemail)");
                qry.Add("values(@documenttypeid, @documenttype, @floorplan, @rowtype, @videos, @panoramic, @autoattachtoemail)");
                qry.AddParameter("@documenttypeid", documenttypeid);
                qry.AddParameter("@documenttype", documenttype);
                qry.AddParameter("@rowtype", rowtype);
                qry.AddParameter("@floorplan", FwConvert.LogicalToCharacter(floorplan));
                qry.AddParameter("@videos", FwConvert.LogicalToCharacter(videos));
                qry.AddParameter("@panoramic", FwConvert.LogicalToCharacter(panoramic));
                qry.AddParameter("@autoattachtoemail", FwConvert.LogicalToCharacter(autoattachtoemail));
                await qry.ExecuteAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static async Task InsertWebAuditAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string uniqueid, string webusersid, string createdflag)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
                qry.Add("exec insertwebaudit @uniqueid, @webusersid, @createdflag");
                qry.AddParameter("@uniqueid", uniqueid);
                qry.AddParameter("@webusersid", webusersid);
                qry.AddParameter("@createdflag", createdflag);
                await qry.ExecuteAsync();
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task SetCompanyContactStatusAwait(FwSqlConnection conn, SqlServerConfig dbConfig, string compcontactid, bool active)
        {
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
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
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
                await qry.ExecuteAsync();
            }
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
        public static async Task<GetEmailReportControlResponse> GetEmailReportControlAsync(FwSqlConnection conn, SqlServerConfig dbConfig)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
                qry.Add("select top 1 *");
                qry.Add("from emailreportcontrol");
                await qry.ExecuteAsync();
                var result = new GetEmailReportControlResponse();
                result.accountname     = qry.GetField("accountname").ToString().TrimEnd();
                result.accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
                result.authtype        = qry.GetField("authtype").ToString().TrimEnd();
                result.deletedays      = qry.GetField("deletedays").ToInt32();
                result.host            = qry.GetField("host").ToString().TrimEnd();
                result.pdfpath         = qry.GetField("pdfpath").ToString().TrimEnd();
                result.port            = qry.GetField("port").ToInt32();
                return result;
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task ToggleAppdocumentInactiveAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string appdocumentid)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.toggleappdocumentinactive", dbConfig.QueryTimeout))
            { 
                sp.AddParameter("@appdocumentid", appdocumentid);
                await sp.ExecuteAsync();
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task ToggleAppNoteInactiveAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string appnoteid)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.toggleappnoteinactive", dbConfig.QueryTimeout))
            { 
                sp.AddParameter("@appnoteid", appnoteid);
                await sp.ExecuteAsync();
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<string> CheckDatabaseVersionAsync(FwSqlConnection conn, SqlServerConfig dbConfig, string requireddbversion, string requiredhotfixfilename)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
            { 
                qry.Add("select dbversion         = (select rtrim(dbversion)");
                qry.Add("                            from control with (nolock)),");
                qry.Add("       hasrequiredhotfix = case when exists (select *");
                qry.Add("                                             from hotfix with (nolock)");
                qry.Add("                                             where filename = @filename)");
                qry.Add("                                then 'T'");
                qry.Add("                                else 'F'");
                qry.Add("                           end");
                qry.AddParameter("@filename", requiredhotfixfilename);
                await qry.ExecuteAsync();
                string dbversion          = qry.GetField("dbversion").ToString();
                bool hasrequireddbversion = (dbversion == requireddbversion);
                bool hasrequiredhotfix    = qry.GetField("hasrequiredhotfix").ToBoolean();
                StringBuilder result = new StringBuilder();
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
        }
        //-----------------------------------------------------------------------------
        static public async Task<bool> IsTraining(SqlServerConfig dbConfig)
        {
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                {
                    qry.Add("select value = dbo.fw_istrainingdb()");
                    await qry.ExecuteAsync();
                    string value = qry.GetField("value").ToString().TrimEnd().ToUpper();
                    return FwConvert.ToBoolean(value);
                }
            }
        }
        //-----------------------------------------------------------------------------
        static public async Task<bool> StartMeter(FwSqlConnection conn, SqlServerConfig dbConfig, string sessionId, string caption, int steps)
        {
            bool success = false;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.insertspmeter", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@sessionid", sessionId);
                sp.AddParameter("@caption", caption);
                sp.AddParameter("@totalsteps", steps);
                await sp.ExecuteAsync();
                success = true;
            }
            return success;
        }
        //-----------------------------------------------------------------------------
        static public async Task<bool> StepMeter(FwSqlConnection conn, SqlServerConfig dbConfig, string sessionId, string newCaption = "", int steps = 1)
        {
            bool success = false;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.stepspmeter", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@sessionid", sessionId);
                sp.AddParameter("@newcaption", newCaption);
                sp.AddParameter("@steps", steps);
                await sp.ExecuteAsync();
                success = true;
            }
            return success;
        }
        //-----------------------------------------------------------------------------
        static public async Task<bool> FinishMeter(FwSqlConnection conn, SqlServerConfig dbConfig, string sessionId)
        {
            bool success = false;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.finishspmeter", dbConfig.QueryTimeout))
            {
                sp.AddParameter("@sessionid", sessionId);
                await sp.ExecuteAsync();
                success = true;
            }
            return success;
        }
        //-----------------------------------------------------------------------------
    }
}