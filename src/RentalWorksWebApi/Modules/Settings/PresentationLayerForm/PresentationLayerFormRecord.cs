using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
using System.Collections.Generic;

namespace RentalWorksWebApi.Modules.Settings.PresentationLayerForm
{
    [FwSqlTable("presentationlayerform")]
    public class PresentationLayerFormRecord : RwDataReadWriteRecord
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            if ((request != null) && (request.miscfields != null))
            {
                IDictionary<string, object> miscfields = ((IDictionary<string, object>)request.miscfields);
                if (miscfields.ContainsKey("PresentationLayerId"))
                {
                    select.AddWhere("presentationlayerid = @presentationlayerid");
                    select.AddParameter("@presentationlayerid", miscfields["PresentationLayerId"].ToString());
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}