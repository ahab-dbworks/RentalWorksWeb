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
                Alert: { id: 'gFfpaR5mDAzX', caption: 'Alert', nav: 'module/alert', nodetype: 'Module' },
                CustomField: { id: 'cZHPJQyBxolS', caption: 'Custom Fields', nav: 'module/customfield', nodetype: 'Module' },
                CustomForm: { id: '11txpzVKVGi2', caption: 'Custom Forms', nav: 'module/customform', nodetype: 'Module' },
                CustomReportLayout: { id: 'EtrF5NHJ7dRg6', caption: 'Custom Report Layouts', nav: 'module/customreportlayout' },
                DuplicateRule: { id: 'v7oBspDLjli8', caption: 'Duplicate Rules', nav: 'module/duplicaterule', nodetype: 'Module' },
                EmailHistory: { id: '3XHEm3Q8WSD8z', caption: 'Email History', nav: 'module/emailhistory', nodetype: 'Module' },
                Group: { id: '0vP4rXxgGL1M', caption: 'Group', nav: 'module/group', nodetype: 'Module' },
                Hotfix: { id: 'yeqaGIUYfYNX', caption: 'Hotfix', nav: 'module/hotfix', nodetype: 'Module' },
                Reports: { id: '', caption: 'Reports', nav: 'module/reports', nodetype: 'Module' },
                Settings: { id: '', caption: 'Settings', nav: 'module/settings', nodetype: 'Module' },
                User: { id: 'r1fKvn1KaFd0u', caption: 'User', nav: 'module/user', nodetype: 'Module' }
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
            children: {}
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
                InventorySettings: {
                    caption: 'Inventory',
                    id: 'Settings.InventorySettings',
                    nodetype: 'Category',
                    children: {
                        Attribute: { id: 'Ok4Yh4kdsxk', caption: 'Inventory Attribute', nav: 'module/inventorysettings/attribute', nodetype: 'Module', description: '' },
                        InventoryCondition: { id: 'JL0j4lk1KfBY', caption: 'Inventory Condition', nav: 'module/inventorysettings/inventorycondition', nodetype: 'Module', description: '' },
                        InventoryRank: { id: '3YXhU6x3GseH', caption: 'Inventory Rank', nav: 'module/inventorysettings/inventoryrank', description: '' },
                        InventoryType: { id: 'aFLFxVNukHJt', caption: 'Inventory Type', nav: 'module/inventorysettings/inventorytype', nodetype: 'Module', description: '' },
                        RentalCategory: { id: 'whxFImy6IZG2p', caption: 'Rental Category', nav: 'module/inventorysettings/rentalcategory', nodetype: 'Module', description: '' },
                        Unit: { id: 'K87j9eupQwohK', caption: 'Unit of Measure', nav: 'module/inventorysettings/unit', nodetype: 'Module', description: '' }
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
                        OrderType: { id: 'yFStSrvTlwWY', caption: 'Order Type', nav: 'settings/ordersettings/ordertype', nodetype: 'Module', description: '' }
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
                    id: 'Settings.RepairSettings',
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
                        UserStatus: { id: 'YjSbfCF9CEvjz', caption: 'User Status', nav: 'settings/usersettings/userstatus', nodetype: 'Module', description: '' }
                    }
                },
                VendorSettings: {
                    caption: 'Vendor',
                    id: 'Settings.VendorSettings',
                    nodetype: 'Category',
                    children: {
                        OrganizationType: { id: 'ENv2O3MbwKrI', caption: 'Organization Type', nav: 'settings/vendorsettings/organizationtype', nodetype: 'Module', description: '' },
                        VendorClass: { id: 'EH6T4hlMVhYxq', caption: 'Vendor Class', nav: 'settings/vendorsettings/class', nodetype: 'Module', description: '' }
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
                CopyTemplate: { id: '{87B6695A-2597-448A-99A0-970996470369}' },
                Search: { id: '{DD765DEF-DF9A-489C-A61D-9A2409B50CFA}' },
                LockUnlockSelected: { id: '{6AEC9EA9-E2A0-4B80-A9BF-3784D82EB64C}' },
                BoldUnBoldSelected: { id: '{7C2C3F95-D939-4861-B75E-023EE00B6B7A}' },
                SubWorksheet: { id: '{D31432DB-1343-4542-85B0-40EB6CAF5DE7}' },
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
//# sourceMappingURL=Constants.js.map