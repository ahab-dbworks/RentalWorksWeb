using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace WebApi.Modules.Inventory.Asset

/*
03/20/2020 justin hoffman
keeping this empty class here to allow us to easily add expensive calculated fields here for the Asset form (only) as needed
*/

{
    [FwSqlTable("rentalitemwebview")]
    public class ItemLoader : ItemBrowseLoader
    {

    }
}