﻿routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });

class CheckIn {
    Module: string = 'CheckIn';

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
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        //Default Department
        var department = JSON.parse(sessionStorage.getItem('department'));
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
                let contractId = FwFormField.getValueByDataField($form, 'ContractId');
                let request: any = {};
                request = {
                    Code: FwFormField.getValueByDataField($form, 'BarCode')
                }
                if (contractId) {
                    request.ContractId = contractId;
                }

                FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/checkinitem', request, FwServices.defaultTimeout, function onSuccess(response) {
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
                        FwFormField.setValueByDataField($form, 'OrderId', response.OrderId, response.OrderNumber);
                        FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                        FwFormField.setValueByDataField($form, 'Description', response.OrderDescription);
                        FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
                        let $checkedInItemsGridControl = $form.find('div[data-name="CheckedInItemGrid"]');
                        FwBrowse.search($checkedInItemsGridControl);
                        $form.find('[data-datafield="BarCode"] input').select();

                        if (response.status === 107) {
                            $form.find('[data-datafield="Quantity"] input').select();
                        }
                    }
                    else if (!response.success) {
                        let errormsg = $form.find('.errormsg');
                        errormsg.html(`<div style="margin-left:8px; margin-top: 10px;"><span>${response.msg}</span></div>`);
                        $form.find('[data-datafield="BarCode"] input').select();
                    }
                }, null, null);
            }
        });
        //Quantity input
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (e.which === 13) {
                $form.find('.errormsg').html('');
                let request: any = {};
                request = {
                    Code: FwFormField.getValueByDataField($form, 'BarCode')
                    , ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                    , Quantity: FwFormField.getValueByDataField($form, 'Quantity')
                }
                FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/checkinitem', request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success) {
                        let contractId = FwFormField.getValueByDataField($form, 'ContractId');
                        let $checkedInItemsGridControl = $form.find('div[data-name="CheckedInItemGrid"]');
                        FwBrowse.search($checkedInItemsGridControl);
                        FwFormField.setValueByDataField($form, 'Quantity', 0);
                        $form.find('[data-datafield="BarCode"] input').select();
                    }
                    else if (!response.success) {
                        let errormsg = $form.find('.errormsg');
                        errormsg.html(`<div style="margin-left:8px; margin-top: 10px;"><span>${response.msg}</span></div>`);
                        $form.find('[data-datafield="Quantity"] input').select();
                    }
                }, null, null);
            }
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
                }, null, null);
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse: any, $form: any, request: any) {
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            CheckIn: true
            , CheckInWarehouseId: warehouseId
        }
    };
}
var CheckInController = new CheckIn();