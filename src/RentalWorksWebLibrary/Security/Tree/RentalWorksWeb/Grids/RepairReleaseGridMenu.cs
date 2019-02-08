using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RepairReleaseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairReleaseGridMenu() : base("{06BFFEEF-632D-4DBE-9DFC-E64309784D44}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{349CBF69-2196-4FB2-950C-7E95DD3FAC67}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{41351ED4-A8EB-46DA-B949-85A94C678078}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6D69B88C-3972-4F99-B90B-B48C31579ACC}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2E877B90-E992-4A01-B010-3614D016AECC}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}