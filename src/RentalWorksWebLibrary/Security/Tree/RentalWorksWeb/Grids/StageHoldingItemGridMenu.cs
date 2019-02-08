using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StageHoldingItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StageHoldingItemGridMenu() : base("{48D4A52C-0B47-4C85-BAB7-8B0A20DF895F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5D194EB8-7436-4C54-95C6-0BAC10BB707F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B007AD8E-457B-48A0-9AA1-0C9828080E76}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{60621CA7-4AE2-4965-988F-17A7E8A04CDC}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AE5FBE48-BF99-48C7-8BC7-561A505E44F6}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}