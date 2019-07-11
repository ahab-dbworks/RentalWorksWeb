using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.ChangeAuditReport
{
    [FwSqlTable("webauditjsonview")]
    public class ChangeAuditReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webauditid", modeltype: FwDataTypes.Integer)]
        public int? WebAuditId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recordtitle", modeltype: FwDataTypes.Text)]
        public string RecordTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string UniqueId1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text)]
        public string UniqueId2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid3", modeltype: FwDataTypes.Text)]
        public string UniqueId3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string WebUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "json", modeltype: FwDataTypes.Text)]
        public string Json { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(ChangeAuditReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    //select.AddWhere("(xxxxid ^> ')"); 
                    //select.AddWhereIn("modulename", request.ModuleName);
                    //select.AddWhereIn("webusersid", request.WebUsersId);
                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);
                    select.AddParameter("@modulename", request.ModuleName);
                    select.AddParameter("@webusersid", request.WebUsersId);
                    select.AddParameter("@keyword", request.Keyword);
                    select.AddOrderBy("modulename, username, datestamp");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            //if (request.IncludeSubHeadingsAndSubTotals)
            //{
            //    string[] totalFields = new string[] { "RentalTotal", "SalesTotal" };
            //    dt.InsertSubTotalRows("GroupField1", "RowType", totalFields);
            //    dt.InsertSubTotalRows("GroupField2", "RowType", totalFields);
            //    dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            //}
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
