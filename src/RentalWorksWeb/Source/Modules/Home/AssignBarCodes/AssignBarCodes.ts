routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarCodesController.getModuleScreen(); } });

class AssignBarCodes {
    Module: string = 'AssignBarCodes';
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
            FwModule.openModuleTab($form, 'Assign Bar Codes', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-AssignBarCodesForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

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
}
var AssignBarCodesController = new AssignBarCodes();