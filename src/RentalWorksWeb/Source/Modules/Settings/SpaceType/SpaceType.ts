declare var FwModule: any;
declare var FwBrowse: any;

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
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="ForReportsOnly"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NonBillable"]'))
            } else {
                FwFormField.disable($form.find('[data-datafield="NonBillable"]'))
            }
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

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="SpaceTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $spaceWarehouseRateGrid: any;
        var $spaceWarehouseRateGridControl: any;

        $spaceWarehouseRateGrid = $form.find('div[data-grid="SpaceWarehouseRateGrid"]');
        $spaceWarehouseRateGridControl = jQuery(jQuery('#tmpl-grids-SpaceWarehouseRateGridBrowse').html());
        $spaceWarehouseRateGrid.empty().append($spaceWarehouseRateGridControl);
        $spaceWarehouseRateGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                RateId: $form.find('div.fwformfield[data-datafield="RateId"] input').val()
            };
        });
        FwBrowse.init($spaceWarehouseRateGridControl);
        FwBrowse.renderRuntimeHtml($spaceWarehouseRateGridControl);
    }

    afterLoad($form: any) {
        var $spaceWarehouseRateGrid: any;

        $spaceWarehouseRateGrid = $form.find('[data-name="SpaceWarehouseRateGrid"]');
        FwBrowse.search($spaceWarehouseRateGrid);
    }

    loadRelatedValidationFields(validationname: any, $valuefield, $tr) {
        var $form

        $form = $valuefield.closest('.fwform');

        $form.find('.ratedescription .fwformfield-value').val($tr.find('.field[data-browsedatafield=Description]').attr('data-originalvalue'));
        $form.find('.rateunit .fwformfield-value').val($tr.find('.field[data-browsedatafield=Unit]').attr('data-originalvalue'));
    }

}

(<any>window).SpaceTypeController = new SpaceType();