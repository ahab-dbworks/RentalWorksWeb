using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksQuikScan.Source.Validations
{
    class RetiredReasonValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            //string warehouseid;
            //dynamic userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
            //                                               , usersId: session.security.webUser.usersid);
            //warehouseid       = userLocation.warehouseId;
            base.setBrowseQry(selectQry); 
            //selectQry.AddWhere("masterno = @masterno");
            //selectQry.AddWhere("(warehouseid = @warehouseid");
            //selectQry.AddParameter("@masterno", request.masterno);
            //selectQry.AddParameter("@warehouseid", warehouseid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
