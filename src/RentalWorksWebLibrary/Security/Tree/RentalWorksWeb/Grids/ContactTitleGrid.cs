using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class ContactTitleGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactTitleGridMenu() : base("{E104C48C-2579-4674-9BD1-41069AC6968B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3416D13B-87C2-4793-952E-ACB521940FAE}", MODULEID);
            tree.AddNewMenuBarButton("{8994E705-2D70-4694-91C9-92043C5EAADD}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{EB9C7DDC-7B25-490B-A1BB-9B6E7B7685F9}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{83DBE1B1-2FA2-4F48-AE19-FF2F7A5E943C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
