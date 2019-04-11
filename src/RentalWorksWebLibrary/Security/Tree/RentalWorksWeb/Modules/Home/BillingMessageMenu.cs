using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class BillingMessageMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingMessageMenu() : base("{B232DF4D-462A-4810-952D-73F8DE66800C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{08F202E9-1110-4FEB-A7E0-68AAA45F51E7}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D984CEEE-09D3-4DF8-99DD-1DAE36B07187}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D0E5B703-FD5F-4EFD-BE4A-E6677BEA78AA}", nodeBrowseMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}