using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CrewScheduleStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CrewScheduleStatusMenu() : base("{E4E11656-0783-4327-A374-161BCFDF0F24}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8A55E0C0-AA7E-4103-AB42-FB483910025D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0E0FC832-5A23-47C8-B0CF-AE92479F66EC}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6FF5A074-981B-4C92-8ED2-71BD216B31A9}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3909C62E-F085-4C98-88F2-CBB0F4444C2E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6CD750B2-164C-4B94-90D2-546C4219E079}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{889882BF-C4C7-44AC-A02B-F6609B19D479}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{08DB526C-9894-4996-A4A3-CA1028B126B8}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E4ADA20D-D9D8-4191-8CF7-17DF8DD37F56}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F76E8651-F5A0-458B-9D91-AFE8ADBE27C9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{BEEB7EEF-2C92-4AF0-9796-88286185DB11}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A95E0835-8AD9-4F07-AF10-138696B77F4C}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{A18123A4-30EA-4EE8-B4B3-041AF182A70C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{B5E70983-D404-44EE-9090-72BEA6FA0EBF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}