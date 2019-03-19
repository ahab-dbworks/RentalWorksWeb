using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Home
{
    public class ContactMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactMenu() : base("{3F803517-618A-41C0-9F0B-2C96B8BDAFC4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0D6D4B2A-B26A-47B1-9DF3-8E9586D53473}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{95DD35BC-70BD-4D55-8BA9-C83F209B0E80}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{A2CEAA51-0579-4F68-A3CF-3CB9FE58EEF2}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{43F394E9-F558-45A1-AFF9-56D21BF78BE9}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{49CE0DC0-7620-45D2-95BC-D722F1AEF9B9}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{CC517F36-90F9-4F50-99A5-68434C0331A6}",    nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{3F3D1696-A1AC-46BA-B5B7-DB28FA9778AD}",   nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{84A03BB6-113F-4C8B-921F-83C366E646FB}",   nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{FBF75550-FB1C-4DE1-921E-326E038F3BF5}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{02ECA7B9-2D14-4314-9CF7-77B9B63923CF}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{97111756-8629-471D-90A0-5C6F4534CD11}", nodeForm.Id);
                    //tree.AddSubMenu("{D3028487-10D8-4447-937A-E157FC20BC41}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{5BC7C19A-BF07-4DEE-BEBA-C22BFDE79757}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}