using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.Home.Master;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.Rate
{
    public abstract class RateLoader : MasterLoader
    {
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        //public string RateId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeAsProfitAndLossCategory { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}