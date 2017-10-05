using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class MiscRateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MiscRateMenu() : base("{15B5AA83-4C3A-4136-B74B-574BDC0141B2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0480F22C-D604-4D0D-BA9F-37C386C3DAF6}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6BF4B607-7F8C-401A-83ED-1C51AA60BE88}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{92C341CE-253E-46EF-BF24-9A12A34328C9}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9FE16FAE-38E1-4C65-BCA6-A5C28FBD3412}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A1F16426-FE1D-4BB4-BFE7-12AFC633AAFE}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B66DCAEF-6889-4A44-BA48-8AFF43BFD4BE}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4C958839-0947-41C3-9354-3730CB4C371F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{CF23A808-1C3B-403D-9454-E842DFDE834E}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CAC421E2-1AFD-49F8-B916-043B47C2E7DE}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4B111BC9-0214-4429-9430-B53497EDB213}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{46FA82BF-09F7-47E3-954A-6ACFB662B5D4}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{2A725DD2-4FAD-4BBB-8AF8-D861C21664CE}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{C5AD8E4A-7EFF-477F-8729-2BB34D01732A}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}
