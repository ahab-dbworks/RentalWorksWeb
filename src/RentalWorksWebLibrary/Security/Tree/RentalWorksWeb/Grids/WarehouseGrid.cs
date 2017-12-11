using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseGrid() : base("{EF27A7FE-26D8-4F3C-85CD-9CD2D6FE57A5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D5F1A714-8C67-4208-862B-62A199CBDA75}", MODULEID);
            tree.AddNewMenuBarButton("{A823498E-9434-4DE0-9A51-93ED243AFF39}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{C405E58A-686E-49FE-9998-ABFB69ED1625}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7AFDE939-3610-4ED8-A317-F0E9C3166282}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
