using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Home.PersonalEvent
{
    [FwSqlTable("personaleventview")]
    public class PersonalEventLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personaleventid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PersonalEventId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text)]
        public string Person { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacteventid", modeltype: FwDataTypes.Text)]
        public string ContactEventId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactevent", modeltype: FwDataTypes.Text)]
        public string ContactEvent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eventdate", modeltype: FwDataTypes.Date)]
        public string EventDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recurring", modeltype: FwDataTypes.Boolean)]
        public bool? Recurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("ContactId", "contactid", select, request);
            addFilterToSelect("ContactEventId", "contacteventid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
