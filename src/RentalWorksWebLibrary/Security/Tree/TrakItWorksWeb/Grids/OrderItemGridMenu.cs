using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class OrderItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public OrderItemGrid() : base("{E17AD193-28FB-4B92-BE62-B04AFC8C8A07}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar   = tree.AddMenuBar("{DD15BE1E-90D6-4680-8418-8C7800D65D91}", MODULEID);
            var nodeGridSubMenu   = tree.AddSubMenu("{8C7B3E82-59E0-44C9-AB05-663F9B8313B7}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{EF12D515-7930-4EDD-AFA1-2815D1F1E80D}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3896BEB3-A843-4699-A89E-E6361AB70345}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Summary View", "{87C47D00-E950-4724-8A8B-4528D0B41124}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Copy Template", "{87B6695A-2597-448A-99A0-970996470369}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Search", "{DD765DEF-DF9A-489C-A61D-9A2409B50CFA}", nodeBrowseOptions.Id, true);
            tree.AddSubMenuItem("Lock / Unlock Selected", "{6AEC9EA9-E2A0-4B80-A9BF-3784D82EB64C}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Bold / Unbold Selected", "{7C2C3F95-D939-4861-B75E-023EE00B6B7A}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Sub Worksheet", "{D31432DB-1343-4542-85B0-40EB6CAF5DE7}", nodeBrowseOptions.Id);
            //tree.AddSubMenuItem("Add Loss and Damage Items", "{FFF72FDF-85A4-4EB6-8FB4-3E4CE5857CF5}", nodeBrowseOptions.Id);        // mv 5/1/19 commented these out, because they are not implemented on the grid
            //tree.AddSubMenuItem("Retire Loss and Damage Items", "{29DECF73-E409-4B51-81B2-B9196B7EDE18}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Refresh Availability", "{BBC9E755-54D3-474A-ACBE-E99D4A8C568D}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{3E6F44A1-DC91-4065-A602-43FBFFFD9127}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F3C33FF2-E571-4CEF-AFFB-95DD7A2DF8C3}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{298C9806-6916-4180-A53F-F4559EF952AF}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}