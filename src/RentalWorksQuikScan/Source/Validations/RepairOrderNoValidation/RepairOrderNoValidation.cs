using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksQuikScan.Source.Validations
{
    class RepairOrderNoValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            string warehouseid;
            dynamic userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            warehouseid       = userLocation.warehouseId;
            base.setBrowseQry(selectQry); 
            selectQry.AddWhere("status not in ('COMPLETE','VOID')");
            selectQry.AddWhere("(warehouseid = @warehouseid or transferredfromwarehouseid = @warehouseid)");
            selectQry.AddParameter("@warehouseid", warehouseid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
