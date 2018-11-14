using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.Hotfix
{
    [FwLogic(Id:"6FKPULfZZjja")]
    public class HotfixLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        HotfixRecord hotfix = new HotfixRecord();
        HotfixLoader hotfixLoader = new HotfixLoader();
        public HotfixLogic()
        {
            dataRecords.Add(hotfix);
            dataLoader = hotfixLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"Nyl4ibZ8vlMJ", IsPrimaryKey:true)]
        public string HotfixId { get { return hotfix.HotfixId; } set { hotfix.HotfixId = value; } }

        [FwLogicProperty(Id:"QENRdMnp5t9R", IsRecordTitle:true)]
        public string FileName { get { return hotfix.FileName; } set { hotfix.FileName = value; } }

        [FwLogicProperty(Id:"PvpT8gzCIbz2Q")]
        public string Description { get { return hotfix.Description; } set { hotfix.Description = value; } }

        [FwLogicProperty(Id:"T6dynmkzPiOEF")]
        public string HotfixBegin { get { return hotfix.HotfixBegin; } set { hotfix.HotfixBegin = value; } }

        [FwLogicProperty(Id:"6f1WqjwUbsOJ1")]
        public string HotfixEnd { get { return hotfix.HotfixEnd; } set { hotfix.HotfixEnd = value; } }

        [FwLogicProperty(Id:"Dk5xaZLwTErz", IsReadOnly:true)]
        public decimal? HotfixSeconds { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
