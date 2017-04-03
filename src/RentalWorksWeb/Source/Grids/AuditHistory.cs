using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Grids
{
    class AuditHistory : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            dynamic fieldUniqueId;
            string uniqueid;
            
            FwValidate.TestPropertyDefined("AuditHistory.setBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            fieldUniqueId = request.uniqueid;
            uniqueid = FwCryptography.AjaxDecrypt(fieldUniqueId);
            selectQry.AddWhere("uniqueid = @uniqueid");
            selectQry.AddParameter("@uniqueid", uniqueid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
