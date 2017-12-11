using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class OrderLocationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderLocationMenu() : base("{CF58D8C9-95EE-4617-97B9-CAFE200719CC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{848B61EE-6E44-4EF3-8731-D9629559D926}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{120E9A08-BF3B-4193-BFBB-130ACE391A24}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0726FDB5-74F2-440A-B5EB-F28A28199EEF}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{301F4C19-1896-4A65-8582-1E024B445AC6}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AE924236-E830-40C4-A0FA-1FA8E30C6EF0}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2C82C50A-6D1E-429E-A181-1968B8FD39C0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BE83C20D-8812-4A0D-B9A2-399A7BDC8441}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C2905D75-801B-4283-989C-87C7383B29AD}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{826E2AF4-F1C2-456C-A0B9-6DB5E014F87E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{08C3A281-1F5C-44F9-B503-35E1F0E4DC71}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DD08D18B-65D3-4E07-B8F6-FE7CA1B49E61}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8A17E91D-7F36-4046-BFE6-50E73B6F78F5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}