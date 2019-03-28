class RwLaborRate {
    Module: string = 'LaborRate';
    apiurl: string = 'api/v1/laborrate';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Labor Rate', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    //----------------------------------------------------------------------------------------------
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

        this.events($form);

        return $form;
    }

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="RateId"] input').val(uniqueids.RateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="RateId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $rateLocationTaxGrid: any;
        var $rateLocationTaxGridControl: any;
        var $rateWarehouseGrid: any;
        var $rateWarehouseGridControl: any;
        var $singleRateWarehouseGrid: any;
        var $singleRateWarehouseGridControl: any;

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

        $singleRateWarehouseGrid = $form.find('div[data-grid="SingleRateWarehouseGrid"]');
        $singleRateWarehouseGridControl = jQuery(jQuery('#tmpl-grids-SingleRateWarehouseGridBrowse').html());
        $singleRateWarehouseGrid.empty().append($singleRateWarehouseGridControl);
        $singleRateWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                RateId: $form.find('div.fwformfield[data-datafield="RateId"] input').val()
            };
        })
        FwBrowse.init($singleRateWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($singleRateWarehouseGridControl);
    }

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $rateLocationTaxGrid: any;
        var $rateWarehouseGrid: any;
        var $singleRateWarehouseGrid: any;

        $rateLocationTaxGrid = $form.find('[data-name="RateLocationTaxGrid"]');
        FwBrowse.search($rateLocationTaxGrid);

        $rateWarehouseGrid = $form.find('[data-name="RateWarehouseGrid"]');
        FwBrowse.search($rateWarehouseGrid);

        $singleRateWarehouseGrid = $form.find('[data-name="SingleRateWarehouseGrid"]');
        FwBrowse.search($singleRateWarehouseGrid);

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.category [data-type="validation"]'))
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategory"]'))
        } else {
            FwFormField.disable($form.find('.category [data-type="validation"]'))
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategory"]'))
        }
        // Display Single or Recurring Rates Tab
        if (FwFormField.getValueByDataField($form, 'RateType') === 'SINGLE') {
            $form.find('.single_rates').show();
            $form.find('.recurring_rates').hide();
        }
        else {
            $form.find('.single_rates').hide();
            $form.find('.recurring_rates').show();
        }
    };

    //----------------------------------------------------------------------------------------------
    events($form: any) {
        // Display Single or Recurring Rates Tab change event
        $form.find('.rate_type_radio').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'SINGLE') {
                $form.find('.single_rates').show();
                $form.find('.recurring_rates').hide();
            }
            else {
                $form.find('.single_rates').hide();
                $form.find('.recurring_rates').show();
            }
        });
    };

    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $grid, request) {
        var LaborTypeValue = jQuery($grid.find('[data-validationname="LaborTypeValidation"] input')).val();

        request.uniqueids = {
            LaborTypeId: LaborTypeValue
        };
    }
}

//----------------------------------------------------------------------------------------------
var LaborRateController = new RwLaborRate();