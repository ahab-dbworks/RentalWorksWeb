using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class UserMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UserMenu() : base("{79E93B21-8638-483C-B377-3F4D561F1243}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D1BB2FE1-030A-4FBF-BDCE-D0AB83E931BF}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{0BA215A4-F02D-4CA4-B89E-5751BD034037}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{A9DD5905-341D-465B-A50D-855BCF4A3B14}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5D821CB2-B176-4B7E-8ADC-3A91C5E48542}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{0E43059D-2655-4D12-B237-9A97A7DADF6E}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{19668B21-C62E-4593-9930-0885CCA4F1C9}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{B96C8846-F395-4146-8277-90100600FDEF}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{24EAD2F8-7CC1-4B9C-9F06-D6840AB173DC}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{AFEC3AB4-BA03-4D88-ABFB-BDF00A8DDDF9}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{510D1D65-75B6-4B27-85A9-A2D968370935}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{4348D17A-9DCD-42B1-A033-567BE0BA39CA}", nodeForm.Id);
                    //tree.AddSubMenu("{AC96612C-C81D-4BFB-8E84-2FA3086C2B4A}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{9283E554-E63D-488C-964D-77BEDBAC9B4A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}