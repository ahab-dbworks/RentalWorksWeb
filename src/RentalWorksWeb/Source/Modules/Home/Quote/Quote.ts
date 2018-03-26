class Quote {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Quote';
        this.apiurl = 'api/v1/quote';
        this.ActiveView = 'ALL';

    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Quote', false, 'BROWSE', true);
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
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $prospect.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'PROSPECT';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $reserved.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RESERVED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $ordered.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ORDERED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $cancelled.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CANCELLED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
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
            FwBrowse.databind($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.databind($browse);
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
        }, null, $form);

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
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
        FwBrowse.setAfterSaveCallback($orderItemGridRentalControl, ($orderItemGridRentalControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'rental');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridRentalControl, ($orderItemGridRentalControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'rental');
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
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
        FwBrowse.setAfterSaveCallback($orderItemGridSalesControl, ($orderItemGridSalesControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'sales');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridSalesControl, ($orderItemGridSalesControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'sales');
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
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
        FwBrowse.setAfterSaveCallback($orderItemGridLaborControl, ($orderItemGridLaborControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'labor');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridLaborControl, ($orderItemGridLaborControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'labor');
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
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
        FwBrowse.setAfterSaveCallback($orderItemGridMiscControl, ($orderItemGridMiscControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'misc');
        });
        FwBrowse.setAfterDeleteCallback($orderItemGridMiscControl, ($orderItemGridMiscControl: JQuery, $tr: JQuery) => {
            this.calculateTotals($form, 'misc');
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
        this.totals($form);
        FwFormField.disable($form.find('.totals'));
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        FwFormField.disable($form.find('[data-caption="Weeks"]'));
 
    }

    copyQuote($form) {
        var $confirmation, $yes, $no, self;
        self = this;

        $confirmation = FwConfirmation.renderConfirmation('Copy Quote', '');

        var html = [];
        //html.push('<div style="white-space:pre;">\n');
        //html.push('<strong>Copy From</strong><br><input type="radio" name="TypeSelect" value="fromOrder" disabled>Quote/ Order');
        //html.push('<input type="radio" name= "TypeSelect" value="fromInvoice" style="margin-left:20px;" disabled> Invoice');
        //html.push('<br><br><div style="float:left; padding-right: 10px;">');
        //html.push('Type:<br><input type="text" name="Type" style="width:80px; padding: 3px 3px; margin: 8px 0px;" disabled></div>');
        //html.push('Deal:<br><input type="text" name="Deal" style="width:245px; padding: 3px 3px; margin: 8px 0px; float:left;"disabled>');
        //html.push('<br><div style="float:left; padding-right:10px;">');
        //html.push('No:<br><input type="text" name="OrderNumber" style="width:80px; padding: 3px 3px; margin: 8px 0px;" disabled></div>');
        //html.push('<br>Description:<br><input type="text" name="Description" style="width:245px;padding:3px 3px; margin: 8px 0px; float:left;" disabled> <br>');
        //html.push('<br><br><strong>Copy To</strong><br><input type="radio" name="copyToType" value="Quote" >Quote');
        //html.push('<input type="radio" name="copyToType" value="Order" style="margin-left:20px;">Order');
        //html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        //html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy From" data-datafield="" data-formreadonly="true">');
        //html.push('      <div data-value="QuoteOrder" data-caption="Quote/Order" > </div>');
        //html.push('    <div data-value="Invoice" data-caption="Invoice" > </div></div><br>');
        //html.push('<br><br>New Deal:<br><input type="text" name="NewDeal" style="width:240px; padding: 3px 3px; margin: 8px 0px;"><br>');
        //html.push('<strong>Options</strong><br><input type="radio" name="Options" value="copy">Copy Cost/Rates from existing Quote/Order<br>');
        //html.push('<input type="radio" name="Options" value="default">Use default Cost/Rates from Inventory<br><br>');
        //html.push('<strong>Date Behavior</strong><br><input type="radio" name="DateBehavior" value="copy">Copy From/To Dates from existing Quote/Order<br>');
        //html.push('<input type="radio" name="DateBehavior" value="current">Use Current Date<br><br>');
        //html.push('<input type="checkbox" name="copyLineItemNotes" value="copyNotes">Copy Line Item Notes<br>');
        //html.push('<input type="checkbox" name="combineSubs" value="combine">Combine Subs<br>');
        //html.push('<input type="checkbox" name="copyDocuments" value="copyDocs">Copy Documents<br><br>');

        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="" style="width:90px;float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="" style="width:235px; float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="" style="width:90px; float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:235px;float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy To" data-datafield="">');
        html.push('      <div data-value="Q" data-caption="Quote"> </div>');
        html.push('    <div data-value="O" data-caption="Order"> </div></div><br>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Rates From Inventory" data-datafield="CopyRatesFromInventory"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('  </div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Subs" data-datafield="CombineSubs"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Documents" data-datafield="CopyDocuments"></div>');
        html.push('</div>');

        var copyConfirmation = html.join('');
        var quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

        FwConfirmation.addControls($confirmation, html.join(''));

        var orderNumber, deal, description;
        $confirmation.find('div[data-caption="Type"] input').val(this.Module);
        orderNumber = FwFormField.getValueByDataField($form, this.Module + 'Number');
        $confirmation.find('div[data-caption="No"] input').val(orderNumber);
        deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="Deal"] input').val(deal);
        description = FwFormField.getValueByDataField($form, 'Description');
        $confirmation.find('div[data-caption="Description"] input').val(description);



        $yes = FwConfirmation.addButton($confirmation, 'OK', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.on('click', function () {
            try {
                $yes.text('Please wait...');
                $yes.off('click');



                var request: any = {};
                request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
                request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
                request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
                request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
                request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
                request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
                request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');

                FwAppData.apiMethod(true, 'POST', 'api/v1/quote/copy/' + quoteId, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Copied');
                    FwConfirmation.destroyConfirmation($confirmation);

                    console.log(response, "RESPONSE");
                    var uniqueids: any = {};
                    if (request.CopyToType == "O") {
                        uniqueids.OrderId = response.QuoteId;
                        var $form = OrderController.loadForm(uniqueids);
                    } else if (request.CopyToType == "Q") {
                        uniqueids.QuoteId = response.QuoteId;
                        var $form = QuoteController.loadForm(uniqueids);
                    }
                    FwModule.openModuleTab($form, "", true, 'FORM', true)

                }, null, $form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    totals($form: any) {
        var self = this;
        var gridTypes = ['rental', 'sales', 'labor', 'misc'];
        setTimeout(function () {
            for (var i = 0; i < gridTypes.length; i++) {
                self.calculateTotals($form, gridTypes[i]);
            }
        }, 4000);
    }

    calculateTotals($form: any, gridType: string) {
        var totals = 0;
        var finalTotal;
        setTimeout(function () {
            var periodExtended = $form.find('.' + gridType + 'grid .periodextended.editablefield');
            if (periodExtended.length > 0) {
                periodExtended.each(function () {
                    var value = jQuery(this).attr('data-originalvalue');
                    var toNumber = parseFloat(parseFloat(value).toFixed(2));

                    totals += toNumber;
                    finalTotal = totals.toLocaleString();

                });

                $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val("$" + finalTotal);
            }
        }, 2000);

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

var QuoteController = new Quote();


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