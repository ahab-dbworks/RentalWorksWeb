using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class UnitMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UnitMenu() : base("{7D1964B8-E6BD-4DF8-8A15-A811474B15D6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8CD443D5-977E-4944-B08C-88210643F446}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{28981C60-51AB-4A77-9FF5-762846116972}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5745614E-DA39-4465-A2BF-9C0FE5AB8A3C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{E2D2D83A-193E-47E0-9151-36DC1CF1FE5B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9A15DB17-870F-411A-AAAF-B7A7894F49FA}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{D1E8047B-7903-4609-AE80-1019DA29F60A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{7F7E88E5-7374-4F6A-9A02-FA4D7C446487}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C76B5948-EAC8-4B0B-B4CF-E14C0FA1C42B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{78CB3547-1CA1-4723-B083-7867F0B3F9A3}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{E0F2E96E-E724-4F13-9E5B-43EE14F62679}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4BF10438-EEC5-48E7-9E5B-E377F7A93E48}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{FEDD63F1-8DCC-48A2-BF91-2FEAE1F54A3F}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{469F59E1-0E59-4B25-B710-B338FC2E5687}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
