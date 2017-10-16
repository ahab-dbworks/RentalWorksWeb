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
    class ContactNoteGrid : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            FwValidate.TestPropertyDefined("ContactNote.SetBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            miscfields = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            selectQry.AddWhere("contactid = @contactid");
            //selectQry.AddWhere("inactive <> 'T'");
            selectQry.AddParameter("@contactid", FwCryptography.AjaxDecrypt(fieldContactId.value));
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactnoteid, contactid, companyid, compcontactid, usersid;
                
            FwValidate.TestPropertyDefined("ContactNote.BeforeInsert", request, "miscfields");
            base.beforeInsert();
            miscfields     = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            contactnoteid  = FwSqlData.GetNextId(this.form.DatabaseConnection);
            contactid      = FwCryptography.AjaxDecrypt(fieldContactId.value);
            companyid      = form.Tables["contactnote"].GetField("companyid").Value;
            form.Tables["contactnote"].GetField("contactnoteid").Value  = contactnoteid;
            form.Tables["contactnote"].GetField("contactid").Value      = contactid;
            compcontactid = (!string.IsNullOrEmpty(companyid)) ? this.GetCompContactId(contactid, companyid) : string.Empty;
            form.Tables["contactnote"].GetField("compcontactid").Value = compcontactid;
            usersid = session.security.webUser.usersid;
            form.Tables["contactnote"].GetField("notesbyid").Value = usersid;
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeUpdate()
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid, companyid, compcontactid, usersid;
                
            FwValidate.TestPropertyDefined("ContactNote.BeforeInsert", request, "miscfields");
            base.beforeUpdate();
            miscfields     = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            contactid      = FwCryptography.AjaxDecrypt(fieldContactId.value);
            companyid      = form.Tables["contactnote"].GetField("companyid").Value;
            compcontactid = (!string.IsNullOrEmpty(companyid)) ? this.GetCompContactId(contactid, companyid) : string.Empty;
            form.Tables["contactnote"].GetField("compcontactid").ColumnSchema.ReadOnly = false;
            form.Tables["contactnote"].GetField("compcontactid").Value = compcontactid;
            usersid = session.security.webUser.usersid;
            form.Tables["contactnote"].GetField("notesbyid").ColumnSchema.ReadOnly = false;
            form.Tables["contactnote"].GetField("notesbyid").Value = usersid;
        }
        //---------------------------------------------------------------------------------------------
        private string GetCompContactId(string contactid, string companyid)
        {
            string compcontactid;
            FwSqlCommand qry;

            qry = new FwSqlCommand(this.form.DatabaseConnection);
            qry.Add("select top 1 compcontactid");
            qry.Add("from compcontact with (nolock)");
            qry.Add("where contactid = @contactid");
            qry.Add("  and companyid = @companyid");
            qry.Add("  and inactive <> 'T'");
            qry.AddParameter("@contactid", contactid);
            qry.AddParameter("@companyid", companyid);
            qry.Execute();
            compcontactid = qry.GetField("compcontactid").ToString();

            return compcontactid;
        }
        //---------------------------------------------------------------------------------------------
    }
}
