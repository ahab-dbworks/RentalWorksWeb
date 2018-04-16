routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2] }; return QuoteController.getModuleScreen(filter); } });

class Quote {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Quote';
        this.apiurl = 'api/v1/quote';
        this.ActiveView = 'ALL';

    }

    getModuleScreen(filter?: { datafield: string, search: string }) {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Quote', false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                filter.search = filter.search.replace(/%20/, ' ');
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var self = this;
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });

        FwBrowse.addLegend($browse, 'Prospect', '#ffffff');
        FwBrowse.addLegend($browse, 'Active', '#fffa00');
        FwBrowse.addLegend($browse, 'Reserved', '#0080ff');
        FwBrowse.addLegend($browse, 'Ordered', '#00c400');
        FwBrowse.addLegend($browse, 'Cancelled', '#ff0080');
        FwBrowse.addLegend($browse, 'Closed', '#ff8040');

        return $browse;
    }

    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('All', true);
        var $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect', true);
        var $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false);
        var $reserved: JQuery = FwMenu.generateDropDownViewBtn('Reserved', false);
        var $ordered: JQuery = FwMenu.generateDropDownViewBtn('Ordered', false);
        var $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false);
        var $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $prospect.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'PROSPECT';
            FwBrowse.search($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.search($browse);
        });
        $reserved.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RESERVED';
            FwBrowse.search($browse);
        });
        $ordered.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ORDERED';
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

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($prospect);
        viewSubitems.push($active);
        viewSubitems.push($reserved);
        viewSubitems.push($ordered);
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

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true')

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
            FwFormField.setValueByDataField($form, 'VersionNumber', 1);

            $form.find('div[data-datafield="DealId"]').attr('data-required', false);
            $form.find('div[data-datafield="PickTime"]').attr('data-required', false);

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

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

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

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }


    renderFrames($form: any) {
        var orderId;

        $form.find('.frame input').css('width', '100%');

        orderId = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();

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

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $orderStatusHistoryGrid: any;
        var $orderStatusHistoryGridControl: any;
        var max = 9999;

        $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: $form.find('div.fwformfield[data-datafield="QuoteId"] input').val()
            };
        })
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);


        var $orderItemGridRental;
        var $orderItemGridRentalControl;
        $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'R'
            };
            request.pagesize = max;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
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
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'S'
            };
            request.pagesize = max;
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
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
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'L'
            };
            request.pagesize = max;
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
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
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'M'
            };
            request.pagesize = max;
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
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
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
            };
        });
        $orderNoteGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');

    }

    beforeValidateDeal($browse, $form, request) {
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        request.uniqueids = {
            LocationId: officeLocationId
        }
    }

    afterLoad($form: any, mode: string) {
        var $orderStatusHistoryGrid: any;
        var $pending = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');

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

        if ($pending === true) {
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
        } else {
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

    }

    copyQuote($form) {
        var $confirmation, $yes, $no, self;
        self = this;

        $confirmation = FwConfirmation.renderConfirmation('Copy Quote', '');
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
        html.push('    <div data-value="O" data-caption="Order"> </div></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Rates & Prices" data-datafield="CopyRatesFromInventory"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Subs" data-datafield="CombineSubs"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Documents" data-datafield="CopyDocuments"></div>');
        html.push('</div>');

        var copyConfirmation = html.join('');
        var quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

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
            }

            for (var key in request) {
                if (request.hasOwnProperty(key)) {
                    if (request[key] == "T") {
                        request[key] = "True";
                    } else if (request[key] == "F") {
                        request[key] = "False";
                    }
                }
            }

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', 'api/v1/quote/copy/' + quoteId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Quote Successfully Copied');
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


}
//-----------------------------------------------------------------------------------------------------
var QuoteController = new Quote();
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{B918C711-32D7-4470-A8E5-B88AB5712863}'] = function (event) {
    var $form
    $form = jQuery(this).closest('.fwform');
    try {
        QuoteController.copyQuote($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{BC3B1A5E-7270-4547-8FD1-4D14F505D452}'] = function (event) {
    var html = [];
    html.push('<div class="fwform" data-controller="none" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 85vw; height: 85vh; overflow:scroll; position:relative;">');

    html.push('     <div id="breadcrumbs" class="fwmenu default" style="width:100%;height:5%; padding-left: 20px;">');
    html.push('         <div class="type" style="float:left; cursor: pointer; font-weight: bold;"></div>');
    html.push('         <div class="category" style="float:left; cursor: pointer; font-weight: bold;"></div>');
    html.push('         <div class="subcategory" style="float:left; cursor: pointer; font-weight: bold;"></div>');
    html.push('     </div>');


    html.push('     <div class="formrow" style="width:100%; position:absolute;">');

    html.push('              <div id="inventoryType" style="width:10%; float:left;">');
    html.push('              </div>');
 
    html.push('             <div id="rentalCategory" style="width:10%; float:left;">');
    html.push('             </div>');
 
    html.push('             <div id="subCategory" style="width:10%; float:left;">');
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
    html.push('                 </div>');

    html.push('                 <div class="inventory" style="overflow:auto">');

    html.push('                 </div>');
    html.push('            </div>');

    html.push('     </div>');

    html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
    html.push('</div>');

    var $form = html.join('');
    var $popup = FwPopup.renderPopup($form, { ismodal: true });
    FwPopup.showPopup($popup);
    FwConfirmation.addControls($popup, $form);

    var inventoryTypeRequest: any = {};
    inventoryTypeRequest.uniqueids = {
        Rental: true
    }
    FwAppData.apiMethod(true, 'POST', "api/v1/inventorytype/browse", inventoryTypeRequest, FwServices.defaultTimeout, function onSuccess(response) {
        var inventoryTypeIndex = response.ColumnIndex.InventoryType;
        var inventoryTypeIdIndex = response.ColumnIndex.InventoryTypeId;

        for (var i = 0; i < response.Rows.length; i++) {
            $popup.find('#inventoryType').append('<ul style="cursor:pointer;" data-value="' + response.Rows[i][inventoryTypeIdIndex] + '">' + response.Rows[i][inventoryTypeIndex] + '</ul>');
        }
    }, null, null);

  
    $popup.on('click', '#inventoryType ul', function (e) {
        $popup.find('#inventoryType ul').css({ 'background-color': 'white', 'color': 'black'});

        var invType = jQuery(e.currentTarget).text();
        var breadcrumb = $popup.find('#breadcrumbs .type');
        $popup.find("#breadcrumbs .category, #breadcrumbs .subcategory").empty();
        breadcrumb.text(invType);

        breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
        jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' });
        var inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
        breadcrumb.attr('data-value', inventoryTypeId);
        var rentalCategoryRequest: any = {};
        rentalCategoryRequest.uniqueids = {
            InventoryTypeId: inventoryTypeId
        }
        FwAppData.apiMethod(true, 'POST', "api/v1/rentalcategory/browse", rentalCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
            var categoryIdIndex = response.ColumnIndex.CategoryId;
            var categoryIndex = response.ColumnIndex.Category;
            $popup.find('#rentalCategory, #subCategory').empty();
            for (var i = 0; i < response.Rows.length; i++) {
                $popup.find('#rentalCategory').append('<ul style="cursor:pointer;" data-value="' + response.Rows[i][categoryIdIndex] + '">' + response.Rows[i][categoryIndex] + '</ul>');
            }
        }, null, null);


        FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", rentalCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
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

                $card.on('mouseenter', function () {
                    jQuery(this).css('box-shadow', '0 8px 16px 0 rgba(0, 0, 0, 0.2)');
                })

                $card.on('mouseleave', function () {
                    jQuery(this).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
                })

            }
        }, null, null);

    });

    $popup.on('click', '#rentalCategory ul', function (e) {
        $popup.find('#rentalCategory ul').css({ 'background-color': 'white', 'color': 'black' });

        var rentalCategory = jQuery(e.currentTarget).text();
        var breadcrumb = $popup.find('#breadcrumbs .category');
        $popup.find("#breadcrumbs .subcategory").empty();
        breadcrumb.text(rentalCategory);
        breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
        jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' });
        var rentalCategoryId = jQuery(e.currentTarget).attr('data-value');
        var inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
        breadcrumb.attr('data-value', rentalCategoryId);
        var subCategoryRequest: any = {};
        subCategoryRequest.uniqueids = {
            CategoryId: rentalCategoryId,
            InventoryTypeId: inventoryTypeId
        }
        FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
            var subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
            var subCategoryIndex = response.ColumnIndex.SubCategory;
            $popup.find('#subCategory').empty();
            for (var i = 0; i < response.Rows.length; i++) {
                $popup.find('#subCategory').append('<ul style="cursor:pointer;" data-value="' + response.Rows[i][subCategoryIdIndex] + '">' + response.Rows[i][subCategoryIndex] + '</ul>');
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

                $card.on('mouseenter', function () {
                    jQuery(this).css('box-shadow', '0 8px 16px 0 rgba(0, 0, 0, 0.2)');
                })

                $card.on('mouseleave', function () {
                    jQuery(this).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
                })
            }
        }, null, null);

    });


    $popup.on('click', '#subCategory ul', function (e) {
        $popup.find('#subCategory ul').css({ 'background-color': 'white', 'color': 'black' });

        var subCategory = jQuery(e.currentTarget).text();
        var breadcrumb = $popup.find('#breadcrumbs .subcategory');
        breadcrumb.text(subCategory);
        var subCategoryId = jQuery(e.currentTarget).attr('data-value');
        breadcrumb.attr('data-value', subCategoryId);
        jQuery(e.currentTarget).css({ 'background-color': 'gray', 'color': 'white' });

        var rentalCategoryId = $popup.find('#breadcrumbs .category').attr('data-value');
        var inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
        var request: any = {};
        request.uniqueids = {
            SubCategoryId: subCategoryId,
            CategoryId: rentalCategoryId,
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

                $card.on('mouseenter', function () {
                    jQuery(this).css('box-shadow', '0 8px 16px 0 rgba(0, 0, 0, 0.2)');
                })

                $card.on('mouseleave', function () {
                    jQuery(this).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
                })
            }
        }, null, null);

    });

    $popup.on('click', '#breadcrumbs .type', function (e) {
        $popup.find("#breadcrumbs .subcategory, #breadcrumbs .category").empty();
 
        var inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
        var rentalCategoryRequest: any = {};
        rentalCategoryRequest.uniqueids = {
            InventoryTypeId: inventoryTypeId
        }
        FwAppData.apiMethod(true, 'POST', "api/v1/rentalcategory/browse", rentalCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
            var categoryIdIndex = response.ColumnIndex.CategoryId;
            var categoryIndex = response.ColumnIndex.Category;
            $popup.find('#rentalCategory, #subCategory').empty();
            for (var i = 0; i < response.Rows.length; i++) {
                $popup.find('#rentalCategory').append('<ul style="cursor:pointer;" data-value="' + response.Rows[i][categoryIdIndex] + '">' + response.Rows[i][categoryIndex] + '</ul>');
            }
        }, null, null);

        FwAppData.apiMethod(true, 'POST', "api/v1/rentalinventory/browse", rentalCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
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

                $card.on('mouseenter', function () {
                    jQuery(this).css('box-shadow', '0 8px 16px 0 rgba(0, 0, 0, 0.2)');
                })

                $card.on('mouseleave', function () {
                    jQuery(this).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
                })

            }
        }, null, null);


    })

    $popup.on('click', '#breadcrumbs .category', function (e) {
        $popup.find("#breadcrumbs .subcategory").empty();

        var rentalCategoryId = jQuery(e.currentTarget).attr('data-value');
        var inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
        var subCategoryRequest: any = {};
        subCategoryRequest.uniqueids = {
            CategoryId: rentalCategoryId,
            InventoryTypeId: inventoryTypeId
        }
        FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCategoryRequest, FwServices.defaultTimeout, function onSuccess(response) {
            var subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
            var subCategoryIndex = response.ColumnIndex.SubCategory;
            $popup.find('#subCategory').empty();
            for (var i = 0; i < response.Rows.length; i++) {
                $popup.find('#subCategory').append('<ul style="cursor:pointer;" data-value="' + response.Rows[i][subCategoryIdIndex] + '">' + response.Rows[i][subCategoryIndex] + '</ul>');
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

                $card.on('mouseenter', function () {
                    jQuery(this).css('box-shadow', '0 8px 16px 0 rgba(0, 0, 0, 0.2)');
                })

                $card.on('mouseleave', function () {
                    jQuery(this).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
                })
            }
        }, null, null);
    })

    $popup.find('.close-modal').one('click', function (e) {
        FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
        jQuery(document).find('.fwpopup').off('click');
        jQuery(document).off('keydown');
    })

};
//-----------------------------------------------------------------------------------------------------