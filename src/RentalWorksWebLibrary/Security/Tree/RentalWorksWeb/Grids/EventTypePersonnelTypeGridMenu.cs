using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class EventTypePersonnelTypeGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EventTypePersonnelTypeGridMenu() : base("{F14FB171-801C-4CD0-A589-DF9511B501F7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{19451942-EB28-4433-B8B4-08FA84693BCE}", MODULEID);
            tree.AddNewMenuBarButton("{745B54F2-B7A1-40DC-97F1-6ED60079116A}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{C493D5F4-A8A9-433F-846C-379A8BBE511F}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B9670809-F31B-4FCD-93B7-248FE618382A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
