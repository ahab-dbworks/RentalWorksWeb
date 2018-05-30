/// <reference path="../deal/deal.ts" />
routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Order {
    Module: string = 'Order';
    apiurl: string = 'api/v1/order';
    caption: string = 'Order';
    ActiveView: string = 'ALL';
    DefaultOrderType: string;
    DefaultOrderTypeId: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var self = this;
        var screen: any = {};
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

    //----------------------------------------------------------------------------------------------
    openBrowse() {
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
        viewLocation.push($all);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        var $form, $submodulePickListBrowse, $submoduleContractBrowse;

        $form = jQuery(jQuery('#tmpl-modules-OrderForm').html());
        $form = FwModule.openForm($form, mode);

        $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);

        $submoduleContractBrowse = this.openContractBrowse($form);
        $form.find('.contract').append($submoduleContractBrowse);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
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
        //RateType change affecting billing tab weeks or months
        $form.find('.RateType').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
                $form.find(".BillingWeeks").hide();
                $form.find(".BillingMonths").show();
            } else {
                $form.find(".BillingMonths").hide();
                $form.find(".BillingWeeks").show();
            }
        });
        //RateType change affecting DaysPerWeek field in rental tab
        $form.find('.RateType').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
                $form.find(".RentalDaysPerWeek").show();
            } else {
                $form.find(".RentalDaysPerWeek").hide();
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

        $form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);

            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
            }
        });

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            var type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'RateType', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Currency"]').attr('data-originalvalue'));
        })

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
		
        if (typeof parentModuleInfo !== 'undefined') { 
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal); 
        } 

        $form.find('.print').on('click', e => {
            var $report, orderNumber, orderId;
            try {
                orderNumber = $form.find('div.fwformfield[data-datafield="OrderNumber"] input').val();
                orderId = $form.find('div.fwformfield[data-datafield="OrderId"] input').val();
                $report = RwPrintOrderController.openForm();
                FwModule.openSubModuleTab($form, $report);
                $report.find('.fwform-section[data-caption="Quote"]').css('display', 'none');
                $report.find('div.fwformfield[data-datafield="OrderId"] input').val(orderId);
                $report.find('div.fwformfield[data-datafield="OrderId"] .fwformfield-text').val(orderNumber);
                jQuery('.tab.submodule.active').find('.caption').html('Print Order');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

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

        var $orderItemGridRental;
        var $orderItemGridRentalControl;
        $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('rental_grid');
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
        }
        );
        FwBrowse.addEventHandler($combinedOrderItemGridControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'combined');
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

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
    };

    //----------------------------------------------------------------------------------------------
    beforeValidateOutShipVia($browse, $grid, request) {
        var validationName = request.module;
        var outDeliveryCarrierId = jQuery($grid.find('[data-datafield="OutDeliveryCarrierId"] input')).val();

        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: outDeliveryCarrierId
                };
                break;
        }
    }

    beforeValidateInShipVia($browse, $grid, request) {
        var validationName = request.module;
        var inDeliveryCarrierId = jQuery($grid.find('[data-datafield="InDeliveryCarrierId"] input')).val();

        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: inDeliveryCarrierId
                };
                break;
        }
    }

    beforeValidateCarrier($browse, $grid, request) {
        var validationName = request.module;

        switch (validationName) {
            case 'VendorValidation':
                request.uniqueids = {
                    Freight: true
                };
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        request.uniqueids = {
            LocationId: officeLocationId
        }
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    };

    //----------------------------------------------------------------------------------------------
    copyOrder($form) {
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

            var request: any = {};
            request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
            request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
            request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
            request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
            request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
            request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
            request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');

            if (request.CopyRatesFromInventory == "T") {
                request.CopyRatesFromInventory = "False"
            };

            for (var key in request) {
                if (request.hasOwnProperty(key)) {
                    if (request[key] == "T") {
                        request[key] = "True";
                    } else if (request[key] == "F") {
                        request[key] = "False";
                    }
                }
            };

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');
            var $confirmationbox = jQuery('.fwconfirmationbox');
            FwAppData.apiMethod(true, 'POST', 'api/v1/Order/copy/' + orderId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Order Successfully Copied');
                FwConfirmation.destroyConfirmation($confirmation);

                var uniqueids: any = {};
                if (request.CopyToType == "O") {
                    uniqueids.OrderId = response.OrderId;
                    var $form = OrderController.loadForm(uniqueids);
                } else if (request.CopyToType == "Q") {
                    uniqueids.QuoteId = response.QuoteId;
                    var $form = QuoteController.loadForm(uniqueids);
                }
                FwModule.openModuleTab($form, "", true, 'FORM', true)

            }, function onError(response) {
                $yes.on('click', makeACopy);
                $yes.text('Copy');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                }, $confirmationbox);
        };
    };

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
    renderFrames($form: any) {
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
                } else if (profit < 0) {
                    jQuery(this).find('input').css('background-color', '#ff9999');
                }

            });

            var $totalFrames = $form.find('.totalColors input');
            $totalFrames.each(function () {
                var total = jQuery(this).val();
                if (total != 0) {
                    jQuery(this).css('background-color', '#ffffe5');
                }
            })


        }, null, null);

        FwFormField.disable($form.find('.frame'));

        $form.find(".frame .add-on").children().hide();
    };

    //----------------------------------------------------------------------------------------------
    events($form: any) {

    }

    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        var $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        $orderItemGridRental.data('rental_grid');

        var $orderPickListGrid;
        $orderPickListGrid = $form.find('[data-name="OrderPickListGrid"]');
        FwBrowse.search($orderPickListGrid);
        var $orderStatusHistoryGrid;
        $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        FwBrowse.search($orderStatusHistoryGrid);        
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);
        var $orderContactGrid;
        $orderContactGrid = $form.find('[data-name="OrderContactGrid"]');
        FwBrowse.search($orderContactGrid);

        var $pickListBrowse = $form.find('#PickListBrowse');
        FwBrowse.search($pickListBrowse);

        var $contractBrowse = $form.find('#ContractBrowse');
        FwBrowse.search($contractBrowse);

        var $pending = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');
        if ($pending === true) {
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
        }
        else {
            FwFormField.enable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.enable($form.find('[data-datafield="PoAmount"]'));
        }

        this.renderFrames($form);
        this.dynamicColumns($form);
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        FwFormField.disable($form.find('[data-caption="Weeks"]'));

        var noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }

        // Display weeks or month field in billing tab
        if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        } else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }

        // Display D/W field in rental
        if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
        } else {
            $form.find(".RentalDaysPerWeek").hide();
        }

        // Bottom Line Total with Tax
        $form.find('.bottom_line_total_tax').on('change', event => {
            this.bottomLineTotalWithTaxChange($form, event);
        });

        // Bottom Line Discount
        $form.find('.bottom_line_discount').on('change', event => {
            this.bottomLineDiscountChange($form, event);
        });

        // Order Item Grid View
        $form.find('.order_item_view_select').on('change', event => {
            this.toggleOrderItemView($form, event);
        });

        // Disable withTax checkboxes if Total field is 0.00
        this.disableWithTaxCheckbox($form);
 
        // RentalDaysPerWeek for Rental OrderItemGrid
        $form.find('.RentalDaysPerWeek').on('change', '.fwformfield-text, .fwformfield-value', event => {
            let request: any = {};
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            let daysperweek = FwFormField.getValueByDataField($form, 'RentalDaysPerWeek');
         
            request.DaysPerWeek = parseFloat(daysperweek);
            request.RecType = 'R';
            request.OrderId = orderId;

            FwAppData.apiMethod(true, 'POST', `api/v1/order/applybottomlinedaysperweek/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($orderItemGridRental);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
    };

    //----------------------------------------------------------------------------------------------
    disableWithTaxCheckbox($form: any) {
        if (FwFormField.getValueByDataField($form, 'PeriodRentalTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'SalesTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodLaborTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodMiscTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodCombinedTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
    }

    //----------------------------------------------------------------------------------------------
    bottomLineDiscountChange($form: any, event: any) {
    // DiscountPercent for all OrderItemGrid
        let $element, $orderItemGrid, orderId, recType, discountPercent;
        let request: any = {};

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

        FwAppData.apiMethod(true, 'POST', `api/v1/order/applybottomlinediscountpercent/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };

    //----------------------------------------------------------------------------------------------
    bottomLineTotalWithTaxChange($form: any, event: any) {
    // Total and With Tax for all OrderItemGrid
        let $element, $orderItemGrid, recType, orderId, total, includeTaxInTotal, isWithTaxCheckbox;
        let request: any = {};

        $element = jQuery(event.currentTarget);

        isWithTaxCheckbox = $element.attr('data-type') === 'checkbox';
        recType = $element.attr('data-rectype');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');

            if (recType === 'R') {
                $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValueByDataField($form, 'PeriodRentalTotal');
                includeTaxInTotal = FwFormField.getValue($form, '.rentalTotalWithTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'RentalDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
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
                } else {
                    FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
                }
            }
            if (recType === 'L') {
                $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValue($form, '.laborOrderItemTotal');
                includeTaxInTotal = FwFormField.getValue($form, '.laborTotalWithTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'LaborDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
                }
            }
            if (recType === 'M') {
                $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValue($form, '.miscOrderItemTotal');
                includeTaxInTotal = FwFormField.getValue($form, '.miscTotalWithTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'MiscDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
                }
            }
            if (recType === '') {
                $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValue($form, '.combinedOrderItemTotal');
                includeTaxInTotal = FwFormField.getValue($form, '.combinedTotalWithTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'CombinedDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
                }
            }

        request.IncludeTaxInTotal = includeTaxInTotal;
        request.RecType = recType;
        request.OrderId = orderId;
        request.Total = +total;

        FwAppData.apiMethod(true, 'POST', `api/v1/order/applybottomlinetotal/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };

    //----------------------------------------------------------------------------------------------
    toggleOrderItemView($form: any, event: any) {
    // Toggle between Detail and Summary view in all OrderItemGrid
        let $element, $orderItemGrid, recType, isSummary, orderId;
        let request: any = {};

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
            $element.children().text('Detail View')
        }
        else {
            isSummary = false;
            $orderItemGrid.data('isSummary', false);
            $element.children().text('Summary View')
        }
        
        $orderItemGrid.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: orderId,
                Summary: isSummary,
                RecType: recType
            }
            request.pagesize = 9999;
            request.orderby = "RowNumber,RecTypeDisplay"
        });

        $orderItemGrid.data('beforesave', request => {
            request.OrderId = orderId;
            request.RecType = recType;
            request.Summary = isSummary;
        });

        FwBrowse.search($orderItemGrid);
    }

    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string) {
        let subTotal, discount, salesTax, grossTotal, total;
        let periodExtendedTotal = new Decimal(0);
        let periodDiscountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);
        const periodExtendedColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="PeriodExtended"]');
        const periodDiscountColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="PeriodDiscountAmount"]');
        const taxColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="Tax"]');

        for (let i = 1; i < periodExtendedColumn.length; i++) {
            // PeriodExtended Column
            let inputValueFromExtended: any = +periodExtendedColumn.eq(i).attr('data-originalvalue');
            periodExtendedTotal = periodExtendedTotal.plus(inputValueFromExtended);
            // PeriodDiscountAmount Column
            let inputValueFromDiscount: any = +periodDiscountColumn.eq(i).attr('data-originalvalue');
            periodDiscountTotal = periodDiscountTotal.plus(inputValueFromDiscount);
            // Tax Column
            let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
            taxTotal = taxTotal.plus(inputValueFromTax);
        };

        subTotal = periodExtendedTotal.toFixed(2);
        discount = periodDiscountTotal.toFixed(2);
        salesTax = taxTotal.toFixed(2);
        grossTotal = periodExtendedTotal.plus(periodDiscountTotal).toFixed(2);
        total = taxTotal.plus(periodExtendedTotal).toFixed(2);

        $form.find('.' + gridType + 'totals [data-totalfield="SubTotal"] input').val(subTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(discount);
        $form.find('.' + gridType + 'totals [data-totalfield="Tax"] input').val(salesTax);
        $form.find('.' + gridType + 'totals [data-totalfield="GrossTotal"] input').val(grossTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(total);
    };


    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = []; 

        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }

        FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.CombineActivityTabs === true) {
                $form.find('.notcombined').css('display', 'none');
                $form.find('.notcombinedtab').css('display', 'none');
                if ($form.find('.combined').css('display') === 'none') {
                    $form.find('.combined').css('display', 'block');
                    $form.find('.combinedtab').css('display', 'flex');
                }
                var $allOrderItemGrid;
                $allOrderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($allOrderItemGrid);
            } else {
                $form.find('.combined').css('display', 'none');
                $form.find('.combinedtab').css('display', 'none');
                if ($form.find('.notcombined').css('display') === 'none') {
                    $form.find('.notcombined').css('display', 'block');
                    $form.find('.notcombinedtab').css('display', 'flex');
                }
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
            }
            var hiddenRentals = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.RentalShowFields))
            var hiddenSales = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.SalesShowFields))
            var hiddenLabor = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.LaborShowFields))
            var hiddenMisc = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.MiscShowFields))
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
        }, null, null);
    };
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
        OrderController.copyOrder($form);
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
FwApplicationTree.clickEvents['{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}'] = function (e) {
    let search, $form, orderId, $popup;
    $form = jQuery(this).closest('.fwform');
    orderId = FwFormField.getValueByDataField($form, 'OrderId');

    if (orderId == "") {
        FwNotification.renderNotification('WARNING', 'Please save the record before performing this function');
    } else {
     search = new SearchInterface();
     $popup = search.renderSearchPopup($form, orderId, 'Order');
    }
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{F2FD2F4C-1AB7-4627-9DD5-1C8DB96C5509}'] = function (e) {
    var $form, $report, orderNumber, orderId;
    try {
        $form = jQuery(this).closest('.fwform');
        orderNumber = $form.find('div.fwformfield[data-datafield="OrderNumber"] input').val();
        orderId = $form.find('div.fwformfield[data-datafield="OrderId"] input').val();
        $report = RwPrintOrderController.openForm();
        FwModule.openSubModuleTab($form, $report);
        $report.find('.fwform-section[data-caption="Quote"]').css('display', 'none');
        $report.find('div.fwformfield[data-datafield="OrderId"] input').val(orderId);
        $report.find('div.fwformfield[data-datafield="OrderId"] .fwformfield-text').val(orderNumber);
        jQuery('.tab.submodule.active').find('.caption').html('Print Order');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//---------------------------------------------------------------------------------------------- 
FwApplicationTree.clickEvents['{D27AD4E7-E924-47D1-AF6E-992B92F5A647}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        if ($form.attr('data-controller') === 'OrderController') {
            OrderController.toggleOrderItemView($form, event);
        } else {
            QuoteController.toggleOrderItemView($form, event);
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};


//----------------------------------------------------------------------------------------------
var OrderController = new Order();