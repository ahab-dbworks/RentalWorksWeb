var Constants = {
    isTrakitWorks: false,
    isRentalWorks: true,
    appId: '0A5F2584-D239-480F-8312-7C2B552A30BA',
    appCaption: 'RentalWorks',
    //appTitle: 'Rental<span class="rwpurple">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span>',
    appTitle: 'Rental<span class="rwpurple">Works</span>',
    MainMenu: {
        Reports: { id: 'Reports' },
        Settings: { id: 'Settings' }
    },
    Modules: {
        Administrator: {
            id: 'Administrator',
            caption: 'Administrator',
            nodetype: 'Category',
            children: {
                Alert:              { id: 'gFfpaR5mDAzX',  caption: 'Alert',          nav: 'module/alert', nodetype: 'Module' },
                CustomField:        { id: 'cZHPJQyBxolS',  caption: 'Custom Field',   nav: 'module/customfield', nodetype: 'Module' },
                CustomForm:         { id: '11txpzVKVGi2',  caption: 'Custom Form',    nav: 'module/customform', nodetype: 'Module' },
                CustomReportLayout: { id: 'EtrF5NHJ7dRg6', caption: 'Custom Report Layout', nav: 'module/customreportlayout', nodetype: 'Module' },
                DuplicateRule:      { id: 'v7oBspDLjli8',  caption: 'Duplicate Rule', nav: 'module/duplicaterule', nodetype: 'Module' },
                EmailHistory:       { id: '3XHEm3Q8WSD8z', caption: 'Email History',  nav: 'module/emailhistory', nodetype: 'Module' },
                Group:              { id: '0vP4rXxgGL1M',  caption: 'Group',          nav: 'module/group', nodetype: 'Module' },
                Hotfix:             { id: 'yeqaGIUYfYNX',  caption: 'Hotfix',         nav: 'module/hotfix', nodetype: 'Module' },
                Reports:            { id: '3C5C7603-9E7B-47AB-A722-B29CA09B3B8C', caption: 'Reports', nav: 'module/reports', nodetype: 'Module' },
                Settings:           { id: '57150967-486A-42DE-978D-A2B0F843341A', caption: 'Settings', nav: 'module/settings', nodetype: 'Module' },
                User:               { id: 'r1fKvn1KaFd0u', caption: 'User',           nav: 'module/user', nodetype: 'Module' }
            }
        },
        Agent: {
            id: 'Agent',
            caption: 'Agent',
            nodetype: 'Category',
            children: {
                Contact: { id: '9ykTwUXTet46', caption: 'Contact', nav: 'module/contact', nodetype: 'Module' },
                Customer: { id: 'InSfo1f2lbFV', caption: 'Customer', nav: 'module/customer', nodetype: 'Module' },
                Deal: { id: '8WdRib388fFF', caption: 'Deal', nav: 'module/deal', nodetype: 'Module' },
                Order: { id: 'U8Zlahz3ke9i', caption: 'Order', nav: 'module/order', nodetype: 'Module' },
                Project: { id: 'k7bYJRoHkf9Jr', caption: 'Project', nav: 'module/project', nodetype: 'Module' },
                PurchaseOrder: { id: '9a0xOMvBM7Uh9', caption: 'Purchase Order', nav: 'module/purchaseorder', nodetype: 'Module' },
                Quote: { id: 'jFkSBEur1dluU', caption: 'Quote', nav: 'module/quote', nodetype: 'Module' },
                Vendor: { id: 'cwytGLEcUzJdn', caption: 'Vendor', nav: 'module/vendor', nodetype: 'Module' }
            }
        },
        Billing: {
            id: 'Billing',
            caption: 'Billing',
            nodetype: 'Category',
            children: {
                Billing: { id: '67cZ8IUbw53c', caption: 'Billing', nav: 'module/billing', nodetype: 'Module' },
                BillingMessage: { id: 'U0HFTNmYWt3a7', caption: 'Billing Message', nav: 'module/billingmessage', nodetype: 'Module' },
                BillingWorksheet: { id: '2BTZbIXJy4tdI', caption: 'Billing Worksheet', nav: 'module/billingworksheet', nodetype: 'Module' },
                Invoice: { id: 'cZ9Z8aGEiDDw', caption: 'Invoice', nav: 'module/invoice', nodetype: 'Module' },
                Receipt: { id: 'q4PPGLusbFw', caption: 'Receipts', nav: 'module/receipt', nodetype: 'Module' },
                VendorInvoice: { id: 'Fq9aOe0yWfY', caption: 'Vendor Invoice', nav: 'module/vendorinvoice', nodetype: 'Module' },
            }
        },
        Container: {
            id: 'Container',
            caption: 'Container',
            nodetype: 'Category',
            children: {
                Container:              { id: 'bSQsBVDvo86X1', caption: 'Container',                nav: 'module/container', nodetype: 'Module' },
                ContainerStatus:        { id: 'AQCjfAUtfHy5',  caption: 'Container Status',         nav: 'module/containerstatus', nodetype: 'Module' },
                FillContainer:          { id: '4D6pES9WoHVJ',  caption: 'Fill Container',           nav: 'module/fillcontainer', nodetype: 'Module' },
                EmptyContainer:         { id: 'tfzkGtEIPTFr',  caption: 'Empty Container',          nav: 'module/emptycontainer', nodetype: 'Module' },
                RemoveFromContainer:    { id: 'J9BTE3hOYuEd',  caption: 'Remove From Container',    nav: 'module/removefromcontainer', nodetype: 'Module' }
            }
        },
        Home: {
            children: {
                CountQuantityInventory: { id: '', caption: 'Count Quantity Inventory', nav: 'module/physicalinventoryquantityinventory', nodetype: 'Module' },
                CreatePickList: { id: '', caption: 'Create Pick List', nav: 'module/contract', nodetype: 'Module' },
                CustomerCredit: { id: 'DCPFcfKgUGnuC', caption: 'Customer Credit', nav: 'module/customercredit', nodetype: 'Module' },
                DealCredit: { id: 'OCkLGwclipEA', caption: 'Deal Credit', nav: 'module/dealcredit', nodetype: 'Module' },
                //Manifest: {
                //    id: 'yMwoSvKvwAbbZ', caption: 'Transfer Manifest', nav: 'module/manifest', nodetype: 'Module',
                //    form: {
                //        menuItems: {
                //            Print: { id: '{8FC8A0F2-C016-476F-971B-64CF2ED95E41}' }
                //        }
                //    }
                //},
                SubWorksheet: { id: '', caption: 'Sub Worksheet', nav: 'module/subworksheet', nodetype: 'Module' },
                SuspendedSession: { id: '', caption: 'Suspended Session', nav: 'module/suspendedsession', nodetype: 'Module' }
            }
        },
        Inventory: {
            id: 'Inventory',
            caption: 'Inventory',
            nodetype: 'Category',
            children: {
                Asset:                    { id: 'kSugPLvkuNsH',  caption: 'Asset',                  nav: 'module/item', nodetype: 'Module' },
                AvailabilityConflicts:    { id: '2xsgUfsXeKJH',  caption: 'Availability Conflicts', nav: 'module/availabilityconflicts', nodetype: 'Module' },
                CompleteQc:               { id: 'VwNYsEONLutM',  caption: 'Complete QC',            nav: 'module/completeqc', nodetype: 'Module' },
                PartsInventory:           { id: '2WDCohbQV6GU',  caption: 'Parts Inventory',        nav: 'module/partsinventory', nodetype: 'Module' },
                PurchaseHistory:          { id: '8XKYiQYXj9BKK', caption: 'Purchase History',       nav: 'module/purchasehistory', nodetype: 'Module' },
                PhysicalInventory:        { id: 'JIuxFUWTLDC6',  caption: 'Physical Inventory',     nav: 'module/physicalinventory', nodetype: 'Module' },
                RentalInventory:          { id: '3ICuf6pSeBh6G', caption: 'Rental Inventory',       nav: 'module/rentalinventory', nodetype: 'Module' },
                Repair:                   { id: 't4gfyzLkSZhyc', caption: 'Repair Order',           nav: 'module/repair', nodetype: 'Module' },
                SalesInventory:           { id: 'ShjGAzM2Pq3kk', caption: 'Sales Inventory',        nav: 'module/salesinventory', nodetype: 'Module' }
            }
        },
        Reports: {
            id: 'Reports',
            caption: 'Reports',
            nodetype: 'Category',
            children: {
                AccountingReports: {
                    id: 'Reports.AccountingReports',
                    caption: 'Accounting Reports',
                    nodetype: 'Category',
                    children: {
                        ArAgingReport: {                    id: 'KHw5yX5TubQ', caption: 'A/R Aging', nav: 'reports/aragingreport', nodetype: 'Module', description: 'List unpaid Invoices, and their corresponding aging totals.  Report is subtotalled by Deal and Customer.' },
                        DailyReceiptsReport: {              id: 'OLyFIS7rBvr8', caption: 'Daily Receipts', nav: 'reports/dailyreceiptsreport', nodetype: 'Module', description: 'List Daily Receipts.  Report is subtotalled by Deal and Customer.' },
                        GlDistributionReport: {             id: 'ClMQ5QkZq4PY', caption: 'G/L Distribution', nav: 'reports/gldistributionreport', nodetype: 'Module', description: 'Summarize transaction totals by Account over a date range.' }
                    }
                },
                BillingReports: {
                    id: 'Reports.Billing',
                    caption: 'Billing Reports',
                    nodetype: 'Category',
                    children: {
                        AgentBillingReport: {               id: 'qx65UNbCoUW', caption: 'Agent Billing', nav: 'reports/agentbillingreport', nodetype: 'Module', description: 'Shows Invoice Activity Totals, subtotalled by Agent.' },
                        BillingAnalysisReport: {            id: 'c2AwOP9UmJFw', caption: 'Billing Analysis', nav: 'reports/billinganalysisreport', nodetype: 'Module', description: 'List all Orders created within a specified date range, showing Order Total vs. Billed Total and Estimated Cost Total vs. Vendor Invoice Total.' },
                        BillingProgressReport: {            id: 'Y5ssGcXnxL2', caption: 'Billing Progress', nav: 'reports/billingprogressreport', nodetype: 'Module', description: 'List all Orders and their percentage of total Billing, subtotalled by Customer and Deal.' },
                        BillingStatementReport: {           id: 'wd7t1jPI4ztQH', caption: 'Billing Statement', nav: 'reports/billingstatementreport', nodetype: 'Module', description: 'Create a printable Billing Statement which itemizes all Billing and Receipt activity per Deal over a specific date range.' },
                        CreateInvoiceProcessReport: {       id: 'qhb1dkFRrS6T', caption: 'Create Invoice Process', nav: 'reports/createinvoiceprocessreport', nodetype: 'Module', description: 'List all Invoices and Exceptions based on a Creation Batch.' },
                        InvoiceDiscountReport: {            id: 'PwAjD9UxITIp', caption: 'Invoice Discount', nav: 'reports/invoicediscountreport', nodetype: 'Module', description: 'List all Invoices which have a Discount.' },
                        InvoiceReport: {                    id: 'o5nbWmTr7xy0n', caption: 'Invoices', nav: 'reports/invoicereport', nodetype: 'Module', description: 'Invoice document.' },
                        InvoiceSummaryReport: {             id: 'LeLwkS6yUBfV', caption: 'Invoice Summary', nav: 'reports/invoicesummaryreport', nodetype: 'Module', description: 'List all Invoices for a specific date range, subtotalled by Customer and Deal.' },
                        ProfitLossReport: {                 id: 'gUCH9E3ZYdRIm', caption: 'Profit and Loss', nav: 'reports/profitlossreport', nodetype: 'Module', description: 'List Profit or Loss for all Orders that start within a specified date range.' },
                        ProjectManagerBillingReport: {      id: 'lTDdAGi63jRVL', caption: 'Project Manager Billing', nav: 'reports/projectmanagerbillingreport', nodetype: 'Module', description: 'Shows Invoice Activity Totals, subtotalled by Project Manager.' },
                        SalesQuoteBillingReport: {          id: 'L8WwjlirkzC55', caption: 'Sales Quote Billing Analysis', nav: 'reports/salesquotebillingreport', nodetype: 'Module', description: 'List Profitability of Sales Quotes.' },
                        SalesRepresentativeBillingReport: { id: 'SgF8AMJVjKARN', caption: 'Sales Representative Billing', nav: 'reports/salesrepresentativebillingreport', nodetype: 'Module', description: 'Shows Invoice Activity Totals, subtotalled by Sales Representative.' },
                        SalesTaxCanadaReport: {             id: 'UX8GjEXNteJz', caption: 'Sales Tax - Canada', nav: 'reports/salestaxcanadareport', nodetype: 'Module', description: 'List all Invoices for a specific date range, grouped and totaled by Tax Option' },
                        SalesTaxUSAReport: {                id: 'TaaSgS14rWsCL', caption: 'Sales Tax - USA', nav: 'reports/salestaxusareport', nodetype: 'Module', description: 'List all Invoices for a specific date range, grouped and totaled by Tax Option' }
                    }
                },
                ChangeAuditReports: {
                    id: 'Reports.ChangeAuditReports',
                    caption: 'Change Audit Reports',
                    nodetype: 'Category',
                    children: {
                        ChangeAuditReport: {                id: 'hfM1GHEQCWBJ', caption: 'Change Audit', nav: 'reports/changeauditreport', nodetype: 'Module', description: 'List all data changes made to any module by any user over a specific date range.' }
                    }
                },
                ChargeProcessingReports: {
                    id: 'Reports.ChargeProcessingReports',
                    caption: 'Charge Processing Reports',
                    nodetype: 'Category',
                    children: {
                        DealInvoiceBatchReport: {           id: 't3byQWNa3hZ4H', caption: 'Deal Invoice Batch', nav: 'reports/dealinvoicebatchreport', nodetype: 'Module', description: 'List all Invoices processed in a selected Batch.' },
                        ReceiptBatchReport: {               id: 'jB7p9OvmCibhx', caption: 'Receipt Batch', nav: 'reports/receiptbatchreport', nodetype: 'Module', description: 'List all Receipts processed in a selected Batch.' },
                        VendorInvoiceBatchReport: {         id: '6gc3QbSGD5BX', caption: 'Vendor Invoice Batch', nav: 'reports/vendorinvoicebatchreport', nodetype: 'Module', description: 'List all Vendor Invoices processed in a selected Batch.' }
                    }
                },
                ContractReports: {
                    id: 'Reports.ContractReports',
                    caption: 'Contract Reports',
                    nodetype: 'Category',
                    children: {
                        ContractRevisionReport: {           id: 'ZDRzzkgcqTb57', caption: 'Contract Revision Activity', nav: 'reports/contractrevisionreport', nodetype: 'Module', description: 'List all modifications made to Contracts over a specified date range.' },
                        OutContractReport: {                id: 'p2tH0HSDc930', caption: 'Out Contract', nav: 'reports/outcontractreport', nodetype: 'Module', description: 'Check-Out Contract document.' }
                    }
                },
                CrewReports: {
                    id: 'Reports.CrewReports',
                    caption: 'Crew Reports',
                    nodetype: 'Category',
                    children: {
                        CrewSignInReport: {                 id: 'kvDH1delOiUT', caption: 'Crew Sign-In', nav: 'reports/crewsigninreport', nodetype: 'Module', description: 'Lists the Sign In and Out activity for Crew members over a specific date range.' }
                    }
                },
                DealReports: {
                    id: 'Reports.DealReports',
                    caption: 'Deal Reports',
                    nodetype: 'Category',
                    children: {
                        CreditsOnAccountReport: {           id: '9JCPjgemNM8D', caption: 'Credits On Account', nav: 'reports/creditsonaccountreport', nodetype: 'Module', description: 'List each Deal that has an outstanding Credit Memo, Depleting Deposit, or Overpayment.' },
                        CustomerRevenueByMonthReport: {     id: '40SdfVGkZPtA6', caption: 'Customer Revenue By Month', nav: 'reports/customerrevenuebymonthreport', nodetype: 'Module', description: 'List all revenue per Customer, per Deal, per Month.' },
                        CustomerRevenueByTypeReport: {      id: 'mIieqY1nHrJP', caption: 'Customer Revenue By Type', nav: 'reports/customerrevenuebytypereport', nodetype: 'Module', description: 'List each Invoice for a specific date range.  Revenue amounts are broken down by Activity Type (ie. Rentals, Sales, etc).  Revenue is subtotalled by Customer and Deal.' },
                        DealInvoiceDetailReport: {          id: 'u5mTLIfmUKWfH', caption: 'Deal Invoice Detail', nav: 'reports/dealinvoicedetailreport', nodetype: 'Module', description: 'List each Invoice for a specific date range, grouped by Deal and Order.  Review revenue broken down by Activity Type (ie. Rental, Sales, etc).  Analyze profits net of Sub Vendor Costs.' },
                        DealOutstandingItemsReport: {       id: 'i5RTw0gXIWhU', caption: 'Deal Outstanding Items', nav: 'reports/dealoutstandingitemsreport', nodetype: 'Module', description: 'List all items still Checked-Out to a specific Deal.' },
                        OrdersByDealReport: {               id: 'ltXuVM54H5dYe', caption: 'Orders By Deal', nav: 'reports/ordersbydealreport', nodetype: 'Module', description: 'List all Orders for specific date ranges, grouped by Deal.' },
                        ReturnReceiptReport: {              id: 'N9wTkNBeCgJQz', caption: 'Return Receipt', nav: 'reports/returnreceiptreport', nodetype: 'Module', description: 'List all inventory checked-in without an Order (ie. Return Receipt) including all reconciliations.' }
                    }
                },
                MultiLocationReports: {
                    id: 'Reports.MultiLocationReports',
                    caption: 'Multi Location Reports',
                    nodetype: 'Category',
                    children: {
                        TransferReport: {                   id: 'XerdM2ZaD51uw', caption: 'Transfer Report', nav: 'reports/transferreport', nodetype: 'Module', description: 'List all Transfer Orders as well as transferred Inventory over a specific date range.' }
                    }
                },
                OrderReports: {
                    id: 'Reports.OrderReports',
                    caption: 'Order Reports',
                    nodetype: 'Category',
                    children: {
                        LateReturnsReport: {                id: 'gOtEnqxlXIOt', caption: 'Late Return / Due Back', nav: 'reports/latereturnsreport', nodetype: 'Module', description: 'List all items that are Late or Due Back on a specific date.  Data is subtotalled by Order and Deal.' },
                        OrderConflictReport: {              id: 'kXV9ZCJogLiwe', caption: 'Availability Item Conflict', nav: 'reports/orderconflictreport', nodetype: 'Module', description: 'List all Negative and Positive Availability Conflicts over a specified date range.' },
                        OrderReport: {                      id: 'Q89Ni6FvVL92', caption: 'Print Order', nav: 'reports/orderreport', nodetype: 'Module', description: 'Order document.' },
                        OutstandingSubRentalReport: {       id: 'NCFNATdQRx5E', caption: 'Outstanding Sub-Rental', nav: 'reports/outstandingsubrentalreport', nodetype: 'Module', description: 'List all Sub-Rental items currently Staged, Out, or in Holding.' },
                        PickListReport: {                   id: 'Rk38wHmvgXTg', caption: 'Pick List', nav: 'reports/picklistreport', nodetype: 'Module', description: 'Pick List document.' },
                        QuikActivityReport: {               id: '4hamhMOWKXD9', caption: 'QuikActivity', nav: 'reports/quikactivityreport', nodetype: 'Module', description: 'List all Quote, Order, Transfer, Purchase, and Repair activities over a specific date range.' },
                        QuoteReport: {                      id: 'SZ80uvR5NjI7', caption: 'Print Quote', nodetype: 'Module', nav: 'reports/quotereport', description: 'Quote document.' },
                        QuoteOrderMasterReport: {           id: 'yx1quQL9wJ9mg', caption: 'Quote / Order Master Report', nav: 'reports/quoteordermasterreport', nodetype: 'Module', description: 'List all Quotes and Orders, filtered by date range, and grouped by Deal.' },
                        SubSalesStagedItemsReport: {        id: '2GIJvJlbIFQN', caption: 'Sub-Sales Staged Items', nav: 'reports/subsalesstageditemsreport', nodetype: 'Module', description: 'List all Sub Sale Inventory currently Staged on Orders.' }
                    }
                },
                PartsInventoryReports: {
                    id: 'Reports.PartsInventoryReports',
                    caption: 'Parts Inventory Reports',
                    nodetype: 'Category',
                    children: {
                        PartsInventoryAttributesReport: {           id: 'qyi6CvOObXr', caption: 'Parts Inventory Attributes', nav: 'reports/partsinventoryattributesreport', nodetype: 'Module', description: 'List all Parts Inventory with their Attributes and Values.' },
                        PartsInventoryCatalogReport: {              id: '5KHdCAEfEbEVo', caption: 'Parts Inventory Catalog', nav: 'reports/partsinventorycatalogreport', nodetype: 'Module', description: 'List all Parts Inventory, current Rates, and Quantity on Hand.' },
                        PartsInventoryPurchaseHistoryReport: {      id: 's3JHeEZYfEu', caption: 'Parts Inventory Purchase History', nav: 'reports/partsinventorypurchasehistoryreport', nodetype: 'Module', description: 'List all Parts Inventory Purchase History.' },
                        PartsInventoryReorderReport: {              id: '2KtGO1TfYVe', caption: 'Parts Inventory Reorder', nav: 'reports/partsinventoryreorderreport', nodetype: 'Module', description: 'List all Parts Inventory to reorder. Filter items with quantity levels at or below their Reorder Point.' },
                        PartsInventoryTransactionReport: {          id: 'p4MdfH1e38wLE', caption: 'Parts Inventory Transactions', nav: 'reports/partsinventorytransactionreport', nodetype: 'Module', description: 'List all Parts Inventory Transactions, including Cost and Price, over a specific date range.' }
                    }
                },
                RentalInventoryReports: {
                    id: 'Reports.RentalInventoryReports',
                    caption: 'Rental Inventory Reports',
                    nodetype: 'Category',
                    children: {
                        RentalInventoryActivityByDateReport: {      id: 'nh7dhdTKT9U0Q', caption: 'Rental Inventory Activity By Date', nav: 'reports/rentalinventoryactivitybydatereport', nodetype: 'Module', description: 'List all Rental Inventory Out and In Activity over a specific date range.' },
                        RentalInventoryAttributesReport: {          id: 'CgvKJwiD2ew',   caption: 'Rental Inventory Attributes', nav: 'reports/rentalinventoryattributesreport', nodetype: 'Module', description: 'List all Rental Inventory with their Attributes and Values.' },
                        RentalInventoryAvailabilityReport: {        id: 'LQj6R2GQBfLfS', caption: 'Rental Inventory Availability', nav: 'reports/rentalinventoryavailabilityreport', nodetype: 'Module', description: 'List all Rental Inventory with projected Availability for up to 30 days.' },
                        RentalInventoryCatalogReport: {             id: '9cNlsCNNeCPV',  caption: 'Rental Inventory Catalog', nav: 'reports/rentalinventorycatalogreport', nodetype: 'Module', description: 'List all Rental Inventory, current Rates, Replacement Cost, and Owned Quantity.' },
                        RentalInventoryChangeReport: {              id: '1hUsFp31mUCV',  caption: 'Rental Inventory Change', nav: 'reports/rentalinventorychangereport', nodetype: 'Module', description: 'List all changes to Rental Inventory quantities over a date range.' },
                        RentalInventoryMasterReport: {              id: 'hVovGdrneLrzq', caption: 'Rental Inventory Master', nav: 'reports/rentalinventorymasterreport', nodetype: 'Module', description: 'List all Rental Inventory with their Quantities, Status, Cost, and Value.' },
                        RentalInventoryMovementReport: {            id: '3fjXnYy5gTPR',  caption: 'Rental Inventory Movement', nav: 'reports/rentalinventorymovementreport', nodetype: 'Module', description: 'List all Rental Inventory with changes in quantity over a date range.' },
                        RentalInventoryPurchaseHistoryReport: {     id: 'kI5HgFqlPzr',   caption: 'Rental Inventory Purchase History', nav: 'reports/rentalinventorypurchasehistoryreport', nodetype: 'Module', description: 'List all Rental Inventory Purchase History.' },
                        RentalInventoryQCRequiredReport: {          id: 'LzwbAbB86Hlvs', caption: 'Rental Inventory QC Required', nav: 'reports/rentalinventoryqcrequiredreport', nodetype: 'Module', description: 'List all Rental Inventory that require QC.' },
                        RentalInventoryStatusAndRevenueReport: {    id: 'uCEqKxMHdcT',   caption: 'Rental Inventory Status and Revenue', nav: 'reports/rentalinventorystatusandrevenuereport', nodetype: 'Module', description: 'List all Rental Inventory including current status counts and revenue over a given date range.' },
                        RentalInventoryUnusedItemsReport: {         id: 'Xb4w7HOWs9vww', caption: 'Rental Inventory Unused Items', nav: 'reports/rentalinventoryunuseditemsreport', nodetype: 'Module', description: 'List all Rental Inventory that has not been rented for the the specified number of days.' },
                        RentalInventoryUsageReport: {               id: 'BplzDmql7vG48', caption: 'Rental Inventory Usage', nav: 'reports/rentalinventoryusagereport', nodetype: 'Module', description: 'List all Rental Inventory and Usage, with owned revenue and sub-rental revenue.' },
                        RentalInventoryValueReport: {               id: 'UZkDL1Yyby6kN', caption: 'Rental Inventory Value', nav: 'reports/rentalinventoryvaluereport', nodetype: 'Module', description: 'List all Rental Inventory and get a current value, historical value, or change in value over a date range.' },
                        RentalLostAndDamagedBillingHistoryReport: { id: '37O4QUBc8nM8t', caption: 'Rental Lost And Damaged Billing History', nav: 'reports/rentallostanddamagedbillinghistoryreport', nodetype: 'Module', description: 'List all Billing for Lost and Damaged inventory.' },
                        RetiredRentalInventoryReport: {             id: 'L8qgvB6nFhAV',  caption: 'Retired Rental Inventory', nav: 'reports/retiredrentalinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory Retired during a specified date range.' },
                        ReturnedToInventoryReport: {                id: 'jXgzMRhDOFMY',  caption: 'Returned To Inventory', nav: 'reports/returnedtoinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory Returned during a specified date range.' },
                        ReturnOnAssetReport: {                      id: '15TIjoDzY09G',  caption: 'Return On Asset', nav: 'reports/returnonassetreport', nodetype: 'Module', description: 'Calculate the Revenue, Value, Utilization, and Return on Asset for all Rental Inventory for various date ranges.' },
                        UnretiredRentalInventoryReport: {           id: 'UaCdIoXMNFyiT', caption: 'Unretired Rental Inventory', nav: 'reports/unretiredrentalinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory unretired during a specified date range.' },
                        ValueOfOutRentalInventoryReport: {          id: 'n4gdQ6awebnX',  caption: 'Value Of Out Rental Inventory', nav: 'reports/valueofoutrentalinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory Out on a specific date, including Cost and Value for each.' },
                    }
                },
                RepairOrderReports: {
                    id: 'Reports.RepairOrderReports',
                    caption: 'Repair Order Reports',
                    nodetype: 'Category',
                    children: {
                        RentalEquipmentRepairHistoryReport: { id: 'bLX4YBftwJvw', caption: 'Rental Equipment Repair History', nav: 'reports/rentalequipmentrepairhistoryreport', nodetype: 'Module', description: 'List all Rental Inventory that has been In Repair over a specified date range.' },
                        RepairOrderStatusReport: {            id: 'KFP6Nq17ZuDPO', caption: 'Repair Order Status', nav: 'reports/repairorderstatusreport', nodetype: 'Module', description: 'List all Repair Orders, including Damage Notes.  Filter by Status, Type, Priority, Days In Repair, and more.' }
                    }
                },
                SalesInventoryReports: {
                    id: 'Reports.SalesInventoryReports',
                    caption: 'Sales Inventory Reports',
                    nodetype: 'Category',
                    children: {
                        SalesBackorderReport: {                id: 'KMdFiy9SEIQEU', caption: 'Sales Inventory Backorder', nav: 'reports/salesbackorderreport', nodetype: 'Module', description: 'List all Orders with Sales Inventory partially checked-out and items still on backorder.' },
                        SalesHistoryReport: {                  id: '0qFfabkzl5Vi', caption: 'Sales History', nav: 'reports/saleshistoryreport', nodetype: 'Module', description: 'List all Sales over a specific date range.' },
                        SalesInventoryAttributesReport: {      id: 'IKtnIOnt3tS', caption: 'Sales Inventory Attributes', nav: 'reports/salesinventoryattributesreport', nodetype: 'Module', description: 'List all Sales Inventory with their Attributes and Values.' },
                        SalesInventoryCatalogReport: {         id: 'psk5v7y4gImL', caption: 'Sales Inventory Catalog', nav: 'reports/salesinventorycatalogreport', nodetype: 'Module', description: 'List all Sales Inventory, current Rates, and Quantity on Hand.' },
                        SalesInventoryMasterReport: {          id: 'iZlMgIbbp6iK2', caption: 'Sales Inventory Master', nav: 'reports/salesinventorymasterreport', nodetype: 'Module', description: 'List all Sales Inventory with their Quantities, Costs, and Revenue Totals.' },
                        SalesInventoryPurchaseHistoryReport: { id: 'mSjHvRjNvI0', caption: 'Sales Inventory Purchase History', nav: 'reports/salesinventorypurchasehistoryreport', nodetype: 'Module', description: 'List all Sales Inventory Purchase History.' },
                        SalesInventoryReorderReport: {         id: 'h1ag1lcZCgd', caption: 'Sales Inventory Reorder', nav: 'reports/salesinventoryreorderreport', nodetype: 'Module', description: 'List all Sales Inventory to reorder. Filter items with quantity levels at or below their Reorder Point.' },
                        SalesInventoryTransactionReport: {     id: 'uUxuUabbhO7ah', caption: 'Sales Inventory Transactions', nav: 'reports/salesinventorytransactionreport', nodetype: 'Module', description: 'List all Sales Inventory Transactions, including Cost and Price, over a specific date range.' }
                    }
                },
                VendorReports: {
                    id: 'Reports.VendorReports',
                    caption: 'Vendor Reports',
                    nodetype: 'Category',
                    children: {
                        PurchaseOrderMasterReport: {        id: 'zJJl8Frw1U7U7', caption: 'Purchase Order Master', nav: 'reports/purchaseordermasterreport', nodetype: 'Module', description: 'List all Purchase Orders for a specified date range.' },
                        PurchaseOrderReport: {              id: 'yrvMp4sG8CzF', caption: 'Purchase Order', nav: 'reports/purchaseorderreport', nodetype: 'Module', description: 'List all Purchase Orders for a specified date range.' },
                        SubItemStatusReport: {              id: 'fL9dlJfzzJf8U', caption: 'Sub Item Status', nav: 'reports/subitemstatusreport', nodetype: 'Module', description: 'List all Sub-Rentals, Sub-Sales, Sub-Misc, and Sub-Labor over a specified date range. Evaluate profitability of each.' },
                        SubRentalBillingAnalysisReport: {   id: 'KIE1O1i2tvtsu', caption: 'Sub-Rental Billing Analysis', nav: 'reports/subrentalbillinganalysisreport', nodetype: 'Module', description: 'List all Sub-Rental Billing activity and compare Deal billing amounts with Vendor billing amounts.' },
                        VendorInvoiceSummaryReport: {       id: 'J2Lczm4sL14Ze', caption: 'Vendor Invoice Summary', nav: 'reports/vendorinvoicesummaryreport', nodetype: 'Module', description: 'List all Invoices for a specific date range, subtotalled by Purchase Order and Deal.' }
                    }
                }
            }
        },
        Settings: {
            id: 'Settings',
            caption: 'Settings',
            nodetype: 'Category',
            children: {
                AccountingSettings: {
                    caption: 'Accounting',
                    id: 'Settings.AccountingSettings',
                    nodetype: 'Category',
                    children: {
                        AccountingSettings: { id: 'Xxp77cNVPq29d', caption: 'Accounting Settings', nav: 'settings/accountingsettings/accountingsettings', nodetype: 'Module', description: '' },
                        GlAccount: { id: '1bUgvfRlo7v4', caption: 'Chart Of Accounts', nav: 'settings/accountingsettings/chartofaccounts', nodetype: 'Module', description: 'Asset, Income, Liability, and Expense Accounts for tracking revenue and expenses.' },
                        GlDistribution: { id: '71QUDQyIbibs', caption: 'G/L Distribution', nav: 'settings/accountingsettings/gldistribution', nodetype: 'Module', description: 'Accounts to use for Accounts Receivable, Receipts, Payables, etc.' }
                    }
                },
                AddressSettings: {
                    caption: 'Address',
                    id: 'Settings.AddressSettings',
                    nodetype: 'Category',
                    children: {
                        Country: { id: 'FV8c2ibthqUF', caption: 'Country', nav: 'settings/addresssettings/country', nodetype: 'Module', description: 'List Countries to relate to your Customers, Deals, and Vendors' },
                        State: { id: 'JW3yCGldGTAqC', caption: 'State / Province', nav: 'settings/addresssettings/state', nodetype: 'Module', description: 'List States to relate to your Customers, Deals, and Vendors' }
                    }
                },
                BillingCycleSettings: {
                    caption: 'Billing Cycle',
                    id: 'Settings.BillingCycleSettings',
                    nodetype: 'Category',
                    children: {
                        BillingCycle: { id: 'FQfXEIcN9q3', caption: 'Billing Cycle', nav: 'settings/billingcyclesettings/billingcycle', nodetype: 'Module', description: 'Define and configure Billing Cycles for your Quotes and Orders' }
                    }
                },
                CompanyDepartmentSettings: {
                    caption: 'Company Department',
                    id: 'Settings.CompanyDepartmentSettings',
                    nodetype: 'Category',
                    children: {
                        Department: { id: 'kuYqT9d6TDEg', caption: 'Company Department', nav: 'settings/companydepartmentsettings/companydepartment', nodetype: 'Module', description: '' }
                    }
                },
                ContactSettings: {
                    caption: 'Contact',
                    id: 'Settings.ContactSettings',
                    nodetype: 'Category',
                    children: {
                        ContactEvent: { id: 'KdvGpc1dQINo', caption: 'Contact Event', nav: 'settings/contactsettings/contactevent', nodetype: 'Module', description: '' },
                        ContactTitle: { id: 'PClZ3w0VUnPt', caption: 'Contact Title', nav: 'settings/contactsettings/contacttitle', description: '' },
                        MailList: { id: 'vUT6JZ1Owu5n', caption: 'Mail List', nav: 'settings/contactsettings/maillist', nodetype: 'Module', description: '' }
                    }
                },
                CurrencySettings: {
                    caption: 'Currency',
                    id: 'Settings.CurrencySettings',
                    nodetype: 'Category',
                    children: {
                        Currency: { id: 'xpyZJmmju0uB', caption: 'Currency', nav: 'settings/currencysettings/currency', nodetype: 'Module', description: '' }
                    }
                },
                CustomerSettings: {
                    caption: 'Customer',
                    id: 'Settings.CustomerSettings',
                    nodetype: 'Category',
                    children: {
                        CreditStatus: { id: 'A4P8o1quoutj', caption: 'Credit Status', nav: 'settings/currencysettings/creditstatus', nodetype: 'Module', description: '' },
                        CustomerCategory: { id: 'HC4q49WUI1NW', caption: 'Customer Category', nav: 'settings/currencysettings/customercategory', nodetype: 'Module', description: '' },
                        CustomerStatus: { id: 'ZbZ8bywECnnE', caption: 'Customer Status', nav: 'settings/currencysettings/customerstatus', nodetype: 'Module', description: '' },
                        CustomerType: { id: 'gk8NipmJErWZ', caption: 'Customer Type', nav: 'settings/currencysettings/customertype', nodetype: 'Module', description: '' }
                    }
                },
                DealSettings: {
                    caption: 'Deal',
                    id: 'SetSettings.DealSettings',
                    nodetype: 'Category',
                    children: {
                        DealClassification: { id: 'uRRVPMAFf61J', caption: 'Deal Classification', nav: 'settings/currencysettings/dealclassification', nodetype: 'Module', description: '' },
                        DealStatus: { id: 'CHOTGdFVlnFK', caption: 'Deal Status', nav: 'settings/currencysettings/dealstatus', nodetype: 'Module', description: '' },
                        DealType: { id: 'jZCS1X5BzeyS', caption: 'DealType', nav: 'settings/currencysettings/dealtype', nodetype: 'Module', description: '' },
                        ProductionType: { id: '3UvqzQ0Svxay6', caption: 'Production Type', nav: 'settings/currencysettings/productiontype', nodetype: 'Module', description: '' },
                        ScheduleType: { id: 'rUWFdPEkKkDAM', caption: 'Schedule Type', nav: 'settings/currencysettings/scheduletype', nodetype: 'Module', description: '' }
                    }
                },
                DiscountTemplateSettings: {
                    caption: 'Discount Template',
                    id: 'Settings.DiscountTemplateSettings',
                    nodetype: 'Category',
                    children: {
                        DiscountTemplate: { id: '1uoU0MeI7hIu', caption: 'Discount Template', nav: 'settings/discounttemplatesettings/discounttemplate', nodetype: 'Module', description: '' }
                    }
                },
                DocumentSettings: {
                    caption: 'Document',
                    id: 'Settings.DocumentSettings',
                    nodetype: 'Category',
                    children: {
                        DocumentType: { id: 'qus92U5z7R9Z', caption: 'Document Type', nav: 'settings/documentsettings/documenttype', nodetype: 'Module', description: '' },
                        CoverLetter: { id: 'ejyCz527IQCS', caption: 'Cover Letter', nav: 'settings/documentsettings/coverletter', nodetype: 'Module', description: '' },
                        TermsConditions: { id: 'lYqC40ZjalGUy', caption: 'Terms & Conditions', nav: 'settings/documentsettings/termsandconditions', nodetype: 'Module', description: '' }
                    }
                },
                EventSettings: {
                    caption: 'Event',
                    id: 'Settings.EventSettings',
                    nodetype: 'Category',
                    children: {
                        EventCategory: { id: '0Zcc827UeucP', caption: 'Event Category', nav: 'settings/eventsettings/eventcategory', nodetype: 'Module', description: '' },
                        EventType: { id: 'HXotrQfoaQCq', caption: 'Event Type', nav: 'settings/eventsettings/eventtype', nodetype: 'Module', description: '' },
                        PersonnelType: { id: 'Dd4V9E1c9Kz8', caption: 'Personnel Type', nav: 'settings/eventsettings/personneltype', nodetype: 'Module', description: '' },
                        PhotographyType: { id: 'bFH6YcKYCqye', caption: 'Photography Type', nav: 'settings/eventsettings/photographytype', nodetype: 'Module', description: '' }
                    }
                },
                ExportSettings: {
                    caption: 'Export',
                    id: 'Settings.ExportSettings',
                    nodetype: 'Category',
                    children: {
                        DataExportFormat: { id: 'ItSDcv89HNNo', caption: 'Data Export Format', nav: 'settings/exportsettings/dataexportformat', nodetype: 'Module', description: '' },
                    }
                },
                FacilitySettings: {
                    caption: 'Facility',
                    id: 'Settings.FacilitySettings',
                    nodetype: 'Category',
                    children: {
                        Building: { id: 'h0sTItX8Ofd', caption: 'Building', nav: 'settings/facilitysettings/building', nodetype: 'Module', description: '' },
                        FacilityCategory: { id: 'YA1ynwQcq11', caption: 'Facility Category', nav: 'settings/facilitysettings/facilitycategory', nodetype: 'Module', description: '' },
                        FacilityRate: { id: 'rA0UZvSMuSF', caption: 'Facility Rate', nav: 'settings/facilitysettings/facilityrate', nodetype: 'Module', description: '' },
                        FacilityScheduleStatus: { id: '8QjijODAMJt', caption: 'Facility Schedule Status', nav: 'settings/facilitysettings/facilityschedulestatus', nodetype: 'Module', description: '' },
                        FacilityStatus: { id: 'xJ4UyFe61kC', caption: 'Facility Status', nav: 'settings/facilitysettings/facilitystatus', nodetype: 'Module', description: '' },
                        FacilityType: { id: 'sp3q4geu1RZM', caption: 'Facility Type', nav: 'settings/facilitysettings/facilitytype', nodetype: 'Module', description: '' },
                        SpaceType: { id: 'lVjqEX5l2s8ZS', caption: 'Facility Space Type', nav: 'settings/facilitysettings/facilityspacetype', nodetype: 'Module', description: '' }
                    }
                },
                FiscalYearSettings: {
                    caption: 'Fiscal Year',
                    id: 'Settings.FiscalYearSettings',
                    nodetype: 'Category',
                    children: {
                        FiscalYear: { id: 'n8p9E78kGRM6', caption: 'Fiscal Year', nav: 'settings/fiscalyearsettings/fiscalyear', nodetype: 'Module', description: '' }
                    }
                },
                GeneratorSettings: {
                    caption: 'Generator',
                    id: 'Settings.GeneratorSettings',
                    nodetype: 'Category',
                    children: {
                        GeneratorFuelType: { id: 'WP4ewzQGUV8U', caption: 'Generator Fuel Type', nav: 'settings/generatorsettings/fueltype', nodetype: 'Module', description: '' },
                        GeneratorMake: { id: 'fHix04T2Hsc6', caption: 'Generator Make', nav: 'settings/generatorsettings/make', nodetype: 'Module', description: '' },
                        GeneratorRating: { id: 'B0hMFj9s7XGT', caption: 'Generator Rating', nav: 'settings/generatorsettings/rating', nodetype: 'Module', description: '' },
                        GeneratorWatts: { id: 'D2Z3jlFgx8Es', caption: 'Generator Watts', nav: 'settings/generatorsettings/watts', nodetype: 'Module', description: '' },
                        GeneratorType: { id: 'mUQp7GqmQlaR', caption: 'Generator Type', nav: 'settings/generatorsettings/type', nodetype: 'Module', description: '' }
                    }
                },
                HolidaySettings: {
                    caption: 'Holiday',
                    id: 'Settings.HolidaySettings',
                    nodetype: 'Category',
                    children: {
                        Holiday: { id: 'nZBYaMILxWSm', caption: 'Holiday', nav: 'settings/holidaysettings/holiday', nodetype: 'Module', description: '' },
                        BlackoutStatus: { id: '1B2gGaIJglY', caption: 'Blackout Status', nav: 'settings/holidaysettings/blackoutstatus', nodetype: 'Module', description: '' }
                    }
                },
                InventorySettings: {
                    caption: 'Inventory',
                    id: 'Settings.InventorySettings',
                    nodetype: 'Category',
                    children: {
                        Attribute:                 { id: 'Ok4Yh4kdsxk'  , caption: 'Inventory Attribute',         nav: 'module/inventorysettings/attribute', nodetype: 'Module', description: '' },
                        BarCodeRange:              { id: 'akTqMO0zmuc'  , caption: 'Bar Code Range',              nav: 'module/inventorysettings/barcoderange', nodetype: 'Module', description: '' },
                        InventoryAdjustmentReason: { id: 'geoncGlrjrAr' , caption: 'Inventory Adjustment Reason', nav: 'module/inventorysettings/inventoryadjustmentreason', nodetype: 'Module', description: '' },
                        InventoryCondition:        { id: 'JL0j4lk1KfBY' , caption: 'Inventory Condition',         nav: 'module/inventorysettings/inventorycondition', nodetype: 'Module', description: '' },
                        InventoryGroup:            { id: 'XgfGFSbbiiHy' , caption: 'Inventory Group',             nav: 'module/inventorysettings/inventorygroup', nodetype: 'Module', description: '' },
                        InventoryRank:             { id: '3YXhU6x3GseH' , caption: 'Inventory Rank',              nav: 'module/inventorysettings/inventoryrank', description: '' },
                        InventoryStatus:           { id: 'eb5WjxGH2duV' , caption: 'Inventory Status',            nav: 'module/inventorysettings/inventorystatus', nodetype: 'Module', description: '' },
                        InventoryType:             { id: 'aFLFxVNukHJt' , caption: 'Inventory Type',              nav: 'module/inventorysettings/inventorytype', nodetype: 'Module', description: '' },
                        PartsCategory:             { id: 'aSzlwy6XYMSV' , caption: 'Parts Category',              nav: 'module/inventorysettings/partscategory', nodetype: 'Module', description: '' },
                        RentalCategory:            { id: 'whxFImy6IZG2p', caption: 'Rental Category',             nav: 'module/inventorysettings/rentalcategory', nodetype: 'Module', description: '' },
                        RetiredReason:             { id: 'hktLnLB9qF7k' , caption: 'Retired Reason',              nav: 'module/inventorysettings/retiredreason', nodetype: 'Module', description: '' },
                        SalesCategory:             { id: 'XS6vdtV5jQTyF', caption: 'Sales Category',              nav: 'module/inventorysettings/salescategory', nodetype: 'Module', description: '' },
                        Unit:                      { id: 'K87j9eupQwohK', caption: 'Unit of Measure',             nav: 'module/inventorysettings/unit', nodetype: 'Module', description: '' },
                        UnretiredReason:           { id: '0SWJ0HNGoioxe', caption: 'Unretired Reason',            nav: 'module/inventorysettings/unretiredreason', nodetype: 'Module', description: '' },
                        WarehouseCatalog:          { id: 'wMXhVrm9w33xO', caption: 'Warehouse Catalog',           nav: 'module/inventorysettings/warehousecatalog', nodetype: 'Module', description: '' }
                    }
                },
                LaborSettings: {
                    caption: 'Labor',
                    id:      'Settings.LaborSettings',
                    nodetype: 'Category',
                    children: {
                        Crew:               { id: '7myCbtZNx85m', caption: 'Crew',                 nav: 'settings/laborsettings/crew', nodetype: 'Module', description: '' },
                        CrewScheduleStatus: { id: 'c0X4YfdKCp06', caption: 'Crew Schedule Status', nav: 'settings/laborsettings/crewschedulestatus', nodetype: 'Module', description: '' },
                        CrewStatus:         { id: 'uW0hAqUv6mDL', caption: 'Crew Status',          nav: 'settings/laborsettings/crewstatus', nodetype: 'Module', description: '' },
                        LaborCategory:      { id: 'nJIiZsDNxc83', caption: 'Labor Category',       nav: 'settings/laborsettings/laborcategory', nodetype: 'Module', description: '' },
                        LaborPosition:      { id: 'ZKb7ET3WoPs2', caption: 'Crew Position',        nav: 'settings/laborsettings/laborposition', nodetype: 'Module', description: '' },
                        LaborRate:          { id: 'GRs9mNWBxRw4', caption: 'Labor Rate',           nav: 'settings/laborsettings/laborrate', nodetype: 'Module', description: '' },
                        LaborType:          { id: 'FGjikpXt4iRf', caption: 'Labor Type',           nav: 'settings/laborsettings/labortype', nodetype: 'Module', description: '' }
                    }
                },
                MiscSettings: {
                    caption: 'Misc',
                    id: 'Settings.MiscellaneousSettings',
                    nodetype: 'Category',
                    children: {
                        MiscCategory:   { id: 'BRtP4O9fieRK', caption: 'Miscellaneous Category',    nav: 'settings/miscsettings/misccategory', nodetype: 'Module', description: '' },
                        MiscRate:       { id: 'tINVceJ8JoN7', caption: 'Miscellaneous Rate',      nav: 'settings/miscsettings/miscrate', nodetype: 'Module', description: '' },
                        MiscType:       { id: 'FjAFN8CLolYu', caption: 'Miscellaneous Type',      nav: 'settings/miscsettings/misctype', nodetype: 'Module', description: '' }
                    }
                },
                OfficeLocationSettings: {
                    caption: 'Office Location',
                    id: 'Settings.OfficeLocationSettings',
                    nodetype: 'Category',
                    children: {
                        OfficeLocation: { id: 'yZhqRrXdTEvN', caption: 'Office Location', nav: 'settings/officeloactionsettings/officelocation', nodetype: 'Module', description: '' }
                    }
                },
                OrderSettings: {
                    caption: 'Order',
                    id: 'Settings.OrderSettings',
                    nodetype: 'Category',
                    children: {
                        OrderType:      { id: 'yFStSrvTlwWY', caption: 'Order Type',        nav: 'settings/ordersettings/ordertype', nodetype: 'Module', description: '' },
                        DiscountReason: { id: 'XyjAvHBEaKL7', caption: 'Discount Reason',   nav: 'settings/ordersettings/discountreason', nodetype: 'Module', description: '' },
                        MarketSegment:  { id: 'NPu4Lci1ndrl', caption: 'Market Segment',    nav: 'settings/ordersettings/marketsegment', nodetype: 'Module', description: '' },
                        MarketType:     { id: 'sEgqHq5tov4n', caption: 'Market Type',       nav: 'settings/ordersettings/markettype', nodetype: 'Module', description: '' },
                        OrderSetNo:     { id: 'OoepsrkqPYRP', caption: 'Order Set No.',     nav: 'settings/ordersettings/ordersetno', nodetype: 'Module', description: '' },
                        OrderLocation:  { id: 'ezKyPjJBJKjQ', caption: 'Order Location',    nav: 'settings/laborsettings/laborrate', nodetype: 'Module', description: '' }
                    }
                },
                PaymentSettings: {
                    caption: 'Payment',
                    id: 'Settings.PaymentSettings',
                    nodetype: 'Category',
                    children: {
                        PaymentTerms:   { id: 'p5RqSdENdPMa', caption: 'Payment Terms', nav: 'settings/paymentsettings/paymentterms', nodetype: 'Module', description: '' },
                        PaymentType:    { id: 'd8RdKxFfho4z', caption: 'Payment Type',  nav: 'settings/paymentsettings/paymenttype', nodetype: 'Module', description: '' }
                    }
                },
                POSettings: {
                    caption: 'PO',
                    id: 'Settings.POSettings',
                    nodetype: 'Category',
                    children: {
                        POApprovalStatus: { id: '9CsrBJ9TN1wT', caption: 'PO Approval Status', nav: 'settings/posettings/poapprovalstatus', nodetype: 'Module', description: '' },
                        POApprover: { id: 'kaGlUrLG9GjN', caption: 'PO Approver', nav: 'settings/posettings/POApprover', nodetype: 'Module', description: '' },
                        POApproverRole: { id: 'HdPKvHGhi3zf', caption: 'PO Approver Role', nav: 'settings/posettings/poapproverrole', nodetype: 'Module', description: '' },
                        POClassification: { id: 'skhmIJOt0Fi0', caption: 'PO Classification', nav: 'settings/posettings/poclassification', nodetype: 'Module', description: '' },
                        POImportance: { id: 'gLt2YTtB2afl', caption: 'PO Importance', nav: 'settings/posettings/poimportance', nodetype: 'Module', description: '' },
                        PORejectReason: { id: 'xwTGYRx4Gg21', caption: 'PO Reject Reason', nav: 'settings/posettings/porejectreason', nodetype: 'Module', description: '' },
                        POType: { id: 'Gyx3ZcMtuH1fi', caption: 'PO Type', nav: 'settings/posettings/potype', nodetype: 'Module', description: '' },
                        VendorInvoiceApprover: { id: '3Hhg9Bl5Rm1mT', caption: 'Vendor Invoice Approver', nav: 'settings/posettings/vendorinvoiceapprover', nodetype: 'Module', description: '' }
                    }
                },
                PresentationSettings: {
                    caption: 'Presentation',
                    id:      'Settings.PresentationSettings',
                    nodetype: 'Category',
                    children: {
                        FormDesign: { id: 'er64NLGnsWN7', caption: 'Form Design', nav: 'settings/presentationsettings/formdesign', nodetype: 'Module', description: '' },
                        PresentationLayer:  { id: '0v54dFE9Zhun8', caption: 'Presentation Layer',   nav: 'settings/presentationsettings/presentationlayer', nodetype: 'Module', description: '' }
                    }
                },
                ProjectSettings: {
                    caption: 'Project',
                    id:      'Settings.ProjectSettings',
                    nodetype: 'Category',
                    children: {
                        ProjectAsBuild:       { id: 'CiTY0pLnyroMa', caption: 'Project As Build',        nav: 'settings/projectsettings/projectasbuild', nodetype: 'Module', description: '' },
                        ProjectCommissioning: { id: '124H9oI67IKRx', caption: 'Project Commissioning',   nav: 'settings/projectsettings/projectcommissioning', nodetype: 'Module', description: '' },
                        ProjectDeposit:       { id: 'z9uSXy8A4AdoO', caption: 'Project Deposit',         nav: 'settings/projectsettings/projectdeposit', nodetype: 'Module', description: '' },
                        ProjectDrawings:      { id: 'e0Ylzlhkp2wY0', caption: 'Project Drawings',        nav: 'settings/projectsettings/projectdrawings', nodetype: 'Module', description: '' },
                        ProjectDropShipItems: { id: 'XzUwxCWh64FDw', caption: 'Project Drop Ship Items', nav: 'settings/projectsettings/projectdropshipitems', nodetype: 'Module', description: '' },
                        ProjectItemsOrdered:  { id: 'oB5CeVYRCU1EG', caption: 'Project Items Ordered',   nav: 'settings/projectsettings/projectitemsordered', nodetype: 'Module', description: '' }
                    }
                },
                PropsSettings: {
                    caption: 'Props',
                    id:      'Settings.PropsSettings',
                    nodetype: 'Category',
                    children: {
                        PropsCondition: { id: 'E793OHd1PFRk4', caption: 'Props Condition', nav: 'settings/propssettings/propscondition', nodetype: 'Module', description: '' }
                    }
                },
                RegionSettings: {
                    caption: 'Region',
                    id:      'Settings.RegionSettings',
                    nodetype: 'Category',
                    children: {
                        Region: { id: 'pqSlzQGRVmxiE', caption: 'Region', nav: 'settings/regionsettings/region', nodetype: 'Module', description: '' }
                    }
                },
                RepairSettings: {
                    caption: 'Repair',
                    id:      'Settings.RepairSettings',
                    nodetype: 'Category',
                    children: {
                        RepairItemStatus: { id: 'iuo4dnWX5KCP8', caption: 'Repair Item Status', nav: 'settings/repairsettings/repairitemstatus', nodetype: 'Module', description: '' }
                    }
                },
                SetSettings: {
                    caption: 'Set',
                    id:      'Settings.SetSettings',
                    nodetype: 'Category',
                    children: {
                        SetCondition:    { id: '3r7dQtlxlUX8u', caption: 'Set Condition',     nav: 'settings/setsettings/setcondition', nodetype: 'Module', description: '' },
                        SetOpening:      { id: 'gwzYE66lX9myO', caption: 'Set Opening',       nav: 'settings/setsettings/setopening', nodetype: 'Module', description: '' },
                        SetSurface:      { id: 'Fg5VqZXTcgja2', caption: 'Set Surface',       nav: 'settings/setsettings/setsurface', nodetype: 'Module', description: '' },
                        WallDescription: { id: '0uJgpWp1Mj9Jd', caption: 'Wall Descriptions', nav: 'settings/setsettings/walldescription', nodetype: 'Module', description: '' },
                        WallType:        { id: 'V45pfjoiW04Ix', caption: 'Wall Type',         nav: 'settings/setsettings/walltype', nodetype: 'Module', description: '' }
                    }
                },
                ShipViaSettings: {
                    caption: 'Ship Via',
                    id: 'Settings.ShipViaSettings',
                    nodetype: 'Category',
                    children: {
                        ShipVia: { id: 'D1wheIde10lAO', caption: 'Ship Via', nav: 'settings/shipviasettings/shipvia', nodetype: 'Module', description: '' }
                    }
                },
                SourceSettings: {
                    caption: 'Source',
                    id: 'Settings.SourceSettings',
                    nodetype: 'Category',
                    children: {
                        Source: { id: 'BOH4LAvrGvVjW', caption: 'Source', nav: 'settings/sourcesettings/source', nodetype: 'Module', description: '' }
                    }
                },
                SystemSettings: {
                    caption: 'System',
                    id: 'Settings.SystemSettings',
                    nodetype: 'Category',
                    children: {
                        AvailabilitySettings: { id: 'UXYMLInJl6JMP', caption: 'Availability Settings', nav: 'settings/systemsettings/availabilitysettings', nodetype: 'Module', description: '' },
                        DefaultSettings: { id: '6pvUgTaKPnjf3', caption: 'Default Settings', nav: 'settings/systemsettings/defaultsettings', nodetype: 'Module', description: '' },
                        DocumentBarCodeSettings: { id: 'iSSvVLPqOGXnD', caption: 'Document Bar Code Settings', nav: 'settings/systemsettings/documentbarcodesettings', nodetype: 'Module', description: '' },
                        EmailSettings: { id: '7MWuq0m77CnZ8', caption: 'Email Settings', nav: 'settings/systemsettings/emailsettings', nodetype: 'Module', description: '' },
                        InventorySettings: { id: 'fdUclyoOCTYbx', caption: 'Inventory Settings', nav: 'module/systemsettings/inventorysettings', nodetype: 'Module', description: '' },
                        LogoSettings: { id: 'FM7iCQcVmmUqK', caption: 'Logo Settings', nav: 'settings/systemsettings/logosettings', nodetype: 'Module', description: '' },
                        SystemSettings: { id: 'v3tPhS7Ug7qgO', caption: 'System Settings', nav: 'settings/systemsettings/systemsettings', nodetype: 'Module', description: '' }
                    }
                },
                TaxSettings: {
                    caption: 'Tax Option',
                    id: 'Settings.TaxSettings',
                    nodetype: 'Category',
                    children: {
                        TaxOption: { id: 'gYT7BJnFn9SLc', caption: 'Tax Option', nav: 'settings/taxsettings/taxoption', nodetype: 'Module', description: '',
                            form: {
                                menuItems: {
                                    ForceTaxRates: { id: '{CE1AEA95-F022-4CF5-A4FA-81CE32523344}' }
                                }
                            }
                        }
                    }
                },
                TemplateSettings: {
                    caption: 'Template',
                    id: 'Settings.TemplateSettings',
                    nodetype: 'Category',
                    children: {
                        Template: { id: '74uXyH7jXXbZM', caption: 'Template', nav: 'settings/templatesettings/template', nodetype: 'Module', description: '',
                            form: {
                                menuItems: {
                                    Search: { id: '{6386E100-98B2-42F3-BF71-5BB432070D10}' }
                                }
                            }
                        }
                    }
                },
                UserSettings: {
                    caption: 'User',
                    id: 'Settings.UserSettings',
                    nodetype: 'Category',
                    children: {
                        UserStatus: { id: 'YjSbfCF9CEvjz', caption: 'User Status',  nav: 'settings/usersettings/userstatus', nodetype: 'Module', description: '' },
                        Sound:      { id: '1SCjkmxKUSbaQ', caption: 'Sound',        nav: 'settings/usersettings/sound', nodetype: 'Module', description: '' }
                    }
                },
                VehicleSettings: {
                    caption: 'Vehicle',
                    id: 'Settings.VehicleSettings',
                    nodetype: 'Category',
                    children: {
                        LicenseClass:           { id: 'gLsGICI8R4VM',   caption: 'License Class',           nav: 'settings/vehiclesettings/licenseclass', nodetype: 'Module', description: '' },
                        VehicleColor:           { id: 'vxf0Ur4W8UEzw',  caption: 'Vehicle Color',           nav: 'settings/vehiclesettings/vehiclecolor', nodetype: 'Module', description: '' },
                        VehicleFuelType:        { id: 'T1uhkfrSK7J3d',  caption: 'Vehicle Fuel Type',       nav: 'settings/vehiclesettings/vehiclefueltype', nodetype: 'Module', description: '' },
                        VehicleMake:            { id: 'Kacj9CuAA7F8m',  caption: 'Vehicle Make',            nav: 'settings/vehiclesettings/vehiclemake', nodetype: 'Module', description: '' },
                        VehicleScheduleStatus:  { id: 'nvZvZeHdxviKG',  caption: 'Vehicle Schedule Status', nav: 'settings/vehiclesettings/vehicleschedulestatus', nodetype: 'Module', description: '' },
                        VehicleStatus:          { id: 'RSXpDaDwDtgH3',  caption: 'Vehicle Status',          nav: 'settings/vehiclesettings/vehiclestatus', nodetype: 'Module', description: '' },
                        VehicleType:            { id: 'lbbSCJTjhBL3U',  caption: 'Vehicle Type',            nav: 'settings/vehiclesettings/vehicletype', nodetype: 'Module', description: '' }
                    }
                },
                VendorSettings: {
                    caption: 'Vendor',
                    id: 'Settings.VendorSettings',
                    nodetype: 'Category',
                    children: {
                        OrganizationType:       { id: 'ENv2O3MbwKrI',   caption: 'Organization Type',           nav: 'settings/vendorsettings/organizationtype', nodetype: 'Module', description: '' },
                        VendorCatalog:          { id: '086ok4V8ztfCu',  caption: 'Vendor Catalog',              nav: 'settings/vendorsettings/vendorcatalog', nodetype: 'Module', description: '' },
                        VendorClass:            { id: 'EH6T4hlMVhYxq',  caption: 'Vendor Class',                nav: 'settings/vendorsettings/class', nodetype: 'Module', description: '' },
                        SapVendorInvoiceStatus: { id: 'rzRfHzdo5DVyn',  caption: 'SAP Vendor Invoice Status',   nav: 'settings/vendorsettings/sapvendorinvoicestatus', nodetype: 'Module', description: '' }
                    }
                },
                WardrobeSettings: {
                    caption: 'Wardrobe',
                    id: 'Settings.WardrobeSettings',
                    nodetype: 'Category',
                    children: {
                        WardrobeCare: { id: 'DcPDf33MJtiO4', caption: 'Wardrobe Care', nav: 'settings/wardrobesettings/wardrobecare', nodetype: 'Module', description: '' },
                        WardrobeColor: { id: 'Kc77x9j3t1Cf8', caption: 'Wardrobe Color', nav: 'settings/wardrobesettings/wardrobecolor', nodetype: 'Module', description: '' },
                        WardrobeCondition: { id: 'W3fQSvmWyXmox', caption: 'Wardrobe Condition', nav: 'settings/wardrobesettings/wardrobecondition', nodetype: 'Module', description: '' },
                        WardrobeGender: { id: 'oZl62v243hmoY', caption: 'Wardrobe Gender', nav: 'settings/wardrobesettings/wardrobegender', nodetype: 'Module', description: '' },
                        WardrobeLabel: { id: 'oPEyg39ExOQhO', caption: 'Wardrobe Label', nav: 'settings/wardrobesettings/wardrobelabel', nodetype: 'Module', description: '' },
                        WardrobeMaterial: { id: 'rTEb0yyIofBBU', caption: 'Wardrobe Material', nav: 'settings/wardrobesettings/wardrobematerial', nodetype: 'Module', description: '' },
                        WardrobePattern: { id: '3SMTlxYJ2LGH1', caption: 'Wardrobe Pattern', nav: 'settings/wardrobesettings/wardrobepattern', nodetype: 'Module', description: '' },
                        WardrobePeriod: { id: 'OhMqm7XJBcfI1', caption: 'Wardrobe Period', nav: 'settings/wardrobesettings/wardrobeperiod', nodetype: 'Module', description: '' },
                        WardrobeSource: { id: 'ngguXevaaonRp', caption: 'Wardrobe Source', nav: 'settings/wardrobesettings/wardrobesource', nodetype: 'Module', description: '' }
                    }
                },
                WarehouseSettings: {
                    caption: 'Warehouse',
                    id: 'Settings.WarehouseSettings',
                    nodetype: 'Category',
                    children: {
                        Warehouse: { id: 'ICJcR2gOu04OB', caption: 'Warehouse', nav: 'settings/warehousesettings/warehouse', nodetype: 'Module', description: '' }
                    }
                },
                WidgetSettings: {
                    caption: 'Widget',
                    id: 'Settings.WidgetSettings',
                    nodetype: 'Category',
                    children: {
                        Widget: { id: 'QLfJikDW0foC1', caption: 'Widget', nav: 'settings/widgetsettings/widget', nodetype: 'Module', description: '' }
                    }
                },
                WorkWeekSettings: {
                    caption: 'Work Week',
                    id: 'Settings.WorkWeekSettings',
                    nodetype: 'Category',
                    children: {
                        WorkWeek: { id: 'hRNv34ONOUmB7', caption: 'Work Week', nav: 'settings/workweeksettings/workweek', nodetype: 'Module', description: '' }
                    }
                }
            }
        },
        Transfers: {
            id: 'Transfers',
            caption: 'Transfers',
            nodetype: 'Category',
            children: {
                TransferIn:      { id: 'aVOT6HR8knES', caption: 'Transfer In',       nav: 'module/transferin' , nodetype: 'Module'},
                Manifest:        { id: 'tc2HgrtvGDJ5', caption: 'Transfer Manifest', nav: 'module/manifest' , nodetype: 'Module'},
                TransferOrder:   { id: 'tWkLbjsVHH6N', caption: 'Transfer Order',    nav: 'module/transferorder', nodetype: 'Module' },
                TransferOut:     { id: 'uxIAX8VBtAwD', caption: 'Transfer Out',      nav: 'module/transferout' , nodetype: 'Module'},
                TransferReceipt: { id: 'VSn3weoOHqLc', caption: 'Transfer Receipt',  nav: 'module/transferreceipt', nodetype: 'Module' },
                TransferStatus:  { id: 'PgppzG0HnSzI', caption: 'Transfer Status',   nav: 'module/transferstatus', nodetype: 'Module' }
            }
        },
        Utilities: {
            id: 'Utilities',
            caption: 'Utilities',
            nodetype: 'Category',
            children: {
                Dashboard:                 { id: 'DF8111F5-F022-40B4-BAE6-23B2C6CF3705', caption: 'Dashboard', nav: 'home', nodetype: 'Module' },
                DashboardSettings:         { id: 'lXpomto7a29v', caption: 'Dashboard Settings', nav: 'module/dashboardsettings', nodetype: 'Module' },
                InventoryPurchaseUtility:  { id: 'sOxbXBmCPc9y', caption: 'Inventory Purchase Utility', nav: 'module/inventorypurchaseutility', nodetype: 'Module' },
                InvoiceProcessBatch:       { id: 'I8d2wTNNRmRJa', caption: 'Process Invoices',        nav: 'module/invoiceprocessbatch', nodetype: 'Module' },
                MigrateOrders:             { id: '8NYSNibMVoO',   caption: 'Migrate Orders',          nav: 'module/migrateorders', nodetype: 'Module' },
                QuikActivityCalendar:      { id: 'yhYOLhLE92IT',  caption: 'QuikActivity Calendar',   nav: 'module/quikactivitycalendar', nodetype: 'Module' },
                QuikSearch:                { id: '0q9EEmHe5xXO',  caption: 'QuikSearch',              nav: 'module/quiksearch', nodetype: 'Module' },
                ReceiptProcessBatch:       { id: 'ThKpggGlj1hqd', caption: 'Process Receipts',        nav: 'module/receiptprocessbatch', nodetype: 'Module' },
                RefreshGLHistory:          { id: 'UuKB0PPalR9p',  caption: 'Refresh G/L History',     nav: 'module/refreshglhistory', nodetype: 'Module' },
                VendorInvoiceProcessBatch: { id: 'gRjYvLD2qZ6NR', caption: 'Process Vendor Invoices', nav: 'module/vendorinvoiceprocessbatch', nodetype: 'Module' }
            }
        },
        Warehouse: {
            id: 'Warehouse',
            caption: 'Warehouse',
            nodetype: 'Category',
            children: {
                AssignBarCodes:    { id: '7UU96BApz2Va', caption: 'Assign Bar Codes',    nav: 'module/assignbarcodes', nodetype: 'Module' },
                CheckIn:           { id: 'krnJWTUs4n5U', caption: 'Check-In',            nav: 'module/checkin', nodetype: 'Module' },
                Contract:          { id: 'Z8MlDQp7xOqu', caption: 'Contract',            nav: 'module/contract', nodetype: 'Module' },
                Exchange:          { id: 'IQS4rxzIVFl',  caption: 'Exchange',            nav: 'module/exchange', nodetype: 'Module' },
                OrderStatus:       { id: 'C8Ycf0jvM2U9', caption: 'Order Status',        nav: 'module/orderstatus', nodetype: 'Module' },
                PickList:          { id: 'bggVQOivrIgi', caption: 'Pick List',           nav: 'module/picklist', nodetype: 'Module' },
                ReceiveFromVendor: { id: 'MtgBxCKWVl7m', caption: 'Receive From Vendor', nav: 'module/receivefromvendor', nodetype: 'Module' },
                ReturnToVendor:    { id: 'cCxoTvTCDTcm', caption: 'Return To Vendor',    nav: 'module/returntovendor', nodetype: 'Module' },
                StagingCheckout:   { id: 'H0sf3MFhL0VK', caption: 'Staging / Check-Out', nav: 'module/checkout', nodetype: 'Module' }
            }
        }
    },
    Grids: {
        AdditionalItemsGrid: { id: 'mEYOByOhi5yT0', caption: 'Additional Items' },
        AlertWebUsersGrid: { id: 'REgcmntq4LWE', caption: 'Alert Web Users' },
        AlternativeDescriptionGrid: { id: '2BkAgaVVrDD3', caption: 'Alternative Description' },
        AttributeValueGrid: { id: '2uvN8jERScu', caption: 'Attribute Value' },
        AuditHistoryGrid: { id: 'xepjGBf0rdL', caption: 'Audit History' },
        AvailabilityHistoryGrid: { id: 'OFP98sWaSXqD3', caption: 'Availability History' },
        BillingCycleEventsGrid: { id: 'KSA8EsXjcrt', caption: 'Billing Cycle Events' },
        CheckedInItemGrid: { id: 'RanTH3xgxNy', caption: 'Checked-In Item' },
        CheckedOutItemGrid: { id: 'HXSEu4U0vSir', caption: 'Checked-Out Item' },
        CheckInExceptionGrid: { id: '3S49xMb3FrcD', caption: 'Check-In Exception' },
        CheckInOrderGrid: { id: 'HSZSZp9Ovrpq', caption: 'Check-In Order' },
        CheckInQuantityItemsGrid: { id: 'BfClP5w8rjl7', caption: 'Check-In Quantity Items' },
        CheckInSwapGrid: { id: 'hA3FE9ProwUn', caption: 'Check-In Swap' },
        CheckOutPendingItemGrid: { id: 'GO96A3pk0UE', caption: 'Check-Out Pending Items' },
        CompanyContactGrid: { id: 'gQHuhVDA5Do2', caption: 'Copmany Contact' },
        CompanyResaleGrid: { id: 'k48X9sulRpmb', caption: 'Company Resale' },
        CompanyTaxOptionGrid: { id: 'B9CzDEmYe1Zf', caption: 'Company Tax Option' },
        ContactCompanyGrid: { id: 'gQHuhVDA5Do2', caption: 'Contact Company' },
        //ContactEmailHistoryGrid: { id: '', caption: 'Contact Email History' },
        ContactNoteGrid: { id: 'mkJ1Ry8nqSnw', caption: 'Contact Note' },
        ContactPersonalEventGrid: { id: '35was7r004gg', caption: 'Contact Personal Event' },
        ContainerWarehouseGrid: { id: '4gsBzepUJdWm', caption: 'Container Warehouse' },
        ContractDetailGrid: { id: 'uJtRkkpKi8zT', caption: 'Contract Detail' },
        ContractExchangeItemGrid: { id: 'Azkpehs1tvl', caption: 'Contract Exchange Item' },
        ContractSummaryGrid: { id: 'a8I3UCKA3LN3', caption: 'Contract Summary' },
        CrewLocationGrid: { id: 'vCrMyhsLCP7h', caption: 'Crew Location' },
        CrewPositionGrid: { id: 'shA9rX1DYWp3', caption: 'Crew Position' },
        CustomerNoteGrid: { id: '6AHfzr9WBEW9', caption: 'Customer Note' },
        CustomFormGroupGrid: { id: '11txpzVKVGi2', caption: 'Custom Form Group' },
        CustomFormUserGrid: { id: 'nHNdXDBX6m6cp', caption: 'Custom Form User' },
        DealNoteGrid: { id: 'jcwmVLFEU88k', caption: 'Deal Notes' },
        DealShipperGrid: { id: '5cMD0y0jSUgz', caption: 'Deal Shipper' },
        DiscountItemLaborGrid: { id: 'UMKuETy6vOLA', caption: 'Discount Item Labor' },
        DiscountItemMiscGrid: { id: 'UMKuETy6vOLA', caption: 'Discount Item Misc' },
        DiscountItemRentalGrid: { id: 'UMKuETy6vOLA', caption: 'Discount Item Rental' },
        DiscountItemSalesGrid: { id: 'UMKuETy6vOLA', caption: 'Discount Item Sales' },
        //DocumentGrid: { id: '', caption: 'Document' },
        DuplicateRuleFieldGrid: { id: 'DlPBQCJUC0iK', caption: 'Duplicate Rule Field' },
        EventTypePersonnelTypeGrid: { id: 'CbjLxfIjRyg', caption: 'Event Type Personnel Type' },
        ExchangeItemGrid: { id: 'Azkpehs1tvl', caption: 'Exchange Item' },
        FiscalMonthGrid: { id: 'wt6RLPk0GOrm', caption: 'Fiscal Month' },
        FiscalYearGrid: { id: 'n8p9E78kGRM6', caption: 'Fiscal Year' },
        FloorGrid: { id: 'LrybQVClgY6f', caption: 'Floor' },
        GeneratorMakeModelGrid: { id: 'CFnh3uxNiWZy', caption: 'Generator Make Model' },
        GeneratorTypeWarehouseGrid: { id: 'N400cxkXaRDx', caption: 'Generator Type Warehouse' },
        GlDistributionGrid: { id: 'UuKB0PPalR9p', caption: 'G/L Distribution' },
        InventoryAttributeValueGrid: { id: 'CntxgVXDQtQ7', caption: 'Inventory Attribute Value' },
        InventoryAvailabilityGrid: { id: 'g8sCuKjUVrW1', caption: 'Inventory Availability' },
        InventoryCompatibilityGrid: { id: 'mlAKf5gRPNNI', caption: 'Inventory Compatibility' },
        InventoryCompleteGrid: { id: 'ABL0XJQpsQQo', caption: 'Inventory Complete',
            menuItems: {
                QuikSearch: { id: '{A3EEC381-6D45-485D-8E12-5DA6B38BB71A}' }
            }
        },
        InventoryCompleteKitGrid: { id: 'gflkb5sQf7it', caption: 'Inventory Complete Kit' },
        InventoryConsignmentGrid: { id: 'JKfdyoLXFqu3', caption: 'Inventory Consignment' },
        InventoryContainerItemGrid: { id: '6ELSTtE6IqSb', caption: 'Inventory Container Item',
            menuItems: {
                QuikSearch: { id: '{BA9FD9DD-2E96-4A4D-80B9-6010BEE66D6F}' }
            }
        },
        InventoryGroupInvGrid: { id: 'IC5rbdvS3Me7', caption: 'Inventory Group Inventory' },
        InventoryKitGrid: { id: 'ABL0XJQpsQQo', caption: 'Inventory Kit',
            menuItems: {
                QuikSearch: { id: '{B599B514-30BD-49B3-A08A-7863693D979C}' }
            }
        },
        InventoryLocationTaxGrid: { id: 'dpDtvVrXRZrd', caption: 'Inventory Location Tax' },
        InventoryPrepGrid: { id: 'CzNh6kOVsRO4', caption: 'Inventory Prep' },
        InventoryQcGrid: { id: 'g8sCuKjUVrW1', caption: 'Inventory QC' },
        InventorySubstituteGrid: { id: '5sN9zKtGzNTq', caption: 'Inventory Substitute' },
        InventoryVendorGrid: { id: 's9vdtBqItIEi', caption: 'Inventory Vendor' },
        InventoryWarehouseStagingGrid: { id: 'g8sCuKjUVrW1', caption: 'Inventory Warehouse Staging' },
        InvoiceItemGrid: { id: '5xgHiF8dduf', caption: 'Invoice Item',
            menuItems: {
                ToggleOrderItemView: { id: '{46D27E42-9C66-42F5-922C-CAE617856F63}' }
            }    
        },
        InvoiceNoteGrid: { id: 'PjT15E4lWmo7', caption: 'Invoice Notes' },
        InvoiceOrderGrid: { id: 'xAv0ILs8aJA5C', caption: 'Invoice Order' },
        InvoiceReceiptGrid: { id: 'cYUr48pou4fc', caption: 'Invoice Receipt' },
        InvoiceRevenueGrid: { id: '2wrr1zqjxBeJ', caption: 'Invoice Revenue' },
        InvoiceStatusHistoryGrid: { id: '3bf1WgNHvIyF', caption: 'Invoice Status History' },
        ItemAttributeValueGrid: { id: 'buplkDkxM1hC', caption: 'Item Attribute Value' },
        ItemQcGrid: { id: 'u4UHiW7AOeZ5', caption: 'Item QC' },
        LaborCategoryGrid: { id: 'nJIiZsDNxc83', caption: 'Labor Category' },
        LossAndDamageItemGrid: { id: 'ATHmxMHmRo9u', caption: 'Lost and Damage Items' },
        ManualGlTransactionsGrid: { id: '00B9yDUY6RQfB', caption: 'Manual G/L Transactions' },
        MarketSegmentJobGrid: { id: 'OWZGrnUnJHon', caption: 'Market Segment Job' },
        MigrateItemGrid: { id: 'szZ66eT0VS5', caption: 'Migrate Orders Item' },
        MiscCategoryGrid: { id: 'BRtP4O9fieRK', caption: 'Misc Category' },
        OrderContactGrid: { id: '7CUe9WvpWNat', caption: 'Order Contact' },
        OrderItemGrid: {  id: 'RFgCJpybXoEb', caption: 'Order Item',
            menuItems: {
                SummaryView: { id: '{D27AD4E7-E924-47D1-AF6E-992B92F5A647}' },
                //ManualSorting: { id: '{AD3FB369-5A40-4984-8A65-46E683851E52}' },
                CopyTemplate: { id: '{B6B68464-B95C-4A4C-BAF2-6AA59B871468}' },
                Search: { id: '{77E511EC-5463-43A0-9C5D-B54407C97B15}' },
                CopyLineItems: { id: '{01EB96CB-6C62-4D5C-9224-8B6F45AD9F63}' },
                LockUnlockSelected: { id: '{BC467EF9-F255-4F51-A6F2-57276D8824A3}' },
                BoldUnBoldSelected: { id: '{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}' },
                SubWorksheet: { id: '{007C4F21-7526-437C-AD1C-4BBB1030AABA}' },
                AddLossAndDamageItems: { id: '{427FCDFE-7E42-4081-A388-150D3D7FAE36}' },
                RetireLossAndDamageItems: { id: '{78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412}' },
                ColorLegend: { id: '{A2CD9CB6-1C38-4E4E-935C-627D32282480}' },
                ShortagesOnly: { id: '{873546DE-E8EF-4B34-8215-B2EC65E12056}' },
                SplitDetails: { id: '{679D6E7A-C212-41A3-88D0-5B48936812A0}' },
            }
        },
        OrderNoteGrid: { id: 'DZwS6DaO7Ed8', caption: 'Order Note' },
        OrderPickListGrid: { id: 'fa0NLEHNkNU0', caption: 'Order Pick List' },
        OrderSnapshotGrid: { id: 'YZQzEHG7tTUP', caption: 'Order Snapshot',
            menuItems: {
                ViewSnapshot: { id: '{C6633D9A-3800-41F2-8747-BC780663E22F}' }
            }
        },
        OrderStatusHistoryGrid: { id: 'lATsdnAx7B4s', caption: 'Order Status History' },
        OrderStatusRentalDetailGrid: { id: '75OyLDxMPa8z', caption: 'Order Status Rental Detail' },
        OrderStatusSalesDetailGrid: { id: '75OyLDxMPa8z', caption: 'Order Status Sales Detail' },
        OrderStatusSummaryGrid: { id: '3NAO1rd02hBF', caption: 'Order Status Summary' },
        OrderTypeActivityDatesGrid: { id: 'oMijD9WAL6Bl', caption: 'Order Type Activity Dates' },
        OrderTypeContactTitleGrid: { id: 'HzNQkWcZ8vEC', caption: 'Order Type Contact Title' },
        OrderTypeCoverLetterGrid: { id: 'acguZNBoT1XC', caption: 'Order Type Cover Letter' },
        OrderTypeInvoiceExportGrid: { id: 'acguZNBoT1XC', caption: 'Order Type Invoice Export' },
        OrderTypeNoteGrid: { id: 'DZwS6DaO7Ed8', caption: 'Order Type Note' },
        OrderTypeTermsAndConditionsGrid: { id: 'acguZNBoT1XC', caption: 'Order Type Terms And Conditions' },
        PartsInventoryCompatibilityGrid: { id: 'mlAKf5gRPNNI', caption: 'Parts Inventory Compatibility' },
        PartsInventorySubstituteGrid: { id: '5sN9zKtGzNTq', caption: 'Parts Inventory Substitute' },
        PartsInventoryWarehouseGrid: { id: 'g8sCuKjUVrW1', caption: 'Parts Inventory Warehouse' },
        PartsInventoryWarehousePricingGrid: { id: 'g8sCuKjUVrW1', caption: 'Parts Inventory Warehouse Pricing' },
        PhysicalInventoryCycleInventoryGrid: { id: 'juyq8FkxJPR5Q', caption: 'Physical Inventory Cycle Inventory' },
        PhysicalInventoryInventoryGrid: { id: 'BEoHoFVd3JFXN', caption: 'Physical Inventory Inventory' },
        PhysicalInventoryQuantityInventoryGrid: { id: 'EZDA4vdM8wY32', caption: 'Physical Inventory Quantity Inventory' },
        PickListItemGrid: { id: 'fa0NLEHNkNU0', caption: 'Pick List Item' },
        PickListUtilityGrid: { id: 'DOnlknWuWfYS', caption: 'Pick List Utility' },
        POApproverGrid: { id: 'kaGlUrLG9GjN', caption: 'PO Approver' },
        POReceiveBarCodeGrid: { id: 'qH0cLrQVt9avI', caption: 'PO Receive Bar Code' },
        POReceiveItemGrid: { id: 'uYBpfQCZBM4V6', caption: 'PO Receive Items' },
        POReturnBarCodeGrid: { id: 'JkwkAFQ4tL7q0', caption: 'PO Return Bar Code' },
        POReturnItemGrid: { id: 'wND2psEV3OEia', caption: 'PO Return Items' },
        PresentationLayerActivityGrid: { id: 'QiLcE27ZUg0sE', caption: 'Presentation Layer Activity' },
        PresentationLayerActivityOverrideGrid: { id: 'HWjX0WDoiG79H', caption: 'Presentation Layer Activity Override' },
        PresentationLayerFormGrid: { id: 'FcJ0Ld64KSUqv', caption: 'Presentation Layer Form' },
        ProjectContactGrid: { id: 'ZvjyLW5OM5s1X', caption: 'Project Contact' },
        ProjectNoteGrid: { id: 'tR09bf745p0YU', caption: 'Project Note' },
        PurchaseVendorGrid: { id: '15yjeHiHe1x99', caption: 'Purchase Vendor' },
        PurchaseVendorInvoiceItemGrid: { id: 'NlKSJj2fN0ly', caption: 'Purchase Vendor Invoice Item' },
        QuikActivityGrid: { id: 'yhYOLhLE92IT', caption: 'QuikActivity' },
        RateLocationTaxGrid: { id: 'Bm6TN9A4IRIuT', caption: 'Rate Location Tax' },
        RateWarehouseGrid: { id: 'oVjmeqXtHEJCm', caption: 'Rate Warehouse' },
        RentalInventoryWarehouseGrid: { id: 'g8sCuKjUVrW1', caption: 'Rental Inventory Warehouse' },
        RentalInventoryWarehousePricingGrid: { id: 'g8sCuKjUVrW1', caption: 'Rental Inventory Warehouse Pricing' },
        RepairCostGrid: { id: 'THGHEcObwRTDc', caption: 'Repair Cost' },
        RepairPartGrid: { id: 'k1Qn9brpxHGhp', caption: 'Repair Part' },
        RepairReleaseGrid: { id: 'O2lL9RZYzdjNg', caption: 'Repair Release' },
        ReportSettingsGrid: { id: 'arqFEggnNSrA6', caption: 'Report Settings' },
        SalesInventoryCompatibilityGrid: { id: 'mlAKf5gRPNNI', caption: 'Sales Inventory Compatibility' },
        SalesInventorySubstituteGrid: { id: '5sN9zKtGzNTq', caption: 'Sales Inventory Substitute' },
        SalesInventoryWarehouseGrid: { id: 'g8sCuKjUVrW1', caption: 'Sales Inventory Warehouse' },
        SalesInventoryWarehousePricingGrid: { id: 'g8sCuKjUVrW1', caption: 'Sales Inventory Warehouse Pricing' },
        SearchPreviewGrid: { id: 'JLDAuUcvHEx1', caption: 'Search Preview',
            menuItems: {
                //RefreshAvailability: { id: '{3756AF3A-1611-4BCD-BBD9-E3233F5A772E}' }
            }
        },
        SingleRateWarehouseGrid: { id: 'oVjmeqXtHEJCm', caption: 'Single Rate Warehouse' },
        SpaceGrid: { id: 'DgWXultjwPXkU', caption: 'Space' },
        SpaceRateGrid: { id: 'iWPadFxStXkcL', caption: 'Space Rate' },
        SpaceWarehouseRateGrid: { id: 'oVjmeqXtHEJCm', caption: 'Space Warehouse Rate' },
        StagedItemGrid: { id: '40bj9sn7JHqai', caption: 'Staged Items' },
        StageHoldingItemGrid: { id: 'i7EMskpGXvByc', caption: 'Stage Holding Items' },
        StageQuantityItemGrid: { id: '0m0QMviBYWVYm', caption: 'Stage Quantity Items' },
        SubCategoryGrid: { id: 'vHMa0l5PUysXo', caption: 'Sub-Category' },
        SubPurchaseOrderItemGrid: { id: '8orfHWAhottty', caption: 'Sub-Purchase Order Items' },
        TransferOrderItemGrid: { id: 'RFgCJpybXoEb', caption: 'Transfer Order Item',
            menuItems: {
                QuikSearch: { id: '{16CD0101-28D7-49E2-A3ED-43C03152FEE6}' },
                CopyTemplate: { id: '{5E73772F-F5E2-4382-9F50-3272F4E79A25}' },
                //RefreshAvailability: { id: '{1065995B-3EF3-4B50-B513-F966F88570F1}' }
            }
        },
        VehicleMakeModelGrid: { id: 'kPPx0KctQjlXx', caption: 'Vehicle Make Model' },
        VehicleTypeWarehouseGrid: { id: '5Oz300mlivVCc', caption: 'Vehicle Type Warehouse' },
        VendorGrid: { id: 'cwytGLEcUzJdn', caption: 'Vendor' },
        VendorInvoiceExportBatchGrid: { id: 'QriRQnYpPbxn', caption: 'Vendor Invoice Export Batch' },
        VendorInvoiceItemGrid: { id: 'mEYOByOhi5yT0', caption: 'Vendor Invoice Item' },
        VendorInvoiceNoteGrid: { id: '8YECGu7qFOty', caption: 'Vendor Invoice Note' },
        VendorInvoicePaymentGrid: { id: 'cD51xfgax4oY', caption: 'Vendor Invoice Payment' },
        VendorInvoiceStatusHistoryGrid: { id: 'laMVsOwWI4Wkj', caption: 'Vendor Invoice Status History' },
        VendorNoteGrid: { id: 'zuywROD73X60O', caption: 'Vendor Note' },
        WardrobeInventoryColorGrid: { id: 'gJN4HKmkowSD', caption: 'Wardrobe Inventory Color' },
        WardrobeInventoryMaterialGrid: { id: 'l35woZUn3E5M', caption: 'Wardrobe Inventory Material' },
        WarehouseAvailabilityHourGrid: { id: '1iBtCdzhTkio4', caption: 'Warehouse Availability Hour' },
        WarehouseDepartmentGrid: { id: 'BlB26FHHFsaQx', caption: 'Warehouse Department' },
        WarehouseDepartmentUserGrid: { id: 'BlB26FHHFsaQx', caption: 'Warehouse Department User' },
        WarehouseGrid: { id: 'ICJcR2gOu04OB', caption: 'Warehouse' },
        WarehouseInventoryTypeGrid: { id: 'HRLS0W2gCu4lD', caption: 'Warehouse Inventory Type' },
        WarehouseOfficeLocationGrid: { id: 'B1kMAlpwQNPLG', caption: 'Warehouse Office Location' },
        WarehouseQuikLocateApproverGrid: { id: 'IBGJoUXyFbKmm', caption: 'Warehouse QuikLocate Approver' },
        WebAlertLogGrid: { id: 'x6SZhutIpRi2', caption: 'Web Alert Log' },
        WidgetGroupGrid: { id: 'BXv7mQIbXokIW', caption: 'Widget Group' },
        WidgetUserGrid: { id: 'CTzXYDyNzi8ET', caption: 'Widget User' }
    }
};
