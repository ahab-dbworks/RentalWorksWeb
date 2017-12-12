﻿using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DealClassification
{
    public class DealClassificationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealClassificationRecord dealClassification = new DealClassificationRecord();
        public DealClassificationLogic()
        {
            dataRecords.Add(dealClassification);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealClassificationId { get { return dealClassification.DealClassificationId; } set { dealClassification.DealClassificationId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string DealClassification { get { return dealClassification.DealClassification; } set { dealClassification.DealClassification = value; } }
        public bool? Inactive { get { return dealClassification.Inactive; } set { dealClassification.Inactive = value; } }
        public string DateStamp { get { return dealClassification.DateStamp; } set { dealClassification.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
