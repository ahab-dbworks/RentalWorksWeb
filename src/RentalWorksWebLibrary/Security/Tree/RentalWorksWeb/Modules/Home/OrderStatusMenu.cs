using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class OrderStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusMenu() : base("{F6AE5BC1-865D-467B-A201-95C93F8E8D0B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{777738B6-D26E-4B9F-8CD2-8CC487CCC2B9}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{81E17239-3A20-4DFF-8E8F-1D8F7EB98CFF}", nodeForm.Id);
  
          
        }
        //---------------------------------------------------------------------------------------------
    }
}