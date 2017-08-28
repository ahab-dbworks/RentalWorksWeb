using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class RepairItemStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairItemStatusMenu() : base("{D952672A-DCF6-47C8-9B99-47561C79B3F8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{02452DA0-1C17-4F0F-87E6-3813B00EDCEA}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3BE4B98C-987C-4780-8158-2EFE01F0B96D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{EF90EF55-7D59-4820-957E-313E4EC7CF8C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{90C54C2B-8502-4E65-9B47-F72C83E6A992}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6BE40989-060F-4863-8E2D-F0214DD4CA2C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A8C02239-66FE-47C7-84AD-FB2135657B1C}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{AEAC4B7E-F55A-4045-8443-CC03F9965A36}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3AD2238D-A82F-445B-B2AE-8692689DE8B2}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C8348691-7E1E-4E13-A57C-B9DB17A77318}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{137F691F-4375-47E4-A184-E71BC97D76C7}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C96745D9-B0D1-42D8-BA4F-4F3C40709A83}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6C2F977F-4945-4853-ACFE-9674A517E44D}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{F3514C48-E5E9-4C96-93A4-7E56DFEAFDE4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}