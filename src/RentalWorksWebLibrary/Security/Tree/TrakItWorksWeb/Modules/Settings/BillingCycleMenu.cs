using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class BillingCycleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingCycleMenu() : base("{01CF64BF-DCCC-4ECA-9262-3BDBD001649E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{4F548431-7639-4669-B8D3-D437274A9A8A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{92448418-9A3D-4120-8AAF-851AB04ED474}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{68D3C34C-A544-4454-97E9-C4B36D60B6D6}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{4D700708-1FCA-4C44-95E5-DE42D86D64BC}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{19407392-15CF-45F8-8527-4A05706E22DB}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{EC881207-FFCC-4258-BC64-149DED277CC9}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F47CFFBA-4475-4070-81D2-BC86452E83F9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{7C628F23-19DC-408E-8784-54CD5FBD4EB3}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CB0C197D-AD4E-45E6-A074-ADBD680F403B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D653EFA0-B97D-45B2-B968-E1488B410298}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{ABD084B2-1FF7-43E6-A850-3EBF408C3DD1}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{8D141F6E-C5BE-402B-B5CD-80097A289E3D}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{558BF963-5887-4E87-9704-97ECDD25897A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
