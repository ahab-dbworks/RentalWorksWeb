using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;

namespace RwQBO.Source.Validations
{
    class UserWarehouse : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);

            selectQry.AddWhere("locationid = @locationid");
            selectQry.AddParameter("@locationid", FwCryptography.AjaxDecrypt(request.boundids.location));
        }
        //---------------------------------------------------------------------------------------------
    }
}
