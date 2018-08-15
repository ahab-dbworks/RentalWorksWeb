using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInOrderGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInOrderGrid() : base("{F314F7FA-8740-4851-8CB5-DA15EC02A5E7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A9341475-F894-41A2-9C09-B9325934F357}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
