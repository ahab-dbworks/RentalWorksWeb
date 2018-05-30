using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace Web.Source.Validations
{
    class WebUsersEmailValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);
            selectQry.AddWhere("rtrim(isnull(email,'')) <> ''");
            selectQry.AddWhere("inactive <> 'T'");
        }
        //---------------------------------------------------------------------------------------------
    }
}
