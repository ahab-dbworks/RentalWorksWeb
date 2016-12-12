using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksQuikScanLibrary.Validations
{
    class RwRetiredReason : FwValidation
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
