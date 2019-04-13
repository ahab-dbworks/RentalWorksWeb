using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class LaborTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LaborTypeMenu() : base("{6757DFC2-360A-450A-B2E8-0B8232E87D6A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{CF342031-7B1A-4DB5-9A5E-3E1EE90C5485}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{65240600-BB06-43FB-A863-63411CCFD5DF}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F3D24CF3-9431-4B1B-AF16-5A07B356B3A7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{906F46DB-34CA-4D04-94FA-319457425EFF}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D0192407-FE2E-44B8-8626-2994F508B0AC}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{E37F944F-0163-4BAC-9E36-605808A9D74D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4060B8FB-F61C-47F7-8705-5EA0CDF2F331}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{92717182-FDAF-4CBA-BB4D-308CC8902F75}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4B5C6A3B-B966-48E3-949F-E3575FC984F4}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{44933E37-F40B-42FB-BD4E-96B845DCE856}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DA555680-8BA9-47D7-8BAF-0C9A7F9944FF}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{D197F145-38A8-43E0-9886-259026AB4C14}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{190CE09B-520D-4838-B052-B0050B27B7A7}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}