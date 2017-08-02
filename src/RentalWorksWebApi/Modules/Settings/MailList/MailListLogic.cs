using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Data.Settings;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.MailList
{
    public class MailListLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        MailListRecord mailList = new MailListRecord();
        public MailListLogic()
        {
            dataRecords.Add(mailList);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MailListId { get { return mailList.MailListId; } set { mailList.MailListId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string MailList { get { return mailList.MailList; } set { mailList.MailList = value; } }
        public bool Inactive { get { return mailList.Inactive; } set { mailList.Inactive = value; } }
        public string DateStamp { get { return mailList.DateStamp; } set { mailList.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
