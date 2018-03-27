﻿routes.push({ pattern: /^module\/createpicklist$/, action: function (match: RegExpExecArray) { return CreatePickListController.getModuleScreen(); } });

class CreatePickList {
    Module: string;

    constructor() {
        this.Module = 'CreatePickList';
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-CreatePickListForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');


        var $optionToggle = $form.find('.optiontoggle');
        $form.find('.options').toggle();
        $optionToggle.on('click', function () {
            $form.find('.options').toggle();
        });

        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="OrderId"] input').val(parentmoduleinfo.OrderId);
        }

        $form.find('.createpicklist').on('click', function () {
            var $report;
            $report = RwPickListReportController.openForm();
            FwModule.openSubModuleTab($form, $report);
            var sessionId = FwFormField.getValueByDataField($form, "OrderId");
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/createpicklist/' + sessionId, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    $report.find('div.fwformfield[data-datafield="PickListId"] input').val(response.PickListId);
                    $report.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(response.PickListId);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);

            jQuery('.tab.submodule.active').find('.caption').html('Print Pick List');
        });

        $form.find('.defaultoptions input').prop('checked', true);


        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $pickListUtilityGrid;
        var $pickListUtilityGridControl;
        $pickListUtilityGrid = $form.find('div[data-grid="PickListUtilityGrid"]');
        $pickListUtilityGridControl = jQuery(jQuery('#tmpl-grids-PickListUtilityGridBrowse').html());
        $pickListUtilityGrid.empty().append($pickListUtilityGridControl);
        $pickListUtilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                , SessionId: FwFormField.getValueByDataField($form, 'OrderId') //jason - placeholder until we can support multiple orders
            };
        });
        FwBrowse.init($pickListUtilityGridControl);
        FwBrowse.renderRuntimeHtml($pickListUtilityGridControl);

        var $options = $form.find('.option');
        $form.find('.applyoptions').on('click', function () {
            var miscfields: any = {};
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

            $pickListUtilityGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                    , SessionId: FwFormField.getValueByDataField($form, 'OrderId')
                };
                request.miscfields = miscfields;
            })
            FwBrowse.search($pickListUtilityGridControl);

        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $pickListUtilityGrid;
        $pickListUtilityGrid = $form.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    //----------------------------------------------------------------------------------------------
}
var CreatePickListController = new CreatePickList();