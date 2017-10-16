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
    class ContactDocumentGrid : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, object> miscfields;
            dynamic fieldContactId;
            string contactid;
            
            FwValidate.TestPropertyDefined("ContactDocument.SetBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            miscfields = request.miscfields;
            fieldContactId = miscfields["contact.contactid"];
            contactid = FwCryptography.AjaxDecrypt(fieldContactId.value);
            selectQry.AddWhere("uniqueid1 = @contactid");
            selectQry.AddWhere("uniqueid2 = ''");
            selectQry.AddWhere("uniqueid1int = 0");
            selectQry.AddParameter("@contactid", contactid);
        }
        //---------------------------------------------------------------------------------------------
        protected override void insert() { update(); }
        //---------------------------------------------------------------------------------------------
        protected override void update()
        {
            string appdocumentid, documenttypeid, uniqueid1, uniqueid2, filedataurl=null, filepath=null, inputbyusersid="", contactid="", projectid, description, notes;
            FwDateTime received="", reviewed="", expiration="";
            IDictionary<string, dynamic> miscfields;
            int uniqueid1int;
            bool uploadpending=false, insertNewVersion=false;

            miscfields     = request.miscfields;
            appdocumentid  = form.Tables["appdocument"].GetUniqueId("appdocumentid").Value;
            documenttypeid = form.Tables["appdocument"].GetField("documenttypeid").Value;
            uniqueid1      = FwCryptography.AjaxDecrypt(miscfields["contact.contactid"].value);
            uniqueid2      = string.Empty;
            uniqueid1int   = 0;
            if (FwValidate.IsPropertyDefined(request.miscfields.image, "filedataurl") && FwValidate.IsPropertyDefined(request.miscfields.image, "filepath"))
            {
                filedataurl    = request.miscfields.image.filedataurl;
                filepath       = request.miscfields.image.filepath;
            }
            received       = form.Tables["appdocument"].GetField("received").Value;
            reviewed       = form.Tables["appdocument"].GetField("reviewed").Value;
            expiration     = form.Tables["appdocument"].GetField("expiration").Value;
            contactid      = (session.security.webUser.usertype == "CONTACT") ? session.security.webUser.contactid : "";
            inputbyusersid = (session.security.webUser.usertype == "USER")    ? session.security.webUser.usersid   : "";
            projectid      = string.Empty;
            description    = null;
            notes          = null;
            FwSqlData.UpdateAppDocumentImage(appdocumentid, documenttypeid, uniqueid1, uniqueid2, uniqueid1int, filedataurl, filepath, received, reviewed, expiration, inputbyusersid, contactid, projectid, description, uploadpending, insertNewVersion, notes);
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeDelete()
        {
            IDictionary<string, object> ids;
            dynamic fieldAppDocumentId;
            string appdocumentid;
            
            base.beforeDelete();
            ids                = request.ids;
            fieldAppDocumentId = ids["appdocument.appdocumentid"];
            appdocumentid      = FwCryptography.AjaxDecrypt(fieldAppDocumentId.value);
            
            // this is a hack and should ideally run in the delete transaction
            FwSqlCommand cmd = new FwSqlCommand(this.form.DatabaseConnection);
            cmd.Add("delete appimage");
            cmd.Add("where uniqueid1 = @uniqueid1");
            cmd.AddParameter("@uniqueid1", appdocumentid);
            cmd.ExecuteNonQuery();
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            switch ((string)request.method)
            {
                case "ToggleAppdocumentInactive": ToggleAppdocumentInactive(); break;
            }
        }
        //---------------------------------------------------------------------------------------------
        private void ToggleAppdocumentInactive()
        {
            string appdocumentid;
            if (FwValidate.IsPropertyDefined(request, "appdocumentid"))
            {
                appdocumentid = FwCryptography.AjaxDecrypt(request.appdocumentid);
                FwSqlData.ToggleAppdocumentInactive(appdocumentid);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
