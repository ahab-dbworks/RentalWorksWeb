using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class FacilityScheduleStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityScheduleStatusMenu() : base("{A693C2F7-DF16-4492-9DE5-FC672375C44E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8A9352C9-B55E-4C78-9FC4-DFB52C06095E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{69E3BF61-0C16-4E29-AF5C-D99C3B510AD0}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3316BFFF-9392-43BB-9381-0E98E00C4A03}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F102C4A2-5609-4A85-84D4-B71B30FAEA19}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6B74E4DD-FFF2-4C3B-AF65-70A991BBD0D8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3EEA5CBD-DD5A-49BA-9845-7CC95A91EB3D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{34FEDB71-D8C6-4939-BD5C-6FDCC09CDDD3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C1909087-E8E3-4CB8-9012-3CB7A23F817B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{36506236-37AE-448F-9E68-73BDA93DD1A2}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4135B0F9-ED3D-493F-91A2-71A5906D9506}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{54626E7E-6807-4702-A8FF-3D3EF4067998}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{D0C7F929-442F-4B76-85C1-340EEAFF6BD2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D6636B1A-05C3-4766-A580-DEF7576E88E7}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}