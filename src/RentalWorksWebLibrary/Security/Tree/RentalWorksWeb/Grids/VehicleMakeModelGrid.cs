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
            tree.AddNewMenuBarButton("{275BA516-03A1-4EDF-B94C-A9D242E20308}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{17CBBD34-D473-4823-A857-52E80A3DC5E7}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9A48B194-5E09-4677-B75C-5FF030734E19}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }

}
