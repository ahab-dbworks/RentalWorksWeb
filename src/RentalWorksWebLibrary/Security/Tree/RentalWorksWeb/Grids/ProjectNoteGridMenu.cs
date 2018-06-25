using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class ProjectNoteGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectNoteGrid() : base("{686240FE-8276-4715-A7ED-44B4D4A7DC86}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F96471BE-3D30-41B1-8D4A-8CA9F95C57DA}", MODULEID);
            tree.AddNewMenuBarButton("{D7DCD827-1841-4648-B9C6-A76B829F0ADF}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{E874B230-BB55-4BBF-A79F-8053FE5FD2AF}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F8384FE5-339B-4AA7-9630-42347E58A691}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
