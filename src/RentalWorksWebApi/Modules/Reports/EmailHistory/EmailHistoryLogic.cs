using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.EmailHistory
{
    [FwLogic(Id: "uSzhCxnn4Th2")]
    public class EmailHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        EmailHistoryRecord emailHistory = new EmailHistoryRecord();
        EmailHistoryLoader emailHistoryLoader = new EmailHistoryLoader();
        public EmailHistoryLogic()
        {
            dataRecords.Add(emailHistory);
            dataLoader = emailHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "y2r5hyD6PrQK", IsPrimaryKey: true)]
        public string EmailHistoryId { get { return emailHistory.EmailHistoryId; } set { emailHistory.EmailHistoryId = value; } }
        [FwLogicProperty(Id: "JD0l6JSTAlol")]
        public string ReportId { get { return emailHistory.ReportId; } set { emailHistory.ReportId = value; } }
        [FwLogicProperty(Id: "GWZF62AEEeVX")]
        public string FromUserId { get { return emailHistory.FromUserId; } set { emailHistory.FromUserId = value; } }
        [FwLogicProperty(Id: "5Z2XW2hdQXzdz")]
        public string FromWebUserId { get; set; }
        [FwLogicProperty(Id: "EDp5xFz69h1fX", IsReadOnly: true)]
        public string FromUser { get; set; }
        [FwLogicProperty(Id: "SxlN6vIxhsw5")]
        public string EmailDate { get { return emailHistory.EmailDate; } set { emailHistory.EmailDate = value; } }
        [FwLogicProperty(Id: "tMVTqHqjIXEb")]
        public string Status { get { return emailHistory.Status; } set { emailHistory.Status = value; } }
        [FwLogicProperty(Id: "O3fqEHmujOj7")]
        public string EmailText { get { return emailHistory.EmailText; } set { emailHistory.EmailText = value; } }
        [FwLogicProperty(Id: "98vAiucdo253")]
        public string EmailTo { get { return emailHistory.EmailTo; } set { emailHistory.EmailTo = value; } }
        [FwLogicProperty(Id: "tsrymk7Lf25I", IsRecordTitle: true)]
        public string EmailSubject { get { return emailHistory.EmailSubject; } set { emailHistory.EmailSubject = value; } }
        [FwLogicProperty(Id: "habL5X0QILcF")]
        public string EmailCC { get { return emailHistory.EmailCC; } set { emailHistory.EmailCC = value; } }
        [FwLogicProperty(Id: "a31kv4RCNfa2")]
        public string Title { get { return emailHistory.Title; } set { emailHistory.Title = value; } }
        [FwLogicProperty(Id: "QSZKOHA6nrng")]
        public string RelatedToId { get { return emailHistory.RelatedToId; } set { emailHistory.RelatedToId = value; } }
        //[FwLogicProperty(Id: "hZwRvC8bnOFe")]
        //public Unknown___varbinary PdfAttachment { get { return emailHistory.PdfAttachment; } set { emailHistory.PdfAttachment = value; } }
        [FwLogicProperty(Id: "rhEv8jmF0vOn")]
        public string DateStamp { get { return emailHistory.DateStamp; } set { emailHistory.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
