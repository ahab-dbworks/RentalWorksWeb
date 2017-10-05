using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class LaborRateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LaborRateMenu() : base("{650305EC-0A53-490B-A8FB-E1AF636DA89B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8108EFDF-C392-4B0A-AB05-BBC6CAFC5166}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7057B233-473C-428E-870F-78DFCD30271A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{41068ACF-7D4E-46E9-BAFB-DC021A3A852F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{55A39F45-DA69-4261-845E-351B32E8278A}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3ABCF9F3-4534-4728-9551-78FC60206595}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5C8BACEF-94D3-41CE-90C1-71C49219B428}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{93692D08-B0D6-4EE7-9001-DCF1DBA44E56}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{DBE8E589-34BD-4EB2-AFDC-15AA73153675}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{739CB020-3F68-4CB8-8573-9DE83F1D30EB}", nodeBrowseMenuBar.Id);
 
            // Form
            var nodeForm = tree.AddForm("{D79703BF-1602-453B-959A-60420CE5256F}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{11E3AE68-95FE-446D-A5A0-90CECE993392}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{73AEAA17-D6FA-4273-BEDB-7464C432A590}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{65C4EC52-9070-4275-9228-8B24BB0D089E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
