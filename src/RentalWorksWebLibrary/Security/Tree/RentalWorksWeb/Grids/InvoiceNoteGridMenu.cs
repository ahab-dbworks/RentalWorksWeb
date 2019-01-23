using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceNoteGridMenu() : base("{09E91168-0C59-4EC7-9DCD-2B65F0EB2A6C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{10B82690-F22F-467F-A8F9-401044ED8F85}", MODULEID);
            tree.AddNewMenuBarButton("{C920C157-83E4-4BA4-9956-7967E5DF2F58}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{013E7A2E-4555-44D0-A1E2-788EDFFA3F57}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{EEE0CBB6-A795-404A-B64F-9BF3C62686B6}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
