using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

/*
03/17/2020 justin hoffman
keeping this empty class here to allow us to easily add expensive calculated fields here for the Vendor form (only) as needed
     
*/

namespace WebApi.Modules.Agent.Vendor
{
    [FwSqlTable("vendorview")]
    public class VendorLoader: VendorBrowseLoader  
    {
    }
}
