using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.PresentationLayerActivityOverride
{
    [FwSqlTable("presentationlayeractivityoverrideview")]
    public class PresentationLayerActivityOverrideLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public PresentationLayerActivityOverrideLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayeractivityoverrideid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string PresentationLayerActivityOverrideId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayeractivityid", modeltype: FwDataTypes.Text)]
        public string PresentationLayerActivityId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activity", modeltype: FwDataTypes.Text)]
        public string Activity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityrename", modeltype: FwDataTypes.Text)]
        public string ActivityRename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "0", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ActivityColor
        {
            get { return getActivityColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("PresentationLayerId", "presentationlayerid", select, request);
        }
        //------------------------------------------------------------------------------------
        private string getActivityColor(string recType)
        {
            string activityColor = null;
            if (recType.Equals(RwConstants.PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED))
            {
                activityColor = RwGlobals.PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED_COLOR;
            }
            return activityColor;
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
                        string recType = row[dt.GetColumnNo("RecType")].ToString();
                        row[dt.GetColumnNo("ActivityColor")] = getActivityColor(recType);
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}