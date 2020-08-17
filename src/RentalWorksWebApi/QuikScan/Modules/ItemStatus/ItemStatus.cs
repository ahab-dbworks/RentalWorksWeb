using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksQuikScan.Source;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class ItemStatus : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public ItemStatus(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="barcode")]
        public async Task GetItemStatus(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                response.itemdata = await this.AppData.WebGetItemStatusAsync(conn: conn,
                                                               usersId: session.security.webUser.usersid,
                                                               barcode: request.barcode);
                response.itemdata.warehousedata = await this.AppData.FuncMasterWhAsync(conn: conn,
                                                                         masterid: response.itemdata.masterId,
                                                                         userswarehouseid: userLocation.warehouseId,
                                                                         filterwarehouseid: string.Empty,
                                                                         currencyid: string.Empty);
                // Telemundo asked to change this to only show the primary image thumbnail
                response.itemdata.images = null;
                if (response.itemdata.trackedby == "BARCODE")
                {
                    response.itemdata.images = await this.AppData.GetPrimaryAppImageThumbnailAsync(conn: conn,
                                                                                     uniqueid1: response.itemdata.rentalitemid,
                                                                                     uniqueid2: string.Empty,
                                                                                     uniqueid3: string.Empty,
                                                                                     description: string.Empty,
                                                                                     rectype: string.Empty);
                }
                if ((response.itemdata.trackedby != "BARCODE") || (response.itemdata.images.Length == 0))
                {
                    response.itemdata.images = await this.AppData.GetPrimaryAppImageThumbnailAsync(conn: conn,
                                                                                     uniqueid1: response.itemdata.masterId,
                                                                                     uniqueid2: string.Empty,
                                                                                     uniqueid3: string.Empty,
                                                                                     description: string.Empty,
                                                                                     rectype: string.Empty);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="tags")]
        public async Task ItemStatusRFID(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.items = await this.AppData.ItemStatusRFIDAsync(conn: conn,
                                                                  tags: request.tags); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
