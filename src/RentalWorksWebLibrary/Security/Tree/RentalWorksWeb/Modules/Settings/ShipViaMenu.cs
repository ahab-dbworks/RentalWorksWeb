using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ShipViaMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ShipViaMenu() : base("{F9E01296-D240-4E16-B267-898787B29509}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{DE09DC7E-769E-4CCA-828C-FE567F4778EF}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{44E4A52F-7179-47B8-A67C-4C83E0529242}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2C9302ED-94A2-4D90-BD60-A4C873A421C4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{BE547F3F-1B9B-430B-ADA5-49DA7B0C4E19}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A5291C8C-A464-44F9-8A22-2B1DDAAE1E41}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{F39430C3-828B-4176-9440-1A8EC611D56D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F30807DA-480A-45EC-BFA9-5D181BCDF7A9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A5F38455-74CC-4F1C-B82C-0E8A91563EEF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FD03D541-FA23-4115-97DC-63A8948E9143}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{00ECB161-9C4B-4203-B550-BDCEE51E1917}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D86D7FC7-C9BA-4199-86D2-8752D7D6AA7B}", nodeForm.Id);           
            tree.AddSaveMenuBarButton("{A3F259E4-2F22-45EA-AF72-8A7C405D75DD}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}