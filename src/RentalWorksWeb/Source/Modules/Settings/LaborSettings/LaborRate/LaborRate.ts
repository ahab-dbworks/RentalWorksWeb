class RwLaborRate {
    Module: string = 'LaborRate';
    apiurl: string = 'api/v1/laborrate';
    caption: string = Constants.Modules.Settings.children.LaborSettings.children.LaborRate.caption;
    nav: string = Constants.Modules.Settings.children.LaborSettings.children.LaborRate.nav;
    id: string = Constants.Modules.Settings.children.LaborSettings.children.LaborRate.id;
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

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.category [data-type="validation"]'))
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategory"]'))
            } else {
                FwFormField.disable($form.find('.category [data-type="validation"]'))
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategory"]'))
            }
        });

        $form.find('[data-datafield="ProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('[data-datafield="OverrideProfitAndLossCategory"]'))
            } else {
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
        let $form = this.openForm('EDIT');
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
            }
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId');
            //}
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
            }
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId');
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
            }
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
        } else {
            $form.find('.single_rates').hide();
            $form.find('.recurring_rates').show();
        }

        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        } else {
            FwFormField.disable($form.find('.subcategory'));
        }

    };

    //----------------------------------------------------------------------------------------------
    events($form: any) {
        // Display Single or Recurring Rates Tab change event
        $form.find('.rate_type_radio').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'SINGLE') {
                $form.find('.single_rates').show();
                $form.find('.recurring_rates').hide();
            } else {
                $form.find('.single_rates').hide();
                $form.find('.recurring_rates').show();
            }
        });
    };

    //----------------------------------------------------------------------------------------------
    //beforeValidateType = function ($browse, $grid, request) {
    //    request.uniqueids = {
    //        HasCategories: true,
    //    };
    //}
    //----------------------------------------------------------------------------------------------
    //beforeValidateCategory = function ($browse, $grid, request) {
    //    const $form = $grid.closest('.fwform');
    //    const laborTypeId = FwFormField.getValueByDataField($form, 'LaborTypeId');

    //    request.uniqueids = {
    //        LaborTypeId: laborTypeId,
    //    };
    //}
    //----------------------------------------------------------------------------------------------
    //beforeValidateSubCategory = function ($browse, $grid, request) {
    //    const $form = $grid.closest('.fwform');
    //    const laborTypeId = FwFormField.getValueByDataField($form, 'LaborTypeId');
    //    const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');

    //    request.uniqueids = {
    //        LaborTypeId: laborTypeId,
    //        CategoryId: categoryId,
    //    };
    //}
    //----------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const laborTypeId = FwFormField.getValueByDataField($form, 'LaborTypeId');
        switch (datafield) {
            case 'LaborTypeId':
                request.uniqueids = {
                    HasCategories: true,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelabortype`);
                break;
            case 'LaborCategoryId':
                request.uniqueids = {
                    LaborTypeId: laborTypeId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelaborcategory`);
                break;
            case 'SubCategoryId':
                const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
                request.uniqueids = {
                    LaborTypeId: laborTypeId,
                    CategoryId: categoryId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
            case 'UnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunit`);
                break;
        }
    }
}

//----------------------------------------------------------------------------------------------
var LaborRateController = new RwLaborRate();