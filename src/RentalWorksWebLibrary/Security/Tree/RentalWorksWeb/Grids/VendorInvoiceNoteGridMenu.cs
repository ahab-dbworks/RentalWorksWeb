using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorInvoiceNoteGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoiceNoteGrid() : base("{D9DBA1D1-65E7-4CE5-99D0-6C79144DECAD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B1AB5F83-1023-451E-9A06-842209853EDA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{002EFF73-A80B-4777-9CBF-145D903351E4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A86AD906-A91B-4528-877B-554B1822FE43}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B304CB86-5C01-4B05-B147-A4E1189451A3}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{8FE89A0E-5DD4-4675-AB1F-A3119364EBD7}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{5418E906-2456-4495-A5A0-B466E0B0EF5D}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A3AEB649-CDF0-4A33-9456-3391435DDFE3}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
