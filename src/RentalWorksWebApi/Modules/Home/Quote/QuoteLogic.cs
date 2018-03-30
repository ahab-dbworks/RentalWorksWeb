using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Modules.Home.Order;
using System;
using WebLibrary;

namespace WebApi.Modules.Home.Quote
{
    public class QuoteLogic : OrderBaseLogic
    {
        QuoteLoader quoteLoader = new QuoteLoader();
        QuoteBrowseLoader quoteBrowseLoader = new QuoteBrowseLoader();
        //------------------------------------------------------------------------------------
        public QuoteLogic()
        {
            dataLoader = quoteLoader;
            browseLoader = quoteBrowseLoader;
            Type = RwConstants.ORDER_TYPE_QUOTE;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string QuoteId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string QuoteNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        public string QuoteDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
        public int VersionNumber { get { return dealOrder.VersionNumber; } set { dealOrder.VersionNumber = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                StatusDate = DateTime.Now.ToString("M/d/yyyy");
                QuoteDate = DateTime.Now.ToString("M/d/yyyy");
                if ((DealId == null) || (DealId.Equals(string.Empty)))
                {
                    Status = RwConstants.QUOTE_STATUS_PROSPECT;
                }
                else
                {
                    Status = RwConstants.QUOTE_STATUS_ACTIVE;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
