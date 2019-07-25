class PhysicalInventory {
    Module: string = 'PhysicalInventory';
    apiurl: string = 'api/v1/physicalinventory';
    caption: string = Constants.Modules.Home.PhysicalInventory.caption;
    nav: string = Constants.Modules.Home.PhysicalInventory.nav;
    id: string = Constants.Modules.Home.PhysicalInventory.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Physical Inventory', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["OfficeLocationId"] == 'undefined') {
            this.ActiveViewFields.OfficeLocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "OfficeLocationId");
        return $menuObject;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            let today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'ScheduleDate', today);

            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);

            FwFormField.setValueByDataField($form, 'CycleIncludeOwned', true);
        }

        $form.find('.prescan').on('click', e => {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Pre-Scan`, `Start Pre-Scan for Physical Inventory ${invNo} ${invDescription}?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Continue', false);
            FwConfirmation.addButton($confirmation, 'Close', true);
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const request: any = {};
            request.PhysicalInventoryId = physicalInventoryId;
            $yes.on('click', () => {
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/prescan', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                    },
                    ex => FwFunc.showError(ex),
                    $form);
            });
        });

        $form.find('.initiate').on('click', e => {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Initiate`, `Initiate Physical Inventory ${invNo} ${invDescription}? This make take a few minutes depending on the number of Inventory items being counted.`);
            const $yes = FwConfirmation.addButton($confirmation, 'Continue', false);
            FwConfirmation.addButton($confirmation, 'Close', true);
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const request: any = {};
            request.PhysicalInventoryId = physicalInventoryId;
            $yes.on('click', () => {
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/initiate', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                    },
                    ex => FwFunc.showError(ex),
                    $form);
            });
        });

        $form.find('.printcountsheet').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();

            const $report = PhysicalInventoryCountSheetReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Count Sheets`);
        });

        $form.find('.printexception').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();

            //const $report = PhysicalInventoryCountSheetReportController.openForm();
            //FwModule.openSubModuleTab($form, $report);

            //FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            //jQuery('.tab.submodule.active').find('.caption').html(`Print Count Sheets`);
        });

        $form.find('.approve').on('click', function (e) {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Approve`, `Approve all Counts for Physical Inventory ${invNo} ${invDescription}?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            var $no = FwConfirmation.addButton($confirmation, 'No', true);
        });

        $form.find('.closeinventorywithadj').on('click', function (e) {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Close Physical Inventory`, `Close Physical Inventory ${invNo} ${invDescription} and apply adjustments to Inventory?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            var $no = FwConfirmation.addButton($confirmation, 'No', true);
        });

        $form.find('.closeinventorywithoutadj').on('click', function (e) {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Close Physical Inventory (Without Adjustments)`, `Close Physical Inventory ${invNo} ${invDescription} without adjusting any Inventory?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            var $no = FwConfirmation.addButton($confirmation, 'No', true);
        });

        $form.find('[data-datafield="CountType"]').on('change', e => {
            const countType = FwFormField.getValueByDataField($form, 'CountType');
            if (countType === 'CYCLE') {
                $form.find('.count-type').show();
            } else if (countType === 'COMPLETE') {
                $form.find('.count-type').hide();
            }
        });

        $form.find('.update-icodes').on('click', () => {
            const $physicalInventoryCycleInventoryGrid = $form.find('[data-name="PhysicalInventoryCycleInventoryGrid"]');
            const request: any = {};
            request.PhysicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const $confirmation = FwConfirmation.renderConfirmation(`Delete all I-Codes?`, `Delete all I-Codes from this list and repopulate the list using these Cycle Count settings?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            FwConfirmation.addButton($confirmation, 'No', true);

            $yes.on('click', (): Promise<any> => {
                return new Promise((resolve, reject) => {
                   promise
                        .then(() => {
                            FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/updateicodes', request, FwServices.defaultTimeout,
                                response => {
                                    $form.find('.error-msg').html('');
                                    if (response.success) {
                                        FwBrowse.search($physicalInventoryCycleInventoryGrid);
                                    } else {
                                        $form.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                                    }
                                },
                                ex => FwFunc.showError(ex),
                                $form);
                            resolve();
                        })
                        .catch((reason) => {
                            reject(reason);
                        });
                });
            });

            var promise = new Promise((resolve, reject) => {
                try {
                    this.saveForm($form, { closetab: false })
                    resolve();
                } catch (ex) {
                    reject(ex);
                }
            })
        });

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PhysicalInventoryId"] input').val(uniqueids.PhysicalInventoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------------------
    renderGrids($form) {
        const $physicalInventoryCycleInventoryGrid = $form.find('div[data-grid="PhysicalInventoryCycleInventoryGrid"]');
        const $physicalInventoryCycleInventoryGridControl = FwBrowse.loadGridFromTemplate('PhysicalInventoryCycleInventoryGrid');
        $physicalInventoryCycleInventoryGrid.empty().append($physicalInventoryCycleInventoryGridControl);
        $physicalInventoryCycleInventoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                PhysicalInventoryId: FwFormField.getValueByDataField($form, 'PhysicalInventoryId')
            };
        });
        $physicalInventoryCycleInventoryGridControl.data('beforesave', request => {
            request.PhysicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId')
        });
        FwBrowse.init($physicalInventoryCycleInventoryGridControl);
        FwBrowse.renderRuntimeHtml($physicalInventoryCycleInventoryGridControl);
    }
    //---------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $physicalInventoryCycleInventoryGrid: any = $form.find('[data-name="PhysicalInventoryCycleInventoryGrid"]');
        FwBrowse.search($physicalInventoryCycleInventoryGrid);

        const countType = FwFormField.getValueByDataField($form, 'CountType');
        if (countType === 'CYCLE') {
            $form.find('.count-type').show();
        } else if (countType === 'COMPLETE') {
            $form.find('.count-type').hide();
        }
    }
    //---------------------------------------------------------------------------------------------
    beforeValidateInventoryType($browse, $grid, request) {
        let validationName = request.module,
            recType = FwFormField.getValueByDataField($grid, 'RecType');
        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {};
                if (recType === 'R') { request.uniqueids.Rental = true };
                if (recType === 'S') { request.uniqueids.Sales = true };
                if (recType === 'P') { request.uniqueids.Parts = true };
                break;
        }

    }
}
//---------------------------------------------------------------------------------------------
var PhysicalInventoryController = new PhysicalInventory();