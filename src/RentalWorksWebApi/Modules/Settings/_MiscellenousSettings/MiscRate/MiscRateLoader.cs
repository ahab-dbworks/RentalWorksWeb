using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Settings.MiscellenousSettings.MiscRate
{
    public class MiscRateLoader : RateLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string RateId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string MiscTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string MiscType { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            select.AddWhere("(availfor='" + RwConstants.RATE_AVAILABLE_FOR_MISC + "')");
        }
        //------------------------------------------------------------------------------------
    }
}