class MiscRate {
    Module: string = 'MiscRate';
    apiurl: string = 'api/v1/miscrate';
    caption: string = Constants.Modules.Settings.children.MiscSettings.children.MiscRate.caption;
    nav: string = Constants.Modules.Settings.children.MiscSettings.children.MiscRate.nav;
    id: string = Constants.Modules.Settings.children.MiscSettings.children.MiscRate.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }

        let userassignedicodes = JSON.parse(sessionStorage.getItem('controldefaults')).userassignedicodes;
        if (userassignedicodes) {
            FwFormField.enable($form.find('[data-datafield="ICode"]'));
            $form.find('[data-datafield="ICode"]').attr(`data-required`, `true`);
        }
        else {
            FwFormField.disable($form.find('[data-datafield="ICode"]'));
            $form.find('[data-datafield="ICode"]').attr(`data-required`, `false`);
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
    renderGrids($form: any) {
        //const $rateLocationTaxGrid = $form.find('div[data-grid="RateLocationTaxGrid"]');
        //const $rateLocationTaxGridControl = FwBrowse.loadGridFromTemplate('RateLocationTaxGrid');
        //$rateLocationTaxGrid.empty().append($rateLocationTaxGridControl);
        //$rateLocationTaxGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'RateId')
        //    };
        //})
        //FwBrowse.init($rateLocationTaxGridControl);
        //FwBrowse.renderRuntimeHtml($rateLocationTaxGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'RateLocationTaxGrid',
            gridSecurityId: 'Bm6TN9A4IRIuT',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId'),
                };
            },
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId');
            //},
        });


        //const $rateWarehouseGrid = $form.find('div[data-grid="RateWarehouseGrid"]');
        //const $rateWarehouseGridControl = FwBrowse.loadGridFromTemplate('RateWarehouseGrid');
        //$rateWarehouseGrid.empty().append($rateWarehouseGridControl);
        //$rateWarehouseGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'RateId')
        //    };
        //})
        //FwBrowse.init($rateWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml($rateWarehouseGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'RateWarehouseGrid',
            gridSecurityId: 'oVjmeqXtHEJCm',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId'),
                };
            },
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId);
            //},
        });

        //const $singleRateWarehouseGrid = $form.find('div[data-grid="SingleRateWarehouseGrid"]');
        //const $singleRateWarehouseGridControl = FwBrowse.loadGridFromTemplate('SingleRateWarehouseGrid');
        //$singleRateWarehouseGrid.empty().append($singleRateWarehouseGridControl);
        //$singleRateWarehouseGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'RateId')
        //    };
        //})
        //FwBrowse.init($singleRateWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml($singleRateWarehouseGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'SingleRateWarehouseGrid',
            gridSecurityId: 'vHMa0l5PUysXo',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId'),
                };
            },
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId');
            //},
        });
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
            FwFormField.enable($form.find('[data-datafield="SubCategoryId"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="SubCategoryId"]'));
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
        })
        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('div[data-datafield="SubCategoryId"]'));
                $form.find('[data-datafield="SubCategoryId"]').attr(`data-required`, `true`);
            } else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '')
                $form.find('[data-datafield="SubCategoryId"]').attr(`data-required`, `false`);
                FwFormField.disable($form.find('div[data-datafield="SubCategoryId"]'));
            }
        })
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateType = function ($browse, $grid, request) {
        request.uniqueids = {
            HasCategories: true,
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateCategory = function ($browse, $grid, request) {
        const $form = $grid.closest('.fwform');
        const miscTypeId = FwFormField.getValueByDataField($form, 'MiscTypeId');

        request.uniqueids = {
            MiscTypeId: miscTypeId,
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateSubCategory = function ($browse, $grid, request) {
        const $form = $grid.closest('.fwform');
        const miscTypeId = FwFormField.getValueByDataField($form, 'MiscTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');

        request.uniqueids = {
            MiscTypeId: miscTypeId,
            CategoryId: categoryId,
        };
    }
    //----------------------------------------------------------------------------------------------
}

//----------------------------------------------------------------------------------------------
var MiscRateController = new MiscRate();