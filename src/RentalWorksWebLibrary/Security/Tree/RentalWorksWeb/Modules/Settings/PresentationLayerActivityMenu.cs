using FwStandard.Security;

namespace RentalWorksWeb.Source.Modules
{
    public class PresentationLayerActivityMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerActivityMenu() : base("{084E26BD-37FD-4E0E-AD65-A0824A6DC884}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3FDFD7E2-FC42-4FB4-B053-0CE5F63745E3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2B092E80-668B-4F09-9E9C-76722136A4E6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C2354CA6-AA76-4840-9FA2-A534C1009B2C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{AA83FDD6-EF70-4FCF-922D-7AD0F24CF464}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2685A566-DB95-4E15-8171-65879C83336D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5F191FF7-354F-40B6-A3D7-03C180CF32FA}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{A182F2C8-5A4E-4043-A3BB-EC4AB13D45DB}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5B1CAF69-597E-4BDD-BDD9-DEB0AA08F212}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C60CF08C-DF62-4987-B242-C26CE1E65FE0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{DDC8E95C-284C-44A0-8DB7-94A626C1989F}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{314661FD-C7BD-4CEC-9944-CC7670DE963C}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{E5428C22-A307-487C-8322-EC9149FB9B58}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{27363F78-1E91-4B69-A8C6-3F7820C5329A}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}