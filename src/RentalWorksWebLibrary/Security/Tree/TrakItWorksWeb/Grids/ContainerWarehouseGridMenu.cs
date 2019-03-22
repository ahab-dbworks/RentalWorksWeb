using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ContainerWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContainerWarehouseGridMenu() : base("{F9766AB6-E3BC-4F3E-9394-9D28BF8C984B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F960FF60-727C-4599-9CA8-28A109A4CCBD}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{762D0F08-AA7B-47A1-B248-1FBD787B1111}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{41A050C0-D84D-4502-A8EB-B127E0B5EA17}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E2063C27-BE89-4B78-94CA-E6C38AF7C1EA}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{6D66C9E8-8F44-4DBE-8B6F-B317A7798814}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

