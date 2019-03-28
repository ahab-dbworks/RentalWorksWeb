routes.push({ pattern: /^module\/createpicklist$/, action: function (match: RegExpExecArray) { return CreatePickListController.getModuleScreen(); } });

class CreatePickList {
    Module: string = 'CreatePickList';
    //id: string = '153FC1F2-01B7-4CEC-B426-82146BB48F79';

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
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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
            var $report, request: any = {};

            try {
                var miscfields = CreatePickListController.getOptions($form);
                request.miscfields = miscfields;
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                    , SessionId: FwFormField.getValueByDataField($form, 'OrderId') //jason - placeholder until we can support multiple orders
                };

                var $tabpage = $form.parent();
                var $tab = jQuery('#' + $tabpage.attr('data-tabid'));

                FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/createpicklist', request, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        //if (response.PickListNumber == "") {
                        //    throw "No items have been added to this Pick List";
                        //}
                        $report = RwPickListReportController.openForm();
                        FwModule.openSubModuleTab($form, $report);
                        FwModule.closeFormTab($tab);
                        $report.find('div.fwformfield[data-datafield="PickListId"] input').val(response.PickListId);
                        $report.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(response.PickListNumber);
                        jQuery('.tab.submodule.active').find('.caption').html('Print Pick List');

                        //refresh pick list browse
                        var $pickListBrowse = jQuery('#PickListBrowse');
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

        $form.find('.applyoptions').on('click', function () {
            var miscfields = CreatePickListController.getOptions($form);

            $pickListUtilityGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                    , SessionId: FwFormField.getValueByDataField($form, 'OrderId')
                };
                request.miscfields = miscfields;
            })
            FwBrowse.search($pickListUtilityGridControl);
        });

        $form.find('.selectall').on('click', function () {
            var request: any = {};
            //var miscfields = CreatePickListController.getOptions($form);
            //request.miscfields = miscfields;
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                , SessionId: FwFormField.getValueByDataField($form, 'OrderId')
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/selectall', request, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwBrowse.search($pickListUtilityGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });

        $form.find('.selectnone').on('click', function () {
            var request: any = {};
            //var miscfields = CreatePickListController.getOptions($form);
            //request.miscfields = miscfields;
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                , SessionId: FwFormField.getValueByDataField($form, 'OrderId')
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/selectnone', request, FwServices.defaultTimeout, function onSuccess(response) {
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
        var $pickListUtilityGrid;
        $pickListUtilityGrid = $form.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    //----------------------------------------------------------------------------------------------
}
var CreatePickListController = new CreatePickList();