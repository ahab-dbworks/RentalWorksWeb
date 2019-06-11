using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class CurrencyMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CurrencyMenu() : base("{54dc9926-f6da-42a9-8d06-3e2aa3ba5052}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{a460775a-99e6-42f0-86ef-e7342ce80e85}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{a16687ac-1c1e-4d31-86d3-eceb7f71323a}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{cb275dac-80a2-48c9-b54c-c5261d08c76c}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9065d560-8f96-4005-a6b8-6523d71a69a6}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{12ea7253-11a2-4aeb-9d6d-390db5e1380f}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{191c41af-c936-496a-b198-d5a37c3e73e3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{65cba43a-a26b-4938-95e4-6d938d61f954}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{49807c05-de9e-40d8-be4a-fd563448cf66}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{89b8677c-64ac-4229-9108-e71c2ca3c1fb}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{14b7ee1e-f702-4828-a4b0-31e061ebb107}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{c71dc7ea-2b68-468d-aa2e-93e859865f98}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{720ffa93-4d66-443d-b940-45cebb07d407}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{066c72e3-fb5e-477e-aa4b-c5dd2270a6ad}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
