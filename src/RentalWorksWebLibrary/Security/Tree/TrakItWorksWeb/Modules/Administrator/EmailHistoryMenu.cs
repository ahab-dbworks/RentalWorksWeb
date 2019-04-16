using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class EmailHistoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EmailHistoryMenu() : base("{34092164-500A-46BB-8F09-86BBE0FEA082}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7DB6A6B3-02F0-4541-97A3-F8A53A72DA84}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{F124D48B-0F0F-47D4-AA30-2FA2C6A0CAEF}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{1454D7C6-0C40-4924-8FCE-1056B9BD96C6}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{20090737-726C-4213-875B-29F234424E6E}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{2DC79D8F-19FF-4EEB-BD44-EBC9289B77E2}", nodeBrowseExport.Id);
                            tree.AddViewMenuBarButton("{9DF55534-B908-481A-905E-8D2BB91AB287}", nodeBrowseMenuBar.Id);


            //// Form
            var nodeForm = tree.AddForm("{446A4B54-1B27-471F-9E8D-82AD62617B63}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7D0ED4DE-C276-4CBA-811E-916686C39BA1}", nodeForm.Id);
                tree.AddSubMenu("{7FC73640-74D9-4652-A76A-61E053C03DC8}", nodeFormMenuBar.Id);
            //        tree.AddSaveMenuBarButton("{0DA26AA6-682B-4011-B13C-151C5A101AED}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
