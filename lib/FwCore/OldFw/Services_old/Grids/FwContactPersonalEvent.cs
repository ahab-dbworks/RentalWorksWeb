using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using FwCore.Services;
using FwCore.SqlServer;
using FwCore.Utilities;
using FwCore.ValueTypes;

namespace FwCore.Services.Grids
{
    class FwContactPersonalEvent : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid;
            
            FwValidate.TestPropertyDefined("FwContactPersonalEvent.SetBrowseQry", request, "miscfields");
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
                
            FwValidate.TestPropertyDefined("FwContactPersonalEvent.BeforeInsert", request, "miscfields");
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
