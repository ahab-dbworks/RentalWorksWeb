using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Modules.Reports.ContractReports.OrderContractReport;
using WebApi.Modules.Reports.ContractReports.ContractReport;

namespace WebApi.Modules.Reports.ContractReports.LostContractReport
{

    public class LostContractReportLoader : OrderContractReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public async Task<LostContractReportLoader> RunReportAsync(LostContractReportRequest request)
        {
            Task<LostContractReportLoader> taskContract;
            LostContractReportLoader Contract = null;

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
                    taskContract = qry.QueryToTypedObjectAsync<LostContractReportLoader>();

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
