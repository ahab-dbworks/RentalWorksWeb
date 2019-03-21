using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ContactNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactNoteGridMenu() : base("{A6BEBACB-24AB-4A5A-9F65-7EF11BF49691}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C9D8F7A5-45E9-4271-A04B-FFE4BBB417DB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{167A5818-B68F-4DD3-8EF8-F0084128E5A4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{AD011479-481C-4928-85CD-C396B30979E8}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AE7FA7F9-313F-4FBD-8FEB-BC88F2C1D37F}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{EAFD8BD4-9943-4DC9-AB4A-8C3711F3CBBD}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{74677D3E-E86A-4D0D-BA6E-242F3C666F8F}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{55D6B190-D459-4C92-8BE9-72DC824E8FD9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}