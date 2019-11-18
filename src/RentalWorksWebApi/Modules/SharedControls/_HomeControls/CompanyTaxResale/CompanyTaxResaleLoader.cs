using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Home.CompanyTaxResale
{
    [FwSqlTable("taxresaleview")]
    public class CompanyTaxResaleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxresaleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CompanyTaxResaleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text, required: true)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "stateid", modeltype: FwDataTypes.Text, required: true)]
        public string StateId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statecode", modeltype: FwDataTypes.Text, required: true)]
        public string StateCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "resaleno", modeltype: FwDataTypes.Text)]
        public string ResaleNumber { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("CompanyId"))
                {
                    select.Parse();
                    select.AddWhere("companyid = @companyid");
                    select.AddParameter("@companyid", uniqueIds["CompanyId"].ToString());
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}