using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VendorInvoiceMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoiceMenu() : base("{854B3C59-7040-47C4-A8A3-8A336FC970FE}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{70B9029E-D840-4630-B370-BC38071AF720}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{A11897BB-92B1-4264-853C-C17FC3B6D0DF}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3BAE6242-4484-4473-83C8-5E4D89ABE2E7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{ABFDCAF4-CEBE-4280-8CC2-E815C598F2BA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A29B8F60-3577-443F-832A-33D7D95DCB36}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{47F0F323-EC4B-409C-87B8-D7042541227F}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0F5DC392-5895-4E19-A6FF-3E194FF4CAAC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5230AAE3-E2DF-4DFC-9DA9-FCFFCB13CA8A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{12021AED-2868-4ADD-B5A1-6194581A058B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{23AA845C-B254-488E-93BD-E710681186C2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{EFA3370F-7B1A-4555-82A7-030B7C9CDEE1}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{79BB5C05-6E28-420D-AB1A-7EC9275D6017}", nodeFormMenuBar.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{56AF631A-2969-48FE-AEA8-53574D37FD58}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{E5E8C2A7-8CD1-438A-B9ED-EEA4B336E9D2}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Approve", "{79ABAD41-19F1-42C1-A88B-41479DE13B3B}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Unapprove", "{FB248072-C14C-4EEC-8B99-5ED8E950CE8A}", nodeFormOptions.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}