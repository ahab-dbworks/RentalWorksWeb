using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ContainerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContainerMenu() : base("{28A49328-FFBD-42D5-A492-EDF540DF7011}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{876F7AA9-99A6-4DE9-A712-19FA469E4F99}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{90A455F1-FB4F-42C3-B2B2-8C6B35382A05}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{BE4CF6E1-B772-460E-84A4-6BA82C0C1380}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DD3081C8-9D68-489E-BA46-5F8AB28AD963}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{D3E201BF-644D-4EB9-992A-4660AC4AA646}", nodeBrowseExport.Id);
                    tree.AddViewMenuBarButton("{90DD0162-1C44-40BF-9C0B-7A0470AD0D66}",   nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{3F90C227-F28A-43D4-BC2D-59A368ABB99B}",   nodeBrowseMenuBar.Id);

            // Form
            //var nodeForm = tree.AddForm("{02ECA7B9-2D14-4314-9CF7-77B9B63923CF}", MODULEID);
            //    var nodeFormMenuBar = tree.AddMenuBar("{97111756-8629-471D-90A0-5C6F4534CD11}", nodeForm.Id);
            //        //tree.AddSubMenu("{D3028487-10D8-4447-937A-E157FC20BC41}",           nodeFormMenuBar.Id);
            //        tree.AddSaveMenuBarButton("{5BC7C19A-BF07-4DEE-BEBA-C22BFDE79757}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}