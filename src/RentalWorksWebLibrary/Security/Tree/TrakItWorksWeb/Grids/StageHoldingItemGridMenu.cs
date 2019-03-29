using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class StageHoldingItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StageHoldingItemGridMenu() : base("{1F06BAB4-5D64-43FC-B2A8-FF088064E4A0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{592457BF-D96E-4CBB-A1D5-273DA2ACD31E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{4F92A0A9-CA6C-4CDD-8A3C-984A0538CD3D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9D47BC08-77D8-440C-87DF-52FC6E4F1184}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0B966576-901A-43F8-8AC8-E758A9B6A92E}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


