using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract;
using RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels;
using System.Data;

namespace RentalWorksAPI.api.v2.Data
{
    public class WarehouseData
    {
        //----------------------------------------------------------------------------------------------------
        public static StageItemQry StageItem(string orderid, string code, string masteritemid, int qty)
        {
            StageItemQry result = new StageItemQry();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_stageitem"))
            {
                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@code", code);
                qry.AddParameter("@masteritemid", SqlDbType.Char, ParameterDirection.InputOutput, masteritemid);
                qry.AddParameter("@qty", qty);
                qry.AddParameter("@statuscode", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@statusmessage", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@barcode", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@quantity", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@datetimestamp", SqlDbType.VarChar, ParameterDirection.Output);
                result = qry.QueryToTypedObject<StageItemQry>();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static UnstageItemQry UnstageItem(string orderid, string code, string masteritemid, int qty)
        {
            UnstageItemQry result = new UnstageItemQry();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_unstageitem"))
            {
                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@code", code);
                qry.AddParameter("@masteritemid", SqlDbType.Char, ParameterDirection.InputOutput, masteritemid);
                qry.AddParameter("@qty", qty);
                qry.AddParameter("@statuscode", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@statusmessage", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@barcode", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@quantity", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@datetimestamp", SqlDbType.VarChar, ParameterDirection.Output);
                result = qry.QueryToTypedObject<UnstageItemQry>();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static MoveToContractSp MoveToContract(string usersid, string orderid, string code, string masteritemid, int qty)
        {
            MoveToContractSp result = new MoveToContractSp();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_movetocontract"))
            {
                qry.AddColumn("datetimestamp", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.DateTime);
                qry.AddParameter("@usersid", usersid); // required
                qry.AddParameter("@orderid", orderid); //required
                qry.AddParameter("@code", code);
                qry.AddParameter("@masteritemid", SqlDbType.Char, ParameterDirection.InputOutput, masteritemid);
                qry.AddParameter("@qty", qty);
                qry.AddParameter("@statuscode", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@statusmessage", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@barcode", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@quantity", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@datetimestamp", SqlDbType.VarChar, ParameterDirection.Output);
                result = qry.QueryToTypedObject<MoveToContractSp>();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}