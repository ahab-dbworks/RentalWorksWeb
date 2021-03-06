using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.Settings.ActivityType
{
    [FwSqlTable("activitytypeview")]
    public class ActivityTypeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ActivityTypeLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? ActivityTypeId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rename", modeltype: FwDataTypes.Text)]
        public string Rename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptiondisplay", modeltype: FwDataTypes.Text)]
        public string DescriptionDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemtype", modeltype: FwDataTypes.Boolean)]
        public bool? IsSystemType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemuser", modeltype: FwDataTypes.Text)]
        public string SystemUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string SystemUserColor
        {
            get { return getSystemUserColor(IsSystemType); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseoutbound", modeltype: FwDataTypes.Boolean)]
        public bool? IsWarehouseOutbound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseinbound", modeltype: FwDataTypes.Boolean)]
        public bool? IsWarehouseInbound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousedispatch", modeltype: FwDataTypes.Boolean)]
        public bool? IsWarehouseDispatch { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("IsWarehouseOutbound", "warehouseoutbound", select, request);
            addFilterToSelect("IsWarehouseInbound", "warehouseinbound", select, request);
            addFilterToSelect("IsWarehouseDispatch", "warehousedispatch", select, request);
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("SystemUserColor")] = getSystemUserColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsSystemType")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        private string getSystemUserColor(bool? isSystemType)
        {
            string color = null;
            if (!isSystemType.GetValueOrDefault(false))
            {
                color = RwGlobals.USER_DEFINED_ACTIVITY_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
    }
}
