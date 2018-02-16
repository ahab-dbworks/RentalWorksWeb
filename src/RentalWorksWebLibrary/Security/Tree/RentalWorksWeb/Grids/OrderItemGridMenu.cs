using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public OrderItemGrid() : base("{C8A77000-43DD-4E49-A226-1E0DC4196F12}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D8762DD6-1603-46AB-ADBD-118D68BC63DA}", MODULEID);
            tree.AddNewMenuBarButton("{5A3352C6-F1D5-4A8C-BD75-045AF7B9988F}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{4113AFA5-29EA-4026-831E-685B904C5ADD}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{89AD5560-637A-4ECF-B7EA-33A462F6B137}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}