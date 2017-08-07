using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class OrganizationTypeMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrganizationTypeMenu() : base("{fe3a764c-ab55-4ce5-8d7f-bfc86f174c11}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{a1a5a15d-d6d1-4ab6-8936-b1109a81a0d8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{41f5c767-d1ec-4c3f-abd1-f1f2d41bbd48}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{8e1ee536-59cc-4177-ad7c-3c421e6c72eb}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3e9dd812-81e3-4540-a702-783f654a3f2e}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{cbe6b6ca-a493-40ac-8f17-01c41d636bc3}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{d2705a40-973b-4064-9ce7-16eecb285b81}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{d71a8dc4-5a52-49a1-9c0b-a7623321f415}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{410777c0-184f-4492-8b8c-a77c2fad673f}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{e829a78f-63e6-447f-b7d2-c391499ee875}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{5ebc5557-6310-41fc-8d07-12cc64a911ff}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{962716f7-a0dc-4518-b6e4-8ebf4cc7d6e2}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{3c233e28-1b29-4388-959f-cb7f0c33c37f}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{47674bab-6cf2-4e05-8f01-1f802a1ecba6}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}