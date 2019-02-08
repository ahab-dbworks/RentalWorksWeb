using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class QuikEntrySubCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuikEntrySubCategoryMenu() : base("{26576DCB-4141-477A-9A3D-4F76D862C581}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{900DE4F0-4A6B-433B-93B5-325C1BC260F0}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{1378F0B4-8DB0-4ED3-AC95-097236BC7C2A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{7E40A9D2-7769-467F-A2F7-FB187828A53D}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{699BFF57-3874-4E24-95DC-FA008B7229E2}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{9D1CD25E-DD10-4E86-A564-128E1012BB82}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{82C72249-8A95-456D-B238-1F048AE0BA76}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{3A05563E-9C1F-4C6C-93D2-FA700759F89E}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C6644585-12D4-4F69-9C45-FC1BB940DACB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}