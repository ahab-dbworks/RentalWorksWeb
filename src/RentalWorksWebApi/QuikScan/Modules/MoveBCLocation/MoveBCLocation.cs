using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;

namespace RentalWorksQuikScan.Modules
{
    class MoveBCLocation
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void BarcodeMove(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "BarcodeMove";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "aisle");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "shelf");
            response.barcodemove = RwAppData.WebMoveBCLocation(conn:    FwSqlConnection.RentalWorks,
                                                               usersId: session.security.webUser.usersid,
                                                               barcode: request.barcode,
                                                               aisle:   request.aisle,
                                                               shelf:   request.shelf);
        }
        //---------------------------------------------------------------------------------------------
    }
}
