﻿routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });

class CheckIn {
    Module: string = 'CheckIn';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Check-In', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-CheckInForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="OrderId"] input.fwformfield-value').val(parentmoduleinfo.OrderId);
            $form.find('div[data-datafield="OrderId"] input.fwformfield-text').val(parentmoduleinfo.OrderNumber);
            jQuery($form.find('[data-datafield="OrderId"] input')).trigger('change');
        }
        this.getSoundUrls($form);
        this.events($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $checkedInItemsGrid: any,
            $checkedInItemsGridControl: any;

        $checkedInItemsGrid = $form.find('div[data-grid="CheckedInItemGrid"]');
        $checkedInItemsGridControl = jQuery(jQuery('#tmpl-grids-CheckedInItemGridBrowse').html());
        $checkedInItemsGrid.empty().append($checkedInItemsGridControl);
        $checkedInItemsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkedInItemsGridControl);
        FwBrowse.renderRuntimeHtml($checkedInItemsGridControl);

        let $checkInExceptionGrid: any,
            $checkInExceptionGridControl: any;

        $checkInExceptionGrid = $form.find('div[data-grid="CheckInExceptionGrid"]');
        $checkInExceptionGridControl = jQuery(jQuery('#tmpl-grids-CheckInExceptionGridBrowse').html());
        $checkInExceptionGrid.empty().append($checkInExceptionGridControl);
        $checkInExceptionGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInExceptionGridControl);
        FwBrowse.renderRuntimeHtml($checkInExceptionGridControl);

        let $checkInOrderGrid: any,
            $checkInOrderGridControl: any;

        $checkInOrderGrid = $form.find('div[data-grid="CheckInOrderGrid"]');
        $checkInOrderGridControl = jQuery(jQuery('#tmpl-grids-CheckInOrderGridBrowse').html());
        $checkInOrderGrid.empty().append($checkInOrderGridControl);
        $checkInOrderGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInOrderGridControl);
        FwBrowse.renderRuntimeHtml($checkInOrderGridControl);

        let $checkInSwapGrid: any,
            $checkInSwapGridControl: any;

        $checkInSwapGrid = $form.find('div[data-grid="CheckInSwapGrid"]');
        $checkInSwapGridControl = jQuery(jQuery('#tmpl-grids-CheckInSwapGridBrowse').html());
        $checkInSwapGrid.empty().append($checkInSwapGridControl);
        $checkInSwapGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInSwapGridControl);
        FwBrowse.renderRuntimeHtml($checkInSwapGridControl);
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
    getSoundUrls($form): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let errorSound, successSound, department, self = this;
        department = JSON.parse(sessionStorage.getItem('department'));
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        //Default Department
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        //Order selection
        $form.find('[data-datafield="OrderId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'DealId', $tr.find('[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Deal"]').attr('data-originalvalue'));
            FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));

            let request: any = {};
            request = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
                , OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                , DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/startcheckincontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                $form.find('[data-datafield="BarCode"] input').focus();
            }, null, null);
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
                $form.find('.errormsg').html('');
                this.checkInItem($form);
            }
        });
        //Quantity input
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (e.which === 13) {
                $form.find('.errormsg').html('');
                let quantity = true;
                this.checkInItem($form, quantity);
            }
        });
        //Add Order to Contract
        $form.find('.addordertocontract').on('click', e => {
            $form.find('.errormsg').html('');
            let addOrderToContract = true;
            this.checkInItem($form, addOrderToContract);

            let $checkInOrderGridControl = $form.find('div[data-name="CheckInOrderGrid"]');
            FwBrowse.search($checkInOrderGridControl);
        });
        //Swap Item
        $form.find('.swapitem').on('click', e => {
            $form.find('.errormsg').html('');
            let swapItem = true;
            this.checkInItem($form, swapItem);

            let $checkInSwapGridControl = $form.find('div[data-name="CheckInSwapGrid"]');
            FwBrowse.search($checkInSwapGridControl);
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
                    $form.find('.errormsg').html('');
                    FwFormField.enable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
                }, null, $form);
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    checkInItem($form, addOrderToContract?: boolean, quantity?: boolean, swapItem?: boolean) {
        let errorSound, successSound, notificationSound;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        notificationSound = new Audio(this.notificationSoundFileName);

        let contractId = FwFormField.getValueByDataField($form, 'ContractId');
        let request: any = {};
        request = {
            Code: FwFormField.getValueByDataField($form, 'BarCode')
        }
        if (contractId) {
            request.ContractId = contractId;
        }
        if (addOrderToContract) {
            $form.find('[data-type="section"][data-caption="Check-In"] .fwform-section-title').text('Multi-Order Check-In');
            $form.find('.orderstab').show();
            $form.find('[data-datafield="OrderId"]').parents('.flexcolumn').hide();
            request.AddOrderToContract = true;
        }
        if (quantity) {
            request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
        }
        if (swapItem) {
            request.SwapItem = true;
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
                FwFormField.setValueByDataField($form, 'OrderId', response.OrderId, response.OrderNumber);
                FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                FwFormField.setValueByDataField($form, 'Description', response.OrderDescription);
                FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));

                let $checkedInItemsGridControl = $form.find('div[data-name="CheckedInItemGrid"]');
                FwBrowse.search($checkedInItemsGridControl);
                $form.find('[data-datafield="BarCode"] input').select();

                if (response.status === 107) {
                    successSound.play();
                    $form.find('[data-datafield="Quantity"] input').select();
                }

                if (quantity) {
                    FwFormField.setValueByDataField($form, 'Quantity', 0);
                    $form.find('[data-datafield="BarCode"] input').select();
                }
            }
            else if (!response.success) {
                let errormsg = $form.find('.errormsg')
                    , $checkInExceptionGridControl = $form.find('div[data-name="CheckInExceptionGrid"]');

                if (response.ShowSwap) {
                    notificationSound.play();
                    $form.find('.swapitem').show();
                } else {
                    errorSound.play();
                    $form.find('.swapitem').hide();
                }
                errormsg.html(`<div style="margin-left:8px; margin-top: 10px;"><span>${response.msg}</span></div>`);
                $form.find('[data-datafield="BarCode"] input').select();
                FwBrowse.search($checkInExceptionGridControl);
            }
            response.ShowNewOrder ? $form.find('.addordertocontract').show() : $form.find('.addordertocontract').hide();

        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form) {
        let $buttonmenu = $form.find('.createcontract[data-type="btnmenu"]');
        let $createContract = FwMenu.generateButtonMenuOption('Create Contract')
            , $createPartialContract = FwMenu.generateButtonMenuOption('Create Partial Contract');

        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });

        $createPartialContract.on('click', e => {
            e.stopPropagation();
            //stuff
        });

        let menuOptions = [];
        menuOptions.push($createContract, $createPartialContract);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    //----------------------------------------------------------------------------------------------
}
var CheckInController = new CheckIn();