using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DiscountTemplateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountTemplateMenu() : base("{258D920E-7024-4F68-BF1F-F07F3613829C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{5F3A810B-F578-4C3D-B77D-E613D39B6768}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{08F5737A-4242-40C9-A3B4-F6ECD7876A01}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{9C0CD347-AF71-4343-99FF-39EF40946260}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{2A3CF447-CE35-43D8-8AC1-00E31F50EA58}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6019E5EF-A4C2-4DF1-AC94-6FE15BF6F7A2}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{8A19F7A4-227A-4E64-ABD1-B7CDFA13B95A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{3DBF71A2-3F1D-4E7D-9CA2-6D930011246E}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{549A95F9-7CD0-45A2-985F-471002251423}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FD7382C1-FBB4-4D1D-A971-BD8CFEDCFF24}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{48CC2020-D63E-4FE8-B659-87FA1DC129D8}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1A356BE6-C8FB-46D4-A574-DCD5AAF782D8}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{18C79978-CE21-427C-AAAF-CE68487684ED}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}