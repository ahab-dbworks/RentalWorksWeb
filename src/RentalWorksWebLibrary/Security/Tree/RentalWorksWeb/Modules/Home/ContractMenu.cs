using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ContractMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractMenu() : base("{6BBB8A0A-53FA-4E1D-89B3-8B184B233DEA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B02EFA48-43CC-4023-AECA-44798BD668C2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B36896C3-9108-499A-9673-4DCBE14A59CC}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2EE8C212-CE8A-4F40-9BD6-AEBE1E17012A}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9FE16021-E197-4F89-BDC6-9D914503B2F5}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2234C245-9BD3-4891-9CFD-8400D6242012}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{F110758E-8758-4251-A537-E129B0756330}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{699EBF83-5B5B-4011-B9BB-F26AACC369E6}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C0ED0D43-3FA4-4B2C-BA16-7F0A54DC1E84}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FF649B28-CC4D-43BB-988C-E85B5574E673}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0A14BDAC-7B11-4FFF-8EEB-0F2DCF7196B6}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8488289C-A2C3-42E9-AC77-F1D25E822FE1}", nodeForm.Id);
            //tree.AddSubMenu("{9A19F6D7-2B30-4513-B80C-5FD70F074DAA}",           nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{769FF27F-1CBD-4BD9-B247-88086CDEA367}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}