using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebLibrary;

namespace WebApi.Logic
{

    public class TSpStatusReponse
    {
        public int status = 0;
        public bool success = false;
        public string msg = "";
    }

    public static class AppFunc
    {

        //-------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the next internalid from controlserver
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public static async Task<string> GetNextIdAsync(FwApplicationConfig appConfig, FwSqlConnection conn = null)
        {
            string id = "";
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            id = await FwSqlData.GetNextIdAsync(conn, appConfig.DatabaseSettings);
            return id;
        }
        //-------------------------------------------------------------------------------------------------------
        static public async Task<string> EncryptAsync(FwApplicationConfig appConfig, string data)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select value = dbo.encrypt(@data)");
                    qry.AddParameter("@data", data);
                    await qry.ExecuteAsync();
                    string value = qry.GetField("value").ToString().Trim();
                    return value;
                }
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<FwDatabaseField[]> GetDataAsync(FwApplicationConfig appConfig, string tablename, string[] wherecolumns, string[] wherecolumnvalues, string[] selectcolumns)
        {
            FwDatabaseField[] results;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 ");
                for (int c = 0; c < selectcolumns.Length; c++)
                {
                    qry.Add(selectcolumns[c]);
                    if (c < (selectcolumns.Length - 1))
                    {
                        qry.Add(",");
                    }
                }
                qry.Add("from " + tablename + " with (nolock)");

                qry.Add("where ");
                for (int c = 0; c < wherecolumns.Length; c++)
                {
                    qry.Add(wherecolumns[c] + " = @wherecolumnvalue" + c.ToString());
                    if (c < (wherecolumns.Length - 1))
                    {
                        qry.Add(" and ");
                    }
                }

                for (int c = 0; c < wherecolumnvalues.Length; c++)
                {
                    qry.AddParameter("@wherecolumnvalue" + c.ToString(), wherecolumnvalues[c]);
                }

                await qry.ExecuteAsync();
                results = new FwDatabaseField[selectcolumns.Length];  // array of nulls

                if (qry.RowCount == 1)
                {
                    for (int c = 0; c < selectcolumns.Length; c++)
                    {
                        results[c] = qry.GetField(selectcolumns[c]);
                    }
                }
            }

            return results;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string[]> GetStringDataAsync(FwApplicationConfig appConfig, string tablename, string[] wherecolumns, string[] wherecolumnvalues, string[] selectcolumns)
        {
            FwDatabaseField[] results = await GetDataAsync(appConfig, tablename, wherecolumns, wherecolumnvalues, selectcolumns);
            string[] resultsStr = new string[results.Length];

            for (int c = 0; c < selectcolumns.Length; c++)
            {
                resultsStr[c] = (results[c] != null) ? results[c].ToString().TrimEnd() : string.Empty;
            }
            return resultsStr;
        }
        //-----------------------------------------------------------------------------
        public static async Task<FwDatabaseField> GetDataAsync(FwApplicationConfig appConfig, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField result;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 " + selectcolumn);
                qry.Add("from " + tablename + " with (nolock)");
                qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
                qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
                await qry.ExecuteAsync();
                result = (qry.RowCount == 1) ? qry.GetField(selectcolumn) : null;
            }

