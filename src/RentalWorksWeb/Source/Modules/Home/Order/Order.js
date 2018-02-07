var Order = /** @class */ (function () {
    function Order() {
        this.Module = 'Order';
        this.apiurl = 'api/v1/order';
        this.caption = 'Order';
        this.ActiveView = 'ALL';
        var self = this;
        FwApplicationTree.clickEvents['{EE96992B-47EB-4F4B-A91A-AC9B7138D03B}'] = function (event) {
            var $browse, pickListId, pickListNumber;
            try {
                $browse = jQuery(this).closest('.fwbrowse');
                pickListNumber = $browse.find('.selected [data-browsedatafield="PickListNumber"]').attr('data-originalvalue');
                pickListId = $browse.find('.selected [data-browsedatafield="PickListId"]').attr('data-originalvalue');
                console.log(pickListNumber, pickListId);
                if (pickListId != null) {
                    $browse = RwPickListReportController.openForm();
                    FwModule.openModuleTab($browse, 'Pick List Report for ' + pickListNumber, true, 'REPORT', true);
                    $browse.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
                    $browse.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);
                }
                else {
                    throw new Error("Please select a Pick List to print");
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        };
        //Confirmation for cancelling Pick List
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
        FwBrowse.addLegend($browse, 'On Hold', '#ff8040'); //placeholder colors
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
        //Location Filter
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
        var $form;
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
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
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
                OrderId: $form.find('div.fwformfield[data-datafield="OrderId"] input').val()
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
                OrderId: $form.find('div.fwformfield[data-datafield="OrderId"] input').val()
            };
        });
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
    };
    ;
    Order.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="OrderId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    ;
    Order.prototype.cancelPickList = function (pickListId, pickListNumber, $form) {
        var $confirmation, $yes, $no, self;
        self = this;
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
                            OrderId: $form.find('[data-datafield="OrderId"] input').val()
                        };
                    });
                    FwBrowse.search($pickListGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        });
    };
    ;
    Order.prototype.renderFrames = function ($form) {
        var orderId;
        orderId = $form.find('div.fwformfield[data-datafield="OrderId"] input').val();
        FwAppData.apiMethod(true, 'GET', "api/v1/ordersummary/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
            var key;
            for (key in response) {
                if (response.hasOwnProperty(key)) {
                    $form.find('[data-framedatafield="' + key + '"] input').val(response[key]);
                }
            }
        });
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
    Order.prototype.loadRelatedValidationFields = function (validationName, $valuefield, $tr, $datafield) {
        var $form;
        $form = $valuefield.closest('.fwform');
        $form.find('[data-datafield="DealNumber"] input').val($tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
    };
    ;
    return Order;
}());
window.OrderController = new Order();
//# sourceMappingURL=Order.js.map