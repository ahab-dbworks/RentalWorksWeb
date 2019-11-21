routes.push({ pattern: /^module\/subworksheet$/, action: function (match: RegExpExecArray) { return SubWorksheetController.getModuleScreen(); } });

class SubWorksheet {
    Module: string = 'SubWorksheet';
    caption: string = Constants.Modules.Home.children.SubWorksheet.caption;
	nav: string = Constants.Modules.Home.children.SubWorksheet.nav;
	id: string = Constants.Modules.Home.children.SubWorksheet.id;
    OrderId: string;
    SessionId: string;
    RecType: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = () =>  {
            FwModule.openModuleTab($form, 'Sub Worksheet', false, 'FORM', true);
            this.SessionId = '';
            this.OrderId = '';
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form, me = this, worksheetRequest, errorMsg, createNew, modifyExisting, newPo, existingPo;
        this.OrderId = parentmoduleinfo.OrderId;
        this.RecType = parentmoduleinfo.RecType;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        errorMsg = $form.find('.error-msg:not(.qty)');

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

        createNew.on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                modifyExisting.prop('checked', false);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.enable(jQuery(newPo[i]));
                }
                FwFormField.disable(existingPo);
            } else {
                modifyExisting.prop('checked', true);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.disable(jQuery(newPo[i]));
                }
                FwFormField.enable(existingPo);
            }
        });

        modifyExisting.on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                createNew.prop('checked', false);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.disable(jQuery(newPo[i]));
                }
                FwFormField.enable(existingPo);
            } else {
                createNew.prop('checked', true);
                for (let i = 0; i < newPo.length; i++) {
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

        $form.find('.openworksheet').on('click', e => {
            worksheetRequest = {
                OrderId: parentmoduleinfo.OrderId,
                RecType: parentmoduleinfo.RecType,
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
            if (FwFormField.getValueByDataField($form, 'RentalTo') === '') {
                worksheetRequest.ToDate = undefined;
            }

            if (FwFormField.getValueByDataField($form, 'ReqDate') !== '') {
                worksheetRequest.RequiredDate = FwFormField.getValueByDataField($form, 'ReqDate')
            }

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/order/startpoworksheetsession", worksheetRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        errorMsg.html('');
                        FwFormField.disable($form.find('.subworksheet'));
                        $form.find('.openworksheet').hide();
                        let gridUniqueIds: any = {
                            SessionId: response.SessionId
                        };
                        this.SessionId = response.SessionId;
                        this.renderPOGrid($form);

                        var $subPurchaseOrderItemGridControl: any;
                        $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                        $subPurchaseOrderItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = gridUniqueIds
                        })
                        FwBrowse.addEventHandler($subPurchaseOrderItemGridControl, 'afterdatabindcallback', () => {
                            this.getSubPOItemGridTotals($form);
                        });
                        FwBrowse.search($subPurchaseOrderItemGridControl);
                        if (createNew.prop('checked')) {
                            $form.find('.completeorder').show();
                        } else {
                            $form.find('.completeorder').text('Update Purchase Order');
                            $form.find('.completeorder').show();
                        }

                        FwFormField.getValueByDataField($form, 'RateId') === 'DAILY' ? $form.find('.daily').show() : $form.find('.daily').hide();
                    } else {
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }

            $form.attr('data-modified', false);
        })

        $form.find('.createpo').on('click', e => {
            try {
                var sessionRequest = {
                    SessionId: this.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/order/completepoworksheetsession", sessionRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        try {
                            let $purchaseOrderForm = PurchaseOrderController.loadForm({
                                PurchaseOrderId: response.PurchaseOrderId
                            });
                            FwModule.openSubModuleTab($form, $purchaseOrderForm);

                            let fields = $form.find('.fwformfield');
                            for (let i = 0; i < fields.length; i++) {
                                FwFormField.setValue2(jQuery(fields[i]), '', '');
                            }
                            FwFormField.enable($form.find('.subworksheet'));
                            $form.find('div[data-grid="SubPurchaseOrderItemGrid"]').empty();
                            $form.find('.completeorder').hide();
                            $form.find('.openworksheet').show();
                            $form.find('div[data-datafield="CreateNew"] input').prop('checked', true);
                            this.SessionId = '';
                            this.OrderId = '';
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
            $form.attr('data-modified', false);
        })

        $form.find('.selectall').on('click', e => {
            try {
                var sessionRequest = {
                    SessionId: this.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/subpurchaseorderitem/selectall", sessionRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        try {
                            var $subPurchaseOrderItemGridControl: any;
                            $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                            $subPurchaseOrderItemGridControl.data('ondatabind', request => {
                                request.uniqueids = {
                                    SessionId: this.SessionId
                                }
                            })
                            FwBrowse.search($subPurchaseOrderItemGridControl);
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
            $form.attr('data-modified', false);
        })

        $form.find('.selectnone').on('click', e => {
            try {
                var sessionRequest = {
                    SessionId: this.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/subpurchaseorderitem/selectnone", sessionRequest, FwServices.defaultTimeout, response =>  {
                    if (response.success) {
                        try {
                            var $subPurchaseOrderItemGridControl: any;
                            $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                            $subPurchaseOrderItemGridControl.data('ondatabind', request =>  {
                                request.uniqueids = {
                                    SessionId: this.SessionId
                                }
                            })
                            FwBrowse.search($subPurchaseOrderItemGridControl);
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
            $form.attr('data-modified', false);
        })

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getSubPOItemGridTotals($form: JQuery): void {
        FwAppData.apiMethod(true, 'GET', `api/v1/order/poworksheetsessiontotals/${this.SessionId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            $form.find(`div[data-totalfield="SubTotal"] input`).val(response.SubTotal);
            $form.find(`div[data-totalfield="Discount"] input`).val(response.Discount);
            $form.find(`div[data-totalfield="Tax"] input`).val(response.Tax);
            $form.find(`div[data-totalfield="GrossTotal"] input`).val(response.GrossTotal);
            $form.find(`div[data-totalfield="Total"] input`).val(response.Total);
        }, function onError(response) {
            FwFunc.showError(response);
            }, $form);
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateContact($browse, $grid, request) {
        const vendorId = jQuery($grid.find('[data-validationname="VendorValidation"] input')).val();
        request.uniqueIds = {
            CompanyId: vendorId
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateVendor($browse, $grid, request) {
        const vendorId = jQuery($grid.find('[data-validationname="VendorValidation"] input')).val();
        request.uniqueIds = {
        }
        switch (this.RecType) {
            case 'R':
                request.uniqueIds['SubRent'] = true;
                break;
            case 'S':
                request.uniqueIds['SubSale'] = true;
                break;
            case 'M':
                request.uniqueIds['SubMisc'] = true;
                break;
            case 'L':
                request.uniqueIds['SubLabor'] = true;
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    renderPOGrid($form: JQuery): void {
        var $subPurchaseOrderItemGrid;
        var $subPurchaseOrderItemGridControl;
        $subPurchaseOrderItemGrid = $form.find('div[data-grid="SubPurchaseOrderItemGrid"]');
        $subPurchaseOrderItemGridControl = jQuery(jQuery('#tmpl-grids-SubPurchaseOrderItemGridBrowse').html());
        $subPurchaseOrderItemGrid.empty().append($subPurchaseOrderItemGridControl);
        $subPurchaseOrderItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: this.OrderId
            };
        });
        $subPurchaseOrderItemGridControl.data('beforesave', request => {
            request.SessionId = this.SessionId;
            request.RecType = 'R';
        });
        FwBrowse.init($subPurchaseOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($subPurchaseOrderItemGridControl);
    }
    //----------------------------------------------------------------------------------------------
}
var SubWorksheetController = new SubWorksheet();