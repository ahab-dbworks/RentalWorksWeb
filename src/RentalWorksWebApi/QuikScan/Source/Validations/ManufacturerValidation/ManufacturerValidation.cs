using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksQuikScan.Source.Validations
{
    class ManufacturerValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);
            selectQry.AddWhere("manufacturer = 'T'");
        }
        //---------------------------------------------------------------------------------------------
    }
}
