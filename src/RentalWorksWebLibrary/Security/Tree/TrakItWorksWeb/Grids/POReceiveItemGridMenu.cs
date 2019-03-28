using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class POReceiveItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReceiveItemGridMenu() : base("{22FFEC79-6500-4C85-B446-E111C8CDAC7C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{59C26E5F-B1B2-4271-A8A1-44CC0105D606}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{F7017607-DE09-44D8-9D6F-6CFC84DFEB64}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{07C422AE-B9B1-46FB-8B1D-ED0CA9D94330}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7147A3E6-46EE-4037-8C7D-C6FA4886883A}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
