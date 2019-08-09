using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.LaborCategory
{
    public class LaborCategoryLoader: CategoryLoader
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string LaborTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(rectype='L')");
            addFilterToSelect("LaborTypeId", "inventorydepartmentid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}
