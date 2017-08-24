using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class GeneratorRatingMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorRatingMenu() : base("{140E6997-1BA9-49B7-AA79-CD5EF6444C72}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F1CA6B14-3C16-4DD1-9D4C-D7399ED28216}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3DD79D2F-850B-44F0-8847-75718EF3BE80}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{4E80D83A-893B-4AF2-99A8-C82079A40080}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{75A925C3-563A-49E5-844F-F7B46ED12443}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{385CFDA0-6F91-4D76-B199-0D875D90056D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{DA17A2A9-AD32-4DD4-B142-DF05E998FAD0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D4AA14D0-1AE7-49F9-9D15-5204C2B9611C}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{75E7F688-16FE-4195-B2F8-09FF5637B297}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1C19C600-D39E-4C97-9C1B-A54473CA8F79}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{9BE5D49F-DA90-47CF-9F85-1B30EC040E49}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5112530C-2BC9-4F90-80AA-28C86A9AC3A0}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C6D63D09-0B82-43EE-B36E-42927D561CAF}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9C7103CF-BC1A-450E-B383-510454C9EE9F}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}