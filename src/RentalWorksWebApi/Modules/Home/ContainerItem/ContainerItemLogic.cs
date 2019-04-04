using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Home.Item;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.ContainerItem
{
    public class ContainerItemLogic : ItemLogic
    {
        //------------------------------------------------------------------------------------ 
        ContainerItemBrowseLoader itemBrowseLoader = new ContainerItemBrowseLoader();
        public ContainerItemLogic()
        {
            browseLoader = itemBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
    }
}
