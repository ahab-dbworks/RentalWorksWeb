using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CreditStatus
{
    public class CreditStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CreditStatusRecord creditStatus = new CreditStatusRecord();
        public CreditStatusLogic()
        {
            dataRecords.Add(creditStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CreditStatusId { get { return creditStatus.CreditStatusId; } set { creditStatus.CreditStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string CreditStatus { get { return creditStatus.CreditStatus; } set { creditStatus.CreditStatus = value; } }
        public bool? CreateContractAllowed { get { return creditStatus.CreateContractAllowed; } set { creditStatus.CreateContractAllowed = value; } }
        public bool? Inactive { get { return creditStatus.Inactive; } set { creditStatus.Inactive = value; } }
        public string DateStamp { get { return creditStatus.DateStamp; } set { creditStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
