using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.Home.Taxable
{
    [FwSqlTable("masterlocationview")]
    public class TaxableLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {

            if ((MasterId == null) || (MasterId.Equals(string.Empty)))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("MasterId"))
                    {
                        MasterId = uniqueIds["MasterId"].ToString();
                    }
                }
            }


            if ((LocationId == null) || (LocationId.Equals(string.Empty)))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("LocationId"))
                    {
                        LocationId = uniqueIds["LocationId"].ToString();
                    }
                }
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            addFilterToSelect("MasterId", "masterid", select, request);
            addFilterToSelect("LocationId", "locationid", select, request);


        }
        //------------------------------------------------------------------------------------ 

    }
}