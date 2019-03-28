﻿routes.push({ pattern: /^module\/createpicklist$/, action: function (match: RegExpExecArray) { return CreatePickListController.getModuleScreen(); } });

class CreatePickList {
    Module: string = 'CreatePickList';
    //id: string = '153FC1F2-01B7-4CEC-B426-82146BB48F79';
    Type: string = 'Order';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT', null);

        screen.load = function () {
            FwModule.openModuleTab($form, 'Create Pick List', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo) {
        let $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        var $optionToggle = $form.find('.optiontoggle');
        $form.find('.options').toggle();
        $optionToggle.on('click', function () {
            $form.find('.options').toggle();
        });

        if (typeof parentmoduleinfo !== 'undefined') {
            if (parentmoduleinfo.IsTransfer) {
                this.Type = 'Transfer';
                $form.find('[data-datafield="OrderId"]').attr('data-datafield', 'TransferId');
            }
            FwFormField.setValueByDataField($form, `${this.Type}Id`, parentmoduleinfo.OrderId);
        }
        $form.find('.defaultoptions input').prop('checked', true);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const $pickListUtilityGrid = $form.find('div[data-grid="PickListUtilityGrid"]');
        const $pickListUtilityGridControl = FwBrowse.loadGridFromTemplate('PickListUtilityGrid');
        $pickListUtilityGrid.empty().append($pickListUtilityGridControl);
        $pickListUtilityGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                , SessionId: FwFormField.getValueByDataField($form, `${this.Type}Id`) //jason - placeholder until we can support multiple orders
            };
        });
        FwBrowse.init($pickListUtilityGridControl);
        FwBrowse.renderRuntimeHtml($pickListUtilityGridControl);

        $form.on('click', '.applyoptions', () => {
            const miscfields = CreatePickListController.getOptions($form);

            $pickListUtilityGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                    , SessionId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                };
                request.miscfields = miscfields;
            })
            FwBrowse.search($pickListUtilityGridControl);
        });

        $form.on('click', '.selectall', () => {
            const request: any = {};
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                , SessionId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/selectall', request, FwServices.defaultTimeout, response => {
                try {
                    FwBrowse.search($pickListUtilityGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });

        $form.on('click', '.selectnone', () => {
            const request: any = {};
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                , SessionId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/selectnone', request, FwServices.defaultTimeout, response => {
                try {
                    FwBrowse.search($pickListUtilityGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.on('click', '.createpicklist', () => {
            try {
                const miscfields = CreatePickListController.getOptions($form);
                const request: any = {};
                request.miscfields = miscfields;
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                    , SessionId: FwFormField.getValueByDataField($form, `${this.Type}Id`) //jason - placeholder until we can support multiple orders
                };

                const $tabpage = $form.parent();
                const $tab = jQuery(`#${$tabpage.attr('data-tabid')}`);

                FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/createpicklist', request, FwServices.defaultTimeout, response => {
                    try {
                        const $report = RwPickListReportController.openForm();
                        FwModule.openSubModuleTab($form, $report);
                        FwModule.closeFormTab($tab);
                        FwFormField.setValueByDataField($report, 'PickListId', response.PickListId, response.PickListNumber);
                        jQuery('.tab.submodule.active').find('.caption').html('Print Pick List');

                        //refresh pick list browse
                        const $pickListBrowse = jQuery('#PickListBrowse');
                        FwBrowse.search($pickListBrowse);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, $form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            };
        });
    };
    //----------------------------------------------------------------------------------------------
    getOptions($form) {
        var miscfields: any = {};
        var $options = $form.find('.option');
        var optionName, optionValue, optionType;
        $options.each(function () {
            optionName = jQuery(this).attr('data-datafield');
            optionType = jQuery(this).attr('data-type');

            if (optionType == "checkbox") {
                optionValue = jQuery(this).find('input:checked').val();
                if (optionValue == "on") {
                    optionValue = true;
                } else {
                    optionValue = false;
                }
            } else {
                optionValue = jQuery(this).find('input').val();
            }
            if (!(optionType == "date" && optionValue == "")) {
                miscfields[optionName] = optionValue;
            }
        })

        return miscfields;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $pickListUtilityGrid = $form.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    //----------------------------------------------------------------------------------------------
}
var CreatePickListController = new CreatePickList();