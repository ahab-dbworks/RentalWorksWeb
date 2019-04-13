using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class GeneratorWattsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorWattsMenu() : base("{503349D6-711A-4F45-8891-4B3203008441}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F336A133-2049-4930-942F-C3F7FE0E459B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{413798B0-D788-4606-AF43-6CAA1F0D4692}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{8F78E4FA-C24E-49A9-88D4-5E164367A168}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{86B2703A-C497-404F-851F-2C60DB78978D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4385625C-ED46-44FB-9170-A74F6573E420}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5C0A704E-EA4D-468C-9319-8B0B6071FE70}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{585120E9-8A5A-4170-91DB-974F267C16A2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{18F92CD5-FAD9-4170-9CE7-D02BE63A1891}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{73E179CB-4831-4E31-A6EE-4F008C657DFE}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{2A09E868-8148-4A1A-B255-3840F25757CE}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{FC8DF834-FEB0-4C2D-B696-1104566D98CE}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6F22C427-013C-4A23-AB03-9A72CE573AD6}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{92B090D3-186D-42D4-B555-BED54ACBEF03}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}