using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.RateUpdateBatch
{
    [FwLogic(Id: "NzVVvRwszmTiL")]
    public class RateUpdateBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateUpdateBatchRecord rateUpdateBatch = new RateUpdateBatchRecord();
        RateUpdateBatchLoader rateUpdateBatchLoader = new RateUpdateBatchLoader();
        public RateUpdateBatchLogic()
        {
            dataRecords.Add(rateUpdateBatch);
            dataLoader = rateUpdateBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "o03Q1viDGs8Xu", IsPrimaryKey: true)]
        public int? RateUpdateBatchId { get { return rateUpdateBatch.RateUpdateBatchId; } set { rateUpdateBatch.RateUpdateBatchId = value; } }
        [FwLogicProperty(Id: "O0OqWaKAToJhJ", IsRecordTitle: true)]
        public string RateUpdateBatch { get { return rateUpdateBatch.RateUpdateBatch; } set { rateUpdateBatch.RateUpdateBatch = value; } }
        [FwLogicProperty(Id: "o0p6xASMrVfUw")]
        public string UsersId { get { return rateUpdateBatch.UsersId; } set { rateUpdateBatch.UsersId = value; } }
        [FwLogicProperty(Id: "o2YFnHFeP4iMz", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "o35Sn2NHX7sLi")]
        public string Applied { get { return rateUpdateBatch.Applied; } set { rateUpdateBatch.Applied = value; } }
        [FwLogicProperty(Id: "o48ak6D0VliHb")]
        public string DateStamp { get { return rateUpdateBatch.DateStamp; } set { rateUpdateBatch.DateStamp = value; } }
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
