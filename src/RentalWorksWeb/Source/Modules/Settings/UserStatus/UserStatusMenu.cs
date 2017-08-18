using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class UserStatusMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UserStatusMenu() : base("{E19916C6-A844-4BD1-A338-FAB0F278122C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1A14A27E-EA7E-44CA-B64D-5DBA41CF45C3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D79F61D6-0E7F-49B5-BE66-3387C20C15E0}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C1AE2047-B52D-49F3-BF63-81B740145E15}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{84FE2814-01B0-4A99-AEA8-1E93B087BA3E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E08FCC3D-E794-4C27-8170-E83AC2EFAC0C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A0BDB4DC-BF1A-4652-84A6-5E7437941245}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2C00AF78-C9ED-47E5-BA33-34F6FAC23947}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{340F52B2-FA19-49F8-B854-712D142E2F41}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{3D4139CF-A150-47DF-B5C4-EE8627EA8704}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0BA62A0F-8AAE-4641-A257-4DC3BB5400F3}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8984079F-5673-4B71-ACAC-7CEB730DF31D}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{89A643D2-4BAA-4C37-BDED-FE1209EA0F61}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{DFB4C57B-A03C-459E-9A45-751A1906B191}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
