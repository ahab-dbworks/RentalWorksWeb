using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInSwapGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInSwapGrid() : base("{47563A6D-1B0A-43C2-AE0E-8EF7AEB5D13B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5050EFC5-28A3-4D57-85D7-D7D6E52656E8}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
