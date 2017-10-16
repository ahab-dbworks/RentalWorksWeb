using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;

namespace RentalWorksQuikScan.Source.Validations
{
    class SetCharactersByICodeValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks,session.security.webUser.usersid);
            base.setBrowseQry(selectQry); 
            selectQry.AddParameter("@masterid", FwCryptography.AjaxDecrypt(request.boundids.masterid));
        }
        //---------------------------------------------------------------------------------------------
    }
}
