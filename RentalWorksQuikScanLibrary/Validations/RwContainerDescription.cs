﻿using System;
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
    class RwContainerDescription : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            string warehouseid, scannablemasterid;
            dynamic userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                   , usersId: session.security.webUser.usersid);
            warehouseid       = userLocation.warehouseId;
            scannablemasterid = request.boundids.scannablemasterid;
            base.setBrowseQry(selectQry);
            selectQry.AddWhere("warehouseid = @warehouseid");
            selectQry.AddWhere("availfor in ('R')");
            selectQry.AddWhere("availfrom in ('W')");
            selectQry.AddWhere("class in ('N')");
            selectQry.AddWhere("inactive <> 'T'");
            selectQry.AddWhere("scannablemasterid = @scannablemasterid");
            selectQry.AddParameter("@warehouseid", warehouseid);
            selectQry.AddParameter("@scannablemasterid", scannablemasterid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
