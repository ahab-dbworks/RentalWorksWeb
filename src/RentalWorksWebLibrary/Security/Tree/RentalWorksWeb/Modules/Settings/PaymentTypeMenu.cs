using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PaymentTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PaymentTypeMenu() : base("{E88C4957-3A3E-4258-8677-EB6FB61F9BA3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{4830C93A-10D9-42AE-8855-C19B6BB8F97B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{FF793776-5828-45E5-8F12-8A180FB25A17}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{40978C09-3CEA-4611-84E1-263721E8813C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B862E39E-0BA8-409F-9A6B-2F3425E22576}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B7B34578-46AE-4F5F-904D-E57256A7EAAA}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{4575DA08-D676-43B0-9D52-F067E97FB431}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{1C1F6169-561B-42CB-865F-D335EDC2BF93}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{61C308FE-0087-40FC-97D3-F8466122786D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E024FD56-3EA2-4F50-B380-AD1A03233DBA}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{778503AD-4C30-44BB-A7DD-FB0F0AA444A0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1F5D3213-B13B-497E-8B1E-5225E4C73DDF}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{420FF344-5E1B-4912-BE94-3AF71EA8921D}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{5693CC88-B8F9-4952-8FC1-76A407B59D39}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}