routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });

class Order extends OrderBase {
    Module:               string = 'Order';
    apiurl:               string = 'api/v1/order';
    caption:              string = 'Order';
    nav:                  string = 'module/order';
    id:                   string = '68B3710E-FE07-4461-9EFD-04E0DBDAF5EA';
    lossDamageSessionId:  string = '';
    successSoundFileName: string;
    errorSoundFileName:   string;
    ActiveViewFields:     any = {};
    ActiveViewFieldsId:   string;
    //-----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view      = FwModule.getModuleControl(`${this.Module}Controller`);

        const $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Order', false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse     = FwModule.openBrowse($browse);

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });

        $browse.data('ondatabind', request =>  {
            request.activeviewfields = this.ActiveViewFields;
        });

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        const department = JSON.parse(sessionStorage.getItem('department'));;
        const location   = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', `api/v1/departmentlocation/${department.departmentid}~${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultOrderType   = response.DefaultOrderType;
            this.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        const $all       = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false, "CONFIRMED");
        const $active    = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $hold      = FwMenu.generateDropDownViewBtn('Hold', false, "HOLD");
        const $complete  = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed    = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
      
        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $confirmed, $active, $hold, $complete, $cancelled, $closed);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location      = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        const $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);

        const $submoduleContractBrowse = this.openContractBrowse($form);
        $form.find('.contract').append($submoduleContractBrowse);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            $form.find('.OrderId').attr('data-hasBeenCanceled', 'false');
            $form.find('.combinedtab').hide();
            $form.data('data-hasBeenCanceled', false)

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);

            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);

            $form.find('div[data-datafield="PickTime"]').attr('data-required', 'false');

            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            $form.find('div[data-datafield="PendingPo"] input').prop('checked', true);
            $form.find('div[data-datafield="Rental"] input').prop('checked', true);
            $form.find('[data-type="tab"][data-caption="Loss and Damage"]').hide();
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));

            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
            $form.find('[data-type="tab"][data-caption="Used Sale"]').hide();
        };

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', 'false');
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', 'false');

        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver to Customer' },
            { value: 'SHIP', text: 'Ship to Customer' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);

        FwFormField.loadItems($form.find('.intype'), [
            { value: 'DELIVER', text: 'Customer Deliver' },
            { value: 'SHIP', text: 'Customer Ship' },
            { value: 'PICK UP', text: 'Pick Up from Customer' }
        ], true);

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
            FwFormField.setValue($form, 'div[data-datafield="RateType"]', parentModuleInfo.RateTypeId, parentModuleInfo.RateType);
        }

        const $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLossDamage);

        // Hides Add, Search, and Sub-Worksheet buttons on grid
        $orderItemGridLossDamage.find('.submenu-btn').filter('[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"], [data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridLossDamage.find('.buttonbar').hide();

        this.events($form);
        this.getSoundUrls($form);
        this.activityCheckboxEvents($form, mode);
        if (typeof parentModuleInfo !== 'undefined' && mode !== 'NEW') {
            this.renderFrames($form, parentModuleInfo.OrderId);
            this.dynamicColumns($form, parentModuleInfo.OrderTypeId);
        }

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    openPickListBrowse($form) {
        const $browse = PickListController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PickListController.ActiveViewFields;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        const $browse = ContractController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ContractController.ActiveViewFields;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        let $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        $form.find('.subPurchaseOrderSubModule').append($submodulePurchaseOrderBrowse);
        //let $submoduleInvoiceBrowse = this.openInvoiceBrowse($form);
        //$form.find('.invoiceSubModule').append($submoduleInvoiceBrowse);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openPurchaseOrderBrowse($form) {
        const $browse = PurchaseOrderController.openBrowse();
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PurchaseOrderController.ActiveViewFields;
            request.uniqueids = {
                OrderId: orderId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        const totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]
        // ----------
        const $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        const $orderStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('OrderStatusHistoryGrid');
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
        // ----------
        //const $orderSnapshotGrid = $form.find('div[data-grid="OrderSnapshotGrid"]');
        //const $orderSnapshotGridControl = FwBrowse.loadGridFromTemplate('OrderSnapshotGrid');
        //$orderSnapshotGrid.empty().append($orderSnapshotGridControl);
        //$orderSnapshotGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId')
        //    };
        //});
        //FwBrowse.init($orderSnapshotGridControl);
        //FwBrowse.renderRuntimeHtml($orderSnapshotGridControl);
        // ----------
        const $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('isSummary', false);
        $orderItemGridRental.addClass('R');

        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'R'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'R';
        }
        );
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', ($control, dt) => {
            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });

        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        // ----------
        const $orderItemGridLossDamage = $form.find('.lossdamagegrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridLossDamageControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridLossDamage.empty().append($orderItemGridLossDamageControl);
        $orderItemGridLossDamageControl.data('isSummary', false);
        $orderItemGridLossDamage.addClass('F');
        $orderItemGridLossDamage.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true'); 
        $orderItemGridLossDamage.find('div[data-datafield="Description"]').attr('data-formreadonly', 'true');
        $orderItemGridLossDamage.find('div[data-datafield="ItemId"]').attr('data-formreadonly', 'true');
        $orderItemGridLossDamage.find('div[data-datafield="Price"]').attr('data-digits', '3'); 
        $orderItemGridLossDamage.find('div[data-datafield="Price"]').attr('data-digitsoptional', 'false'); 

        $orderItemGridLossDamageControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'F'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridLossDamageControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'F';
        }
        );
        FwBrowse.addEventHandler($orderItemGridLossDamageControl, 'afterdatabindcallback', ($control, dt) => {
            let lossDamageItems = $form.find('.lossdamagegrid tbody').children();
            lossDamageItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="LossAndDamage"]')) : FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        });

        FwBrowse.init($orderItemGridLossDamageControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLossDamageControl);
        // ----------
        const $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        const $orderNoteGridControl = FwBrowse.loadGridFromTemplate('OrderNoteGrid');
        $orderNoteGrid.empty().append($orderNoteGridControl);
        $orderNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        $orderNoteGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);
        // ----------
        const $orderContactGrid = $form.find('div[data-grid="OrderContactGrid"]');
        const $orderContactGridControl = FwBrowse.loadGridFromTemplate('OrderContactGrid');
        $orderContactGrid.empty().append($orderContactGridControl);
        $orderContactGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        $orderContactGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($orderContactGridControl);
        FwBrowse.renderRuntimeHtml($orderContactGridControl);

        const itemGrids = [$orderItemGridRental];
        if ($form.attr('data-mode') === 'NEW') {
            for (let i = 0; i < itemGrids.length; i++) {                
                itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
                    .off()
                    .on('click', () => {
                        this.saveForm($form, { closetab: false });
                    })
            }
        }

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form) {
        const uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        let status = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'),
            lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]')

        if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { $form.find('[data-type="tab"][data-caption="Loss and Damage"]').hide() }
        if ($form.find('[data-datafield="CombineActivity"] input').val() === 'false') {
            // show / hide tabs
            if (!FwFormField.getValueByDataField($form, 'Rental')) { rentalTab.hide() }
            if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { lossDamageTab.hide(), FwFormField.disable($form.find('[data-datafield="Rental"]')); }
        }

        if (status === 'CLOSED' || status === 'CANCELLED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
        }

        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }

        var $orderStatusHistoryGrid  = $form.find('[data-name="OrderStatusHistoryGrid"]');
        var $orderSnapshotGrid       = $form.find('[data-name="OrderSnapshotGrid"]');
        var $orderNoteGrid           = $form.find('[data-name="OrderNoteGrid"]');
        var $orderContactGrid        = $form.find('[data-name="OrderContactGrid"]');
        var $orderItemGridRental     = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        var $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');

        // Hides Loss and Damage menu item from non-LD grids
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();

        if (FwFormField.getValueByDataField($form, 'DisableEditingRentalRate')) {
            $orderItemGridRental.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'LossAndDamage'));
        }
        if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { $form.find('[data-type="tab"][data-caption="Loss And Damage"]').hide() }

        var rate = FwFormField.getValueByDataField($form, 'RateType');
        if (rate === '3WEEK') {
            $orderItemGridRental.find('.3week').parent().show();
            $orderItemGridRental.find('.weekextended').parent().hide();
        }

        // Display D/W field in rental
        if (rate === 'DAILY') {
            $orderItemGridRental.find('.dw').parent().show();
        }

        super.afterLoad($form);
    };
    //----------------------------------------------------------------------------------------------
    getSoundUrls = ($form): void => {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    //cancelPickList(pickListId, pickListNumber, $form) {
    //    var $confirmation, $yes, $no;
    //    var orderId = FwFormField.getValueByDataField($form, 'OrderId');
    //    $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', '<div style="white-space:pre;">\n' +
    //        'Cancel Pick List ' + pickListNumber + '?</div>');
    //    $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
    //    $no = FwConfirmation.addButton($confirmation, 'No');
    //    $yes.on('click', function () {
    //        FwAppData.apiMethod(true, 'DELETE', 'api/v1/picklist/' + pickListId, {}, FwServices.defaultTimeout, function onSuccess(response) {
    //            try {
    //                FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
    //                FwConfirmation.destroyConfirmation($confirmation);
    //                var $pickListGridControl = $form.find('[data-name="OrderPickListGrid"]');
    //                $pickListGridControl.data('ondatabind', function (request) {
    //                    request.uniqueids = {
    //                        OrderId: orderId
    //                    };
    //                });
    //                FwBrowse.search($pickListGridControl);
    //            }
    //            catch (ex) {
    //                FwFunc.showError(ex);
    //            }
    //        }, null, $form);
    //    });
    //};
    //----------------------------------------------------------------------------------------------
    // Form menu item -- corresponding grid menu item function in OrderSnapshotGrid controller
    //viewSnapshotOrder($form, event) {
    //    let $orderForm, $selectedCheckBoxes, $orderSnapshotGrid, snapshotId, orderNumber;

    //    $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
    //    $selectedCheckBoxes = $orderSnapshotGrid.find('.cbselectrow:checked');

    //    try {
    //        if ($selectedCheckBoxes.length !== 0) {
    //            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
    //                snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
    //                orderNumber = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderNumber"]').attr('data-originalvalue');
    //                var orderInfo: any = {};
    //                orderInfo.OrderId = snapshotId;
    //                $orderForm = OrderController.openForm('EDIT', orderInfo);
    //                FwModule.openSubModuleTab($form, $orderForm);
    //                jQuery('.tab.submodule.active').find('.caption').html(`Snapshot for Order ${orderNumber}`);
    //            }
    //        } else {
    //            FwNotification.renderNotification('WARNING', 'Select rows in Order Snapshot Grid in order to perform this function.');
    //        }
    //    }
    //    catch (ex) {
    //        FwFunc.showError(ex);
    //    }
    //};
    //----------------------------------------------------------------------------------------------
    addLossDamage($form: JQuery, event: any): void {
        let sessionId, $lossAndDamageItemGridControl;
        const userWarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const errorSound = new Audio(this.errorSoundFileName);
        const successSound = new Audio(this.successSoundFileName);
        const HTML: Array<string> = [];
        HTML.push(
            `<div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form" data-caption="Loss and Damage">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss and Damage">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:50px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:20px;">Loss and Damage</div>
                            <div class="formrow error-msg"></div>
                            <div class="formrow sub-header" style="margin-left:8px;font-size:16px;"><span>Select one or more Orders with Lost or Damaged items, then click Continue</span></div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Continue</div>
                      </div>
                      <div class="formrow session-buttons" style="display:none;">
                        <div class="options-button fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                        <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                        <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                        <div class="complete-session fwformcontrol" data-type="button" style="float:right;">Add To Order</div>
                      </div>
                      <div class="formrow option-list" style="display:none;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Placeholder..." data-datafield=""></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
        );

        const addOrderBrowse = () => {
            var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
            $browse.attr('data-hasmultirowselect', 'true');
            $browse.attr('data-type', 'Browse');
            $browse.attr('data-showsearch', 'false');
            FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
                if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                    $tr.css('color', '#aaaaaa');
                }
            });

            $browse = FwModule.openBrowse($browse);
            $browse.find('.fwbrowse-menu').hide();

            $browse.data('ondatabind', function (request) {
                request.ActiveViewFields = OrderController.ActiveViewFields;
                request.pagesize = 15;
                request.orderby = 'OrderDate desc';
                request.miscfields = {
                    LossAndDamage: true,
                    LossAndDamageWarehouseId: userWarehouseId,
                    LossAndDamageDealId: dealId
                }
            });
            return $browse;
        }

        const startLDSession = (): void => {
            let $browse = jQuery($popup).children().find('.fwbrowse');
            let orderId, $selectedCheckBoxes: any, orderIds: string = '';
            $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                    if (orderId) {
                        orderIds = orderIds.concat(', ', orderId);
                    }
                }
                orderIds = orderIds.substring(2);

                const request: any = {};
                request.OrderIds = orderIds;
                request.DealId = dealId;
                request.WarehouseId = userWarehouseId;
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/startsession`, request, FwServices.defaultTimeout, response => {
                    sessionId = response.SessionId
                    this.lossDamageSessionId = sessionId;
                    if (sessionId) {
                        $popup.find('.container').html('<div class="formrow"><div data-control="FwGrid" data-grid="LossAndDamageItemGrid" data-securitycaption=""></div></div>');
                        $popup.find('.add-button').hide();
                        $popup.find('.sub-header').hide();
                        $popup.find('.session-buttons').show();
                        const $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                        $lossAndDamageItemGridControl = jQuery(jQuery('#tmpl-grids-LossAndDamageItemGridBrowse').html());
                        $lossAndDamageItemGrid.data('sessionId', sessionId);
                        $lossAndDamageItemGrid.data('orderId', orderId);
                        $lossAndDamageItemGrid.empty().append($lossAndDamageItemGridControl);
                        $lossAndDamageItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = {
                                SessionId: sessionId
                            };
                        });
                        FwBrowse.init($lossAndDamageItemGridControl);
                        FwBrowse.renderRuntimeHtml($lossAndDamageItemGridControl);

                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, null, $browse);
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in order to perform this function.');
            }
        }
        const events = () => {
            let $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            let gridContainer = $popup.find('.container');
            //Close the popup
            $popup.find('.close-modal').one('click', e => {
                FwPopup.destroyPopup($popup);
                jQuery(document).find('.fwpopup').off('click');
                jQuery(document).off('keydown');
            });
            // Starts LD session
            $popup.find('.select-items').on('click', event => {
                startLDSession();
            });
            // Complete Session
            $popup.find('.complete-session').on('click', event => {
            let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
            $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);
                let request: any = {};
                request.SourceOrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.SessionId = this.lossDamageSessionId
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/completesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwPopup.destroyPopup($popup);
                        FwBrowse.search($orderItemGridLossDamage);
                    } else {
                        FwNotification.renderNotification('ERROR', response.msg); //justin 01/31/2019
                    }
                }, null, $lossAndDamageItemGrid)
            });
            // Select All
            $popup.find('.selectall').on('click', e => {
                let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                const request: any = {};
                request.SessionId = this.lossDamageSessionId; //justin 01/31/2019
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        $popup.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
            });
            // Select None
            $popup.find('.selectnone').on('click', e => {
                let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                const request: any = {};
                request.SessionId = this.lossDamageSessionId; //justin 01/31/2019
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl);
                        $popup.find('div.error-msg').html(`<div><span">${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl); //justin 01/31/2019
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
            });
            //Options button
            $popup.find('.options-button').on('click', e => {
                $popup.find('div.formrow.option-list').toggle();
            });
        }
        const $popupHtml = HTML.join('');
        const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);
        const $orderBrowse = addOrderBrowse();
        $popup.find('.container').append($orderBrowse);
        FwBrowse.search($orderBrowse);
        events();
    }
    //----------------------------------------------------------------------------------------------
    retireLossDamage($form: JQuery): void {
        const $confirmation = FwConfirmation.renderConfirmation('Confirm?', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');

        const html: Array<string> = [];;
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Create a Lost Contract and Retire the Items?</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        const $yes = FwConfirmation.addButton($confirmation, 'Retire', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', retireLD);

        function retireLD() {
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');
            const request: any = {}
            request.OrderId = orderId;
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            FwFormField.disable($no);
            $yes.text('Retiring...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/retire`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    const uniqueids: any = {};
                    uniqueids.ContractId = response.ContractId;
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Retired');
                    FwConfirmation.destroyConfirmation($confirmation);
                    const $contractForm = ContractController.loadForm(uniqueids);
                    FwModule.openModuleTab($contractForm, "", true, 'FORM', true)
                    FwModule.refreshForm($form, OrderController);
                }
            }, function onError(response) {
                $yes.on('click', retireLD);
                $yes.text('Retire');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, OrderController);
                }, $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    //createSnapshotOrder($form: JQuery, event: any): void {
    //    let orderNumber, orderId, $orderSnapshotGrid;
    //    orderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
    //    orderId = FwFormField.getValueByDataField($form, 'OrderId');
    //    $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');

    //    let $confirmation, $yes, $no;

    //    $confirmation = FwConfirmation.renderConfirmation('Create Snapshot', '');
    //    $confirmation.find('.fwconfirmationbox').css('width', '450px');
    //    let html = [];
    //    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
    //    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
    //    html.push(`    <div>Create a Snapshot for Order ${orderNumber}?</div>`);
    //    html.push('  </div>');
    //    html.push('</div>');

    //    FwConfirmation.addControls($confirmation, html.join(''));

    //    $yes = FwConfirmation.addButton($confirmation, 'Create Snapshot', false);
    //    $no = FwConfirmation.addButton($confirmation, 'Cancel');

    //    $yes.on('click', createSnapshot);
    //    let $confirmationbox = jQuery('.fwconfirmationbox');
    //    function createSnapshot() {
    //        FwAppData.apiMethod(true, 'POST', `api/v1/order/createsnapshot/${orderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
    //            FwNotification.renderNotification('SUCCESS', 'Snapshot Successfully Created.');
    //            FwConfirmation.destroyConfirmation($confirmation);

    //            $orderSnapshotGrid.data('ondatabind', request => {
    //                request.uniqueids = {
    //                    OrderId: orderId,
    //                }
    //                request.pagesize = 10;
    //                request.orderby = "OrderDate"
    //            });

    //            $orderSnapshotGrid.data('beforesave', request => {
    //                request.OrderId = orderId;
    //            });

    //            FwBrowse.search($orderSnapshotGrid);
    //        }, null, $confirmationbox);
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    afterSave($form) {
        if (this.CombineActivity === 'true') {
            $form.find('.combined').css('display', 'block');
            $form.find('.combinedtab').css('display', 'flex');
            $form.find('.generaltab').click();
        } else {
            $form.find('.notcombined').css('display', 'block');
            $form.find('.notcombinedtab').css('display', 'flex');
            $form.find('.generaltab').click();
        }
        this.renderGrids($form);
        this.renderFrames($form, FwFormField.getValueByDataField($form, 'OrderId'));
        this.dynamicColumns($form, FwFormField.getValueByDataField($form, 'OrderTypeId'));
    }
    //----------------------------------------------------------------------------------------------
}

