using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VendorClassMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorClassMenu() : base("{8B2C9EE3-AE87-483F-A651-8BA633E6C439}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F1CDAAA7-60AB-4668-BC67-AC865EA86E90}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{AD152734-98C8-4ADC-BCAC-B3DCFE1C22D1}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6F3AE2AC-B8A4-440C-BF9F-A8CC5C0BFB98}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{BE62192F-9CB5-4F96-80A3-F2B2F4A5EF43}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AF57DA50-2CFD-46EC-981F-811F119E2888}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{287D900A-67AD-44B3-A529-CB4C16F4BAE9}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{1F4CC870-5146-41F0-9421-70659F226BD9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{95F570D5-75F7-4AFA-B5E6-C186FC69E184}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7F495ECD-E0E0-4719-8F44-21DB10356BB5}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{1F478C2C-C6F7-43E6-B84D-CD2C23C70263}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C9B42BFF-DAA8-4359-96BB-AAC8FE4CDD08}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{EFCAF0F5-4688-43A8-9701-33163C6FE270}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9919AF08-6252-4898-B186-D32CE8BDB985}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

