const Constants = {
    isTrakitWorks: false,
    isRentalWorks: true,
    appId: '0A5F2584-D239-480F-8312-7C2B552A30BA',
    appCaption: 'RentalWorks',
    //appTitle: 'Rental<span class="rwpurple">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span>',
    appTitle: 'Rental<span class="rwpurple">Works</span>',
    MainMenu: {
        Reports: { id: '7FEC9D55-336E-44FE-AE01-96BF7B74074C' },
        Settings: { id: '730C9659-B33B-493E-8280-76A060A07DCE' }
    },
    Modules: {
        Administrator: {
            Alert: { id: '6E5F47FB-1F18-443E-B464-9D2351857361', caption: 'Alert', nav: 'module/alert' },
            CustomField: { id: 'C98C4CB4-2036-4D70-BC29-8F5A2874B178', caption: 'Custom Field', nav: 'module/customfield' },
            CustomForm: { id: 'CB2EF8FF-2E8D-4AD0-B880-07037B839C5E', caption: 'Custom Form', nav: 'module/customform' },
            CustomReportLayout: { id: 'B89CDAF3-53B2-4FE8-97C6-39DC98E98DBA', caption: 'Custom Report Layout', nav: 'module/customreportlayout' },
            DuplicateRule: { id: '2E0EA479-AC02-43B1-87FA-CCE2ABA6E934', caption: 'Duplicate Rule', nav: 'module/duplicaterule' },
            EmailHistory: { id: '3F44AC27-CE34-46BA-B4FB-A8AEBB214167', caption: 'Email History', nav: 'module/emailhistory' },
            Group: { id: '9BE101B6-B406-4253-B2C6-D0571C7E5916', caption: 'Group', nav: 'module/group' },
            Hotfix: { id: '9D29A5D9-744F-40CE-AE3B-09219611A680', caption: 'Hotfix', nav: 'module/hotfix' },

            Reports: { id: '3C5C7603-9E7B-47AB-A722-B29CA09B3B8C', caption: 'Reports', nav: 'module/reports' },
            Settings: { id: '57150967-486A-42DE-978D-A2B0F843341A', caption: 'Settings', nav: 'module/settings' },
            User: { id: '79E93B21-8638-483C-B377-3F4D561F1243', caption: 'User', nav: 'module/user' }
        },
        Home: {
            Asset: { id: '1C45299E-F8DB-4AE4-966F-BE142295E3D6', caption: 'Asset', nav: 'module/item' },
            AssignBarCodes: { id: '4B9C17DE-7FC0-4C33-B953-26FC90F32EA0', caption: 'Assign Bar Codes', nav: 'module/assignbarcodes' },
            AvailabilityConflicts: { id: 'DF2859D1-3834-42DA-A367-85B168850ED9', caption: 'Availability Conflicts', nav: 'module/availabilityconflicts' },
            Billing: { id: '34E0472E-9057-4C66-8CC2-1938B3222569', caption: 'Billing', nav: 'module/billing' },
            BillingWorksheet: {
                id: 'BF8E2838-A31D-46B2-ABE1-5B09FC3E2A9E', caption: 'Billing Worksheet', nav: 'module/billingworksheet',
                browse: {
                    menuItems: {
                        Approve: { id: '20CA8800-41C5-4387-8EF0-558330B96AAA' },
                        Unapprove: { id: 'DE1069AB-2A4D-4556-AD00-9D89FAA22B54' }
                    }
                },
                form: {
                    menuItems: {
                        Approve: { id: '16932E29-821B-45B1-A7B7-82D203258E70' },
                        Unapprove: { id: '09B18F85-BD05-462F-994D-DF7989D37E44' },
                    }
                }
            },
            BillingMessage: { id: 'B232DF4D-462A-4810-952D-73F8DE66800C', caption: 'Billing Message', nav: 'module/billingmessage' },
            CheckIn: {
                id: '77317E53-25A2-4C12-8DAD-7541F9A09436', caption: 'Check-In', nav: 'module/checkin',
                form: {
                    menuItems: {
                        Cancel: { id: '{52BEF7F5-C9F7-44DE-AD84-8E5AC68A9D7B}' }
                    }
                }
            },
            CompleteQc: { id: '3F20813A-CC21-49D8-A5F8-9930B7F05404', caption: 'Complete QC', nav: 'module/completeqc' },
            Contact: { id: '3F803517-618A-41C0-9F0B-2C96B8BDAFC4', caption: 'Contact', nav: 'module/contact' },
            Container: { id: '28A49328-FFBD-42D5-A492-EDF540DF7011', caption: 'Container', nav: 'module/container' },
            ContainerStatus: { id: '0CD07ACF-D9A4-42A3-A288-162398683F8A', caption: 'Container Status', nav: 'module/containerstatus' },
            Contract: {
                id: '6BBB8A0A-53FA-4E1D-89B3-8B184B233DEA', caption: 'Contract', nav: 'module/contract',
                form: {
                    menuItems: {
                        PrintOrder: { id: '{8C34754E-B27F-4FE1-93F3-8D6D84339322}' },
                        VoidContract: { id: '{426E75B4-D91E-416F-AEB2-F6B4F8BB5936}' }
                    }
                }
            },
            CountQuantityInventory: { id: '0A02B28D-C025-4579-993B-860832F8837F', caption: 'Count Quantity Inventory', nav: 'module/physicalinventoryquantityinventory' },
            CreatePickList: { id: '1407A536-B5C9-4363-8B54-A56DB8CE902D', caption: 'Create Pick List', nav: 'module/contract' },
            Customer: { id: '214C6242-AA91-4498-A4CC-E0F3DCCCE71E', caption: 'Customer', nav: 'module/customer' },
            CustomerCredit: { id: 'CCFCD376-FC2B-49F4-BAE0-3FB1F0258F66', caption: 'Customer Credit', nav: 'module/customercredit' },
            Deal: { id: 'C67AD425-5273-4F80-A452-146B2008B41C', caption: 'Deal', nav: 'module/deal' },
            DealCredit: { id: '3DD1BA32-0213-472E-ADA8-E54D531464CC', caption: 'Deal Credit', nav: 'module/dealcredit' },
            EmptyContainer: { id: '60CAE944-DE89-459E-86AC-2F1B68211E07', caption: 'Empty Container', nav: 'module/emptycontainer' },
            Exchange: {
                id: '2AEDB175-7998-48BC-B2C4-D4794BF65342', caption: 'Exchange', nav: 'module/exchange',
                form: {
                    menuItems: {
                        Cancel: { id: '{2301B78E-7928-4672-8747-29ED57C529FC}' }
                    }
                }
            },
            FillContainer: { id: '0F1050FB-48DF-41D7-A969-37300B81B7B5', caption: 'Fill Container', nav: 'module/fillcontainer' },
            InventoryPurchaseUtility: { id: '5EEED3A9-40FF-4038-B53D-DB6E777FAC7C', caption: 'Inventory Purchase Utility', nav: 'module/inventorypurchaseutility' },
            Invoice: {
                id: '9B79D7D8-08A1-4F6B-AC0A-028DFA9FE10F', caption: 'Invoice', nav: 'module/invoice',
                browse: {
                    menuItems: {
                        Void: { id: '{DACF4B06-DE63-4867-A684-4C77199D6961}' },
                        Approve: { id: '{9D1A3607-EE4A-49E6-8EAE-DB3E0FF06EAE}' },
                        Unapprove: { id: '{F9D43CB6-2666-4AE0-B35C-77735561B9B9}' }
                    }
                },
                form: {
                    menuItems: {
                        Void: { id: '{DF6B0708-EC5A-475F-8EFB-B52E30BACAA3}' },
                        PrintInvoice: { id: '{3A693D4E-3B9B-4749-A9B6-C8302F1EDE6A}' },
                        Approve: { id: '{117CCDFA-FFC3-49CE-B41B-0F6CE9A69518}' },
                        Unapprove: { id: '{F8C5F06C-4B9D-4495-B589-B44B02AE7915}' },
                        CreditInvoice: { id: '{CC80D0FC-2E28-4C3D-83EB-C8B5EE0CB4B5}' }
                    }
                }
            },
            Manifest: {
                id: '1643B4CE-D368-4D64-8C05-6EF7C7D80336', caption: 'Transfer Manifest', nav: 'module/manifest',
                form: {
                    menuItems: {
                        Print: { id: '{8FC8A0F2-C016-476F-971B-64CF2ED95E41}' }
                    }
                }
            },
            Order: {
                id: '64C46F51-5E00-48FA-94B6-FC4EF53FEA20', caption: 'Order', nav: 'module/order',
                browse: {
                    menuItems: {
                        CancelUncancel: { id: '{DAE6DC23-A2CA-4E36-8214-72351C4E1449}' }
                    }
                },
                form: {
                    menuItems: {
                        Search: { id: '{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}' },
                        CopyOrder: { id: '{E25CB084-7E7F-4336-9512-36B7271AC151}' },
                        PrintOrder: { id: '{F2FD2F4C-1AB7-4627-9DD5-1C8DB96C5509}' },
                        CreatePickList: { id: '{91C9FD3E-ADEE-49CE-BB2D-F00101DFD93F}' },
                        CreateSnapshot: { id: '{AB1D12DC-40F6-4DF2-B405-54A0C73149EA}' },
                        CancelSnapshot: { id: '{515A0924-C0B7-4EFA-A9A0-6CFFBF55DAB2}' },
                        ViewSnapshot: { id: '{03000DCC-3D58-48EA-8BDF-A6D6B30668F5}' },
                        CancelUncancel: { id: '{6B644862-9030-4D42-A29B-30C8DAC29D3E}' },
                        OnHold: { id: '{00AB18C2-221A-46F9-86DC-A7145D13A0D8}' },
                        OrderStatus: { id: '{CF245A59-3336-42BC-8CCB-B88807A9D4EA}' },
                        CheckOut: { id: '{771DCE59-EB57-48B2-B189-177B414A4ED3}' },
                        CheckIn: { id: '{380318B6-7E4D-446D-A018-1EB7720F4338}' },
                        ChangeOfficeLocation: { id: '{31C62070-9045-478D-A0DC-D3B62B61E108}' },
                    }
                }
            },
            OrderStatus: { id: 'F6AE5BC1-865D-467B-A201-95C93F8E8D0B', caption: 'Order Status', nav: 'module/orderstatus' },
            PartsInventory: {
                id: '351B8A09-7778-4F06-A6A2-ED0920A5C360', caption: 'Parts Inventory', nav: 'module/partsinventory',
                form: {
                    menuItems: {
                        CreateComplete: { id: '{1881D7B6-E17A-4D20-A2E5-71F383FBD8CB}' }
                    }
                }
            },
            PhysicalInventory: { id: 'BABFE80E-8A52-49D4-81D9-6B6EBB518E89', caption: 'Physical Inventory', nav: 'module/physicalinventory' },
            PickList: {
                id: '7B04E5D4-D079-4F3A-9CB0-844F293569ED', caption: 'Pick List', nav: 'module/picklist',
                browse: {
                    menuItems: {
                        PrintPickList: { id: '{51C78FB1-CD66-431F-A7BA-FFFB3BFDFD6C}' }
                    }
                },
                form: {
                    menuItems: {
                        PrintPickList: { id: '{069BBE73-5B14-4F3E-A594-8699676D9B8E}' },
                        CancelPickList: { id: '{3BF7AEF3-BF52-4B8B-8324-910A92005B2B}' }
                    }
                }
            },
            Project: {
                id: 'C6C8167A-C3B5-4915-8290-4520AF7EDB35', caption: 'Project', nav: 'module/project',
                form: {
                    menuItems: {
                        CreateQuote: { id: '{92B78408-298F-431C-A535-2ADC7C4DD2F7}' }
                    }
                }
            },
            PurchaseOrder: {
                id: '67D8C8BB-CF55-4231-B4A2-BB308ADF18F0', caption: 'Purchase Order', nav: 'module/purchaseorder',
                form: {
                    menuItems: {
                        AssignBarCodes: { id: '{649E744B-0BDD-43ED-BB6E-5945CBB0BFA5}' },
                        Search: { id: '{D512214F-F6BD-4098-8473-0AC7F675893D}' },
                        ReceiveFromVendor: { id: '{4BB0AB54-641E-4638-89B4-0F9BFE88DF82}' },
                        ReturnToVendor: { id: '{B287428E-FF45-469A-8203-3BFF18E90810}' },
                        Void: { id: '{7F102668-CB35-471B-8812-352C13C123AB}' },
                    }
                }
            },
            Quote: {
                id: '4D785844-BE8A-4C00-B1FA-2AA5B05183E5', caption: 'Quote', nav: 'module/quote',
                browse: {
                    menuItems: {
                        CancelUncancel: { id: '{78ACB73C-23DD-46F0-B179-0571BAD3A17D}' }
                    }
                },
                form: {
                    menuItems: {
                        CopyQuote: { id: '{B918C711-32D7-4470-A8E5-B88AB5712863}' },
                        Search: { id: '{BC3B1A5E-7270-4547-8FD1-4D14F505D452}' },
                        PrintQuote: { id: '{B20DDE47-A5D7-49A9-B980-8860CADBF7F6}' },
                        CreateOrder: { id: '{E265DFD0-380F-4E8C-BCFD-FA5DCBA4A654}' },
                        NewVersion: { id: '{F79F8C21-66DF-4458-BBEB-E19B2BFCAEAA}' },
                        MakeQuoteActive: { id: '{32BF5F46-987A-4D61-9E85-6A7954897077}' },
                        Reserve: { id: '{C122C2C5-9D68-4CDF-86C9-E37CB70C57A0}' },
                        CancelUncancel: { id: '{BF633873-8A40-4BD6-8ED8-3EAC27059C84}' },
                        ChangeOfficeLocation: { id: '{FDFED8DE-151C-4C03-B509-5AE4B19A2588}' },
                    }
                }
            },
            Receipt: { id: '57E34535-1B9F-4223-AD82-981CA34A6DEC', caption: 'Receipts', nav: 'module/receipt' },
            ReceiveFromVendor: {
                id: '00539824-6489-4377-A291-EBFE26325FAD', caption: 'Receive From Vendor', nav: 'module/receivefromvendor',
                form: {
                    menuItems: {
                        Cancel: { id: '{A3BA715F-9249-4504-B076-1E9195F35372}' }
                    }
                }
            },
            RemoveFromContainer: { id: 'FB9876B5-165E-486C-9E06-DFB3ACB3CBF0', caption: 'Remove From Container', nav: 'module/removefromcontainer' },
            RentalInventory: {
                id: 'FCDB4C86-20E7-489B-A8B7-D22EE6F85C06', caption: 'Rental Inventory', nav: 'module/rentalinventory',
                form: {
                    menuItems: {
                        CreateComplete: { id: '{B3371C86-740C-44C4-A8FA-E8DE750800F3}' }
                    }
                }
            },
            Repair: {
                id: '2BD0DC82-270E-4B86-A9AA-DD0461A0186A', caption: 'Repair Order', nav: 'module/repair',
                browse: {
                    menuItems: {
                        Void: { id: '{AFA36551-F49E-4FB9-84DD-A54A423CCFF3}' }
                    }
                },
                form: {
                    menuItems: {
                        Estimate: { id: '{AEDCEB81-2A5A-4779-8A88-25FD48E88E6A}' },
                        Complete: { id: '{6EE5D9E2-8075-43A6-8E81-E2BCA99B4308}' },
                        Void: { id: '{9F58C03B-89CD-484A-8332-CDBF9961A258}' },
                        ReleaseItems: { id: '{EE709549-C91C-473E-96CC-2DB121082FB5}' }
                    }
                }
            },
            ReturnToVendor: {
                id: 'D54EAA01-A710-4F78-A1EE-5FC9EE9150D8', caption: 'Return To Vendor', nav: 'module/returntovendor',
                form: {
                    menuItems: {
                        Cancel: { id: '{C072441D-1FE3-4D2E-A015-DBE871CEC0FD}' }
                    }
                }
            },
            SalesInventory: {
                id: 'B0CF2E66-CDF8-4E58-8006-49CA68AE38C2', caption: 'Sales Inventory', nav: 'module/salesinventory',
                form: {
                    menuItems: {
                        CreateComplete: { id: '{B13C0180-D25C-4AFC-9B2C-556C7B0FA53F}' }
                    }
                }
            },
            StagingCheckout: {
                id: 'C3B5EEC9-3654-4660-AD28-20DE8FF9044D', caption: 'Staging / Check-Out', nav: 'module/checkout',
                form: {
                    menuItems: {
                        Cancel: { id: '{6E95996C-E104-4BBA-BE13-5FD73E4AAD04}' }
                    }
                }
            },
            SubWorksheet: { id: '2227B6C3-587D-48B1-98B6-B9125E0E4D9D', caption: 'Sub Worksheet', nav: 'module/subworksheet' },
            SuspendedSession: { id: '5FBE7FF8-3770-48C5-855C-4320C961D95A', caption: 'Suspended Session', nav: 'module/suspendedsession' },
            TransferIn: { id: 'D9F487C2-5DC1-45DF-88A2-42A05679376C', caption: 'Transfer In', nav: 'module/transferin' },
            TransferOrder: {
                id: 'F089C9A9-554D-40BF-B1FA-015FEDE43591', caption: 'Transfer Order', nav: 'module/transferorder',
                form: {
                    menuItems: {
                        Search: { id: '{EE207266-01FC-4D0E-8469-48F5B099ED71}' },
                        Confirm: { id: '{A35F0AAD-81B5-4A0C-8970-D448A67D5A82}' },
                        CreatePickList: { id: '{5CA07E25-A93E-4FA0-9206-B3F556684B0C}' },
                        TransferStatus: { id: '{A256288F-238F-4594-8A6A-3B70613925DA}' },
                        TransferOut: { id: '{D0AB3734-7F96-46A6-8297-331110A4854F}' },
                        TransferIn: { id: '{E362D71D-7597-4752-8BDD-72EE0CB7B2C4}' }
                    }
                }
            },
            TransferOut: { id: '91E79272-C1CF-4678-A28F-B716907D060C', caption: 'Transfer Out', nav: 'module/transferout' },
            TransferReceipt: {
                id: '2B60012B-ED6A-430B-B2CB-C1287FD4CE8B', caption: 'Transfer Receipt', nav: 'module/transferreceipt',
                form: {
                    menuItems: {
                        Print: { id: '{5C35E285-F8DA-4D27-AA64-379156213B7F}' }
                    }
                }
            },
            TransferStatus: { id: '58D5D354-136E-40D5-9675-B74FD7807D6F', caption: 'Transfer Status', nav: 'module/transferstatus' },
            Vendor: { id: 'AE4884F4-CB21-4D10-A0B5-306BD0883F19', caption: 'Vendor', nav: 'module/vendor' },
            VendorInvoice: {
                id: '854B3C59-7040-47C4-A8A3-8A336FC970FE', caption: 'Vendor Invoice', nav: 'module/vendorinvoice',
                browse: {
                    menuItems: {
                        Approve: { id: '{4A8CEF38-F59F-4306-8A9B-9B43FF6D127D}' },
                        Unapprove: { id: '{9378DA62-F7FD-4FD2-8310-3277BBD155BF}' }
                    }
                },
                form: {
                    menuItems: {
                        Approve: { id: '{79ABAD41-19F1-42C1-A88B-41479DE13B3B}' },
                        Unapprove: { id: '{FB248072-C14C-4EEC-8B99-5ED8E950CE8A}' }
                    }
                }
            }
        },
        Settings: {
            DefaultSettings: { id: '3F551271-C157-44F6-B06A-8F835F7A2084', caption: 'Default Settings', nav: 'module/defaultsettings' },
            InventorySettings: { id: '5C7D5BFA-3EA3-42C5-B90A-27A9EA5EA9FC', caption: 'Inventory Settings', nav: 'module/inventorysettings' },
            LogoSettings: { id: 'B3ADDF49-64EB-4740-AB41-4327E6E56242', caption: 'Logo Settings', nav: 'module/logosettings' },
            SystemSettings: { id: '6EFC3A8C-E83E-4FE3-BAC8-8E04EBD7F204', caption: 'System Settings', nav: 'module/systemsettings' },
            TaxOption: {
                form: {
                    menuItems: {
                        ForceTaxRates: { id: '{CE1AEA95-F022-4CF5-A4FA-81CE32523344}' }
                    }
                }
            },
            Template: {
                form: {
                    menuItems: {
                        Search: { id: '{6386E100-98B2-42F3-BF71-5BB432070D10}' }
                    }
                }
            },
            Warehouse: { id: '931D3E75-68CB-4280-B12F-9A955444AA0C', caption: 'Warehouse', nav: 'module/warehouse' }
        },
        Utilities: {
            Dashboard: { id: 'DF8111F5-F022-40B4-BAE6-23B2C6CF3705', caption: 'Dashboard', nav: 'module/dashboard' },
            DashboardSettings: { id: '1B40C62A-1FA0-402E-BE52-9CBFDB30AD3F', caption: 'Dashboard Settings', nav: 'module/dashboardsettings' },
            InvoiceProcessBatch: {
                id: '5DB3FB9C-6F86-4696-867A-9B99AB0D6647', caption: 'Process Invoices', nav: 'module/invoiceprocessbatch',
                form: {
                    menuItems: {
                        ExportSettings: { id: '{28D5F4EF-9A60-4D7F-B294-4B302B88413F}' }
                    }
                }
            },
            MigrateOrders: { id: '6FAA0140-ACA2-40CA-9FDD-507EAC437F2A', caption: 'Migrate Orders', nav: 'module/migrateorders' },
            QuikActivityCalendar: { id: '897BCF55-6CE7-412C-82CB-557B045F8C0A', caption: 'QuikActivity Calendar', nav: 'module/quikactivitycalendar' },
            QuikSearch: { id: '07587E25-9802-4379-8630-96DBA3136595', caption: 'QuikSearch', nav: 'module/quiksearch' },
            ReceiptProcessBatch: {
                id: '0BB9B45C-57FA-47E1-BC02-39CEE720792C', caption: 'Process Receipts', nav: 'module/receiptprocessbatch',
                form: {
                    menuItems: {
                        ExportSettings: { id: '{0D951DA8-1843-4080-AD73-B0DF7F27189B}' }
                    }
                }
            },
            RefreshGLHistory: { id: '8F036E39-78D3-4FB9-A98E-BD33A5DB7FDA', caption: 'Refresh G/L History', nav: 'module/refreshglhistory' },
            VendorInvoiceProcessBatch: { id: '4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240', caption: 'Process Vendor Invoices', nav: 'module/vendorinvoiceprocessbatch' }

        }
    },
    Grids: {
        ContractDetailGrid: {
            menuItems: {
                VoidItems: { id: '{DD6F2FD1-B70F-4525-BCAA-322EF3DBC9C1}' }
            }
        },
        InventoryCompleteGrid: {
            menuItems: {
                QuikSearch: { id: '{A3EEC381-6D45-485D-8E12-5DA6B38BB71A}' }
            }
        },
        InventoryContainerItemGrid: {
            menuItems: {
                QuikSearch: { id: '{BA9FD9DD-2E96-4A4D-80B9-6010BEE66D6F}' }
            }
        },
        InventoryKitGrid: {
            menuItems: {
                QuikSearch: { id: '{B599B514-30BD-49B3-A08A-7863693D979C}' }
            }
        },
        InvoiceItemGrid: {
            menuItems: {
                ToggleOrderItemView: { id: '{46D27E42-9C66-42F5-922C-CAE617856F63}' }
            }
        },
        OrderItemGrid: {
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
        OrderSnapshotGrid: {
            menuItems: {
                ViewSnapshot: { id: '{C6633D9A-3800-41F2-8747-BC780663E22F}' }
            }
        },
        SearchPreviewGrid: {
            menuItems: {
                //RefreshAvailability: { id: '{3756AF3A-1611-4BCD-BBD9-E3233F5A772E}' }
            }
        },
        StagedItemGrid: {
            menuItems: {
                UnstageItems: { id: '{43010EEE-85B8-4444-9FA7-A0A4A0ABC8CF}' }
            }
        },
        StagedQuantityItemGrid: {
            menuItems: {
                UnstageItems: { id: '{FECB5FC0-4E01-4F99-8D29-2F9CE446846B}' }
            }
        },
        TransferOrderItemGrid: {
            menuItems: {
                QuikSearch: { id: '{16CD0101-28D7-49E2-A3ED-43C03152FEE6}' },
                CopyTemplate: { id: '{5E73772F-F5E2-4382-9F50-3272F4E79A25}' },
                //RefreshAvailability: { id: '{1065995B-3EF3-4B50-B513-F966F88570F1}' }
            }
        }
    }
};

