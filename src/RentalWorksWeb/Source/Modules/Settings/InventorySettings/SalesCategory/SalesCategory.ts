class SalesCategory {
    Module: string = 'SalesCategory';
    apiurl: string = 'api/v1/salescategory';
    caption: string = Constants.Modules.Settings.children.InventorySettings.children.SalesCategory.caption;
    nav: string = Constants.Modules.Settings.children.InventorySettings.children.SalesCategory.nav;
    id: string = Constants.Modules.Settings.children.InventorySettings.children.SalesCategory.id;
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
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasMultiRowEditing = true;
        FwMenu.addBrowseMenuButtons(options);
    }
    //-----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasMultiEdit = true;
        FwMenu.addFormMenuButtons(options);
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

        FwFormField.loadItems($form.find('div[data-datafield="BarCodeType"]'), [
            { value: '1', caption: 'Small', checked: true },
            { value: '2', caption: 'Large' }
        ]);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CategoryId"] input').val(uniqueids.CategoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.on('change', '[data-datafield="OverrideProfitAndLossCategory"]', (e) => {
            if (FwFormField.getValueByDataField($form, 'OverrideProfitAndLossCategory')) {
                FwFormField.enable($form.find('.catvalidation'))
            } else {
                FwFormField.disable($form.find('.catvalidation'))
            }
        });

        $form.find('[data-datafield="CatalogCategory"]').on('change', function () {
            const isChecked = FwFormField.getValueByDataField($form, 'CatalogCategory');
            if (isChecked) {
                FwFormField.enable($form.find('.designer'))
                FwFormField.disable($form.find('.barcodetype'))
            } else {
                FwFormField.disable($form.find('.designer'))
                FwFormField.enable($form.find('.barcodetype'))
            }
        });

        $form.find('div[data-datafield="IncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="IncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="SubIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="SubIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="AssetAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AssetAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="CostOfGoodsSoldExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsSoldExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // Sub Category Grid
        //const $subCategoryGrid = $form.find('div[data-grid="SubCategoryGrid"]');
        //const $subCategoryControl = FwBrowse.loadGridFromTemplate('SubCategoryGrid');
        //$subCategoryGrid.empty().append($subCategoryControl);
        //$subCategoryControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        CategoryId: FwFormField.getValueByDataField($form, 'CategoryId')
        //    }
        //});
        //$subCategoryControl.data('beforesave', request => {
        //    request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        //})
        //FwBrowse.init($subCategoryControl);
        //FwBrowse.renderRuntimeHtml($subCategoryControl);


        FwBrowse.renderGrid({
            nameGrid: 'SubCategoryGrid',
            gridSecurityId: 'vHMa0l5PUysXo',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CategoryId: FwFormField.getValueByDataField($form, 'CategoryId'),
                };
            },
            beforeSave: (request: any) => {
                request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            },
        });
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $subCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        FwBrowse.search($subCategoryGrid);

        if (FwFormField.getValueByDataField($form, 'CatalogCategory')) {
            FwFormField.enable($form.find('.designer'))
            FwFormField.disable($form.find('.barcodetype'))
        } else {
            FwFormField.disable($form.find('.designer'))
            FwFormField.enable($form.find('.barcodetype'))
        }

        if (FwFormField.getValueByDataField($form, 'OverrideProfitAndLossCategory')) {
            FwFormField.enable($form.find('.catvalidation'))
        } else {
            FwFormField.disable($form.find('.catvalidation'))
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {
                    Sales: true
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'InventoryBarCodeDesignerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorybarcodedesigner`);
                break;
            case 'BarCodeDesignerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebarcodedesigner`);
                break;
            case 'ProfitAndLossCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprofitandlosscategoryid`);
                break;
            case 'AssetAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateassetaccount`);
                break;
            case 'IncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateincomeaccount`);
                break;
            case 'SubIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
}

var SalesCategoryController = new SalesCategory();