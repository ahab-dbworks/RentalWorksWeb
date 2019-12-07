class GeneratorType {
    Module: string = 'GeneratorType';
    apiurl: string = 'api/v1/generatortype';
    caption: string = Constants.Modules.Settings.children.GeneratorSettings.children.GeneratorType.caption;
    nav: string = Constants.Modules.Settings.children.GeneratorSettings.children.GeneratorType.nav;
    id: string = Constants.Modules.Settings.children.GeneratorSettings.children.GeneratorType.id;
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
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    disableFields(): void {
        jQuery('.disablefield').attr('data-required', 'false');
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        //const $generatorTypeWarehouseGrid = $form.find('div[data-grid="GeneratorTypeWarehouseGrid"]');
        //const $generatorTypeWarehouseControl = FwBrowse.loadGridFromTemplate('GeneratorTypeWarehouseGrid');
        //$generatorTypeWarehouseGrid.empty().append($generatorTypeWarehouseControl);
        //$generatorTypeWarehouseControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        GeneratorTypeId: FwFormField.getValueByDataField($form, 'GeneratorTypeId')
        //    }
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //});
        //FwBrowse.init($generatorTypeWarehouseControl);
        //FwBrowse.renderRuntimeHtml($generatorTypeWarehouseControl);

        FwBrowse.renderGrid({
            nameGrid: 'GeneratorTypeWarehouseGrid',
            gridSecurityId: 'N400cxkXaRDx',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    GeneratorTypeId: FwFormField.getValueByDataField($form, 'GeneratorTypeId'),
                };
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                }
            },
            //beforeSave: (request: any) => {
            //    request.GeneratorTypeId = FwFormField.getValueByDataField($form, 'GeneratorTypeId');
            //},
        });
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="GeneratorTypeId"] input').val(uniqueids.GeneratorTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $generatorTypeWarehouseGrid = $form.find('[data-name="GeneratorTypeWarehouseGrid"]');
        FwBrowse.search($generatorTypeWarehouseGrid);

        this.disableFields();
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('div[data-datafield="AssetAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AssetAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="IncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="IncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="SubIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="SubIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="EquipmentSaleIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="EquipmentSaleIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="LdIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="LdIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="CostOfGoodsSoldExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsSoldExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="CostOfGoodsRentedExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsRentedExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateInventoryType($browse, $grid, request) {
        request.uniqueids = {
            Vehicle: true
        };
    }
    //----------------------------------------------------------------------------------------------
}

var GeneratorTypeController = new GeneratorType();