using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POReceiveItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReceiveItemGridMenu() : base("{EF042B8D-23B8-4253-A6E8-11603E800629}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{462FA992-2286-4496-8537-4A5DD79F2520}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{7681EBFB-D2F6-4D4C-B2F9-1CE5E7076821}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2AAADA77-F374-4393-97D6-BA560EB0447A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{74DDA6E6-8A63-4CB2-9E8A-EDB81A96FEBD}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}