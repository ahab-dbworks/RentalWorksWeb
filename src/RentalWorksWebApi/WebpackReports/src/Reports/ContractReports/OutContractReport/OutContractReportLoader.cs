using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Modules.Reports.ContractReports.ContractReport;
using WebApi.Modules.Reports.ContractReports.OrderContractReport;

namespace WebApi.Modules.Reports.ContractReports.OutContractReport
{
    public class OutContractReportLoader : OrderContractReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsiblepersonid", modeltype: FwDataTypes.Text)]
        public string ResponsiblePersonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsibleperson", modeltype: FwDataTypes.Text)]
        public string ResponsiblePerson { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personprintname", modeltype: FwDataTypes.Text)]
        public string PersonPrintName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personsignature", modeltype: FwDataTypes.JpgDataUrl)]
        public string PersonSignature { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<OutContractReportLoader> RunReportAsync(OutContractReportRequest request)
        {
            Task<OutContractReportLoader> taskContract;
            OutContractReportLoader Contract = null;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.Add("select *                           ");
                    qry.Add(" from  " + TableName + " c         ");
                    qry.Add(" where c.contractid = @contractid  ");
                    qry.AddParameter("@contractid", request.ContractId);
                    AddPropertiesAsQueryColumns(qry);
                    taskContract = qry.QueryToTypedObjectAsync<OutContractReportLoader>();

                    Task<List<ContractItemReportLoader>> taskContractItems;
                    ContractItemReportLoader ContractItems = new ContractItemReportLoader();
                    ContractItems.SetDependencies(AppConfig, UserSession);
                    taskContractItems = ContractItems.LoadItems<ContractItemReportLoader>(request);

                    await Task.WhenAll(new Task[] { taskContract, taskContractItems });

                    Contract = taskContract.Result;

                    if (Contract != null)
                    {
                        Contract.Items = taskContractItems.Result;
                    }

                }
            }
            return Contract;
        }
        //------------------------------------------------------------------------------------ 
    }
}
