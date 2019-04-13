using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class DealStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealStatusMenu() : base("{2B66A033-1E35-44B7-9DDE-8A13DD26AE04}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B2AE1334-5C5F-4855-8384-5FFF7D044B0A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{BC2DBF48-3B1B-4E37-8BEB-00DA61F24063}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{FED7FB38-0843-4A62-80B2-D197E9B3A577}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5485E37A-9D54-4B97-806A-5DBAC3E09BF9}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{489F5D3B-0C40-45F5-8C2B-080DDDF6A498}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{BD8D009A-D39F-48AB-A549-D335B2065602}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{A2918ACA-E4EE-48DD-8F60-D2235196FEC7}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{AE195BE4-D2EC-41B8-82D3-327CEBCA1959}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7AD0070E-EC23-4CF0-9386-7E39D07C21C4}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{6CA94FC4-463E-4FDD-A94C-A91174F57706}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E262997F-4219-4770-8935-BE72D4F42877}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{2DFAB556-E31B-426E-A263-DF904E88B4EF}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A1F23556-48FD-430E-8F94-E61CC191892D}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
