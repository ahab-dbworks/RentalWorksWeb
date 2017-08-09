using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class ScheduleTypeMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ScheduleTypeMenu() : base("{8646d7bb-9676-4fdd-b9ea-db98045390f4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{99dfe534-4cad-4b83-8ccc-1fbcd8da2a67}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{a390f6b8-cd1c-4221-9820-45307e4e6df8}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{d88a0a91-c074-4461-a760-4080d51c4266}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{38ca226c-ea6f-4b5f-9580-0e3ad03bbc62}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{88a9e5b6-fec0-438c-b15d-629ba55b3ebe}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{20234c30-4242-4b3d-97e6-72c29b8ecb81}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2e3a80cf-3b0c-4f40-8da9-f892b335bb02}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{29acffa8-cb5a-43a3-a8bd-5d1b67eb54ee}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{a721f70f-f1ce-4c68-b705-9ef107ba5470}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4fb64250-6ac5-48cf-9ea4-d9baadef4e0d}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{739b1dad-5816-4d00-967b-bd49c92a6dcb}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{e43040eb-6c10-4585-a014-8aade708c4dc}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{3fdb9361-0c46-426e-b7e3-15e706730b6b}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}