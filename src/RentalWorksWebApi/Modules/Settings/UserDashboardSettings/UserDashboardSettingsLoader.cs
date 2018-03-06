using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Settings.UserDashboardSettings
{
    [FwSqlTable("webusers")]
    public class UserDashboardSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webuserswidgetid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string UserWidgetId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "widgetid", modeltype: FwDataTypes.Text)]
        public string WidgetId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "widget", modeltype: FwDataTypes.Text)]
        public string Widget { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaulttype", modeltype: FwDataTypes.Text)]
        public string DefaultType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "widgettype", modeltype: FwDataTypes.Text)]
        public string WidgetType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------

        //jh 01/29/2018 note: I don't really want to override SelectAsync this way.  This is a hack until we can figure out a clean way to parse the "exec stored_procedure_name" syntax" using teh FwSqlSelect object
        public override async Task<List<T>> SelectAsync<T>(BrowseRequest request, FwCustomFields customFields = null)
        {
            string webUsersId = "";

            IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
            if (uniqueIds.ContainsKey("WebUsersId"))
            {
                webUsersId = uniqueIds["WebUsersId"].ToString();
            }

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Clear();
                    qry.Add("exec getwebuserdashboardsettings '" + webUsersId + "'");

                    MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(this.GetType());
                    object openAndCloseConnection = true;
                    dynamic result = generic.Invoke(qry, new object[] { openAndCloseConnection, customFields });
                    dynamic records = await result;
                    return records;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
