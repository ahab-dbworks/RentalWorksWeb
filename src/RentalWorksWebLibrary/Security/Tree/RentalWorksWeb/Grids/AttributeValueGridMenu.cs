using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class AttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AttributeValueGridMenu() : base("{C11904A1-D612-469C-BFA6-E14534FC8E31}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1C69781C-3638-407E-9C0A-EBC4BBA6A4A4}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CF9D3FB7-AC49-4582-B67B-F2FFA4AFB31D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A509847D-98AE-460A-BB1D-6A83A74EEB10}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1705F2C2-0B9D-424A-A6BD-F6DDF635AED7}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{8D0BF6A9-AD7A-4B28-82A0-3A435B4BBD7C}",   nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{46CA589A-F2E3-4775-B199-B68E682DB534}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{27EE1323-4332-478C-BD08-6CAE891E56B9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}