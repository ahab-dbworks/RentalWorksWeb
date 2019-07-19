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
            tree.AddViewMenuBarButton("{699EBF83-5B5B-4011-B9BB-F26AACC369E6}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C0ED0D43-3FA4-4B2C-BA16-7F0A54DC1E84}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0A14BDAC-7B11-4FFF-8EEB-0F2DCF7196B6}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8488289C-A2C3-42E9-AC77-F1D25E822FE1}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{769FF27F-1CBD-4BD9-B247-88086CDEA367}", nodeFormMenuBar.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{1AAC6F42-ED48-45FC-981A-73EFE471ABCF}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{FB5A9DA4-90E3-4637-B29B-C20A1C891F22}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Print Order", "{8C34754E-B27F-4FE1-93F3-8D6D84339322}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Void Entire Contract", "{426E75B4-D91E-416F-AEB2-F6B4F8BB5936}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}