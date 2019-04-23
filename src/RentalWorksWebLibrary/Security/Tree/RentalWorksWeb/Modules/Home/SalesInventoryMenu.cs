using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SalesInventoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventoryMenu() : base("{B0CF2E66-CDF8-4E58-8006-49CA68AE38C2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3D1286D9-8E6F-48C5-99AB-1F75E2DBD79E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{11229871-EA85-4099-8D2B-4FC9B5BFC6EB}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F63E849D-0690-45C0-B00F-690DB1C7E69D}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{CCB3B2D6-F59A-4A15-BB14-1035D1A12E37}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B3C312AE-DDC3-4AFE-BD6A-FFDEF68C9886}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{164B12D4-B194-4BF2-865F-AA039016C6B5}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F1BED0FB-41F7-44E1-B2C4-ECC90DEA9A73}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EE3D7EE9-733A-460E-AB2F-8D77B71152DF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A4BC3A56-99B5-4D98-A462-BE03951F8B4D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D7857C1E-C9ED-42B5-B4B8-04F02B8DDAC5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{82EC9BFF-A7B2-4BA8-8A9D-CB1495FC9FE1}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{18CD8528-1DDB-4606-B7EE-83D78DE2577A}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{A261A07F-0BA3-48E3-94A7-EDAD5CF11698}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Create Complete", "{B13C0180-D25C-4AFC-9B2C-556C7B0FA53F}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{3972A6F9-685A-4CDE-97F1-CF68E9558731}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}