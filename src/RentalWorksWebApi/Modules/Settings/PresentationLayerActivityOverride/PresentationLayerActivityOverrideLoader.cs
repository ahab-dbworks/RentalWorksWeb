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
    }
}