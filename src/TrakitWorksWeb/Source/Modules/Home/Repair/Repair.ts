routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
routes.push({ pattern: /^module\/repair\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return RepairController.getModuleScreen(filter); } });


class Repair {
    Module:             string = 'Repair';
    apiurl:             string = 'api/v1/repair';
    caption: string = Constants.Modules.Home.Repair.caption;
	nav: string = Constants.Modules.Home.Repair.nav;
	id: string = Constants.Modules.Home.Repair.id;
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = (filter?: { datafield: string, search: string }) => {
        const screen: any = {};
        screen.$view      = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel  = {};
        screen.properties = {};

        const $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            // Dashboard search
            if (typeof filter !== 'undefined') {
                const datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                const parsedSearch = filter.search.replace(/%20/g, " ");
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(parsedSearch);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        const self = this;
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
            const out = response.filter(item => item.StatusType === 'OUT');
            const intransit = response.filter(item => item.StatusType === 'INTRANSIT');

            FwBrowse.addLegend($browse, 'Priority', '#EA300F');
            FwBrowse.addLegend($browse, 'Outside', '#fffb38');
            FwBrowse.addLegend($browse, 'Released', '#d6d319');
            FwBrowse.addLegend($browse, 'Pending Repair', out[0].Color);
            FwBrowse.addLegend($browse, 'Transferred', intransit[0].Color);
        }, null, $browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }
        const viewSubItems: Array<JQuery> = [];
        viewSubItems.push($userWarehouse, $all);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubItems, true, "WarehouseId");

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm = (mode: string) => {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        // New Orders
        if (mode === 'NEW') {
            const today      = FwFunc.getDate();
            const department = JSON.parse(sessionStorage.getItem('department'));
            const office     = JSON.parse(sessionStorage.getItem('location'));
            const warehouse  = JSON.parse(sessionStorage.getItem('warehouse'));
            const userId     = JSON.parse(sessionStorage.getItem('userid'));
            const location   = JSON.parse(sessionStorage.getItem('location'));

            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValueByDataField($form, 'Priority', 'MED');
            FwFormField.setValueByDataField($form, 'RepairDate', today);
            FwFormField.setValueByDataField($form, 'Location', office.location);
            FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);
            FwFormField.setValueByDataField($form, 'Quantity', 1);
            FwFormField.setValueByDataField($form, 'InputByUserId', userId.webusersid);
            FwFormField.setValueByDataField($form, 'LocationId', location.locationid);

            FwFormField.enable($form.find('div[data-displayfield="ICode"]'));
        }

        FwFormField.loadItems($form.find('div[data-datafield="Priority"]'), [
            { value: 'HIG', text: 'High' },
            { value: 'MED', text: 'Medium', selected:true },
            { value: 'LOW', text: 'Low' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="AvailFor"]'), [
            { value: 'R', text: 'Rent' },
            { value: 'S', text: 'Sell' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="RepairType"]'), [
            { value: 'OWNED',   text: 'Owned' },
            { value: 'OUTSIDE', text: 'Not Owned' }
        ], true);

        this.events($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm = (uniqueids: any) => {
        let $form: JQuery = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="RepairId"] input').val(uniqueids.RepairId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const $repairReleaseGrid = $form.find('div[data-grid="RepairReleaseGrid"]');
        const $repairReleaseGridControl = FwBrowse.loadGridFromTemplate('RepairReleaseGrid');
        $repairReleaseGrid.empty().append($repairReleaseGridControl);
        $repairReleaseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RepairId: FwFormField.getValueByDataField($form, 'RepairId')
            }
        });
        $repairReleaseGridControl.data('beforesave', request => {
            request.RepairId = FwFormField.getValueByDataField($form, 'RepairId');
        });
        FwBrowse.init($repairReleaseGridControl);
        FwBrowse.renderRuntimeHtml($repairReleaseGridControl);
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        $form.find('.completeestimate').show();
        $form.find('.releasesection').show();

        let $repairReleaseGrid: any = $form.find('[data-name="RepairReleaseGrid"]');
        FwBrowse.search($repairReleaseGrid);

        if (FwFormField.getValueByDataField($form, 'Status') === 'ESTIMATED') {
            $form.data('hasEstimated', true);
        } else {
            $form.data('hasEstimated', false);
        }

        if (FwFormField.getValueByDataField($form, 'Status') === 'COMPLETE') {
            $form.data('hasCompleted', true);
        } else {
            $form.data('hasCompleted', false);
        }

        FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
        FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
        FwFormField.disable($form.find('div[data-displayfield="ICode"]'));
        FwFormField.disable($form.find('div[data-displayfield="RfId"]'));
        FwFormField.disable($form.find('div[data-displayfield="DamageOrderNumber"]'));
        FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
        FwFormField.disable($form.find('div[data-datafield="RepairType"]'));
        FwFormField.disable($form.find('div[data-datafield="PendingRepair"]'));
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        // BarCode, SN, RFID Validation
        $form.find('div[data-datafield="ItemId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-displayfield="BarCode"]', $tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-displayfield="ICode"]', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="ICode"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-displayfield="SerialNumber"] ', $tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-displayfield="RfId"]', $tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="RfId"]').attr('data-originalvalue'));
            FwFormField.disable($form.find('div[data-displayfield="ICode"]'));
        });

        // ICode Validation
        $form.find('div[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
            FwFormField.enable($form.find('div[data-datafield="Quantity"]'));
            FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
            FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
            FwFormField.disable($form.find('div[data-displayfield="RfId"]'));

            if (FwFormField.getValue($form, 'div[data-datafield="AvailFor"]') === 'S') {
                FwFormField.setValue($form, '.icoderental', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'));
            } else {
                FwFormField.setValue($form, '.icodesales', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'));
            }
        });

        // Order Validation
        $form.find('div[data-datafield="DamageOrderId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="DamageOrderDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="DamageDeal"]', $tr.find('.field[data-formdatafield="Deal"]').attr('data-originalvalue'));
        });

        $form
            .on('change', 'div[data-datafield="AvailFor"]', function () {
                if (FwFormField.getValueByDataField($form, 'RepairType') === 'OWNED') {
                    if (FwFormField.getValue($form, 'div[data-datafield="AvailFor"]') === 'S') {
                        $form.find('.icodesales').show();
                        $form.find('.icoderental').hide();
                    } else {
                        $form.find('.icodesales').hide();
                        $form.find('.icoderental').show();
                    }
                }
            })
            .on('change', 'div[data-datafield="RepairType"]', function () {
                if (FwFormField.getValueByDataField($form, 'RepairType') === 'OUTSIDE') {
                    $form.find('.itemid').hide();
                    FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
                } else {
                    $form.find('.itemid').show();
                    FwFormField.enable($form.find('div[data-datafield="AvailFor"]'));
                }
            })
        ;
    }
    //----------------------------------------------------------------------------------------------
    estimateOrder($form: JQuery): void {
        if ($form.data('hasCompleted') === true) {
            var $confirmation = FwConfirmation.renderConfirmation('Estimate', 'This Repair Order has already been completed and cannot be unestimated.');
            var $no           = FwConfirmation.addButton($confirmation, 'OK');
        } else if ($form.data('hasEstimated') === true) {
            var $confirmation = FwConfirmation.renderConfirmation('Estimate', 'Cancel this estimate for this Repair Order?');
            var $yes          = FwConfirmation.addButton($confirmation, 'Cancel Estimate', false);
            var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', function cancelEstimate() {
                $form.data('hasEstimated', false)
                FwFormField.disable($yes);
                $yes.text('Canceling Estimate...');
                $yes.off('click');

                const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
                FwAppData.apiMethod(true, 'POST', `api/v1/repair/estimate/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Estimate Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                }, function onError(response) {
                    $yes.on('click', cancelEstimate);
                    $yes.text('Cancel Estimate');
                    FwFunc.showError(response);
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form, RepairController);
                }, $form);
            });
        } else {
            var $confirmation = FwConfirmation.renderConfirmation('Estimate', 'Make an estimate for this Repair Order?');
            var $yes          = FwConfirmation.addButton($confirmation, 'Estimate', false);
            var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', function makeEstimate() {
                $form.data('hasEstimated', true);
                FwFormField.disable($yes);
                $yes.text('Estimating...');
                $yes.off('click');

                const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
                FwAppData.apiMethod(true, 'POST', `api/v1/repair/estimate/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Estimated');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                }, function onError(response) {
                    $yes.on('click', makeEstimate);
                    $yes.text('Estimate');
                    FwFunc.showError(response);
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form, RepairController);
                }, $form);
            });
        }
    };
    //----------------------------------------------------------------------------------------------
    completeOrder($form: JQuery): void {
        if ($form.data('hasCompleted') === true) {
            var $confirmation = FwConfirmation.renderConfirmation('Complete', 'This Repair Order has already been completed.');
            var $no           = FwConfirmation.addButton($confirmation, 'OK');
        } else if ($form.data('hasEstimated') === true) {
            var $confirmation = FwConfirmation.renderConfirmation('Complete', 'Complete this Repair Order?');
            var $yes          = FwConfirmation.addButton($confirmation, 'Complete', false);
            var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeComplete);
        } else {
            var $confirmation = FwConfirmation.renderConfirmation('Complete', 'Not yet estimated. Make estimate and complete this Repair Order?');
            var $yes          = FwConfirmation.addButton($confirmation, 'Complete', false);
            var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeComplete);
        }

        function makeComplete() {
            FwFormField.disable($yes);
            $yes.text('Completing...');
            $yes.off('click');

            const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
            FwAppData.apiMethod(true, 'POST', `api/v1/repair/complete/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Completed');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, RepairController);
                $form.data('hasCompleted', true);
            }, function onError(response) {
                $yes.on('click', makeComplete);
                $yes.text('Complete');
                FwFunc.showError(response);
                FwFormField.enable($yes);
                FwModule.refreshForm($form, RepairController);
                $form.data('hasCompleted', true);
            }, $form);
        };
    };
    //----------------------------------------------------------------------------------------------
    voidOrder($form: JQuery): void {
        const $confirmation = FwConfirmation.renderConfirmation('Void', 'Void this Repair Order?');
        const $yes          = FwConfirmation.addButton($confirmation, 'Void', false);
        const $no           = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', function makeVoid() {
            FwFormField.disable($yes);
            $yes.text('Voiding...');
            $yes.off('click');

            const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
            FwAppData.apiMethod(true, 'POST', `api/v1/repair/void/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Voided');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, RepairController);
            }, function onError(response) {
                $yes.on('click', makeVoid);
                $yes.text('Void');
                FwFunc.showError(response);
                FwFormField.enable($yes);
                FwModule.refreshForm($form, RepairController);
            }, $form);
        });
    };
    //----------------------------------------------------------------------------------------------
    releaseItems($form: JQuery): void {
        const releasedQuantityForm = +FwFormField.getValueByDataField($form, 'ReleasedQuantity');
        const quantityForm         = +FwFormField.getValueByDataField($form, 'Quantity');
        const totalQuantity        = quantityForm - releasedQuantityForm;

        if (quantityForm > releasedQuantityForm && quantityForm > 0) {
            var $confirmation = FwConfirmation.renderConfirmation('Release Items', '');
            var $yes          = FwConfirmation.addButton($confirmation, 'Release', false);
            var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="width:90px;float:left;"></div>');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ItemDescription" data-enabled="false" style="width:340px; float:left;"></div>');
            html.push('  </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" data-enabled="false" style="width:75px; float:left;"></div>');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Released" data-datafield="Released" data-enabled="false" style="width:75px;float:left;"></div>');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity to Release" data-datafield="ReleasedQuantity" data-enabled="true" style="width:75px;float:left;"></div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            var ICode             = FwFormField.getValueByDataField($form, 'InventoryId');
            var ItemDescription   = FwFormField.getValueByDataField($form, 'ItemDescription');
            var Quantity          = +FwFormField.getValueByDataField($form, 'Quantity');
            var ReleasedQuantity  = +FwFormField.getValueByDataField($form, 'ReleasedQuantity');
            var QuantityToRelease = Quantity - ReleasedQuantity;

            FwFormField.setValue($confirmation, 'div[data-datafield="ICode"]', ICode);
            FwFormField.setValue($confirmation, 'div[data-datafield="ItemDescription"]', ItemDescription);
            FwFormField.setValue($confirmation, 'div[data-datafield="Quantity"]', Quantity);
            FwFormField.setValue($confirmation, 'div[data-datafield="Released"]', ReleasedQuantity);
            FwFormField.setValue($confirmation, 'div[data-datafield="ReleasedQuantity"]', QuantityToRelease);

            $yes.on('click', function release() {
                const releasedQuantityConfirmation = +FwFormField.getValueByDataField($confirmation, 'ReleasedQuantity');

                if (releasedQuantityConfirmation <= totalQuantity) {
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Releasing...');
                    $yes.off('click');

                    const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
                    FwAppData.apiMethod(true, 'POST', `api/v1/repair/releaseitems/${RepairId}/${releasedQuantityConfirmation}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Items Successfully Released');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwModule.refreshForm($form, RepairController);
                    }, function onError(response) {
                        $yes.on('click', release);
                        $yes.text('Release');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                    }, $form);
                } else {
                    FwFunc.showError("You are attempting to release more items than are available.");
                }
            });
        } else {
            var $confirmation = FwConfirmation.renderConfirmation('Release Items', 'There are either no items to release or number chosen is greater than items available.');
            var $ok           = FwConfirmation.addButton($confirmation, 'OK');
        }
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'AssetValidation':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid
                };
                break;
            case 'RentalInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY',
                };
                break;
            case 'SalesInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY',
                };
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
}

//------------------------------------------------------------------------------------------------
// Complete
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.Complete.id] = function (event) {
    const $form = jQuery(this).closest('.fwform');
    try {
        RepairController.completeOrder($form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
// Estimate
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.Estimate.id] = function (event) {
    const $form = jQuery(this).closest('.fwform');
    try {
        RepairController.estimateOrder($form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
// Void
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.Void.id] = function (event) {
    const $form = jQuery(this).closest('.fwform');
    try {
        RepairController.voidOrder($form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
// Release
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.ReleaseItems.id] = function (event) {
    const $form = jQuery(this).closest('.fwform');
    try {
        RepairController.releaseItems($form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
// Void - Browse
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.browse.menuItems.Void.id] = function (event) {
    try {
        const $browse  = jQuery(this).closest('.fwbrowse');
        const RepairId = $browse.find('.selected [data-browsedatafield="RepairId"]').attr('data-originalvalue');
        if (RepairId != null) {
            const $confirmation = FwConfirmation.renderConfirmation('Void', 'Void this Repair Order?');
            const $yes          = FwConfirmation.addButton($confirmation, 'Void', false);
            const $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', function makeVoid() {
                FwFormField.disable($yes);
                $yes.text('Voiding...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/repair/void/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response);
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            });
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Repair Order to void.');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//------------------------------------------------------------------------------------------------

var RepairController = new Repair();