routes.push({ pattern: /^module\/quote$/, action: function (match) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return QuoteController.getModuleScreen(filter); } });
class Quote {
    constructor() {
        this.Module = 'Quote';
        this.apiurl = 'api/v1/quote';
        this.caption = Constants.Modules.Agent.children.Quote.caption;
        this.nav = Constants.Modules.Agent.children.Quote.nav;
        this.id = Constants.Modules.Agent.children.Quote.id;
        this.ActiveViewFields = {};
        this.totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',];
    }
    getModuleScreen(filter) {
        var self = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            if (typeof filter !== 'undefined') {
                filter.search = filter.search.replace(/%20/, ' ');
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('');
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });
        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
        var department = JSON.parse(sessionStorage.getItem('department'));
        var location = JSON.parse(sessionStorage.getItem('location'));
        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);
        return $browse;
    }
    addBrowseMenuItems(options) {
        FwMenu.addBrowseMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'dpH0uCuEp3E89', (e) => {
            try {
                this.browseCancelOption(options.$browse);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $request = FwMenu.generateDropDownViewBtn('Request', false, "REQUEST");
        const $prospect = FwMenu.generateDropDownViewBtn('Prospect', true, "PROSPECT");
        const $active = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $reserved = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
        const $ordered = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
        const $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
        FwMenu.addVerticleSeparator(options.$menu);
        let viewSubitems = [];
        viewSubitems.push($all, $new, $request, $prospect, $active, $reserved, $ordered, $cancelled, $closed);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");
        if (sessionStorage.getItem('userType') === 'USER') {
            const location = JSON.parse(sessionStorage.getItem('location'));
            const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
            const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
            if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
                this.ActiveViewFields.LocationId = [location.locationid];
            }
            let viewLocation = [];
            viewLocation.push($userLocation, $allLocations);
            FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
        }
        else if (sessionStorage.getItem('userType') === 'CONTACT') {
            const deal = JSON.parse(sessionStorage.getItem('deal'));
            if (typeof this.ActiveViewFields["DealId"] == 'undefined') {
                this.ActiveViewFields.DealId = [deal.dealid];
            }
        }
    }
    addFormMenuItems(options) {
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Copy Quote', 'KaKyGTNrQjLq', (e) => {
            try {
                this.copyOrderOrQuote(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Quote', '', (e) => {
            try {
                this.printQuoteOrder(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Order', 'PPxCaMMveWaz', (e) => {
            try {
                this.createOrder(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'gchoKFMGs1WY', (e) => {
            try {
                OrderController.cancelUncancelOrder(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    openForm(mode, parentModuleInfo) {
        let userType = sessionStorage.getItem('userType');
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            var today = FwFunc.getDate();
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValueByDataField($form, 'VersionNumber', 1);
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="Rental"]', true);
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
            FwFormField.setValueByDataField($form, 'PendingPo', true);
            FwFormField.setValueByDataField($form, 'PoNumber', 'PENDING');
            if (userType === 'CONTACT') {
                let deal = JSON.parse(sessionStorage.getItem('deal'));
                FwFormField.setValueByDataField($form, 'DealId', deal.dealid, deal.deal);
            }
        }
        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
        }
        this.events($form);
        this.renderPrintButton($form);
        this.renderSearchButton($form);
        if (userType === 'CONTACT') {
            FwFormField.disableDataField($form, 'DealId');
            this.renderSubmitButton($form);
        }
        else if (userType === 'USER') {
            this.renderActiveRequest($form);
        }
        return $form;
    }
    renderPrintButton($form) {
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', function () {
            try {
                var $form = jQuery(this).closest('.fwform');
                var quoteNumber = FwFormField.getValue($form, 'div[data-datafield="QuoteNumber"]');
                var quoteId = FwFormField.getValue($form, 'div[data-datafield="QuoteId"]');
                var $report = QuoteReportController.openForm();
                FwModule.openSubModuleTab($form, $report);
                FwFormField.setValueByDataField($report, 'QuoteId', quoteId, quoteNumber);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    renderSearchButton($form) {
        var $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.addClass('disabled');
        $search.on('click', function () {
            try {
                let $form = jQuery(this).closest('.fwform');
                let orderId = FwFormField.getValueByDataField($form, 'QuoteId');
                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                }
                else if (!jQuery(this).hasClass('disabled')) {
                    let search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, 'Request');
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    renderSubmitButton($form) {
        var $submit = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Submit Request', 'submitbtn');
        $submit.prepend('<i class="material-icons">publish</i>');
        $submit.addClass('disabled');
        $submit.on('click', function () {
            try {
                let $form = jQuery(this).closest('.fwform');
                let quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                if (quoteId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                }
                else if (!jQuery(this).hasClass('disabled')) {
                    var $confirmation = FwConfirmation.renderConfirmation('Submit Request', 'Would you like to submit this request?');
                    var $submit = FwConfirmation.addButton($confirmation, 'Submit');
                    var $cancel = FwConfirmation.addButton($confirmation, 'Cancel');
                    $submit.on('click', function () {
                        FwAppData.apiMethod(true, 'POST', `api/v1/quote/submit/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                            if (response !== null) {
                                FwNotification.renderNotification('SUCCESS', 'Request Submitted.');
                                FwFormField.setValueByDataField($form, 'Status', response.Status);
                                $form.find('.btn[data-securityid="submitbtn"]').addClass('disabled');
                            }
                        }, null, null);
                    });
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    renderActiveRequest($form) {
        var $activate = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Activate Request', 'activatebtn');
        $activate.prepend('<i class="material-icons">publish</i>');
        $activate.addClass('disabled');
        $activate.on('click', function () {
            try {
                let $form = jQuery(this).closest('.fwform');
                if (!jQuery(this).hasClass('disabled')) {
                    var $confirmation = FwConfirmation.renderConfirmation('Activate Request?', 'Would you like to activate this request?');
                    var $activate = FwConfirmation.addButton($confirmation, 'Activate');
                    var $cancel = FwConfirmation.addButton($confirmation, 'Cancel');
                    $activate.on('click', function () {
                        let quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                        let $quoteTab = jQuery('#' + $form.closest('.tabpage').attr('data-tabid'));
                        FwAppData.apiMethod(true, 'POST', `api/v1/quote/activatequoterequest/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                            FwConfirmation.destroyConfirmation($confirmation);
                            FwTabs.removeTab($quoteTab);
                            let uniqueids = {
                                QuoteId: response.QuoteId
                            };
                            var $quoteform = QuoteController.loadForm(uniqueids);
                            FwModule.openModuleTab($quoteform, "", true, 'FORM', true);
                            FwNotification.renderNotification('SUCCESS', 'Request Activated.');
                        }, null, $confirmation);
                    });
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    loadAudit($form) {
        var uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'OrderStatusHistoryGrid',
            gridSecurityId: 'GVa1yJY2edEl',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'QuoteItemGrid',
            gridSelector: '.rentalgrid div[data-grid="QuoteItemGrid"]',
            gridSecurityId: '0urUow0W9krD',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'R'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid, $browse) => {
                $fwgrid.addClass('R');
                $browse.data('isSummary', false);
            },
            afterDataBindCallback: ($browse, dt) => {
                let rentalItems = $form.find('.rentalgrid tbody').children();
                rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderNoteGrid',
            gridSecurityId: 'LAQkGf7X3I5o',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderContactGrid',
            gridSecurityId: 'MV7F8hvcH8xq',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
    }
    afterLoad($form) {
        let self = this;
        let status = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes = FwFormField.getValueByDataField($form, 'HasNotes');
        let usertype = sessionStorage.getItem('userType');
        if ((status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED') ||
            ((usertype === 'CONTACT') && (status === 'ACTIVE' || status === 'RESERVED')) ||
            ((usertype === 'USER') && (status === 'REQUEST' || status === 'NEW'))) {
            FwModule.setFormReadOnly($form);
        }
        else {
            $form.find('.btn[data-securityid="searchbtn"]').removeClass('disabled');
        }
        if ((usertype === 'CONTACT') && (status === 'NEW')) {
            $form.find('.btn[data-securityid="submitbtn"]').removeClass('disabled');
        }
        if ((usertype === 'USER') && (status === 'REQUEST')) {
            $form.find('.btn[data-securityid="activatebtn"]').removeClass('disabled');
        }
        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }
        var $quoteItemGridRental = $form.find('[data-name="QuoteItemGrid"]');
        FwBrowse.search($quoteItemGridRental);
    }
    events($form) {
        $form
            .on('changeDate', 'div[data-datafield="PickDate"], div[data-datafield="EstimatedStartDate"], div[data-datafield="EstimatedStopDate"]', event => {
            var $element = jQuery(event.currentTarget);
            var PickDate = Date.parse(FwFormField.getValueByDataField($form, 'PickDate'));
            var EstimatedStartDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStartDate'));
            var EstimatedStopDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStopDate'));
            if ($element.attr('data-datafield') === 'EstimatedStartDate' && EstimatedStartDate < PickDate) {
                $form.find('div[data-datafield="EstimatedStartDate"]').addClass('error');
                FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before 'Pick Date'.");
            }
            else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStartDate < PickDate) {
                $form.find('div[data-datafield="PickDate"]').addClass('error');
                FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'From Date'.");
            }
            else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStopDate < PickDate) {
                $form.find('div[data-datafield="PickDate"]').addClass('error');
                FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'To Date'.");
            }
            else if (EstimatedStopDate < EstimatedStartDate) {
                $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
            }
            else if (EstimatedStopDate < PickDate) {
                $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'Pick Date'.");
            }
            else {
                $form.find('div[data-datafield="PickDate"]').removeClass('error');
                $form.find('div[data-datafield="EstimatedStartDate"]').removeClass('error');
                $form.find('div[data-datafield="EstimatedStopDate"]').removeClass('error');
            }
        })
            .on('click', '[data-type="tab"]', event => {
            if ($form.attr('data-mode') !== 'NEW') {
                const $tab = jQuery(event.currentTarget);
                const tabpageid = jQuery(event.currentTarget).data('tabpageid');
                if ($tab.hasClass('audittab') == false) {
                    const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
                    if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                        for (let i = 0; i < $gridControls.length; i++) {
                            try {
                                const $gridcontrol = jQuery($gridControls[i]);
                                FwBrowse.search($gridcontrol);
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        }
                    }
                    const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                    if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                        for (let i = 0; i < $browseControls.length; i++) {
                            const $browseControl = jQuery($browseControls[i]);
                            FwBrowse.search($browseControl);
                        }
                    }
                }
                $tab.addClass('tabGridsLoaded');
            }
        });
    }
    browseCancelOption($browse) {
        try {
            let $confirmation, $yes, $no;
            const quoteId = $browse.find('.selected [data-browsedatafield="QuoteId"]').attr('data-originalvalue');
            const quoteStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
            if (quoteId != null) {
                if (quoteStatus === "CANCELLED") {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    const html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to un-cancel this Quote?</div>');
                    html.push('  </div>');
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Quote', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');
                    $yes.on('click', uncancelQuote);
                }
                else {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to cancel this Quote?</div>');
                    html.push('  </div>');
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Cancel Quote', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');
                    $yes.on('click', cancelQuote);
                }
                function cancelQuote() {
                    let request = {};
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Canceling...');
                    $yes.off('click');
                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Quote Successfully Cancelled');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', cancelQuote);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                }
                ;
                function uncancelQuote() {
                    let request = {};
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Retrieving...');
                    $yes.off('click');
                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Quote Successfully Retrieved');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', uncancelQuote);
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
                FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    copyOrderOrQuote($form) {
        const module = this.Module;
        const $confirmation = FwConfirmation.renderConfirmation(`Copy ${module}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="" style="width:90px; float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:340px;float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy To" data-datafield="CopyTo">');
        html.push('      <div data-value="Q" data-caption="Quote"> </div>');
        html.push('    <div data-value="O" data-caption="Order"> </div></div><br>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('</div>');
        FwConfirmation.addControls($confirmation, html.join(''));
        $confirmation.find('div[data-caption="Type"] input').val(module);
        const orderNumber = FwFormField.getValueByDataField($form, `${module}Number`);
        $confirmation.find('div[data-caption="No"] input').val(orderNumber);
        const deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="Deal"] input').val(deal);
        const description = FwFormField.getValueByDataField($form, 'Description');
        $confirmation.find('div[data-caption="Description"] input').val(description);
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
        const dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-value').val(dealId);
        if (module === 'Order') {
            $confirmation.find('div[data-datafield="CopyTo"] [data-value="O"] input').prop('checked', true);
        }
        ;
        FwFormField.disable($confirmation.find('div[data-caption="Type"]'));
        FwFormField.disable($confirmation.find('div[data-caption="No"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Deal"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Description"]'));
        $confirmation.find('div[data-datafield="CopyRatesFromInventory"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDates"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyLineItemNotes"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CombineSubs"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDocuments"] input').prop('checked', true);
        const $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.on('click', makeACopy);
        function makeACopy() {
            const request = {};
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
            for (let key in request) {
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
            const $confirmationbox = jQuery('.fwconfirmationbox');
            const orderId = FwFormField.getValueByDataField($form, `${module}Id`);
            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/copyto${(request.CopyToType === "Q" ? "quote" : "order")}/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Copied`);
                FwConfirmation.destroyConfirmation($confirmation);
                let $control;
                const uniqueids = {};
                if (request.CopyToType == "O") {
                    uniqueids.OrderId = response.OrderId;
                    uniqueids.OrderTypeId = response.OrderTypeId;
                    $control = OrderController.loadForm(uniqueids);
                }
                else if (request.CopyToType == "Q") {
                    uniqueids.QuoteId = response.QuoteId;
                    uniqueids.OrderTypeId = response.OrderTypeId;
                    $control = QuoteController.loadForm(uniqueids);
                }
                FwModule.openModuleTab($control, "", true, 'FORM', true);
            }, function onError(response) {
                $yes.on('click', makeACopy);
                $yes.text('Copy');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
            }, $confirmationbox);
        }
        ;
    }
    ;
    printQuoteOrder($form) {
        try {
            var module = this.Module;
            var orderNumber = FwFormField.getValue($form, `div[data-datafield="${module}Number"]`);
            var orderId = FwFormField.getValue($form, `div[data-datafield="${module}Id"]`);
            var recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
            var $report = (module === 'Order') ? OrderReportController.openForm() : QuoteReportController.openForm();
            FwModule.openSubModuleTab($form, $report);
            FwFormField.setValue($report, `div[data-datafield="${module}Id"]`, orderId, orderNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);
            var printTab = jQuery('.tab.submodule.active');
            printTab.find('.caption').html(`Print ${module}`);
            printTab.attr('data-caption', `${module} ${recordTitle}`);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    createOrder($form) {
        const status = FwFormField.getValueByDataField($form, 'Status');
        if ((status === 'ACTIVE') || (status === 'RESERVED')) {
            const quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
            const $confirmation = FwConfirmation.renderConfirmation('Create Order', `<div>Create Order for Quote ${quoteNumber}?</div>`);
            const $yes = FwConfirmation.addButton($confirmation, 'Create Order', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');
            $yes.on('click', function () {
                const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
                FwAppData.apiMethod(true, 'POST', `api/v1/quote/createorder/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwConfirmation.destroyConfirmation($confirmation);
                    const $quoteTab = jQuery(`#${$form.closest('.tabpage').attr('data-tabid')}`);
                    FwTabs.removeTab($quoteTab);
                    const uniqueids = {
                        OrderId: response.OrderId
                    };
                    const $orderform = OrderController.loadForm(uniqueids);
                    FwModule.openModuleTab($orderform, "", true, 'FORM', true);
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Created.');
                }, null, realConfirm);
            });
        }
        else {
            FwNotification.renderNotification('WARNING', 'Can only convert an "ACTIVE" or "RESERVED" Quote to an Order.');
        }
    }
}
var QuoteController = new Quote();
//# sourceMappingURL=Quote.js.map