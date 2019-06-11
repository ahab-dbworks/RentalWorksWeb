using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class OrderTypeNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeNoteGridMenu() : base("{55C06675-4E4A-4304-9846-5113C003EAEC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B15EDC15-1543-425F-B69F-55612C905CFB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{5ECC2498-BDD5-4EB5-8F8D-01F4473584FE}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2C6A4741-0445-413B-9F55-E715BDA7D34D}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2E9D48FF-D3AB-4C8F-9F79-286111F3029B}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{6B4584FF-B57D-4DC8-9A71-2D138D780E8B}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F1445C9A-6930-4207-8680-917F94E5CC90}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B15390C0-AC9A-4F6B-A495-AC1B3B6E6448}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
