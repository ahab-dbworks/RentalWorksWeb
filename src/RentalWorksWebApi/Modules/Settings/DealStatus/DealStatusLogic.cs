using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.DealStatus
{
    public class DealStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealStatusRecord dealStatus = new DealStatusRecord();
        DealStatusLoader dealStatusLoader = new DealStatusLoader();

        public DealStatusLogic()
        {
            dataRecords.Add(dealStatus);
            dataLoader = dealStatusLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealStatusId { get { return dealStatus.DealStatusId; } set { dealStatus.DealStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string DealStatus { get { return dealStatus.DealStatus; } set { dealStatus.DealStatus = value; } }
        public string StatusType { get { return dealStatus.StatusType; } set { dealStatus.StatusType = value; } }
        public string CreditStatusId { get { return dealStatus.CreditStatusId; } set { dealStatus.CreditStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CreditStatus { get; set; }
        public bool? Inactive { get { return dealStatus.Inactive; } set { dealStatus.Inactive = value; } }
        public string DateStamp { get { return dealStatus.DateStamp; } set { dealStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
