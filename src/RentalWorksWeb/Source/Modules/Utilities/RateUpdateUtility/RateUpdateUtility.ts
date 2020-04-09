routes.push({ pattern: /^module\/rateupdateutility/, action: function (match: RegExpExecArray) { return RateUpdateUtilityController.getModuleScreen(); } });

class RateUpdateUtility {
    Module: string = 'RateUpdateUtility';
    apiurl: string = 'api/v1/rateupdateutility'
    caption: string = Constants.Modules.Utilities.children.RateUpdateUtility.caption;
    nav: string = Constants.Modules.Utilities.children.RateUpdateUtility.nav;
    id: string = Constants.Modules.Utilities.children.RateUpdateUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.loadItems($form.find('div[data-datafield="Classification"]'), [
            { value: "I", text: "Item", selected: "F" },
            { value: "A", text: "Accessory", selected: "F" },
            { value: "C", text: "Complete", selected: "F" },
            { value: "K", text: "Kit", selected: "F" },
            { value: "S", text: "Set", selected: "F" },
            { value: "W", text: "Wall", selected: "F" }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="OrderBy"]'), [
            { value: "warehouse", text: "Warehouse", selected: "F" },
            { value: "inventorydepartment", text: "Department", selected: "F" },
            { value: "category", text: "Category", selected: "F" },
            { value: "icode", text: "I-Code", selected: "F" }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="Rank"]'), [
            { value: "A", text: "A", selected: "T" },
            { value: "B", text: "B", selected: "T" },
            { value: "C", text: "C", selected: "T" },
            { value: "D", text: "D", selected: "T" },
            { value: "E", text: "E", selected: "T" },
            { value: "F", text: "F", selected: "T" },
            { value: "G", text: "G", selected: "T" }
        ]);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
        //Search button to refresh grid with filters
        $form.on('click', '.search', e => {
            const $rateUpdateItemGrid = $form.find('[data-name="RateUpdateItemGrid"]');
            //const searchRequest = {
            //    AvailableFor: FwFormField.getValueByDataField($form, 'AvailableFor'),
            //    Classification: FwFormField.getValueByDataField($form, 'Classification'),
            //    OrderBy: FwFormField.getValueByDataField($form, 'OrderBy'),
            //    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
            //    Description: FwFormField.getValueByDataField($form, 'Description'),
            //    InventoryTypeId: FwFormField.getValueByDataField($form, 'InventoryTypeId'),
            //    CategoryId: FwFormField.getValueByDataField($form, 'CategoryId'),
            //    SubCategoryId: FwFormField.getValueByDataField($form, 'SubCategoryId'),
            //    Rank: FwFormField.getValueByDataField($form, 'Rank'),
            //    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
            //    UnitId: FwFormField.getValueByDataField($form, 'UnitId'),
            //    ManufacturerId: FwFormField.getValueByDataField($form, 'ManufacturerId'),
            //    ShowPendingModifications: FwFormField.getValueByDataField($form, 'ShowPendingModifications')
            //};

            //if ($rateUpdateItemGrid) {
            //    const databind = $rateUpdateItemGrid.data('ondatabind');
            //    if (typeof databind === 'function') {
            //        $rateUpdateItemGrid.data('ondatabind', request => {
            //            databind(request);
            //            request.uniqueids = searchRequest;
            //        });
            //        FwBrowse.search($rateUpdateItemGrid);
            //    }
            //}
            FwBrowse.search($rateUpdateItemGrid);
        });

        //Enable/Disable fields based on Activity Type
        $form.on('change', '[data-datafield="AvailableFor"]', e => {
            const activityType = FwFormField.getValueByDataField($form, 'AvailableFor');

            $form.find('[data-datafield="Classification"] input, [data-datafield="Rank"] input').removeAttr('disabled');
            $form.find('[data-datafield="UnitId"], [data-datafield="ManufacturerId"]').attr('data-enabled', 'true');
            
            switch (activityType) {
                case 'S':
                    $form.find('[data-datafield="Classification"]').find('[data-value="S"], [data-value="W"]').find('input').attr('disabled', 'disabled');
                    break;
                case 'L':
                    $form.find('[data-datafield="Classification"] input, [data-datafield="Rank"] input').attr('disabled', 'disabled');
                    $form.find('[data-datafield="UnitId"], [data-datafield="ManufacturerId"]').attr('data-enabled', 'false');
                    break;
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let availableFor = FwFormField.getValueByDataField($form, 'AvailableFor');
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        let subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        switch (datafield) {
            case 'InventoryTypeId':
                if (availableFor === 'R') {
                    request.uniqueids = {
                        Rental: true,
                    };
                } else if (availableFor === 'S') {
                    request.uniqueids = {
                        Sales: true,
                    };
                } else if (availableFor === 'P') {
                    request.uniqueids = {
                        Parts: true,
                    };
                } else if (availableFor === 'L') {
                    request.uniqueids = {
                        Labor: true,
                    };
                } else if (availableFor === 'M') {
                    request.uniqueids = {
                        Misc: true,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'CategoryId':
                if (inventoryTypeId) {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                break;
            case 'SubCategoryId':
                if (inventoryTypeId) {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    };
                }
                if (categoryId) {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
            case 'InventoryId':
                if (availableFor) {
                    request.uniqueids = {
                        AvailableFor: availableFor,
                    };
                }
                if (inventoryTypeId) {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    };
                }
                if (categoryId) {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    };
                }
                if (subCategoryId) {
                    request.uniqueids = {
                        SubCategoryId: subCategoryId,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'UnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunit`);
                break;
            case 'ManufacturerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturer`);
                break;

        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        FwBrowse.renderGrid({
            nameGrid: 'RateUpdateItemGrid',
            gridSecurityId: 'QQwyjnERS0Jx',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    AvailableFor: FwFormField.getValueByDataField($form, 'AvailableFor')
                };
                if (FwFormField.getValueByDataField($form, 'Classification')) request.uniqueids.Classification = FwFormField.getValueByDataField($form, 'Classification');
                if (FwFormField.getValueByDataField($form, 'InventoryId')) request.uniqueids.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                if (FwFormField.getValueByDataField($form, 'Description')) request.uniqueids.Description = FwFormField.getValueByDataField($form, 'Description');
                if (FwFormField.getValueByDataField($form, 'InventoryTypeId')) request.uniqueids.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
                if (FwFormField.getValueByDataField($form, 'CategoryId')) request.uniqueids.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
                if (FwFormField.getValueByDataField($form, 'SubCategoryId')) request.uniqueids.SubCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
                if (FwFormField.getValueByDataField($form, 'Rank')) request.uniqueids.Rank = FwFormField.getValueByDataField($form, 'Rank');
                if (FwFormField.getValueByDataField($form, 'WarehouseId')) request.uniqueids.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                if (FwFormField.getValueByDataField($form, 'UnitId')) request.uniqueids.UnitId = FwFormField.getValueByDataField($form, 'UnitId');
                if (FwFormField.getValueByDataField($form, 'ManufacturerId')) request.uniqueids.ManufacturerId = FwFormField.getValueByDataField($form, 'ManufacturerId');
                if (FwFormField.getValueByDataField($form, 'ShowPendingModifications')) request.uniqueids.ShowPendingModifications = FwFormField.getValueByDataField($form, 'ShowPendingModifications');
                if (FwFormField.getValueByDataField($form, 'OrderBy')) request.uniqueids.OrderBy = FwFormField.getValueByDataField($form, 'OrderBy');
            },
            //afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
             
            //}
        });
    }
    //----------------------------------------------------------------------------------------------
}
var RateUpdateUtilityController = new RateUpdateUtility();