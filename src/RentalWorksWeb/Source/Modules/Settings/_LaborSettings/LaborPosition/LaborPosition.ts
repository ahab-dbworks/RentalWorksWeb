class RwLaborPosition {
    Module:  string = 'LaborPosition';
    apiurl:  string = 'api/v1/position';
    caption: string = Constants.Modules.Settings.children.LaborSettings.children.LaborPosition.caption;
    nav:     string = Constants.Modules.Settings.children.LaborSettings.children.LaborPosition.nav;
    id:      string = Constants.Modules.Settings.children.LaborSettings.children.LaborPosition.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = function () {
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

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PositionId"] input').val(uniqueids.PositionId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        //const $rateLocationTaxGrid = $form.find('div[data-grid="RateLocationTaxGrid"]');
        //const $rateLocationTaxGridControl = FwBrowse.loadGridFromTemplate('RateLocationTaxGrid');
        //$rateLocationTaxGrid.empty().append($rateLocationTaxGridControl);
        //$rateLocationTaxGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'PositionId')
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
                    RateId: FwFormField.getValueByDataField($form, 'PositionId'),
                };
            }
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'PositionId');
            //},
        });

        //const $rateWarehouseGrid = $form.find('div[data-grid="RateWarehouseGrid"]');
        //const $rateWarehouseGridControl = FwBrowse.loadGridFromTemplate('RateWarehouseGrid');
        //$rateWarehouseGrid.empty().append($rateWarehouseGridControl);
        //$rateWarehouseGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'PositionId')
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
                    RateId: FwFormField.getValueByDataField($form, 'PositionId'),
                };
            }
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'PositionId');
            //},
        });
    }
    //----------------------------------------------------------------------------------------------
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

        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        } else {
            FwFormField.disable($form.find('.subcategory'));
        }


    }


    beforeValidateType = function ($browse, $grid, request) {
        request.uniqueids = {
            HasCategories: true,
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateCategory = function ($browse, $grid, request) {
        const $form = $grid.closest('.fwform');
        const laborTypeId = FwFormField.getValueByDataField($form, 'LaborTypeId');

        request.uniqueids = {
            LaborTypeId: laborTypeId,
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateSubCategory = function ($browse, $grid, request) {
        const $form = $grid.closest('.fwform');
        const laborTypeId = FwFormField.getValueByDataField($form, 'LaborTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');

        request.uniqueids = {
            LaborTypeId: laborTypeId,
            CategoryId: categoryId,
        };
    }
    //----------------------------------------------------------------------------------------------

}

var LaborPositionController = new RwLaborPosition();