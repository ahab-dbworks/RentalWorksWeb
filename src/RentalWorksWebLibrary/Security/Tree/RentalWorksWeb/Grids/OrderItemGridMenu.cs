using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public OrderItemGrid() : base("{C8A77000-43DD-4E49-A226-1E0DC4196F12}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D8762DD6-1603-46AB-ADBD-118D68BC63DA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{00944CE3-2EDF-4E27-B7FB-38ADBA46AC2E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1A932D18-69F0-44CA-80AE-F1F10D3AAB9F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AE2C816F-BF52-4DD5-A579-9A8B91DAC1BA}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Summary View", "{D27AD4E7-E924-47D1-AF6E-992B92F5A647}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Manual Sorting", "{AD3FB369-5A40-4984-8A65-46E683851E52}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Copy Template", "{B6B68464-B95C-4A4C-BAF2-6AA59B871468}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Search Inventory", "{77E511EC-5463-43A0-9C5D-B54407C97B15}", nodeBrowseOptions.Id, true);
            tree.AddSubMenuItem("Copy Line-Items", "{01EB96CB-6C62-4D5C-9224-8B6F45AD9F63}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Lock / Unlock Selected", "{BC467EF9-F255-4F51-A6F2-57276D8824A3}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Bold / Unbold Selected", "{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Sub Worksheet", "{007C4F21-7526-437C-AD1C-4BBB1030AABA}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Add Loss and Damage Items", "{427FCDFE-7E42-4081-A388-150D3D7FAE36}", nodeBrowseOptions.Id); 
            tree.AddSubMenuItem("Retire Loss and Damage Items", "{78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Refresh Availability", "{9476D532-5274-429C-A563-FE89F5B89B01}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{5A3352C6-F1D5-4A8C-BD75-045AF7B9988F}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{4113AFA5-29EA-4026-831E-685B904C5ADD}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{89AD5560-637A-4ECF-B7EA-33A462F6B137}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}