using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksQuikScan.Source.Validations
{
    class RwPrimaryVendor : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);
            selectQry.AddWhere("inactive <> 'T'");
            selectQry.AddWhere("buyer = 'T'");
        }
        //---------------------------------------------------------------------------------------------
    }
}
