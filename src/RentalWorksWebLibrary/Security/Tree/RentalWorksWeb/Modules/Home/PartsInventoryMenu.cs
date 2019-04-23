using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PartsInventoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventoryMenu() : base("{351B8A09-7778-4F06-A6A2-ED0920A5C360}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{12B9DF1D-FE5A-4E37-909B-64A580FEEBB4}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6541122C-5AC6-4442-A1C5-F00387DEC083}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{DCF0EC36-D093-4B50-B71C-0A24149C8833}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{E63750F1-E6C1-482C-B51C-DCC827F8F7BF}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4DA6DCFC-F00C-4DBC-8086-707FFE7070C1}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{94C0B421-6AEF-4D3F-84FA-15D98FD1ABB8}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{3F13EBD5-DF87-4E2A-92B2-1F49402EE77E}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{CA64B1D3-896C-4535-91B0-DE6ED75E4589}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5685A399-B0EF-4957-9C3F-D5BF555F284A}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{BE3507F3-C0B3-4920-828E-DA417BE3FDB1}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{53EDB2EA-5209-4746-86E2-5DE87E788A03}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{4D921487-E3F5-4DE3-A64B-F05007113784}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{2915215F-7C9C-43D8-936C-DFF413C5D31A}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Create Complete", "{1881D7B6-E17A-4D20-A2E5-71F383FBD8CB}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{7A53C40E-D13E-4A33-8C51-C8D0C486F859}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}