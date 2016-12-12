using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksQuikScanLibrary.Validations
{
    class RwSetCharactersByICode : FwValidation
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
