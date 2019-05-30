const Constants = {
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
            Control: { id: '044829ED-579F-4AAD-B464-B4823FDB5A35', caption: 'Control', nav: 'module/control' },
            CustomField: { id: '99D56DA6-5779-44A5-8BA6-E033F343C6D0', caption: 'Custom Fields', nav: 'module/customfield' },
            CustomForm: { id: '2F07BFC4-A120-4C97-9D96-F16906CD1B88', caption: 'Custom Forms', nav: 'module/customform' },
            DuplicateRule: { id: '8A1EA4A2-6019-4B9B-8324-6143BD7916A1', caption: 'Duplicate Rules', nav: 'module/duplicaterule' },
            EmailHistory: { id: '34092164-500A-46BB-8F09-86BBE0FEA082', caption: 'Email History', nav: 'module/emailhistory' },
            Group: { id: '849D2706-72EC-48C0-B41C-0890297BF24B', caption: 'Group', nav: 'module/group' },
            Hotfix: { id: 'B7336B5E-4BA4-4A99-97D6-60385045238B', caption: 'Hotfix', nav: 'module/hotfix' },
            Reports: { id: 'CEAFF154-60DF-4491-96D4-4D4685665E60', caption: 'Reports', nav: 'module/reports' },
            Settings: { id: 'AD8656B4-F161-4568-9AFF-64C81A3680E6', caption: 'Settings', nav: 'module/settings' },
            User: { id: 'CE9E187C-288F-44AB-A54A-27A8CFF6FF53', caption: 'User', nav: 'module/user' }
        },
        Home: {
            Asset: { id: 'E1366299-0008-429C-93CC-B8ED8969B180', caption: 'Asset', nav: 'module/asset' },
            AssignBarCodes: { id: '81B0D93C-9765-4340-8B40-63040E0343B8', caption: 'Assign Bar Codes', nav: 'module/assignbarcodes' },
            CheckIn: { id: '3D1EB9C4-95E2-440C-A3EF-10927C4BDC65', caption: 'Check-In', nav: 'module/checkin' },
            Contact: { id: '9DC167B7-3313-4783-8A97-03C55B6AD5F2', caption: 'Contact', nav: 'module/contact' },
            Contract: { id: 'F6D42CC1-FAC6-49A9-9BF2-F370FE408F7B', caption: 'Contract', nav: 'module/contract', 
                form: {
                    menuItems: { 
                        PrintOrder: { id: '{8E3727B5-8E75-4648-AE52-FB3BD7729F02}' } 
                    } 
                }
            },
            CreatePickList: { id: '1407A536-B5C9-4363-8B54-A56DB8CE902D', caption: 'Create Pick List', nav: 'module/createpicklist' },
            Customer: { id: '8237418B-923D-4044-951F-98938C1EC3DE', caption: 'Customer', nav: 'module/customer' },
            Deal: { id: '393DE600-2911-4753-85FD-ABBC4F0B1407', caption: 'Job', nav: 'module/deal' },
            Exchange: { id: 'F9012ABC-B97E-433B-A604-F1DADFD6D7B7', caption: 'Exchange', nav: 'module/exchange' },
            ExpendableItem: { id: '4115FFCE-69BB-4D2F-BCDC-3924AE045AA8', caption: 'Expendable Item', nav: 'module/expendableitem' },
            Order: { id: '68B3710E-FE07-4461-9EFD-04E0DBDAF5EA', caption: 'Order', nav: 'module/order',
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
            OrderStatus: { id: '7BB8BB8C-8041-41F6-A2FA-E9FA107FF5ED', caption: 'Order Status', nav: 'module/orderstatus' },
            PickList: { id: '744B371E-5478-42F9-9852-E143A1EC5DDA', caption: 'Pick List', nav: 'module/picklist',
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
            PurchaseOrder: { id: 'DA900327-CEAC-4CB0-9911-CAA2C67059C2', caption: 'Purchase Order', nav: 'module/purchaseorder',
                form: {
                    menuItems: {
                        Search: { id: '{932A7318-3B62-4AA5-A0C5-904BB4BB9F17}' },
                        ReceiveFromVendor: { id: '{B719A865-4B0A-4C60-A1D9-3614EC6D0515}' },
                        ReturnToVendor: { id: '{6D3E64B9-6B08-47B0-8F51-CFAE12651630}' },
                        AssignBarCodes: { id: '{D7722622-BBAE-4FE7-9DAD-1E2A4419CC3D}' }
                    }
                }    
            },
            Quote: { id: '9213AF53-6829-4276-9DF9-9DAA704C2CCF', caption: 'Request', nav: 'module/quote',
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
                        //NewVersion: { id: '{18ECF5BB-18E0-45F5-AB9A-98A377E38D1F}' },
                        CancelUncancel: { id: '{8F3E3263-B5AE-4CB4-8CBB-E9F680AA8C11}' }
                    }
                }    
            },
            ReceiveFromVendor: { id: 'EC4052D5-664E-4C34-8802-78E086920628', caption: 'Receive From Vendor', nav: 'module/receivefromvendor' },
            InventoryItem: { id: '803A2616-4DB6-4BAC-8845-ECAD34C369A8', caption: 'Inventory Item', nav: 'module/inventoryitem' },
            Repair: { id: 'D567EC42-E74C-47AB-9CA8-764DC0F02D3B', caption: 'Repair Order', nav: 'module/repair',
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
            },
            ReturnToVendor: { id: '79EAD1AF-3206-42F2-A62B-DA1C44092A7F', caption: 'Return To Vendor', nav: 'module/returntovendor' },
            StagingCheckout: { id: 'AD92E203-C893-4EB9-8CA7-F240DA855827', caption: 'Staging / Check-Out', nav: 'module/checkout' },
            SubWorksheet: { id: '2227B6C3-587D-48B1-98B6-B9125E0E4D9D', caption: 'Sub Worksheet', nav: '' },
            SuspendedSession: { id: '', caption: 'Suspended Session', nav: 'module/suspendedsession' },
            Vendor: { id: '92E6B1BE-C9E1-46BD-91A0-DF257A5F909A', caption: 'Vendor', nav: 'module/vendor' }
        },
        Settings: {
            Warehouse: { id: '8DD21206-86D4-4C69-9094-A8CF0A5C93FF', caption: 'Warehouse', nav: 'module/warehouse' }
        },
        Utilities: {
            Dashboard: { id: 'E01F0032-CFAA-4556-9F24-E4C28C5B50A1', caption: 'Dashboard', nav: 'module/dashboard' },
            DashboardSettings: { id: 'AD262A8E-A487-4786-895D-6E3DA1DB13BD', caption: 'Dashboard Settings', nav: 'module/dashboardsettings' },
            QuikActivityCalendar: { id: 'FB114A8F-1675-4C7C-BC9C-A4C005A405D7', caption: 'QuikActivity Calendar', nav: 'module/quikactivitycalendar' }
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
