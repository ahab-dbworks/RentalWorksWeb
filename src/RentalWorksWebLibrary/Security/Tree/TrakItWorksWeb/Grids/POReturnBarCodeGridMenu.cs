using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class POReturnBarCodeGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReturnBarCodeGridMenu() : base("{4A0DBA79-DCA2-4A57-AF72-FB49A350777C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1A4A985A-4D64-40D2-BBA2-7070FC350976}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B36CBAC9-BF39-4015-BDE7-F04962A92BC8}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{7ED38AE5-E921-4010-9EC5-B91189948FAD}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0F6D2C0D-A9C7-435D-912C-1C7AED0E3A9A}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
