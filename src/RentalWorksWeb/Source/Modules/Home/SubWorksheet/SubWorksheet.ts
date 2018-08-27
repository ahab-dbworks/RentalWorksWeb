routes.push({ pattern: /^module\/subworksheet$/, action: function (match: RegExpExecArray) { return SubWorksheetController.getModuleScreen(); } });

class SubWorksheet {
    Module: string = 'SubWorksheet';
    OrderId: string;
    SessionId: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        var me = this;
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Sub Worksheet', false, 'FORM', true);
            me.SessionId = '';
            me.OrderId = '';
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form, me = this, worksheetRequest, createNew, modifyExisting, newPo, existingPo;
        this.OrderId = parentmoduleinfo.OrderId;

        $form = jQuery(jQuery('#tmpl-modules-SubWorksheetForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');
        
        createNew = $form.find('div[data-datafield="CreateNew"] input');
        modifyExisting = $form.find('div[data-datafield="ModifyExisting"] input');
        newPo = $form.find('.new');
        existingPo = $form.find('.existing');
        createNew.prop('checked', true);

        FwFormField.setValueByDataField($form, 'ReqDate', parentmoduleinfo.EstimatedStartDate);
        FwFormField.setValueByDataField($form, 'RentalFrom', parentmoduleinfo.EstimatedStartDate);
        FwFormField.setValueByDataField($form, 'RentalTo', parentmoduleinfo.EstimatedStopDate);
        FwFormField.setValueByDataField($form, 'ReqTime', parentmoduleinfo.EstimatedStartTime);

        createNew.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                modifyExisting.prop('checked', false);
                for (var i = 0; i < newPo.length; i++) {
                    FwFormField.enable(jQuery(newPo[i]));
                }
                FwFormField.disable(existingPo);
            } else {
                modifyExisting.prop('checked', true);
                for (var i = 0; i < newPo.length; i++) {
                    FwFormField.disable(jQuery(newPo[i]));
                }
                FwFormField.enable(existingPo);
            }
        });

        modifyExisting.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                createNew.prop('checked', false);
                for (var i = 0; i < newPo.length; i++) {
                    FwFormField.disable(jQuery(newPo[i]));
                }
                FwFormField.enable(existingPo);
            } else {
                createNew.prop('checked', true);
                for (var i = 0; i < newPo.length; i++) {
                    FwFormField.enable(jQuery(newPo[i]));
                }
                FwFormField.disable(existingPo);
            }
        });
        
        $form.find('div[data-datafield="VendorId"]').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'RateId', $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'CurrencyId', $tr.find('.field[data-browsedatafield="DefaultCurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="DefaultCurrencyCode"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'BillingCycleId', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'ContactId', $tr.find('.field[data-browsedatafield="PrimaryContactId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PrimaryContact"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'OfficePhone', $tr.find('.field[data-browsedatafield="PrimaryContactPhone"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'OfficeExtension', $tr.find('.field[data-browsedatafield="PrimaryContactExtension"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="ContactId"]').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'OfficePhone', $tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'OfficeExtension', $tr.find('.field[data-browsedatafield="OfficeExtension"]').attr('data-originalvalue'));
        });

        $form.find('.openworksheet').on('click', function (e) {

            worksheetRequest = {
                OrderId: parentmoduleinfo.OrderId,
                RecType: 'R',
                VendorId: FwFormField.getValueByDataField($form, 'VendorId'),
                ContactId: FwFormField.getValueByDataField($form, 'ContactId'),
                RateType: FwFormField.getValueByDataField($form, 'RateId'),
                BillingCycleId: FwFormField.getValueByDataField($form, 'BillingCycleId'),
                RequiredTime: FwFormField.getValueByDataField($form, 'ReqTime'),
                FromDate: FwFormField.getValueByDataField($form, 'RentalFrom'),
                ToDate: FwFormField.getValueByDataField($form, 'RentalTo'),
                DeliveryId: '',
                AdjustContractDates: true
            }
            if (FwFormField.getValueByDataField($form, 'ReqDate') !== '') {
                worksheetRequest.RequiredDate = FwFormField.getValueByDataField($form, 'ReqDate')
            }

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/order/startpoworksheetsession", worksheetRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success) {
                        $form.find('div.errormsg').html('');
                        FwFormField.disable($form.find('.subworksheet'));
                        $form.find('.openworksheet').hide();
                        let gridUniqueIds: any = {
                            SessionId: response.SessionId
                        };
                        me.SessionId = response.SessionId;
                        me.renderPOGrid($form);

                        var $subPurchaseOrderItemGridControl: any;
                        $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                        $subPurchaseOrderItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = gridUniqueIds
                        })
                        FwBrowse.search($subPurchaseOrderItemGridControl);
                        if (createNew.prop('checked')) {
                            $form.find('.completeorder').show();
                        } else {
                            $form.find('.completeorder').text('Update Purchase Order');
                            $form.find('.completeorder').show();
                        }

                        FwFormField.getValueByDataField($form, 'RateId') === 'DAILY' ? $form.find('.daily').show() : $form.find('.daily').hide();
                    } else {
                        $form.find('div.errormsg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }

            $form.attr('data-modified', false);
        })

        $form.find('.createpo').on('click', function (e) {
            try {
                var sessionRequest = {
                    SessionId: me.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/order/completepoworksheetsession", sessionRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success) {
                        try {
                            let $purchaseOrderForm = PurchaseOrderController.loadForm({
                                PurchaseOrderId: response.PurchaseOrderId
                            });
                            FwModule.openSubModuleTab($form, $purchaseOrderForm);

                            let fields = $form.find('.fwformfield');
                            for (var i = 0; i < fields.length; i++) {
                                FwFormField.setValue2(jQuery(fields[i]), '', '');
                            }
                            FwFormField.enable($form.find('.subworksheet'));
                            $form.find('div[data-grid="SubPurchaseOrderItemGrid"]').empty();
                            $form.find('.completeorder').hide();
                            $form.find('div[data-datafield="CreateNew"] input').prop('checked', true);
                            me.SessionId = '';
                            me.OrderId = '';
                            FwFormField.setValueByDataField($form, 'ReqDate', parentmoduleinfo.EstimatedStartDate);
                            FwFormField.setValueByDataField($form, 'RentalFrom', parentmoduleinfo.EstimatedStartDate);
                            FwFormField.setValueByDataField($form, 'RentalTo', parentmoduleinfo.EstimatedStopDate);
                            FwFormField.setValueByDataField($form, 'ReqTime', parentmoduleinfo.EstimatedStartTime);
                            FwFormField.disable($form.find('div[data-datafield="OfficePhone"]'));
                            FwFormField.disable($form.find('div[data-datafield="OfficeExtension"]'));
                            FwFormField.disable($form.find('div[data-datafield="POId"]'));
                            FwFormField.disable($form.find('.totals'));
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    } else {

                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        
        return $form;
    }
    //---------------------------------------------------------------------------------------------- 
    beforeValidateContact($browse, $grid, request) {
        const vendorId = jQuery($grid.find('[data-validationname="VendorValidation"] input')).val();
        request.uniqueIds = {
            CompanyId: vendorId
        }
    }
    //----------------------------------------------------------------------------------------------
    renderPOGrid($form) {
        var $subPurchaseOrderItemGrid;
        var $subPurchaseOrderItemGridControl;
        var self = this;
        $subPurchaseOrderItemGrid = $form.find('div[data-grid="SubPurchaseOrderItemGrid"]');
        $subPurchaseOrderItemGridControl = jQuery(jQuery('#tmpl-grids-SubPurchaseOrderItemGridBrowse').html());
        $subPurchaseOrderItemGrid.empty().append($subPurchaseOrderItemGridControl);
        $subPurchaseOrderItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: self.OrderId
            };
        });
        $subPurchaseOrderItemGridControl.data('beforesave', request => {
            request.SessionId = self.SessionId;
            request.RecType = 'R';
        });
        FwBrowse.init($subPurchaseOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($subPurchaseOrderItemGridControl);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
    //----------------------------------------------------------------------------------------------

}
var SubWorksheetController = new SubWorksheet();