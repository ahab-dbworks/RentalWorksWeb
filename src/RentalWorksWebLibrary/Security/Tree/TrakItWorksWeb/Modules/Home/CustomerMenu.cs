using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class CustomerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerMenu() : base("{8237418B-923D-4044-951F-98938C1EC3DE}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7E3C641B-5075-4A33-BBFC-2242C582149E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3685D060-5586-4EBC-B7D2-A7AB947867EA}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{7097A4CB-11AC-4052-AF68-A944DAB887FA}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0D7D39D1-5671-4ED5-8474-B17BB39A704F}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{80A9AE92-8871-4965-8E6E-F7736C37BB85}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{DEED8F6A-D984-4EF7-BFFE-D475AD25AA78}",    nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0C980AAE-6085-40D9-B1B2-E180F1A01A4D}",   nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{216F738F-DEEB-44FA-AD78-56425FA3EFD2}",   nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4C2C8C91-D71A-42FB-8359-459E88410D68}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{00176471-D1BE-4E27-98CD-44E421CEE5B7}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5EB3A74D-2148-4643-9167-5E249F33113E}", nodeForm.Id);
            //tree.AddSubMenu("{9586974C-BD2D-4FA2-A487-32B673338876}",           nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D4A2B88E-510A-4777-BE76-9D865D1C827B}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}