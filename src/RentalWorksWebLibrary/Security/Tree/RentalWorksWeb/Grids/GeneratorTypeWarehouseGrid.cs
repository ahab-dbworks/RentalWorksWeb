using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class GeneratorTypeWarehouseGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorTypeWarehouseGrid() : base("{A310B3F4-2B34-433A-8F24-04400B45670A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{433E33F6-7F73-4E7A-B68D-D93ED3981C84}", MODULEID);
            tree.AddNewMenuBarButton("{679BC592-A2DE-4AED-907F-B346F858EC2B}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{699226AC-F9BD-48DB-9668-6FFF86C4F9E9}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D4A52549-0002-4785-9CAE-9579DBCF9E30}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
