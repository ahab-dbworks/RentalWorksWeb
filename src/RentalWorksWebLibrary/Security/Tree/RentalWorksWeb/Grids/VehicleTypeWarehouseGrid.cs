using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VehicleTypeWarehouseGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleTypeWarehouseGrid() : base("{51707760-645D-452C-A545-37A4C861B139}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D0FAC42A-3367-4993-A8B1-008EF6E2C327}", MODULEID);
            tree.AddNewMenuBarButton("{A7FF2D80-6388-4D3C-BE75-7D809EAD140A}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F2C56529-7F4C-44DC-B2AE-77037A213C3C}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7C96DDD4-3922-4A5F-BF21-BB42B7AF22CE}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
