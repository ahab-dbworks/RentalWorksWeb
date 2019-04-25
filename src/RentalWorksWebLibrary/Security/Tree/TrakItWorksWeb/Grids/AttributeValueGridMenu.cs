using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class AttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AttributeValueGridMenu() : base("{75647141-0545-442F-9A4D-03C90742E745}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FF19675D-DD46-4392-8981-9CB9CFFC753D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{5C4B5A4F-4931-4130-83C1-1FE98A5131E3}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4B7E4CF4-EF51-4260-A729-F7B0BA8D5BA4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1BBF9CEC-FB08-4FF6-B872-FB5322F6E828}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{B10771AC-BD6C-4D16-83C3-B532207A713E}",   nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{85B6C9A6-B8B0-4DA5-A99E-B95C7D469C45}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{05FF581A-DA11-41E5-93B6-1F4578FFC0BB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
