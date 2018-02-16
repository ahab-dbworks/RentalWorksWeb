var Order = (function () {
    function Order() {
        this.Module = 'Order';
        this.apiurl = 'api/v1/order';
        this.caption = 'Order';
        this.ActiveView = 'ALL';
        var self = this;
        FwApplicationTree.clickEvents['{C6CC3D94-24CE-41C1-9B4F-B4F94A50CB48}'] = function (event) {
            var $form, pickListId, pickListNumber;
            $form = jQuery(this).closest('.fwform');
            pickListId = $form.find('tr.selected > td.column > [data-formdatafield="PickListId"]').attr('data-originalvalue');
            pickListNumber = $form.find('tr.selected > td.column > [data-formdatafield="PickListNumber"]').attr('data-originalvalue');
            try {
                self.cancelPickList(pickListId, pickListNumber, $form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        };
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
        FwBrowse.init($browse);
        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'On Hold', '#ff8040');
        FwBrowse.addLegend($browse, 'No Charge', '#ff0080');
        FwBrowse.addLegend($browse, 'Late', '#ffff80');
        FwBrowse.addLegend($browse, 'Foreign Currency', '#03de3a');
        FwBrowse.addLegend($browse, 'Multi-Warehouse', '#20b7ff');
        FwBrowse.addLegend($browse, 'Repair', '#20b7ff');
        FwBrowse.addLegend($browse, 'L&D', '#20b7ff');
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
        var $form, $submodulePickListReport;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
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
        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridLabor);
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);
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
    };
    ;
    return Order;
}());
window.OrderController = new Order();
//# sourceMappingURL=Order.js.map