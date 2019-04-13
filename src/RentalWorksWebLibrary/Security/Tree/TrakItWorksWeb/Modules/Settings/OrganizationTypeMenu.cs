using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class OrganizationTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrganizationTypeMenu() : base("{11290C88-D1C8-4FAA-A660-0C4A53200CBD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{45A817BC-335A-4917-B7DD-EDEE3A27C137}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{02F4B8D0-A61F-411F-8A49-03A52854DB4A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0BF8AB84-88BA-4700-A1ED-821F3E3773AD}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9F96AB84-123F-4CB9-8F28-241CB804BE55}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F89DC69B-7308-4906-B1DD-5A19E1EACE7C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B5D3EFB6-4233-403B-B4DE-E869CDB51E65}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{105B8277-4399-4BF5-B931-8A9C25DB0657}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{36A1628B-0C83-4DC6-B8F9-22FB968ED1D1}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{31A1AEC7-D7F4-4934-90D5-2C41F9265549}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{BA2D873C-1A4D-4E6C-B9CA-C7EE3875A583}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{87592867-991E-4147-8E71-BB5C385076CA}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{B4FFD8B0-267E-4885-9AD3-4E8C3CCFE4DA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{52214C3A-40C3-4A41-8A2B-B30BCBAC60E9}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
