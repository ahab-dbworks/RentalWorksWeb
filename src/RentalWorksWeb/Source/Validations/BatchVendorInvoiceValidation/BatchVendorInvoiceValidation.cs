using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksWeb.Source.Validations
{
    class BatchVendorInvoiceValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);

            selectQry.AddWhere("batchtype = 'VENDORINVOICE'");
            selectQry.AddWhere("locationid = @locationid");
            selectQry.AddParameter("@locationid", session.security.webUser.locationid);
        }
        //---------------------------------------------------------------------------------------------
    }
}

