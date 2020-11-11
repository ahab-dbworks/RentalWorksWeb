routes.push({ pattern: /^module\/partsinventory$/, action: function (match: RegExpExecArray) { return PartsInventoryController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class PartsInventory extends InventoryBase {
    Module: string = 'PartsInventory';
    apiurl: string = 'api/v1/partsinventory';
    caption: string = Constants.Modules.Inventory.children.PartsInventory.caption;
    nav: string = Constants.Modules.Inventory.children.PartsInventory.nav;
    id: string = Constants.Modules.Inventory.children.PartsInventory.id;
    AvailableFor: string = "P";
    //----------------------------------------------------------------------------------------------
    setupNewMode($form: any) {
        super.setupNewMode($form);
        const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        FwFormField.setValueByDataField($form, 'CostCalculation', controlDefaults.defaultpartsquantityinventorycostcalculation);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        super.renderGrids($form, 'Parts');
        //hide columns
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="DailyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="DailyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="WeeklyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="WeeklyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="MonthlyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="MonthlyRate"]').parent('td').hide();
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);

        const $partsInventoryWarehouseGrid = $form.find('[data-name="PartsInventoryWarehouseGrid"]');
        FwBrowse.search($partsInventoryWarehouseGrid);
        const $partsInventoryWarehousePricingGrid = $form.find('[data-name="PartsInventoryWarehousePricingGrid"]');
        FwBrowse.search($partsInventoryWarehousePricingGrid);

        //originalTrackedBy value used in "TrackedBy" evt listener in InventoryBase
        $form.data('originalTrackedBy', FwFormField.getValueByDataField($form, 'TrackedBy'));

        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'PartsInventoryDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${inventoryId}/document`;
            },
            gridSecurityId: 'lwYABhj5zknM',
            moduleSecurityId: this.id,
            parentFormDataFields: 'InventoryId',
            uniqueid1Name: 'InventoryId',
            getUniqueid1Value: () => inventoryId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {
                    Parts: true,
                    HasCategories: true,
                };

                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'CategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                break;
            case 'SubCategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                    CategoryId: categoryId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
            case 'UnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunit`);
                break;
            case 'Rank':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterank`);
                break;
            case 'ManufacturerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturer`);
                break;
            case 'AssetAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateassetaccount`);
                break;
            case 'IncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
            case 'ProfitAndLossCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprofitandloss`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'CountryOfOriginId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountryoforigin`);
        }
    }
};
//----------------------------------------------------------------------------------------------
var PartsInventoryController = new PartsInventory();