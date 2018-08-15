using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInExceptionGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInExceptionGrid() : base("{E6A2B313-ADEC-41DD-824E-947097E63060}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6530DC2B-CE31-4249-8836-D1E607EAE5F3}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
