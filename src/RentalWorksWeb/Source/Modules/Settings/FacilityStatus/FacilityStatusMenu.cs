using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class FacilityStatusMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityStatusMenu() : base("{DB2C8448-9287-4885-952F-BE3D0E4BFEF1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{53EF9997-2839-4930-B066-460BF5BBB2AC}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D8889374-AC26-4506-8A9E-EBCEB8EB5825}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C5442C2F-A0FE-40FC-B995-15E5C1D02E15}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DD4EA534-466E-4BF2-AECA-AD4A59E48668}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C5446ABF-10AE-4DB8-A9A3-2023B92C6202}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{E42AA7DB-1A19-4B77-81BB-746B993774EC}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{806DE9B4-E0B0-482C-AB40-84CFF29978C0}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5DE7E1EA-2375-4BF3-A43A-7A133FCE10D2}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B5DB99A8-3EE9-4341-B126-48BFCDF9F252}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D39054E5-F539-4D5E-8015-095CEDE7586D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4934428D-0D7F-4E50-B5BC-6BF2300B02DC}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{9AC85236-3E26-4AC9-B125-1783D85FCD5F}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{00EB79C3-3D76-4AC6-A042-ADB109FF5FBF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
