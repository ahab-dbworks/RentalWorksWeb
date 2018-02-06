declare var FwModule: any;
declare var FwBrowse: any;

class RwMiscRate {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'MiscRate';
        this.apiurl = 'api/v1/miscrate';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Misc Rate', false, 'BROWSE', true);
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

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true')
        }

        $form = FwModule.openForm($form, mode);

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
        $form.find('div.fwformfield[data-datafield="RateId"] input').val(uniqueids.RateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="RateId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $rateLocationTaxGrid: any;
        var $rateLocationTaxGridControl: any;
        var $rateWarehouseGrid: any;
        var $rateWarehouseGridControl: any;

        $rateLocationTaxGrid = $form.find('div[data-grid="RateLocationTaxGrid"]');
        $rateLocationTaxGridControl = jQuery(jQuery('#tmpl-grids-RateLocationTaxGridBrowse').html());
        $rateLocationTaxGrid.empty().append($rateLocationTaxGridControl);
        $rateLocationTaxGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                RateId: $form.find('div.fwformfield[data-datafield="RateId"] input').val()
            };
        })
        FwBrowse.init($rateLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($rateLocationTaxGridControl);

        $rateWarehouseGrid = $form.find('div[data-grid="RateWarehouseGrid"]');
        $rateWarehouseGridControl = jQuery(jQuery('#tmpl-grids-RateWarehouseGridBrowse').html());
        $rateWarehouseGrid.empty().append($rateWarehouseGridControl);
        $rateWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                RateId: $form.find('div.fwformfield[data-datafield="RateId"] input').val()
            };
        })
        FwBrowse.init($rateWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rateWarehouseGridControl);
    }

    afterLoad($form: any) {
        var $rateLocationTaxGrid: any;
        var $rateWarehouseGrid: any;

        $rateLocationTaxGrid = $form.find('[data-name="RateLocationTaxGrid"]');
        FwBrowse.search($rateLocationTaxGrid);

        $rateWarehouseGrid = $form.find('[data-name="RateWarehouseGrid"]');
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

(<any>window).MiscRateController = new RwMiscRate();