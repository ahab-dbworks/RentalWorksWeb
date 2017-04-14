using Fw.Json.SqlServer;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Fw.Json.Services.Common;
using Fw.Json.Utilities;
using System.Reflection;
using System.Drawing;
using System.Text;
using System.IO;

namespace Fw.Json.Services
{
    public class FwModule
    {
        protected dynamic request, response, session;
        protected FwForm form;
        protected FwBrowse browse;
        public enum ComponentTypes {Module, Grid, Validation}
        protected ComponentTypes ComponentType {get;set;}
        private string applicationServicesNamespace;
        private string applicationServicesTypePrefix;
        private Assembly webServiceAssembly;
        //---------------------------------------------------------------------------------------------
        public FwModule() : this("Module")
        {
            
        }
        //---------------------------------------------------------------------------------------------
        public FwModule(string componentType)
        {
            this.ComponentType = (ComponentTypes)Enum.Parse(typeof(ComponentTypes), componentType);
        }
        //---------------------------------------------------------------------------------------------
        public void Init(string applicationServicesNamespace, string applicationServicesTypePrefix, Assembly webServiceAssembly, IDictionary<String, object> irequest, IDictionary<String, object> iresponse, IDictionary<String, object> isession)
        {
            FwApplicationSchema.Browse browseSchema;
            FwApplicationSchema.Form formSchema;
            string name;

            this.request  = irequest;
            this.response = iresponse;
            this.session  = isession;
            FwValidate.TestPropertyDefined("FwModule.Init", this.request, "module");
            name = request.module;
            this.applicationServicesNamespace = applicationServicesNamespace;
            this.applicationServicesTypePrefix = applicationServicesTypePrefix;
            this.webServiceAssembly = webServiceAssembly;
            switch(ComponentType)
            {
                case ComponentTypes.Module:
                    browseSchema = FwApplicationSchema.Current.Modules[name].Browse;
                    this.browse  = new FwBrowse(browseSchema);
                    formSchema   = FwApplicationSchema.Current.Modules[name].Form;
                    this.form    = new FwForm(applicationServicesNamespace, applicationServicesTypePrefix, webServiceAssembly, formSchema);
                    this.form.OnLoadCalculatedFields += form_OnLoadCalculatedFields;
                    break;
                case ComponentTypes.Grid:
                    browseSchema = FwApplicationSchema.Current.Grids[name].Browse;
                    this.browse  = new FwBrowse(browseSchema);
                    formSchema   = FwApplicationSchema.Current.Grids[name].Form;
                    this.form    = new FwForm(applicationServicesNamespace, applicationServicesTypePrefix, webServiceAssembly, formSchema);
                    this.form.OnLoadCalculatedFields += form_OnLoadCalculatedFields;
                    break;  
                case ComponentTypes.Validation:
                    browseSchema = FwApplicationSchema.Current.Validations[name].Browse;
                    this.browse  = new FwBrowse(browseSchema);
                    break;
                default:
                    throw new Exception("Invalid Component Type '" + ComponentType + "'. [FwModule.cs:Init]");
            }
            
        }
        //---------------------------------------------------------------------------------------------
        public virtual void Load()
        {
            Dictionary<string, FwJsonFormTable> jsonTables;
            string tabName;
            this.form.Mode = Fw.Json.Services.FwForm.Modes.EDIT;

            this.beforeLoad();
            form.LoadSchemaTableColumns();
            jsonTables       = form.Load();
            this.afterLoad(jsonTables);

            tabName          = this.getTabName();
            response.tables  = jsonTables;
            response.tabname = tabName;
        }
        //---------------------------------------------------------------------------------------------
        void form_OnLoadCalculatedFields(object sender, FwForm.EventArgsOnLoadCalculatedField e)
        {
            e.JsonTables["#"] = new FwJsonFormTable();
            LoadCalculatedFields(e.JsonTables);
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void LoadCalculatedFields(Dictionary<string, FwJsonFormTable> jsonTables)
        {
            
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void gatherData()
        {
            dynamic uniqueids, fields;
            FwValidate.TestPropertyDefined("FwModule.GatherData", request, "module");
            FwValidate.TestPropertyDefined("FwModule.GatherData", request, "ids");
            uniqueids = request.ids;
            fields    = null;
            if (FwValidate.IsPropertyDefined(request, "fields"))
            {
                fields = request.fields;
            }
            form.GatherData(uniqueids, fields);
        }
        //---------------------------------------------------------------------------------------------
        protected virtual bool validateForm()
        {
            bool isValid;
            
            isValid = form.ValidateRequiredFields();

            return isValid;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void prepData()
        {
            FwSqlConnection conn = new FwSqlConnection(this.form.Database);
            foreach (var tableitem in this.form.Tables)
            {
                FwSqlTable sqlTable;
                sqlTable = tableitem.Value;
                foreach (var columnItem in sqlTable.Fields)
                {
                    string columnName, columnValue, dataType;
                    FwSqlColumn column;

                    column         = columnItem.Value;
                    columnName     = column.Column;
                    columnValue    = column.Value;

                    if (this.form.FormSchema.Tables[sqlTable.Table].UniqueIds.ContainsKey(column.Column))
                    {
                        dataType = this.form.FormSchema.Tables[sqlTable.Table].UniqueIds[column.Column].DataType;
                        switch(dataType)
                        {
                            case "key":
                                column.Value = FwCryptography.AjaxDecrypt(columnValue);
                                break;
                        }
                    }
                    if (this.form.FormSchema.Tables[sqlTable.Table].Columns.ContainsKey(column.Column))
                    {
                        dataType = this.form.FormSchema.Tables[sqlTable.Table].Columns[column.Column].DataType;
                        switch(dataType)
                        {
                            case "key":
                                column.Value = FwCryptography.AjaxDecrypt(columnValue);
                                break;
                            case "validation":
                                column.Value = FwCryptography.AjaxDecrypt(columnValue);
                                break;
                            case "color":
                                column.Value = FwConvert.HexToOle(columnValue).ToString();
                                break;
                            case "password":
                                column.Value = FwCryptography.DbwEncrypt(conn, columnValue.ToUpper()); //All passwords saved in uppercase
                                break;
                            case "ssn":
                                column.Value = FwCryptography.DbwEncrypt(conn, columnValue);
                                break;
                            case "encrypt":
                                column.Value = FwCryptography.DbwEncrypt(conn, columnValue);
                                break;
                            //case "combobox":
                            //    column.Value = FwCryptography.AjaxDecrypt(columnValue);
                            //    break;
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void beforeInsert()
        {
            
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void insert()
        {
            this.form.Insert();
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void afterInsert()
        {
            FwSqlConnection conn = new FwSqlConnection(this.form.Database);
            if (this.form.FormSchema.HasAudit)
            {
                string uniqueId, webUsersId;
                uniqueId = getFormUniqueId();
                webUsersId = session.security.webUser.webusersid;
                if (uniqueId == "")
                {
                    throw new Exception("Missing unique id. [FwModule.cs:AfterInsert]");
                }
                FwSqlData.InsertWebAudit(conn, uniqueId, webUsersId, "T");
            }
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void beforeUpdate()
        {
            
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void update()
        {
            this.form.Update();
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void afterUpdate()
        {
            FwSqlConnection conn = new FwSqlConnection(this.form.Database);
            if (this.form.FormSchema.HasAudit)
            {
                string uniqueId, webUsersId;
                uniqueId = getFormUniqueId();
                webUsersId = session.security.webUser.webusersid;
                if (uniqueId == "")
                {
                    throw new Exception("Missing unique id. [FwModule.cs:AfterUpdate]");
                }
                FwSqlData.InsertWebAudit(conn, uniqueId, webUsersId, "");
            }
        }
        //---------------------------------------------------------------------------------------------
        public virtual void Save()
        {
            bool isValid;
            
            FwValidate.TestPropertyDefined("FwModule.Save", this.request, "mode");
            this.form.Mode = (Fw.Json.Services.FwForm.Modes)Enum.Parse(typeof(Fw.Json.Services.FwForm.Modes), this.request.mode);
            this.gatherData();
            isValid = this.validateForm();
            if (isValid)
            {
                this.prepData();
                switch(this.form.Mode)
                {
                    case Fw.Json.Services.FwForm.Modes.NEW:
                        this.beforeInsert();
                        this.insert();
                        this.afterInsert();
                        this.form.Mode = FwForm.Modes.EDIT;
                        break;
                    case Fw.Json.Services.FwForm.Modes.EDIT:
                        this.beforeUpdate();
                        this.update();
                        this.afterUpdate();
                        break;
                }
                response.saved = true;
                switch(ComponentType)
                {
                    case ComponentTypes.Module:
                        this.form.LoadSchemaTableColumns();
                        response.tables  = this.form.Load();
                        response.tabname = this.getTabName();
                        break;
                }
            }
            else
            {
                response.saved = false;
            }
        }
        //---------------------------------------------------------------------------------------------
        public virtual void Browse()
        {
            int pageNo, pageSize;
            List<string> searchfields, searchfieldoperators, searchfieldvalues;
            string orderby;
            FwSqlSelect selectQry;
            FwJsonDataTable jsonTable;

            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "module");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "pageno");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "pagesize");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "searchfields");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "searchfieldoperators");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "searchfieldvalues");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "orderby");
            pageNo               = this.request.pageno;
            pageSize             = this.request.pagesize;
            searchfields         = (this.request.searchfields.Length      > 0) ? new List<string>(this.request.searchfields)      : new List<string>();
            searchfieldoperators = (this.request.searchfieldoperators.Length > 0) ? new List<string>(this.request.searchfieldoperators) : new List<string>();
            searchfieldvalues    = (this.request.searchfieldvalues.Length > 0) ? new List<string>(this.request.searchfieldvalues) : new List<string>();
            orderby              = this.request.orderby;
            selectQry            = this.browse.GetBrowseQry(pageNo, pageSize, searchfields, searchfieldoperators, searchfieldvalues, orderby);
            this.setBrowseQry(selectQry);
            jsonTable            = selectQry.SqlCommand.QueryToFwJsonTable(selectQry, this.browse.browseSchema);
            this.response.browse = this.getBrowseQryResponse(jsonTable);
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void setBrowseQry(FwSqlSelect selectQry)
        {
            
        }
        //---------------------------------------------------------------------------------------------
        protected virtual FwJsonDataTable getBrowseQryResponse(FwJsonDataTable jsonTable)
        {
            return jsonTable;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void beforeDelete()
        {

        }
        //---------------------------------------------------------------------------------------------
        public virtual void Delete()
        {
            this.gatherData();
            this.beforeDelete();
            this.form.Delete();
            this.AfterDelete();
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void AfterDelete()
        {

        }
        //---------------------------------------------------------------------------------------------
        public virtual void ExportBrowseXLSX()
        {
            int pageNo, pageSize;
            List<string> searchfields, searchfieldoperators, searchfieldvalues;
            string orderby, saveas;
            FwSqlSelect selectQry;
            FwJsonDataTable jsonTable;

            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "module");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "pageno");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "pagesize");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "searchfields");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "searchfieldoperators");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "searchfieldvalues");
            FwValidate.TestPropertyDefined("FwModule.Browse", this.request, "orderby");
            //pageNo               = this.request.pageno;
            pageNo = 0;
            //pageSize             = this.request.pagesize;
            pageSize = 0;
            searchfields         = (this.request.searchfields.Length         > 0) ? new List<string>(this.request.searchfields)         : new List<string>();
            searchfieldoperators = (this.request.searchfieldoperators.Length > 0) ? new List<string>(this.request.searchfieldoperators) : new List<string>();
            searchfieldvalues    = (this.request.searchfieldvalues.Length    > 0) ? new List<string>(this.request.searchfieldvalues)    : new List<string>();
            orderby              = this.request.orderby;
            selectQry            = this.browse.GetBrowseQry(pageNo, pageSize, searchfields, searchfieldoperators, searchfieldvalues, orderby);
            this.setBrowseQry(selectQry);
            jsonTable            = selectQry.SqlCommand.QueryToFwJsonTable(selectQry, this.browse.browseSchema);
            saveas               = this.request.saveas + ".xlsx";
            this.response.downloadurl = jsonTable.ExportXLSX(saveas);
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void beforeLoad()
        {
            this.gatherData();
        }
        //---------------------------------------------------------------------------------------------
        protected virtual void afterLoad(Dictionary<string, FwJsonFormTable> jsonTables)
        {

        }
        //---------------------------------------------------------------------------------------------
        protected virtual string getTabName()
        {
            return string.Empty;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string getFormUniqueId()
        {
            return string.Empty;
        }
        //---------------------------------------------------------------------------------------------
        public virtual void GetDisplayFieldQuery(FwSqlSelect selectQuery)
        {

        }
        //---------------------------------------------------------------------------------------------
        protected string getUniqueIdFromRequest(string datafield)
        {
            string encryptedUniqueid, uniqueid;
            
            FwValidate.TestPropertyDefined("FwModule.GetUniqueIdFromRequest", request.ids, datafield);
            encryptedUniqueid = ((IDictionary<string, Object>)((IDictionary<string, object>)request.ids)[datafield])["value"].ToString();
            uniqueid          = FwCryptography.AjaxDecrypt(encryptedUniqueid);

            return uniqueid;
        }
        //---------------------------------------------------------------------------------------------
        protected void setUniqueIdOnRequest(string datafield, string value)
        {
            ((IDictionary<string, Object>)((IDictionary<string, object>)request.ids)[datafield])["value"] = FwCryptography.AjaxEncrypt(value);
        }
        //---------------------------------------------------------------------------------------------
        public virtual void GetData()
        {

        }
        //---------------------------------------------------------------------------------------------
        public virtual void ValidateDuplicate()
        {
            bool duplicate;
            string table;
            Dictionary<string, string> datafields;

            datafields = new Dictionary<string, string>();
            table      = request.table;

            ValidateDatafields(datafields);

            duplicate = form.ValidateDuplicate(table, datafields);

            response.duplicate = duplicate;
        }
        //---------------------------------------------------------------------------------------------
        public virtual void ValidateDatafields(Dictionary<string, string> datafields)
        {
            string field, value, type;
            IDictionary<string, object> fields;
            dynamic fieldvalues;
            FwSqlConnection conn = new FwSqlConnection(this.form.Database);

            fields    = request.fields;
            foreach (var fieldinfo in fields)
            {
                fieldvalues = fields[fieldinfo.Key];

                field     = fieldvalues.datafield.Split('.')[1];
                type      = fieldvalues.type;
                switch (type)
                {
                    case "validation":
                        value = FwCryptography.AjaxDecrypt(fieldvalues.value);
                        break;
                    case "ssn":
                        value = FwCryptography.DbwEncrypt(conn, fieldvalues.value);
                        break;
                    default:
                        value = fieldvalues.value;
                        break;
                }
                datafields.Add(field, value);
            }
        }

        //---------------------------------------------------------------------------------------------
        #region EmailAppDocumentBrowseColumn

        public void GetFromEmail()
        {
            response.fromemail = FwSqlData.GetWebUsersView(FwSqlConnection.AppConnection, session.security.webUser.webusersid).email;
        }
        //---------------------------------------------------------------------------------------------
        public void GetEmailByWebUsersId()
        {
            const string METHOD_NAME = "GetEmailByWebUsersId";
            string[] webusersids, toemails;
            List<string> emails;
            dynamic webusersview;
            StringBuilder sb;
            FwSqlConnection conn;
            string emailto;

            
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "webusersids");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "to");
            conn = FwSqlConnection.AppConnection;
            webusersids = ((string)request.webusersids).Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            toemails    = ((string)request.to).Split(new char[]{',', ';'}, StringSplitOptions.RemoveEmptyEntries);
            emails      = new List<string>();
            foreach (string email in toemails)
            {
                if (!emails.Contains(email)) 
                {
                    emails.Add(email);
                }
            }
            foreach (string webusersidencrypted in webusersids)
            {
                string webusersid, email;
                webusersid = FwCryptography.AjaxDecrypt(webusersidencrypted);
                webusersview = FwSqlData.GetWebUsersView(conn, webusersid);
                email = webusersview.email;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    if (!emails.Contains(email)) 
                    {
                        emails.Add(email);
                    }
                }
            }
            sb = new StringBuilder();
            for (int i = 0; i < emails.Count; i++)
            {
                if (i > 0) sb.Append(";");
                sb.Append(emails[i]);
            }
            emailto = sb.ToString();
            response.emailto = emailto;
        }
        //---------------------------------------------------------------------------------------------
        public virtual void SendDocumentEmail()
        {
            string from, to, cc, subject, body, appimageid, filename, fileextension;
            dynamic webuser;

            appimageid    = FwCryptography.AjaxDecrypt(request.appimageid);
            filename      = request.filename;
            fileextension = request.fileextension;
            webuser       = FwSqlData.GetWebUsersView(FwSqlConnection.AppConnection, session.security.webUser.webusersid);
            from          = webuser.email;
            to            = request.email.to;
            cc            = request.email.cc;
            subject       = request.email.subject;
            body          = request.email.body;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.AppConnection))
            {
                qry.Add("select top 1 image");
                qry.Add("from appimage");
                qry.Add("where appimageid = @appimageid");
                qry.Parameters.AddWithValue("@appimageid", appimageid);
                qry.Execute();
                byte[] image = qry.GetField("image").ToByteArray();
                if (image == null)
                {
                    throw new Exception("Unable to send, document is null!");
                }
                using (MemoryStream stream = new MemoryStream(image))
                {
                    List<System.Net.Mail.Attachment> attachments = new List<System.Net.Mail.Attachment>();
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(stream, filename + "." + fileextension);
                    attachments.Add(attachment);
                    FwEmailService.SendEmail(FwSqlConnection.AppConnection, from, to, cc, subject, body, false, attachments);
                }
            }
        }

        #endregion EmailAppDocumentBrowseColumn
        //---------------------------------------------------------------------------------------------
    }
}
