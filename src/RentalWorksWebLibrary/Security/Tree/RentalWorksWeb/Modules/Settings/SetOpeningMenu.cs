using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SetOpeningMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SetOpeningMenu() : base("{15E52CA3-475D-4BDA-B940-525E5EEAF8CD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{EF851F9A-E11F-4982-8899-BAACC3D1055E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{64446A70-5498-4BF0-BEF6-D80B1FE852A8}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{35B9B758-8B2A-459D-9DC9-C8E7B8A1FA40}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0262A33D-9166-418B-88B1-F929E226E229}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2156EAAB-4AD7-43F0-A42A-15D921544A21}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CF431DD0-312C-48AB-8380-1D13C28C03EE}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{20FB3F61-D138-4891-83D2-AEC60660AF90}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{F1640CDE-46C3-428D-A4D2-72557A46B772}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4C3D8C60-E354-4670-A8CC-90EFE14E4F0E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{13C609B1-54E7-4B97-AFCF-4AAF01E78DCD}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{826D06FD-D136-4873-B559-CF1B3A5DE096}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{34362961-5FCF-45A6-854E-3655AA51192F}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{FCF3BF5C-AC5C-4A50-B215-80FED707BB4E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}