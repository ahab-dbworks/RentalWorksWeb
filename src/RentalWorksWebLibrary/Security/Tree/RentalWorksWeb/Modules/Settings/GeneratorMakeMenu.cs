using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class GeneratorMakeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorMakeMenu() : base("{D7C38A54-A198-4304-8EC2-CE8038D3BE9C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{41AA16BC-F30A-46F2-B434-353ECDBBA08F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{C84AD567-279D-45B1-A5B8-CE56DE16A29C}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{9848CA9B-2260-4601-9336-E188F872E070}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{1D732FF0-823C-4838-B8C1-E67CF51851EE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{EDAAC330-F169-4902-A2E6-B1F81508F8A7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2CE24569-FB28-444A-BD37-7392D3CE79A2}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{EC369585-145A-4E07-80A7-A97573F24B98}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D242EB26-AE8B-432B-9704-2301CC614132}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8EC6A797-783F-42B0-BF2E-8CC351BA2265}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{48A330F7-AA2C-46F5-AB83-9D76419A7CB9}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{28D0D63F-B07C-41E8-9B17-7F71AAAB9936}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C34EEF14-058A-4269-8AF3-A813DCEF90B7}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{0FA5F3E3-67A8-4A43-AC9C-14C266903AF9}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}