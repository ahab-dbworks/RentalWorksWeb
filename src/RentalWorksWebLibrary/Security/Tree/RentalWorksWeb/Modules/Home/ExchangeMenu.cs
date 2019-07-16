using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ExchangeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeMenu() : base("{2AEDB175-7998-48BC-B2C4-D4794BF65342}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{A9D002C4-7522-4B6A-AC82-2099F2C733E4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BAE64A52-57AB-4377-8EDD-B5E51C0F3F2F}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{EBF4FB8F-9A86-45DE-9C35-3EE2D76A39FA}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{BDECC917-969C-4DB1-BC4C-9E38382B3619}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Cancel Exchange", "{2301B78E-7928-4672-8747-29ED57C529FC}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}