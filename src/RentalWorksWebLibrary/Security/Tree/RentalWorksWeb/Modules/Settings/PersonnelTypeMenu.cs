using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PersonnelTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PersonnelTypeMenu() : base("{46339c9c-c663-4041-aeb4-a7f85783996f}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{ece3a305-815b-44f6-8f5b-9e1d9c81a2f1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0d905ed3-9cfd-4779-8458-4c07fd24b1e1}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{c5225ed8-ca7a-4261-baeb-9dc5c8562df4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{ae6add26-26d7-477c-a8cf-d4cafadc99cb}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{74acd7a9-804b-4a8c-8c80-b18c1ba51322}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3b7df6ba-b47e-4a29-9d6b-99094e4a810c}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{552d3a39-15e8-473a-abda-1599df90b4d3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5b3a3726-7116-4d52-ab3c-0f2a11268835}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{d79ebceb-7a89-425e-9a8b-ed8c9879335d}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{a214b180-b3b4-4628-a037-a5155cd8432f}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5d6f8d10-67c8-494a-af27-82c52d386024}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5b31c973-38a6-435c-8c62-11fe99692c56}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{27e2d34f-f738-4ed7-87bb-6c54fb5505ff}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}