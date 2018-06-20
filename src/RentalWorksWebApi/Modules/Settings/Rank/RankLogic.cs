using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.Rank
{
    public class RankLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RankLoader rankLoader = new RankLoader();
        public RankLogic()
        {
            dataLoader = rankLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true, isPrimaryKey: true, isRecordTitle: true)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}