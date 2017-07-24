using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class CreditStatusLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isTitle: true)]
        public string CreditStatus { get { return creditStatus.CreditStatus; } set { creditStatus.CreditStatus = value; } }
        public bool CreateContractAllowed { get { return creditStatus.CreateContractAllowed; } set { creditStatus.CreateContractAllowed = value; } }
        public bool Inactive { get { return creditStatus.Inactive; } set { creditStatus.Inactive = value; } }
        public string DateStamp { get { return creditStatus.DateStamp; } set { creditStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
