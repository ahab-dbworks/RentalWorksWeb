routes.push({ pattern: /^module\/inventorypurchaseutility$/, action: function (match: RegExpExecArray) { return InventoryPurchaseUtilityController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class InventoryPurchaseUtility {
    Module: string = 'InventoryPurchaseUtility';
    caption: string = Constants.Modules.Utilities.children.InventoryPurchaseUtility.caption;
    apiurl: string = 'api/v1/inventorypurchaseutility';
    nav: string = Constants.Modules.Utilities.children.InventoryPurchaseUtility.nav;
    id: string = Constants.Modules.Utilities.children.InventoryPurchaseUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'View Inventory Purchase Sessions', 'S3zkxYNnBXzo', (e: JQuery.ClickEvent) => {
            try {
                this.viewSessions(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');
        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $manufacturerValidation = $form.find('[data-datafield="ManufacturerVendorId"]');
        $manufacturerValidation.data('beforevalidate', ($form, $manufacturerValidation, request) => {
            request.uniqueids = {
                'Manufacturer': true,
            }
        });

        const $purchaseValidation = $form.find('[data-datafield="PurchaseVendorId"]');
        $purchaseValidation.data('beforevalidate', ($form, $purchaseValidation, request) => {
            request.uniqueids = {
                'RentalInventory': true,
            }
        });

        const $rentalInventoryValidation = $form.find('.icode[data-datafield="InventoryId"]');
        $rentalInventoryValidation.data('beforevalidate', ($form, $rentalInventoryValidation, request) => {
            request.uniqueids = {
                'WarehouseId': warehouse.warehouseid,
                'Classification': 'I,A',
            }
        });

        const $rentalInventoryDescValidation = $form.find('.description[data-datafield="InventoryId"]');
        $rentalInventoryDescValidation.data('beforevalidate', ($form, $rentalInventoryDescValidation, request) => {
            request.uniqueids = {
                'WarehouseId': warehouse.warehouseid,
                'Classification': 'I,A',
            }
        });

        this.setDefaults($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    setDefaults($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);
        FwFormField.setValueByDataField($form, 'Quantity', 1);
        FwFormField.setValueByDataField($form, 'PurchaseDate', today);
        FwFormField.setValueByDataField($form, 'ReceiveDate', today);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $itemGridControl = $form.find('[data-name="InventoryPurchaseItemGrid"]');

        $form.find('.description[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue2($form.find('.icode[data-datafield="InventoryId"]'), FwBrowse.getValueByDataField(null, $tr, 'InventoryId'), FwBrowse.getValueByDataField(null, $tr, 'ICode'))
            $form.find('.icode[data-datafield="InventoryId"]').data('onchange')($tr);
        });

        $form.find('.icode[data-datafield="InventoryId"]').data('onchange', $tr => {
            const trackedBy = FwBrowse.getValueByDataField(null, $tr, 'TrackedBy');
            if (trackedBy === 'QUANTITY') {
                $form.find('.tracked-by').hide();
            } else {
                $form.find('.tracked-by').show();
            }
            $form.find('.additems').show();
            const inventoryId = FwBrowse.getValueByDataField(null, $tr, 'InventoryId');
            const description = FwBrowse.getValueByDataField(null, $tr, 'Description');
            FwFormField.setValue2($form.find('.description[data-datafield="InventoryId"]'), inventoryId, description, false);
            const unitVal = FwBrowse.getValueByDataField(null, $tr, 'UnitValue');
            FwFormField.setValueByDataField($form, 'UnitCost', unitVal);
            const aisleLoc = FwBrowse.getValueByDataField(null, $tr, 'AisleLocation');
            FwFormField.setValueByDataField($form, 'AisleLocation', aisleLoc);
            const shelfLoc = FwBrowse.getValueByDataField(null, $tr, 'ShelfLocation');
            FwFormField.setValueByDataField($form, 'ShelfLocation', shelfLoc);

            const request: any = {};
            request.InventoryId = inventoryId;
            request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
            if (typeof $form.data('sessionid') === 'string') {
                request.SessionId = $form.data('sessionid');
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/updatesession`, request, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            } else {
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/startsession`, request, FwServices.defaultTimeout,
                    response => {
                        $form.data("sessionid", response.SessionId);
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            }
        });

        $form.find('[data-datafield="WarrantyDays"]').on('change', e => {
            const days = FwFormField.getValueByDataField($form, 'WarrantyDays');
            const today = FwFunc.getDate();
            const expiration = FwFunc.getDate(today, parseInt(days));
            FwFormField.setValueByDataField($form, 'WarrantyExpiration', expiration);
        });

        $form.find('[data-datafield="ManufacturerVendorId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'CountryId', FwBrowse.getValueByDataField(null, $tr, 'CountryId'), FwBrowse.getValueByDataField(null, $tr, 'Country'));
        });

        $form.find('[data-datafield="Quantity"]').on('change', e => {
            const request: any = {};
            request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
            if (typeof $form.data('sessionid') === 'string') {
                request.SessionId = $form.data('sessionid');
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/updatesession`, request, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            }
        });

        //Add items button
        $form.find('.additems').on('click', e => {
            let request: any = {
                SessionId: $form.data('sessionid'),
                InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
                Quantity: FwFormField.getValueByDataField($form, 'Quantity'),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
                AisleLocation: FwFormField.getValueByDataField($form, 'AisleLocation'),
                ShelfLocation: FwFormField.getValueByDataField($form, 'ShelfLocation'),
                ManufacturerVendorId: FwFormField.getValueByDataField($form, 'ManufacturerVendorId'),
                ManufacturerModelNumber: FwFormField.getValueByDataField($form, 'ManufacturerModelNumber'),
                ManufacturerPartNumber: FwFormField.getValueByDataField($form, 'ManufacturerPartNumber'),
                CountryId: FwFormField.getValueByDataField($form, 'CountryId'),
                WarrantyDays: FwFormField.getValueByDataField($form, 'WarrantyDays'),
                WarrantyExpiration: FwFormField.getValueByDataField($form, 'WarrantyExpiration'),
                PurchaseVendorId: FwFormField.getValueByDataField($form, 'PurchaseVendorId'),
                OutsidePoNumber: FwFormField.getValueByDataField($form, 'OutsidePoNumber'),
                PurchaseDate: FwFormField.getValueByDataField($form, 'PurchaseDate'),
                ReceiveDate: FwFormField.getValueByDataField($form, 'ReceiveDate'),
                VendorPartNumber: FwFormField.getValueByDataField($form, 'VendorPartNumber'),
                UnitCost: FwFormField.getValueByDataField($form, 'UnitCost'),
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/inventorypurchaseutility/completesession', request, FwServices.defaultTimeout,
                response => {
                    if (response.success) {
                        FwNotification.renderNotification("SUCCESS", "Inventory Purchase Completed Successfully");
                        $form.find('.fwformfield input').val('');
                        $itemGridControl.find('tr.viewmode').empty();
                        $form.removeData('sessionid');
                        this.setDefaults($form);
                        //    const uniqueids: any = {};
                        //    uniqueids.PurchaseId = 
                        //    const $control = PurchaseController.loadForm(uniqueids);
                        //    FwModule.openModuleTab($control, "", true, 'FORM', true);
                    } else {
                        FwNotification.renderNotification("ERROR", response.msg);
                    }
                }, ex => FwFunc.showError(ex), $form);
        });

        //Assign Bar Codes button
        $form.find('.assignbarcodes').on('click', e => {
            if (typeof $form.data('sessionid') === 'string') {
                const request: any = {};
                request.SessionId = $form.data('sessionid');
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/assignbarcodes`, request, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'InventoryPurchaseItemGrid',
            gridSecurityId: 'qH0cLrQVt9avI',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    SessionId: $form.data('sessionid')
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.on('keydown', '[data-browsedatafield="BarCode"], [data-browsedatafield="SerialNumber"]', e => {
                    const keycode = e.keyCode || e.which;
                    if (keycode === 13) {
                        const $tr = jQuery(e.currentTarget).parents('tr');
                        const datafield = jQuery(e.currentTarget).attr('data-browsedatafield');
                        let $nextRow = FwBrowse.selectNextRow($browse);
                        const nextIndex = FwBrowse.getSelectedIndex($browse);
                        FwBrowse.saveRow($browse, $tr)
                            .then((value) => {
                                if (nextIndex != -1) {
                                    $nextRow = FwBrowse.selectRowByIndex($browse, nextIndex);
                                    FwBrowse.setRowEditMode($browse, $nextRow);
                                    $browse.data('selectedfield', datafield);
                                }
                            });
                    }
                });
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    saveBarCodeOnRow($form, $browse, evt) {
        try {
            const $this = jQuery(evt.currentTarget);
            const $tr = $this.parents('tr');
            const datafield = $this.attr('data-browsedatafield');
            let $nextRow = FwBrowse.selectNextRow($browse);
            const nextIndex = FwBrowse.getSelectedIndex($browse);

            FwBrowse.saveRow($browse, $tr)
                .then((value) => {
                    if (nextIndex != -1) {
                        $nextRow = FwBrowse.selectRowByIndex($browse, nextIndex);
                        FwBrowse.setRowEditMode($browse, $nextRow);
                        $browse.data('selectedfield', datafield);
                    }
                });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    viewSessions($form) {

                const $browse = InventoryPurchaseSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Inventory Purchase Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                       
                    }
                });
                FwBrowse.search($browse);

                $browse.on('dblclick', 'tr.viewmode', e => {

                    FwPopup.destroyPopup($popup);
                });
    }
    //----------------------------------------------------------------------------------------------
    //async temporaryBarCodes($form, $browse, $tr, exception?) {

    //    async function retreiveSessionIdOrBarcodes(data): Promise<any> {
    //        return FwAjax.callWebApi<any, any>({
    //            httpMethod: 'POST',
    //            data: data,
    //            url: `${applicationConfig.apiurl}api/v1/inventorypurchaseitem/browse/`,
    //            $elementToBlock: jQuery('#application'),
    //        })
    //    }
    //    const request: any = {
    //        orderby: "BarCode",
    //        uniqueids: {
    //            BarCode: FwBrowse.getValueByDataField($browse, $tr, 'BarCode'),
    //        }
    //    };

    //    await retreiveSessionIdOrBarcodes(request) // Based on the BarCode, retrieve the sessionId associated with its prior activity
    //        .then(res => {
    //            const sessionId = res.Rows[0][1];
    //            const thisSessionId = $form.data('sessionid');

    //            if (thisSessionId !== sessionId) { // Allow normal fw error handling if user is trying to use the same barcode multiple times within a session
    //                const request: any = {
    //                    orderby: "BarCode",
    //                    uniqueids: {
    //                        SessionId: sessionId,
    //                    }
    //                };
    //                retreiveSessionIdOrBarcodes(request) // use SessionId gained from above to get a list of all BarCodes from the session
    //                    .then(res => {
    //                        const rows = res.Rows;
    //                        const barCodes = [];
    //                        const sessionIds = []
    //                        for (let i = 0; i < rows.length; i++) {
    //                            const barCode = rows[i][2];
    //                            const sessionId = rows[i][0];
    //                            if (barCode !== '') {
    //                                barCodes.push(barCode);
    //                                sessionIds.push(sessionId);
    //                            } else {
    //                                console.error(`BarCode with ${sessionId} is a blank value.`)
    //                            }
    //                        }
    //                        const $confirmation = FwConfirmation.renderConfirmation(`Temporary Bar Codes`, '');
    //                        $confirmation.find('.fwconfirmationbox').css('width', '490px');

    //                        const html: Array<string> = [];
    //                        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
    //                        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
    //                        html.push(`    <div>The Bar Code${barCodes.length > 1 ? 's' : ''} ${barCodes.join(', ')} exist${barCodes.length > 1 ? '' : 's'} with a previous session. Delete and free up for future use?</div>`);
    //                        html.push('  </div>');
    //                        html.push('</div>');

    //                        FwConfirmation.addControls($confirmation, html.join(''));
    //                        const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
    //                        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
    //                        $yes.focus();
    //                        async function deleteBarCodes(sessionId): Promise<any> {
    //                            return FwAjax.callWebApi<any, any>({
    //                                httpMethod: 'DELETE',
    //                                data: null,
    //                                url: `${applicationConfig.apiurl}api/v1/inventorypurchaseitem/${sessionId}`,
    //                                $elementToBlock: jQuery('#application'),
    //                            });
    //                        }
    //                        $yes.on('click', async e => {
    //                            FwConfirmation.destroyConfirmation($confirmation);
    //                            for (let i = 0; i < sessionIds.length; i++) {
    //                                if (sessionIds[i] !== '') {
    //                                    await deleteBarCodes(sessionIds[i])
    //                                        .then(res => {
    //                                            FwNotification.renderNotification('INFO', `The Bar Code ${barCodes[i]} has been deleted and is free for use.`);
    //                                        });
    //                                }
    //                            }
    //                        });
    //                    });
    //            }
    //        });
    //}
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'ManufacturerVendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturervendor`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'PurchaseVendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepurchasevendor`);
                break;
        }
    }
};
//----------------------------------------------------------------------------------------------
var InventoryPurchaseUtilityController = new InventoryPurchaseUtility();