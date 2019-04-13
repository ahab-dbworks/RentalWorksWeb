using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class ShipViaMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ShipViaMenu() : base("{86FB4938-CD09-4898-BCEC-5E209879201A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{10460B8A-101A-47B5-ACAC-143E23946BC4}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E6120C6B-C883-4BB8-95E1-AA59B28A07B5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2D1A7DDB-3075-4F38-9BD4-5302AA98BE20}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{46FBC2EC-8677-43B7-9D63-3E04594F464E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8342450F-0937-477D-8A19-C0218A5BB89D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0F83A1EA-B611-4316-ADE8-DE8F6559CB69}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{63687963-6AF3-4E55-9615-093D63665C70}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3A2CBF7F-FEA5-493D-BFD1-082AB82F499E}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{04314D15-E21F-4295-9390-12A17C5741BA}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{FEAE204C-4F9D-4B90-87D3-2B597CB8B72E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6EEF7CFA-6806-4CA0-BCDD-CC0CD0FDD728}", nodeForm.Id);           
            tree.AddSaveMenuBarButton("{9257C3D8-9BA3-475E-8567-3228B7894E01}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
