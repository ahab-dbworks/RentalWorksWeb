using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.Hotfix
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string HotfixId { get { return hotfix.HotfixId; } set { hotfix.HotfixId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string FileName { get { return hotfix.FileName; } set { hotfix.FileName = value; } }
        public string Description { get { return hotfix.Description; } set { hotfix.Description = value; } }
        public string HotfixBegin { get { return hotfix.HotfixBegin; } set { hotfix.HotfixBegin = value; } }
        public string HotfixEnd { get { return hotfix.HotfixEnd; } set { hotfix.HotfixEnd = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? HotfixSeconds { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
