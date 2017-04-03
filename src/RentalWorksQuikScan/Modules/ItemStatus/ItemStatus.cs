﻿using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using RentalWorksQuikScan.Source;

namespace RentalWorksQuikScan.Modules
{
    public class ItemStatus
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="barcode")]
        public static void GetItemStatus(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            response.itemdata = RwAppData.WebGetItemStatus(conn:    FwSqlConnection.RentalWorks,
                                                           usersId: session.security.webUser.usersid,
                                                           barcode: request.barcode);
            response.itemdata.warehousedata = RwAppData.FuncMasterWh(conn:              FwSqlConnection.RentalWorks,
                                                                     masterId:          response.itemdata.masterId,
                                                                     usersWarehouseId:  userLocation.warehouseId,
                                                                     filterWarehouseId: string.Empty);
            // Telemundo asked to change this to only show the primary image thumbnail
            response.itemdata.images = RwAppData.GetPrimaryAppImageThumbnail(conn:        FwSqlConnection.RentalWorks,
                                                                             uniqueid1:   response.itemdata.masterId,
                                                                             uniqueid2:   string.Empty,
                                                                             uniqueid3:   string.Empty,
                                                                             description: string.Empty,
                                                                             rectype:     string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="tags")]
        public static void ItemStatusRFID(dynamic request, dynamic response, dynamic session)
        {
            response.items = RwAppData.ItemStatusRFID(conn:      FwSqlConnection.RentalWorks,
                                                      tags:       request.tags);
        }
        //---------------------------------------------------------------------------------------------
    }
}
