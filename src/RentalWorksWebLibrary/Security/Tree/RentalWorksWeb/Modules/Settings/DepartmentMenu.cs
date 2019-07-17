using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DepartmentMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DepartmentMenu() : base("{A6CC5F50-F9DE-4158-B627-6FDC1044C22A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F12CAE72-53BA-4468-8EE8-3BD59F78D7D6}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F48A07A2-6EFC-4C2A-8393-7A4EA425D972}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C4D0C672-62BA-46B4-8014-4C618AC9D951}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{292293B1-5751-4033-8900-AE4A29EBCCDC}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AD52F53D-9048-47BE-A2CE-EE1FD2952EBD}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{998B0D67-EBD4-4F7C-BD23-B1A8C1BCCF3E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{73078E69-17D8-4839-80FD-2E532EB673E9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{8F288862-D8CE-42B0-B3A2-47DBA4094081}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C60CE0BB-7A1B-40B2-8126-D16395200C75}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{5466A966-B30A-464C-8E89-AFFB9A6199B5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{043A695D-1F7E-4F14-B23F-7EB1AE2A3F61}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{1AB618A4-CA87-4BD1-AB91-B4D06A53A470}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{DD451880-281A-4E89-BC7A-CAEE3AD82C79}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}