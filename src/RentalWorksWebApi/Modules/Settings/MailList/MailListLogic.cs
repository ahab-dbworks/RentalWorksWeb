using FwStandard.AppManager;
using WebApi.Data.Settings;
using WebApi.Logic;

namespace WebApi.Modules.Settings.MailList
{
    [FwLogic(Id:"2CB6bfXITDyX")]
    public class MailListLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        MailListRecord mailList = new MailListRecord();
        public MailListLogic()
        {
            dataRecords.Add(mailList);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"ff6IRyHI6fXr", IsPrimaryKey:true)]
        public string MailListId { get { return mailList.MailListId; } set { mailList.MailListId = value; } }

        [FwLogicProperty(Id:"ff6IRyHI6fXr", IsRecordTitle:true)]
        public string MailList { get { return mailList.MailList; } set { mailList.MailList = value; } }

        [FwLogicProperty(Id:"QODB8LdlCDH")]
        public bool? Inactive { get { return mailList.Inactive; } set { mailList.Inactive = value; } }

        [FwLogicProperty(Id:"eiQEubRgGP1")]
        public string DateStamp { get { return mailList.DateStamp; } set { mailList.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
