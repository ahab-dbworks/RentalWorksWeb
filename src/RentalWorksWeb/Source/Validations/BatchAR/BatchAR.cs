using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Validations
{
    class BatchAR : FwValidation
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

