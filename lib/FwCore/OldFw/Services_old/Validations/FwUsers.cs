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
    class FwUsers : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);
            selectQry.AddWhere("inactive <> 'T'");
        }
        //---------------------------------------------------------------------------------------------
    }
}
