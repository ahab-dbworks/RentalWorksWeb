using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SetSurfaceMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SetSurfaceMenu() : base("{EC55E743-0CB1-4A74-9D10-6C4C6045AAAB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3E8C38B6-07BB-4E24-AF1F-26E60F7F81BD}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{5BCA0958-53E8-4E6D-86F5-9F00ADFA57F1}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{567BC2A2-58DE-4FC6-AA3E-E1A7F55DF2BF}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F30B624B-4CC8-44FB-862F-6CC25D880637}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{03CB5513-A7D7-4755-9352-FBE2D6B451C3}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{D9403122-EB12-468A-9D44-D17E3F31EAFD}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{E6DA4FCE-BBBA-4299-B23E-E9C0A9796E58}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EB659ED9-C766-4A1E-BFA2-12271314D452}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{675B21AB-64B8-48F2-B999-6EA92E7AEC41}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{5C7D919C-297B-4487-997C-B5DD72B57A01}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{43BC68E8-369D-4D36-AD41-E84A5CCEFCF6}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{47A38795-AD9B-4FA6-9399-A2F9C645C878}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{79F8FE03-FB3A-4DD7-BF4E-72D88620AE34}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}