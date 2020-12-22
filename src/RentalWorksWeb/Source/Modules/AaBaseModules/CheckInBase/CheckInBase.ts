abstract class CheckInBase implements IModule {
    Module: string;
    apiurl: string;
    caption: string;
    nav: string;
    id: string;
    Type: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(): IModuleScreen {
        const screen: IModuleScreen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        switch (this.Module) {
            case 'CheckIn':
                this.Type = 'Order';
                break;
            case 'TransferIn':
                this.Type = 'Transfer';
                break;
        }
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        if (typeof parentmoduleinfo !== 'undefined') {
            if (this.Module === 'CheckIn') {
                FwFormField.setValueByDataField($form, 'OrderId', parentmoduleinfo.OrderId, parentmoduleinfo.OrderNumber);
            } else if (this.Module === 'TransferIn') {
                FwFormField.setValueByDataField($form, 'TransferId', parentmoduleinfo.TransferId, parentmoduleinfo.TransferNumber);
            }
            jQuery($form.find(`[data-datafield="${this.Type}Id"] input`)).trigger('change');
            $form.attr('data-showsuspendedsessions', 'false');
        }

        this.events($form);
        this.getSuspendedSessions($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        if (showSuspendedSessions != "false") {
            let sessionType;
            //let orderType;
            switch (this.Module) {
                case 'CheckIn':
                    sessionType = 'IN';
                    //orderType = 'O';
                    break;
                case 'TransferIn':
                    sessionType = 'RECEIPT';
                    //orderType = 'T';
                    break;
            }
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/suspendedsessionsexist?warehouseId=${warehouseId}`, null, FwServices.defaultTimeout, response => {
                if (response) {
                    $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
                }
            }, ex => FwFunc.showError(ex), $form);

            $form.on('click', '.suspendedsession', e => {
                const $browse = SuspendedSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Suspended Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                        SessionType: sessionType,
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                    }
                });
                FwBrowse.search($browse);

                $browse.on('dblclick', 'tr.viewmode', e => {
                    const $tr = jQuery(e.currentTarget);
                    const orderId = FwBrowse.getValueByDataField($browse, $tr, 'OrderId');
                    const orderNo = FwBrowse.getValueByDataField($browse, $tr, 'OrderNumber');
                    const contractId = FwBrowse.getValueByDataField($browse, $tr, 'ContractId');
                    FwFormField.setValueByDataField($form, 'ContractId', contractId);
                    FwFormField.setValueByDataField($form, `${this.Type}Id`, orderId, orderNo);
                    if (this.Module == 'CheckIn') {
                        const dealId = FwBrowse.getValueByDataField($browse, $tr, 'DealId');
                        const dealNumber = FwBrowse.getValueByDataField($browse, $tr, 'DealOrVendor');
                        if (dealId !== "") {
                            FwFormField.setValueByDataField($form, 'DealId', dealId, dealNumber);
                        }
                    }
                    FwPopup.destroyPopup($popup);
                    $form.find(`[data-datafield="${this.Type}Id"]`).data().onchange($tr);
                    $form.find('.suspendedsession').hide();

                    const $checkedInItemGrid = $form.find('div[data-name="CheckedInItemGrid"]');
                    FwBrowse.search($checkedInItemGrid);
                });
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'CheckedInItemGrid',
            gridSecurityId: 'RanTH3xgxNy',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 9999,  // for regression test to be able to select all rows and cancel them
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;

                FwMenu.addSubMenuItem(options.$groupActions, 'Cancel Selected Items', '8bSrfYlth57y', (e: JQuery.ClickEvent) => {
                    try {
                        this.cancelItems($form, e);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                if (this.Module === 'TransferIn') {
                    $browse.attr('data-caption', 'Transferred In Items');
                }
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'CheckInExceptionGrid',
            gridSecurityId: '3S49xMb3FrcD',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                };
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'CheckInOrderGrid',
            gridSecurityId: 'HSZSZp9Ovrpq',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                };
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'CheckInSwapGrid',
            gridSecurityId: 'hA3FE9ProwUn',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                };
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'CheckInQuantityItemsGrid',
            gridSecurityId: 'BfClP5w8rjl7',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    AllOrdersForDeal: FwFormField.getValueByDataField($form, 'AllOrdersForDeal'),
                    OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                    OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    async cancelItems($form: JQuery, event): Promise<any> {
        const $element = jQuery(event.currentTarget);
        const $grid = jQuery($element.closest('[data-type="Grid"]'));
        const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
        const errorMsg = $form.find('.error-msg:not(.qty)');

        if ($selectedCheckBoxes.length !== 0) {
            await this.cancelSelectedItems($form, $selectedCheckBoxes).then(errorMessages => {
                FwBrowse.search($grid);
                if (errorMessages.length == 0) {
                    errorMsg.html('');
                    FwFunc.playSuccessSound();
                }
                else {
                    FwFunc.playErrorSound();
                    errorMsg.html(`<div><span>${errorMessages.join('<br>')}</span></div>`);
                }
            });

        } else {
            FwNotification.renderNotification('WARNING', 'Select rows to unstage in order to perform this function.');
        }
    }
    //----------------------------------------------------------------------------------------------
    async cancelSelectedItems($form, $selectedCheckBoxes): Promise<Array<string>> {
        function delay(ms: number) {
            return new Promise(resolve => setTimeout(resolve, ms));
        }

        let responseCount = 0;
        const errorMessages: Array<string> = [];
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            const $tr = $selectedCheckBoxes.eq(i).closest('tr');

            const orderId = FwBrowse.getValueByDataField(null, $tr, 'OrderId');
            const contractId = FwBrowse.getValueByDataField(null, $tr, 'ContractId');
            const orderTranId = FwBrowse.getValueByDataField(null, $tr, 'OrderTranId');
            const internalChar = FwBrowse.getValueByDataField(null, $tr, 'InternalChar');
            const orderItemId = FwBrowse.getValueByDataField(null, $tr, 'OrderItemId');
            const inventoryId = FwBrowse.getValueByDataField(null, $tr, 'InventoryId');
            const vendorId = FwBrowse.getValueByDataField(null, $tr, 'VendorId');
            const qty = FwBrowse.getValueByDataField(null, $tr, 'Quantity');
            const description = FwBrowse.getValueByDataField(null, $tr, 'Description');

            const request = {
                OrderTranId: orderTranId,
                InternalChar: internalChar,
                ContractId: contractId,
                OrderId: orderId,
                OrderItemId: orderItemId,
                InventoryId: inventoryId,
                Description: description,
                Quantity: qty,
                VendorId: vendorId
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/checkin/cancelcheckinitems`, request, FwServices.defaultTimeout, response => {
                responseCount++;
                // success/error response not programmed yet
                //if (response.success) {
                // //
                //}
                //else {
                //    errorMessages.push(response.msg);  // gather all errors into the errorMessages array
                //}
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        }

        while (responseCount < $selectedCheckBoxes.length) {
            await delay(1000);
        }

        return errorMessages;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        switch (datafield) {
            case 'OrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
                request.miscfields = {
                    CheckIn: true,
                    CheckInWarehouseId: warehouseId
                }
                break;
            case 'TransferId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetransfer`);
                request.miscfields = {
                    TransferIn: true,
                    TransferInWarehouseId: warehouseId
                }
                break;
            case 'SpecificOrderId':
                const dealId = FwFormField.getValueByDataField($form, 'DealId');
                request.uniqueids = {
                    DealId: dealId
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatespecificorder`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    //beforeValidateSpecificOrder($browse: any, $form: any, request: any) {
    //    const dealId = FwFormField.getValueByDataField($form, 'DealId');
    //    const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
    //    request.uniqueids = {
    //        DealId: dealId
    //    }
    //    request.miscfields = {
    //        CheckIn: true,
    //        CheckInWarehouseId: warehouseId
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        const errorMsg = $form.find('.error-msg:not(.qty)');
        const type = (this.Module === 'CheckIn' ? 'Order' : 'Transfer');

        //Default Department
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        //Order selection
        $form.find('[data-datafield="OrderId"], [data-datafield="TransferId"]').data('onchange', $tr => {
            let descriptionFieldName;
            if ($tr.find('[data-browsedatafield="Description"]').length > 0) {
                descriptionFieldName = "Description";
            } else {
                descriptionFieldName = "OrderDescription";
            }
            FwFormField.setValueByDataField($form, 'Description', FwBrowse.getValueByDataField($form, $tr, descriptionFieldName));
            if (type === 'Order') {
                let dealName;
                if ($tr.find('[data-browsedatafield="Deal"]').length > 0) {
                    dealName = "Deal";
                } else {
                    dealName = "DealOrVendor";
                }
                FwFormField.setValueByDataField($form, 'DealId', FwBrowse.getValueByDataField($form, $tr, 'DealId'), FwBrowse.getValueByDataField($form, $tr, dealName));
                FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
            } else if (type === 'Transfer') {
                FwFormField.disable($form.find('[data-datafield="TransferId"]'));
            }

            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId.length === 0) {
                let request: any = {};
                request = {
                    OrderId: FwFormField.getValueByDataField($form, `${type}Id`),
                    DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId'),
                    OfficeLocationId: JSON.parse(sessionStorage.getItem('location')).locationid,
                    WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                }
                if (this.Module === 'CheckIn') {
                    request.DealId = FwFormField.getValueByDataField($form, 'DealId');
                }
                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/startcheckincontract`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                    $form.find('.suspendedsession').hide();
                    $form.find('[data-datafield="BarCode"] input').focus();
                }, null, null);
            }
        });
        //Deal selection
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId.length === 0) {
                FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
                const request: any = {
                    DealId: FwFormField.getValueByDataField($form, 'DealId'),
                    DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId'),
                    OfficeLocationId: JSON.parse(sessionStorage.getItem('location')).locationid,
                    WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                }
                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/startcheckincontract`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                    $form.find('.suspendedsession').hide();
                    $form.find('[data-datafield="BarCode"] input').focus();
                }, null, null);
            }
        });
        //BarCode input
        $form.find('[data-datafield="BarCode"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                let checkInTranType = 'BarCode';
                this.checkInItem($form, checkInTranType);
            }
        });
        //Quantity input
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                let checkInTranType = 'Quantity';
                this.checkInItem($form, checkInTranType);
            }
        });
        //Add Order to Contract
        $form.find('.addordertocontract').on('click', e => {
            errorMsg.html('');
            let checkInTranType = 'AddOrderToContract';
            this.checkInItem($form, checkInTranType);
        });
        //Swap Item
        $form.find('.swapitem').on('click', e => {
            errorMsg.html('');
            let checkInTranType = 'SwapItem';
            this.checkInItem($form, checkInTranType);
        });
        //Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId) {
                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/completecheckincontract/${contractId}`, null, FwServices.defaultTimeout,
                    response => {
                        let contractInfo: any = {}, $contractForm;
                        contractInfo.ContractId = response.ContractId;
                        $contractForm = ContractController.loadForm(contractInfo);
                        FwModule.openSubModuleTab($form, $contractForm);
                        this.resetForm($form);
                    }, null, $form);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.')
            }
        });
        //Refresh grids on tab click
        $form.find('div.exceptionstab').on('click', e => {
            //Disable clicking Exception tab w/o a ContractId
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId) {
                let $checkInExceptionGridControl = $form.find('div[data-name="CheckInExceptionGrid"]');
                FwBrowse.search($checkInExceptionGridControl);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.')
            }
        });
        const $checkInQuantityItemsGridControl = $form.find('div[data-name="CheckInQuantityItemsGrid"]');
        const allActiveOrders = $form.find('[data-datafield="AllOrdersForDeal"] input');
        $form.find('div.quantityitemstab').on('click', e => {
            //Disable clicking Quantity Items tab w/o a ContractId
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let orderId = FwFormField.getValueByDataField($form, `${type}Id`);
            if (contractId) {
                FwBrowse.search($checkInQuantityItemsGridControl);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.')
            }
            if (orderId === '') {
                if ($form.find('.optionlist').css('display') === 'none') {
                    $form.find('.optionlist').toggle();
                }
                if (this.Module == 'CheckIn') {
                    allActiveOrders.prop('checked', true);
                } else if (this.Module == 'TransferIn') {
                    allActiveOrders.prop('checked', false);
                    $form.find('.all-orders').hide();
                }
                FwBrowse.search($checkInQuantityItemsGridControl);
            }
        });
        $form.find('div.orderstab').on('click', e => {
            const $checkInOrderGridControl = $form.find('div[data-name="CheckInOrderGrid"]');
            FwBrowse.search($checkInOrderGridControl);
        });
        $form.find('div.swapitemtab').on('click', e => {
            const $checkInSwapGridControl = $form.find('div[data-name="CheckInSwapGrid"]');
            FwBrowse.search($checkInSwapGridControl);
        });

        //Options button
        $form.find('.optionsbutton').on('click', e => {
            $form.find('.optionlist').toggle();
            if (this.Module == 'TransferIn') {
                allActiveOrders.prop('checked', false);
                $form.find('.all-orders').hide();
            }
        });
        const specificOrder = $form.find('[data-datafield="SpecificOrder"] input');
        const specificOrderValidation = $form.find('div[data-datafield="SpecificOrderId"]');
        //AllOrdersForDeal Checkbox functionality
        allActiveOrders.on('change', e => {
            if (allActiveOrders.prop('checked')) {
                FwFormField.disable(specificOrderValidation);
                specificOrder.prop('checked', false);
                FwBrowse.search($checkInQuantityItemsGridControl);
            } else {
                FwFormField.enable(specificOrderValidation);
                specificOrder.prop('checked', true);
                FwBrowse.search($checkInQuantityItemsGridControl);
            }
        });

        specificOrder.on('change', e => {
            if (specificOrder.prop('checked')) {
                if (specificOrderValidation.find('input').val() !== '') {
                    $checkInQuantityItemsGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                            AllOrdersForDeal: 'F',
                            OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                            OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
                        }
                    });
                    FwBrowse.search($checkInQuantityItemsGridControl);
                }
                FwFormField.enable(specificOrderValidation);
                allActiveOrders.prop('checked', false);
            } else {
                FwFormField.disable(specificOrderValidation);
                allActiveOrders.prop('checked', true);
                FwBrowse.search($checkInQuantityItemsGridControl);
            }
        });

        specificOrderValidation.data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'SpecificDescription', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $checkInQuantityItemsGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    AllOrdersForDeal: FwFormField.getValueByDataField($form, 'AllOrdersForDeal'),
                    OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                    OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
                }
            })
            FwBrowse.search($checkInQuantityItemsGridControl);
        });
        specificOrderValidation.focusout(function () {
            if (jQuery(this).find('input.fwformfield-text').val() === '') {
                FwFormField.setValueByDataField($form, 'SpecificDescription', '');
            }
        })

        $form.find('[data-datafield="ShowQuantityOut"] input').on('change', e => {
            FwBrowse.search($checkInQuantityItemsGridControl);
        });

        //Order Status Button
        $form.find('.orderstatus').on('click', e => {
            let orderInfo: any = {}, $orderStatusForm;
            const $checkedInItemGrid = $form.find('[data-name="CheckedInItemGrid"]');
            if ($checkedInItemGrid.find('tbody >:not(.empty)').length === 0) {
                orderInfo.OrderId = FwFormField.getValueByDataField($form, `${type}Id`);
                orderInfo.OrderNumber = FwFormField.getTextByDataField($form, `${type}Id`);
            } else {
                let $tr;
                if ($checkedInItemGrid.find('tbody tr .tdselectrow input:checked').length === 0) {
                    $tr = $checkedInItemGrid.find('tbody tr .tdselectrow:visible').eq(0).parent('tr');
                } else {
                    $tr = $checkedInItemGrid.find('tbody tr .tdselectrow input:checked').eq(0).parents('tr');
                }
                orderInfo.OrderId = FwBrowse.getValueByDataField($checkedInItemGrid, $tr, 'OrderId');
                orderInfo.OrderNumber = FwBrowse.getValueByDataField($checkedInItemGrid, $tr, 'OrderNumber');
            }

            if (this.Module == 'TransferIn') {
                $orderStatusForm = (<any>window).TransferStatusController.openForm('EDIT', orderInfo);
                FwModule.openSubModuleTab($form, $orderStatusForm);
                const $tab = FwTabs.getTabByElement($orderStatusForm);
                $tab.find('.caption').html('Transfer Status');
            } else {
                $orderStatusForm = OrderStatusController.openForm('EDIT', orderInfo);
                FwModule.openSubModuleTab($form, $orderStatusForm);
                const $tab = FwTabs.getTabByElement($orderStatusForm);
                $tab.find('.caption').html('Order Status');
            }

        });
        //Refresh grid on Check-In tab click
        $form.find('.checkintab').on('click', e => {
            const $checkedInItemsGrid = $form.find('div[data-name="CheckedInItemGrid"]');
            FwBrowse.search($checkedInItemsGrid);
        });

        //Check-In all
        $form.find('.check-in-all').on('click', async e => {
            const $grid = $form.find('[data-name="CheckInQuantityItemsGrid"]');
            const $items = $grid.find('tbody tr');
            const $confirmation = FwConfirmation.renderConfirmation("Checking In All..", '');
            FwConfirmation.addControls($confirmation, `<div style="text-align:center;"><progress class="progress" max="${$items.length}" value="0"></progress></div><div style="margin:10px 0 0 0;text-align:center;"><span class="recordno">1</span> of ${$items.length}<div>`);
            try {
                for (let i = 0; i < $items.length; i++) {
                    $confirmation.find('.recordno').html((i + 1).toString());
                    $confirmation.find('.progress').attr('value', (i + 1).toString());
                    const $row = jQuery($items[i]);
                    await this.checkInAll($form, $grid, $row);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
            finally {
                errorMsg.html('');
                FwConfirmation.destroyConfirmation($confirmation);
                await FwBrowse.search($grid);
            }
        });

        //Cancel Check-In All
        $form.find('.cancel-check-in-all').on('click', async e => {
            const $grid = $form.find('[data-name="CheckInQuantityItemsGrid"]');
            const $items = $grid.find('tbody tr');
            const $confirmation = FwConfirmation.renderConfirmation('Cancelling Check-In All..', '');
            FwConfirmation.addControls($confirmation, `<div style="text-align:center;"><progress class="progress" max="${$items.length}" value="0"></progress></div><div style="margin:10px 0 0 0;text-align:center;"><span class="recordno">1</span> of ${$items.length}<div>`);
            try {
                for (let i = 0; i < $items.length; i++) {
                    $confirmation.find('.recordno').html((i + 1).toString());
                    $confirmation.find('.progress').attr('value', (i + 1).toString());
                    const $row = jQuery($items[i]);
                    await this.cancelCheckInAll($form, $grid, $row);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
            finally {
                errorMsg.html('');
                FwConfirmation.destroyConfirmation($confirmation);
                await FwBrowse.search($grid);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    async checkInAll($form: JQuery, $grid: JQuery, $row: JQuery): Promise<void> {
        return new Promise<void>(async (resolve, reject) => {
            try {
                const errorMsg = $form.find('.error-msg:not(.qty)');
                const qtyOut = parseInt($row.find('[data-browsedatafield="QuantityOut"] .fieldvalue').text());
                const qty = parseInt(FwBrowse.getValueByDataField($grid, $row, 'Quantity'));
                if (qtyOut > 0) {
                    const request = new FwAjaxRequest<any>();
                    request.data = {
                        ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                        OrderItemId: FwBrowse.getValueByDataField($grid, $row, 'OrderItemId'),
                        Code: $row.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext'),
                        Quantity: qtyOut,
                        ModuleType: 'O',
                        VendorId: FwBrowse.getValueByDataField($grid, $row, 'VendorId'),
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid,
                    };
                    request.url = applicationConfig.apiurl + "api/v1/checkin/checkinitem";
                    request.httpMethod = 'POST';
                    const response = await FwAjax.callWebApi<any, any>(request);
                    if (request.xmlHttpRequest.status === 200 || request.xmlHttpRequest.status === 404) {
                        FwBrowse.setFieldValue($grid, $row, 'Quantity', { value: response.InventoryStatus.QuantityOrdered });
                        FwBrowse.setFieldValue($grid, $row, 'QuantityOut', { value: 0, text: "0" });
                        resolve();
                    }
                    else {
                        FwFunc.playErrorSound();
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $row.find('[data-browsedatafield="Quantity"] input').val(qty);
                        reject(response);
                    }
                } else {
                    resolve();
                }
            } catch (ex) {
                reject(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    async cancelCheckInAll($form: JQuery, $grid: JQuery, $row: JQuery): Promise<void> {
        return new Promise<void>(async (resolve, reject) => {
            try {
                const errorMsg = $form.find('.error-msg:not(.qty)');
                const qty = parseInt(FwBrowse.getValueByDataField($grid, $row, 'Quantity'));
                if (qty > 0) {
                    const request = new FwAjaxRequest<any>();
                    request.data = {
                        ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                        OrderItemId: FwBrowse.getValueByDataField($grid, $row, 'OrderItemId'),
                        Code: $row.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext'),
                        Quantity: - qty,
                        ModuleType: 'O',
                        VendorId: FwBrowse.getValueByDataField($grid, $row, 'VendorId'),
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid,
                    };
                    request.url = applicationConfig.apiurl + "api/v1/checkin/checkinitem";
                    request.httpMethod = 'POST';
                    const response = await FwAjax.callWebApi<any, any>(request);
                    if (request.xmlHttpRequest.status === 200 || request.xmlHttpRequest.status === 404) {
                        FwBrowse.setFieldValue($grid, $row, 'Quantity', { value: 0 });
                        FwBrowse.setFieldValue($grid, $row, 'QuantityOut', { value: response.InventoryStatus.QuantityOut, text: response.InventoryStatus.QuantityOut });
                        resolve();
                    }
                    else {
                        FwFunc.playErrorSound();
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $row.find('[data-browsedatafield="Quantity"] input').val(qty);
                        reject(response);
                    }
                } else {
                    resolve();
                }
            } catch (ex) {
                reject(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    checkInItem($form, checkInTranType?: string) {
        const module = this.Module;
        const request: any = {};
        let idType;
        switch (module) {
            case 'CheckIn':
                idType = 'Order';
                request.ModuleType = 'O';
                break;
            case 'TransferIn':
                idType = 'Transfer';
                request.ModuleType = 'T';
                break;
        }

        $form.find('.swapitem').hide();
        let contractId = FwFormField.getValueByDataField($form, 'ContractId');

        request.Code = FwFormField.getValueByDataField($form, 'BarCode');
        request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        if (contractId) {
            request.ContractId = contractId;
        }

        switch (checkInTranType) {
            case 'Quantity':
                request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
                break;
            case 'AddOrderToContract':
                $form.find('[data-type="section"][data-caption="Check-In"] .fwform-section-title').text('Multi-Order Check-In');
                $form.find('.orderstab').show();
                $form.find('[data-datafield="OrderId"]').parents('.flexcolumn').hide();
                request.AddOrderToContract = true;
                break;
            case 'SwapItem':
                $form.find('.swapitemtab').show();
                request.SwapItem = true;
                break;
        }

        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/checkinitem`, request, FwServices.defaultTimeout, response => {
            if (response.success) {
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                FwFormField.setValueByDataField($form, 'QuantityIn', response.InventoryStatus.QuantityIn);
                FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                $form.find('.suspendedsession').hide();
                FwFunc.playSuccessSound();

                if (this.Module == 'CheckIn') FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                if (checkInTranType !== 'SwapItem') {
                    FwFormField.setValueByDataField($form, `${idType}Id`, response.OrderId, response.OrderNumber);
                    FwFormField.setValueByDataField($form, 'Description', response.OrderDescription);
                }

                FwFormField.disable($form.find(`[data-datafield=${idType}Id]`));
                if (this.Module == 'CheckIn') FwFormField.disable($form.find(`[data-datafield="DealId"]`));

                let $checkedInItemsGridControl = $form.find('div[data-name="CheckedInItemGrid"]');
                FwBrowse.search($checkedInItemsGridControl);
                $form.find('[data-datafield="BarCode"] input').select();

                if (response.status === 107) {
                    $form.find('[data-datafield="Quantity"] input').select();
                    FwFunc.playSuccessSound();
                }

                if (checkInTranType === 'Quantity') {
                    FwFormField.setValueByDataField($form, 'Quantity', 0);
                    $form.find('[data-datafield="BarCode"] input').select();
                }
            } else if (!response.success) {
                if (response.ShowSwap) {
                    FwFunc.playNotificationSound();
                    $form.find('.swapitem').show();
                } else {
                    FwFunc.playErrorSound();
                    $form.find('.swapitem').hide();
                }
                $form.find('.error-msg:not(.qty)').html(`<div><span>${response.msg}</span></div>`);
                $form.find('[data-datafield="BarCode"] input').select();
            }
            response.ShowNewOrder ? $form.find('.addordertocontract').show() : $form.find('.addordertocontract').hide();
        }, null, contractId ? null : $form);
    }
    //----------------------------------------------------------------------------------------------
    resetForm($form) {
        $form.find('.error-msg').html('');
        $form.find('.fwformfield').not('[data-datafield="DepartmentId"]').find('input').val('');
        $form.find('div[data-type="checkbox"] input').prop('checked', false);
        $form.find('div[data-name="CheckedInItemGrid"] tr.viewmode').empty();
        $form.find('div[data-name="CheckInQuantityItemsGrid"] tr.viewmode').empty();
        $form.find('div[data-name="CheckInExceptionGrid"] tr.viewmode').empty();

        FwFormField.enable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"], [data-datafield="TransferId"]'));

        $form.find('.suspendedsession').show();
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="checkinform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${this.caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
          <div id="checkinform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="checkintab" class="checkintab tab" data-tabpageid="checkintabpage" data-caption="${this.caption}"></div>
              <div data-type="tab" id="orderstab" class="orderstab tab" data-tabpageid="orderstabpage" data-caption="Orders" style="display:none;"></div>
              <div data-type="tab" id="quantityitemstab" class="quantityitemstab tab" data-tabpageid="quantityitemstabpage" data-caption="Quantity Items"></div>
              <div data-type="tab" id="swapitemtab" class="swapitemtab tab" data-tabpageid="swapitemtabpage" data-caption="Swapped Items" style="display:none;"></div>
              <div data-type="tab" id="exceptionstab" class="exceptionstab tab" data-tabpageid="exceptionstabpage" data-caption="Pending Items"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="checkintabpage" class="tabpage" data-tabid="checkintab">
                <div class="flexpage">
                  <div class="flexrow" style="max-width:1300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="${this.caption}">
                      <div class="flexrow" style="max-width:1300px;">
                        <div class="flexcolumn" style="flex:1 1 450px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:1 1 250px;"></div>
                            ${this.Module == 'CheckIn' ?
                '<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>'
                : '<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transfer No." data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="flex:0 1 175px;"></div>'}
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 450px;">
                          <div class="flexrow">
                            ${this.Module == 'CheckIn' ? `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="${Constants.Modules.Agent.children.Deal.caption}" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:0 1 350px;"></div>` : ''}
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:0 1 200px;" data-enabled="false"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / I-Code" data-datafield="BarCode" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="flex:1 1 300px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="InventoryDescription" style="flex:1 1 400px;" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="flex:0 1 100px; margin-right:267px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="QuantityOrdered" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub" data-datafield="QuantitySub" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Out" data-datafield="QuantityOut" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Staged" data-datafield="QuantityStaged" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In" data-datafield="QuantityIn" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="QuantityRemaining" style="flex:0 1 100px;" data-enabled="false"></div>
                      </div>
                      <div class="error-msg" style="margin-top:8px;"></div>
                      <div class="fwformcontrol addordertocontract" data-type="button" style="display:none; flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Add Order To Contract</div>
                      <div class="fwformcontrol swapitem" data-type="button" style="display:none; flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Swap Item</div>
                      <div class="flexrow" style="max-width:1300px;">
                        <div class="flexcolumn" style="flex:1 1 950px;">
                          <div data-control="FwGrid" data-grid="CheckedInItemGrid" data-securitycaption=""></div>
                        </div>
                    </div>
                      <div class="formrow">
                        <div class="fwformcontrol orderstatus" data-type="button" style="float:left; margin-left:10px;">${this.Module == 'CheckIn' ? 'Order' : 'Transfer'} Status</div>
                        <div class="fwformcontrol createcontract" data-type="button" style="float:right;">Create ${this.Module == 'CheckIn' ? 'Contract' : 'Receipt'}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="orderstabpage" class="tabpage" data-tabid="orderstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInOrderGrid" data-securitycaption=""></div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="quantityitemstabpage" class="tabpage" data-tabid="quantityitemstab">
                <div class="flexpage">
                  <div class="flexrow error-msg qty"></div>
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInQuantityItemsGrid" data-securitycaption=""></div>
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol optionsbutton" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol check-in-all" data-type="button" style="float:left; margin-left:10px;">Check-In All</div>
                    <div class="fwformcontrol cancel-check-in-all" data-type="button" style="float:left; margin-left:10px;">Cancel Check-In All</div>
                  </div>
                  <div class="flexrow optionlist all-orders" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show all ACTIVE Orders for this ${Constants.Modules.Agent.children.Deal.caption}" data-datafield="AllOrdersForDeal" style="flex:0 1 350px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Specific Order" data-datafield="SpecificOrder" style="flex:0 1 150px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="SpecificOrderId" data-displayfield="SpecificOrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="SpecificDescription" style="flex:1 1 250px;" data-enabled="false"></div>
                  </div>
                  <div class="flexrow optionlist" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only show items with Quantity Out" data-datafield="ShowQuantityOut"></div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="swapitemtabpage" class="tabpage" data-tabid="swapitemtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInSwapGrid" data-securitycaption=""></div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="exceptionstabpage" class="tabpage" data-tabid="exceptionstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInExceptionGrid" data-securitycaption=""></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        `;
    }
    //----------------------------------------------------------------------------------------------
}