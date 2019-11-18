using System.Threading.Tasks;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [FwSqlTable("presentationlayeractivity")]
    public class PresentationLayerActivityLoader : AppDataLoadRecord
    {
        public PresentationLayerActivityLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayeractivityid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PresentationLayerActivityId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activity", modeltype: FwDataTypes.Text)]
        public string Activity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "0", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ActivityColor
        {
            get { return getActivityColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityrename", modeltype: FwDataTypes.Text)]
        public string ActivityRename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupno", modeltype: FwDataTypes.Text)]
        public string GroupNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("PresentationLayerId", "presentationlayerid", select, request);
            addFilterToSelect("RecType", "rectype", select, request);
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
