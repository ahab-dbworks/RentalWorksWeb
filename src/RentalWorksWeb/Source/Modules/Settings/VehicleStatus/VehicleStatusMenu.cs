using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class Vehicle StatusMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public Vehicle StatusMenu() : base("{FB12061D-E6AF-4C09-95A0-8647930C289A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{6C6133E1-F822-4981-8723-C78D76500B4C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D09C1678-DA01-45E0-AB19-8D68EC90CE3C}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{61F7D74D-BA23-4083-B9AF-92DB50126019}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8F32D94B-87D1-4E29-801B-7DB8935172C6}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4481DD35-08B3-42EF-A36D-2A947DCE18BB}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{8E350E75-6E72-4994-8692-38820327F137}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{766E8413-5343-4CFC-8E0F-6AC0A2B54B65}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A32E2FD9-9119-4CDE-9D64-FF5E24F3F970}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{EE0BF675-C3ED-4797-AB2F-0CA5B03A4639}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F2A5D334-9738-4D0B-A5EB-6F24D1EF80F2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{91EE5CF0-0529-4429-AE46-4634993B31B9}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5ACA8EAF-EFF7-4A9E-B736-0A014E9E56C2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{77167A66-6E37-47D9-9301-84C14C1EDC38}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
