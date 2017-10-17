using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class LaborPositionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LaborPositionMenu() : base("{6D3B3D4F-2DD8-436F-8942-8FF68B73F3B6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{35A32B18-C221-43BE-B123-CA33FA2E8D8A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{08F1E5F0-4BA2-48E6-80D9-47FCF00ABFCF}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5D9451E3-9459-42CE-AE01-292FA89C8977}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9C299D20-1A4F-4E6E-8DE1-DC04D6F73742}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{ED7D1471-B464-4CD7-9123-B2F249D94ECF}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3E9A7B24-D3B9-4B13-A03B-3E4F13954A36}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{96DB8EE2-D6EC-46F0-A73A-77174F76F8F3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B4E42C05-550E-4F5D-A47D-A27E95978C2D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{76C7C951-D12B-4AFF-A90C-5953B69CE7E5}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0820578A-01E6-403B-AE64-E3435177159D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{CB6BA7A9-C91C-4C1C-B7F3-1EF4DD2A1188}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{2C2644FB-7C94-4508-80D4-6DB7FD52920A}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4ECBE1E4-F849-4812-8EF6-BCCC6DA61FA1}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
