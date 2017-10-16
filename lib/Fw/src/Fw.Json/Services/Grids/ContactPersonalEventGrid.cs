using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace Fw.Json.Services.Grids
{
    class ContactPersonalEventGrid : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid;
            
            FwValidate.TestPropertyDefined("ContactPersonalEvent.SetBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            miscfields = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            contactid = FwCryptography.AjaxDecrypt(fieldContactId.value);
            selectQry.AddWhere("contactid = @contactid");
            selectQry.AddParameter("@contactid", contactid);
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string personaleventid, contactid;
                
            FwValidate.TestPropertyDefined("ContactPersonalEvent.BeforeInsert", request, "miscfields");
            base.beforeInsert();
            miscfields      = request.miscfields;
            fieldContactId  = miscfields["contact.contactid"];
            personaleventid = FwSqlData.GetNextId(this.form.DatabaseConnection);
            contactid       = FwCryptography.AjaxDecrypt(fieldContactId.value);
            form.Tables["personalevent"].GetField("personaleventid").Value = personaleventid;
            form.Tables["personalevent"].GetField("contactid").Value       = contactid;
        }
        //---------------------------------------------------------------------------------------------
    }
}
