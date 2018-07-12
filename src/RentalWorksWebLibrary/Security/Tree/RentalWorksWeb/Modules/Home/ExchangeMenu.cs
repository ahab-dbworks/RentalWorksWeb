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
        }
        //---------------------------------------------------------------------------------------------
    }
}