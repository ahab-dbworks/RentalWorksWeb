using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class BillingWorksheetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingWorksheetMenu() : base("{F178308F-ECEF-4810-9EBF-5CA118051061}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{AC801FB7-988F-45B0-8E45-0331E029B84A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6C563803-D8DB-4F0D-A87A-ED64341769C3}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{FD11D79E-33C1-4E45-9DFB-C65256ABCB42}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{87DEFA5F-683B-4410-816E-2C96B8F92A43}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{97B8B11B-63C2-4B8D-A9F6-C2D1AE234B16}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{9351C468-3A1A-417C-B335-1B2BB9A62DF8}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2E84E8F8-2B1F-4F25-B742-5F968BE4E4BA}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{CD9D3657-E739-4B3C-874E-B1FF28841CA0}", nodeBrowseMenuBar.Id);
            tree.AddSubMenuItem("Void", "{123DC979-2CA0-4C02-9BD1-94A16BEC89AF}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Approve", "{259F747F-9B40-4484-9DDA-84B427DBB9D3}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Unapprove", "{7FA7E8A9-A81D-4AE4-AFD3-F92C3F57186E}", nodeBrowseExport.Id);

            // Form
            var nodeForm = tree.AddForm("{43BB3546-1B2C-4851-BE98-29F4E12AC81A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{3C0432A9-FA83-4258-8134-561108977529}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{A4B0DCDB-BB0A-4977-8641-C5A8824D9436}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{F50F0545-F8F9-4968-86D9-B8140B5DEA4E}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{523CCC29-B53B-4FEA-A5F6-58E2913C28A1}", nodeFormMenuBar.Id);
            tree.AddSubMenuItem("Void", "{86057E2F-6F23-4F36-A27E-13995DB22938}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Approve", "{A4451EF0-92BC-4535-AB63-36341F194BF4}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Unapprove", "{86723D8A-E4AA-40EA-8902-09F5F3DA7E5C}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Print Worksheet", "{4A26CE34-2DB6-434E-A83E-E2A3D95C5260}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}