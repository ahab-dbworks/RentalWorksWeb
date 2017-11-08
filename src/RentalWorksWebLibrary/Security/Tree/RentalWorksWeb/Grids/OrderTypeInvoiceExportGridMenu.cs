using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderTypeInvoiceExportGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeInvoiceExportGridMenu() : base("{B24187E9-6B1D-4717-B9C2-F95C5543AE45}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8291B167-1521-4A70-9C76-740C69ED37FF}", MODULEID);
            tree.AddEditMenuBarButton("{A08931BF-F649-4946-AC3D-89B3B27F9BC5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}