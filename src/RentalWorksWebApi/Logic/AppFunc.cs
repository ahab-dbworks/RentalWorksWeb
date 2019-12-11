using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebApi;
using WebApi.Modules.HomeControls.InventoryAvailability;

namespace WebApi.Logic
{

    public class TSpStatusResponse
    {
        public int status = 0;
        public bool success = false;
        public string msg = "";
    }

    public class SortItemsRequest
    {
        public string TableName { get; set; } = "";
        public string RowNumberFieldName { get; set; } = "";
        public List<List<string>> Ids { get; set; } = new List<List<string>>();
        /*
               @ids varchar(max),   --// ids are comma-separated.                  example: A1234567,A2345678,A3456789
                                    --// multi-key field are separated with tilde. example: A1234567~A2345678,B3456789~B6658005,C6806308~C6806329

        */
        public List<string> IdFieldNames { get; set; } = new List<string>();
        public int? StartAtIndex { get; set; } = 1;
        public int? RowNumberDigits { get; set; } = 4;
    }

    public class SortItemsResponse : TSpStatusResponse { }

    public class UpdateInventoryQuantityRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        public string TransactionType { get; set; }
        public string OrderType { get; set; }
        public decimal QuantityChange { get; set; }
        public bool UpdateCost { get; set; }
        public decimal? CostPerItem { get; set; }
        public decimal? ForceCost { get; set; }
        public string UniqueId1 { get; set; }
        public string UniqueId2 { get; set; }
        public string UniqueId3 { get; set; }
        public int? UniqueId4 { get; set; }
        public bool LogOnly { get; set; }
    }
    public class UpdateInventoryQuantityResponse : TSpStatusResponse { }

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
        public static async Task<string> GetInternalChar(FwApplicationConfig appConfig, FwSqlConnection conn = null)
        {
            string internalchar = "";
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            internalchar = await FwSqlData.GetInternalChar(conn, appConfig.DatabaseSettings);
            return internalchar;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> EncryptAsync(FwApplicationConfig appConfig, string data)
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
        public static async Task<bool> InsertDataAsync(FwApplicationConfig appConfig, string tablename, string[] columns, string[] values)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("insert into " + tablename + "(");
                for (int c = 0; c < columns.Length; c++)
                {
                    qry.Add(columns[c]);
                    if (c < (columns.Length - 1))
                    {
                        qry.Add(",");
                    }
                }
                qry.Add(") values (");
                for (int c = 0; c < values.Length; c++)
                {
                    qry.Add("@" + columns[c]);
                    if (c < (values.Length - 1))
                    {
                        qry.Add(", ");
                    }
                }

                for (int c = 0; c < values.Length; c++)
                {
                    qry.AddParameter("@" + columns[c], values[c]);
                }
                qry.Add(")");

                await qry.ExecuteAsync();
                success = true;
            }

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
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
        public static async Task<bool> DeleteDataAsync(FwApplicationConfig appConfig, string tablename, string[] wherecolumns, string[] wherecolumnvalues, int? rowCount = null)
        {
            bool success = false;

            if (wherecolumns.Length.Equals(0))
            {
                throw new Exception("missing wherecolumns in call to DeleteDataAsync.");
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("delete ");
                    if (rowCount != null)
                    {
                        qry.Add(" top " + rowCount.ToString());
                    }
                    qry.Add(" from " + tablename);
                    qry.Add(" where ");
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
                    success = true;
                }
            }
            return success;
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
                string returnValueFieldName = "getdata__returnvalue";
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                //qry.Add("select top 1 " + selectcolumn);
                qry.Add("select top 1 " + returnValueFieldName + " = " + selectcolumn);
                qry.Add("from " + tablename + " with (nolock)");
                qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
                qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
                await qry.ExecuteAsync();
                //result = (qry.RowCount == 1) ? qry.GetField(selectcolumn) : null;
                result = (qry.RowCount == 1) ? qry.GetField(returnValueFieldName) : null;
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
            qry.AddParameter("@uniqueid1valuestr", SqlDbType.NVarChar, ParameterDirection.Input, RwConstants.CONTROL_ID);
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
        public static async Task<bool> SaveNoteAsync(FwApplicationConfig appConfig, FwUserSession userSession, string uniqueId1, string uniqueId2, string uniqueId3, string note)
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
        public static async Task<string> GetDepartmentLocationAsync(FwApplicationConfig appConfig, FwUserSession userSession, string departmentId, string locationId, string fieldName)
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
        public static async Task<string> GetLocationAsync(FwApplicationConfig appConfig, FwUserSession userSession, string locationId, string fieldName, FwSqlConnection conn = null)
        {
            string str = "";
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
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
            return str;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> IsDbWorksUserAsync(FwApplicationConfig appConfig, FwUserSession userSession)
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
        public static string GetOrderTypeDescription(string orderType)
        {
            string orderTypeDescription = orderType;
            switch (orderType)
            {
                case RwConstants.ORDER_TYPE_QUOTE:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_QUOTE;
                    break;
                case RwConstants.ORDER_TYPE_ORDER:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_ORDER;
                    break;
                case RwConstants.ORDER_TYPE_PROJECT:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_PROJECT;
                    break;
                case RwConstants.ORDER_TYPE_PURCHASE_ORDER:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_PURCHASE_ORDER;
                    break;
                case RwConstants.ORDER_TYPE_TRANSFER:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_TRANSFER;
                    break;
                case RwConstants.ORDER_TYPE_CONTAINER:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_CONTAINER;
                    break;
                case RwConstants.ORDER_TYPE_REPAIR:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_REPAIR;
                    break;
                case RwConstants.ORDER_TYPE_PENDING_EXCHANGE:
                    orderTypeDescription = RwConstants.ORDER_TYPE_DESCRIPTION_PENDING_EXCHANGE;
                    break;
            }
            return orderTypeDescription;
        }
        //-------------------------------------------------------------------------------------------------------
        public static string GetOrderTypeDescriptionFronEndControllerName(string orderTypeDescription)
        {
            string frontEndControllerName = "Unknown";
            switch (orderTypeDescription)
            {
                case RwConstants.ORDER_TYPE_DESCRIPTION_RESERVED:
                    frontEndControllerName = "Quote";
                    break;
                case RwConstants.ORDER_TYPE_DESCRIPTION_ORDER:
                    frontEndControllerName = "Order";
                    break;
                case RwConstants.ORDER_TYPE_DESCRIPTION_PROJECT:
                    frontEndControllerName = "Project";
                    break;
                case RwConstants.ORDER_TYPE_DESCRIPTION_PURCHASE_ORDER:
                    frontEndControllerName = "PurchaseOrder";
                    break;
                case RwConstants.ORDER_TYPE_DESCRIPTION_TRANSFER:
                    frontEndControllerName = "TransferOrder";
                    break;
                case RwConstants.ORDER_TYPE_DESCRIPTION_CONTAINER:
                    frontEndControllerName = "Container";
                    break;
                case RwConstants.ORDER_TYPE_DESCRIPTION_REPAIR:
                    frontEndControllerName = "Repair";
                    break;
            }
            return frontEndControllerName;
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
        public static string GetInventoryRecTypeDisplay(string recType)
        {
            string recTypeDisplay = recType;
            switch (recType)
            {
                case RwConstants.RECTYPE_RENTAL:
                    recTypeDisplay = RwConstants.RECTYPE_RENTAL_DESCRIPTION;
                    break;
                case RwConstants.RECTYPE_SALE:
                    recTypeDisplay = RwConstants.RECTYPE_SALE_DESCRIPTION;
                    break;
                case RwConstants.RECTYPE_PARTS:
                    recTypeDisplay = RwConstants.RECTYPE_PARTS_DESCRIPTION;
                    break;
            }
            return recTypeDisplay;
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
        public static bool InventoryClassIsPackage(string classification)
        {
            bool isPackage = false;
            if (!string.IsNullOrEmpty(classification))
            {
                isPackage = (classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT));
            }
            return isPackage;
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
        public static async Task<SessionLocation> GetSessionLocationAsync(FwApplicationConfig appConfig, string locationid)
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
        public static async Task<SessionWarehouse> GetSessionWarehouseAsync(FwApplicationConfig appConfig, string warehouseid)
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
        public static async Task<SessionDepartment> GetSessionDepartmentAsync(FwApplicationConfig appConfig, string departmentid)
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
            public string contactid { get; set; } = string.Empty;
            public string usertype { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
            public string fullname { get; set; } = string.Empty;
            public string name { get; set; } = string.Empty;
            public int browsedefaultrows { get; set; } = 0;
            public string applicationtheme { get; set; } = string.Empty;
            public string locationid { get; set; } = string.Empty;
            public string location { get; set; } = string.Empty;
            public string warehouseid { get; set; } = string.Empty;
            public string warehouse { get; set; } = string.Empty;
            public string departmentid { get; set; } = string.Empty;
            public string department { get; set; } = string.Empty;
            public bool webadministrator { get; set; } = false;

        }
        public static async Task<SessionUser> GetSessionUserAsync(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            var response = new SessionUser();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select webusersid, usersid, contactid, usertype, email, fullname, name, browsedefaultrows, applicationtheme, locationid, location, warehouseid, warehouse, departmentid, department, webadministrator");
                    qry.Add("from webusersview with (nolock)");
                    qry.Add("where webusersid = @webusersid");
                    qry.AddParameter("@webusersid", userSession.WebUsersId);
                    await qry.ExecuteAsync();
                    response.webusersid = qry.GetField("webusersid").ToString().TrimEnd();
                    response.usersid = qry.GetField("usersid").ToString().TrimEnd();
                    response.contactid = qry.GetField("contactid").ToString().TrimEnd();
                    response.usertype = qry.GetField("usertype").ToString().TrimEnd();
                    response.email = qry.GetField("email").ToString().TrimEnd();
                    response.fullname = qry.GetField("fullname").ToString().TrimEnd();
                    response.name = qry.GetField("name").ToString().TrimEnd();
                    response.browsedefaultrows = qry.GetField("browsedefaultrows").ToInt32();
                    if (response.browsedefaultrows <= 0)
                    {
                        response.browsedefaultrows = 15;
                    }
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
        public class SessionDeal
        {
            public string dealid { get; set; } = string.Empty;
            public string deal { get; set; } = string.Empty;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SessionDeal> GetSessionDealAsync(FwApplicationConfig appConfig, string contactid)
        {
            SessionDeal response = null;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry1 = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry1.Add("select top 1 d.dealid, d.deal");
                    qry1.Add("from contact c with (nolock)");
                    qry1.Add("  left outer join dealview d with (nolock) on (c.dealid = d.dealid)");
                    qry1.Add("where contactid = @contactid");
                    qry1.Add("  and d.inactive <> 'T'");
                    qry1.Add("  and d.dealid <> ''");
                    qry1.Add("  and d.dealid in (select companyid");
                    qry1.Add("                   from compcontactview ccv with (nolock)");
                    qry1.Add("                   where ccv.contactid = @contactid");
                    qry1.Add("                     and ccv.companytype = 'DEAL'");
                    qry1.Add("                     and ccv.inactive <> 'T')");
                    qry1.AddParameter("@contactid", contactid);
                    await qry1.ExecuteAsync();

                    // The Contact has an Active dealid set
                    if (qry1.RowCount == 1)
                    {
                        response = new SessionDeal();
                        response.dealid = qry1.GetField("dealid").ToString().TrimEnd();
                        response.deal = qry1.GetField("deal").ToString().TrimEnd();
                    }
                    else
                    {
                        // since the Contact has an invalid active dealid, go blank it out
                        using (FwSqlCommand qry3 = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                        {
                            qry3.Add("update contact");
                            qry3.Add("set dealid = @dealid");
                            qry3.Add("where contactid = @contactid");
                            qry3.AddParameter("@dealid", string.Empty);
                            qry3.AddParameter("@contactid", contactid);
                            await qry3.ExecuteNonQueryAsync();
                        }
                    }
                }

                if (response == null)
                {
                    // load the first deal the Contact belongs to alphabetically
                    using (FwSqlCommand qry2 = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry2.Add("select top 1 d.dealid, d.deal");
                        qry2.Add("from compcontactview ccv with (nolock)");
                        qry2.Add("  left outer join deal d with (nolock) on (ccv.companyid = d.dealid)");
                        qry2.Add("where contactid = @contactid");
                        qry2.Add("  and ccv.companytype = 'DEAL'");
                        qry2.Add("  and ccv.inactive <> 'T'");
                        qry2.Add("order by company");
                        qry2.AddParameter("@contactid", contactid);
                        await qry2.ExecuteAsync();
                        if (qry2.RowCount == 1)
                        {
                            response = new SessionDeal();
                            response.dealid = qry2.GetField("dealid").ToString().TrimEnd();
                            response.deal = qry2.GetField("deal").ToString().TrimEnd();

                            // set this Deal as the Contact's active dealid, since it's going to be selected as their active dealid when they login
                            using (FwSqlCommand qry3 = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                            {
                                qry3.Add("update contact");
                                qry3.Add("set dealid = @dealid");
                                qry3.Add("where contactid = @contactid");
                                qry3.AddParameter("@dealid", response.dealid);
                                qry3.AddParameter("@contactid", contactid);
                                await qry3.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> ValidateBrowseRequestActiveViewDealId(FwApplicationConfig appConfig, FwUserSession userSession, BrowseRequest browseRequest)
        {
            var isValid = true;
            if (userSession.UserType == "CONTACT")
            {
                // DealId is request for Contacts
                if (!browseRequest.activeviewfields.ContainsKey("DealId"))
                {
                    isValid = false;
                }
                var dealIds = browseRequest.activeviewfields["DealId"];
                if (dealIds.Count <= 0)
                {
                    isValid = false;
                }
                isValid = await AppFunc.ValidateContactBelongsToDealsAsync(appConfig, userSession.ContactId, dealIds);
            }
            return isValid;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> ValidateContactBelongsToDealsAsync(FwApplicationConfig appConfig, string contactid, List<string> dealids)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select hasdeal = case");
                    qry.Add("                   when exists(select companyid");
                    qry.Add("                               from compcontactview with (nolock)");
                    qry.Add("                               where contactid = @contactid");
                    qry.Add("                                  and companytype = 'DEAL'");
                    qry.Add("                                  and inactive <> 'T'");
                    StringBuilder companyFilter = new StringBuilder();
                    companyFilter.Append("                                  and companyid in (");
                    for (int i = 0; i < dealids.Count; i++)
                    {
                        if (i > 0)
                        {
                            companyFilter.Append(", ");
                        }
                        string dealidParameterName = $"@dealid{i}";
                        companyFilter.Append(dealidParameterName);
                        qry.AddParameter(dealidParameterName, dealids[i]);
                    }
                    companyFilter.Append(")) then 'T'");
                    qry.Add(companyFilter.ToString());
                    qry.Add("                   else 'F'");
                    qry.Add("                 end");
                    qry.AddParameter("@contactid", contactid);
                    await qry.ExecuteAsync();
                    var hasdeal = false;
                    if (qry.RowCount == 1)
                    {
                        hasdeal = qry.GetField("hasdeal").ToBoolean();
                    }
                    return hasdeal;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetDealLocationIdAsync(FwApplicationConfig appConfig, string dealid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string locationid = (await FwSqlCommand.GetDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "deal", "dealid", dealid, "locationid")).ToString().TrimEnd();
                return locationid;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortItems(FwApplicationConfig appConfig, FwUserSession userSession, SortItemsRequest request)
        {
            SortItemsResponse response = new SortItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                /*
                       @ids varchar(max),   --// ids are comma-separated.                  example: A1234567,A2345678,A3456789
                                            --// multi-key field are separated with tilde. example: A1234567~A2345678,B3456789~B6658005,C6806308~C6806329

                */
                StringBuilder ids = new StringBuilder();
                foreach (List<string> idSet in request.Ids)
                {
                    if (ids.Length > 0)
                    {
                        ids.Append(",");
                    }
                    string id = string.Join('~', idSet);
                    ids.Append(id);
                }

                FwSqlCommand qry = new FwSqlCommand(conn, "sortitemsweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@tablename", SqlDbType.NVarChar, ParameterDirection.Input, request.TableName);
                qry.AddParameter("@rownumberfieldname", SqlDbType.NVarChar, ParameterDirection.Input, request.RowNumberFieldName);
                qry.AddParameter("@ids", SqlDbType.NVarChar, ParameterDirection.Input, ids.ToString());
                qry.AddParameter("@idfieldnames", SqlDbType.NVarChar, ParameterDirection.Input, string.Join(',', request.IdFieldNames));
                qry.AddParameter("@startatindex", SqlDbType.Int, ParameterDirection.Input, request.StartAtIndex);
                qry.AddParameter("@rownumberdigits", SqlDbType.Int, ParameterDirection.Input, request.RowNumberDigits);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static bool FwDataTypeIsDecimal(FwDataTypes t)
        {
            return (t.Equals(FwDataTypes.Decimal) ||
                    t.Equals(FwDataTypes.DecimalString1Digit) ||
                    t.Equals(FwDataTypes.DecimalString2Digits) ||
                    t.Equals(FwDataTypes.DecimalString3Digits) ||
                    t.Equals(FwDataTypes.DecimalString4Digits) ||
                    t.Equals(FwDataTypes.DecimalStringNoTrailingZeros) ||
                    t.Equals(FwDataTypes.CurrencyString) ||
                    t.Equals(FwDataTypes.CurrencyStringNoDollarSign) ||
                    t.Equals(FwDataTypes.CurrencyStringNoDollarSignNoDecimalPlaces) ||
                    t.Equals(FwDataTypes.Percentage));
        }
        //-------------------------------------------------------------------------------------------------------    
        //temporary location for this method
        public static async Task<UpdateInventoryQuantityResponse> UpdateInventoryQuantity(FwApplicationConfig appConfig, FwUserSession userSession, UpdateInventoryQuantityRequest request)
        {
            UpdateInventoryQuantityResponse response = new UpdateInventoryQuantityResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatemasterwhqty", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorId);
                qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorAgreementId);
                qry.AddParameter("@trantype", SqlDbType.NVarChar, ParameterDirection.Input, request.TransactionType);
                qry.AddParameter("@ordertype", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderType);
                qry.AddParameter("@qtychange", SqlDbType.Decimal, ParameterDirection.Input, request.QuantityChange);
                qry.AddParameter("@updatecost", SqlDbType.NVarChar, ParameterDirection.Input, request.UpdateCost);
                qry.AddParameter("@costperitem", SqlDbType.Decimal, ParameterDirection.Input, request.CostPerItem);
                qry.AddParameter("@forcecost", SqlDbType.NVarChar, ParameterDirection.Input, request.ForceCost);
                qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId1);
                qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId2);
                qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId3);
                qry.AddParameter("@uniqueid4", SqlDbType.Int, ParameterDirection.Input, request.UniqueId4);
                qry.AddParameter("@logonly", SqlDbType.NVarChar, ParameterDirection.Input, request.LogOnly);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.success = true;

                string classification = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "class").Result;
                InventoryAvailabilityFunc.RequestRecalc(request.InventoryId, request.WarehouseId, classification);

            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
