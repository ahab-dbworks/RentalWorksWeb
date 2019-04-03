using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
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
            tree.AddNewMenuBarButton("{D0A9A37D-52FF-4E8C-AF00-3E758BC05058}",    nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{5EEADB16-5219-4E8D-8E2B-44FA40847306}",   nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{988276FA-B491-46B9-A846-E4346915E029}",   nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A0BF65BB-6AFF-466A-8149-A1C17BF32E3A}", nodeBrowseMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}