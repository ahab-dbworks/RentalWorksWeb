using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class DealStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealStatusRecord dealStatus = new DealStatusRecord();
        public DealStatusLogic()
        {
            dataRecords.Add(dealStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealStatusId { get { return dealStatus.DealStatusId; } set { dealStatus.DealStatusId = value; } }
        [FwBusinessLogicField(isTitle: true)]
        public string DealStatus { get { return dealStatus.DealStatus; } set { dealStatus.DealStatus = value; } }
        public string StatusType { get { return dealStatus.StatusType; } set { dealStatus.StatusType = value; } }
        public string CreditStatusId { get { return dealStatus.CreditStatusId; } set { dealStatus.CreditStatusId = value; } }
        public bool Inactive { get { return dealStatus.Inactive; } set { dealStatus.Inactive = value; } }
        public string DateStamp { get { return dealStatus.DateStamp; } set { dealStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
