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

        FwMenu.addSubMenuItem(options.$groupOptions, 'Rate Update Report', '', (e: JQuery.ClickEvent) => {
            try {
                this.printRateUpdateReport(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        FwMenu.addSubMenuItem(options.$groupOptions, 'Apply All Pending Modifications Now', '', (e: JQuery.ClickEvent) => {
            try {
                this.applyAllPendingModifications(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
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
            //{ value: "S", text: "Set", selected: "F" },
            //{ value: "W", text: "Wall", selected: "F" }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="OrderBy"]'), [
            { value: "Warehouse", text: "Warehouse", selected: "T" },
            { value: "InventoryType", text: "Type", selected: "T" },
            { value: "Category", text: "Category", selected: "T" },
            { value: "SubCategory", text: "Sub-Category", selected: "T" },
            { value: "ICode", text: "I-Code", selected: "T" }
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

        FwFormField.loadItems($form.find('div[data-datafield="AvailableFor"]'), [
            { value: "R", caption: "Rental", checked: 'checked'},
            { value: "S", caption: "Sales" },
            { value: "P", caption: "Parts" },
            { value: "L", caption: "Labor" },
            { value: "M", caption: "Miscellaneous" },
            //{ value: "T", caption: "Transportation" }
        ]);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'))
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
        //Search button to refresh grid with filters
        $form.on('click', '.search', e => {
            const $rateUpdateItemGrid = $form.find('[data-name="RateUpdateItemGrid"]');
            FwBrowse.search($rateUpdateItemGrid);
        });

        //Enable/Disable fields based on Activity Type

        $form.on('change', '[data-datafield="AvailableFor"]', e => {
            const activityType = FwFormField.getValueByDataField($form, 'AvailableFor');

            //enables previously disabled fields
            $form.find('[data-datafield="Classification"] input, [data-datafield="Rank"] input').removeAttr('disabled');
            $form.find('[data-datafield="ManufacturerId"]').attr('data-enabled', 'true');

            switch (activityType) {
                case 'R':
                    break;
                case 'S':
                case 'P':
                    $form.find('[data-datafield="Classification"]').find('[data-value="S"], [data-value="W"]').find('input').attr('disabled', 'disabled');
                    break;
                case 'M':
                case 'L':
                    $form.find('[data-datafield="Classification"] input, [data-datafield="Rank"] input').attr('disabled', 'disabled');
                    $form.find('[data-datafield="ManufacturerId"]').attr('data-enabled', 'false');
                    break;
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    toggleAvailableFor($form: JQuery) {
        const rentalColumns: Array<string> = ['Manufacturer', 'PartNumber', 'Rank', 'DailyRate', 'WeeklyRate', 'Week2Rate', 'Week3Rate', 'Week4Rate', 'MonthlyRate', 'MaxDiscount', 'MinimumDaysPerWeek', 'DailyCost', 'WeeklyCost', 'MonthlyCost'];
        const salesColumns: Array<string> = ['Manufacturer', 'PartNumber', 'Rank', 'Retail', /*'Sell',*/ 'DefaultCost', 'MaxDiscount']; //same columns for Parts
        const laborColumns: Array<string> = ['HourlyRate', 'DailyRate', 'WeeklyRate', 'MonthlyRate', 'HourlyCost', 'DailyCost', 'WeeklyCost', 'MonthlyCost']; //same columns for Misc
        const activityType = FwFormField.getValueByDataField($form, 'AvailableFor');
        const $rateUpdateItemGrid = $form.find('[data-name="RateUpdateItemGrid"]');

        const toggleColumns = (type: string) => {
            switch (type) {
                case 'R':
                    activityTypeColumnDisplay(salesColumns, false);
                    activityTypeColumnDisplay(laborColumns, false);
                    activityTypeColumnDisplay(rentalColumns, true);
                    break;
                case 'S':
                case 'P':
                    activityTypeColumnDisplay(rentalColumns, false);
                    activityTypeColumnDisplay(laborColumns, false);
                    activityTypeColumnDisplay(salesColumns, true);
                    break;
                case 'M':
                case 'L':
                    activityTypeColumnDisplay(salesColumns, false);
                    activityTypeColumnDisplay(rentalColumns, false);
                    activityTypeColumnDisplay(laborColumns, true);
                    break;
            }
        }

        const activityTypeColumnDisplay = (columns: Array<string>, showColumns: boolean) => {
            for (let i = 0; i < columns.length; i++) {
                const $td = $rateUpdateItemGrid.find(`[data-browsedatafield="${columns[i]}"]`).parents('td');
                if (showColumns) {
                    $td.show();
                } else {
                    $td.hide();
                }
            }
        }
        //if (cachedType != activityType) {
          /*  toggleColumns(cachedType, false);*/ //hides cached type's columns
            toggleColumns(activityType); //shows selected type's columns
           /* $form.data('activitytype', activityType);*/ //updates cached type
        //}
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let availableFor = FwFormField.getValueByDataField($form, 'AvailableFor');
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        let subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        switch (datafield) {
            case 'InventoryId':
                if (availableFor) {
                    request.uniqueids = {
                        AvailFor: availableFor,
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
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                const availableFor = FwFormField.getValueByDataField($form, 'AvailableFor');
                request.uniqueids = {
                    AvailableFor: availableFor
                };
                if (FwFormField.getValueByDataField($form, 'InventoryId')) request.uniqueids.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                if (FwFormField.getValueByDataField($form, 'Description')) request.uniqueids.Description = FwFormField.getValueByDataField($form, 'Description');
                if (FwFormField.getValueByDataField($form, 'InventoryTypeId')) request.uniqueids.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
                if (FwFormField.getValueByDataField($form, 'CategoryId')) request.uniqueids.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
                if (FwFormField.getValueByDataField($form, 'SubCategoryId')) request.uniqueids.SubCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
                if (availableFor == 'L' || availableFor == 'M') {
                    request.uniqueids.Rank = '';
                    request.uniqueids.Classification = '';
                } else {
                    if (FwFormField.getValueByDataField($form, 'Rank')) request.uniqueids.Rank = FwFormField.getValueByDataField($form, 'Rank');
                    if (FwFormField.getValueByDataField($form, 'Classification')) request.uniqueids.Classification = FwFormField.getValueByDataField($form, 'Classification');
                }
                if (FwFormField.getValueByDataField($form, 'WarehouseId')) request.uniqueids.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                if (FwFormField.getValueByDataField($form, 'UnitId')) request.uniqueids.UnitId = FwFormField.getValueByDataField($form, 'UnitId');
                if (FwFormField.getValueByDataField($form, 'ManufacturerId')) request.uniqueids.ManufacturerId = FwFormField.getValueByDataField($form, 'ManufacturerId');
                if (FwFormField.getValueByDataField($form, 'ShowPendingModifications')) request.uniqueids.ShowPendingModifications = FwFormField.getValueByDataField($form, 'ShowPendingModifications');
                if (FwFormField.getValueByDataField($form, 'OrderBy').length) {
                    const $checkboxlist = FwFormField.getValueByDataField($form, 'OrderBy');
                    let orderBy: string = "";
                    for (let i = 0; i < $checkboxlist.length; i++) {
                        if (i != 0 && i < $checkboxlist.length) {
                            orderBy = orderBy.concat(',');
                        }
                        orderBy = orderBy.concat(`${$checkboxlist[i].value.toString()}`);
                    }
                    request.orderby = orderBy;
                }
            },
            beforeSave: (request: any, $browse: JQuery, $tr: JQuery) => {
                //justin hoffman 04/28/2020 this is an unusual case where I need to use the InventoryId, which is a read-only validation field, also as a key field
                request.InventoryId = $tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.toggleAvailableFor($form);
                this.renderWideGridColumns($form.find('[data-name="RateUpdateItemGrid"]'));
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderWideGridColumns($grid: JQuery) {
        if ($grid.find('thead tr').length < 2) {
            const $thead = $grid.find('thead tr').clone(false);
            $thead.find('[data-sort]').removeAttr('data-sort');
            $thead.find('td div.divselectrow').hide();
            $thead.find('td div.field:not([data-sharedcolumn])').hide();
            //const $sharedColumnTds = $thead.find('td div[data-sharedcolumn]:even').parents('td');
            const $sharedColumnTds = $thead.find('td div[data-widerow]').parents('td');
            for (let i = 0; i < $sharedColumnTds.length; i++) {
                const $td = jQuery($sharedColumnTds[i]);
                const caption = $td.find('div[data-sharedcolumn]').attr('data-sharedcolumn');
                $td.find('.caption').text(caption);
            }
            //$sharedColumnTds.attr('colspan', 2);
            $sharedColumnTds.css('text-align', 'center');
            //$thead.find('td div[data-sharedcolumn]:odd').parents().remove();
            $grid.find('thead').prepend($thead);
        }
    }
    //----------------------------------------------------------------------------------------------
    printRateUpdateReport($form: any, batch?: any) {
        try {
            const $report = RateUpdateReportController.openForm();
            FwModule.openModuleTab($report, 'Rate Update Report', true, 'FORM', true);
            if (typeof batch != 'undefined') {
                FwFormField.setValueByDataField($form, 'RateUpdateBatchId', batch.RateUpdateBatchId, batch.RateUpdateBatchName);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
     //----------------------------------------------------------------------------------------------
    applyAllPendingModifications($form: any) {
        let $confirmation, $ok;
        $confirmation = FwConfirmation.renderConfirmation('Apply All Pending Modifications', '');
        const html: Array<string> = [];;
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Batch Name" data-datafield="RateUpdateBatchName"></div>');
        html.push('    <div style="margin:1em; color:red;">New Costs and Rates will be applied immediately, and cannot be undone.</div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        $ok = FwConfirmation.addButton($confirmation, 'OK', false);
        FwConfirmation.addButton($confirmation, 'Cancel');
        $ok.on('click', () => {
            try {
                const request: any = {};
                request.RateUpdateBatchName = FwFormField.getValueByDataField($confirmation, 'RateUpdateBatchName');
                FwAppData.apiMethod(true, 'POST', 'api/v1/rateupdateutility/apply', request, FwServices.defaultTimeout,
                    response => {
                        try {
                            FwNotification.renderNotification('SUCCESS', 'Rates Successfully Updated.');
                            FwConfirmation.destroyConfirmation($confirmation);
                            program.navigate('module/rateupdateutility');
                            this.printRateUpdateReport($form, { batchid: response.RateUpdateBatchId, batchname: response.RateUpdateBatchName });
                            //todo - add progress meter
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }, ex => FwFunc.showError(ex), $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
     //----------------------------------------------------------------------------------------------
}
var RateUpdateUtilityController = new RateUpdateUtility();