using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class QC : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public QC(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ItemScan(dynamic request, dynamic response, dynamic session)
        {
            using(FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
{
                response.qcitem = await WebCompleteQCItemAsync(request.code, session.security.webUser.usersid);
                response.itemstatus = await this.AppData.WebGetItemStatusAsync(conn, session.security.webUser.usersid, request.code);
                response.conditions = await GetRentalConditionsAsync(response.itemstatus.masterId); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebCompleteQCItemAsync(string code, string usersid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand sp;

                sp = new FwSqlCommand(conn, "dbo.webcompleteqcitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@code", code);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                sp.AddParameter("@rentalitemid", SqlDbType.Char, ParameterDirection.Output);
                sp.AddParameter("@rentalitemqcid", SqlDbType.Char, ParameterDirection.Output);
                sp.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                result = new ExpandoObject();
                result.masterNo = sp.GetParameter("@masterno").ToString().Trim();
                result.rentalitemid = sp.GetParameter("@rentalitemid").ToString().Trim();
                result.rentalitemqcid = sp.GetParameter("@rentalitemqcid").ToString().Trim();
                result.description = sp.GetParameter("@description").ToString().Trim();
                result.status = sp.GetParameter("@status").ToInt32();
                result.msg = sp.GetParameter("@msg").ToString().Trim();

                return result; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        private async Task<dynamic> GetRentalConditionsAsync(string masterid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("conditionid", false);
                qry.AddColumn("condition", false);
                qry.Add("select value = c.conditionid, text = c.condition");
                qry.Add("from   condition c with (nolock)");
                qry.Add("where  c.rental = 'T'");
                qry.Add("  and  c.conditionid in (select conditionid from dbo.funcconditionfilter(@masterid))");
                qry.AddParameter("@masterid", masterid);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task UpdateQCItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                byte[] image;

                if (!string.IsNullOrEmpty(request.conditionid) || !string.IsNullOrEmpty(request.note))
                {
                    await UpdateRentalItemQCAsync(request.qcitem.rentalitemid, request.qcitem.rentalitemqcid, request.conditionid, request.note);
                }

                if (FwValidate.IsPropertyDefined(request, "images") && request.images != null)
                for (int i = 0; i < request.images.Count; i++)
                {
                    image = Convert.FromBase64String(request.images[i]);
                    await FwSqlData.InsertAppImageAsync(conn, this.ApplicationConfig.DatabaseSettings, request.qcitem.rentalitemqcid, string.Empty, string.Empty, string.Empty, string.Empty, "JPG", image);
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> UpdateRentalItemQCAsync(string rentalitemid, string rentalitemqcid, string conditionid, string note)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand sp;

                sp = new FwSqlCommand(conn, "dbo.webupdaterentalitemqc", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@rentalitemid", rentalitemid);
                sp.AddParameter("@rentalitemqcid", rentalitemqcid);
                sp.AddParameter("@conditionid", conditionid);
                sp.AddParameter("@note", note);
                sp.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@errmsg", SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteNonQueryAsync();
                result = new ExpandoObject();
                result.errno = sp.GetParameter("@errno").ToInt32();
                result.errmsg = sp.GetParameter("@errmsg").ToString().Trim();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
