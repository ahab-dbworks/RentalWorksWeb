using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.ContactTitle
{
    public class ContactTitleLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactTitleRecord contactTitle = new ContactTitleRecord();
        public ContactTitleLogic()
        {
            dataRecords.Add(contactTitle);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContactTitleId { get { return contactTitle.ContactTitleId; } set { contactTitle.ContactTitleId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ContactTitle { get { return contactTitle.ContactTitle; } set { contactTitle.ContactTitle = value; } }
        public string TitleType { get { return contactTitle.TitleType;  } set { contactTitle.TitleType = value; } }
        public bool AccountsPayable { get { return contactTitle.AccountsPayable;  } set { contactTitle.AccountsPayable = value; } }
        public bool AccountsReceivable { get { return contactTitle.AccountsReceivable; } set { contactTitle.AccountsReceivable = value; } }
        public bool Inactive { get { return contactTitle.Inactive; } set { contactTitle.Inactive = value; } }
        public string DateStamp { get { return contactTitle.DateStamp; } set { contactTitle.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
