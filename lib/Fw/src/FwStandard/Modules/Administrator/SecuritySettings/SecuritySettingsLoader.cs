using FwStandard.AppManager;
using System.Xml.Serialization;

namespace FwStandard.Modules.Administrator.SecuritySettings
{
    [XmlRoot("settings")]
    public class SecuritySettingsLoader
    {
        [XmlElement("requireminlengthpassword")]
        public string requireminlengthpassword { get; set; }
        [XmlElement("hubspotaccesstoken")]
        public string hubspotaccesstoken { get; set; }
        [XmlElement("hubspotrefreshtoken")]
        public string hubspotrefreshtoken { get; set; }
        [XmlElement("minlengthpassword")]
        public int minlengthpassword { get; set; }
        [XmlElement("requiredigitinpassword")]
        public string requiredigitinpassword { get; set; }
        [XmlElement("requiresymbolinpassword")]
        public string requiresymbolinpassword { get; set; }
        [XmlElement("autologoutuser")]
        public string autologoutuser { get; set; }
        [XmlElement("autologoutminutes")]
        public int autologoutminutes { get; set; }
        [XmlElement("lockuserafterfailedattempts")]
        public string lockuserafterfailedattempts { get; set; }
        [XmlElement("lockuserafterfailedattemptsnumber")]
        public int lockuserafterfailedattemptsnumber { get; set; }
        [XmlElement("epochlastsynced")]
        public long epochlastsynced { get; set; } = 0;
        [FwLogicProperty(Id: "0CeIdtbq2fhA")]
        public string RecordTitle { get; set; }

    }
}
