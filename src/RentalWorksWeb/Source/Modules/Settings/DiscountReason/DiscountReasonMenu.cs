using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class DiscountReasonMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountReasonMenu() : base("{CBBBFA51-DE2D-4A24-A50E-F7F4774016F6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{719F31A4-4E72-4F9B-B24C-3E46371B7AA8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E3D749ED-1379-41F8-8EA5-9D0FDAE362D2}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{CA791141-58EF-4493-ADEC-3372D8622CF9}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{A2F4F125-A080-43BD-BBF0-B435AF9B35A7}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{731DF4CC-E30C-4585-B4DF-8FBFE47CB0C8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5A4897D7-326E-408C-9586-8DCABF2DEB4D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D843D15F-83D2-4F4C-A4C0-95168C530061}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{0AA3AAC3-E55B-4BBB-B8F8-E4D25715793F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{944353AA-CB7E-479C-B644-D1E5F5209D6C}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{952887E7-3EE5-4832-A34D-930A31B04673}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D29E9BA0-3DE4-4695-B212-9ED8550BA798}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{D1706B61-6723-43F5-B663-84A5B19D1A3E}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9758C2E0-27A3-46F7-8DD6-9D8A6A36E8F9}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}