using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.Rank
{
    [FwLogic(Id:"7yGshJaW8LPa5")]
    public class RankLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RankLoader rankLoader = new RankLoader();
        public RankLogic()
        {
            dataLoader = rankLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"f7BbDs2p3TVVR", IsPrimaryKey:true, IsRecordTitle:true, IsReadOnly:true)]
        public string Rank { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
