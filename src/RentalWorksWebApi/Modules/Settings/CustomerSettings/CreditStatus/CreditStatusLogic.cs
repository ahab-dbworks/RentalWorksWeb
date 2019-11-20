using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CustomerSettings.CreditStatus
{
    [FwLogic(Id:"LkEGdUJMMDql")]
    public class CreditStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CreditStatusRecord creditStatus = new CreditStatusRecord();
        public CreditStatusLogic()
        {
            dataRecords.Add(creditStatus);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"L44QxXGriI0R", IsPrimaryKey:true)]
        public string CreditStatusId { get { return creditStatus.CreditStatusId; } set { creditStatus.CreditStatusId = value; } }

        [FwLogicProperty(Id:"L44QxXGriI0R", IsRecordTitle:true)]
        public string CreditStatus { get { return creditStatus.CreditStatus; } set { creditStatus.CreditStatus = value; } }

        [FwLogicProperty(Id:"v9EIheqZGVoc")]
        public bool? CreateContractAllowed { get { return creditStatus.CreateContractAllowed; } set { creditStatus.CreateContractAllowed = value; } }

        [FwLogicProperty(Id:"TmNEHLIVBbZs")]
        public bool? Inactive { get { return creditStatus.Inactive; } set { creditStatus.Inactive = value; } }

        [FwLogicProperty(Id:"1Ulf2XayNm5b")]
        public string DateStamp { get { return creditStatus.DateStamp; } set { creditStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
