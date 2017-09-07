using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CompanyTaxGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyTaxGridMenu() : base("{0679DED3-7CDF-468D-8513-7271024403A6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{51540DC8-F7FE-4D9C-9246-21A006C95E4A}", MODULEID);
            tree.AddEditMenuBarButton("{323456A6-37A9-4B22-8D32-07729D30F8CC}", nodeGridMenuBar.Id);            
        }
        //---------------------------------------------------------------------------------------------
    }
}
