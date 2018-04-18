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
            //FwFormField.setValue($form, 'div[data-datafield="RateType"]', $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue'));
            var type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            $form.find('div[data-datafield="RateType"]').attr('data-originalvalue', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-value').val(type);
        })

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

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

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

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
        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'R'
            };
            request.pagesize = max;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        }
        );
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
            this.calculateTotals($form, 'rental');
            this.calculateDiscount($form, 'rental');
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
            request.pagesize = max;
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateTotals($form, 'sales');
            this.calculateDiscount($form, 'sales');
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
            request.pagesize = max;
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateTotals($form, 'labor');
            this.calculateDiscount($form, 'labor');
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
            request.pagesize = max;
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        }
        );
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateTotals($form, 'misc');
            this.calculateDiscount($form, 'misc');
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
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');

    };

    beforeValidateDeal($browse, $form, request) {
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        request.uniqueids = {
            LocationId: officeLocationId
        }
    }

    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    };

    copyOrder($form) {
        var $confirmation, $yes, $no, self;
        self = this;

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
            }, $form);
        };


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
        }, null, null);

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
        this.dynamicColumns($form);
        FwFormField.disable($form.find('.totals'));
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        FwFormField.disable($form.find('[data-caption="Weeks"]'));

        var noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }
    };

    calculateTotals($form: any, gridType: string) {
        var total: any = 0;
        var periodExtended = $form.find('.' + gridType + 'grid .periodextended');

        for (var i = 1; i < periodExtended.length; i++) {
            var value: any = parseFloat(periodExtended.eq(i).attr('data-originalvalue'));
            total += value;
        };
        $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(total);

    };

    calculateDiscount($form: any, gridType: string) {
        var total: any = 0;
        var periodDiscount = $form.find('.' + gridType + 'grid [data-browsedatafield="PeriodDiscountAmount"]');

        for (var i = 1; i < periodDiscount.length; i++) {
            var value: any = parseFloat(periodDiscount.eq(i).attr('data-originalvalue'));
            total += value;
        };
        $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(total);

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
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
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
        }, null, null);
    };

    renderSearchPopup() {
        //render popup
        var html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 85vw; height: 85vh; overflow:scroll; position:relative;">');

        html.push('     <div id="breadcrumbs" class="fwmenu default" style="width:100%;height:5%; padding-left: 20px;">');
        html.push('         <div class="type" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        html.push('         <div class="category" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        html.push('         <div class="subcategory" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        html.push('     </div>');

        html.push('     <div class="formrow" style="width:100%; position:absolute;">');
        html.push('              <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Inventory Type" data-type="radio" style="width:30%; margin: 5px 0px 25px 35px; float:clear;">');
        html.push('                  <div data-value="Rental" data-caption="Rental" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="Sales" data-caption="Sales" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="Labor" data-caption="Labor" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="Misc" data-caption="Misc" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="Parts" data-caption="Parts" style="float:left; width:20%;"></div>');
        html.push('              </div>');

        html.push('              <div id="inventoryType" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        html.push('              </div>');

        html.push('             <div id="category" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        html.push('             </div>');

        html.push('             <div id="subCategory" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        html.push('             </div>');

        html.push('            <div style="width:65%; position:absolute; top: 5%; left: 35%; right: 5%;">')
        html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Start" data-datafield="" style="width:120px; float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Stop" data-datafield="" style="width:120px;float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Activity" data-datafield="" style="width:150px;float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Select" data-datafield="" style="width:150px;float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sort By" data-datafield="" style="width:150px;float:left;"></div>');
        html.push('                      <div data-type="button" class="fwformcontrol" style="width:70px; float:left; margin:15px;">Preview</div>');
        html.push('                      <div data-type="button" class="fwformcontrol" style="width:125px; float:left; margin:15px;">Add to Order</div>');
        html.push('                  </div>');
        html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search By" data-datafield="" style="width:120px; float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search " data-datafield="" style="width:570px; float:left;"></div>');
        html.push('                      <div data-type="button" class="fwformcontrol" style="margin: 12px 6px 12px 60px;  padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>');
        html.push('                      <div data-type="button" class="fwformcontrol" style="margin: 12px 6px 12px 6px;  padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>');
        html.push('                      <div data-type="button" class="fwformcontrol" style="margin: 12px 6px 12px 6px;  padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>');
        html.push('                 </div>');


        html.push('                 <div class="inventory" style="overflow:auto">');

        html.push('                 </div>');
        html.push('            </div>');

        html.push('     </div>');

        html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');

        var $searchForm = html.join('');
        var $popup = FwPopup.renderPopup($searchForm, { ismodal: true });
        FwPopup.showPopup($popup);
        FwConfirmation.addControls($popup, $searchForm);

        $popup.find('.close-modal').one('click', function (e) {
            FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        })

        return $popup;
    }

    populateTypeMenu($popup, request) {
        FwAppData.apiMethod(true, 'POST', "api/v1/inventoryType/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
            var inventoryTypeIndex, inventoryTypeIdIndex;

            inventoryTypeIndex = response.ColumnIndex.InventoryType;
            inventoryTypeIdIndex = response.ColumnIndex.InventoryTypeId;

            for (var i = 0; i < response.Rows.length; i++) {
                $popup.find('#inventoryType').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][inventoryTypeIdIndex] + '">' + response.Rows[i][inventoryTypeIndex] + '</ul>');

            }
        }, null, null);
    }

    typeOnClickEvents($popup) {
        $popup.on('click', '#inventoryType ul', function (e) {
            var invType, inventoryTypeId, breadcrumb, request: any = {};
            $popup.find('#inventoryType ul').css({ 'background-color': 'white', 'color': 'black' });

            invType = jQuery(e.currentTarget).text();
            $popup.find('#inventoryType ul').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .type');
            $popup.find("#breadcrumbs .category, #breadcrumbs .subcategory").empty();
            breadcrumb.text(invType);
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');

            jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' });
            inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
            breadcrumb.attr('data-value', inventoryTypeId);

            request.uniqueids = {
                InventoryTypeId: inventoryTypeId
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/rentalcategory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var categoryIdIndex = response.ColumnIndex.CategoryId;
                var categoryIndex = response.ColumnIndex.Category;
                $popup.find('#category, #subCategory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('#category').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '">' + response.Rows[i][categoryIndex] + '</ul>');
                }
            }, null, null);

            FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var descriptionIndex = response.ColumnIndex.Description;
                $popup.find('.inventory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('.inventory').append('<div class="card" style="cursor:pointer; width:225px; height:230px; float:left; padding:10px; margin:8px;">' + response.Rows[i][descriptionIndex] + '</div>');
                }

                var css = {
                    'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
                    'transition': '0.3s'
                }
                var $card = $popup.find('.inventory > div');
                $card.css(css);

            }, null, null);
        });

    }

    categoryOnClickEvents($popup) {
        $popup.on('click', '#category ul', function (e) {
            var category, breadcrumb, categoryId, inventoryTypeId, request: any = {};
            $popup.find('#category ul').css({ 'background-color': 'white', 'color': 'black' });

            category = jQuery(e.currentTarget).text();
            $popup.find('#category ul').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .category');
            $popup.find("#breadcrumbs .subcategory").empty();
            breadcrumb.text(category);
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
            jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' });
            categoryId = jQuery(e.currentTarget).attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            breadcrumb.attr('data-value', categoryId);
            
            request.uniqueids = {
                CategoryId: categoryId,
                InventoryTypeId: inventoryTypeId
            }
            FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                var subCategoryIndex = response.ColumnIndex.SubCategory;
                $popup.find('#subCategory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('#subCategory').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][subCategoryIdIndex] + '">' + response.Rows[i][subCategoryIndex] + '</ul>');
                }
            }, null, null);

            FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var descriptionIndex = response.ColumnIndex.Description;
                $popup.find('.inventory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('.inventory').append('<div class="card" style="cursor:pointer; width:225px; height:230px; float:left; padding:10px; margin:8px;">' + response.Rows[i][descriptionIndex] + '</div>');

                    var css = {
                        'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
                        'transition': '0.3s'
                    }
                    var $card = $popup.find('div.card');
                    $card.css(css);
                }
            }, null, null);

        });


    };

    subCategoryOnClickEvents($popup) {
        $popup.on('click', '#subCategory ul', function (e) {
            var subCategory, breadcrumb, subCategoryId, categoryId, inventoryTypeId, request: any = {};
            $popup.find('#subCategory ul').css({ 'background-color': 'white', 'color': 'black' });

            subCategory = jQuery(e.currentTarget).text();
            $popup.find('#subCategory ul').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .subcategory');
            breadcrumb.text(subCategory);
            subCategoryId = jQuery(e.currentTarget).attr('data-value');
            breadcrumb.attr('data-value', subCategoryId);
            jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' });

            categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            request.uniqueids = {
                SubCategoryId: subCategoryId,
                CategoryId: categoryId,
                InventoryTypeId: inventoryTypeId
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var descriptionIndex = response.ColumnIndex.Description;
                $popup.find('.inventory').empty();
                for (var i = 0; i < response.Rows.length; i++) {

                    $popup.find('.inventory').append('<div class="card" style="cursor:pointer; width:225px; height:230px; float:left; padding:10px; margin:8px;">' + response.Rows[i][descriptionIndex] + '</div>');

                    var css = {
                        'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
                        'transition': '0.3s'
                    }
                    var $card = $popup.find('.inventory > div');
                    $card.css(css);
                   
                }
            }, null, null);
        });
    }

    breadCrumbs($popup) {
        $popup.on('click', '#breadcrumbs .type', function (e) {
            $popup.find("#breadcrumbs .subcategory, #breadcrumbs .category").empty();

            var inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
            var request: any = {};
            request.uniqueids = {
                InventoryTypeId: inventoryTypeId
            }
            FwAppData.apiMethod(true, 'POST', "api/v1/rentalcategory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var categoryIdIndex = response.ColumnIndex.CategoryId;
                var categoryIndex = response.ColumnIndex.Category;
                $popup.find('#category, #subCategory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('#category').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '">' + response.Rows[i][categoryIndex] + '</ul>');
                }
            }, null, null);

            FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var descriptionIndex = response.ColumnIndex.Description;
                $popup.find('.inventory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('.inventory').append('<div class="card" style="cursor:pointer; width:225px; height:230px; float:left; padding:10px; margin:8px;">' + response.Rows[i][descriptionIndex] + '</div>');

                    var css = {
                        'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
                        'transition': '0.3s'
                    }
                    var $card = $popup.find('.inventory > div');
                    $card.css(css);
                }
            }, null, null);


        })


        $popup.on('click', '#breadcrumbs .category', function (e) {
            $popup.find("#breadcrumbs .subcategory").empty();

            var categoryId = jQuery(e.currentTarget).attr('data-value');
            var inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            var subCategoryRequest: any = {};
            subCategoryRequest.uniqueids = {
                CategoryId: categoryId,
                InventoryTypeId: inventoryTypeId
            }
            FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
                var subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                var subCategoryIndex = response.ColumnIndex.SubCategory;
                $popup.find('#subCategory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('#subCategory').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][subCategoryIdIndex] + '">' + response.Rows[i][subCategoryIndex] + '</ul>');
                }
            }, null, null);

            FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", subCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
                var descriptionIndex = response.ColumnIndex.Description;
                $popup.find('.inventory').empty();
                for (var i = 0; i < response.Rows.length; i++) {
                    $popup.find('.inventory').append('<div class="card" style="cursor:pointer; width:225px; height:230px; float:left; padding:10px; margin:8px;">' + response.Rows[i][descriptionIndex] + '</div>');

                    var css = {
                        'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
                        'transition': '0.3s'
                    }
                    var $card = $popup.find('div.card');
                    $card.css(css);
                }
            }, null, null);
        })
    }
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
        FwBrowse.search($pickListUtilityGrid);
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
        OrderController.cancelPickList(pickListId, pickListNumber, $form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};


