using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class POClassificationMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POClassificationMenu() : base("{58ef51c5-a97b-43c6-9298-08b064a84a48}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{52c0da24-9d6b-454a-99f4-2b1d63c63649}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{a6d4b8a2-7b62-4d68-a413-0f2b6c82820b}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{770bc658-19e2-40c3-91bb-8459d36c6a45}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{050e37b7-9eaf-4dda-9307-1da0ae37245e}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{d4752582-7cac-4a1b-8e94-6fdbc392486a}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{ee4ab8f8-e5d6-4aa7-80b2-8610bfc6ef30}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0a9f5dda-f916-4153-9a40-c33d07a2bfbe}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{de61da7e-fc80-48c4-b33b-e7c63a66dde2}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{aa1ace86-d3be-4436-ae0b-b1b7b751984e}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{d467c18a-1db2-4bd0-ad4a-d6e2764644a4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{d57a94b6-ede6-4a62-9c4c-60d74a25360b}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{993e8943-d7b4-45bf-ae9a-4e2dad2d99b6}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6fe78a18-a13b-4c81-b2f9-8fe3ecb3e214}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}