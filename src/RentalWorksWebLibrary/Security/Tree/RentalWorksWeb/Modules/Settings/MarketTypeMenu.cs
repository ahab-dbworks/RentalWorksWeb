using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class MarketTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MarketTypeMenu() : base("{77D7FD11-EBD2-40A2-A40D-C82D32528A01}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F02F3120-B4D4-4280-BD01-C87B634EF77A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{BE529F3E-5A6F-4F23-BA40-E334EC8CE623}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{7630E014-A789-4AA8-9E6B-AF27EF3F3EC2}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{61BB4EB1-A438-43EA-B50E-85A7BCCB1BDD}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{576EEFB4-49F0-48C8-8A43-ECE52DFC0E41}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0F2817FE-F3D4-433E-A284-DB61DA745BF0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BC1EB19A-0CBE-41D3-A7F6-800A4AECA9D0}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D1429FAF-9562-4C44-87EF-B59616C6A30E}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{390E2ABA-91AC-4C6F-A9F5-C188D4846F01}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{C629D1C5-8E81-4489-81B1-BFF384704379}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{FEDC873D-BC50-464B-AE31-E02BF2CF97A2}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{824FABC9-5085-44E3-8A12-2A9B19342929}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{51136FA6-D872-4102-AF6C-40846B6E0EA4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}




