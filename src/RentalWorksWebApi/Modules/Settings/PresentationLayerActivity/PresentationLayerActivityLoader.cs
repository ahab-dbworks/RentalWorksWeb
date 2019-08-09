using System.Threading.Tasks;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [FwSqlTable("presentationlayeractivity")]
    public class PresentationLayerActivityLoader : AppDataLoadRecord
    {
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
    }
}
