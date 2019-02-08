using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DiscountItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountItemGridMenu() : base("{2EB32722-33D0-43C4-B799-ECD81EDF9C99}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FB0FE8C4-EB7F-48A5-A035-D9E92AC55F3C}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{329AD64E-E1AB-4C13-A7FE-DA4D41D8285E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E52B814D-B68A-4CA7-B51F-53B3DB6855F6}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3FE72AFC-3F13-4E60-8D28-416FAA5EE602}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{8AC0C78D-7988-4832-A05D-A4011A98A193}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}