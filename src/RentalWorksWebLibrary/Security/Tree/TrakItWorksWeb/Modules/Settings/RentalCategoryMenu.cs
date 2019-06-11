using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class RentalCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalCategoryMenu() : base("{FE2EF49E-D178-4DAE-9293-4019FFD7E52B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D7FD6E29-057E-4364-9902-E4307C50911E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{ADC0DED4-A8C5-4B20-AE59-A69359C53218}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{026CA16F-E800-4CF0-89D1-FAEF63A7B5D7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8C5216B8-9C2D-4064-827B-90203CAAE4A1}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{82B5667D-3109-4D95-B03F-C6D7BB65911A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{26BDAE45-6E2F-4760-B7EC-2ACD950B66B4}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B2845A80-2052-4D0E-A6E6-163C672D881A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A61B3051-0368-42B5-9A4D-E1E1F4B5511B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{03FD83AB-451E-4461-AF4D-C10818B3129F}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{B949984C-1E2C-435A-8C7F-BC98AA16B9FF}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7B00C07B-801E-4567-8E22-3D9528D2C22B}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{60523B2D-D70A-499B-BAB5-4E016492A49B}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{375D9409-CF03-4300-96FC-E649518161F5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
