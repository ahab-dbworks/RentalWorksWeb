using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class CustomFieldsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomFieldsMenu() : base("{C98C4CB4-2036-4D70-BC29-8F5A2874B178}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{33B7B343-F313-4962-B6EE-9DBAEC083634}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7481C5C2-49D8-4FEF-A85F-0CD4C3E06766}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{625634E0-DB31-42D7-8B32-9D15C29CA306}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9FDBC130-88B0-4480-B8A5-CB54B81C3FE8}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{770071A6-AB87-4964-9FC1-A7A75489551A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{79F1C3A7-10B9-4B70-B7C1-271F10A253C3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{09A3D918-8A20-4D9D-BCCD-F9F740124429}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{22A26912-F9E3-4F49-90B0-054796967ECE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C9B8416E-A493-4440-9C77-301F8356DC26}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{84F63908-DD80-43CE-9953-32F44C0F9552}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{35FD4882-B2A0-4F48-9D8B-E7AC51456EBA}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{62932FC2-716D-42FD-BDCC-9947F583FEC3}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{48243A40-DF02-4E45-9ED4-3D26769C7397}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}