using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class ContactEventLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactEventRecord contactEvent = new ContactEventRecord();
        public ContactEventLogic()
        {
            dataRecords.Add(contactEvent);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContactEventId { get { return contactEvent.ContactEventId; } set { contactEvent.ContactEventId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ContactEvent { get { return contactEvent.ContactEvent; } set { contactEvent.ContactEvent = value; } }
        public string Color { get { return contactEvent.Color;  } set { contactEvent.Color = value; } }
        public bool WhiteText { get { return contactEvent.WhiteText;  } set { contactEvent.WhiteText = value; } }
        public bool Recurring { get { return contactEvent.Recurring; } set { contactEvent.Recurring = value; } }
        public bool Inactive { get { return contactEvent.Inactive; } set { contactEvent.Inactive = value; } }
        public string DateStamp { get { return contactEvent.DateStamp; } set { contactEvent.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