//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{427FCDFE-7E42-4081-A388-150D3D7FAE36}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    if ($form.attr('data-mode') !== 'NEW') {
        try {
            OrderController.addLossDamage($form, event);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    } else {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    }
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    if ($form.attr('data-mode') !== 'NEW') {
        try {
            OrderController.retireLossDamage($form);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    } else {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    }
};
//---------------------------------------------------------------------------------
//FwApplicationTree.clickEvents['{AB1D12DC-40F6-4DF2-B405-54A0C73149EA}'] = function (event) {
//    let $form;
//    $form = jQuery(this).closest('.fwform');

//    try {
//        OrderController.createSnapshotOrder($form, event);
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//---------------------------------------------------------------------------------
//FwApplicationTree.clickEvents['{03000DCC-3D58-48EA-8BDF-A6D6B30668F5}'] = function (event) {
//    //View Snapshot
//    let $form;
//    $form = jQuery(this).closest('.fwform');

//    try {
//        OrderController.viewSnapshotOrder($form, event);
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};

//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{91C9FD3E-ADEE-49CE-BB2D-F00101DFD93F}'] = function (event) {
    var $form, $pickListForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        $pickListForm = CreatePickListController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $pickListForm);
        jQuery('.tab.submodule.active').find('.caption').html('New Pick List');
        var $pickListUtilityGrid;
        $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Confirmation for cancelling Pick List
