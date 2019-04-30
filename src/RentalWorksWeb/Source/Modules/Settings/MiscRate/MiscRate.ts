class RwMiscRate {
    Module: string = 'MiscRate';
    apiurl: string = 'api/v1/miscrate';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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

        $form.find('div[data-datafield="CategoryId"]').data('onchange', function ($tr) {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            } else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '')
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
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const $rateLocationTaxGrid = $form.find('div[data-grid="RateLocationTaxGrid"]');
        const $rateLocationTaxGridControl = FwBrowse.loadGridFromTemplate('RateLocationTaxGrid');
        $rateLocationTaxGrid.empty().append($rateLocationTaxGridControl);
        $rateLocationTaxGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RateId: FwFormField.getValueByDataField($form, 'RateId')
            };
        })
        FwBrowse.init($rateLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($rateLocationTaxGridControl);

        const $rateWarehouseGrid = $form.find('div[data-grid="RateWarehouseGrid"]');
        const $rateWarehouseGridControl = FwBrowse.loadGridFromTemplate('RateWarehouseGrid');
        $rateWarehouseGrid.empty().append($rateWarehouseGridControl);
        $rateWarehouseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RateId: FwFormField.getValueByDataField($form, 'RateId')
            };
        })
        FwBrowse.init($rateWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rateWarehouseGridControl);

        const $singleRateWarehouseGrid = $form.find('div[data-grid="SingleRateWarehouseGrid"]');
        const $singleRateWarehouseGridControl = FwBrowse.loadGridFromTemplate('SingleRateWarehouseGrid');
        $singleRateWarehouseGrid.empty().append($singleRateWarehouseGridControl);
        $singleRateWarehouseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RateId: FwFormField.getValueByDataField($form, 'RateId')
            };
        })
        FwBrowse.init($singleRateWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($singleRateWarehouseGridControl);
    }

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $rateLocationTaxGrid = $form.find('[data-name="RateLocationTaxGrid"]');
        FwBrowse.search($rateLocationTaxGrid);

        const $rateWarehouseGrid = $form.find('[data-name="RateWarehouseGrid"]');
        FwBrowse.search($rateWarehouseGrid);

        const $singleRateWarehouseGrid = $form.find('[data-name="SingleRateWarehouseGrid"]');
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

        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        } else {
            FwFormField.disable($form.find('.subcategory'));
        }
    }

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
        var MiscTypeValue = jQuery($grid.find('[data-validationname="MiscTypeValidation"] input')).val();

        request.uniqueids = {
            MiscTypeId: MiscTypeValue
        };
    }
}

//----------------------------------------------------------------------------------------------
var MiscRateController = new RwMiscRate();