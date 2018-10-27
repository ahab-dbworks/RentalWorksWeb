routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });
//----------------------------------------------------------------------------------------------
class Order extends OrderBase {
    Module = 'Order';
    apiurl: string = 'api/v1/order';
    caption: string = 'Order';
    nav: string = 'module/order';
    id: string = '64C46F51-5E00-48FA-94B6-FC4EF53FEA20';
    lossDamageSessionId: string = '';
    successSoundFileName: string;
    errorSoundFileName: string;
    //-----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Order', false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
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
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });

        var location = JSON.parse(sessionStorage.getItem('location'));
        self.ActiveView = 'LocationId=' + location.locationid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'On Hold', '#EA300F');
        FwBrowse.addLegend($browse, 'No Charge', '#FF8040');
        FwBrowse.addLegend($browse, 'Late', '#FFB3D9');
        FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
        FwBrowse.addLegend($browse, 'Multi-Warehouse', '#D6E180');
        FwBrowse.addLegend($browse, 'Repair', '#5EAEAE');
        FwBrowse.addLegend($browse, 'L&D', '#400040');

        var department = JSON.parse(sessionStorage.getItem('department'));;
        var location = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All', true);
        var $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false);
        var $active = FwMenu.generateDropDownViewBtn('Active', false);
        var $hold = FwMenu.generateDropDownViewBtn('Hold', false);
        var $complete = FwMenu.generateDropDownViewBtn('Complete', false);
        var $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false);
        var $closed = FwMenu.generateDropDownViewBtn('Closed', false);
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $confirmed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONFIRMED';
            FwBrowse.search($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.search($browse);
        });
        $hold.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'HOLD';
            FwBrowse.search($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.search($browse);
        });
        $cancelled.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CANCELLED';
            FwBrowse.search($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.search($browse);
        });
        var viewSubitems = [];
        viewSubitems.push($all);
        viewSubitems.push($confirmed);
        viewSubitems.push($active);
        viewSubitems.push($hold);
        viewSubitems.push($complete);
        viewSubitems.push($cancelled);
        viewSubitems.push($closed);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        //Location Filter
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        var $form, $submodulePickListBrowse, $submoduleContractBrowse;
        var self = this;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);

        $submoduleContractBrowse = this.openContractBrowse($form);
        $form.find('.contract').append($submoduleContractBrowse);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            $form.find('.OrderId').attr('data-hasBeenCanceled', 'false');
            $form.find('.combinedtab').hide();
            $form.data('data-hasBeenCanceled', false)
            var today = FwFunc.getDate();
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);

            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
            FwFormField.setValueByDataField($form, 'BillingMonths', '0');

            $form.find('div[data-datafield="PickTime"]').attr('data-required', false);
            $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
            $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            $form.find('div[data-datafield="PendingPo"] input').prop('checked', true);
            $form.find('div[data-datafield="Rental"] input').prop('checked', true);
            $form.find('div[data-datafield="Sales"] input').prop('checked', true);
            $form.find('div[data-datafield="Miscellaneous"] input').prop('checked', true);
            $form.find('div[data-datafield="Labor"] input').prop('checked', true);
            $form.find('[data-type="tab"][data-caption="Loss and Damage"]').hide();
            FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));

            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
            $form.find('[data-type="tab"][data-caption="Used Sale"]').hide();
        };

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

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

        FwFormField.loadItems($form.find('.online'), [
            { value: 'PARTIAL', text: 'Partial' },
            { value: 'COMPLETE', text: 'Complete' }
        ], true);

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
            FwFormField.setValue($form, 'div[data-datafield="RateType"]', parentModuleInfo.RateTypeId, parentModuleInfo.RateType);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', parentModuleInfo.BillingCycleId, parentModuleInfo.BillingCycle);
        }

        this.events($form);
        this.getSoundUrls($form);
        this.activityCheckboxEvents($form, mode);
        return $form;
    };

    //----------------------------------------------------------------------------------------------
    openPickListBrowse($form) {
        var $browse;
        $browse = PickListController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.ActiveView = PickListController.ActiveView;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        var $browse;
        $browse = ContractController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.ActiveView = ContractController.ActiveView;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        var $orderPickListGrid;
        var $orderPickListGridControl;
        var max = 9999;
        var self = this;

        $orderPickListGrid = $form.find('div[data-grid="OrderPickListGrid"]');
        $orderPickListGridControl = jQuery(jQuery('#tmpl-grids-OrderPickListGridBrowse').html());
        $orderPickListGrid.empty().append($orderPickListGridControl);
        $orderPickListGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderPickListGridControl);
        FwBrowse.renderRuntimeHtml($orderPickListGridControl);

        var $orderStatusHistoryGrid;
        var $orderStatusHistoryGridControl;
        $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);

        var $orderSnapshotGrid;
        var $orderSnapshotGridControl;
        $orderSnapshotGrid = $form.find('div[data-grid="OrderSnapshotGrid"]');
        $orderSnapshotGridControl = jQuery(jQuery('#tmpl-grids-OrderSnapshotGridBrowse').html());
        $orderSnapshotGrid.empty().append($orderSnapshotGridControl);
        $orderSnapshotGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderSnapshotGridControl);
        FwBrowse.renderRuntimeHtml($orderSnapshotGridControl);

        var $orderItemGridRental;
        var $orderItemGridRentalControl;
        $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('isSummary', false);
        $orderItemGridRental.addClass('R');

        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'R'
            };
            request.pagesize = max;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'R';
        }
        );
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'rental');

            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });

        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        var $orderItemGridSales;
        var $orderItemGridSalesControl;
        $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSales.addClass('S');
        $orderItemGridSalesControl.data('isSummary', false);

        $orderItemGridSalesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'S'
            };
            request.pagesize = max;
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sales');

            let salesItems = $form.find('.salesgrid tbody').children();
            salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        });

        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);

        var $orderItemGridLabor;
        var $orderItemGridLaborControl;
        $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLabor.addClass('L');
        $orderItemGridLaborControl.data('isSummary', false);

        $orderItemGridLaborControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'L'
            };
            request.pagesize = max;
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'labor');

            let laborItems = $form.find('.laborgrid tbody').children();
            laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        });

        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);

        var $orderItemGridMisc;
        var $orderItemGridMiscControl;
        $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMisc.addClass('M');
        $orderItemGridMiscControl.data('isSummary', false);

        $orderItemGridMiscControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'M'
            };
            request.pagesize = max;
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'M';
        }
        );

        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'misc');

            let miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });

        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);

        var $orderItemGridUsedSale;
        var $orderItemGridUsedSaleControl;
        $orderItemGridUsedSale = $form.find('.usedsalegrid div[data-grid="OrderItemGrid"]');
        $orderItemGridUsedSaleControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridUsedSale.empty().append($orderItemGridUsedSaleControl);
        $orderItemGridUsedSale.addClass('RS');
        $orderItemGridUsedSaleControl.data('isSummary', false);

        $orderItemGridUsedSaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'RS'
            };
            request.pagesize = max;
        });
        $orderItemGridUsedSaleControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'RS';
        });
        FwBrowse.init($orderItemGridUsedSaleControl);
        FwBrowse.renderRuntimeHtml($orderItemGridUsedSaleControl);

        var $combinedOrderItemGrid;
        var $combinedOrderItemGridControl;
        $combinedOrderItemGrid = $form.find('.combinedgrid div[data-grid="OrderItemGrid"]');
        $combinedOrderItemGridControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $combinedOrderItemGridControl.find('.combined').attr('data-visible', 'true');
        $combinedOrderItemGridControl.find('.individual').attr('data-visible', 'false');
        $combinedOrderItemGrid.empty().append($combinedOrderItemGridControl);
        $combinedOrderItemGrid.addClass('A');
        $combinedOrderItemGridControl.data('isSummary', false);

        $combinedOrderItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
            request.pagesize = max;
        });
        $combinedOrderItemGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        }
        );
        FwBrowse.addEventHandler($combinedOrderItemGridControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'combined');
        });

        FwBrowse.init($combinedOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($combinedOrderItemGridControl);

        var $orderItemGridLossDamage;
        var $orderItemGridLossDamageControl;
        $orderItemGridLossDamage = $form.find('.lossdamagegrid div[data-grid="OrderItemGrid"]');
        $orderItemGridLossDamageControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
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
            request.pagesize = max;
        });
        $orderItemGridLossDamageControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'F';
        }
        );
        FwBrowse.addEventHandler($orderItemGridLossDamageControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'lossdamage');

            let lossDamageItems = $form.find('.lossdamagegrid tbody').children();
            lossDamageItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="LossAndDamage"]')) : FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        });

        FwBrowse.init($orderItemGridLossDamageControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLossDamageControl);

        var $orderNoteGrid;
        var $orderNoteGridControl;
        $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
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

        var $orderContactGrid;
        var $orderContactGridControl;
        $orderContactGrid = $form.find('div[data-grid="OrderContactGrid"]');
        $orderContactGridControl = jQuery(jQuery('#tmpl-grids-OrderContactGridBrowse').html());
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

        let itemGrids = [$orderItemGridRental, $orderItemGridSales, $orderItemGridLabor, $orderItemGridMisc];
        if ($form.attr('data-mode') === 'NEW') {
            for (var i = 0; i < itemGrids.length; i++) {                
                itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
                    .off()
                    .on('click', function() {
                        self.saveForm($form, { closetab: false });
                    })
            }
        }

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.usedsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        super.afterLoad($form);
        var status = FwFormField.getValueByDataField($form, 'Status');

        if (status === 'CLOSED' || status === 'CANCELLED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
        }

        var $orderPickListGrid = $form.find('[data-name="OrderPickListGrid"]');
        //FwBrowse.search($orderPickListGrid);
        var $orderStatusHistoryGrid;
        $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        //FwBrowse.search($orderStatusHistoryGrid);
        var $orderSnapshotGrid;
        $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');
        //FwBrowse.search($orderSnapshotGrid);
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        //FwBrowse.search($orderNoteGrid);
        var $orderContactGrid;
        $orderContactGrid = $form.find('[data-name="OrderContactGrid"]');
        //FwBrowse.search($orderContactGrid);
        var $allOrderItemGrid;
        $allOrderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($allOrderItemGrid);
        var $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridRental);
        var $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridSales);
        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLabor);
        var $orderItemGridMisc;
        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridMisc);
        var $orderItemGridUsedSale;
        $orderItemGridUsedSale = $form.find('.usedsalegrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridUsedSale);
        var $orderItemGridLossDamage;
        $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLossDamage);

        // Hides Add, Search, and Sub-Worksheet buttons on grid
        $orderItemGridLossDamage.find('.submenu-btn').filter('[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"], [data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridLossDamage.find('.buttonbar').hide();
        // Hides Loss and Damage menu item from non-LD grids
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridUsedSale.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();

        if (FwFormField.getValueByDataField($form, 'DisableEditingUsedSaleRate')) {
            $orderItemGridUsedSale.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingMiscellaneousRate')) {
            $orderItemGridMisc.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingLaborRate')) {
            $orderItemGridLabor.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingSalesRate')) {
            $orderItemGridSales.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingRentalRate')) {
            $orderItemGridRental.find('.rates').attr('data-formreadonly', true);
        }

        //var $pickListBrowse = $form.find('#PickListBrowse');
        //FwBrowse.search($pickListBrowse);

        //var $contractBrowse = $form.find('#ContractBrowse');
        //FwBrowse.search($contractBrowse);

        if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        if (FwFormField.getValueByDataField($form, 'HasSalesItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Sales'));
        }
        if (FwFormField.getValueByDataField($form, 'HasMiscellaneousItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Miscellaneous'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLaborItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Labor'));
        }
        if (FwFormField.getValueByDataField($form, 'HasRentalSaleItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'LossAndDamage'));
        }
        if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { $form.find('[data-type="tab"][data-caption="Loss And Damage"]').hide() }



        var rate = FwFormField.getValueByDataField($form, 'RateType');
        if (rate === '3WEEK') {
            $allOrderItemGrid.find('.3week').parent().show();
            $allOrderItemGrid.find('.weekextended').parent().hide();
            $allOrderItemGrid.find('.price').find('.caption').text('Week 1 Rate');
            $orderItemGridRental.find('.3week').parent().show();
            $orderItemGridRental.find('.weekextended').parent().hide();
            $orderItemGridRental.find('.price').find('.caption').text('Week 1 Rate');
        }
        // Display weeks or month field in billing tab
        if (rate === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        } else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }
        // Display D/W field in rental
        if (rate === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
            $form.find(".combineddw").show();
            $allOrderItemGrid.find('.dw').parent().show();
            $orderItemGridRental.find('.dw').parent().show();
            $orderItemGridLabor.find('.dw').parent().show();
            $orderItemGridMisc.find('.dw').parent().show();
        } else {
            $form.find(".RentalDaysPerWeek").hide();
        }

        this.renderFrames($form);
        this.dynamicColumns($form);
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        var noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }

        //// Display weeks or month field in billing tab | Commented out because code is redundant and repeated above (O.W. 10/15/18)
        //if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
        //    $form.find(".BillingWeeks").hide();
        //    $form.find(".BillingMonths").show();
        //} else {
        //    $form.find(".BillingMonths").hide();
        //    $form.find(".BillingWeeks").show();
        //}

        //// Display D/W field in rental
        //if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
        //    $form.find(".RentalDaysPerWeek").show();
        //} else {
        //    $form.find(".RentalDaysPerWeek").hide();
        //}
        // Disable withTax checkboxes if Total field is 0.00
        this.disableWithTaxCheckbox($form);

        let rentalActivity = FwFormField.getValueByDataField($form, 'Rental'),
            usedSaleActivity = FwFormField.getValueByDataField($form, 'RentalSale'),
            lossDamageActivity = FwFormField.getValueByDataField($form, 'LossAndDamage'),
            usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]');
        if (rentalActivity) {
            FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            usedSaleTab.hide();
        }
        if (lossDamageActivity) {
            $form.find('[data-type="tab"][data-caption="Loss and Damage"]').show();
        } else {
            $form.find('[data-type="tab"][data-caption="Loss and Damage"]').hide();
        }
        if (usedSaleActivity) {
            FwFormField.disable($form.find('[data-datafield="Rental"]'));
            usedSaleTab.show();
        }
    };
    //----------------------------------------------------------------------------------------------
    getSoundUrls = ($form): void => {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    cancelPickList(pickListId, pickListNumber, $form) {
        var $confirmation, $yes, $no;
        var orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', '<div style="white-space:pre;">\n' +
            'Cancel Pick List ' + pickListNumber + '?</div>');
        $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        $no = FwConfirmation.addButton($confirmation, 'No');
        $yes.on('click', function () {
            FwAppData.apiMethod(true, 'DELETE', 'api/v1/picklist/' + pickListId, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    var $pickListGridControl = $form.find('[data-name="OrderPickListGrid"]');
                    $pickListGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            OrderId: orderId
                        };
                    });
                    FwBrowse.search($pickListGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
    };
    //----------------------------------------------------------------------------------------------
    // Form menu item -- corresponding grid menu item function in OrderSnapshotGrid controller
    viewSnapshotOrder($form, event) {
        let $orderForm, $selectedCheckBoxes, $orderSnapshotGrid, snapshotId, orderNumber;

        $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
        $selectedCheckBoxes = $orderSnapshotGrid.find('.cbselectrow:checked');

        try {
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
                    orderNumber = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderNumber"]').attr('data-originalvalue');
                    var orderInfo: any = {};
                    orderInfo.OrderId = snapshotId;
                    $orderForm = OrderController.openForm('EDIT', orderInfo);
                    FwModule.openSubModuleTab($form, $orderForm);
                    jQuery('.tab.submodule.active').find('.caption').html(`Snapshot for Order ${orderNumber}`);
                }
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in Order Snapshot Grid in order to perform this function.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //----------------------------------------------------------------------------------------------
    addLossDamage($form: JQuery, event: any): void {
        let HTML: Array<string> = [], $popupHtml, $popup, $orderBrowse, sessionId, userWarehouseId, dealId, $lossAndDamageItemGridControl;
        userWarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        dealId = FwFormField.getValueByDataField($form, 'DealId');
        let errorSound, successSound;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        HTML.push(
            `<div id="searchpopup" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-caption="Loss and Damage">
              <div id="lossdamageform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss and Damage">
                      <div class="formrow">
                        <div class="formcolumn summaryview" style="width:100%;margin-top:50px;">
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
            let $browse;
            $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
            $browse.attr('data-hasmultirowselect', 'true');
            $browse.attr('data-type', 'Grid');
            $browse.attr('data-showsearch', 'false');
            FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
                if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                    $tr.css('color', '#aaaaaa');
                }
            });
            //var location = JSON.parse(sessionStorage.getItem('location'));
            //self.ActiveView = 'LocationId=' + location.locationid;

            //$browse.data('ondatabind', function (request) {
            //    request.activeview = self.ActiveView;
            //});
            $browse = FwModule.openBrowse($browse);

            $browse.data('ondatabind', function (request) {
                request.ActiveView = OrderController.ActiveView;
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
            let orderId, $selectedCheckBoxes: any, orderIds: string = '', request: any = {};
            $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
            if ($selectedCheckBoxes.length !== 0) { 
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                    if (orderId) {
                        orderIds = orderIds.concat(', ', orderId);
                    }
                }
                orderIds = orderIds.substring(2);

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
                        let $lossAndDamageItemGrid
                        $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
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
                }, null, null);
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in order to perform this function.');
            }
        }
        const events = () => {
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
                let request: any = {};
                request.SourceOrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.SessionId = this.lossDamageSessionId
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/completesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwPopup.destroyPopup($popup);
                        let $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
                        FwBrowse.search($orderItemGridLossDamage);
                    } else {
                        FwConfirmation.renderConfirmation('ERROR', 'Error')
                    }
                }, null, $popup);
            });
            // Select All
            $popup.find('.selectall').on('click', e => {
                let request: any = {};
                const orderId = FwFormField.getValueByDataField($form, 'OrderId');

                request.OrderId = orderId;
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        $popup.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                }, null);
            });
            // Select None
            $popup.find('.selectnone').on('click', e => {
                let request: any = {};
                const orderId = FwFormField.getValueByDataField($form, 'OrderId');

                request.OrderId = orderId;
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl);
                        $popup.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                }, null);
            });
            //Options button
            $popup.find('.options-button').on('click', e => {
                $popup.find('div.formrow.option-list').toggle();
            });
        }
        $popupHtml = HTML.join('');
        $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);
        $orderBrowse = addOrderBrowse();
        $popup.find('.container').append($orderBrowse);
        FwBrowse.search($orderBrowse);
        events();
    }
    //----------------------------------------------------------------------------------------------
    createSnapshotOrder($form: JQuery, event: any): void {
        let orderNumber, orderId, $orderSnapshotGrid;
        orderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');

        let $confirmation, $yes, $no;

        $confirmation = FwConfirmation.renderConfirmation('Create Snapshot', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        let html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Create a Snapshot for Order ${orderNumber}?</div>`);
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Create Snapshot', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', createSnapshot);
        let $confirmationbox = jQuery('.fwconfirmationbox');
        function createSnapshot() {
            FwAppData.apiMethod(true, 'POST', `api/v1/order/createsnapshot/${orderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Snapshot Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);

                $orderSnapshotGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                    }
                    request.pagesize = 10;
                    request.orderby = "OrderDate"
                });

                $orderSnapshotGrid.data('beforesave', request => {
                    request.OrderId = orderId;
                });

                FwBrowse.search($orderSnapshotGrid);
            }, null, $confirmationbox);
        }
    };

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
    }
};
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
FwApplicationTree.clickEvents['{AB1D12DC-40F6-4DF2-B405-54A0C73149EA}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.createSnapshotOrder($form, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{03000DCC-3D58-48EA-8BDF-A6D6B30668F5}'] = function (event) {
    //View Snapshot
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.viewSnapshotOrder($form, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

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
FwApplicationTree.clickEvents['{C6CC3D94-24CE-41C1-9B4F-B4F94A50CB48}'] = function (event) {
    var $form, pickListId, pickListNumber;
    $form = jQuery(this).closest('.fwform');
    pickListId = $form.find('tr.selected > td.column > [data-formdatafield="PickListId"]').attr('data-originalvalue');
    pickListNumber = $form.find('tr.selected > td.column > [data-formdatafield="PickListNumber"]').attr('data-originalvalue');
    try {
        OrderController.cancelPickList(pickListId, pickListNumber, $form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

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
FwApplicationTree.clickEvents['{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}'] = function (e) {
    let search, $form, orderId, $popup;
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
        $popup = search.renderSearchPopup($form, orderId, 'Order');
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
//OrderItemGrid Lock Selected
FwApplicationTree.clickEvents['{BC467EF9-F255-4F51-A6F2-57276D8824A3}'] = function (event) {
    let $browse, $form;

    $browse = jQuery(this).closest('.fwbrowse');
    $form = jQuery(this).closest('.fwform');

    try {
        if ($form.attr('data-controller') === 'OrderController') {
            OrderController.orderItemGridLockUnlock($browse, event);
        } else {
            QuoteController.orderItemGridLockUnlock($browse, event);
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//---------------------------------------------------------------------------------
//OrderItemGrid Bold Selected
FwApplicationTree.clickEvents['{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}'] = function (event) {
    let $browse, $form;

    $browse = jQuery(this).closest('.fwbrowse');
    $form = jQuery(this).closest('.fwform');

    try {
        if ($form.attr('data-controller') === 'OrderController') {
            OrderController.orderItemGridBoldUnbold($browse, event);
        } else {
            QuoteController.orderItemGridBoldUnbold($browse, event);
        }
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

//----------------------------------------------------------------------------------------------
var OrderController = new Order();