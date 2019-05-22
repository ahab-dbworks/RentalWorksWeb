using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class DealCreditMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealCreditMenu() : base("{3DD1BA32-0213-472E-ADA8-E54D531464CC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{ACF2A387-303D-4633-93AB-F7A5158393F1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{5F8EFDD0-6B8E-4FEE-87AA-0B044DC8ABA5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2DB305BC-8212-45F0-A8CA-BB38DEFA23ED}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{4B5A17CC-0601-4A1D-AF1B-B65F7F54B6AA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D4A81F48-F486-45F2-8C96-11DF046FD1E6}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{16113D75-B757-45A4-8A3B-52557348E295}",   nodeBrowseMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}