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
        }
        //---------------------------------------------------------------------------------------------
    }
}