class SpaceType {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SpaceType';
        this.apiurl = 'api/v1/spacetype';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Space Type', false, 'BROWSE', true);
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

        $form.find('[data-datafield="ForReportsOnly"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NonBillable"]'))
            } else {
                FwFormField.disable($form.find('[data-datafield="NonBillable"]'))
            }
        });

        $form.find('div[data-datafield="RateId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="RateDescription"]', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="RateUnit"]', $tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="SpaceTypeId"] input').val(uniqueids.SpaceTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="SpaceTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        const $spaceWarehouseRateGrid = $form.find('div[data-grid="SpaceWarehouseRateGrid"]');
        const $spaceWarehouseRateGridControl = FwBrowse.loadGridFromTemplate('SpaceWarehouseRateGrid');
        $spaceWarehouseRateGrid.empty().append($spaceWarehouseRateGridControl);
        $spaceWarehouseRateGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RateId: FwFormField.getValueByDataField($form, 'RateId')
            };
        });
        FwBrowse.init($spaceWarehouseRateGridControl);
        FwBrowse.renderRuntimeHtml($spaceWarehouseRateGridControl);
    }

    afterLoad($form: any) {
        const $spaceWarehouseRateGrid = $form.find('[data-name="SpaceWarehouseRateGrid"]');
        FwBrowse.search($spaceWarehouseRateGrid);
    }
}

var SpaceTypeController = new SpaceType();