            return result;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetStringDataAsync(FwApplicationConfig appConfig, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            string result = string.Empty;

            field = await GetDataAsync(appConfig, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = (field != null) ? field.ToString().TrimEnd() : string.Empty;

            return result;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<int> GetIntDataAsync(FwApplicationConfig appConfig, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            int result = 0;

            field = await GetDataAsync(appConfig, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = ((field != null) ? field.ToInt32() : 0);

            return result;
        }
        //-------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the next counter from the "syscontrol" table based on the counter column name provided
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="userSession"></param>
        /// <param name="counterColumnName"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public static async Task<string> GetNextSystemCounterAsync(FwApplicationConfig appConfig, FwUserSession userSession, string counterColumnName, FwSqlConnection conn = null)
        {
            string counter = "";
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "fw_getnextcounter", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@tablename", SqlDbType.NVarChar, ParameterDirection.Input, "syscontrol");
            qry.AddParameter("@columnname", SqlDbType.NVarChar, ParameterDirection.Input, counterColumnName);
            qry.AddParameter("@uniqueid1name", SqlDbType.NVarChar, ParameterDirection.Input, "controlid");
            qry.AddParameter("@uniqueid1valuestr", SqlDbType.NVarChar, ParameterDirection.Input, "1");
            qry.AddParameter("@counter", SqlDbType.Int, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            counter = qry.GetParameter("@counter").ToString().TrimEnd();
            return counter;
        }
        //-------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the next counter from the "location" table for the moduleName provided.  based on User's Location, or specific Location if provided.
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="userSession"></param>
        /// <param name="moduleName"></param>
        /// <param name="locationId"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public static async Task<string> GetNextModuleCounterAsync(FwApplicationConfig appConfig, FwUserSession userSession, string moduleName, string locationId = "", FwSqlConnection conn = null)
        {
            string counter = "";
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "getnextcounter", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@module", SqlDbType.NVarChar, ParameterDirection.Input, moduleName);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, locationId);
            qry.AddParameter("@newcounter", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            counter = qry.GetParameter("@newcounter").ToString().TrimEnd();
            return counter;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> SaveNoteASync(FwApplicationConfig appConfig, FwUserSession userSession, string uniqueId1, string uniqueId2, string uniqueId3, string note)
        {
            bool saved = false;
            if (note != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "updateappnote", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, uniqueId1);
                    qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, uniqueId2);
                    qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, uniqueId3);
                    qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, note);
                    await qry.ExecuteNonQueryAsync();
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Applies the current tax rates to active quotes, orders, po's, etc that use this taxOptionId
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="userSession"></param>
        /// <param name="taxOptionId"></param>
        /// <param name="taxId"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public static async Task<bool> UpdateTaxFromTaxOptionASync(FwApplicationConfig appConfig, FwUserSession userSession, string taxOptionId, string taxId, FwSqlConnection conn = null)
        {
            bool saved = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "updatetaxfromtaxoption", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@taxoptionid", SqlDbType.NVarChar, ParameterDirection.Input, taxOptionId);
            qry.AddParameter("@taxid", SqlDbType.NVarChar, ParameterDirection.Input, taxId);
            await qry.ExecuteNonQueryAsync();
            saved = true;
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetDepartmentLocation(FwApplicationConfig appConfig, FwUserSession userSession, string departmentId, string locationId, string fieldName)
        {
            string str = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select " + fieldName);
                qry.Add(" from  deptloc dl ");
                qry.Add(" where dl.departmentid = @departmentid");
                qry.Add(" and   dl.locationid = @locationid");
                qry.AddParameter("@departmentid", departmentId);
                qry.AddParameter("@locationid", locationId);
                qry.AddColumn(fieldName);
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    str = table.Rows[0][0].ToString();
                }
            }
            return str;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetLocation(FwApplicationConfig appConfig, FwUserSession userSession, string locationId, string fieldName)
        {
            string str = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select " + fieldName);
                qry.Add(" from  location l ");
                qry.Add(" where l.locationid = @locationid");
                qry.AddParameter("@locationid", locationId);
                qry.AddColumn(fieldName);
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    str = table.Rows[0][0].ToString();
                }
            }
            return str;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> IsDbWorksUser(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            bool isDbWorks = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string email = await FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", userSession.WebUsersId, "email");
                isDbWorks = email.Contains("@dbworks.com");
            }
            return isDbWorks;
        }
        //-------------------------------------------------------------------------------------------------------
        public static string GetCompanyTypeColor(string companyType)
        {
            string companyTypeColor = null;
            switch (companyType)
            {
                case RwConstants.COMPANY_TYPE_LEAD:
                    companyTypeColor = RwGlobals.COMPANY_TYPE_LEAD_COLOR;
                    break;
                case RwConstants.COMPANY_TYPE_PROSPECT:
                    companyTypeColor = RwGlobals.COMPANY_TYPE_PROSPECT_COLOR;
                    break;
                case RwConstants.COMPANY_TYPE_CUSTOMER:
                    companyTypeColor = RwGlobals.COMPANY_TYPE_CUSTOMER_COLOR;
                    break;
                case RwConstants.COMPANY_TYPE_DEAL:
                    companyTypeColor = RwGlobals.COMPANY_TYPE_DEAL_COLOR;
                    break;
                case RwConstants.COMPANY_TYPE_VENDOR:
                    companyTypeColor = RwGlobals.COMPANY_TYPE_VENDOR_COLOR;
                    break;
            }
            return companyTypeColor;
        }
        //-------------------------------------------------------------------------------------------------------
        public static string GetItemClassICodeColor(string itemClass)
        {
            string iCodeColor = null;
            switch (itemClass)
            {
                case RwConstants.ITEMCLASS_COMPLETE:
                case RwConstants.ITEMCLASS_COMPLETE_ITEM:
                case RwConstants.ITEMCLASS_COMPLETE_OPTION:
                    iCodeColor = RwGlobals.COMPLETE_COLOR;
                    break;
                case RwConstants.ITEMCLASS_KIT:
                case RwConstants.ITEMCLASS_KIT_ITEM:
                case RwConstants.ITEMCLASS_KIT_OPTION:
                    iCodeColor = RwGlobals.KIT_COLOR;
                    break;
                case RwConstants.ITEMCLASS_CONTAINER:
                case RwConstants.ITEMCLASS_CONTAINER_ITEM:
                case RwConstants.ITEMCLASS_CONTAINER_OPTION:
                    iCodeColor = RwGlobals.CONTAINER_COLOR;
                    break;
                case RwConstants.ITEMCLASS_MISCELLANEOUS:
                    iCodeColor = RwGlobals.MISCELLANEOUS_COLOR;
                    break;
            }
            return iCodeColor;
        }
        //-------------------------------------------------------------------------------------------------------
        public static string GetItemClassDescriptionColor(string itemClass)
        {
            string descriptionColor = null;
            switch (itemClass)
            {
                case RwConstants.ITEMCLASS_COMPLETE:
                    descriptionColor = RwGlobals.COMPLETE_COLOR;
                    break;
                case RwConstants.ITEMCLASS_KIT:
                    descriptionColor = RwGlobals.KIT_COLOR;
                    break;
                case RwConstants.ITEMCLASS_CONTAINER:
                    descriptionColor = RwGlobals.CONTAINER_COLOR;
                    break;
            }
            return descriptionColor;
        }
        //-------------------------------------------------------------------------------------------------------
        public static string GetInventoryRecTypeColor(string recType)
        {
            string recTypeColor = null;
            switch (recType)
            {
                case RwConstants.INVENTORY_AVAILABLE_FOR_SALE:
                    recTypeColor = RwGlobals.INVENTORY_AVAILABLE_FOR_SALE_COLOR;
                    break;
                case RwConstants.INVENTORY_AVAILABLE_FOR_PARTS:
                    recTypeColor = RwGlobals.INVENTORY_AVAILABLE_FOR_PARTS_COLOR;
                    break;
            }
            return recTypeColor;
        }
        //-------------------------------------------------------------------------------------------------------
        public static string GetReceiptRecTypeColor(string recType)
        {
            string recTypeColor = null;
            switch (recType)
            {
                case RwConstants.RECEIPT_RECTYPE_OVERPAYMENT:
                    recTypeColor = RwGlobals.RECEIPT_RECTYPE_OVERPAYMENT_COLOR;
                    break;
                case RwConstants.RECEIPT_RECTYPE_DEPLETING_DEPOSIT:
                    recTypeColor = RwGlobals.RECEIPT_RECTYPE_DEPLETING_DEPOSIT_COLOR;
                    break;
                case RwConstants.RECEIPT_RECTYPE_REFUND:
                    recTypeColor = RwGlobals.RECEIPT_RECTYPE_REFUND_CHECK_COLOR;
                    break;
                case RwConstants.RECEIPT_RECTYPE_NSF_ADJUSTMENT:
                    recTypeColor = RwGlobals.RECEIPT_RECTYPE_NSF_ADJUSTMENT_COLOR;
                    break;
                case RwConstants.RECEIPT_RECTYPE_WRITE_OFF:
                    recTypeColor = RwGlobals.RECEIPT_RECTYPE_WRITE_OFF_COLOR;
                    break;
                case RwConstants.RECEIPT_RECTYPE_CREDIT_MEMO:
                    recTypeColor = RwGlobals.RECEIPT_RECTYPE_CREDIT_MEMO_COLOR;
                    break; 
            }
            return recTypeColor;
        }
        //-------------------------------------------------------------------------------------------------------
        public class SessionLocation
        {
            public string locationid { get; set; } = string.Empty;
            public string location { get; set; } = string.Empty;
            public string companyname { get; set; } = string.Empty;
            public string locationcolor { get; set; } = string.Empty;
            public string ratetype { get; set; } = string.Empty;
        }
        public static async Task<SessionLocation> GetSessionLocation(FwApplicationConfig appConfig, string locationid)
        {
            var response = new SessionLocation();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select locationid, location, locationcolor, company, ratetype");
                    qry.Add("from location with (nolock)");
                    qry.Add("where locationid = @locationid");
                    qry.AddParameter("@locationid", locationid);
                    await qry.ExecuteAsync();
                    response.locationid = qry.GetField("locationid").ToString().TrimEnd();
                    response.location = qry.GetField("location").ToString().TrimEnd();
                    response.companyname = qry.GetField("company").ToString().TrimEnd();
                    response.locationcolor = qry.GetField("locationcolor").ToHtmlColor();
                    response.ratetype = qry.GetField("ratetype").ToString().TrimEnd();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public class SessionWarehouse
        {
            public string warehouseid { get; set; } = string.Empty;
            public string warehouse { get; set; } = string.Empty;
            public bool promptforcheckoutexceptions { get; set; } = true;
            public bool promptforcheckinexceptions { get; set; } = true;
        }
        public static async Task<SessionWarehouse> GetSessionWarehouse(FwApplicationConfig appConfig, string warehouseid)
        {
            var response = new SessionWarehouse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select warehouseid, warehouse, promptforcheckoutexceptions, promptforcheckinexceptions");
                    qry.Add("from warehouse with (nolock)");
                    qry.Add("where warehouseid = @warehouseid");
                    qry.AddParameter("@warehouseid", warehouseid);
                    await qry.ExecuteAsync();
                    response.warehouseid = qry.GetField("warehouseid").ToString().TrimEnd();
                    response.warehouse = qry.GetField("warehouse").ToString().TrimEnd();
                    response.promptforcheckoutexceptions = qry.GetField("promptforcheckoutexceptions").ToBoolean();
                    response.promptforcheckinexceptions = qry.GetField("promptforcheckinexceptions").ToBoolean();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public class SessionDepartment
        {
            public string departmentid { get; set; } = string.Empty;
            public string department { get; set; } = string.Empty;
        }
        public static async Task<SessionDepartment> GetSessionDepartment(FwApplicationConfig appConfig, string departmentid)
        {
            var response = new SessionDepartment();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select departmentid, department");
                    qry.Add("from department with (nolock)");
                    qry.Add("where departmentid = @departmentid");
                    qry.AddParameter("@departmentid", departmentid);
                    await qry.ExecuteAsync();
                    response.departmentid = qry.GetField("departmentid").ToString().TrimEnd();
                    response.department = qry.GetField("department").ToString().TrimEnd();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public class SessionUser
        {
            public string webusersid { get; set; } = string.Empty;
            public string usersid { get; set; } = string.Empty;
            public string usertype { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
            public string fullname { get; set; } = string.Empty;
            public string name { get; set; } = string.Empty;
            public string browsedefaultrows { get; set; } = string.Empty;
            public string applicationtheme { get; set; } = string.Empty;
            public string locationid { get; set; } = string.Empty;
            public string location { get; set; } = string.Empty;
            public string warehouseid { get; set; } = string.Empty;
            public string warehouse { get; set; } = string.Empty;
            public string departmentid { get; set; } = string.Empty;
            public string department { get; set; } = string.Empty;

        }
        public static async Task<SessionUser> GetSessionUser(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            var response = new SessionUser();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select webusersid, usersid, usertype, email, fullname, name, browsedefaultrows, applicationtheme, locationid, location, warehouseid, warehouse, departmentid, department");
                    qry.Add("from webusersview with (nolock)");
                    qry.Add("where webusersid = @webusersid");
                    qry.AddParameter("@webusersid", userSession.WebUsersId);
                    await qry.ExecuteAsync();
                    response.webusersid = qry.GetField("webusersid").ToString().TrimEnd();
                    response.usersid = qry.GetField("usersid").ToString().TrimEnd();
                    response.usertype = qry.GetField("usertype").ToString().TrimEnd();
                    response.email = qry.GetField("email").ToString().TrimEnd();
                    response.fullname = qry.GetField("fullname").ToString().TrimEnd();
                    response.name = qry.GetField("name").ToString().TrimEnd();
                    response.browsedefaultrows = qry.GetField("browsedefaultrows").ToString().TrimEnd();
                    response.applicationtheme = qry.GetField("applicationtheme").ToString().TrimEnd();
                    if (string.IsNullOrEmpty(response.applicationtheme))
                    {
                        response.applicationtheme = "theme-material";
                    }
                    response.locationid = qry.GetField("locationid").ToString().TrimEnd();
                    response.location = qry.GetField("location").ToString().TrimEnd();
                    response.warehouseid = qry.GetField("warehouseid").ToString().TrimEnd();
                    response.warehouse = qry.GetField("warehouse").ToString().TrimEnd();
                    response.departmentid = qry.GetField("departmentid").ToString().TrimEnd();
                    response.department = qry.GetField("department").ToString().TrimEnd();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
