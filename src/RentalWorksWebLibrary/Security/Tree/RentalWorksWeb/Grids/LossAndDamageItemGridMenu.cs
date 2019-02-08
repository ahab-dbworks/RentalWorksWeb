using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class LossAndDamageItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LossAndDamageItemGridMenu() : base("{D9D02203-025E-47BD-ADF4-0436DC5593BB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{EA65E2CC-2B5D-4C47-BD9A-71A6CC1F7381}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{02F6EB4A-7023-485A-9015-8C9212A75E7B}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{CF9254E3-1247-4962-9211-3E4E1ED70799}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{72BA990F-B215-404C-9A3C-A20855594569}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}