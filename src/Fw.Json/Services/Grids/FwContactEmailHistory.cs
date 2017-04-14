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
    class FwContactEmailHistory : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid, usersid, contactEmail;
            FwSqlCommand qry;

            usersid = this.session.security.webUser.usersid;
            FwValidate.TestPropertyDefined("FwContactEmailHistory.SetBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            miscfields = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            contactid = FwCryptography.AjaxDecrypt(fieldContactId.value);
            qry = new FwSqlCommand(this.browse.DatabaseConnection);
            qry.Add("select email = lower(email)");
            qry.Add("from contact");
            qry.Add("where contactid = @contactid");
            qry.AddParameter("@contactid", contactid);
            qry.Execute();
            contactEmail = qry.GetField("email").ToString();
            selectQry.AddWhere("lower(emailto) like @contactemail");
            selectQry.AddParameter("@contactemail", "%" + contactEmail + "%");
        }
        //---------------------------------------------------------------------------------------------
    }
}
