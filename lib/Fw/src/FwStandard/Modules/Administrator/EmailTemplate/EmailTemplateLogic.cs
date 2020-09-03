
using FwStandard.AppManager;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using FwStandard.BusinessLogic;

namespace FwStandard.Modules.Administrator.EmailTemplate
{
    [FwLogic(Id: "f9v9syKoKLml")]
    public class EmailTemplateLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        EmailTemplateRecord EmailTemplate = new EmailTemplateRecord();
        EmailTemplateLoader EmailTemplateLoader = new EmailTemplateLoader();
        public EmailTemplateLogic()
        {
            dataRecords.Add(EmailTemplate);
            dataLoader = EmailTemplateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ktrXR17WExSp", IsPrimaryKey: true)]
        public string AppEmailId { get { return EmailTemplate.AppEmailId; } set { EmailTemplate.AppEmailId = value; } }
        [FwLogicProperty(Id: "uW5OrFgjqpuo", IsRecordTitle: true)]
        public string Description { get { return EmailTemplate.Description; } set { EmailTemplate.Description = value; } }
        [FwLogicProperty(Id: "mraOVGcwSq2v")]
        public string FilterId { get { return EmailTemplate.FilterId; } set { EmailTemplate.FilterId = value; } }
        [FwLogicProperty(Id: "PXkH7WSV6Wag")]
        public string Subject { get; set; }
        [FwLogicProperty(Id: "vU86wOr58dMA", IsReadOnly: true)]
        public string EmailText { get { return EmailTemplate.EmailText; } set { EmailTemplate.EmailText = value; } }
        [FwLogicProperty(Id: "1sb7vwGd5K8D")]
        public string Category { get { return EmailTemplate.Category; } set { EmailTemplate.Category = value; } }
        [FwLogicProperty(Id: "uJmDGIryr3ua")]
        public string BodyFormat { get { return EmailTemplate.BodyFormat; } set { EmailTemplate.BodyFormat = value; } }
        [FwLogicProperty(Id: "Ifad86wMkUQP")]
        public string EmailType { get { return EmailTemplate.EmailType; } set { EmailTemplate.EmailType = value; } }
        [FwLogicProperty(Id: "yB8OgS7I6h7n")]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public class TemplateCategoriesResponse
        {
            public List<string> categories { get; set; }
        }
        public class TemplateFieldsResponse
        {
            public List<string> fields { get; set; }
        }
        public class GetTemplateFieldsRequest
        {
            public string category { get; set; }
        }
        public async Task<TemplateCategoriesResponse> GetTemplateCategoriesAsync()
        {
            TemplateCategoriesResponse response = new TemplateCategoriesResponse();
            List<string> categories = new List<string>();
            List<string> bodyformats = new List<string>();
            FwJsonDataTable dt;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select category");
                    qry.Add("from getappemailsetting()");
                    qry.Add("where emailtype = 'EMAIL'");
                    dt = await qry.QueryToFwJsonTableAsync();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string category = dt.Rows[i][dt.GetColumnNo("category")].ToString();
                        categories.Add(category);
                    }
                }
            }
            response.categories = categories;
            return response;
        }
        public async Task<TemplateFieldsResponse> GetTemplateFieldsAsync(GetTemplateFieldsRequest request)
        {
            TemplateFieldsResponse response = new TemplateFieldsResponse();
            FwJsonDataTable dt;
            List<string> emailFields = new List<string>();
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select * from dbo.funcgetappemailcolumns(@category)");
                    qry.AddParameter("@category", request.category);
                    dt = await qry.QueryToFwJsonTableAsync();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string field = dt.Rows[i][dt.GetColumnNo("columnname")].ToString();
                        emailFields.Add(field);
                    }
                }
            }
            response.fields = emailFields;
            return response;
        }
    }
}