FwApplicationTree.clickEvents['{E25CB084-7E7F-4336-9512-36B7271AC151}'] = function (event) {
    var $form
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.copyOrder($form);
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

FwApplicationTree.clickEvents['{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}'] = function (e) {
    var $popup,
        inventoryType,
        inventoryTypeRequest: any = {};

    $popup = OrderController.renderSearchPopup();

    $popup.find('[data-type="radio"]').on('change', function () {
        inventoryType = $popup.find('[data-type="radio"] input:checked').val();

        switch (inventoryType) {
            case 'Rental':
                inventoryTypeRequest.uniqueids = {
                    Rental: true
                }
                break;
            case 'Sales':
                inventoryTypeRequest.uniqueids = {
                    Sales: true
                }
                break;
            case 'Labor':
                inventoryTypeRequest.uniqueids = {
                    Labor: true
                }
                break;
            case 'Misc':
                inventoryTypeRequest.uniqueids = {
                    Misc: true
                }
                break;
            case 'Parts':
                inventoryTypeRequest.uniqueids = {
                    Parts: true
                }
                break;
            default:
                inventoryTypeRequest.uniqueids = {
                    Rental: true
                }
        }

        console.log(inventoryType)
    });

    OrderController.populateTypeMenu($popup, inventoryTypeRequest);
    OrderController.typeOnClickEvents($popup);
    OrderController.categoryOnClickEvents($popup);
    OrderController.subCategoryOnClickEvents($popup);
    OrderController.breadCrumbs($popup);

    $popup.on('mouseenter', '.inventory > div', function () {
        jQuery(this).css('box-shadow', '0 10px 18px 0 rgba(0, 0, 0, 0.2)');
    });

    $popup.on('mouseleave', '.inventory > div', function (e) {
        var selected = jQuery(e.currentTarget).hasClass('selected');
        if (selected) {
            jQuery(e.currentTarget).css('box-shadow', '0 12px 20px 0 rgba(0,0,153,0.2)');
        } else {
            jQuery(e.currentTarget).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
        }

    });

    $popup.on('click', '.inventory > div', function (e) {
        $popup.find('.inventory > div').removeClass('selected');
        $popup.find('.inventory > div').css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');

        jQuery(e.currentTarget).addClass('selected');
  
        jQuery(e.currentTarget).css('box-shadow', '0 12px 20px 0 rgba(0,0,153,0.2)');
    });

    var highlight = {
        'background-color': '#D3D3D3',
        'color': 'white'
    }

    var unhighlight = {
        'background-color': 'white',
        'color': 'black'
    }
    $popup.on('mouseenter', '#inventoryType ul, #category ul, #subCategory ul', function (e) {
        jQuery(e.currentTarget).css(highlight)
    });

    $popup.on('mouseleave', '#inventoryType ul, #category ul, #subCategory ul', function (e) {
        var selected = jQuery(e.currentTarget).hasClass('selected');
        if (selected) {
            jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' })
        } else {
            jQuery(e.currentTarget).css(unhighlight)
        }
    });
};