routes.push({ pattern: /^module\/order$/, action: function (match) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });
var Order = (function () {
    function Order() {
        this.Module = 'Order';
        this.apiurl = 'api/v1/order';
        this.caption = 'Order';
        this.ActiveView = 'ALL';
        var self = this;
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
            FwBrowse.databind($browse);
        });
        $confirmed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONFIRMED';
            FwBrowse.databind($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.databind($browse);
        });
        $hold.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'HOLD';
            FwBrowse.databind($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.databind($browse);
        });
        $cancelled.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CANCELLED';
            FwBrowse.databind($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.databind($browse);
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
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
        var $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=ALL';
            FwBrowse.databind($browse);
        });
        $userWarehouse.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
            FwBrowse.databind($browse);
        });
        var viewLocation = [];
        viewLocation.push($userWarehouse);
        viewLocation.push($all);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };
    ;
    ;
    Order.prototype.openForm = function (mode) {
        var $form, $submodulePickListBrowse;
        $form = jQuery(jQuery('#tmpl-modules-OrderForm').html());
        $form = FwModule.openForm($form, mode);
        $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);
        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            var today = new Date(Date.now()).toLocaleString();
            var date = today.split(',');
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValueByDataField($form, 'PickDate', date[0]);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', date[0]);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', date[0]);
            FwFormField.setValueByDataField($form, 'OfficeLocation', office.location);
            FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);
            $form.find('div[data-datafield="PickTime"]').attr('data-required', false);
            $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
            $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);
            FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid);
            FwFormField.setValueByDataField($form, 'OfficeLocationId', office.locationid);
            FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid);
            $form.find('div[data-datafield="Department"] input').val(department.department);
            $form.find('div[data-datafield="PendingPo"] input').prop('checked', true);
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
        }
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
    Order.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    ;
    Order.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };
    ;
    Order.prototype.renderGrids = function ($form) {
        var _this = this;
        var $orderPickListGrid;
        var $orderPickListGridControl;
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
        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'R'
            };
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        });
        FwBrowse.setAfterSaveCallback($orderItemGridRentalControl, function ($orderItemGridRentalControl, $tr) {
            _this.calculateTotals($form, 'rental');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridRentalControl, function ($orderItemGridRentalControl, $tr) {
            _this.calculateTotals($form, 'rental');
        });
        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);
        var $orderItemGridSales;
        var $orderItemGridSalesControl;
        $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSalesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'S'
            };
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        });
        FwBrowse.setAfterSaveCallback($orderItemGridSalesControl, function ($orderItemGridSalesControl, $tr) {
            _this.calculateTotals($form, 'sales');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridSalesControl, function ($orderItemGridSalesControl, $tr) {
            _this.calculateTotals($form, 'sales');
        });
        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);
        var $orderItemGridLabor;
        var $orderItemGridLaborControl;
        $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLaborControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'L'
            };
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        });
        FwBrowse.setAfterSaveCallback($orderItemGridLaborControl, function ($orderItemGridLaborControl, $tr) {
            _this.calculateTotals($form, 'labor');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridLaborControl, function ($orderItemGridLaborControl, $tr) {
            _this.calculateTotals($form, 'labor');
        });
        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);
        var $orderItemGridMisc;
        var $orderItemGridMiscControl;
        $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMiscControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'M'
            };
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        });
        FwBrowse.setAfterSaveCallback($orderItemGridMiscControl, function ($orderItemGridMiscControl, $tr) {
            _this.calculateTotals($form, 'misc');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridMiscControl, function ($orderItemGridMiscControl, $tr) {
            _this.calculateTotals($form, 'misc');
        });
        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);
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
    Order.prototype.copyOrder = function ($form) {
        var $confirmation, $yes, $no, self;
        self = this;
        var html = [];
        html.push('<div style="white-space:pre;">\n');
        html.push('<strong>Copy From</strong><br><input type="radio" name="TypeSelect" value="fromOrder" disabled>Quote/ Order');
        html.push('<input type="radio" name= "TypeSelect" value="fromInvoice" style="margin-left:20px;" disabled> Invoice');
        html.push('<br><br><div style="float:left; padding-right: 10px;">');
        html.push('Type:<br><input type="text" name="Type" style="width:80px; padding: 3px 3px; margin: 8px 0px;" disabled></div>');
        html.push('Deal:<br><input type="text" name="Deal" style="width:245px; padding: 3px 3px; margin: 8px 0px; float:left;"disabled>');
        html.push('<br><div style="float:left; padding-right:10px;">');
        html.push('No:<br><input type="text" name="OrderNumber" style="width:80px; padding: 3px 3px; margin: 8px 0px;" disabled></div>');
        html.push('<br>Description:<br><input type="text" name="Description" style="width:245px;padding:3px 3px; margin: 8px 0px; float:left;" disabled> <br>');
        html.push('<br><br><strong>Copy To</strong><br><input type="radio" name="copyToType" value="Quote" >Quote');
        html.push('<input type="radio" name="copyToType" value="toOrder" style="margin-left:20px;">Order');
        html.push('<br><br>New Deal:<br><input type="text" name="NewDeal" style="width:240px; padding: 3px 3px; margin: 8px 0px;"><br>');
        html.push('<strong>Options</strong><br><input type="radio" name="Options" value="copy">Copy Cost/Rates from existing Quote/Order<br>');
        html.push('<input type="radio" name="Options" value="default">Use default Cost/Rates from Inventory<br><br>');
        html.push('<strong>Date Behavior</strong><br><input type="radio" name="DateBehavior" value="copy">Copy From/To Dates from existing Quote/Order<br>');
        html.push('<input type="radio" name="DateBehavior" value="current">Use Current Date<br><br>');
        html.push('<input type="checkbox" name="copyLineItemNotes" value="copyNotes">Copy Line Item Notes<br>');
        html.push('<input type="checkbox" name="combineSubs" value="combine">Combine Subs<br>');
        html.push('<input type="checkbox" name="copyDocuments" value="copyDocs">Copy Documents<br><br>');
        var copyConfirmation = html.join('');
        var orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $confirmation = FwConfirmation.renderConfirmation('Copy Order', copyConfirmation);
        if (this.Module == "Order" || "Quote") {
            var orderNumber, deal, description;
            $confirmation.find('input[value="fromOrder"]').prop('checked', true);
            $confirmation.find('input[name="Type"]').val(this.Module);
            orderNumber = FwFormField.getValueByDataField($form, this.Module + 'Number');
            $confirmation.find('input[name="OrderNumber"]').val(orderNumber);
            deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
            $confirmation.find('input[name="Deal"]').val(deal);
            description = FwFormField.getValueByDataField($form, 'Description');
            $confirmation.find('input[name="Description"]').val(description);
        }
        $yes = FwConfirmation.addButton($confirmation, 'OK', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.on('click', function () {
            try {
                var copyToType, newDeal, rateOptions, dateOptions, copyLineItem, combineSubs, copyDocuments;
                $yes.text('Please wait...');
                $yes.off('click');
                copyToType = $confirmation.find('input[name="copyToType"]:checked').val();
                console.log(copyToType);
                newDeal = $confirmation.find('input[name="NewDeal"]').val();
                console.log(newDeal);
                rateOptions = $confirmation.find('input[name="Options"]:checked').val();
                console.log(rateOptions);
                dateOptions = $confirmation.find('input[name="DateBehavior"]:checked').val();
                console.log(dateOptions);
                copyLineItem = $confirmation.find('input[name="copyLineItemNotes"]').prop('checked');
                combineSubs = $confirmation.find('input[name="combineSubs"]').prop('checked');
                copyDocuments = $confirmation.find('input[name="copyDocuments"]').prop('checked');
                console.log(copyLineItem, combineSubs, copyDocuments);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    ;
    Order.prototype.cancelPickList = function (pickListId, pickListNumber, $form) {
        var $confirmation, $yes, $no, self;
        self = this;
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
    Order.prototype.renderFrames = function ($form) {
        var orderId;
        orderId = FwFormField.getValueByDataField($form, 'OrderId'),
            $form.find('.frame input').css('width', '100%');
        FwAppData.apiMethod(true, 'GET', "api/v1/ordersummary/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
            var key;
            for (key in response) {
                if (response.hasOwnProperty(key)) {
                    $form.find('[data-framedatafield="' + key + '"] input').val(response[key]);
                }
            }
        }, null, $form);
        FwFormField.disable($form.find('.frame'));
        $form.find(".frame .add-on").children().hide();
    };
    Order.prototype.afterLoad = function ($form) {
        var $orderPickListGrid;
        $orderPickListGrid = $form.find('[data-name="OrderPickListGrid"]');
        FwBrowse.search($orderPickListGrid);
        var $orderStatusHistoryGrid;
        $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        FwBrowse.search($orderStatusHistoryGrid);
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
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);
        var $pickListBrowse = $form.find('#PickListBrowse');
        FwBrowse.search($pickListBrowse);
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
        this.totals($form);
        FwFormField.disable($form.find('.totals'));
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');
    };
    ;
    Order.prototype.totals = function ($form) {
        var self = this;
        var gridTypes = ['rental', 'sales', 'labor', 'misc'];
        setTimeout(function () {
            for (var i = 0; i < gridTypes.length; i++) {
                self.calculateTotals($form, gridTypes[i]);
            }
        }, 4000);
    };
    Order.prototype.calculateTotals = function ($form, gridType) {
        var totals = 0;
        var finalTotal;
        setTimeout(function () {
            var periodExtended = $form.find('.' + gridType + 'grid .periodextended.editablefield');
            if (periodExtended.length > 0) {
                periodExtended.each(function () {
                    var value = jQuery(this).text();
                    if (value.charAt(0) === '$') {
                        value = value.slice(1).replace(/,/g, '');
                    }
                    var toNumber = parseFloat(parseFloat(value).toFixed(2));
                    totals += toNumber;
                    finalTotal = totals.toLocaleString();
                });
                $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val("$" + finalTotal);
            }
        }, 2000);
    };
    ;
    Order.prototype.dynamicColumns = function ($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"), $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'), $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'), $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'), $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'), rentalType = "RentalShow", salesType = "SalesShow", laborType = "LaborShow", miscType = "MiscShow", substring, column, fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'), fieldNames = [];
        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-browsedatafield');
            fieldNames.push(name);
        }
        FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var key in response) {
                if (key.indexOf(rentalType) !== -1) {
                    substring = key.replace(rentalType, '');
                    for (var i = 0; i < fieldNames.length; i++) {
                        switch (fieldNames[i]) {
                            case 'InventoryId':
                                fieldNames[i] = 'ICode';
                                break;
                            case 'WarehouseId':
                                fieldNames[i] = 'Warehouse';
                                break;
                            case 'ReturnToWarehouseId':
                                fieldNames[i] = 'ReturnToWarehouse';
                                break;
                        }
                        var propertyExists = response.hasOwnProperty(rentalType + fieldNames[i]);
                        if (!propertyExists) {
                            jQuery($rentalGrid.find('[data-browsedatafield="' + fieldNames[i] + '"]')).parent().hide();
                        }
                    }
                    switch (substring) {
                        case 'ICode':
                            column = jQuery($rentalGrid.find('[data-browsedatafield="InventoryId"]'));
                            break;
                        case 'Warehouse':
                            column = jQuery($rentalGrid.find('[data-browsedatafield="WarehouseId"]'));
                            break;
                        case 'ReturnToWarehouse':
                            column = jQuery($rentalGrid.find('[data-browsedatafield="ReturnToWarehouseId"]'));
                            break;
                        default:
                            column = jQuery($rentalGrid.find('[data-browsedatafield="' + substring + '"]'));
                            break;
                    }
                    if (response[key]) {
                        column.parent().show();
                    }
                    else {
                        column.parent().hide();
                    }
                }
                ;
                if (key.indexOf(salesType) !== -1) {
                    substring = key.replace(salesType, '');
                    for (var i = 0; i < fieldNames.length; i++) {
                        switch (fieldNames[i]) {
                            case 'InventoryId':
                                fieldNames[i] = 'ICode';
                                break;
                            case 'WarehouseId':
                                fieldNames[i] = 'Warehouse';
                                break;
                            case 'ReturnToWarehouseId':
                                fieldNames[i] = 'ReturnToWarehouse';
                                break;
                        }
                        var propertyExists = response.hasOwnProperty(salesType + fieldNames[i]);
                        if (!propertyExists) {
                            jQuery($salesGrid.find('[data-browsedatafield="' + fieldNames[i] + '"]')).parent().hide();
                        }
                    }
                    switch (substring) {
                        case 'ICode':
                            column = jQuery($salesGrid.find('[data-browsedatafield="InventoryId"]'));
                            break;
                        case 'Warehouse':
                            column = jQuery($salesGrid.find('[data-browsedatafield="WarehouseId"]'));
                            break;
                        case 'ReturnToWarehouse':
                            column = jQuery($salesGrid.find('[data-browsedatafield="ReturnToWarehouseId"]'));
                            break;
                        default:
                            column = jQuery($salesGrid.find('[data-browsedatafield="' + substring + '"]'));
                            break;
                    }
                    if (response[key]) {
                        column.parent().show();
                    }
                    else {
                        column.parent().hide();
                    }
                }
                ;
                if (key.indexOf(laborType) !== -1) {
                    substring = key.replace(laborType, '');
                    for (var i = 0; i < fieldNames.length; i++) {
                        switch (fieldNames[i]) {
                            case 'InventoryId':
                                fieldNames[i] = 'ICode';
                                break;
                            case 'WarehouseId':
                                fieldNames[i] = 'Warehouse';
                                break;
                            case 'ReturnToWarehouseId':
                                fieldNames[i] = 'ReturnToWarehouse';
                                break;
                        }
                        var propertyExists = response.hasOwnProperty(laborType + fieldNames[i]);
                        if (!propertyExists) {
                            jQuery($laborGrid.find('[data-browsedatafield="' + fieldNames[i] + '"]')).parent().hide();
                        }
                    }
                    switch (substring) {
                        case 'ICode':
                            column = jQuery($laborGrid.find('[data-browsedatafield="InventoryId"]'));
                            break;
                        case 'Warehouse':
                            column = jQuery($laborGrid.find('[data-browsedatafield="WarehouseId"]'));
                            break;
                        case 'ReturnToWarehouse':
                            column = jQuery($laborGrid.find('[data-browsedatafield="ReturnToWarehouseId"]'));
                            break;
                        default:
                            column = jQuery($laborGrid.find('[data-browsedatafield="' + substring + '"]'));
                            break;
                    }
                    if (response[key]) {
                        column.parent().show();
                    }
                    else {
                        column.parent().hide();
                    }
                }
                ;
                if (key.indexOf(miscType) !== -1) {
                    substring = key.replace(miscType, '');
                    for (var i = 0; i < fieldNames.length; i++) {
                        switch (fieldNames[i]) {
                            case 'InventoryId':
                                fieldNames[i] = 'ICode';
                                break;
                            case 'WarehouseId':
                                fieldNames[i] = 'Warehouse';
                                break;
                            case 'ReturnToWarehouseId':
                                fieldNames[i] = 'ReturnToWarehouse';
                                break;
                        }
                        var propertyExists = response.hasOwnProperty(miscType + fieldNames[i]);
                        if (!propertyExists) {
                            jQuery($miscGrid.find('[data-browsedatafield="' + fieldNames[i] + '"]')).parent().hide();
                        }
                    }
                    switch (substring) {
                        case 'ICode':
                            column = jQuery($miscGrid.find('[data-browsedatafield="InventoryId"]'));
                            break;
                        case 'Warehouse':
                            column = jQuery($miscGrid.find('[data-browsedatafield="WarehouseId"]'));
                            break;
                        case 'ReturnToWarehouse':
                            column = jQuery($miscGrid.find('[data-browsedatafield="ReturnToWarehouseId"]'));
                            break;
                        default:
                            column = jQuery($miscGrid.find('[data-browsedatafield="' + substring + '"]'));
                            break;
                    }
                    if (response[key]) {
                        column.parent().show();
                    }
                    else {
                        column.parent().hide();
                    }
                }
                ;
            }
        }, null, $form);
    };
    ;
    return Order;
}());
var OrderController = new Order();
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
//# sourceMappingURL=Order.js.map