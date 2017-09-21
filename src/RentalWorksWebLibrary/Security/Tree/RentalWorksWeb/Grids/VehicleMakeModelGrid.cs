using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VehicleMakeModelGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleMakeModelGrid() : base("{C10EC66E-AA26-4BF6-93BF-35307715FE44}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6ECD7342-FD5B-4F60-ABFB-24EEEA16877C}", MODULEID);
            tree.AddNewMenuBarButton("{757D8FBF-2C40-4C2F-BAEB-FFF74817F805}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{B3661BB5-BAD3-439C-9C0E-6023296CDA72}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{510781C0-CD5E-4314-A492-3462C89A7F3E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }

}
