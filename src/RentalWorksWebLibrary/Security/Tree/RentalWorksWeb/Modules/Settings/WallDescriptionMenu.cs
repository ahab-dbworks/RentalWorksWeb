using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WallDescriptionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WallDescriptionMenu() : base("{F34F1A9B-53C6-447C-B52B-7FF5BAE38AB5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{E334173C-B93E-4674-A6DE-D9EEC80403A9}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0C7DABE5-26CF-4E12-A747-DA0ED62116A3}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{A905048A-951E-4D26-80A1-8789B97E9B12}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{CDFA3837-1842-4252-A2B9-975255C93C74}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2A0680DD-1974-4AE5-91B5-D26F910C481B}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{F61904AF-8539-4B23-90E5-C2EFB1673AA1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{66958329-93E2-4A08-BAE5-0463D847B21A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{00B9C646-94E5-452E-B999-3C4AF388D17E}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9294FE56-D637-478F-AD06-EDFDFB2C224E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{33A2B0FC-9D2C-41C1-95BD-3CC3E399667D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DCFFF570-4523-4DC9-85D9-0914BA8A547E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{A61661D9-78DA-468F-B2C5-78B92C2106BB}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A25DD99E-0AB1-4D1D-897B-A8A59BEFED29}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}