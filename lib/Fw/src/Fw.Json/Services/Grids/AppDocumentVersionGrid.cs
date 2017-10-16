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
    class AppDocumentVersionGrid : FwGrid
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            IDictionary<string, dynamic> miscfields;
            string uniqueid1, documenttypeid;
            
            FwValidate.TestPropertyDefined("AppDocumentVersion.SetBrowseQry", request, "miscfields");
            base.setBrowseQry(selectQry);
            miscfields     = request.miscfields;
            uniqueid1      = FwCryptography.AjaxDecrypt(miscfields["uniqueid1"]);
            documenttypeid = FwCryptography.AjaxDecrypt(miscfields["documenttypeid"]);
            selectQry.AddWhere("uniqueid1 = @uniqueid1");
            selectQry.AddWhere("documenttypeid = @documenttypeid");
            selectQry.AddParameter("@uniqueid1", uniqueid1);
            selectQry.AddParameter("@documenttypeid", documenttypeid);
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
            documenttypeid = FwCryptography.AjaxDecrypt(miscfields["documenttypeid"]);
            uniqueid1      = FwCryptography.AjaxDecrypt(miscfields["uniqueid1"]);
            uniqueid2      = string.Empty;
            uniqueid1int   = 0;
            //if (FwValidate.IsPropertyDefined(request.miscfields.image, "filedataurl") && FwValidate.IsPropertyDefined(request.miscfields.image, "filepath"))
            //{
            //    filedataurl  = request.miscfields.image.filedataurl;
            //    filepath     = request.miscfields.image.filepath;
            //}
            if (FwValidate.IsPropertyDefined(request.miscfields.image, "filedataurl") && FwValidate.IsPropertyDefined(request.miscfields.image, "filepath") && FwValidate.IsPropertyDefined(request.miscfields.image, "ismodified"))
            {
                if (request.miscfields.image.ismodified)
                {
                    filedataurl      = request.miscfields.image.filedataurl;
                    filepath         = request.miscfields.image.filepath;
                    uploadpending    = (session.security.webUser.usertype == "CONTACT");
                    //insertNewVersion = true;
                }
            }
            received         = form.Tables["appdocument"].GetField("received").Value;
            reviewed         = form.Tables["appdocument"].GetField("reviewed").Value;
            expiration       = form.Tables["appdocument"].GetField("expiration").Value;
            contactid        = (session.security.webUser.usertype == "CONTACT") ? session.security.webUser.contactid : "";
            inputbyusersid   = (session.security.webUser.usertype == "USER")    ? session.security.webUser.usersid   : "";
            projectid        = string.Empty;
            description      = null;
            notes            = null;
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
            //FwSqlCommand cmd = new FwSqlCommand(this.form.DatabaseConnection);
            //cmd.Add("delete appimage");
            //cmd.Add("where uniqueid1 = @uniqueid1");
            //cmd.AddParameter("@uniqueid1", appdocumentid);
            //cmd.ExecuteNonQuery();
        }
        //---------------------------------------------------------------------------------------------
    }
}
