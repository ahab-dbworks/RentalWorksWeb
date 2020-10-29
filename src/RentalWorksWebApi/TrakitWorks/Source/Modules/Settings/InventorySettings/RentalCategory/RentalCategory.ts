class RentalCategory {
    Module: string = 'RentalCategory';
    apiurl: string = 'api/v1/rentalcategory';
    caption: string = Constants.Modules.Settings.children.InventorySettings.children.RentalCategory.caption;
    nav: string = Constants.Modules.Settings.children.InventorySettings.children.RentalCategory.nav;
    id: string = Constants.Modules.Settings.children.InventorySettings.children.RentalCategory.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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
    renderGrids($form: any) {
        //const $subCategoryGrid = $form.find('div[data-grid="SubCategoryGrid"]');
        //const $subCategoryControl = FwBrowse.loadGridFromTemplate('SubCategoryGrid');
        //$subCategoryGrid.empty().append($subCategoryControl);
        //$subCategoryControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CategoryId: $form.find('div.fwformfield[data-datafield="CategoryId"] input').val()
        //    }
        //});
        //$subCategoryControl.data('beforesave', function (request) {
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
            }
        });
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
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CategoryId"] input').val(uniqueids.CategoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $laborCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        FwBrowse.search($laborCategoryGrid);

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
    events($form: JQuery): void {
        $form.on('change', '[data-datafield="OverrideProfitAndLossCategory"]', (e) => {
            if (FwFormField.getValueByDataField($form, 'OverrideProfitAndLossCategory')) {
                FwFormField.enable($form.find('.catvalidation'))
            } else {
                FwFormField.disable($form.find('.catvalidation'))
            }
        });

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
        $form.find('div[data-datafield="DepreciationExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="DepreciationExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="AccumulatedDepreciationExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AccumulatedDepreciationExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
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
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        request.uniqueids = {
            Rental: true
        }
        switch (datafield) {
            case 'InventoryTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'InventoryBarCodeDesignerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorybarcodedesigner`);
                break;
            case 'BarCodeDesignerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebarcodedesigner`);
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
            case 'EquipmentSaleIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateequipmentsaleincomeaccount`);
                break;
            case 'LdIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateldincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
            case 'CostOfGoodsRentedExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodsrentedexpenseaccount`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
}

var RentalCategoryController = new RentalCategory();