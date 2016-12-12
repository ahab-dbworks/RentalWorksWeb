using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksWebLibrary.Validations
{
    class RwUserOfficeLocation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);

            selectQry.AddParameter("@webusersid", session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
