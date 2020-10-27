var Constants = {
    isTrakitWorks: false,
    isRentalWorks: true,
    appId: 'D901DE93-EC22-45A1-BB4A-DD282CAF59FB',
    appCaption: 'TrakitWorks',
    appTitle: 'Trakit<span class="tiwred">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span>',
    MainMenu: {
        Reports: { id: 'F62D2B01-E4C4-4E97-BFAB-6CF2B872A4E4' },
        Settings: { id: 'CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485' }
    },
    Modules: {
        Administrator: {
            id: 'Administrator',
            caption: 'Administrator',
            nodetype: 'Category',
            children: {
                Alert:         { id: 'gFfpaR5mDAzX',  caption: 'Alert',          nav: 'module/alert', nodetype: 'Module' },
                CustomField:   { id: 'cZHPJQyBxolS', caption: 'Custom Fields', nav: 'module/customfield', nodetype: 'Module' },
                CustomForm:    { id: '11txpzVKVGi2',  caption: 'Custom Forms',    nav: 'module/customform', nodetype: 'Module' },
                CustomReportLayout: { id: 'EtrF5NHJ7dRg6', caption: 'Custom Report Layouts', nav: 'module/customreportlayout' },
                DuplicateRule: { id: 'v7oBspDLjli8',  caption: 'Duplicate Rules', nav: 'module/duplicaterule', nodetype: 'Module' },
                EmailHistory:  { id: '3XHEm3Q8WSD8z', caption: 'Email History',  nav: 'module/emailhistory', nodetype: 'Module' },
                Group:         { id: '0vP4rXxgGL1M',  caption: 'Group',          nav: 'module/group', nodetype: 'Module' },
                Hotfix:        { id: 'yeqaGIUYfYNX',  caption: 'Hotfix',         nav: 'module/hotfix', nodetype: 'Module' },
                Reports:       { id: '', caption: 'Reports', nav: 'module/reports', nodetype: 'Module' },
                Settings:      { id: '', caption: 'Settings', nav: 'module/settings', nodetype: 'Module' },
                User:          { id: 'r1fKvn1KaFd0u', caption: 'User',           nav: 'module/user', nodetype: 'Module' }
            }
        },
        Agent: {
            id: 'Agent',
            caption: 'Agent',
            nodetype: 'Category',
            children: {
                Contact: { id: '9ykTwUXTet46', caption: 'Contact', nav: 'module/contact' },
                Customer: { id: 'InSfo1f2lbFV', caption: 'Customer', nav: 'module/customer' },
                Deal: { id: '8WdRib388fFF', caption: 'Job', nav: 'module/deal' },
                Order: { id: 'U8Zlahz3ke9i', caption: 'Order', nav: 'module/order',
                    browse: {
                        menuItems: {
                            CancelUncancel: { id: '{CCD05127-481F-4352-A706-FEC6575DBEAF}' }
                        }
                    },
                    form: {
                        menuItems: {
                            Search: { id: '{0C8F88D0-F945-4B95-9E91-8704B2D04C30}' },
                            CopyOrder: { id: '{FFD9C063-FCF6-4A14-846D-4BD2887CF523}' },
                            PrintOrder: { id: '{B2A04C34-45BF-440E-B588-DD070CD65E59}' },
                            CreatePickList: { id: '{223FC05F-ABE5-4427-A459-CC66336400EC}' },
                            CancelUncancel: { id: '{127B392C-EF2C-4684-AE59-5A8B0ED6B518}' },
                            OrderStatus: { id: '{ECFE0CE4-3424-44EB-B213-29409CE3D595}' },
                            CheckOut: { id: '{3C9AF5C2-F7FB-44C8-B3B9-FF09F40CC58F}' },
                            CheckIn: { id: '{790B9193-AAFC-4EEE-9D5E-34D1F8DDD603}' }
                        }
                    }
                },
                PurchaseOrder: { id: '9a0xOMvBM7Uh9', caption: 'Purchase Order', nav: 'module/purchaseorder',
                    form: {
                        menuItems: {
                            Search: { id: '{932A7318-3B62-4AA5-A0C5-904BB4BB9F17}' },
                            ReceiveFromVendor: { id: '{B719A865-4B0A-4C60-A1D9-3614EC6D0515}' },
                            ReturnToVendor: { id: '{6D3E64B9-6B08-47B0-8F51-CFAE12651630}' },
                            AssignBarCodes: { id: '{D7722622-BBAE-4FE7-9DAD-1E2A4419CC3D}' }
                        }
                    }    
                },
                Quote: { id: 'jFkSBEur1dluU', caption: 'Request', nav: 'module/quote',
                    browse: {
                        menuItems: {
                            CancelUncancel: { id: '{86412C14-9351-4C03-8D13-5338AB0EAEC8}' }
                        }
                    },
                    form: {
                        menuItems: {
                            CopyQuote: { id: '{85B7FD07-74CD-4320-AFB0-9718EE5C8CDD}' },
                            Search: { id: '{66EDC5EA-DC03-4A5B-82F2-A41D2B8A34E4}' },
                            PrintQuote: { id: '{CC3F3DB4-21A4-4E70-8992-30886B2D1515}' },
                            CreateOrder: { id: '{F9B8EAC3-07BD-4286-B4F8-CCBE53710B1F}' },
                            CancelUncancel: { id: '{8F3E3263-B5AE-4CB4-8CBB-E9F680AA8C11}' }
                        }
                    }    
                },
                Vendor: { id: 'cwytGLEcUzJdn', caption: 'Vendor', nav: 'module/vendor' }
            }
        },
        Inventory: {
            id: 'Inventory',
            caption: 'Inventory',
            nodetype: 'Category',
            children: {
                Asset: { id: 'kSugPLvkuNsH', caption: 'Asset', nav: 'module/asset' },
                InventoryItem: { id: '3ICuf6pSeBh6G', caption: 'Inventory Item', nav: 'module/inventoryitem' },
                Repair: { id: 't4gfyzLkSZhyc', caption: 'Repair Order', nav: 'module/repair',
                    browse: {
                        menuItems: {
                            Void: { id: '{4F0A3AF7-5CDF-4CCB-B7DF-8DFAF14AA516}' }
                        }
                    },
                    form: {
                        menuItems: {
                            Estimate: { id: '{8733EA9A-790E-4DF1-BFF2-13302A7DCD26}' },
                            Complete: { id: '{136159CD-A50A-4BCA-AA28-4AB3A1BDC1CB}' },
                            Void: { id: '{B048566D-9A69-488E-B7AA-BF243821E4B0}' },
                            ReleaseItems: { id: '{7A6B5CFD-1DFA-44BB-8B60-3FEE1E347654}' }
                        }
                    }
                }
            }
        },
        Warehouse: {
            id: 'Warehouse',
            caption: 'Warehouse',
            nodetype: 'Category',
            children: {
                AssignBarCodes: { id: '7UU96BApz2Va', caption: 'Assign Bar Codes', nav: 'module/assignbarcodes' },
                CheckIn: { id: 'krnJWTUs4n5U', caption: 'Check-In', nav: 'module/checkin' },
                Contract: { id: 'Z8MlDQp7xOqu', caption: 'Contract', nav: 'module/contract', 
                    form: {
                        menuItems: { 
                            PrintOrder: { id: '{8E3727B5-8E75-4648-AE52-FB3BD7729F02}' } 
                        } 
                    }
                },
                Exchange: { id: 'IQS4rxzIVFl', caption: 'Exchange', nav: 'module/exchange' },
                OrderStatus: { id: 'C8Ycf0jvM2U9', caption: 'Order Status', nav: 'module/orderstatus' },
                PickList: { id: 'bggVQOivrIgi', caption: 'Pick List', nav: 'module/picklist',
                    browse: {
                        menuItems: {
                            PrintPickList: { id: '{E6EA3633-4CB8-4F5F-8EB4-C29D41C1B394}' }
                        }
                    },
                    form: {
                        menuItems: {
                            PrintPickList: { id: '{E4C83683-8B4A-46F4-8A47-E11416AB10E7}' },
                            CancelPickList: { id: '{DDC5BB9F-D214-458C-9559-67F2900DD011}' }
                        }
                    }
                },
                ReceiveFromVendor: { id: 'MtgBxCKWVl7m', caption: 'Receive From Vendor', nav: 'module/receivefromvendor' },
                ReturnToVendor: { id: 'cCxoTvTCDTcm', caption: 'Return To Vendor', nav: 'module/returntovendor' },
                StagingCheckout: { id: 'H0sf3MFhL0VK', caption: 'Staging / Check-Out', nav: 'module/checkout' }
            }
        },
        Home: {
            id: 'Home',
            caption: 'Home',
            nodetype: 'Category',
            children: {
                CreatePickList: { id: '1407A536-B5C9-4363-8B54-A56DB8CE902D', caption: 'Create Pick List', nav: 'module/createpicklist' },
                ExpendableItem: { id: 'ShjGAzM2Pq3kk', caption: 'Expendable Item', nav: 'module/expendableitem' },
                SubWorksheet: { id: '2227B6C3-587D-48B1-98B6-B9125E0E4D9D', caption: 'Sub Worksheet', nav: '' },
                SuspendedSession: { id: '5FBE7FF8-3770-48C5-855C-4320C961D95A', caption: 'Suspended Session', nav: 'module/suspendedsession' }
            }
        },
        Reports: {
            id: 'Reports',
            caption: 'Reports',
            nodetype: 'Category',
            children: {
                //AccountingReports: {
                //    id: 'Reports.AccountingReports',
                //    caption: 'Accounting Reports',
                //    nodetype: 'Category',
                //    children: {
                //        ArAgingReport: { id: 'KHw5yX5TubQ', caption: 'A/R Aging', nav: 'reports/aragingreport', nodetype: 'Module', description: 'List unpaid Invoices, and their corresponding aging totals.  Report is subtotalled by Deal and Customer.' },
                //        DailyReceiptsReport: { id: 'OLyFIS7rBvr8', caption: 'Daily Receipts', nav: 'reports/dailyreceiptsreport', nodetype: 'Module', description: 'List Daily Receipts.  Report is subtotalled by Deal and Customer.' },
                //        GlDistributionReport: { id: 'ClMQ5QkZq4PY', caption: 'G/L Distribution', nav: 'reports/gldistributionreport', nodetype: 'Module', description: 'Summarize transaction totals by Account over a date range.' },
                //    }
                //},
                //BillingReports: {
                //    id: 'Reports.Billing',
                //    caption: 'Billing Reports',
                //    nodetype: 'Category',
                //    children: {
                //        AgentBillingReport: { id: 'qx65UNbCoUW', caption: 'Agent Billing', nav: 'reports/agentbillingreport', nodetype: 'Module', description: 'Shows Invoice Activity Totals, subtotalled by Agent.' },
                //        BillingAnalysisReport: { id: 'c2AwOP9UmJFw', caption: 'Billing Analysis', nav: 'reports/billinganalysisreport', nodetype: 'Module', description: 'List all Orders created within a specified date range, showing Order Total vs. Billed Total and Estimated Cost Total vs. Vendor Invoice Total.' },
                //        BillingProgressReport: { id: 'Y5ssGcXnxL2', caption: 'Billing Progress', nav: 'reports/billingprogressreport', nodetype: 'Module', description: 'List all Orders and their percentage of total Billing, subtotalled by Customer and Deal.' },
                //        BillingStatementReport: { id: 'wd7t1jPI4ztQH', caption: 'Billing Statement', nav: 'reports/billingstatementreport', nodetype: 'Module', description: 'Create a printable Billing Statement which itemizes all Billing and Receipt activity per Deal over a specific date range.' },
                //        CreateInvoiceProcessReport: { id: 'qhb1dkFRrS6T', caption: 'Create Invoice Process', nav: 'reports/createinvoiceprocessreport', nodetype: 'Module', description: 'List all Invoices and Exceptions based on a Creation Batch.' },
                //        InvoiceDiscountReport: { id: 'PwAjD9UxITIp', caption: 'Invoice Discount', nav: 'reports/invoicediscountreport', nodetype: 'Module', description: 'List all Invoices which have a Discount.' },
                //        InvoiceReport: { id: 'o5nbWmTr7xy0n', caption: 'Invoices', nav: 'reports/invoicereport', nodetype: 'Module', description: 'Invoice document.' },
                //        InvoiceSummaryReport: { id: 'LeLwkS6yUBfV', caption: 'Invoice Summary', nav: 'reports/invoicesummaryreport', nodetype: 'Module', description: 'List all Invoices for a specific date range, subtotalled by Customer and Deal.' },
                //        ProfitLossReport: { id: 'gUCH9E3ZYdRIm', caption: 'Profit and Loss', nav: 'reports/profitlossreport', nodetype: 'Module', description: 'List Profit or Loss for all Orders that start within a specified date range.' },
                //        ProjectManagerBillingReport: { id: 'lTDdAGi63jRVL', caption: 'Project Manager Billing', nav: 'reports/projectmanagerbillingreport', nodetype: 'Module', description: 'Shows Invoice Activity Totals, subtotalled by Project Manager.' },
                //        SalesQuoteBillingReport: { id: 'L8WwjlirkzC55', caption: 'Sales Quote Billing Analysis', nav: 'reports/salesquotebillingreport', nodetype: 'Module', description: 'List Profitability of Sales Quotes.' },
                //        SalesRepresentativeBillingReport: { id: 'SgF8AMJVjKARN', caption: 'Sales Representative Billing', nav: 'reports/salesrepresentativebillingreport', nodetype: 'Module', description: 'Shows Invoice Activity Totals, subtotalled by Sales Representative.' },
                //        SalesTaxCanadaReport: { id: 'UX8GjEXNteJz', caption: 'Sales Tax - Canada', nav: 'reports/salestaxcanadareport', nodetype: 'Module', description: 'List all Invoices for a specific date range, grouped and totaled by Tax Option' },
                //        SalesTaxUSAReport: { id: 'TaaSgS14rWsCL', caption: 'Sales Tax - USA', nav: 'reports/salestaxusareport', nodetype: 'Module', description: 'List all Invoices for a specific date range, grouped and totaled by Tax Option' },
                //    }
                //},
                //ChangeAuditReports: {
                //    id: 'Reports.ChangeAuditReports',
                //    caption: 'Change Audit Reports',
                //    nodetype: 'Category',
                //    children: {
                //        ChangeAuditReport: { id: 'hfM1GHEQCWBJ', caption: 'Change Audit', nav: 'reports/changeauditreport', nodetype: 'Module', description: 'List all data changes made to any module by any user over a specific date range.' },
                //    }
                //},
                //ChargeProcessingReports: {
                //    id: 'Reports.ChargeProcessingReports',
                //    caption: 'Charge Processing Reports',
                //    nodetype: 'Category',
                //    children: {
                //        DealInvoiceBatchReport: { id: 't3byQWNa3hZ4H', caption: 'Deal Invoice Batch', nav: 'reports/dealinvoicebatchreport', nodetype: 'Module', description: 'List all Invoices processed in a selected Batch.' },
                //        ReceiptBatchReport: { id: 'jB7p9OvmCibhx', caption: 'Receipt Batch', nav: 'reports/receiptbatchreport', nodetype: 'Module', description: 'List all Receipts processed in a selected Batch.' },
                //        VendorInvoiceBatchReport: { id: '6gc3QbSGD5BX', caption: 'Vendor Invoice Batch', nav: 'reports/vendorinvoicebatchreport', nodetype: 'Module', description: 'List all Vendor Invoices processed in a selected Batch.' },
                //    }
                //},
                ContractReports: {
                    id: 'Reports.ContractReports',
                    caption: 'Contract Reports',
                    nodetype: 'Category',
                    children: {
                        //ExchangeContractReport: { id: 'MAR5QYKd01qwx', caption: 'Exchange Contract', nav: 'reports/exchangecontractreport', nodetype: 'Module', description: 'Exchange Contract document.' },
                        //ContractRevisionReport: { id: 'ZDRzzkgcqTb57', caption: 'Contract Revision Activity', nav: 'reports/contractrevisionreport', nodetype: 'Module', description: 'List all modifications made to Contracts over a specified date range.' },
                        //InContractReport: { id: 'DQ0kEL13GYyEg', caption: 'In Contract', nav: 'reports/Incontractreport', nodetype: 'Module', description: 'Check-In Contract document.' },
                        //LostContractReport: { id: 'F6uNUdehIUVSB', caption: 'Lost Contract', nav: 'reports/lostcontractreport', nodetype: 'Module', description: 'Lost Contract document.' },
                        OutContractReport: { id: 'p2tH0HSDc930', caption: 'Out Contract', nav: 'reports/outcontractreport', nodetype: 'Module', description: 'Check-Out Contract document.' },
                        //ReceiveContractReport: { id: 'KXKz4J1x0t71K', caption: 'Receive Contract', nav: 'reports/receivecontractreport', nodetype: 'Module', description: 'Receive Contract document.' },
                        //ReturnContractReport: { id: 'gkK2yG12BU8kk', caption: 'Return Contract', nav: 'reports/returncontractreport', nodetype: 'Module', description: 'Return Contract document.' },
                        ReturnListReport: { id: 'cYzbGgLOUMRz', caption: 'Return List', nav: 'reports/returnlistreport', nodetype: 'Module', description: 'Return List document.' },
                        //TransferManifestReport: { id: 'wf68uZe0JPXDt', caption: 'Transfer Manifest ', nav: 'reports/transfermanifesttreport', nodetype: 'Module', description: 'Transfer Manifest document.' },
                        //TransferReceiptReport: { id: 'F6XtBsrOH4cjm', caption: 'Transfer Receipt', nav: 'reports/transferreceiptreport', nodetype: 'Module', description: 'Transfer Receipt document.' },
                    }
                },
                //CrewReports: {
                //    id: 'Reports.CrewReports',
                //    caption: 'Crew Reports',
                //    nodetype: 'Category',
                //    children: {
                //        CrewSignInReport: { id: 'kvDH1delOiUT', caption: 'Crew Sign-In', nav: 'reports/crewsigninreport', nodetype: 'Module', description: 'Lists the Sign In and Out activity for Crew members over a specific date range.' },
                //    }
                //},
                DealReports: {
                    id: 'Reports.DealReports',
                    caption: 'Deal Reports',
                    nodetype: 'Category',
                    children: {
                        //CreditsOnAccountReport: { id: '9JCPjgemNM8D', caption: 'Credits On Account', nav: 'reports/creditsonaccountreport', nodetype: 'Module', description: 'List each Deal that has an outstanding Credit Memo, Depleting Deposit, or Overpayment.' },
                        //CustomerRevenueByMonthReport: { id: '40SdfVGkZPtA6', caption: 'Customer Revenue By Month', nav: 'reports/customerrevenuebymonthreport', nodetype: 'Module', description: 'List all revenue per Customer, per Deal, per Month.' },
                        //CustomerRevenueByTypeReport: { id: 'mIieqY1nHrJP', caption: 'Customer Revenue By Type', nav: 'reports/customerrevenuebytypereport', nodetype: 'Module', description: 'List each Invoice for a specific date range.  Revenue amounts are broken down by Activity Type (ie. Rentals, Sales, etc).  Revenue is subtotalled by Customer and Deal.' },
                        //DealInvoiceDetailReport: { id: 'u5mTLIfmUKWfH', caption: 'Deal Invoice Detail', nav: 'reports/dealinvoicedetailreport', nodetype: 'Module', description: 'List each Invoice for a specific date range, grouped by Deal and Order.  Review revenue broken down by Activity Type (ie. Rental, Sales, etc).  Analyze profits net of Sub Vendor Costs.' },
                        DealOutstandingItemsReport: { id: 'i5RTw0gXIWhU', caption: 'Deal Outstanding Items', nav: 'reports/dealoutstandingitemsreport', nodetype: 'Module', description: 'List all items still Checked-Out to a specific Deal.' },
                        //OrdersByDealReport: { id: 'ltXuVM54H5dYe', caption: 'Orders By Deal', nav: 'reports/ordersbydealreport', nodetype: 'Module', description: 'List all Orders for specific date ranges, grouped by Deal.' },
                        //ReturnReceiptReport: { id: 'N9wTkNBeCgJQz', caption: 'Return Receipt', nav: 'reports/returnreceiptreport', nodetype: 'Module', description: 'List all inventory checked-in without an Order (ie. Return Receipt) including all reconciliations.' },
                    }
                },
                //MultiLocationReports: {
                //    id: 'Reports.MultiLocationReports',
                //    caption: 'Multi Location Reports',
                //    nodetype: 'Category',
                //    children: {
                //        TransferReport: { id: 'XerdM2ZaD51uw', caption: 'Transfer Report', nav: 'reports/transferreport', nodetype: 'Module', description: 'List all Transfer Orders as well as transferred Inventory over a specific date range.' },
                //    }
                //},
                OrderReports: {
                    id: 'Reports.OrderReports',
                    caption: 'Order Reports',
                    nodetype: 'Category',
                    children: {
                        //IncomingShippingLabel: { id: 'U2RQ1fjYwkIZ6', caption: 'Incoming Shipping Label', nav: 'reports/incomingshippinglabel', nodetype: 'Module', description: 'Print Incoming Shipping Label' },
                        LateReturnsReport: { id: 'gOtEnqxlXIOt', caption: 'Late Return / Due Back', nav: 'reports/latereturnsreport', nodetype: 'Module', description: 'List all items that are Late or Due Back on a specific date.  Data is subtotalled by Order and Deal.' },
                        //ManifestReport: { id: '8lSfSBPXlYh5', caption: 'Order Item Value Sheet', nav: 'reports/manifestreport', nodetype: 'Module', description: 'Gets item manifest information' },
                        //OrderConflictReport: { id: 'kXV9ZCJogLiwe', caption: 'Availability Item Conflict', nav: 'reports/orderconflictreport', nodetype: 'Module', description: 'List all Negative and Positive Availability Conflicts over a specified date range.' },
                        OrderReport: { id: 'Q89Ni6FvVL92', caption: 'Print Order', nav: 'reports/orderreport', nodetype: 'Module', description: 'Order document.' },
                        OrderStatusDetailReport: { id: 'EY9uBXnssjv1', caption: 'Order Status Detail', nav: 'reports/orderstatusdetailreport', nodetype: 'Module', description: 'Gets status detail for specified order.' },
                        OrderStatusSummaryReport: { id: '44jjIwel6TP0d', caption: 'Order Status Summary', nav: 'reports/orderstatussummaryreport', nodetype: 'Module', description: 'Gets status summary for specified order.' },
                        //OutgoingShippingLabel: { id: 'tzTGi6kzrelFp', caption: 'Outgoing Shipping Label', nav: 'reports/outgoingshippinglabel', nodetype: 'Module', description: 'Print Outgoing Shipping Label' },
                        //OutstandingSubRentalReport: { id: 'NCFNATdQRx5E', caption: 'Outstanding Sub-Rental', nav: 'reports/outstandingsubrentalreport', nodetype: 'Module', description: 'List all Sub-Rental items currently Staged, Out, or in Holding.' },
                        PickListReport: { id: 'Rk38wHmvgXTg', caption: 'Pick List', nav: 'reports/picklistreport', nodetype: 'Module', description: 'Pick List document.' },
                        //QuikActivityReport: { id: '4hamhMOWKXD9', caption: 'QuikActivity', nav: 'reports/quikactivityreport', nodetype: 'Module', description: 'List all Quote, Order, Transfer, Purchase, and Repair activities over a specific date range.' },
                        QuoteReport: { id: 'SZ80uvR5NjI7', caption: 'Print Quote', nav: 'reports/quotereport', nodetype: 'Module', description: 'Quote document.' },
                        //QuoteOrderMasterReport: { id: 'yx1quQL9wJ9mg', caption: 'Quote / Order Master Report', nav: 'reports/quoteordermasterreport', nodetype: 'Module', description: 'List all Quotes and Orders, filtered by date range, and grouped by Deal.' },
                        //SubSalesStagedItemsReport: { id: '2GIJvJlbIFQN', caption: 'Sub-Sales Staged Items', nav: 'reports/subsalesstageditemsreport', nodetype: 'Module', description: 'List all Sub Sale Inventory currently Staged on Orders.' },
                    }
                },
                //PartsInventoryReports: {
                //    id: 'Reports.PartsInventoryReports',
                //    caption: 'Parts Inventory Reports',
                //    nodetype: 'Category',
                //    children: {
                //        PartsInventoryAttributesReport: { id: 'qyi6CvOObXr', caption: 'Parts Inventory Attributes', nav: 'reports/partsinventoryattributesreport', nodetype: 'Module', description: 'List all Parts Inventory with their Attributes and Values.' },
                //        PartsInventoryCatalogReport: { id: '5KHdCAEfEbEVo', caption: 'Parts Inventory Catalog', nav: 'reports/partsinventorycatalogreport', nodetype: 'Module', description: 'List all Parts Inventory, current Rates, and Quantity on Hand.' },
                //        PartsInventoryChangeReport: { id: 'PYSHE3LpB2yf', caption: 'Parts Inventory Change', nav: 'reports/partsinventorychangereport', nodetype: 'Module', description: 'List all changes to Parts Inventory quantities over a date range.' },
                //        PartsInventoryPurchaseHistoryReport: { id: 's3JHeEZYfEu', caption: 'Parts Inventory Purchase History', nav: 'reports/partsinventorypurchasehistoryreport', nodetype: 'Module', description: 'List all Parts Inventory Purchase History.' },
                //        PartsInventoryReorderReport: { id: '2KtGO1TfYVe', caption: 'Parts Inventory Reorder', nav: 'reports/partsinventoryreorderreport', nodetype: 'Module', description: 'List all Parts Inventory to reorder. Filter items with quantity levels at or below their Reorder Point.' },
                //        PartsInventoryTransactionReport: { id: 'p4MdfH1e38wLE', caption: 'Parts Inventory Transactions', nav: 'reports/partsinventorytransactionreport', nodetype: 'Module', description: 'List all Parts Inventory Transactions, including Cost and Price, over a specific date range.' },
                //    }
                //},
                RentalInventoryReports: {
                    id: 'Reports.RentalInventoryReports',
                    caption: 'Rental Inventory Reports',
                    nodetype: 'Category',
                    children: {
                        //FixedAssetBookValueReport: { id: '03HEFOHpPOEm', caption: 'Fixed Asset Book Value', nav: 'reports/fixedassetbookvaluereport', nodetype: 'Module', description: 'List all Fixed Asset Inventory items and get a current or historical Book Value.' },
                        //FixedAssetDepreciationReport: { id: '3KJaC5Kc7149', caption: 'Fixed Asset Depreciation', nav: 'reports/fixedassetdepreciationreport', nodetype: 'Module', description: 'List Depreciation for all Fixed Asset Inventory items over a given date range.' },
                        //RentalInventoryActivityByDateReport: { id: 'nh7dhdTKT9U0Q', caption: 'Rental Inventory Activity By Date', nav: 'reports/rentalinventoryactivitybydatereport', nodetype: 'Module', description: 'List all Rental Inventory Out and In Activity over a specific date range.' },
                        RentalInventoryAttributesReport: { id: 'CgvKJwiD2ew', caption: 'Rental Inventory Attributes', nav: 'reports/rentalinventoryattributesreport', nodetype: 'Module', description: 'List all Rental Inventory with their Attributes and Values.' },
                        //RentalInventoryAvailabilityReport: { id: 'LQj6R2GQBfLfS', caption: 'Rental Inventory Availability', nav: 'reports/rentalinventoryavailabilityreport', nodetype: 'Module', description: 'List all Rental Inventory with projected Availability for up to 30 days.' },
                        RentalInventoryCatalogReport: { id: '9cNlsCNNeCPV', caption: 'Rental Inventory Catalog', nav: 'reports/rentalinventorycatalogreport', nodetype: 'Module', description: 'List all Rental Inventory, current Rates, Replacement Cost, and Owned Quantity.' },
                        RentalInventoryChangeReport: { id: '38oJf9KIuq4x', caption: 'Rental Inventory Change', nav: 'reports/rentalinventorychangereport', nodetype: 'Module', description: 'List all changes to Rental Inventory quantities over a date range.' },
                        //RentalInventoryMasterReport: { id: 'hVovGdrneLrzq', caption: 'Rental Inventory Master', nav: 'reports/rentalinventorymasterreport', nodetype: 'Module', description: 'List all Rental Inventory with their Quantities, Status, Cost, and Value.' },
                        //RentalInventoryMovementReport: { id: '3fjXnYy5gTPR', caption: 'Rental Inventory Movement', nav: 'reports/rentalinventorymovementreport', nodetype: 'Module', description: 'List all Rental Inventory with changes in quantity over a date range.' },
                        RentalInventoryPurchaseHistoryReport: { id: 'kI5HgFqlPzr', caption: 'Rental Inventory Purchase History', nav: 'reports/rentalinventorypurchasehistoryreport', nodetype: 'Module', description: 'List all Rental Inventory Purchase History.' },
                        RentalInventoryQCRequiredReport: { id: 'LzwbAbB86Hlvs', caption: 'Rental Inventory QC Required', nav: 'reports/rentalinventoryqcrequiredreport', nodetype: 'Module', description: 'List all Rental Inventory that require QC.' },
                        //RentalInventoryRepairHistoryReport: { id: 'bLX4YBftwJvw', caption: 'Rental Inventory Repair History', nav: 'reports/rentalinventoryrepairhistoryreport', nodetype: 'Module', description: 'List all Rental Inventory that has been In Repair over a specified date range.' },
                        //RentalInventoryStatusAndRevenueReport: { id: 'uCEqKxMHdcT', caption: 'Rental Inventory Status and Revenue', nav: 'reports/rentalinventorystatusandrevenuereport', nodetype: 'Module', description: 'List all Rental Inventory including current status counts and revenue over a given date range.' },
                        //RentalInventoryUnusedItemsReport: { id: 'Xb4w7HOWs9vww', caption: 'Rental Inventory Unused Items', nav: 'reports/rentalinventoryunuseditemsreport', nodetype: 'Module', description: 'List all Rental Inventory that has not been rented for the the specified number of days.' },
                        RentalInventoryUsageReport: { id: 'BplzDmql7vG48', caption: 'Rental Inventory Usage', nav: 'reports/rentalinventoryusagereport', nodetype: 'Module', description: 'List all Rental Inventory and Usage, with owned revenue and sub-rental revenue.' },
                        RentalInventoryValueReport: { id: 'UZkDL1Yyby6kN', caption: 'Rental Inventory Value', nav: 'reports/rentalinventoryvaluereport', nodetype: 'Module', description: 'List all Rental Inventory and get a current value, historical value, or change in value over a date range.' },
                        //RentalLostAndDamagedBillingHistoryReport: { id: '37O4QUBc8nM8t', caption: 'Rental Lost And Damaged Billing History', nav: 'reports/rentallostanddamagedbillinghistoryreport', nodetype: 'Module', description: 'List all Billing for Lost and Damaged inventory.' },
                        RetiredRentalInventoryReport: { id: 'L8qgvB6nFhAV', caption: 'Retired Rental Inventory', nav: 'reports/retiredrentalinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory Retired during a specified date range.' },
                        ReturnedToInventoryReport: { id: 'jXgzMRhDOFMY', caption: 'Returned To Inventory', nav: 'reports/returnedtoinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory Returned during a specified date range.' },
                        //ReturnOnAssetReport: { id: '15TIjoDzY09G', caption: 'Return On Asset', nav: 'reports/returnonassetreport', nodetype: 'Module', description: 'Calculate the Revenue, Value, Utilization, and Return on Asset for all Rental Inventory for various date ranges.' },
                        UnretiredRentalInventoryReport: { id: 'UaCdIoXMNFyiT', caption: 'Unretired Rental Inventory', nav: 'reports/unretiredrentalinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory unretired during a specified date range.' },
                        ValueOfOutRentalInventoryReport: { id: 'n4gdQ6awebnX', caption: 'Value Of Out Rental Inventory', nav: 'reports/valueofoutrentalinventoryreport', nodetype: 'Module', description: 'List all Rental Inventory Out on a specific date, including Cost and Value for each.' },
                    }
                },
                //RepairOrderReports: {
                //    id: 'Reports.RepairOrderReports',
                //    caption: 'Repair Order Reports',
                //    nodetype: 'Category',
                //    children: {
                //        RepairOrderReport: { id: 'tqBQZCgmOUDcE', caption: 'Repair Order', nav: 'reports/repairorderreport', nodetype: 'Module', description: 'Repair Order Document.' },
                //        RepairOrderStatusReport: { id: 'KFP6Nq17ZuDPO', caption: 'Repair Order Status', nav: 'reports/repairorderstatusreport', nodetype: 'Module', description: 'List all Repair Orders, including Damage Notes.  Filter by Status, Type, Priority, Days In Repair, and more.' },
                //        RepairTag: { id: 'TNvVB0kI42ngF', caption: 'Repair Tag', nav: 'reports/repairtag', nodetype: 'Module', description: 'Print a specialized tag/label with information about the item to be repaired.' },
                //    }
                //},
                //SalesInventoryReports: {
                //    id: 'Reports.SalesInventoryReports',
                //    caption: 'Sales Inventory Reports',
                //    nodetype: 'Category',
                //    children: {
                //        SalesBackorderReport: { id: 'KMdFiy9SEIQEU', caption: 'Sales Inventory Backorder', nav: 'reports/salesbackorderreport', nodetype: 'Module', description: 'List all Orders with Sales Inventory partially checked-out and items still on backorder.' },
                //        SalesHistoryReport: { id: '0qFfabkzl5Vi', caption: 'Sales History', nav: 'reports/saleshistoryreport', nodetype: 'Module', description: 'List all Sales over a specific date range.' },
                //        SalesInventoryAttributesReport: { id: 'IKtnIOnt3tS', caption: 'Sales Inventory Attributes', nav: 'reports/salesinventoryattributesreport', nodetype: 'Module', description: 'List all Sales Inventory with their Attributes and Values.' },
                //        SalesInventoryCatalogReport: { id: 'psk5v7y4gImL', caption: 'Sales Inventory Catalog', nav: 'reports/salesinventorycatalogreport', nodetype: 'Module', description: 'List all Sales Inventory, current Rates, and Quantity on Hand.' },
                //        SalesInventoryChangeReport: { id: 'ICUgr2QYcrPE', caption: 'Sales Inventory Change', nav: 'reports/salesinventorychangereport', nodetype: 'Module', description: 'List all changes to Sales Inventory quantities over a date range.' },
                //        SalesInventoryMasterReport: { id: 'iZlMgIbbp6iK2', caption: 'Sales Inventory Master', nav: 'reports/salesinventorymasterreport', nodetype: 'Module', description: 'List all Sales Inventory with their Quantities, Costs, and Revenue Totals.' },
                //        SalesInventoryPurchaseHistoryReport: { id: 'mSjHvRjNvI0', caption: 'Sales Inventory Purchase History', nav: 'reports/salesinventorypurchasehistoryreport', nodetype: 'Module', description: 'List all Sales Inventory Purchase History.' },
                //        SalesInventoryReorderReport: { id: 'h1ag1lcZCgd', caption: 'Sales Inventory Reorder', nav: 'reports/salesinventoryreorderreport', nodetype: 'Module', description: 'List all Sales Inventory to reorder. Filter items with quantity levels at or below their Reorder Point.' },
                //        SalesInventoryRepairHistoryReport: { id: 'ZpC5S5veTzD0', caption: 'Sales Inventory Repair History', nav: 'reports/salesinventoryrepairhistoryreport', nodetype: 'Module', description: 'List all Sales Inventory that has been In Repair over a specified date range.' },
                //        SalesInventoryTransactionReport: { id: 'uUxuUabbhO7ah', caption: 'Sales Inventory Transactions', nav: 'reports/salesinventorytransactionreport', nodetype: 'Module', description: 'List all Sales Inventory Transactions, including Cost and Price, over a specific date range.' },
                //    }
                //},
                VendorReports: {
                    id: 'Reports.VendorReports',
                    caption: 'Vendor Reports',
                    nodetype: 'Category',
                    children: {
                        //PurchaseOrderMasterReport: { id: 'zJJl8Frw1U7U7', caption: 'Purchase Order Master', nav: 'reports/purchaseordermasterreport', nodetype: 'Module', description: 'List all Purchase Orders for a specified date range.' },
                        //PurchaseOrderReport: { id: 'ZcNjp0seMeWi', caption: 'Purchase Order', nav: 'reports/purchaseorderreport', nodetype: 'Module', description: 'Purchase Order Document.' },
                        PurchaseOrderReturnList: { id: 'tM8if9Yclmiv6', caption: 'Purchase Order Return List', nav: 'reports/purchaseorderreturnlist', nodetype: 'Module', description: 'Purchase Order Return List.' },
                        //PurchaseOrderSummaryReport: { id: 'yrvMp4sG8CzF', caption: 'Purchase Order Summary', nav: 'reports/purchaseordersummaryreport', nodetype: 'Module', description: 'List all Purchase Orders for a specified date range.' },
                        //SubItemStatusReport: { id: 'fL9dlJfzzJf8U', caption: 'Sub Item Status', nav: 'reports/subitemstatusreport', nodetype: 'Module', description: 'List all Sub-Rentals, Sub-Sales, Sub-Misc, and Sub-Labor over a specified date range. Evaluate profitability of each.' },
                        //SubRentalBillingAnalysisReport: { id: 'KIE1O1i2tvtsu', caption: 'Sub-Rental Billing Analysis', nav: 'reports/subrentalbillinganalysisreport', nodetype: 'Module', description: 'List all Sub-Rental Billing activity and compare Deal billing amounts with Vendor billing amounts.' },
                        //VendorInvoiceSummaryReport: { id: 'J2Lczm4sL14Ze', caption: 'Vendor Invoice Summary', nav: 'reports/vendorinvoicesummaryreport', nodetype: 'Module', description: 'List all Invoices for a specific date range, subtotalled by Purchase Order and Deal.' },
                    }
                },
                //WarehouseReports: {
                //    id: 'Reports.WarehouseReports',
                //    caption: 'Warehouse Reports',
                //    nodetype: 'Category',
                //    children: {
                //        RateUpdateReport: { id: 'Nxb4NonfG10c9', caption: 'Rate Update', nav: 'reports/rateupdatereport', nodetype: 'Module', description: 'List all rates updated in a selected Batch.' },
                //        WarehouseDispatchReport: { id: 'gs5q3h0v0HzXF', caption: 'Warehouse Dispatch', nav: 'reports/warehousedispatchreport', nodetype: 'Module', description: 'List all Warehouse Dispatch Activities for a specific date range.' },
                //        WarehouseInboundReport: { id: 'gQLKq8L9zC32b', caption: 'Warehouse Inbound', nav: 'reports/warehouseinboundreport', nodetype: 'Module', description: 'List all Warehouse Inbound Activities for a specific date range.' },
                //        WarehouseOutboundReport: { id: 'gPuvfa4B1tHuE', caption: 'Warehouse Outbound', nav: 'reports/warehouseoutboundreport', nodetype: 'Module', description: 'List all Warehouse Outbound Activities for a specific date range.' },
                //    }
                //}
            }

        },
        Settings: {
            id: 'Settings',
            caption: 'Settings',
            nodetype: 'Category',
            children: {
                AddressSettings: {
                    caption: 'Address',
                    id: 'Settings.AddressSettings',
                    nodetype: 'Category',
                    children: {
                        Country: { id: 'FV8c2ibthqUF', caption: 'Country', nav: 'settings/addresssettings/country', nodetype: 'Module', description: 'List Countries to relate to your Customers, Deals, and Vendors' },
                        State: { id: 'JW3yCGldGTAqC', caption: 'State / Province', nav: 'settings/addresssettings/state', nodetype: 'Module', description: 'List States to relate to your Customers, Deals, and Vendors' }
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
                        ContactTitle: { id: 'PClZ3w0VUnPt', caption: 'Contact Title', nav: 'settings/contactsettings/contacttitle', description: '' }
                    }
                },
                CurrencySettings: {
                    caption: 'Currency',
                    id: 'Settings.CurrencySettings',
                    children: {
                        Currency: { id: 'xpyZJmmju0uB', caption: 'Currency', nav: 'settings/currencysettings/currency', nodetype: 'Module', description: '' }
                    }
                },
                DealSettings: {
                    caption: 'Deal',
                    id: 'SetSettings.DealSettings',
                    nodetype: 'Category',
                    children: {
                        DealClassification: { id: 'uRRVPMAFf61J', caption: 'Deal Classification', nav: 'settings/currencysettings/dealclassification', nodetype: 'Module', description: '' },
                        DealStatus: { id: 'CHOTGdFVlnFK', caption: 'Deal Status', nav: 'settings/currencysettings/dealstatus', nodetype: 'Module', description: '' },
                        DealType: { id: 'jZCS1X5BzeyS', caption: 'DealType', nav: 'settings/currencysettings/dealtype', nodetype: 'Module', description: '' }
                    }
                },
                DepartmentSettings: {
                    caption: 'Department',
                    id: 'Settings.CompanyDepartmentSettings',
                    nodetype: 'Category',
                    children: {
                        Department: { id: 'kuYqT9d6TDEg', caption: 'Department', nav: 'module/companydepartment', nodetype: 'Module', description: '' },
                    }
                },
                InventorySettings: {
                    caption: 'Inventory',
                    id: 'Settings.InventorySettings',
                    nodetype: 'Category',
                    children: {
                        Attribute:                 { id: 'Ok4Yh4kdsxk'  , caption: 'Inventory Attribute',         nav: 'module/inventorysettings/attribute', nodetype: 'Module', description: '' },
                        InventoryCondition:        { id: 'JL0j4lk1KfBY' , caption: 'Inventory Condition',         nav: 'module/inventorysettings/inventorycondition', nodetype: 'Module', description: '' },
                        InventoryRank:             { id: '3YXhU6x3GseH' , caption: 'Inventory Rank',              nav: 'module/inventorysettings/inventoryrank', description: '' },
                        InventoryType:             { id: 'aFLFxVNukHJt' , caption: 'Inventory Type',              nav: 'module/inventorysettings/inventorytype', nodetype: 'Module', description: '' },
                        RentalCategory:            { id: 'whxFImy6IZG2p', caption: 'Rental Category',             nav: 'module/inventorysettings/rentalcategory', nodetype: 'Module', description: '' },
                        Unit:                      { id: 'K87j9eupQwohK', caption: 'Unit of Measure',             nav: 'module/inventorysettings/unit', nodetype: 'Module', description: '' }
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
                        OrderType:      { id: 'yFStSrvTlwWY', caption: 'Order Type',        nav: 'settings/ordersettings/ordertype', nodetype: 'Module', description: '' }
                    }
                },
                POSettings: {
                    caption: 'PO',
                    id: 'Settings.POSettings',
                    nodetype: 'Category',
                    children: {
                        POClassification: { id: 'skhmIJOt0Fi0', caption: 'PO Classification', nav: 'settings/posettings/poclassification', nodetype: 'Module', description: '' },
                        POType: { id: 'Gyx3ZcMtuH1fi', caption: 'PO Type', nav: 'settings/posettings/potype', nodetype: 'Module', description: '' }
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
                ShipViaSettings: {
                    caption: 'Ship Via',
                    id: 'Settings.ShipViaSettings',
                    nodetype: 'Category',
                    children: {
                        ShipVia: { id: 'D1wheIde10lAO', caption: 'Ship Via', nav: 'settings/shipviasettings/shipvia', nodetype: 'Module', description: '' }
                    }
                },
                UserSettings: {
                    caption: 'User',
                    id: 'Settings.UserSettings',
                    nodetype: 'Category',
                    children: {
                        UserStatus: { id: 'YjSbfCF9CEvjz', caption: 'User Status',  nav: 'settings/usersettings/userstatus', nodetype: 'Module', description: '' }
                    }
                },
                VendorSettings: {
                    caption: 'Vendor',
                    id: 'Settings.VendorSettings',
                    nodetype: 'Category',
                    children: {
                        OrganizationType:       { id: 'ENv2O3MbwKrI',   caption: 'Organization Type',           nav: 'settings/vendorsettings/organizationtype', nodetype: 'Module', description: '' },
                        VendorClass:            { id: 'EH6T4hlMVhYxq',  caption: 'Vendor Class',                nav: 'settings/vendorsettings/class', nodetype: 'Module', description: '' }
                    }
                },
                WarehouseSettings: {
                    id: 'WarehouseSettings',
                    caption: 'Warehouse Settings',
                    nodetype: 'Category',
                    children: {
                        Warehouse: { id: 'ICJcR2gOu04OB', caption: 'Warehouse', nav: 'module/warehouse' }
                    }
                }
            }
        },
        Utilities: {
            id: 'Utilities',
            caption: 'Utilities',
            nodetype: 'Category',
            children: {
                Dashboard: { id: 'DF8111F5-F022-40B4-BAE6-23B2C6CF3705', caption: 'Dashboard', nav: 'module/dashboard' },
                DashboardSettings: { id: 'lXpomto7a29v', caption: 'Dashboard Settings', nav: 'module/dashboardsettings' },
                QuikActivityCalendar: { id: 'yhYOLhLE92IT', caption: 'QuikActivity Calendar', nav: 'module/quikactivitycalendar' }
            }
        }
    },
    Grids: {
        OrderItemGrid: {
            menuItems: {
                SummaryView: { id: '{87C47D00-E950-4724-8A8B-4528D0B41124}' },
                //ManualSorting: { id: '' },
                CopyTemplate: { id: '{87B6695A-2597-448A-99A0-970996470369}' },
                Search: { id: '{DD765DEF-DF9A-489C-A61D-9A2409B50CFA}' },
                //CopyLineItems: { id: '' },
                LockUnlockSelected: { id: '{6AEC9EA9-E2A0-4B80-A9BF-3784D82EB64C}' },
                BoldUnBoldSelected: { id: '{7C2C3F95-D939-4861-B75E-023EE00B6B7A}' },
                SubWorksheet: { id: '{D31432DB-1343-4542-85B0-40EB6CAF5DE7}' },
                //AddLossAndDamageItems: { id: '{FFF72FDF-85A4-4EB6-8FB4-3E4CE5857CF5}' },
                //RetireLossAndDamageItems: { id: '{29DECF73-E409-4B51-81B2-B9196B7EDE18}' },
                RefreshAvailability: { id: '{BBC9E755-54D3-474A-ACBE-E99D4A8C568D}' }
            }
        },
        QuoteItemGrid: {
            menuItems: {
                SummaryView: { id: '{224054C8-1D06-4DC5-BBED-D6A1112CCB3C}' },
                Search: { id: '{A87867E1-6756-49C4-AE71-C31648A5F029}' },
                BoldUnBoldSelected: { id: '{373CEE50-A632-463D-9F6F-0387557509CB}' },
                RefreshAvailability: { id: '{74E160D8-54C7-4293-9944-CAB226AD4565}' }
            }
        },
        SearchPreviewGrid: {
            menuItems: {
                RefreshAvailability: { id: '{C4168B67-EDD2-4749-9841-95A113251248}' }
            }
        }
    }
};
