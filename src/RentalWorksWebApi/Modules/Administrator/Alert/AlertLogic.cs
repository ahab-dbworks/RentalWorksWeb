using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.Alert
{
    [FwLogic(Id: "uDfoWwYV6HwI ")]
    public class AlertLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AlertRecord alert = new AlertRecord();
        public AlertLogic()
        {
            dataRecords.Add(alert);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "6c9PL4mHxpfO ")]
        public string AlertId { get { return alert.AlertId; } set { alert.AlertId = value; } }
        [FwLogicProperty(Id: "QlcNGC0VsxNE ")]
        public string AlertName { get { return alert.AlertName; } set { alert.AlertName = value; } }
        [FwLogicProperty(Id: "iL9L6gQI0Bh6 ")]
        public string ModuleName { get { return alert.ModuleName; } set { alert.ModuleName = value; } }
        [FwLogicProperty(Id: "bOJyRGKEFtWo ")]
        public bool? Inactive { get { return alert.Inactive; } set { alert.Inactive = value; } }
        [FwLogicProperty(Id: "d3w1qNkDKUVBP")]
        public string DateStamp { get { return alert.DateStamp; } set { alert.DateStamp = value; } }
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
