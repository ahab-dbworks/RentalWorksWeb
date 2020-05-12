using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.HomeControls.Master;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.Rate
{
    public abstract class RateLoader : MasterLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeAsProfitAndLossCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incomeaccountid", modeltype: FwDataTypes.Text)]
        public string IncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeglno", modeltype: FwDataTypes.Text)]
        public string IncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string IncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeaccountid", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeglno", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseaccountid", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseglno", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
    }
}