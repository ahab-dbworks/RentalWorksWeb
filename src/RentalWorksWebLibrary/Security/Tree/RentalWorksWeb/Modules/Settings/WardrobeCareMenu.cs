using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeCareMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeCareMenu() : base("{BE6E4F7C-5D81-4437-A343-8F4933DD6545}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{284456EA-D0F3-405C-B6B4-4A08C57644E2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{78B0BEBC-47E3-46A5-A9FC-EB90E26E5DF7}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C4F2842E-8248-4610-AF15-0A09F8876F49}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0E8E6CC3-E0E6-46AD-8823-8E4A28C5A589}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BB57CD85-2441-446B-AB76-5759380DFABF}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3339074A-76FD-4F1C-AC9A-C1ED379F7721}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{3E3A6BE3-3674-4E68-9769-2767982483F8}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{454AE8A1-3E4A-487D-90AE-443C399F0999}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C325F4F7-A86B-478C-A2F5-ADE3B8FA03DA}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7577D2CD-3925-4A0A-8F61-40B0B886385D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{82C03DB4-A498-43CB-B973-DD7BA7E709ED}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{8CF6406A-1713-4546-B4EB-9295B790290B}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{FCB380C8-99D5-418F-9701-C3EB00F3E44F}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

