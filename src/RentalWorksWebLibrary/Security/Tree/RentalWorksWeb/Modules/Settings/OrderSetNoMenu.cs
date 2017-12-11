using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class OrderSetNoMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderSetNoMenu() : base("{4960D9A7-D1E0-4558-B571-DF1CE1BB8245}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{181D70DC-6FDF-436B-B03B-A34D29CF9BF2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{DAC842D0-ACEA-4386-A45D-9EBD4F93BBD7}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0A9E1370-D6CE-425C-8EB2-D88716765164}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{E3683F35-5190-4726-B3DC-77644DCBD85F}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{064EA903-C36F-4EE8-8A7F-04AB627D90A0}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3C89901F-EBB5-4BF0-9BB0-224569AC49C1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{EC39A33F-94FF-4534-B3EC-8137DCAB7348}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{82A8E8A2-0384-459D-B743-8FA8AFA24C9C}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{030038B7-5EC7-42EE-819A-0C40F0C1A347}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0EA0AEED-FAA1-4C93-A99A-DF4682EC6532}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F95F7C68-D7CC-4B39-A6ED-21ED75B9BE7A}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A8DB7C64-B7B7-44BE-8648-31C8C5C7DA0F}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}