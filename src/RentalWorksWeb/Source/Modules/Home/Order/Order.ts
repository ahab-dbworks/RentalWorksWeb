/// <reference path="../deal/deal.ts" />
routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });
//---------------------------------------------------------------------------------
class Order {
    Module: string;
    apiurl: string;
    caption: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Order';
        this.apiurl = 'api/v1/order';
        this.caption = 'Order';
        this.ActiveView = 'ALL';
        var self = this;

        //FwApplicationTree.clickEvents['{EE96992B-47EB-4F4B-A91A-AC9B7138D03B}'] = function (event) {
        //    var $browse, pickListId, pickListNumber;
        //    try {
        //        $browse = jQuery(this).closest('.fwbrowse');
        //        pickListNumber = $browse.find('.selected [data-browsedatafield="PickListNumber"]').attr('data-originalvalue');
        //        pickListId = $browse.find('.selected [data-browsedatafield="PickListId"]').attr('data-originalvalue');
        //        console.log(pickListNumber, pickListId);
        //        if (pickListId != null) {
        //            $browse = RwPickListReportController.openForm();
        //            FwModule.openModuleTab($browse, 'Pick List Report for ' + pickListNumber, true, 'REPORT', true);
        //            $browse.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
        //            $browse.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);
        //        } else {
        //            throw new Error("Please select a Pick List to print");
        //        }
        //    }
        //    catch (ex) {
        //        FwFunc.showError(ex);
        //    }

        //};

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
        return $browse;
    };

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

    openForm(mode) {
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
    }

    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    saveForm($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };

    renderGrids($form) {
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        }
        );
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        }
        );
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');



    };

    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    };

    cancelPickList(pickListId, pickListNumber, $form) {
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

    renderFrames($form: any) {
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
    }

    afterLoad($form) {
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
        this.totals($form);
        this.dynamicColumns($form);
        $form.find('.totals input').css('text-align', 'right');
    };

    totals($form: any) {
        FwFormField.disable($form.find('.totals'));
        $form.find(".totals .add-on").hide();

        var $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');

        setTimeout(function () {
            OrderController.calculateTotals($form);
        }, 4000);

        jQuery($rentalGrid).on('click', '.divsaverow', function (e) {
            setTimeout(function () {
                OrderController.calculateTotals($form);
            }, 1000);
        });
        //need to capture delete
    }

    calculateTotals($form: any) {
        var totals = 0;
        var finalTotal;
        var periodExtended = $form.find('.rentalgrid .periodextended.editablefield');
        periodExtended.each(function () {
            var value = jQuery(this).text();
            if (value.charAt(0) === '$') {
                value = value.slice(1).replace(/,/g, '');
            }
            var toNumber = parseFloat(parseFloat(value).toFixed(2));

            totals += toNumber;
            finalTotal = totals.toLocaleString();
        });
        $form.find('.rentaltotals [data-totalfield="Total"] input').val("$" + finalTotal);
    };

    dynamicColumns($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            rentalType = "RentalShow",
            salesType = "SalesShow",
            laborType = "LaborShow",
            miscType = "MiscShow",
            substring,
            column,
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [];

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
                    } else {
                        column.parent().hide();
                    }
                };

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
                    } else {
                        column.parent().hide();
                    }
                };

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
                    } else {
                        column.parent().hide();
                    }
                };

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
                    } else {
                        column.parent().hide();
                    }
                };


            }
        }, null, $form);
    };
}


//---------------------------------------------------------------------------------
var OrderController = new Order();
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
        //CreatePickListController.renderGrids($form);
        FwBrowse.search($pickListUtilityGrid);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};