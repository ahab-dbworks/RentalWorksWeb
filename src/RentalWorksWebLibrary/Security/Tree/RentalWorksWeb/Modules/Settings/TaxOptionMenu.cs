using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class TaxOptionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TaxOptionMenu() : base("{5895CA39-5EF8-405B-9E97-2FEB83939EE5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C54A9482-13CE-4B50-BBD7-F2ED3DE8CFB3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B8906B86-7403-4DF9-9036-8E56F9353EF1}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{8ED9D19B-4D60-49C7-9EA2-C1824363043F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D3D98605-0A51-4254-AAEC-2F1587601BCE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9EFBABF9-6B75-4FEA-8EA7-B797BF65C113}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{AB62570C-9961-405B-B5AF-09F1E412FFEA}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{25F2E5B6-D4BA-47D3-96C2-D0B53769F4F3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D208140A-8BCA-4E3B-A60D-DEEF2C98FB97}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FDC20DA4-930B-442C-A58B-CFD9C0BDFAE9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{128030DD-3FF1-486E-9481-D68EBD3C24F4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{CDC9E5D0-67F8-4062-8FFB-59239456E9E2}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{ECDF8EB4-51E5-47EC-A00A-CC11FD98E3F9}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{A4043D0C-CBDE-4CA6-92BC-ECD41033FAAD}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Force Tax Rates", "{CE1AEA95-F022-4CF5-A4FA-81CE32523344}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{88D09422-88B4-4453-BD5B-6863687C4156}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}