//FwApplicationTree.clickEvents['{C6CC3D94-24CE-41C1-9B4F-B4F94A50CB48}'] = function (event) {
//    var $form, pickListId, pickListNumber;
//    $form = jQuery(this).closest('.fwform');
//    pickListId = $form.find('tr.selected > td.column > [data-formdatafield="PickListId"]').attr('data-originalvalue');
//    pickListNumber = $form.find('tr.selected > td.column > [data-formdatafield="PickListNumber"]').attr('data-originalvalue');
//    try {
//        OrderController.cancelPickList(pickListId, pickListNumber, $form);
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{E25CB084-7E7F-4336-9512-36B7271AC151}'] = function (event) {
    var $form;
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.copyOrderOrQuote($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Form Cancel Option
FwApplicationTree.clickEvents['{6B644862-9030-4D42-A29B-30C8DAC29D3E}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.cancelUncancelOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{CF245A59-3336-42BC-8CCB-B88807A9D4EA}'] = function (e) {
    var $form, $orderStatusForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $orderStatusForm);
        jQuery('.tab.submodule.active').find('.caption').html('Order Status');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Check In Option
FwApplicationTree.clickEvents['{380318B6-7E4D-446D-A018-1EB7720F4338}'] = function (e) {
    var $form, $checkinForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        $checkinForm = CheckInController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $checkinForm);
        jQuery('.tab.submodule.active').find('.caption').html('Check-In');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{771DCE59-EB57-48B2-B189-177B414A4ED3}'] = function (event) {
    // Stage Item/ Check Out
    let $form, $stagingCheckoutForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderInfo.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        orderInfo.Warehouse = $form.find('div[data-datafield="WarehouseId"] input.fwformfield-text').val();
        orderInfo.DealId = FwFormField.getValueByDataField($form, 'DealId');
        orderInfo.Deal = $form.find('div[data-datafield="DealId"] input.fwformfield-text').val();
        $stagingCheckoutForm = StagingCheckoutController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $stagingCheckoutForm);
        jQuery('.tab.submodule.active').find('.caption').html('Staging / Check-Out');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Open Search Interface
