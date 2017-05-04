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
    class FwContactCompany : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid;
            
            FwValidate.TestPropertyDefined("FwContactCompany.SetBrowseQry", request, "miscfields");
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
            List<dynamic> compcontacts;
            string compcontactid, contactid, companyid;
                
            FwValidate.TestPropertyDefined("FwContactCompany.BeforeInsert", request, "miscfields");
            base.beforeInsert();
            miscfields     = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            compcontactid  = FwSqlData.GetNextId(this.form.DatabaseConnection);
            contactid      = FwCryptography.AjaxDecrypt(fieldContactId.value);
            form.Tables["compcontact"].GetField("compcontactid").Value  = compcontactid;
            form.Tables["compcontact"].GetField("contactid").Value      = contactid;
            form.Tables["compcontact"].GetField("activedate").Value      = FwConvert.ToUSShortDate(DateTime.Today);
            companyid = form.Tables["compcontact"].GetField("companyid").Value;
            compcontacts = GetCompContact(contactid, companyid);
            if (compcontacts.Count > 0)
            {
                if (compcontacts[0].inactive == "T")
                {
                    throw new Exception("An inactive contact already exists for this company.  Please activate the existing record instead of creating a new contact.");
                }
                else
                {
                    throw new Exception("An active contact already exists for this company.");
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        protected List<dynamic> GetCompContact(string contactid, string companyid)
        {
            List<dynamic> result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(this.form.DatabaseConnection);
            qry.Add("select compcontactid, inactive");
            qry.Add("from compcontact with (nolock)");
            qry.Add("where contactid = @contactid");
            qry.Add("  and companyid = @companyid");
            qry.Add("order by inactivedate desc");
            qry.AddParameter("@contactid", contactid);
            qry.AddParameter("@companyid", companyid);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            switch ((string)request.method)
            {
                case "SetCompanyContactStatus": SetCompanyContactStatus(); break;
            }
        }
        //---------------------------------------------------------------------------------------------
        private void SetCompanyContactStatus()
        {
            string compcontactid;
            bool active;
            compcontactid = FwCryptography.AjaxDecrypt(request.compcontactid);
            active        = request.active == "T";

            FwSqlData.SetCompanyContactStatus(this.form.DatabaseConnection, compcontactid, active);
        }
        //---------------------------------------------------------------------------------------------
    }
}
