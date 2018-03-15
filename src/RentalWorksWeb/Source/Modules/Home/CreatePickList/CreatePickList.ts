routes.push({ pattern: /^module\/createpicklist$/, action: function (match: RegExpExecArray) { return CreatePickListController.getModuleScreen(); } });

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

        var selectedOptions = [];
        var $options = $form.find('.options input');
        $options.on('change', function (e) {
            var $optionItem = jQuery(jQuery(e.currentTarget).parents().eq(1)),
                optionType = $optionItem.attr('data-type'),
                optionName: any;

            switch (optionType) {
                case 'date':
                    optionName = jQuery(e.currentTarget).val().toString();
                    break;
                case 'validation':
                    optionName = jQuery(e.currentTarget).val().toString();
                    break;
                case 'checkbox':
                    optionName = $optionItem.attr('data-caption').toString();
                    break;
            }


            if (selectedOptions.indexOf(optionName) === -1) {
                selectedOptions.push(optionName);
            } else {
                selectedOptions = selectedOptions.filter((item) => item !== optionName);
            }

            console.log(selectedOptions);

        });


        $form.find('.applyoptions').on('click', function () {
            //send selectedOptions
            console.log("APPLY OPTIONS");
        });

        $form.find('.createpicklist').on('click', function () {
            console.log("CREATE PICK LIST");
            var $report;
            $report = RwPickListReportController.openForm();
            FwModule.openSubModuleTab($form, $report);
            $report.find('div.fwformfield[data-datafield="PickListId"] input').val("");//need to fill with values and close previous submodule
            $report.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val("");
            jQuery('.tab.submodule.active').find('.caption').html('Print Pick List');
        });



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
        $pickListUtilityGridControl.data('beforesave', function (request) {
            request.uniqueids = {
                OrderId:  FwFormField.getValueByDataField($form, 'OrderId')
                , SessionId: FwFormField.getValueByDataField($form, 'OrderId')
            }
        });
        FwBrowse.init($pickListUtilityGridControl);
        FwBrowse.renderRuntimeHtml($pickListUtilityGridControl);
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