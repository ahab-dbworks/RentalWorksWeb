using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseDepartmentGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseDepartmentGrid() : base("{CB4CE3A5-6DCC-497D-84D1-0B3FBAAEB19B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F9404E78-955B-44E6-9448-36E3B33369DB}", MODULEID);
            tree.AddNewMenuBarButton("{E143A8FE-1F5A-489F-9C04-8EE169233DB0}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{355E582D-C791-4355-95B9-31AE121CC505}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B7BFA544-F367-4F83-A28C-5F621C364EF7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
