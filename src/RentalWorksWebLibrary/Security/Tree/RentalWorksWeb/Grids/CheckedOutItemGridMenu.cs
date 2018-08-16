using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckedOutItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckedOutItemGrid() : base("{48CC9E19-7B73-4BA7-9531-20BEA3780193}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A8F5673A-6406-4603-9A6E-007B171BA608}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
