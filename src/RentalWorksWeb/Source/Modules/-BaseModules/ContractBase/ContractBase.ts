abstract class ContractBase {
    Module: string;
    apiurl: string;
    caption: string;
    nav: string;
    id: string;
    ActiveViewFields: any;
    ActiveViewFieldsId: string;
    BillingDate: string;
    uniqueIdFieldName: string = "ContractId";
    numberFieldName: string = "ContractNumber";
    //----------------------------------------------------------------------------------------------
    afterAddBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }
        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        switch (this.Module) {
            case 'Contract':
                $browse.data('ondatabind', request => {
                    request.activeviewfields = this.ActiveViewFields;
                });
                break;
            case 'Manifest':
                $browse.data('ondatabind', request => {
                    request.activeviewfields = this.ActiveViewFields;
                    request.uniqueids.ContractType = 'MANIFEST';
                });
                break;
            case 'TransferReceipt':
                $browse.data('ondatabind', request => {
                    request.activeviewfields = this.ActiveViewFields;
                    request.uniqueids.ContractType = 'RECEIPT';
                });
                break;
        }

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('div[data-datafield="DeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Deliver to Customer' },
            { value: 'SHIP', text: 'Ship to Customer' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);
        //Toggle Buttons - SubRental tab - Sub-Rental totals
        FwFormField.loadItems($form.find('div[data-datafield="DeliveryAddressType"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'VENUE', caption: 'Venue' },
            { value: 'WAREHOUSE', caption: 'Warehouse' },
            { value: 'OTHER', caption: 'Other' }
        ]);
        this.events($form);
        this.renderPrintButton($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const module = (this.Module == 'Contract' ? 'Contract' : 'Manifest');

        const $form = this.openForm('EDIT');
        $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(uniqueids[`${module}Id`]);

        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        const $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', () => {
            this.printContract($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Track shipment
        if (this.Module === 'Contract') {
            $form.find('.track-shipment').on('click', e => {
                const trackingURL = FwFormField.getValueByDataField($form, 'DeliveryFreightTrackingUrl');
                if (trackingURL !== '') {
                    try {
                        window.open(trackingURL);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            });
        }
        // DeliveryType radio in Deliver tab
        $form.find('div[data-datafield="DeliveryAddressType"]').on('change', event => {
            this.deliveryTypeAddresses($form, event);
        });
        // Stores previous value for DeliveryDeliveryType
        $form.find('div[data-datafield="DeliveryDeliveryType"]').on('click', event => {
            const $element = jQuery(event.currentTarget);
            $element.data('prevValue', FwFormField.getValueByDataField($form, 'DeliveryDeliveryType'))
        });
        // Delivery type select field on Deliver tab
        $form.find('div[data-datafield="DeliveryDeliveryType"]').on('change', event => {
            const $element = jQuery(event.currentTarget);
            const newValue = $element.find('.fwformfield-value').val();
            const prevValue = $element.data('prevValue');

            if (newValue === 'DELIVER' && prevValue === 'PICK UP') {
                FwFormField.setValueByDataField($form, 'DeliveryAddressType', 'DEAL');
            }
            if (newValue === 'SHIP' && prevValue === 'PICK UP') {
                FwFormField.setValueByDataField($form, 'DeliveryAddressType', 'DEAL');
            }
            if (newValue === 'PICK UP') {
                FwFormField.setValueByDataField($form, 'DeliveryAddressType', 'WAREHOUSE');
            }
            $form.find('div[data-datafield="DeliveryAddressType"]').change();
        });
    }
    //----------------------------------------------------------------------------------------------
    printContract($form: JQuery): void {
        try {
            let docType, $report;
            switch (this.Module) {
                case 'Contract':
                    const contractType = FwFormField.getValueByDataField($form, 'ContractType');
                    docType = 'Contract';
                    if (contractType && contractType === 'LOST') {
                        $report = LostContractReportController.openForm();
                    }
                    if (contractType && contractType === 'OUT') {
                        $report = OutContractReportController.openForm();
                    }
                    if (contractType && contractType === 'IN') {
                        $report = InContractReportController.openForm();
                    }
                    if (contractType && contractType === 'RETURN') {
                        $report = ReturnContractReportController.openForm();
                    }
                    if (contractType && contractType === 'RECEIVE') {
                        $report = ReceiveContractReportController.openForm();
                    }
                    if (contractType && contractType === 'EXCHANGE') {
                        $report = ExchangeContractReportController.openForm();
                    }
                    if (contractType && contractType === 'MANIFEST') {
                        $report = TransferManifestReportController.openForm();
                    }
                    if (contractType && contractType === 'RECEIPT') {
                        $report = TransferReceiptReportController.openForm();
                    }
                    FwModule.openSubModuleTab($form, $report);
                    break;
                case 'Manifest':
                    docType = 'Manifest';
                    $report = TransferManifestReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    break;
                case 'TransferReceipt':
                    docType = 'Receipt';
                    $report = TransferReceiptReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    break;
            }

            const id = $form.find(`div.fwformfield[data-datafield="${this.uniqueIdFieldName}"] input`).val();
            const number = $form.find(`div.fwformfield[data-datafield="${this.numberFieldName}"] input`).val();
            $report.find(`div.fwformfield[data-datafield="ContractId"] input`).val(id);
            $report.find(`div.fwformfield[data-datafield="ContractId"] .fwformfield-text`).val(number);
            jQuery('.tab.submodule.active').find('.caption').html(`Print ${docType}`);

            const printTab = jQuery('.tab.submodule.active');
            printTab.find('.caption').html(`Print ${docType}`);
            const recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
            printTab.attr('data-caption', `${docType} ${recordTitle}`);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    voidContract($form: JQuery): void {
        try {
            const $confirmation = FwConfirmation.renderConfirmation('Void Entire Contract', '');
            const $voidItems = FwConfirmation.addButton($confirmation, 'Void Items', false);
            FwConfirmation.addButton($confirmation, 'Cancel', true);
            const html = [];
            const contractType = FwFormField.getValueByDataField($form, 'ContractType');
            if (contractType === 'OUT') {
                html.push('<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unstage items after voiding" data-datafield="ReturnToInventory"></div>');
            }
            html.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="Reason" style="width:600px;"></div>');
            FwConfirmation.addControls($confirmation, html.join(''));

            $voidItems.on('click', e => {
                const request: any = {};
                const contractId = FwFormField.getValueByDataField($form, this.uniqueIdFieldName);
                request.uniqueids = {
                    ContractId: contractId
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/contractitemdetail/browse`, request, FwServices.defaultTimeout, response => {
                    const orderIdColumnIndex = response.ColumnIndex.OrderId;
                    const orderItemIdColumnIndex = response.ColumnIndex.OrderItemId;
                    const vendorIndex = response.ColumnIndex.VendorId;
                    const barcodeColumnIndex = response.ColumnIndex.Barcode;
                    const quantityColumnIndex = response.ColumnIndex.Quantity;
                    const isVoidColumnIndex = response.ColumnIndex.IsVoid;
                    const items = [];
                    for (let i = 0; i < response.Rows.length; i++) {
                        if (!response.Rows[i][isVoidColumnIndex]) {
                            const item = {
                                OrderId: response.Rows[i][orderIdColumnIndex],
                                OrderItemId: response.Rows[i][orderItemIdColumnIndex],
                                VendorId: response.Rows[i][vendorIndex],
                                BarCode: response.Rows[i][barcodeColumnIndex],
                                Quantity: response.Rows[i][quantityColumnIndex]
                            }
                            items.push(item);
                        }
                    }

                    const voiditemsrequest: any = {
                        Items: items,
                        ContractId: contractId,
                        Reason: FwFormField.getValueByDataField($confirmation, 'Reason')
                    };

                    if (contractType === 'OUT') {
                        let returnToInventory = FwFormField.getValueByDataField($confirmation, 'ReturnToInventory');
                        returnToInventory = returnToInventory === 'T' ? true : false;
                        request.ReturnToInventory = returnToInventory;
                    }

                    FwAppData.apiMethod(true, 'POST', `api/v1/contractitemdetail/voiditems`, voiditemsrequest, FwServices.defaultTimeout,
                        response => {
                            if (!response.success) {
                                FwNotification.renderNotification('ERROR', response.msg);
                            }
                            FwModule.refreshForm($form);
                            FwConfirmation.destroyConfirmation($confirmation);
                        },
                        ex => FwFunc.showError(ex), $confirmation.find('.fwconfirmationbox'));

                }, ex => FwFunc.showError(ex), $form);
            }); 
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const module = (this.Module == 'Contract' ? 'Contract' : 'Manifest');
        FwBrowse.renderGrid({
            nameGrid: 'ContractSummaryGrid',
            gridSecurityId: 'a8I3UCKA3LN3',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, this.uniqueIdFieldName)
                };
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'ContractDetailGrid',
            gridSelector: '.rentaldetailgrid div[data-grid="ContractDetailGrid"]',
            gridSecurityId: 'uJtRkkpKi8zT',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;

                FwMenu.addSubMenuItem(options.$groupActions, 'Void Items', 'pbZeqJ3pd8r', (e: JQuery.ClickEvent) => {
                    try {
                        this.VoidItems($form, e);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, this.uniqueIdFieldName),
                    RecType: 'R, RS'
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
            },
        });

        FwBrowse.renderGrid({
            nameGrid: 'ContractDetailGrid',
            gridSelector: '.salesdetailgrid div[data-grid="ContractDetailGrid"]',
            gridSecurityId: 'uJtRkkpKi8zT',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;

                FwMenu.addSubMenuItem(options.$groupActions, 'Void Items', 'pbZeqJ3pd8r', (e: JQuery.ClickEvent) => {
                    try {
                        this.VoidItems($form, e);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, this.uniqueIdFieldName),
                    RecType: 'S'
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
            },
        });

        FwBrowse.renderGrid({
            nameGrid: 'ContractExchangeItemGrid',
            gridSecurityId: 'Azkpehs1tvl',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, this.uniqueIdFieldName),
                };
            }
        });

    }
    //----------------------------------------------------------------------------------------------
    VoidItems($form: JQuery, event) {
        try {
            const $element = jQuery(event.currentTarget);
            const $grid = jQuery($element.closest('[data-type="Grid"]'));
            const $summaryGrid = $form.find('[data-name="ContractSummaryGrid"]');
            const contractItems: any = [];
            const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
            const contractId = FwFormField.getValueByDataField($form, this.uniqueIdFieldName);
            let isSingleRow = false;
            if ($selectedCheckBoxes.length > 0) {
                if ($selectedCheckBoxes.length === 1) { isSingleRow = true }
                const $confirmation = FwConfirmation.renderConfirmation('Void Items', '');
                const $select = FwConfirmation.addButton($confirmation, 'Void Items', false);
                FwConfirmation.addButton($confirmation, 'Cancel', true);
                const html = [];
                const contractType = FwFormField.getValueByDataField($form, 'ContractType');
                if (contractType === 'OUT') {
                    html.push('<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unstage items after voiding" data-datafield="ReturnToInventory"></div>');
                }
                if (isSingleRow) {
                    html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="width:75px;"></div>');
                }
                html.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="Reason" style="width:600px;"></div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                if (isSingleRow) {
                    const confirmationQty = jQuery($selectedCheckBoxes[0]).closest('tr').find('div[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                    FwFormField.setValueByDataField($confirmation, 'Quantity', confirmationQty)
                }
                $select.on('click', () => {
                    for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                        const $this = jQuery($selectedCheckBoxes[i]);
                        const $tr = $this.closest('tr');
                        const isVoid = $tr.find('div[data-browsedatafield="IsVoid"]').attr('data-originalvalue');
                        if (isVoid == 'false') {
                            const orderId = $tr.find('div[data-browsedatafield="OrderId"]').attr('data-originalvalue');
                            const orderItemId = $tr.find('div[data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
                            const vendorId = $tr.find('div[data-browsedatafield="VendorId"]').attr('data-originalvalue');
                            const barCode = $tr.find('div[data-browsedatafield="Barcode"]').attr('data-originalvalue');
                            let quantity;
                            if (isSingleRow) {
                                quantity = FwFormField.getValueByDataField($confirmation, 'Quantity');
                            } else {
                                quantity = $tr.find('div[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                            }
                            const item = {
                                OrderId: orderId,
                                OrderItemId: orderItemId,
                                VendorId: vendorId,
                                BarCode: barCode,
                                Quantity: quantity
                            }
                            contractItems.push(item);
                        }
                    };

                    const request: any = {
                        Items: contractItems,
                        ContractId: contractId,
                        Reason: FwFormField.getValueByDataField($confirmation, 'Reason')
                    };

                    if (contractType === 'OUT') {
                        let returnToInventory = FwFormField.getValueByDataField($confirmation, 'ReturnToInventory');
                        returnToInventory = returnToInventory === 'T' ? true : false;
                        request.ReturnToInventory = returnToInventory;
                    }

                    FwAppData.apiMethod(true, 'POST', `api/v1/contractitemdetail/voiditems`, request, FwServices.defaultTimeout,
                        response => {
                            if (!response.success) {
                                FwNotification.renderNotification('ERROR', response.msg);
                            }
                            FwBrowse.search($grid);
                            FwBrowse.search($summaryGrid);
                            FwConfirmation.destroyConfirmation($confirmation);
                        },
                        ex => FwFunc.showError(ex), $confirmation.find('.fwconfirmationbox'));
                });
            } else {
                FwNotification.renderNotification('WARNING', 'Select items in the grid to continue.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $contractSummaryGrid = $form.find('[data-name="ContractSummaryGrid"]');
        FwBrowse.search($contractSummaryGrid);
        const $contractRentalGrid = $form.find('.rentaldetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractRentalGrid);
        const $contractSalesGrid = $form.find('.salesdetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractSalesGrid);
        const $contractExchangeItemGrid = $form.find('[data-name="ContractExchangeItemGrid"]');
        FwBrowse.search($contractExchangeItemGrid);

        if (this.Module == 'Contract') {
            this.BillingDate = FwFormField.getValueByDataField($form, 'BillingDate');
            const type = FwFormField.getValueByDataField($form, 'ContractType');
            const $billing = $form.find('[data-datafield="BillingDate"] .fwformfield-caption');

            switch (type) {
                case 'RECEIVE':
                    $billing.html('Billing Start');
                    break;
                case 'OUT':
                    $billing.html('Billing Start');
                    break;
                case 'IN':
                    $billing.html('Billing Stop');
                    break;
                case 'RETURN':
                    $billing.html('Billing Stop');
                    break;
                case 'LOST':
                    $billing.html('Billing Stop');
                    break;
                default:
                    $billing.html('Billing Date');
                    break;
            }
        }

        const showSales = FwFormField.getValueByDataField($form, 'Sales');
        const showRental = FwFormField.getValueByDataField($form, 'Rental');
        const showExchange = FwFormField.getValueByDataField($form, 'Exchange');

        if (showSales) {
            $form.find('[data-type="tab"][data-caption="Sales Detail"]').show();
        } else {
            $form.find('[data-type="tab"][data-caption="Sales Detail"]').hide();
        }

        if (showRental) {
            $form.find('[data-type="tab"][data-caption="Rental Detail"]').show();
        } else {
            $form.find('[data-type="tab"][data-caption="Rental Detail"]').hide();
        }

        if (showExchange) {
            //$form.find('.summary-grid').hide();
            //$form.find('.exchange-item-grid').show();
            $contractSummaryGrid.hide();
            $contractExchangeItemGrid.show();
        } else {
            //$form.find('.summary-grid').show();
            //$form.find('.exchange-item-grid').hide();
            $contractSummaryGrid.show();
            $contractExchangeItemGrid.hide();
        }

        if (this.Module == 'Contract') {
            // Highlight Billing Date field if adjusted
            if (FwFormField.getValueByDataField($form, 'BillingDateAdjusted') === true) {
                $form.find('[data-datafield="BillingDate"] .fwformfield-control').css('background-color', '#ff6f6f')
            }
        }

        //Disable 'Track Shipment' button
        if (this.Module === 'Contract') {
            const trackingNumber = FwFormField.getValueByDataField($form, 'DeliveryFreightTrackingNumber');
            const $trackShipmentBtn = $form.find('.track-shipment');
            if (trackingNumber === '') {
                FwFormField.disable($trackShipmentBtn);
            } else {
                FwFormField.enable($trackShipmentBtn);
            }
            // Billing Date change
            $form.find('div[data-datafield="BillingDate"]').on('changeDate', event => {
                const billingDate = FwFormField.getValueByDataField($form, 'BillingDate');

                if (billingDate !== this.BillingDate) {
                    $form.find('.date-change-reason').show();
                    $form.find('div[data-datafield="BillingDateChangeReason"]').attr('data-required', 'true');

                } else {
                    $form.find('.date-change-reason').hide();
                    FwFormField.setValueByDataField($form, 'BillingDateChangeReason', '');
                    $form.find('div[data-datafield="BillingDateChangeReason"]').attr('data-required', 'false');
                }
            });
            // After Save, remove and clear out reason row
            $form.find('.date-change-reason').hide();
            FwFormField.setValueByDataField($form, 'BillingDateChangeReason', '');
        }
    }
    //----------------------------------------------------------------------------------------------
    deliveryTypeAddresses($form: any, event: any): void {
        const value = FwFormField.getValueByDataField($form, 'DeliveryAddressType');
        if (value === 'WAREHOUSE') {
            this.getWarehouseAddress($form);
        } else if (value === 'DEAL') {
            this.fillDeliveryAddressFieldsforDeal($form);
        }
    }
    //----------------------------------------------------------------------------------------------
    getWarehouseAddress($form: any): void {
        let WHresponse: any = {};

        if ($form.data('whAddress')) {
            WHresponse = $form.data('whAddress');
            FwFormField.setValueByDataField($form, `DeliveryToLocation`, WHresponse.Warehouse);
            FwFormField.setValueByDataField($form, `DeliveryToAttention`, WHresponse.Attention);
            FwFormField.setValueByDataField($form, `DeliveryToAddress1`, WHresponse.Address1);
            FwFormField.setValueByDataField($form, `DeliveryToAddress2`, WHresponse.Address2);
            FwFormField.setValueByDataField($form, `DeliveryToCity`, WHresponse.City);
            FwFormField.setValueByDataField($form, `DeliveryToState`, WHresponse.State);
            FwFormField.setValueByDataField($form, `DeliveryToZipCode`, WHresponse.Zip);
            FwFormField.setValueByDataField($form, `DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
        } else {
            const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            FwAppData.apiMethod(true, 'GET', `api/v1/warehouse/${warehouseId}`, null, FwServices.defaultTimeout, response => {
                WHresponse = response;
                FwFormField.setValueByDataField($form, `DeliveryToLocation`, WHresponse.Warehouse);
                FwFormField.setValueByDataField($form, `DeliveryToAttention`, WHresponse.Attention);
                FwFormField.setValueByDataField($form, `DeliveryToAddress1`, WHresponse.Address1);
                FwFormField.setValueByDataField($form, `DeliveryToAddress2`, WHresponse.Address2);
                FwFormField.setValueByDataField($form, `DeliveryToCity`, WHresponse.City);
                FwFormField.setValueByDataField($form, `DeliveryToState`, WHresponse.State);
                FwFormField.setValueByDataField($form, `DeliveryToZipCode`, WHresponse.Zip);
                FwFormField.setValueByDataField($form, `DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
                // Preventing unnecessary API calls once warehouse addresses have been requested once
                $form.data('whAddress', {
                    'Warehouse': response.Warehouse,
                    'Attention': response.Attention,
                    'Address1': response.Address1,
                    'Address2': response.Address2,
                    'City': response.City,
                    'State': response.State,
                    'Zip': response.Zip,
                    'CountryId': response.CountryId,
                    'Country': response.Country
                })
            }, null, null);
        }
    }
    //----------------------------------------------------------------------------------------------
    fillDeliveryAddressFieldsforDeal($form: any): void {
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, res => {
            FwFormField.setValueByDataField($form, `DeliveryToLocation`, res.Deal);
            FwFormField.setValueByDataField($form, `DeliveryToAttention`, res.ShipAttention);
            FwFormField.setValueByDataField($form, `DeliveryToAddress1`, res.ShipAddress1);
            FwFormField.setValueByDataField($form, `DeliveryToAddress2`, res.ShipAddress2);
            FwFormField.setValueByDataField($form, `$DeliveryToCity`, res.ShipCity);
            FwFormField.setValueByDataField($form, `$DeliveryToState`, res.ShipState);
            FwFormField.setValueByDataField($form, `DeliveryToZipCode`, res.ShipZipCode);
            FwFormField.setValueByDataField($form, `DeliveryToCountryId`, res.ShipCountryId, res.ShipCountry);
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'DeliveryCarrierId':
                request.miscfields = {
                    Freight: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeliverycarrier`);
                break;
            case 'DeliveryShipViaId':
                const deliveryCarrierId = jQuery($form.find('[data-datafield="DeliveryCarrierId"] input')).val();
                request.uniqueids = {
                    VendorId: deliveryCarrierId
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateshipvia`);
                break;
            case 'DeliveryToCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeliverytocountry`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------  
    getBrowseTemplate(): string {
        return `
      <div data-name="Contract" data-control="FwBrowse" data-type="Browse" id="ContractBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="ContractController">
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="ContractId" data-browsedatatype="key"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Contract No." data-datafield="ContractNumber" data-browsedatatype="text" data-sort="desc" data-sortsequence="3" data-searchfieldoperators="startswith"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Type" data-datafield="ContractType" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Date" data-datafield="ContractDate" data-browsedatatype="date" data-cellcolor="ContractDateColor" data-sortsequence="1" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Time" data-datafield="ContractTime" data-browsedatatype="text" data-cellcolor="ContractTimeColor" data-sortsequence="2" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Department" data-datafield="Department" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Vendor" data-datafield="Vendor" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Order/PO Description" data-datafield="PoOrderDescription" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Billing Start/Stop" data-datafield="BillingDate" data-browsedatatype="date" data-cellcolor="BillingDateColor" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
        </div>
         <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Location" data-datafield="Location" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column spacer" data-width="auto" data-visible="true"></div>
      </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
      <div id="contractform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Contract" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ContractController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ContractId"></div>
        <div id="contractform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
            <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental Detail"></div>
            <div data-type="tab" id="salestab" class="tab" data-tabpageid="salestabpage" data-caption="Sales Detail"></div>
            <div data-type="tab" id="deliverytab" class="tab" data-tabpageid="deliverytabpage" data-caption="Delivery"></div>
            <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
          </div>
          <div class="tabpages">
            <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contract Number" data-datafield="ContractNumber" style="float:left;width:250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="ContractDate" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="ContractTime" style="float:left;width:100px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="OfficeLocationValidation" data-displayfield="Location" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="WarehouseValidation" data-displayfield="Warehouse" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="ContractType" style="float:left;width:200px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="DepartmentValidation" data-displayfield="Department" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" style="float:left;width:250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Billing Start" data-datafield="BillingDate" style="float:left;width:150px;" data-enabled="true"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="float:left;width:250px;display:none;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="float:left;width:250px;display:none;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exchange" data-datafield="Exchange" style="float:left;width:250px;display:none;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="BillingDateAdjusted" data-datafield="BillingDateAdjusted" style="float:left;width:250px;display:none;"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="DealValidation" data-displayfield="Deal" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" style="float:left;width:250px;display:none;" data-enabled="false"></div>
                  </div>
                </div>
                <div class="flexrow date-change-reason" style="max-width:800px;display:none;padding-left:10px;">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Date Change Reason" data-datafield="BillingDateChangeReason" style="float:left;width:250px;" data-enabled="true"></div>
                  </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Summary">
                  <div class="flexrow summary-grid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractSummaryGrid" data-securitycaption="Contract Summary"></div>
                  </div>
                  <div class="flexrow exchange-item-grid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractExchangeItemGrid" data-securitycaption="Contract Exchange Item"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="rentaltabpage" class="tabpage" data-tabid="rentaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                  <div class="flexrow rentaldetailgrid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Rental Detail"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="salestabpage" class="tabpage" data-tabid="salestab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                  <div class="flexrow salesdetailgrid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Sales Detail"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="deliverytabpage" class="tabpage" data-tabid="deliverytab">
                <div class="flexpage" style="max-width:500px;">
                 <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 575px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Delivery">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="DeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="DeliveryRequiredDate" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="DeliveryRequiredTime" style="flex:0 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="DeliveryToContact" style="flex:1 1 210px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="DeliveryToContactPhone" style="flex:0 1 150px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="DeliveryCarrierId" data-displayfield="DeliveryCarrier"></div>
                        <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="DeliveryShipViaId" data-displayfield="DeliveryShipVia"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking URL" data-datafield="DeliveryFreightTrackingUrl" data-allcaps="false" style="display:none;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking Number" data-datafield="DeliveryFreightTrackingNumber" data-allcaps="false" style="flex:1 1 250px;"></div>
                        <div class="fwformcontrol track-shipment" data-type="button" style="flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Track Shipment</div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:75%;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Address" data-datafield="DeliveryAddressType" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="DeliveryToLocation"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="DeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="DeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="DeliveryToAddress2"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="DeliveryToCity"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="DeliveryToState"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="DeliveryToZipCode"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="DeliveryToCountryId" data-displayfield="DeliveryToCountry"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="DeliveryToCrossStreets"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="DeliveryDeliveryNotes"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="DeliveryOnlineOrderNumber"></div>
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="DeliveryOnlineOrderStatus"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                </div>
            </div>
            <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                   <div class="flexrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Contract Notes" data-datafield="Note" data-height="500px"></div>
                   </div>
                   <div class="flexrow">
                       <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print on Order" data-datafield="PrintNoteOnOrder"></div>
                   </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>`;
    }
    //----------------------------------------------------------------------------------------------
}