using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class PhysicalInventory : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public PhysicalInventory(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetInventoryItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetInventoryItem";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "phyNo");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.item = await WebSelectPhyInvAsync(conn, session.security.webUser.usersid, request.phyNo); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> WebSelectPhyInvAsync(FwSqlConnection conn, string usersId, string phyNo)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand sp = new FwSqlCommand(conn, "dbo.webselectphyinv", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@physicalno", phyNo);
            sp.AddParameter("@usersid", usersId);
            sp.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@phystatus", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@scheduledate", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@counttype", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouse", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@department", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();

            result.phyNo = phyNo;
            result.physicalId = sp.GetParameter("@physicalid").ToString().TrimEnd();
            result.dealId = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.description = sp.GetParameter("@description").ToString().TrimEnd();
            result.phyStatus = sp.GetParameter("@phystatus").ToString().TrimEnd();
            result.scheduleDate = sp.GetParameter("@scheduledate").ToString().TrimEnd();
            result.counttype = sp.GetParameter("@counttype").ToString().TrimEnd();
            result.rectype = sp.GetParameter("@rectype").ToString().TrimEnd();
            result.warehouse = sp.GetParameter("@warehouse").ToString().TrimEnd();
            result.department = sp.GetParameter("@department").ToString().TrimEnd();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg = sp.GetParameter("@msg").ToString().TrimEnd();

            return result; 
        }
        //---------------------------------------------------------------------------------------------
    }
}