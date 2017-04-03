using System.Collections.Generic;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
namespace Fw.Json.Services.Validations
{
    class FwContactCompany : FwValidation
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid;
            
            FwValidate.TestPropertyDefined("FwContactCompany.SetBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            
            //MY 3/17/2015: validation should not filter based on contact ID according to ahab.
            miscfields = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            contactid = FwCryptography.AjaxDecrypt(fieldContactId.value);
            //selectQry.AddWhere("inactive <> 'T'");
            selectQry.AddWhere("contactid = @contactid");
            selectQry.AddParameter("@contactid", contactid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
