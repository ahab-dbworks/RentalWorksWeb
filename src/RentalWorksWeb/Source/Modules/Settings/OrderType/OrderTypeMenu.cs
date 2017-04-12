using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class OrderTypeMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeMenu() : base("{33178C5C-1E9C-4638-8C31-00FAF9EF2054}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1A6E6411-AD4C-49FA-B72B-DDE2AB499001}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{01E7356E-9A38-495D-8922-CA988008F1F1}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{E7C47D39-D950-4708-AB6D-4DFB2597ACE9}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{C151F1A0-AD52-45BC-B9A8-65B7B9758DA7}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{45ACD1A3-88FA-46BB-8F53-18CA7C2FCB7C}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{272281A7-0819-48D2-AEA0-C083EFCEEBF0}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{365D72AE-8A02-496A-906B-9E75A376D205}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{99985836-5D61-4847-BF05-84D731FE68EE}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{C2E6FB2D-4536-436C-8BC7-DDDE61509477}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{2C2D8E12-F8BD-447A-9C27-4DF8A5C73F03}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{6C18D00E-E12E-42D1-A3F6-5AE368022E58}", nodeForm.Id);
                    tree.AddSubMenu("{252AD368-A654-4C10-899B-A5A4DC41B8A1}", nodeFormMenuBar.Id);
                tree.AddSaveMenuBarButton("{6C9E224C-D60F-4801-9C5C-3743A72B18D2}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}