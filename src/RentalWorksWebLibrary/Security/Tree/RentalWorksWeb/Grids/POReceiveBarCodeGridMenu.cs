using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class POReceiveBarCodeGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReceiveBarCodeGrid() : base("{27703F1E-8F2A-44E3-93AF-F46BADC3D4B1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9B1A698B-5A01-4621-95C3-7E6C18E01F32}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
