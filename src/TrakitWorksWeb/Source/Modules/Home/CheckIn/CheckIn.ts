routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });

class CheckIn {
    Module: string = 'CheckIn';
    caption: string = Constants.Modules.Home.CheckIn.caption;
	nav: string = Constants.Modules.Home.CheckIn.nav;
	id: string = Constants.Modules.Home.CheckIn.id;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        if (typeof parentmoduleinfo !== 'undefined') {
            if (this.Module === 'CheckIn') {
                FwFormField.setValueByDataField($form, 'OrderId', parentmoduleinfo.OrderId, parentmoduleinfo.OrderNumber);
                $form.find(`[data-datafield="OrderId"]`).change();
            } else if (this.Module === 'TransferIn') {
                FwFormField.setValueByDataField($form, 'TransferId', parentmoduleinfo.TransferId, parentmoduleinfo.TransferNumber);
                $form.find(`[data-datafield="TransferId"]`).change();
            }
            $form.attr('data-showsuspendedsessions', 'false');
        }
        this.getSoundUrls($form);
        this.events($form);
        this.getSuspendedSessions($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        let showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/checkin/suspendedsessionsexist', null, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
            }, null, $form);

            $form.on('click', '.suspendedsession', e => {
                let html = `<div>
                 <div style="background-color:white; padding-right:10px; text-align:right;" class="close-modal"><i style="cursor:pointer;" class="material-icons">clear</i></div>
<div id="suspendedSessions" style="max-width:90vw; max-height:90vh; overflow:auto;"></div>
            </div>`;

                let $popup = FwPopup.renderPopup(jQuery(html), { ismodal: true });

                let $browse = SuspendedSessionController.openBrowse();
                let officeLocationId = JSON.parse(sessionStorage.getItem('location'));
                officeLocationId = officeLocationId.locationid;
                $browse.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OfficeLocationId: officeLocationId
                        , SessionType: 'IN'
                        , OrderType: 'O'
                    }
                });

                FwPopup.showPopup($popup);
                jQuery('#suspendedSessions').append($browse);
                FwBrowse.search($browse);

                $popup.find('.close-modal > i').one('click', function (e) {
                    FwPopup.destroyPopup($popup);
                    jQuery(document).find('.fwpopup').off('click');
                    jQuery(document).off('keydown');
                });

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    let dealId = $this.find(`[data-browsedatafield="DealId"]`).attr('data-originalvalue');
                    let dealNumber = $this.find(`[data-browsedatafield="DealNumber"]`).attr('data-originalvalue');
                    if (dealId !== "") {
                        FwFormField.setValueByDataField($form, 'DealId', dealId, dealNumber);
                    }
                    FwFormField.setValueByDataField($form, 'OrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="OrderId"] input').change();
                    $form.find('.suspendedsession').hide();
                });

            });
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const $checkedInItemsGrid = $form.find('div[data-grid="CheckedInItemGrid"]');
        const $checkedInItemsGridControl = FwBrowse.loadGridFromTemplate('CheckedInItemGrid');
        $checkedInItemsGrid.empty().append($checkedInItemsGridControl);
        $checkedInItemsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkedInItemsGridControl);
        FwBrowse.renderRuntimeHtml($checkedInItemsGridControl);
        //----------------------------------------------------------------------------------------------
        const $checkInExceptionGrid = $form.find('div[data-grid="CheckInExceptionGrid"]');
        const $checkInExceptionGridControl = FwBrowse.loadGridFromTemplate('CheckInExceptionGrid');
        $checkInExceptionGrid.empty().append($checkInExceptionGridControl);
        $checkInExceptionGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInExceptionGridControl);
        FwBrowse.renderRuntimeHtml($checkInExceptionGridControl);
        //----------------------------------------------------------------------------------------------
        const $checkInOrderGrid = $form.find('div[data-grid="CheckInOrderGrid"]');
        const $checkInOrderGridControl = FwBrowse.loadGridFromTemplate('CheckInOrderGrid');
        $checkInOrderGrid.empty().append($checkInOrderGridControl);
        $checkInOrderGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInOrderGridControl);
        FwBrowse.renderRuntimeHtml($checkInOrderGridControl);
        //----------------------------------------------------------------------------------------------
        const $checkInSwapGrid = $form.find('div[data-grid="CheckInSwapGrid"]');
        const $checkInSwapGridControl = FwBrowse.loadGridFromTemplate('CheckInSwapGrid');
        $checkInSwapGrid.empty().append($checkInSwapGridControl);
        $checkInSwapGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInSwapGridControl);
        FwBrowse.renderRuntimeHtml($checkInSwapGridControl);
        //----------------------------------------------------------------------------------------------
        const $checkInQuantityItemsGrid = $form.find('div[data-grid="CheckInQuantityItemsGrid"]');
        const $checkInQuantityItemsGridControl = FwBrowse.loadGridFromTemplate('CheckInQuantityItemsGrid');
        $checkInQuantityItemsGrid.empty().append($checkInQuantityItemsGridControl);
        $checkInQuantityItemsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                AllOrdersForDeal: FwFormField.getValueByDataField($form, 'AllOrdersForDeal'),
                OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
            }
        });
        FwBrowse.init($checkInQuantityItemsGridControl);
        FwBrowse.renderRuntimeHtml($checkInQuantityItemsGridControl);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse: any, $form: any, request: any) {
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            CheckIn: true
            , CheckInWarehouseId: warehouseId
        }
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateSpecificOrder($browse: any, $form: any, request: any) {
        let dealId = FwFormField.getValueByDataField($form, 'DealId');
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.uniqueids = {
            DealId: dealId
        }
        request.miscfields = {
            CheckIn: true,
            CheckInWarehouseId: warehouseId
        }
    }
    //----------------------------------------------------------------------------------------------
    getSoundUrls($form): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let errorMsg, errorSound, successSound, department, self = this;
        department = JSON.parse(sessionStorage.getItem('department'));
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        errorMsg = $form.find('.error-msg:not(.qty)');
        const $checkInQuantityItemsGridControl = $form.find('div[data-name="CheckInQuantityItemsGrid"]');
        const allActiveOrders = $form.find('[data-datafield="AllOrdersForDeal"] input');
        const specificOrder = $form.find('[data-datafield="SpecificOrder"] input');
        const specificOrderValidation = $form.find('div[data-datafield="SpecificOrderId"]');
        const type = (this.Module === 'CheckIn' ? 'Order' : 'Transfer');

        //Default Department
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        //Order selection
        $form.find('[data-datafield="OrderId"], [data-datafield="TransferId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            if (type === 'Order') {
                FwFormField.setValueByDataField($form, 'DealId', $tr.find('[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Deal"]').attr('data-originalvalue'));
                FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
            } else if (type === 'Transfer') {
                FwFormField.disable($form.find('[data-datafield="TransferId"]'));
            }

            let request: any = {};
            request = {
              OrderId: FwFormField.getValueByDataField($form, `${type}Id`)
                , DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId')
            }
            if (this.Module === 'CheckIn') request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/startcheckincontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                $form.find('[data-datafield="BarCode"] input').focus();
            }, null, null);

            $form.find('.suspendedsession').hide();
        });
        //Deal selection
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
            let request: any = {};
            request = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
                , DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/startcheckincontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                $form.find('[data-datafield="BarCode"] input').focus();
            }, null, null);
        });
        //BarCode input
        $form.find('[data-datafield="BarCode"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                this.checkInItem($form);
            }
        });
        //Quantity input
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                let type = 'Quantity';
                this.checkInItem($form, type);
            }
        });
        //Add Order to Contract
        $form.find('.addordertocontract').on('click', e => {
            errorMsg.html('');
            let type = 'AddOrderToContract';
            this.checkInItem($form, type);
        });
        //Swap Item
        $form.find('.swapitem').on('click', e => {
            errorMsg.html('');
            let type = 'SwapItem';
            this.checkInItem($form, type);
        });
        //Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId) {
                FwAppData.apiMethod(true, 'POST', `api/v1/checkin/completecheckincontract/${contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.fwformfield').not('[data-datafield="DepartmentId"]').find('input').val('');
                    $form.find('div[data-name="CheckedInItemGrid"] tr.viewmode').empty();
                    errorMsg.html('');
                    FwFormField.enable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
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
        $form.find('div.quantityitemstab').on('click', e => {
            //Disable clicking Quantity Items tab w/o a ContractId
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (contractId) {
                FwBrowse.search($checkInQuantityItemsGridControl);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.');
            }
            if (orderId === '') {
                if ($form.find('.optionlist').css('display') === 'none') {
                    $form.find('.optionlist').toggle();
                }
                allActiveOrders.prop('checked', true);
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
        });

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
            orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            orderInfo.OrderNumber = FwFormField.getTextByDataField($form, 'OrderId');
            $orderStatusForm = OrderStatusController.openForm('EDIT', orderInfo);
            FwModule.openSubModuleTab($form, $orderStatusForm);
            jQuery('.tab.submodule.active').find('.caption').html('Order Status');
        });
        //Refresh grid on Check-In tab click
        $form.find('.checkintab').on('click', e => {
            const $checkedInItemsGrid = $form.find('div[data-name="CheckedInItemGrid"]');
            FwBrowse.search($checkedInItemsGrid);
        });
    };
    //----------------------------------------------------------------------------------------------
    checkInItem($form, type?: string) {
        let errorSound, successSound, notificationSound;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        notificationSound = new Audio(this.notificationSoundFileName);

        $form.find('.swapitem').hide();
        let contractId = FwFormField.getValueByDataField($form, 'ContractId');
        let request: any = {};
        request = {
            Code: FwFormField.getValueByDataField($form, 'BarCode')
        }
        if (contractId) {
            request.ContractId = contractId;
        }

        switch (type) {
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

        FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/checkinitem', request, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.success) {
                successSound.play();
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                FwFormField.setValueByDataField($form, 'QuantityIn', response.InventoryStatus.QuantityIn);
                FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                if (type !== 'SwapItem') {
                    FwFormField.setValueByDataField($form, 'OrderId', response.OrderId, response.OrderNumber);
                    FwFormField.setValueByDataField($form, 'Description', response.OrderDescription);
                }
                FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));

                let $checkedInItemsGridControl = $form.find('div[data-name="CheckedInItemGrid"]');
                FwBrowse.search($checkedInItemsGridControl);
                $form.find('[data-datafield="BarCode"] input').select();

                if (response.status === 107) {
                    successSound.play();
                    $form.find('[data-datafield="Quantity"] input').select();
                }

                if (type === 'Quantity') {
                    FwFormField.setValueByDataField($form, 'Quantity', 0);
                    $form.find('[data-datafield="BarCode"] input').select();
                }
            }
            else if (!response.success) {
                if (response.ShowSwap) {
                    notificationSound.play();
                    $form.find('.swapitem').show();
                } else {
                    errorSound.play();
                    $form.find('.swapitem').hide();
                }
                $form.find('.error-msg:not(.qty)').html(`<div><span>${response.msg}</span></div>`);
                $form.find('[data-datafield="BarCode"] input').select();
            }
            response.ShowNewOrder ? $form.find('.addordertocontract').show() : $form.find('.addordertocontract').hide();
        }, null, contractId ? null : $form);
    }
    //----------------------------------------------------------------------------------------------
}
var CheckInController = new CheckIn();
