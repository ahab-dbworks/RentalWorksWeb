routes.push({ pattern: /^module\/order$/, action: function (match) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });
var Order = (function () {
    function Order() {
        this.Module = 'Order';
        this.apiurl = 'api/v1/order';
        this.caption = 'Order';
        this.ActiveView = 'ALL';
    }
    Order.prototype.getModuleScreen = function (filter) {
        var self = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
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
    ;
    Order.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
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
        var department = JSON.parse(sessionStorage.getItem('department'));
        ;
        var location = JSON.parse(sessionStorage.getItem('location'));
        ;
        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);
        return $browse;
    };
    ;
    Order.prototype.addBrowseMenuItems = function ($menuObject) {
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
        viewLocation.push($all);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };
    ;
    Order.prototype.openForm = function (mode, parentModuleInfo) {
        var _this = this;
        var $form, $submodulePickListBrowse, $submoduleContractBrowse;
        var self = this;
        $form = jQuery(jQuery('#tmpl-modules-OrderForm').html());
        $form = FwModule.openForm($form, mode);
        $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);
        $submoduleContractBrowse = this.openContractBrowse($form);
        $form.find('.contract').append($submoduleContractBrowse);
        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            $form.find('.OrderId').attr('data-hasBeenCanceled', 'false');
            $form.data('data-hasBeenCanceled', false);
            var today = FwFunc.getDate();
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));
            var usersid = sessionStorage.getItem('usersid');
            var name_1 = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name_1);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name_1);
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
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
        }
        ;
        $form.find('[data-datafield="BillToAddressDifferentFromIssuedToAddress"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.differentaddress'));
            }
            else {
                FwFormField.disable($form.find('.differentaddress'));
            }
        });
        $form.find('div[data-datafield="OrderTypeId"]').data('onchange', function ($tr) {
            self.CombineActivity = $tr.find('.field[data-browsedatafield="CombineActivityTabs"]').attr('data-originalvalue');
            $form.find('[data-datafield="CombineActivity"] input').val(self.CombineActivity);
            var rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'), salesTab = $form.find('[data-type="tab"][data-caption="Sales"]'), miscTab = $form.find('[data-type="tab"][data-caption="Misc"]'), laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            var combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == "true") {
                $form.find('.notcombinedtab').hide();
                $form.find('.combinedtab').show();
            }
            else if (combineActivity == "false") {
                $form.find('.combinedtab').hide();
                $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
                $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
                $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
                $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
            }
        });
        $form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
            }
        });
        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));
        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            var type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'RateType', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Currency"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);
        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);
        FwFormField.loadItems($form.find('.intype'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);
        FwFormField.loadItems($form.find('.online'), [
            { value: 'PARTIAL', text: 'Partial' },
            { value: 'COMPLETE', text: 'Complete' }
        ], true);
        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
        }
        $form.find('.print').on('click', function (e) {
            var $report, orderNumber, orderId, recordTitle, printOrderTab;
            try {
                orderNumber = $form.find('div.fwformfield[data-datafield="OrderNumber"] input').val();
                orderId = $form.find('div.fwformfield[data-datafield="OrderId"] input').val();
                recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
                $report = RwPrintOrderController.openForm();
                FwModule.openSubModuleTab($form, $report);
                $report.find('.fwform-section[data-caption="Quote"]').css('display', 'none');
                $report.find('div.fwformfield[data-datafield="OrderId"] input').val(orderId);
                $report.find('div.fwformfield[data-datafield="OrderId"] .fwformfield-text').val(orderNumber);
                jQuery('.tab.submodule.active').find('.caption').html('Print Order');
                printOrderTab = jQuery('.tab.submodule.active');
                printOrderTab.find('.caption').html('Print Order');
                printOrderTab.attr('data-caption', 'Order ' + recordTitle);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $form.find('.allFrames').css('display', 'none');
        $form.find('.hideFrames').css('display', 'none');
        $form.find('.expandArrow').on('click', function (e) {
            $form.find('.hideFrames').toggle();
            $form.find('.expandFrames').toggle();
            $form.find('.allFrames').toggle();
            $form.find('.totalRowFrames').toggle();
            if ($form.find('.summarySection').css('flex') != '0 1 65%') {
                $form.find('.summarySection').css('flex', '0 1 65%');
            }
            else {
                $form.find('.summarySection').css('flex', '');
            }
        });
        $form.find('.copy').on('click', function (e) {
            var $confirmation, $yes, $no;
            $confirmation = FwConfirmation.renderConfirmation('Confirm Copy', '');
            var html = [];
            html.push('<div class="flexrow">Copy Outgoing Address into Incoming Address?</div>');
            FwConfirmation.addControls($confirmation, html.join(''));
            $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');
            $yes.on('click', copyAddress);
            var $confirmationbox = jQuery('.fwconfirmationbox');
            function copyAddress() {
                FwNotification.renderNotification('SUCCESS', 'Address Successfully Copied.');
                FwConfirmation.destroyConfirmation($confirmation);
                FwFormField.setValueByDataField($form, 'InDeliveryToLocation', FwFormField.getValueByDataField($form, 'OutDeliveryToLocation'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAttention', FwFormField.getValueByDataField($form, 'OutDeliveryToAttention'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAddress1', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress1'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAddress2', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress2'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCity', FwFormField.getValueByDataField($form, 'OutDeliveryToCity'));
                FwFormField.setValueByDataField($form, 'InDeliveryToState', FwFormField.getValueByDataField($form, 'OutDeliveryToState'));
                FwFormField.setValueByDataField($form, 'InDeliveryToZipCode', FwFormField.getValueByDataField($form, 'OutDeliveryToZipCode'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCountryId', FwFormField.getValueByDataField($form, 'OutDeliveryToCountryId'), FwFormField.getTextByDataField($form, 'OutDeliveryToCountryId'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCrossStreets', FwFormField.getValueByDataField($form, 'OutDeliveryToCrossStreets'));
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });
        $form.find(".weeklyType").show();
        $form.find(".monthlyType").hide();
        $form.find(".periodType input").prop('checked', true);
        $form.find(".totalType input").on('change', function (e) {
            var $target = jQuery(e.currentTarget), gridType = $target.parents('.totalType').attr('data-gridtype'), rateType = $target.val();
            switch (rateType) {
                case 'W':
                    $form.find('.' + gridType + 'AdjustmentsPeriod').hide();
                    $form.find('.' + gridType + 'AdjustmentsWeekly').show();
                    break;
                case 'M':
                    $form.find('.' + gridType + 'AdjustmentsPeriod').hide();
                    $form.find('.' + gridType + 'AdjustmentsMonthly').show();
                    break;
                case 'P':
                    $form.find('.' + gridType + 'AdjustmentsWeekly').hide();
                    $form.find('.' + gridType + 'AdjustmentsMonthly').hide();
                    $form.find('.' + gridType + 'AdjustmentsPeriod').show();
                    break;
            }
            _this.calculateOrderItemGridTotals($form, gridType);
        });
        this.events($form);
        return $form;
    };
    ;
    Order.prototype.openPickListBrowse = function ($form) {
        var $browse;
        $browse = PickListController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.ActiveView = PickListController.ActiveView;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            };
        });
        return $browse;
    };
    ;
    Order.prototype.openContractBrowse = function ($form) {
        var $browse;
        $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.ActiveView = ContractController.ActiveView;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            };
        });
        return $browse;
    };
    ;
    Order.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    ;
    Order.prototype.saveForm = function ($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    ;
    Order.prototype.renderGrids = function ($form) {
        var _this = this;
        var $orderPickListGrid;
        var $orderPickListGridControl;
        var max = 9999;
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
        });
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', function () {
            _this.calculateOrderItemGridTotals($form, 'rental');
            var rentalItems = $form.find('.rentalgrid tbody').children();
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
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', function () {
            _this.calculateOrderItemGridTotals($form, 'sales');
            var salesItems = $form.find('.salesgrid tbody').children();
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
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', function () {
            _this.calculateOrderItemGridTotals($form, 'labor');
            var laborItems = $form.find('.laborgrid tbody').children();
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
        });
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', function () {
            _this.calculateOrderItemGridTotals($form, 'misc');
            var miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });
        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);
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
        });
        FwBrowse.addEventHandler($combinedOrderItemGridControl, 'afterdatabindcallback', function () {
            _this.calculateOrderItemGridTotals($form, 'combined');
        });
        FwBrowse.init($combinedOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($combinedOrderItemGridControl);
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
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
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
    };
    ;
    Order.prototype.loadAudit = function ($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    };
    ;
    Order.prototype.renderFrames = function ($form) {
        var orderId;
        orderId = FwFormField.getValueByDataField($form, 'OrderId'),
            $form.find('.frame input').css('width', '100%');
        FwAppData.apiMethod(true, 'GET', "api/v1/ordersummary/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
            var key;
            for (key in response) {
                if (response.hasOwnProperty(key)) {
                    $form.find('[data-framedatafield="' + key + '"] input').val(response[key]);
                    $form.find('[data-framedatafield="' + key + '"]').attr('data-originalvalue', response[key]);
                }
            }
            var $profitFrames = $form.find('.profitframes .frame');
            $profitFrames.each(function () {
                var profit = parseFloat(jQuery(this).attr('data-originalvalue'));
                if (profit > 0) {
                    jQuery(this).find('input').css('background-color', '#A6D785');
                }
                else if (profit < 0) {
                    jQuery(this).find('input').css('background-color', '#ff9999');
                }
            });
            var $totalFrames = $form.find('.totalColors input');
            $totalFrames.each(function () {
                var total = jQuery(this).val();
                if (total != 0) {
                    jQuery(this).css('background-color', '#ffffe5');
                }
            });
        }, null, null);
        FwFormField.disable($form.find('.frame'));
        $form.find(".frame .add-on").children().hide();
    };
    ;
    Order.prototype.afterLoad = function ($form) {
        var $orderPickListGrid;
        $orderPickListGrid = $form.find('[data-name="OrderPickListGrid"]');
        FwBrowse.search($orderPickListGrid);
        var $orderStatusHistoryGrid;
        $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        FwBrowse.search($orderStatusHistoryGrid);
        var $orderSnapshotGrid;
        $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');
        FwBrowse.search($orderSnapshotGrid);
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);
        var $orderContactGrid;
        $orderContactGrid = $form.find('[data-name="OrderContactGrid"]');
        FwBrowse.search($orderContactGrid);
        var $allOrderItemGrid;
        $allOrderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($allOrderItemGrid);
        var $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridRental);
        var $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridSales);
        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridLabor);
        var $orderItemGridMisc;
        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridMisc);
        var $pickListBrowse = $form.find('#PickListBrowse');
        FwBrowse.search($pickListBrowse);
        var $contractBrowse = $form.find('#ContractBrowse');
        FwBrowse.search($contractBrowse);
        var rate = FwFormField.getValueByDataField($form, 'RateType');
        if (rate === '3WEEK') {
            $allOrderItemGrid.find('.3week').parent().show();
            $allOrderItemGrid.find('.weekextended').parent().hide();
            $allOrderItemGrid.find('.price').find('.caption').text('Week 1 Rate');
            $orderItemGridRental.find('.3week').parent().show();
            $orderItemGridRental.find('.weekextended').parent().hide();
            $orderItemGridRental.find('.price').find('.caption').text('Week 1 Rate');
        }
        if (rate === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        }
        else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }
        if (rate === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
            $allOrderItemGrid.find('.dw').parent().show();
            $orderItemGridRental.find('.dw').parent().show();
            $orderItemGridLabor.find('.dw').parent().show();
            $orderItemGridMisc.find('.dw').parent().show();
        }
        else {
            $form.find(".RentalDaysPerWeek").hide();
        }
        this.renderFrames($form);
        this.dynamicColumns($form);
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');
        var noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        }
        else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }
        if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        }
        else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }
        if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
        }
        else {
            $form.find(".RentalDaysPerWeek").hide();
        }
        this.disableWithTaxCheckbox($form);
        var rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'), salesTab = $form.find('[data-type="tab"][data-caption="Sales"]'), miscTab = $form.find('[data-type="tab"][data-caption="Misc"]'), laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
        $form.find('[data-datafield="Rental"] input').on('change', function (e) {
            var combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                }
                else {
                    rentalTab.hide();
                }
            }
        });
        $form.find('[data-datafield="Sales"] input').on('change', function (e) {
            var combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    salesTab.show();
                }
                else {
                    salesTab.hide();
                }
            }
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', function (e) {
            var combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    miscTab.show();
                }
                else {
                    miscTab.hide();
                }
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', function (e) {
            var combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    laborTab.show();
                }
                else {
                    laborTab.hide();
                }
            }
        });
    };
    ;
    Order.prototype.copyOrder = function ($form) {
        var $confirmation, $yes, $no;
        $confirmation = FwConfirmation.renderConfirmation('Copy Order', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        var html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="" style="width:90px;float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="" style="width:340px; float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="" style="width:90px; float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:340px;float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy To" data-datafield="">');
        html.push('      <div data-value="Q" data-caption="Quote"> </div>');
        html.push('    <div data-value="O" data-caption="Order"> </div></div><br>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Rates & Prices" data-datafield="CopyRatesFromInventory"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Subs" data-datafield="CombineSubs"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Documents" data-datafield="CopyDocuments"></div>');
        html.push('</div>');
        var copyConfirmation = html.join('');
        var orderId = FwFormField.getValueByDataField($form, 'OrderId');
        FwConfirmation.addControls($confirmation, html.join(''));
        var orderNumber, deal, description, dealId;
        $confirmation.find('div[data-caption="Type"] input').val(this.Module);
        orderNumber = FwFormField.getValueByDataField($form, this.Module + 'Number');
        $confirmation.find('div[data-caption="No"] input').val(orderNumber);
        deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="Deal"] input').val(deal);
        description = FwFormField.getValueByDataField($form, 'Description');
        $confirmation.find('div[data-caption="Description"] input').val(description);
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
        dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-value').val(dealId);
        FwFormField.disable($confirmation.find('div[data-caption="Type"]'));
        FwFormField.disable($confirmation.find('div[data-caption="No"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Deal"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Description"]'));
        $confirmation.find('div[data-datafield="CopyRatesFromInventory"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDates"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyLineItemNotes"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CombineSubs"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDocuments"] input').prop('checked', true);
        $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.on('click', makeACopy);
        function makeACopy() {
            var request = {};
            request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
            request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
            request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
            request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
            request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
            request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
            request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');
            if (request.CopyRatesFromInventory == "T") {
                request.CopyRatesFromInventory = "False";
            }
            ;
            for (var key in request) {
                if (request.hasOwnProperty(key)) {
                    if (request[key] == "T") {
                        request[key] = "True";
                    }
                    else if (request[key] == "F") {
                        request[key] = "False";
                    }
                }
            }
            ;
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');
            var $confirmationbox = jQuery('.fwconfirmationbox');
            FwAppData.apiMethod(true, 'POST', 'api/v1/Order/copy/' + orderId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Order Successfully Copied');
                FwConfirmation.destroyConfirmation($confirmation);
                var uniqueids = {};
                if (request.CopyToType == "O") {
                    uniqueids.OrderId = response.OrderId;
                    var $form = OrderController.loadForm(uniqueids);
                }
                else if (request.CopyToType == "Q") {
                    uniqueids.QuoteId = response.QuoteId;
                    var $form = QuoteController.loadForm(uniqueids);
                }
                FwModule.openModuleTab($form, "", true, 'FORM', true);
            }, function onError(response) {
                $yes.on('click', makeACopy);
                $yes.text('Copy');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
            }, $confirmationbox);
        }
        ;
    };
    ;
    Order.prototype.cancelPickList = function (pickListId, pickListNumber, $form) {
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
    ;
    Order.prototype.beforeValidateOutShipVia = function ($browse, $grid, request) {
        var validationName = request.module;
        var outDeliveryCarrierId = jQuery($grid.find('[data-datafield="OutDeliveryCarrierId"] input')).val();
        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: outDeliveryCarrierId
                };
                break;
        }
    };
    Order.prototype.beforeValidateInShipVia = function ($browse, $grid, request) {
        var validationName = request.module;
        var inDeliveryCarrierId = jQuery($grid.find('[data-datafield="InDeliveryCarrierId"] input')).val();
        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: inDeliveryCarrierId
                };
                break;
        }
    };
    Order.prototype.beforeValidateCarrier = function ($browse, $grid, request) {
        var validationName = request.module;
        switch (validationName) {
            case 'VendorValidation':
                request.uniqueids = {
                    Freight: true
                };
                break;
        }
    };
    Order.prototype.beforeValidate = function ($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        request.uniqueids = {
            LocationId: officeLocationId
        };
    };
    ;
    Order.prototype.beforeValidateMarketSegment = function ($browse, $grid, request) {
        var validationName = request.module;
        var marketTypeValue = jQuery($grid.find('[data-validationname="MarketTypeValidation"] input')).val();
        var marketSegmentValue = jQuery($grid.find('[data-validationname="MarketSegmentValidation"] input')).val();
        switch (validationName) {
            case 'MarketSegmentValidation':
                if (marketTypeValue !== "") {
                    request.uniqueids = {
                        MarketTypeId: marketTypeValue,
                    };
                    break;
                }
            case 'MarketSegmentJobValidation':
                if (marketSegmentValue !== "") {
                    request.uniqueids = {
                        MarketTypeId: marketTypeValue,
                        MarketSegmentId: marketSegmentValue,
                    };
                    break;
                }
        }
        ;
    };
    ;
    Order.prototype.events = function ($form) {
        var _this = this;
        var weeklyType = $form.find(".weeklyType");
        var monthlyType = $form.find(".monthlyType");
        var rentalDaysPerWeek = $form.find(".RentalDaysPerWeek");
        var billingMonths = $form.find(".BillingMonths");
        var billingWeeks = $form.find(".BillingWeeks");
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="MarketSegmentJobId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="MarketTypeId"]', $tr.find('.field[data-browsedatafield="MarketTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="MarketSegmentId"]', $tr.find('.field[data-browsedatafield="MarketSegmentId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketSegment"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="MarketSegmentId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="MarketTypeId"]', $tr.find('.field[data-browsedatafield="MarketTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="MarketSegmentJobId"]', $tr.find('.field[data-browsedatafield="MarketSegmentJobId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketSegmentJob"]').attr('data-originalvalue'));
        });
        $form.find('[data-datafield="MarketTypeId"] input').on('change', function (event) {
            FwFormField.setValueByDataField($form, 'MarketSegmentId', '');
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        $form.find('[data-datafield="MarketSegmentId"] input').on('change', function (event) {
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        $form.find('.bottom_line_total_tax').on('change', function (event) {
            _this.bottomLineTotalWithTaxChange($form, event);
        });
        $form.find('.bottom_line_discount').on('change', function (event) {
            _this.bottomLineDiscountChange($form, event);
        });
        $form.find('.RentalDaysPerWeek').on('change', '.fwformfield-text, .fwformfield-value', function (event) {
            var request = {};
            var $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            var orderId = FwFormField.getValueByDataField($form, 'OrderId');
            var daysperweek = FwFormField.getValueByDataField($form, 'RentalDaysPerWeek');
            request.DaysPerWeek = parseFloat(daysperweek);
            request.RecType = 'R';
            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', "api/v1/order/applybottomlinedaysperweek/", request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($orderItemGridRental);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
        $form.find('.RateType').on('change', function ($tr) {
            var rateType = FwFormField.getValueByDataField($form, 'RateType');
            switch (rateType) {
                case 'DAILY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    $form.find('.combinedgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.rentalgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.salesgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.laborgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.miscgrid [data-name="OrderItemGrid"]').parent().show();
                    break;
                case 'WEEKLY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case '3WEEK':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case 'MONTHLY':
                    weeklyType.hide();
                    monthlyType.show();
                    rentalDaysPerWeek.hide();
                    billingWeeks.hide();
                    billingMonths.show();
                    break;
                default:
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
            }
        });
        $form.find('[data-datafield="PendingPo"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
                FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
            }
            else {
                FwFormField.enable($form.find('[data-datafield="PoNumber"]'));
                FwFormField.enable($form.find('[data-datafield="PoAmount"]'));
            }
        });
        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
        });
        $form.find('.pick_date_validation').on('changeDate', function (event) {
            _this.checkDateRangeForPick($form, event);
        });
        $form.find('.billing_start_date').on('changeDate', function (event) {
            _this.adjustWeekorMonthBillingField($form, event);
        });
        $form.find('.billing_end_date').on('changeDate', function (event) {
            _this.adjustWeekorMonthBillingField($form, event);
        });
        $form.find('.week_or_month_field').on('change', function (event) {
            _this.adjustBillingEndDate($form, event);
        });
    };
    ;
    Order.prototype.adjustBillingEndDate = function ($form, event) {
        var newEndDate, daysToAdd, parsedBillingStartDate, daysBetweenDates, parsedBillingEndDate, monthValue, weeksValue, billingStartDate;
        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
        billingStartDate = FwFormField.getValueByDataField($form, 'BillingStartDate');
        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000;
        monthValue = FwFormField.getValueByDataField($form, 'BillingMonths');
        weeksValue = FwFormField.getValueByDataField($form, 'BillingWeeks');
        if (!isNaN(parsedBillingStartDate)) {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
                if (!isNaN(monthValue) && monthValue !== '0' && Math.sign(monthValue) !== -1 && Math.sign(monthValue) !== -0) {
                    FwAppData.apiMethod(true, 'GET', "api/v1/datefunctions/addmonths?Date=" + billingStartDate + "&Months=" + monthValue, null, FwServices.defaultTimeout, function onSuccess(response) {
                        newEndDate = FwFunc.getDate(response, -1);
                        FwFormField.setValueByDataField($form, 'BillingEndDate', newEndDate);
                        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000;
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
            }
            else {
                if (!isNaN(weeksValue) && weeksValue !== '0' && Math.sign(weeksValue) !== -1 && Math.sign(weeksValue) !== -0) {
                    daysToAdd = +(weeksValue * 7) - 1;
                    newEndDate = FwFunc.getDate(billingStartDate, daysToAdd);
                    FwFormField.setValueByDataField($form, 'BillingEndDate', newEndDate);
                    parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                    parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                    daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000;
                }
            }
        }
        if (!isNaN(daysBetweenDates)) {
            if (Math.sign(daysBetweenDates) >= 0) {
                $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
            }
            else {
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
                FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            }
        }
    };
    ;
    Order.prototype.adjustWeekorMonthBillingField = function ($form, event) {
        var monthValue, daysBetweenDates, billingStartDate, billingEndDate, weeksValue, parsedBillingStartDate, parsedBillingEndDate;
        billingStartDate = FwFormField.getValueByDataField($form, 'BillingStartDate');
        billingEndDate = FwFormField.getValueByDataField($form, 'BillingEndDate');
        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
        monthValue = FwFormField.getValueByDataField($form, 'BillingMonths');
        weeksValue = FwFormField.getValueByDataField($form, 'BillingWeeks');
        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000;
        if (!isNaN(parsedBillingStartDate)) {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
                monthValue = Math.ceil(daysBetweenDates / 31);
                if (!isNaN(monthValue) && monthValue !== '0' && Math.sign(monthValue) !== -1 && Math.sign(monthValue) !== -0) {
                    FwAppData.apiMethod(true, 'GET', "api/v1/datefunctions/numberofmonths?FromDate=" + billingStartDate + "&ToDate=" + billingEndDate, null, FwServices.defaultTimeout, function onSuccess(response) {
                        monthValue = response;
                        FwFormField.setValueByDataField($form, 'BillingMonths', monthValue);
                        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000;
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
                else if (daysBetweenDates === 0) {
                    FwFormField.setValueByDataField($form, 'BillingMonths', '0');
                }
            }
            else {
                weeksValue = Math.ceil(daysBetweenDates / 7);
                if (!isNaN(weeksValue) && weeksValue !== '0' && Math.sign(weeksValue) !== -1 && Math.sign(weeksValue) !== -0) {
                    FwFormField.setValueByDataField($form, 'BillingWeeks', weeksValue);
                    parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                    parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                    daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000;
                }
                else if (daysBetweenDates === 0) {
                    FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                }
            }
        }
        if (!isNaN(daysBetweenDates)) {
            if (Math.sign(daysBetweenDates) >= 0) {
                $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
            }
            else {
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
                FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            }
        }
    };
    ;
    Order.prototype.orderItemGridLockUnlock = function ($browse, event) {
        var $confirmation, $yes, $no, orderId, orderItemId, lockedStatus;
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        orderItemId = $browse.find('.selected [data-formdatafield="OrderItemId"]').attr('data-originalvalue');
        lockedStatus = $browse.find('.selected [data-formdatafield="Locked"]').attr('data-originalvalue');
        if (orderId != null) {
            if (lockedStatus === "true") {
                $confirmation = FwConfirmation.renderConfirmation('Unlock', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                var html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to unlock this item?</div>');
                html.push('  </div>');
                html.push('</div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Unlock Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.on('click', unlockItem);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                var html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to lock this Item?</div>');
                html.push('  </div>');
                html.push('</div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Lock Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.on('click', lockItem);
            }
        }
        else {
            throw new Error("Please select an Item to perform this action.");
        }
        function lockItem() {
            var request = {};
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Locking Item...');
            $yes.off('click');
            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Locked: true,
            };
            FwAppData.apiMethod(true, 'POST', "api/v1/orderitem", request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Item Successfully Locked');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', lockItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        }
        ;
        function unlockItem() {
            var request = {};
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Unlocking...');
            $yes.off('click');
            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Locked: false,
            };
            FwAppData.apiMethod(true, 'POST', "api/v1/orderitem", request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Item Successfully Unlocked');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', unlockItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        }
        ;
    };
    ;
    Order.prototype.orderItemGridBoldUnbold = function ($browse, event) {
        var $confirmation, $yes, $no, orderId, orderItemId, boldStatus;
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        orderItemId = $browse.find('.selected [data-formdatafield="OrderItemId"]').attr('data-originalvalue');
        boldStatus = $browse.find('.selected [data-formdatafield="Bold"]').attr('data-originalvalue');
        if (orderId != null) {
            if (boldStatus === "true") {
                $confirmation = FwConfirmation.renderConfirmation('Unbold', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                var html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to remove bold for this item?</div>');
                html.push('  </div>');
                html.push('</div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Unbold Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.on('click', unboldItem);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                var html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to bold this Item?</div>');
                html.push('  </div>');
                html.push('</div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Bold Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.on('click', boldItem);
            }
        }
        else {
            throw new Error("Please select an Item to perform this action.");
        }
        function boldItem() {
            var request = {};
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Bolding Item...');
            $yes.off('click');
            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Bold: true,
            };
            FwAppData.apiMethod(true, 'POST', "api/v1/orderitem", request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Success');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', boldItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        }
        ;
        function unboldItem() {
            var request = {};
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Removing...');
            $yes.off('click');
            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Bold: false,
            };
            FwAppData.apiMethod(true, 'POST', "api/v1/orderitem", request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Bold Removed');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', unboldItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        }
        ;
    };
    ;
    Order.prototype.checkDateRangeForPick = function ($form, event) {
        var $element, parsedPickDate, parsedFromDate, parsedToDate;
        $element = jQuery(event.currentTarget);
        parsedPickDate = Date.parse(FwFormField.getValueByDataField($form, 'PickDate'));
        parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStartDate'));
        parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStopDate'));
        if ($element.attr('data-datafield') === 'EstimatedStartDate' && parsedFromDate < parsedPickDate) {
            $form.find('div[data-datafield="EstimatedStartDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before 'Pick Date'.");
        }
        else if ($element.attr('data-datafield') === 'PickDate' && parsedFromDate < parsedPickDate) {
            $form.find('div[data-datafield="PickDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'From Date'.");
        }
        else if ($element.attr('data-datafield') === 'PickDate' && parsedToDate < parsedPickDate) {
            $form.find('div[data-datafield="PickDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'To Date'.");
        }
        else if (parsedToDate < parsedFromDate) {
            $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
        }
        else if (parsedToDate < parsedPickDate) {
            $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'Pick Date'.");
        }
        else {
            $form.find('div[data-datafield="PickDate"]').removeClass('error');
            $form.find('div[data-datafield="EstimatedStartDate"]').removeClass('error');
            $form.find('div[data-datafield="EstimatedStopDate"]').removeClass('error');
        }
    };
    ;
    Order.prototype.disableWithTaxCheckbox = function ($form) {
        if (FwFormField.getValueByDataField($form, 'PeriodRentalTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'SalesTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodLaborTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodMiscTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodCombinedTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
    };
    ;
    Order.prototype.bottomLineDiscountChange = function ($form, event) {
        var $element, $orderItemGrid, orderId, recType, discountPercent;
        var request = {};
        $element = jQuery(event.currentTarget);
        recType = $element.attr('data-rectype');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        discountPercent = $element.find('.fwformfield-value').val().slice(0, -1);
        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodRentalTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        }
        if (recType === 'S') {
            $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'SalesTotal', '');
            FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        }
        if (recType === 'L') {
            $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodLaborTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodMiscTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodCombinedTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
        request.DiscountPercent = parseFloat(discountPercent);
        request.RecType = recType;
        request.OrderId = orderId;
        FwAppData.apiMethod(true, 'POST', "api/v1/order/applybottomlinediscountpercent/", request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };
    ;
    Order.prototype.bottomLineTotalWithTaxChange = function ($form, event) {
        var $element, $orderItemGrid, recType, orderId, total, includeTaxInTotal, isWithTaxCheckbox, totalType;
        var request = {};
        $element = jQuery(event.currentTarget);
        isWithTaxCheckbox = $element.attr('data-type') === 'checkbox';
        recType = $element.attr('data-rectype');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.rentalOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.rentalTotalWithTax:visible');
            totalType = $form.find('.rentalgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'RentalDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.rentalTotalWithTax:visible'));
            }
            else {
                FwFormField.enable($form.find('.rentalTotalWithTax:visible'));
            }
        }
        if (recType === 'S') {
            $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.salesOrderItemTotal');
            includeTaxInTotal = FwFormField.getValue($form, '.salesTotalWithTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'SalesDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
            }
            else {
                FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
            }
        }
        if (recType === 'L') {
            $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.laborOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.laborTotalWithTax:visible');
            totalType = $form.find('.laborgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'LaborDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.laborTotalWithTax:visible'));
            }
            else {
                FwFormField.enable($form.find('.laborTotalWithTax:visible'));
            }
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.miscOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.miscTotalWithTax:visible');
            totalType = $form.find('.miscgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'MiscDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.miscTotalWithTax:visible'));
            }
            else {
                FwFormField.enable($form.find('.miscTotalWithTax:visible'));
            }
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.combinedOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.combinedTotalWithTax:visible');
            totalType = $form.find('.combinedgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'CombinedDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.combinedTotalWithTax:visible'));
            }
            else {
                FwFormField.enable($form.find('.combinedTotalWithTax:visible'));
            }
        }
        request.TotalType = totalType;
        request.IncludeTaxInTotal = includeTaxInTotal;
        request.RecType = recType;
        request.OrderId = orderId;
        request.Total = +total;
        FwAppData.apiMethod(true, 'POST', "api/v1/order/applybottomlinetotal/", request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };
    ;
    Order.prototype.toggleOrderItemView = function ($form, event) {
        var $element, $orderItemGrid, recType, isSummary, orderId;
        var request = {};
        $element = jQuery(event.currentTarget);
        recType = $element.parentsUntil('.flexrow').eq(9).attr('class');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        }
        if (recType === 'S') {
            $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        }
        if (recType === 'L') {
            $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid div[data-grid="OrderItemGrid"]');
        }
        if ($orderItemGrid.data('isSummary') === false) {
            isSummary = true;
            $orderItemGrid.data('isSummary', true);
            $element.children().text('Detail View');
        }
        else {
            isSummary = false;
            $orderItemGrid.data('isSummary', false);
            $element.children().text('Summary View');
        }
        $orderItemGrid.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId,
                Summary: isSummary,
                RecType: recType
            };
            request.pagesize = 9999;
            request.orderby = "RowNumber,RecTypeDisplay";
        });
        $orderItemGrid.data('beforesave', function (request) {
            request.OrderId = orderId;
            request.RecType = recType;
            request.Summary = isSummary;
        });
        FwBrowse.search($orderItemGrid);
    };
    ;
    Order.prototype.calculateOrderItemGridTotals = function ($form, gridType) {
        var subTotal, discount, salesTax, grossTotal, total, rateType;
        var extendedTotal = new Decimal(0);
        var discountTotal = new Decimal(0);
        var taxTotal = new Decimal(0);
        var rateValue = $form.find('.' + gridType + 'grid .totalType input:checked').val();
        switch (rateValue) {
            case 'W':
                rateType = 'Weekly';
                break;
            case 'P':
                rateType = 'Period';
                break;
            case 'M':
                rateType = 'Monthly';
                break;
        }
        var extendedColumn = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'Extended"]');
        var discountColumn = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'DiscountAmount"]');
        var taxColumn = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'Tax"]');
        for (var i = 1; i < extendedColumn.length; i++) {
            var inputValueFromExtended = +extendedColumn.eq(i).attr('data-originalvalue');
            extendedTotal = extendedTotal.plus(inputValueFromExtended);
            var inputValueFromDiscount = +discountColumn.eq(i).attr('data-originalvalue');
            discountTotal = discountTotal.plus(inputValueFromDiscount);
            var inputValueFromTax = +taxColumn.eq(i).attr('data-originalvalue');
            taxTotal = taxTotal.plus(inputValueFromTax);
        }
        ;
        subTotal = extendedTotal.toFixed(2);
        discount = discountTotal.toFixed(2);
        salesTax = taxTotal.toFixed(2);
        grossTotal = extendedTotal.plus(discountTotal).toFixed(2);
        total = taxTotal.plus(extendedTotal).toFixed(2);
        $form.find('.' + gridType + 'totals [data-totalfield="SubTotal"] input').val(subTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(discount);
        $form.find('.' + gridType + 'totals [data-totalfield="Tax"] input').val(salesTax);
        $form.find('.' + gridType + 'totals [data-totalfield="GrossTotal"] input').val(grossTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(total);
        $form.find('.' + gridType + 'Adjustments .' + gridType + 'OrderItemTotal:visible input').val(total);
    };
    ;
    Order.prototype.createSnapshotOrder = function ($form, event) {
        var orderNumber, orderId, $orderSnapshotGrid;
        orderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');
        var $confirmation, $yes, $no;
        $confirmation = FwConfirmation.renderConfirmation('Create Snapshot', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        var html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push("    <div>Create a snapshot for Order " + orderNumber + "?</div>");
        html.push('  </div>');
        html.push('</div>');
        FwConfirmation.addControls($confirmation, html.join(''));
        $yes = FwConfirmation.addButton($confirmation, 'Create Snapshot', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.on('click', createSnapshot);
        var $confirmationbox = jQuery('.fwconfirmationbox');
        function createSnapshot() {
            FwAppData.apiMethod(true, 'POST', "api/v1/order/createsnapshot/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Snapshot Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);
                $orderSnapshotGrid.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId,
                    };
                    request.pagesize = 10;
                    request.orderby = "OrderDescription";
                });
                $orderSnapshotGrid.data('beforesave', function (request) {
                    request.OrderId = orderId;
                });
                FwBrowse.search($orderSnapshotGrid);
            }, null, $confirmationbox);
        }
    };
    ;
    Order.prototype.cancelUncancelOrder = function ($form) {
        var $confirmation, $yes, $no, orderId, orderStatus, self;
        self = this;
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderStatus = FwFormField.getValueByDataField($form, 'Status');
        if (orderId != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                var html = [];
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
                var html = [];
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
        }
        else {
            throw new Error("Please select an Order to perform this action.");
        }
        function cancelOrder() {
            var request = {};
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Canceling...');
            $yes.off('click');
            FwAppData.apiMethod(true, 'POST', "api/v1/order/cancel/" + orderId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Order Successfully Canceled');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', cancelOrder);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
        }
        ;
        function uncancelOrder() {
            var request = {};
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Retrieving...');
            $yes.off('click');
            FwAppData.apiMethod(true, 'POST', "api/v1/order/uncancel/" + orderId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', uncancelOrder);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
        }
        ;
    };
    ;
    Order.prototype.dynamicColumns = function ($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"), $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'), $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'), $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'), $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'), fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'), fieldNames = [];
        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }
        FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
            $form.find('[data-datafield="CombineActivity"] input').val(response.CombineActivityTabs);
            var rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'), salesTab = $form.find('[data-type="tab"][data-caption="Sales"]'), miscTab = $form.find('[data-type="tab"][data-caption="Misc"]'), laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            if (response.CombineActivityTabs === false) {
                $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
                $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
                $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
                $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
            }
            if (response.CombineActivityTabs === true) {
                $form.find('.notcombined').css('display', 'none');
                $form.find('.notcombinedtab').css('display', 'none');
            }
            else {
                $form.find('.combined').css('display', 'none');
                $form.find('.combinedtab').css('display', 'none');
            }
            var hiddenRentals = fieldNames.filter(function (field) {
                return !this.has(field);
            }, new Set(response.RentalShowFields));
            var hiddenSales = fieldNames.filter(function (field) {
                return !this.has(field);
            }, new Set(response.SalesShowFields));
            var hiddenLabor = fieldNames.filter(function (field) {
                return !this.has(field);
            }, new Set(response.LaborShowFields));
            var hiddenMisc = fieldNames.filter(function (field) {
                return !this.has(field);
            }, new Set(response.MiscShowFields));
            for (var i = 0; i < hiddenRentals.length; i++) {
                jQuery($rentalGrid.find('[data-mappedfield="' + hiddenRentals[i] + '"]')).parent().hide();
            }
            for (var j = 0; j < hiddenSales.length; j++) {
                jQuery($salesGrid.find('[data-mappedfield="' + hiddenSales[j] + '"]')).parent().hide();
            }
            for (var k = 0; k < hiddenLabor.length; k++) {
                jQuery($laborGrid.find('[data-mappedfield="' + hiddenLabor[k] + '"]')).parent().hide();
            }
            for (var l = 0; l < hiddenMisc.length; l++) {
                jQuery($miscGrid.find('[data-mappedfield="' + hiddenMisc[l] + '"]')).parent().hide();
            }
            if (!hiddenRentals.includes('WeeklyExtended')) {
                $rentalGrid.find('.3weekextended').parent().show();
            }
        }, null, null);
    };
    ;
    Order.prototype.afterSave = function ($form) {
        if (this.CombineActivity === 'true') {
            $form.find('.combined').css('display', 'block');
            $form.find('.combinedtab').css('display', 'flex');
            $form.find('.generaltab').click();
        }
        else {
            $form.find('.notcombined').css('display', 'block');
            $form.find('.notcombinedtab').css('display', 'flex');
            $form.find('.generaltab').click();
        }
    };
    return Order;
}());
;
FwApplicationTree.clickEvents['{AB1D12DC-40F6-4DF2-B405-54A0C73149EA}'] = function (event) {
    var $form;
    $form = jQuery(this).closest('.fwform');
    try {
        OrderController.createSnapshotOrder($form, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{91C9FD3E-ADEE-49CE-BB2D-F00101DFD93F}'] = function (event) {
    var $form, $pickListForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo = {};
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
FwApplicationTree.clickEvents['{E25CB084-7E7F-4336-9512-36B7271AC151}'] = function (event) {
    var $form;
    $form = jQuery(this).closest('.fwform');
    try {
        OrderController.copyOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{6B644862-9030-4D42-A29B-30C8DAC29D3E}'] = function (event) {
    var $form;
    $form = jQuery(this).closest('.fwform');
    try {
        OrderController.cancelUncancelOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{CF245A59-3336-42BC-8CCB-B88807A9D4EA}'] = function (e) {
    var $form, $orderStatusForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo = {};
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
FwApplicationTree.clickEvents['{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}'] = function (e) {
    var search, $form, orderId, $popup;
    $form = jQuery(this).closest('.fwform');
    orderId = FwFormField.getValueByDataField($form, 'OrderId');
    if (orderId == "") {
        FwNotification.renderNotification('WARNING', 'Please save the record before performing this function');
    }
    else {
        search = new SearchInterface();
        $popup = search.renderSearchPopup($form, orderId, 'Order');
    }
};
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
FwApplicationTree.clickEvents['{BC467EF9-F255-4F51-A6F2-57276D8824A3}'] = function (event) {
    var $browse, $form;
    $browse = jQuery(this).closest('.fwbrowse');
    $form = jQuery(this).closest('.fwform');
    try {
        if ($form.attr('data-controller') === 'OrderController') {
            OrderController.orderItemGridLockUnlock($browse, event);
        }
        else {
            QuoteController.orderItemGridLockUnlock($browse, event);
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}'] = function (event) {
    var $browse, $form;
    $browse = jQuery(this).closest('.fwbrowse');
    $form = jQuery(this).closest('.fwform');
    try {
        if ($form.attr('data-controller') === 'OrderController') {
            OrderController.orderItemGridBoldUnbold($browse, event);
        }
        else {
            QuoteController.orderItemGridBoldUnbold($browse, event);
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{D27AD4E7-E924-47D1-AF6E-992B92F5A647}'] = function (event) {
    var $form;
    $form = jQuery(this).closest('.fwform');
    try {
        if ($form.attr('data-controller') === 'OrderController') {
            OrderController.toggleOrderItemView($form, event);
        }
        else {
            QuoteController.toggleOrderItemView($form, event);
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{DAE6DC23-A2CA-4E36-8214-72351C4E1449}'] = function (event) {
    var $confirmation, $yes, $no, $browse, orderId, orderStatus;
    $browse = jQuery(this).closest('.fwbrowse');
    orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
    orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
    try {
        if (orderId != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                var html = [];
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
                var html = [];
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
                var request = {};
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Canceling...');
                $yes.off('click');
                FwAppData.apiMethod(true, 'POST', "api/v1/order/cancel/" + orderId, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Canceled');
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
            }
            ;
            function uncancelOrder() {
                var request = {};
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Retrieving...');
                $yes.off('click');
                FwAppData.apiMethod(true, 'POST', "api/v1/order/uncancel/" + orderId, request, FwServices.defaultTimeout, function onSuccess(response) {
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
            }
            ;
        }
        else {
            throw new Error("Please select an Order to perform this action.");
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
var OrderController = new Order();
//# sourceMappingURL=Order.js.map