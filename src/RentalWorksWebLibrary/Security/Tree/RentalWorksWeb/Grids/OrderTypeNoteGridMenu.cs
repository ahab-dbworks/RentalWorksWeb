using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderTypeNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeNoteGridMenu() : base("{DD3B6D98-DBAC-467D-A3A8-244FCD4E750A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{AB700EC6-9BF3-4193-ACFF-04998D1B727E}", MODULEID);
            tree.AddNewMenuBarButton("{F4DCA901-0E82-4FC4-A509-B113DB2EE8C7}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{1E10221A-500E-4D80-A129-CB589946B851}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8497BD23-6661-4B54-ABB1-A8FAB8651AEA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
