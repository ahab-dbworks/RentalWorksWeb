routes.push({ pattern: /^module\/subworksheet$/, action: function (match: RegExpExecArray) { return SubWorksheetController.getModuleScreen(); } });

class SubWorksheet {
    Module: string = 'SubWorksheet';
    OrderId: string;
    SessionId: string;
    RecType: string;
    caption: string = 'Sub Worksheet';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () =>  {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.SessionId = '';
            this.OrderId = '';
        };
        screen.unload = function () { };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.OrderId = parentmoduleinfo.OrderId;
        this.RecType = parentmoduleinfo.RecType;
        $form.find('div[data-datafield="CreateNew"] input').prop('checked', true);
        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        FwFormField.setValueByDataField($form, 'ReqDate', parentmoduleinfo.EstimatedStartDate);
        FwFormField.setValueByDataField($form, 'RentalFrom', parentmoduleinfo.EstimatedStartDate);
        FwFormField.setValueByDataField($form, 'RentalTo', parentmoduleinfo.EstimatedStopDate);
        FwFormField.setValueByDataField($form, 'ReqTime', parentmoduleinfo.EstimatedStartTime);

        this.events($form, parentmoduleinfo)
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery, parentmoduleinfo?): void {
        const createNew = $form.find('div[data-datafield="CreateNew"] input');
        const modifyExisting = $form.find('div[data-datafield="ModifyExisting"] input');
        const newPo = $form.find('.new');
        const existingPo = $form.find('.existing');
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
        // Modify Existing checkbox
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

        $form.find('.openworksheet').on('click', e => {
            const worksheetRequest = {
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
                AdjustContractDates: true,
                RequiredDate: ''
            }
            if (FwFormField.getValueByDataField($form, 'RentalTo') === '') {
                worksheetRequest.ToDate = undefined;
            }

            if (FwFormField.getValueByDataField($form, 'ReqDate') !== '') {
                worksheetRequest.RequiredDate = FwFormField.getValueByDataField($form, 'ReqDate')
            }

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/order/startcreatepoworksheetsession", worksheetRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        $form.find('.error-msg:not(.qty)').html('');
                        FwFormField.disable($form.find('.subworksheet'));
                        $form.find('.openworksheet').hide();
                        const gridUniqueIds: any = {
                            SessionId: response.SessionId
                        };
                        this.SessionId = response.SessionId;
                        this.renderPOGrid($form);

                        const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                        const totalFields: Array<string> = ["VendorWeeklyTax", "VendorWeeklyTotal", "VendorMonthlyTax", "VendorMonthlyTotal", "VendorPeriodTax", "VendorPeriodTotal"];

                        $subPurchaseOrderItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = gridUniqueIds
                            request.totalfields = totalFields;
                        })
                        FwBrowse.addEventHandler($subPurchaseOrderItemGridControl, 'afterdatabindcallback', ($subPurchaseOrderItemGridControl, response) => {
                            this.getSubPOItemGridTotals($form, response);
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
                        $form.find('.error-msg:not(.qty)').html(`<div style="margin-left:5px;"><span>${response.msg}</span></div>`);
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }

            $form.attr('data-modified', 'false');
        })

        $form.find('.createpo').on('click', e => {
            try {
                var sessionRequest = {
                    SessionId: this.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/order/completepoworksheetsession", sessionRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        try {
                            const $purchaseOrderForm = PurchaseOrderController.loadForm({
                                PurchaseOrderId: response.PurchaseOrderId
                            });
                            FwModule.openSubModuleTab($form, $purchaseOrderForm);

                            const fields = $form.find('.fwformfield');
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
            $form.attr('data-modified', 'false');
        })
        // Select All
        $form.find('.selectall').on('click', e => {
            try {
                const sessionRequest = {
                    SessionId: this.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/subpurchaseorderitem/selectall", sessionRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        try {
                            const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
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
            $form.attr('data-modified', 'false');
        })
        // Select None
        $form.find('.selectnone').on('click', e => {
            try {
                const sessionRequest = {
                    SessionId: this.SessionId
                }
                FwAppData.apiMethod(true, 'POST', "api/v1/subpurchaseorderitem/selectnone", sessionRequest, FwServices.defaultTimeout, response =>  {
                    if (response.success) {
                        try {
                            const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
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
            $form.attr('data-modified', 'false');
        })
        // Misc events
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

    }
    //----------------------------------------------------------------------------------------------
    getSubPOItemGridTotals($form: JQuery, response: any): void {
        FwFormField.setValue($form, 'div[data-totalfield="Total"]', response.Totals.Total);
        FwFormField.setValue($form, 'div[data-totalfield="Tax"]', response.Totals.Tax);
        FwFormField.setValue($form, 'div[data-totalfield="SubTotal"]', response.Totals.SubTotal);
        FwFormField.setValue($form, 'div[data-totalfield="GrossTotal"]', response.Totals.GrossTotal);
        FwFormField.setValue($form, 'div[data-totalfield="Discount"]', response.Totals.Discount);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateContact($browse, $grid, request) {
        const vendorId = jQuery($grid.find('[data-validationname="VendorValidation"] input')).val();
        request.uniqueIds = {
            CompanyId: vendorId
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateVendor($browse, $grid, request) {
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
        const $subPurchaseOrderItemGrid = $form.find('div[data-grid="SubPurchaseOrderItemGrid"]');
        const $subPurchaseOrderItemGridControl = FwBrowse.loadGridFromTemplate('SubPurchaseOrderItemGrid');
        const totalFields: Array<string> = ["VendorWeeklyTax", "VendorWeeklyTotal", "VendorMonthlyTax", "VendorMonthlyTotal", "VendorPeriodTax", "VendorPeriodTotal"];
        $subPurchaseOrderItemGrid.empty().append($subPurchaseOrderItemGridControl);
        $subPurchaseOrderItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: this.OrderId,
            };
            request.totalfields = totalFields;
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