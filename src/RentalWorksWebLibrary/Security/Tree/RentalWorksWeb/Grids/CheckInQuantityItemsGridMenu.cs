using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInQuantityItemsGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInQuantityItemsGrid() : base("{2D2D0746-D66E-476E-9750-C11BA93A20C9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FEA467EB-40AE-4E44-9EA7-7916B86A1CA8}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
