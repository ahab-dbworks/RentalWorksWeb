using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseInventoryTypeGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseInventoryTypeGrid() : base("{D90C2659-F1FB-419D-89B6-738766DFCAD2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{96D3B669-FD46-4E17-8334-5C74380EA70E}", MODULEID);
            tree.AddNewMenuBarButton("{2ACCB20F-C2DF-49BA-B29F-202428A66CBE}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{D234E26B-E3E1-42E4-B0A0-058D2C9A5B35}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8CB929EB-5997-4E30-9487-3B8DAFCE377B}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
