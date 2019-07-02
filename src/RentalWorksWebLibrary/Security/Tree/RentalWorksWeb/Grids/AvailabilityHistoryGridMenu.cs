using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class AvailabilityHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AvailabilityHistoryGridMenu() : base("{FA9450FE-2772-41BD-8FF0-03E7CF40097B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{13E0DE8D-906C-4236-8572-C22BC2E0CAF3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{A4988ACB-D9B7-4B86-A6B1-CEA961962CFF}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B4F049BC-ED1C-4148-BC1E-7CFB4E44C679}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2070AF28-F455-4BA8-A792-83233F1619C1}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
