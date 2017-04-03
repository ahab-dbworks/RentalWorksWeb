﻿using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksWeb.Source.Validations
{
    class UserOfficeLocation : FwValidation
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
