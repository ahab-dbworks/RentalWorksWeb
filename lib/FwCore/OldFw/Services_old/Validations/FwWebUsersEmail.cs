using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using FwCore.Services;
using FwCore.SqlServer;
using FwCore.Utilities;
using FwCore.ValueTypes;

namespace FwCore.Services.Validations
{
    class FwWebUsersEmail : FwValidation
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
