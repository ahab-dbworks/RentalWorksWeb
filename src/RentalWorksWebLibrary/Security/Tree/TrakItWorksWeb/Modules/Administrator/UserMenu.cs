using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class UserMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UserMenu() : base("{CE9E187C-288F-44AB-A54A-27A8CFF6FF53}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{74AA937C-0736-4558-9AAB-E820D1E0467F}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{AAB73DA4-2C4D-4BE7-B3EB-9D9C4CB47395}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{31EF5D4B-AFEF-4F1E-BF9C-75C2E6D5E613}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8AF7C2E6-FF2A-4E8B-B21C-79ACE1DAC933}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{9C5E8CE6-7BEC-462A-9486-FC8F4711BCFE}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{4BB3A5D4-B7F9-47F9-A399-682945367864}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{6D97DD75-F893-40FA-99DE-AEFD32F36D4B}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{05D709E1-F40B-4FEC-9E58-FC1247A846C6}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{F9EE2CE2-62B8-4004-8CD6-ADD3E484DA8D}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{AEE9D955-44C9-4960-BDC9-DA45110251EF}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{186CDE47-A574-4BF3-9B2D-11A795A4E4E5}", nodeForm.Id);
                    //tree.AddSubMenu("{FE638E00-E2F6-4FAD-BF50-32640A00FDB7}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{CA911C69-C2B5-4D8C-B7B0-DC9136177119}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}