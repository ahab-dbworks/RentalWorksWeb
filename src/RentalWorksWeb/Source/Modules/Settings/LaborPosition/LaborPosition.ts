class RwLaborPosition {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'LaborPosition';
        this.apiurl = 'api/v1/position';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Position', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true')
        }

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.category [data-type="validation"]'))
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategory"]'))
            }
            else {
                FwFormField.disable($form.find('.category [data-type="validation"]'))
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategory"]'))
            }
        });

        $form.find('[data-datafield="ProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('[data-datafield="OverrideProfitAndLossCategory"]'))
            }
            else {
                FwFormField.enable($form.find('[data-datafield="OverrideProfitAndLossCategory"]'))
            }
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PositionId"] input').val(uniqueids.PositionId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    beforeValidate = function ($browse, $grid, request) {
        var LaborTypeValue = jQuery($grid.find('[data-validationname="LaborTypeValidation"] input')).val();

        request.uniqueids = {
            LaborTypeId: LaborTypeValue
        };
    }

    renderGrids($form: any) {
        const $rateLocationTaxGrid = $form.find('div[data-grid="RateLocationTaxGrid"]');
        const $rateLocationTaxGridControl = FwBrowse.loadGridFromTemplate('RateLocationTaxGrid');
        $rateLocationTaxGrid.empty().append($rateLocationTaxGridControl);
        $rateLocationTaxGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RateId: FwFormField.getValueByDataField($form, 'PositionId')
            };
        })
        FwBrowse.init($rateLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($rateLocationTaxGridControl);

        const $rateWarehouseGrid = $form.find('div[data-grid="RateWarehouseGrid"]');
        const $rateWarehouseGridControl = FwBrowse.loadGridFromTemplate('RateWarehouseGrid');
        $rateWarehouseGrid.empty().append($rateWarehouseGridControl);
        $rateWarehouseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RateId: FwFormField.getValueByDataField($form, 'PositionId')
            };
        })
        FwBrowse.init($rateWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rateWarehouseGridControl);
    }

    afterLoad($form: any) {
        const $rateLocationTaxGrid = $form.find('[data-name="RateLocationTaxGrid"]');
        FwBrowse.search($rateLocationTaxGrid);

        const $rateWarehouseGrid = $form.find('[data-name="RateWarehouseGrid"]');
        FwBrowse.search($rateWarehouseGrid);

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.category [data-type="validation"]'))
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategory"]'))
        } else {
            FwFormField.disable($form.find('.category [data-type="validation"]'))
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategory"]'))
        }
    }
}

var LaborPositionController = new RwLaborPosition();