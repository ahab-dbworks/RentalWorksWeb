using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POReturnBarCodeGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReturnBarCodeGridMenu() : base("{C25168A5-1741-4E77-83C9-CA52FBC2C794}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{081FCA2E-645E-4137-ACF9-BE686E74735E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3361399A-7D71-4907-86E5-D7B6741396C8}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{78B3D391-ACFC-48FD-A104-41BB20175888}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{13312C09-BB3E-4600-B713-5E5732BFA76C}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}