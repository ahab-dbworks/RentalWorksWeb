using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.Company
{
    [FwSqlTable("contactcompanyview")]
    public class CompanyLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyno", modeltype: FwDataTypes.Text)]
        public string CompanyNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyname", modeltype: FwDataTypes.Text)]
        public string Company { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companytype", modeltype: FwDataTypes.Text)]
        public string CompanyType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companytypecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CompanyTypeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 

            AddActiveViewFieldToSelect("CompanyType", "companytype", select, request);

            //if ((request != null) && (request.activeview != null) && (!request.activeview.Equals(string.Empty)))
            //{
            //    switch (request.activeview)
            //    {
            //        case "ALL":
            //            break;
            //        default:
            //            select.AddWhere("(companytype = @companytype)");
            //            select.AddParameter("@companytype", request.activeview);
            //            break;
            //    }
            //}

        }
        //------------------------------------------------------------------------------------ 
    }
}