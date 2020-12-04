using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.Administrator.DataHealth
{
    [FwSqlTable("datahealthview")]
    public class DataHealthBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public DataHealthBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datahealthid", modeltype: FwDataTypes.Integer, identity: true, isPrimaryKey: true)]
        public int? DataHealthId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datahealthtype", modeltype: FwDataTypes.Text)]
        public string DataHealthType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "capturedatetime", modeltype: FwDataTypes.DateTime)]
        public string CaptureDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "capturedate", modeltype: FwDataTypes.DateTime)]
        public string CaptureDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "severity", modeltype: FwDataTypes.Text)]
        public string Severity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string SeverityColor
        {
            get { return getSeverityColor(Severity); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "resolved", modeltype: FwDataTypes.Boolean)]
        public bool? Resolved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            DateTime captureDate = GetUniqueIdAsDate("CaptureDate", request) ?? DateTime.MinValue; 
            //bool excludeResolved = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("Severity", "severity", select, request);
            if (captureDate != DateTime.MinValue)
            {
                select.AddWhere("(capturedate = @capturedate)");
                select.AddParameter("@capturedate", captureDate);
            }

            //addFilterToSelect("WebUserId", "webusersid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramboolean", paramBoolean); 
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
                        row[dt.GetColumnNo("SeverityColor")] = getSeverityColor(row[dt.GetColumnNo("Severity")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
        protected string getSeverityColor(string severity)
        {
            string color = null;
            if (severity != null)
            {
                switch (severity)
                {
                    case RwConstants.DATA_HEALTH_SEVERITY_CRITICAL: 
                        color = RwGlobals.DATA_HEALTH_SEVERITY_CRITICAL_COLOR;
                        break;
                    case RwConstants.DATA_HEALTH_SEVERITY_HIGH:
                        color = RwGlobals.DATA_HEALTH_SEVERITY_HIGH_COLOR;
                        break;
                    case RwConstants.DATA_HEALTH_SEVERITY_MEDIUM:
                        color = RwGlobals.DATA_HEALTH_SEVERITY_MEDIUM_COLOR;
                        break;
                    case RwConstants.DATA_HEALTH_SEVERITY_LOW:
                        color = RwGlobals.DATA_HEALTH_SEVERITY_LOW_COLOR;
                        break;
                    case RwConstants.DATA_HEALTH_SEVERITY_WARNING:
                        color = RwGlobals.DATA_HEALTH_SEVERITY_WARNING_COLOR;
                        break;
                }
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
    }
}
