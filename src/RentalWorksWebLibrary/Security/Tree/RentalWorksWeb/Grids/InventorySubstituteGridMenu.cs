using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventorySubstituteGridMenu() : base("{B715DFB4-5700-48DE-878A-F8D93F99ECA3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A6FC3B66-D99E-4E38-AEB6-2B07AAFC7F42}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{7987B7A3-4EE4-413C-96AE-506E506842B8}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{774814A9-B734-485C-863A-6C0B3538BFD5}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F7BF0837-1558-4A1A-84CF-B97B58F68693}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{15CCA7B4-6E06-40FF-AB57-14FD12AE31F5}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{B3457C97-40EA-45FB-A615-D861D0CB0667}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{BC5ABB37-EB8B-4BAD-B9E9-1B00A81683F3}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}