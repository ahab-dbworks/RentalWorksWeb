using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class OrderStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusMenu() : base("{7BB8BB8C-8041-41F6-A2FA-E9FA107FF5ED}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{639BF9C7-6969-443C-97C4-3E31929E1877}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E8CF4C12-946E-4B61-92E8-D090922D39BB}", nodeForm.Id);
  
          
        }
        //---------------------------------------------------------------------------------------------
    }
}