using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeSourceMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeSourceMenu() : base("{6709D1A1-3319-435C-BF0E-15D2602575B0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{33EB0876-DE15-44CC-B66D-C2319F9A8825}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F2692287-767D-4DE1-9661-83FE3BB9157B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{378F15D5-B9A4-4323-83A4-05F98F790E1F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{AE867D95-5EF2-4F0F-BDCE-81CA3B510258}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{20073D8E-BF77-4535-951F-4C2CE39931D7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A080A042-662E-405F-BC3B-1CA566B7F3C0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{707E31F7-1203-4B3B-9C92-CF10880FA1D3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4174A0B7-357F-4C16-970E-A15E4A071F61}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{541A10BE-EA76-4C55-9A35-BFB65830C414}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{44823AA3-162D-4C8A-9BBC-6778ABD89A55}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{54088EBE-C8DA-4E4F-8C4B-C75C35F59681}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{0CD3D056-717E-40AD-AFAE-DA71787344A8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{5D1410EF-020D-417D-9065-E1951C4B29E7}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}