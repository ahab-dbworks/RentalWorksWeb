using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseOfficeLocationGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseOfficeLocationGrid() : base("{99C692AB-13CE-4113-88CF-6AC15821B9D4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{463D0F3C-CEE7-43D2-A8C7-1B250DE62A4E}", MODULEID);
            tree.AddEditMenuBarButton("{154B5A5B-A90C-4C81-8385-6946F1D8AC08}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C28C0A8E-63A3-4D11-974B-05605F9791AA}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{1B9B18C3-1E76-4423-9A53-CE43D54D89D2}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
