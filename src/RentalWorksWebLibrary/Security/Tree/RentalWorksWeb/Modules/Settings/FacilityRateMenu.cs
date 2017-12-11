using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class FacilityRateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityRateMenu() : base("{5D49FC0B-F1BA-4FE4-889D-3C52B6202ACD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A5A347C0-9213-4704-B891-9D6C27E4B96E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2C0EFAA1-BF07-404C-93C0-CB26A5E3B3B2}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{54B2E8CA-B9D0-4D05-9354-37A7D602B326}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{1BB5967E-30F2-4DD6-A296-E3D1EC816CA7}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{53F465ED-23A1-4BAF-8C49-A14DAA95C98C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{D190FC64-85B3-4950-871A-D82F9BE3A3CE}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{47EEFEEA-F98D-4F73-AF90-5E028387EE96}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{23E38841-A64D-44FE-9356-014EFC61CF39}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C7607797-22F0-4A2E-844D-F22A7D2443B0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7DECED57-2EEB-4399-8036-F91555406A3C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{955D8A01-B051-4111-918C-6B57001A2724}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C3992678-FECA-40E9-9A58-7E0D00797901}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{0F724E23-4C91-489C-BDD4-90EE0D7B654B}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
