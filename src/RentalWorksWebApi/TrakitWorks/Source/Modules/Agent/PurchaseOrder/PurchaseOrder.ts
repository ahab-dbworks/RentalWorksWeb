routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

class PurchaseOrder {
    Module:                  string = 'PurchaseOrder';
    apiurl:                  string = 'api/v1/purchaseorder';
    caption: string = Constants.Modules.Agent.children.PurchaseOrder.caption;
	nav: string = Constants.Modules.Agent.children.PurchaseOrder.nav;
	id: string = Constants.Modules.Agent.children.PurchaseOrder.id;
    DefaultPurchasePoType:   string;
    DefaultPurchasePoTypeId: string;
    ActiveViewFields:        any    = {};
    ActiveViewFieldsId:      string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwAppData.apiMethod(true, 'GET', `api/v1/officelocation/${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultPurchasePoType = response.DefaultPurchasePoType;
            this.DefaultPurchasePoTypeId = response.DefaultPurchasePoTypeId;
        }, null, null);

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        //FwBrowse.search($browse);
        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    //addBrowseMenuItems($menuObject: any) {
    //    const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
    //    const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
    //    const $open = FwMenu.generateDropDownViewBtn('Open', false, "OPEN");
    //    const $received = FwMenu.generateDropDownViewBtn('Received', false, "RECEIVED");
    //    const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
    //    const $void = FwMenu.generateDropDownViewBtn('Void', false, "VOID");
    //    const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
        
    //    const viewSubitems: Array<JQuery> = [];
    //    viewSubitems.push($all, $new, $open, $received, $complete, $void, $closed);
    //    FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

    //    //Location Filter
    //    const location = JSON.parse(sessionStorage.getItem('location'));
    //    const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
    //    const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

    //    if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
    //        this.ActiveViewFields.LocationId = [location.locationid];
    //    }
        
    //    const viewLocation: Array<JQuery> = [];
    //    viewLocation.push($userLocation, $allLocations);
    //    FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
    //    return $menuObject;
    //};
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $open = FwMenu.generateDropDownViewBtn('Open', false, "OPEN");
        const $received = FwMenu.generateDropDownViewBtn('Received', false, "RECEIVED");
        const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $void = FwMenu.generateDropDownViewBtn('Void', false, "VOID");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $open, $received, $complete, $void, $closed);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Assign Barcodes', 'cCF4flMdl36v', (e: JQuery.ClickEvent) => {
            try {
                this.assignBarCodes(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Receive From Vendor', 'Ib1R4j9P0d9E', (e: JQuery.ClickEvent) => {
            try {
                this.receiveFromVendor(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Return To Vendor', 'R645XpKYOSX1', (e: JQuery.ClickEvent) => {
            try {
                this.returnToVendor(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //no-security
        FwMenu.addSubMenuItem(options.$groupOptions, 'Search', '', (e: JQuery.ClickEvent) => {
            try {
                this.search(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.disable($form.find('[data-datafield="SubRent"]'));
        FwFormField.disable($form.find('[data-datafield="SubSale"]'));
        FwFormField.disable($form.find('[data-datafield="SubLabor"]'));
        FwFormField.disable($form.find('[data-datafield="SubMiscellaneous"]'));
        FwFormField.disable($form.find('[data-datafield="SubVehicle"]'));

        //Toggle Buttons on Deliver/Ship tab
        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Vendor Pick Up' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="InDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Vendor Deliver' },
            { value: 'SHIP', text: 'Vendor Ship' },
            { value: 'PICK UP', text: 'Pick Up' }
        ], true);

        var deliverAddressTypes = [{ value: 'DEAL', caption: 'Job' },
        { value: 'VENUE', caption: 'Venue' },
        { value: 'WAREHOUSE', caption: 'Warehouse' },
        { value: 'OTHER', caption: 'Other' }];
        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryAddressType"]'), deliverAddressTypes);
        FwFormField.loadItems($form.find('div[data-datafield="InDeliveryAddressType"]'), deliverAddressTypes);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');

            // Activity checkbox and tab behavior
            $form.find('div[data-datafield="Rental"] input').prop('checked', true);
            $form.find('[data-type="tab"][data-caption="Sub-Rental"]').hide();

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            //$form.find('div[data-datafield="Labor"] input').prop('checked', true);
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PurchaseOrderDate', today);

            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const office = JSON.parse(sessionStorage.getItem('location'));
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="PoTypeId"]', this.DefaultPurchasePoTypeId, this.DefaultPurchasePoType);
        };

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));

        this.events($form);
        this.activityCheckboxEvents($form, mode);

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            if ($form.data('mode') !== 'NEW') {
                const $tab = jQuery(e.currentTarget);
                const tabpageid = jQuery(e.currentTarget).data('tabpageid');

                if ($tab.hasClass('audittab') == false) {
                    const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
                    if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                        for (let i = 0; i < $gridControls.length; i++) {
                            try {
                                const $gridcontrol = jQuery($gridControls[i]);
                                FwBrowse.search($gridcontrol);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        }
                    }

                    const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                    if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                        for (let i = 0; i < $browseControls.length; i++) {
                            const $browseControl = jQuery($browseControls[i]);
                            FwBrowse.search($browseControl);
                        }
                    }
                }
                $tab.addClass('tabGridsLoaded');
            }
        });

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PurchaseOrderId"] input').val(uniqueids.PurchaseOrderId);
        FwModule.loadForm(this.Module, $form);
        //$form.find('.vendorinvoice').append(this.openVendorInvoiceBrowse($form));
        $form.find('.contractSubModule').append(this.openContractBrowse($form));
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        const poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        const $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                PurchaseOrderId: poId
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        // ----------
        //const $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        //const $orderStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('OrderStatusHistoryGrid');
        //$orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        //$orderStatusHistoryGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //    };
        //});
        //FwBrowse.init($orderStatusHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderStatusHistoryGrid',
            gridSecurityId: 'k6GqKM4CQRme',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                }
            }
        });
        // ----------
        //const $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        //const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        //$orderItemGridRentalControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridRentalControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-formreadonly', 'true');

        //$orderItemGridRental.empty().append($orderItemGridRentalControl);
        //$orderItemGridRentalControl.data('isSummary', false);
        //$orderItemGridRental.addClass('R');

        //$orderItemGridRentalControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'R'
        //    };
        //});
        //$orderItemGridRentalControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'R';
        //});

        //FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
        //    const rentalItems = $form.find('.rentalgrid tbody').children();
        //    rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        //});

        //FwBrowse.init($orderItemGridRentalControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.rentalgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'Vw70Sf1kT0HR',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'R'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $browse.data('isSummary', false);
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-formreadonly', 'true');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                const rentalItems = $form.find('.rentalgrid tbody').children();
                rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
            }
        });
        // ----------
        //const $orderItemGridSubRent = $form.find('.subrentalgrid div[data-grid="OrderItemGrid"]');
        //const $orderItemGridSubRentControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        //$orderItemGridSubRentControl.find('.suborder').attr('data-visible', 'true');
        //$orderItemGridSubRentControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridSubRentControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridSubRentControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridSubRent.empty().append($orderItemGridSubRentControl);
        //$orderItemGridSubRent.addClass('R');
        //$orderItemGridSubRentControl.data('isSummary', false);

        //$orderItemGridSubRentControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'R',
        //        Summary: true,
        //        Subs: true
        //    };
        //});
        //$orderItemGridSubRentControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'R';
        //    request.Summary = true;
        //    request.Subs = true;
        //});
        //FwBrowse.addEventHandler($orderItemGridSubRentControl, 'afterdatabindcallback', () => {
        //    const subrentItems = $form.find('.subrentalgrid tbody').children();
        //    subrentItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubRent"]')) : FwFormField.enable($form.find('[data-datafield="SubRent"]'));
        //});

        //FwBrowse.init($orderItemGridSubRentControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridSubRentControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.subrentalgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: '4xH0ub5zREgD',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'R',
                    Summary: true,
                    Subs: true
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'R';
                request.Summary = true;
                request.Subs = true;
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $browse.data('isSummary', false);
                $browse.find('.suborder').attr('data-visible', 'true');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                const subrentItems = $form.find('.subrentalgrid tbody').children();
                subrentItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubRent"]')) : FwFormField.enable($form.find('[data-datafield="SubRent"]'));
            }
        });
        // ----------
        //const $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        //const $orderNoteGridControl = FwBrowse.loadGridFromTemplate('OrderNoteGrid');
        //$orderNoteGrid.empty().append($orderNoteGridControl);
        //$orderNoteGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //    };
        //});
        //$orderNoteGridControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //});
        //FwBrowse.init($orderNoteGridControl);
        //FwBrowse.renderRuntimeHtml($orderNoteGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderNoteGrid',
            gridSecurityId: 'rf47IV47DeSX',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    assignBarCodes($form) {
        const mode = 'EDIT';
        let purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $assignBarCodesForm = AssignBarCodesController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $assignBarCodesForm);
        jQuery('.tab.submodule.active').find('.caption').html('Assign Bar Codes');
    }
    //----------------------------------------------------------------------------------------------
    returnToVendor($form) {
        let mode = 'EDIT';
        let purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        let $returnToVendorForm = ReturnToVendorController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $returnToVendorForm);
        jQuery('.tab.submodule.active').find('.caption').html('Return To Vendor');
    }
    //----------------------------------------------------------------------------------------------
    search($form) {
        let orderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

        if (orderId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            let search = new SearchInterface();
            let $popup = search.renderSearchPopup($form, orderId, 'PurchaseOrder');
        }
    }
    //----------------------------------------------------------------------------------------------
    receiveFromVendor($form) {
        let mode = 'EDIT';
        let purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        let $receiveFromVendorForm = ReceiveFromVendorController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $receiveFromVendorForm);
        jQuery('.tab.submodule.active').find('.caption').html('Receive From Vendor');
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: JQuery): void {
        const uniqueid = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        const  status = FwFormField.getValueByDataField($form, 'Status');

        if (status === 'VOID' || status === 'CLOSED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
        }

        if (!FwFormField.getValueByDataField($form, 'Rental')) { $form.find('[data-type="tab"][data-caption="Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'SubRent')) { $form.find('[data-type="tab"][data-caption="Sub-Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Repair')) { $form.find('[data-type="tab"][data-caption="Repair"]').hide() }

        const $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSubRent = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
    
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridSubRent.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();

        $orderItemGridSubRent.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();

        $orderItemGridSubRent.find('.buttonbar').hide();

        this.dynamicColumns($form);
        this.disableCheckboxesOnLoad($form);
    };
    //----------------------------------------------------------------------------------------------
    activityCheckboxEvents($form, mode) {
        const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'),
              subrentalTab = $form.find('[data-type="tab"][data-caption="Sub-Rental"]'),
              repairTab = $form.find('[data-type="tab"][data-caption="Repair"]');
        $form.find('[data-datafield="Rental"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                } else {
                    rentalTab.hide();
                }
            } else {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                    FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                } else {
                    rentalTab.hide();
                    FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                }
            }
        });

        $form.find('[data-datafield="SubRent"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? subrentalTab.show() : subrentalTab.hide();
        });
        $form.find('[data-datafield="Repair"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? repairTab.show() : repairTab.hide();
        });
    };
    //----------------------------------------------------------------------------------------------
    disableCheckboxesOnLoad($form: any): void {
        // If a record has xxx items, user cannot uncheck corresponding activity checkbox
        if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        //if (FwFormField.getValueByDataField($form, 'HasRentalSaleItem')) {              // These fields are being served but no corresponding tab or checkbox at the moment
        //    FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
        //}
        //if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
        //    FwFormField.disable(FwFormField.getDataField($form, ''));
        //}
    }
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: any): void {
        const POTYPE = FwFormField.getValueByDataField($form, "PoTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $partGrid = $form.find('.partgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            $subRentalGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]'),
            $subSaleGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]'),
            $subLaborGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]'),
            $subMiscGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field');
        let fieldNames: Array<string> = [];

        for (let i = 3; i < fields.length; i++) {
            let name = jQuery(fields[i]).attr('data-mappedfield');
            if (name !== "QuantityOrdered") {
                fieldNames.push(name);
            }
        }

        FwAppData.apiMethod(true, 'GET', `api/v1/potype/${POTYPE}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            let hiddenPurchase: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.PurchaseShowFields));
            let hiddenMisc: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.MiscShowFields));
            let hiddenLabor: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.LaborShowFields));
            let hiddenSubRental: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubRentalShowFields));
            let hiddenSubSale: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubSaleShowFields));
            let hiddenSubMisc: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubMiscShowFields));
            let hiddenSubLabor: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubLaborShowFields));
            // Non-specific showfields
            for (let i = 0; i < hiddenPurchase.length; i++) {
                jQuery($rentalGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
                jQuery($salesGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
                jQuery($partGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
            }
            // Specific showfields
            for (let i = 0; i < hiddenMisc.length; i++) { jQuery($miscGrid.find(`[data-mappedfield="${hiddenMisc[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenLabor.length; i++) { jQuery($laborGrid.find(`[data-mappedfield="${hiddenLabor[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubSale.length; i++) { jQuery($subSaleGrid.find(`[data-mappedfield="${hiddenSubSale[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubRental.length; i++) { jQuery($subRentalGrid.find(`[data-mappedfield="${hiddenSubRental[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubLabor.length; i++) { jQuery($subLaborGrid.find(`[data-mappedfield="${hiddenSubLabor[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubMisc.length; i++) { jQuery($subMiscGrid.find(`[data-mappedfield="${hiddenSubMisc[i]}"]`)).parent().hide(); }
        }, null, null);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        //Hides Search option for sub item grids
        $form.find('[data-issubgrid="true"] .submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) { };
    //----------------------------------------------------------------------------------------------
}

//----------------------------------------------------------------------------------------------
//FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.ReceiveFromVendor.id] = function (e) {
//    var $form, $receiveFromVendorForm;
//    try {
//        $form = jQuery(this).closest('.fwform');
//        var mode = 'EDIT';
//        var purchaseOrderInfo: any = {};
//        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
//        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
//        $receiveFromVendorForm = ReceiveFromVendorController.openForm(mode, purchaseOrderInfo);
//        FwModule.openSubModuleTab($form, $receiveFromVendorForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Receive From Vendor');
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.ReturnToVendor.id] = function (e) {
//    let $form, $returnToVendorForm;
//    try {
//        $form = jQuery(this).closest('.fwform');
//        let mode = 'EDIT';
//        let purchaseOrderInfo: any = {};
//        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
//        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
//        $returnToVendorForm = ReturnToVendorController.openForm(mode, purchaseOrderInfo);
//        FwModule.openSubModuleTab($form, $returnToVendorForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Return To Vendor');
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------

//FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.Search.id] = function (e) {
//    let search, $form, orderId, $popup;
//    $form = jQuery(this).closest('.fwform');
//    orderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

//    if (orderId == "") {
//        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//    } else {
//        search = new SearchInterface();
//        $popup = search.renderSearchPopup($form, orderId, 'PurchaseOrder');
//    }
//};
//----------------------------------------------------------------------------------------------
//Assign Bar Codes
//FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.AssignBarCodes.id] = function (e) {
//    const $form = jQuery(this).closest('.fwform'); 
//    try {
//        const mode = 'EDIT';
//        let purchaseOrderInfo: any = {};
//        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
//        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
//        const $assignBarCodesForm = AssignBarCodesController.openForm(mode, purchaseOrderInfo);
//        FwModule.openSubModuleTab($form, $assignBarCodesForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Assign Bar Codes');
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------

var PurchaseOrderController = new PurchaseOrder();
