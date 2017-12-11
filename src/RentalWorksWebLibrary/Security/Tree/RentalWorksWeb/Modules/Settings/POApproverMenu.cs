using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class POApproverMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POApproverMenu() : base("{237B99DC-252D-4197-AB4A-01E795076447}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{794C862C-B8A6-40D0-88F9-7740D6819DF5}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{A6E6C936-6F2F-4DC1-805D-666B4655938A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3F39F2E4-D84D-4D90-95E8-CC3E3C79386C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{A3786112-73E5-46FB-AE35-53909D0E0B42}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0125A9F0-75E5-4A2A-B8AD-62C30A31E8B1}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{FC5BE5A5-9D82-4AA5-B082-58898E759571}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B3104D3C-59DB-42C9-8AED-843DDEDBEA09}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3F40BD1C-E432-459F-BF60-BAA5D6E88B3A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0AAFAB56-B9AB-453B-89ED-8546300CBB88}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{29C6156C-4037-4766-B01D-35600AB361E8}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{ADC7F73D-A779-4709-B810-0598E197E687}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{50FA4FB2-B5AC-4763-8C66-0E5922017DAB}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{808401C5-7997-4C1B-A402-684D67C49315}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}