using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class MoveBCLocation : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public MoveBCLocation(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task BarcodeMove(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "BarcodeMove";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "aisle");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "shelf");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.barcodemove = await this.AppData.WebMoveBCLocationAsync(conn: conn,
                                                                                 usersId: session.security.webUser.usersid,
                                                                                 barcode: request.barcode,
                                                                                 aisle: request.aisle,
                                                                                 shelf: request.shelf); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
