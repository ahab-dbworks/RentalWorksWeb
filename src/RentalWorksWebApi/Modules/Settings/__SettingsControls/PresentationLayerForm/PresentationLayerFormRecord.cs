using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.PresentationLayerForm
{
    [FwSqlTable("presentationlayerform")]
    public class PresentationLayerFormRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerformid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PresentationLayerFormId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "formtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string FormType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "formtitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string FormTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
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