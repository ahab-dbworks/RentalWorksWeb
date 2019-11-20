class PhysicalInventory {
    Module: string = 'PhysicalInventory';
    apiurl: string = 'api/v1/physicalinventory';
    caption: string = Constants.Modules.Inventory.children.PhysicalInventory.caption;
    nav: string = Constants.Modules.Inventory.children.PhysicalInventory.nav;
    id: string = Constants.Modules.Inventory.children.PhysicalInventory.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["OfficeLocationId"] == 'undefined') {
            this.ActiveViewFields.OfficeLocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "OfficeLocationId");
    };
    //---------------------------------------------------------------------------------------------
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
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

;
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            let today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'ScheduleDate', today);

            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);

            FwFormField.setValueByDataField($form, 'CycleIncludeOwned', true);

            //disables buttons in NEW mode
            FwFormField.disable($form.find('[data-type="button"]'));
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
                FwConfirmation.destroyConfirmation($confirmation);
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/prescan', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                        const $tabControl = $form.find('.fwtabs');
                        const $countTab = $form.find('[data-type="tab"][data-caption="Count"]');
                        FwTabs.setActiveTab($tabControl, $countTab);
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
                FwConfirmation.destroyConfirmation($confirmation);
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/initiate', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                        const $tabControl = $form.find('.fwtabs');
                        const $countTab = $form.find('[data-type="tab"][data-caption="Count"]');
                        FwTabs.setActiveTab($tabControl, $countTab);
                    },
                    ex => FwFunc.showError(ex),
                    $form);
            });
        });

        $form.find('.printcountsheet').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const $report = PhysicalInventoryCountSheetReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Count Sheets`);
            const request: any = {};
            request.PhysicalInventoryId = physicalInventoryId;
            FwAppData.apiMethod(true, 'POST', `api/v1/physicalinventory/${physicalInventoryId}/updatestep/printcountsheet`, request, FwServices.defaultTimeout,
                response => {
                    $form.empty().append(PhysicalInventoryController.loadForm(request));
                    const $tabControl = $form.find('.fwtabs');
                    const $countTab = $form.find('[data-type="tab"][data-caption="Count"]');
                    FwTabs.setActiveTab($tabControl, $countTab);
                },
                ex => FwFunc.showError(ex),
                $form);
        });
        // ----------
        $form.find('.countquantity').on('click', e => {
            const inventoryInfo: any = {};
            inventoryInfo.PhysicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            inventoryInfo.PhysicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            inventoryInfo.ScheduleDate = FwFormField.getValueByDataField($form, 'ScheduleDate');
            inventoryInfo.Description = FwFormField.getValueByDataField($form, 'Description');
            //inventoryInfo.Status = FwFormField.getValueByDataField($form, 'Status');
            inventoryInfo.OfficeLocation = FwFormField.getTextByDataField($form, 'OfficeLocationId');
            inventoryInfo.OfficeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
            inventoryInfo.Warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
            inventoryInfo.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            const $countquantityForm = CountQuantityInventoryController.openForm('EDIT', inventoryInfo);
            FwModule.openSubModuleTab($form, $countquantityForm);
        });
        // ----------
        $form.find('.countbarcode').on('click', e => {
            const inventoryInfo: any = {};
            inventoryInfo.PhysicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            inventoryInfo.PhysicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            inventoryInfo.ScheduleDate = FwFormField.getValueByDataField($form, 'ScheduleDate');
            inventoryInfo.Description = FwFormField.getValueByDataField($form, 'Description');
            //inventoryInfo.Status = FwFormField.getValueByDataField($form, 'Status');
            inventoryInfo.OfficeLocation = FwFormField.getTextByDataField($form, 'OfficeLocationId');
            inventoryInfo.OfficeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
            inventoryInfo.Warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
            inventoryInfo.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            const $scanbarcodesForm = ScanBarCodesController.openForm('EDIT', inventoryInfo);
            FwModule.openSubModuleTab($form, $scanbarcodesForm);
            $scanbarcodesForm.find('[data-datafield="BarCode"] input').focus();
        });
        // ----------
        $form.find('.printexception').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');

            const $report = PhysicalInventoryExceptionReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Exception Report`);
        });
        // ----------
        $form.find('.printediscrepancy').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');

            const $report = PhysicalInventoryDiscrepancyReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Discrepancy Report`);
        });
        // ----------
        $form.find('.printrecount').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');

            const $report = PhysicalInventoryRecountAnalysisReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Recount Analysis Report`);
        });
        // ----------
        $form.find('.printreconciliation').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');

            const $report = PhysicalInventoryReconciliationReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Reconciliation Report`);
        });
        // ----------
        $form.find('.printphysicalinventory').on('click', e => {
            const physicalInventoryNumber = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');

            const $report = PhysicalInventoryResultsReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValueByDataField($report, 'PhysicalInventoryId', physicalInventoryId, physicalInventoryNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print Results Report`);
        });
        // ----------
        $form.find('.approve').on('click', function (e) {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Approve`, `Approve all Counts for Physical Inventory ${invNo} ${invDescription}?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            FwConfirmation.addButton($confirmation, 'No', true);

            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const request: any = {};
            request.PhysicalInventoryId = physicalInventoryId;
            $yes.on('click', () => {
                FwConfirmation.destroyConfirmation($confirmation);
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/approve', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                    },
                    ex => FwFunc.showError(ex),
                    $form);
            });
        });

        $form.find('.closeinventorywithadj').on('click', function (e) {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Close Physical Inventory`, `Close Physical Inventory ${invNo} ${invDescription} and apply adjustments to Inventory?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            FwConfirmation.addButton($confirmation, 'No', true);

            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const request: any = {};
            request.PhysicalInventoryId = physicalInventoryId;
            $yes.on('click', () => {
                FwConfirmation.destroyConfirmation($confirmation);
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/close', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                    },
                    ex => FwFunc.showError(ex),
                    $form);
            });
        });

        $form.find('.closeinventorywithoutadj').on('click', function (e) {
            const invNo = FwFormField.getValueByDataField($form, 'PhysicalInventoryNumber');
            const invDescription = FwFormField.getValueByDataField($form, 'Description');
            const $confirmation = FwConfirmation.renderConfirmation(`Close Physical Inventory (Without Adjustments)`, `Close Physical Inventory ${invNo} ${invDescription} without adjusting any Inventory?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            FwConfirmation.addButton($confirmation, 'No', true);

            const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            const request: any = {};
            request.PhysicalInventoryId = physicalInventoryId;
            $yes.on('click', () => {
                FwConfirmation.destroyConfirmation($confirmation);
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/close', request, FwServices.defaultTimeout,
                    response => {
                        $form.empty().append(PhysicalInventoryController.loadForm(request));
                    },
                    ex => FwFunc.showError(ex),
                    $form);
            });
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

            $yes.on('click', () => {
                FwConfirmation.destroyConfirmation($confirmation);
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
            });
        });

        //disables update icodes button when the form is modified
        $form.on('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])', e => {
            FwFormField.disable($form.find('.update-icodes'));
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
    afterSave($form) {

    }
    //---------------------------------------------------------------------------------------------
    renderGrids($form) {
        //const $physicalInventoryCycleInventoryGrid = $form.find('div[data-grid="PhysicalInventoryCycleInventoryGrid"]');
        //const $physicalInventoryCycleInventoryGridControl = FwBrowse.loadGridFromTemplate('PhysicalInventoryCycleInventoryGrid');
        //$physicalInventoryCycleInventoryGrid.empty().append($physicalInventoryCycleInventoryGridControl);
        //$physicalInventoryCycleInventoryGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        PhysicalInventoryId: FwFormField.getValueByDataField($form, 'PhysicalInventoryId')
        //    };
        //});
        //$physicalInventoryCycleInventoryGridControl.data('beforesave', request => {
        //    request.PhysicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId')
        //});
        //FwBrowse.init($physicalInventoryCycleInventoryGridControl);
        //FwBrowse.renderRuntimeHtml($physicalInventoryCycleInventoryGridControl);

        //Physical Inventory Cycle Inventory Grid
        FwBrowse.renderGrid({
            nameGrid: 'PhysicalInventoryCycleInventoryGrid',
            gridSecurityId: 'juyq8FkxJPR5Q',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PhysicalInventoryId: FwFormField.getValueByDataField($form, 'PhysicalInventoryId')
                };
            }, 
            beforeSave: (request: any) => {
                request.PhysicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
            }
        });

    }
    //---------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        FwFormField.enable($form.find('.update-icodes'));

        const $physicalInventoryCycleInventoryGrid: any = $form.find('[data-name="PhysicalInventoryCycleInventoryGrid"]');
        FwBrowse.search($physicalInventoryCycleInventoryGrid);

        const countType = FwFormField.getValueByDataField($form, 'CountType');
        if (countType === 'CYCLE') {
            $form.find('.count-type').show();
        } else if (countType === 'COMPLETE') {
            $form.find('.count-type').hide();
        }

        const preScanDateTime = FwFormField.getValueByDataField($form, 'PreScanDateTime');
        if (preScanDateTime.length > 0) {
            $form.find('.prescan-desc span').remove();
            $form.find('.prescan-desc').append(`<span style="color:red; margin-left:20px;">Pre-Scan started on ${preScanDateTime}</span>`);
        }

        const initiateDateTime = FwFormField.getValueByDataField($form, 'InitiateDateTime');
        if (initiateDateTime.length > 0) {
            $form.find('.initiate-desc span').remove();
            $form.find('.initiate-desc').append(`<span style="color:red; margin-left:20px;">Initiated on ${initiateDateTime}</span>`);
        }

        const $buttonFields = $form.find('.button-fields');
        for (let i = 0; i < $buttonFields.length; i++) {
            const field = jQuery($buttonFields[i]).attr('data-datafield');
            const isAllowed = FwFormField.getValueByDataField($form, field);
            const $button = jQuery($buttonFields[i]).siblings('[data-type="button"]');
            isAllowed == 'false' ? FwFormField.disable($button) : FwFormField.enable($button);
        }
    }
    //---------------------------------------------------------------------------------------------
    //beforeValidateInventoryType($browse, $grid, request) {
    //    let validationName = request.module,
    //        recType = FwFormField.getValueByDataField($grid, 'RecType');
    //    switch (validationName) {
    //        case 'InventoryTypeValidation':
    //            request.uniqueids = {};
    //            if (recType === 'R') { request.uniqueids.Rental = true };
    //            if (recType === 'S') { request.uniqueids.Sales = true };
    //            if (recType === 'P') { request.uniqueids.Parts = true };
    //            break;
    //    }
    //}
    //beforeValidateCategory($browse, $grid, request) {
    //    let inventoryTypeId = FwFormField.getValueByDataField($grid, 'InventoryTypeId');
    //    let recType = FwFormField.getValueByDataField($grid, 'RecType');
    //    request.uniqueids = {
    //        InventoryTypeId: inventoryTypeId,
    //        RecType: recType
    //    };
    //}
    //beforeValidateSubCategory($browse, $grid, request) {
    //    let inventoryTypeId = FwFormField.getValueByDataField($grid, 'InventoryTypeId');
    //    let categoryId = FwFormField.getValueByDataField($grid, 'CategoryId');
    //    let recType = FwFormField.getValueByDataField($grid, 'RecType');
    //    request.uniqueids = {
    //        InventoryTypeId: inventoryTypeId,
    //        CategoryId: categoryId,
    //        RecType: recType
    //    };
    //}
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        let recType = FwFormField.getValueByDataField($form, 'RecType');
        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {};
                if (recType === 'R') { request.uniqueids.Rental = true };
                if (recType === 'S') { request.uniqueids.Sales = true };
                if (recType === 'P') { request.uniqueids.Parts = true };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'CategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                    RecType: recType
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                break;
            case 'SubCategoryId':
                request.uniqueids = {
                InventoryTypeId: inventoryTypeId,
                CategoryId: categoryId,
                RecType: recType
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
        }
    }
}
//---------------------------------------------------------------------------------------------
var PhysicalInventoryController = new PhysicalInventory();