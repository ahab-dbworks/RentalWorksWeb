using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RwQBO.Source.Validations
{
    class BatchARValidation : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);

            selectQry.AddWhere("batchtype = 'AR'");
            selectQry.AddWhere("locationid = @locationid");
            selectQry.AddParameter("@locationid", session.security.webUser.locationid);
        }
        //---------------------------------------------------------------------------------------------
    }
}

