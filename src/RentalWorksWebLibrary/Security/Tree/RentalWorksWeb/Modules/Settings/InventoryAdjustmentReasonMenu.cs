using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryAdjustmentReasonMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAdjustmentReasonMenu() : base("{B3156707-4D41-481C-A66E-8951E5233CDA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{965F5D14-57C9-4FE8-A2A5-26766D66A4F3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E5279A16-29A5-463A-BB54-E28C41CF8FDA}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5CABEFB2-4F66-4353-B599-008935E5B3DC}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9CC43816-33F0-40C5-B6B5-E642C23E9834}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6DDFEED3-F5AD-46EC-AE51-5CE45F2DCDBD}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{94D456CB-ECEB-4CB6-A3F9-A1A6CDBE2B39}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D7F09469-052F-4CDD-8A7E-58D804F6A989}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{DC4E4DB6-C246-4A79-AD05-B0EEE9ECD817}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5D618002-A5DF-4338-A730-8633D30E9928}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7BAECA3F-8247-4783-B4F7-6A24A30A728C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D8313775-877E-4A98-91F6-BD62FFCF5299}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6DD5C8FC-FAD7-44DB-BFBE-A72906D7BC29}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{18A9E32B-FA22-467E-A988-D09BFD68BBE3}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}