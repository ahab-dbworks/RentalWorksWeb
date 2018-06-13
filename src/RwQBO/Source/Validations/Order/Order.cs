using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RwQBO.Source.Validations
{
    class Order : FwValidation 
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);

            selectQry.AddWhere("ordertype = 'O'");
            selectQry.AddWhere("status <> 'SNAPSHOT'");
        }
        //---------------------------------------------------------------------------------------------
    }
}

