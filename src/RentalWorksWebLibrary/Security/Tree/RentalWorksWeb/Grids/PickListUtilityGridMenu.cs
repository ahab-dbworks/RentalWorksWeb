using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PickListUtilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PickListUtilityGridMenu() : base("{0DAED562-2319-4569-AC4E-EF89198E54BC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0621362B-3FED-4B6C-B660-223A97C2FB0D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{9A96BC7E-9CE6-4E54-B061-A68489792B2A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2203D496-D239-4F25-B03E-F0C4E22A8B8B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{82AB7A78-FD43-4A93-AC40-2D9FBA57AEB6}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{0B5E9002-2972-47B8-8BEB-801E85AE1877}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}