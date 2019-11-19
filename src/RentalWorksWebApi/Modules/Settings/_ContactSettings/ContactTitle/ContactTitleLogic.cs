using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.ContactSettings.ContactTitle
{
    [FwLogic(Id:"DpcIACJfpPVp")]
    public class ContactTitleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactTitleRecord contactTitle = new ContactTitleRecord();
        public ContactTitleLogic()
        {
            dataRecords.Add(contactTitle);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"w2TZuGZWemY0", IsPrimaryKey:true)]
        public string ContactTitleId { get { return contactTitle.ContactTitleId; } set { contactTitle.ContactTitleId = value; } }

        [FwLogicProperty(Id:"w2TZuGZWemY0", IsRecordTitle:true)]
        public string ContactTitle { get { return contactTitle.ContactTitle; } set { contactTitle.ContactTitle = value; } }

        [FwLogicProperty(Id:"f7Vt5K9Jicq6")]
        public string TitleType { get { return contactTitle.TitleType;  } set { contactTitle.TitleType = value; } }

        [FwLogicProperty(Id:"J6DsIFBwB2Ax")]
        public bool? AccountsPayable { get { return contactTitle.AccountsPayable;  } set { contactTitle.AccountsPayable = value; } }

        [FwLogicProperty(Id:"i21PrCIlt8C3")]
        public bool? AccountsReceivable { get { return contactTitle.AccountsReceivable; } set { contactTitle.AccountsReceivable = value; } }

        [FwLogicProperty(Id:"Jn8jfpflRfkA")]
        public bool? Inactive { get { return contactTitle.Inactive; } set { contactTitle.Inactive = value; } }

        [FwLogicProperty(Id:"lOr5zxVzWs5y")]
        public string DateStamp { get { return contactTitle.DateStamp; } set { contactTitle.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
