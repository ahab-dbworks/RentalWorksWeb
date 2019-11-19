using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.DealSettings.DealStatus
{
    [FwLogic(Id:"RItIXWi2BNLw")]
    public class DealStatusLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"Cs90U27vAnxI", IsPrimaryKey:true)]
        public string DealStatusId { get { return dealStatus.DealStatusId; } set { dealStatus.DealStatusId = value; } }

        [FwLogicProperty(Id:"Cs90U27vAnxI", IsRecordTitle:true)]
        public string DealStatus { get { return dealStatus.DealStatus; } set { dealStatus.DealStatus = value; } }

        [FwLogicProperty(Id:"Fm1tMcyM6HHm")]
        public string StatusType { get { return dealStatus.StatusType; } set { dealStatus.StatusType = value; } }

        [FwLogicProperty(Id:"uPusc19qm3qu")]
        public string CreditStatusId { get { return dealStatus.CreditStatusId; } set { dealStatus.CreditStatusId = value; } }

        [FwLogicProperty(Id:"wVHnhbn3op5M", IsReadOnly:true)]
        public string CreditStatus { get; set; }

        [FwLogicProperty(Id:"2s1FbPxZYoff")]
        public bool? Inactive { get { return dealStatus.Inactive; } set { dealStatus.Inactive = value; } }

        [FwLogicProperty(Id:"xiKGgYZZ8rr8")]
        public string DateStamp { get { return dealStatus.DateStamp; } set { dealStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(DealStatusLogic).GetProperty(nameof(DealStatusLogic.StatusType));
                string[] acceptableValues = { RwConstants.DEAL_STATUS_TYPE_OPEN, RwConstants.DEAL_STATUS_TYPE_CLOSED, RwConstants.DEAL_STATUS_TYPE_HOLD, RwConstants.DEAL_STATUS_TYPE_INACTIVE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
    }

}
