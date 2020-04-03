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
            { value: "WAREHOUSE", text: "Warehouse", selected: "F" },
            { value: "DEPARTMENT", text: "Department", selected: "F" },
            { value: "CATEGORY", text: "Category", selected: "F" },
            { value: "ICODE", text: "I-Code", selected: "F" }
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
        $form.on('change', '[data-datafield="ActivityType"]', e => {
            const activityType = FwFormField.getValueByDataField($form, 'ActivityType');

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
        let activityType = FwFormField.getValueByDataField($form, 'ActivityType');
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        let subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        switch (datafield) {
            case 'InventoryTypeId':
                if (activityType === 'R') {
                    request.uniqueids = {
                        Rental: true,
                    };
                } else if (activityType === 'S') {
                    request.uniqueids = {
                        Sales: true,
                    };
                } else if (activityType === 'P') {
                    request.uniqueids = {
                        Parts: true,
                    };
                } else if (activityType === 'L') {
                    request.uniqueids = {
                        Labor: true,
                    };
                } else if (activityType === 'M') {
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
                if (activityType) {
                    request.uniqueids = {
                        AvailFor: activityType,
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
}
var RateUpdateUtilityController = new RateUpdateUtility();