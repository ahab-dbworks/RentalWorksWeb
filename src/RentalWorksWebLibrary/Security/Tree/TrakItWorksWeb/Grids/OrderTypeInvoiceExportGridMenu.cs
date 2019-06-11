using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderTypeInvoiceExportGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeInvoiceExportGridMenu() : base("{3DE92A84-B28E-4E99-9402-7F0E07336D02}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{282AE94E-A4B8-4BF5-9B04-9C39EA321A7E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B48A2FC4-9FA2-407E-AA76-A5B8045EE4A8}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{0ECAC25E-3358-42FF-99DD-116DF191CC42}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3C4C7075-5993-4243-998C-E790089ABBA3}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{918AF9B2-085E-41C8-99AF-BF8BC33FD563}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}