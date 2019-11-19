using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.ContactSettings.ContactEvent
{
    [FwLogic(Id:"C603FDXMTng1")]
    public class ContactEventLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactEventRecord contactEvent = new ContactEventRecord();
        public ContactEventLogic()
        {
            dataRecords.Add(contactEvent);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"f2L0ndQdv15p", IsPrimaryKey:true)]
        public string ContactEventId { get { return contactEvent.ContactEventId; } set { contactEvent.ContactEventId = value; } }

        [FwLogicProperty(Id:"f2L0ndQdv15p", IsRecordTitle:true)]
        public string ContactEvent { get { return contactEvent.ContactEvent; } set { contactEvent.ContactEvent = value; } }

        [FwLogicProperty(Id:"eEw3iqFlVj5o")]
        public string Color { get { return contactEvent.Color;  } set { contactEvent.Color = value; } }

        [FwLogicProperty(Id:"KE2u99Y7wkcJ")]
        public bool? WhiteText { get { return contactEvent.WhiteText;  } set { contactEvent.WhiteText = value; } }

        [FwLogicProperty(Id:"MdJVSBoLbAUa")]
        public bool? Recurring { get { return contactEvent.Recurring; } set { contactEvent.Recurring = value; } }

        [FwLogicProperty(Id:"NVtw5ydVNIzZ")]
        public bool? Inactive { get { return contactEvent.Inactive; } set { contactEvent.Inactive = value; } }

        [FwLogicProperty(Id:"em9nCbeKL7O1")]
        public string DateStamp { get { return contactEvent.DateStamp; } set { contactEvent.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
