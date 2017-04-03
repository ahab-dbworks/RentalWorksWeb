using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace Fw.Json.Services.Validations
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
