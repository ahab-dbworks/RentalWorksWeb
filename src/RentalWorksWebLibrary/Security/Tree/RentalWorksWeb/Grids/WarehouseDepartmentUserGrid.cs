using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseDepartmentUserGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseDepartmentUserGrid() : base("{4B3FB84E-CC4D-4EAE-917A-1291B733AC89}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{37DDDD3B-1287-462E-A9B0-18EF6B5A60AE}", MODULEID);
            tree.AddEditMenuBarButton("{3DA5A867-A5EB-44DF-B635-F17F1A1E2808}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