FwApplicationTree.clickEvents['{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}'] = function (e) {
    let search, $form, orderId;
    $form = jQuery(this).closest('.fwform');
    orderId = FwFormField.getValueByDataField($form, 'OrderId');
    if ($form.attr('data-mode') === 'NEW') {
        OrderController.saveForm($form, { closetab: false });
        return;
    }
    if (orderId == "") {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    } else {
        search = new SearchInterface();
        search.renderSearchPopup($form, orderId, 'Order');
    }
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{F2FD2F4C-1AB7-4627-9DD5-1C8DB96C5509}'] = function (e) {
    var $form;
    try {
        $form = jQuery(this).closest('.fwform');
        $form.find('.print').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
//Browse Cancel Option
FwApplicationTree.clickEvents['{DAE6DC23-A2CA-4E36-8214-72351C4E1449}'] = function (event) {
    let $confirmation, $yes, $no, $browse, orderId, orderStatus;

    $browse = jQuery(this).closest('.fwbrowse');
    orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
    orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');

    try {
        if (orderId != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to un-cancel this Order?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelOrder);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to cancel this Order?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelOrder);
            }

            function cancelOrder() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Canceling...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', cancelOrder);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };

            function uncancelOrder() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Retrieving...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', uncancelOrder);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------

var OrderController = new Order();