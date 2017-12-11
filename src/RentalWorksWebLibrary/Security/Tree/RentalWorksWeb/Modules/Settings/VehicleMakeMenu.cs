using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VehicleMakeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleMakeMenu() : base("{299DECA3-B427-49ED-B6AC-2E11F6AA1C4D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C5837262-FA73-4CBB-8512-1B4FAE61116C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{C5BA4858-8785-4075-ABB3-ACF13DB54C4E}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{DDB8BEEE-2339-4947-B871-B962D41EE08F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{29541D5B-7E09-4FE6-8D6E-F3BCEA692473}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{017FAF5D-D0BD-4278-9A90-5F338063DA1A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{C949AC1F-D456-486A-A4CA-2D1875453BC1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D37CC370-7B0B-4CBB-BF5D-DEF0D83C128E}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B497D32A-11DB-4709-9FA3-725F12267A3D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1CE1D55F-C6DE-4959-A588-AAB16ACDC293}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{9C59E7B7-7014-4F0E-8644-0FF44C8C2095}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C599BA0D-A0E0-4D80-9E45-9CA64EA415A7}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6FD17EC5-6298-4F98-8A8D-0CA86CC34C40}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{1EC134FE-6D6A-4FA2-8ECB-9382972F29EF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}