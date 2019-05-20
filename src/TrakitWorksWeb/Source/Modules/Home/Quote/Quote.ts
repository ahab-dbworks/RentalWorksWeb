routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return QuoteController.getModuleScreen(filter); } });

class Quote extends OrderBase {
    Module:             string = 'Quote';
    apiurl:             string = 'api/v1/quote';
    caption: string = Constants.Modules.Home.Quote.caption;
	nav: string = Constants.Modules.Home.Quote.nav;
	id: string = Constants.Modules.Home.Quote.id;
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var self        = this;
        var screen: any = {};
        screen.$view    = FwModule.getModuleControl(this.Module + 'Controller');

        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

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
    //----------------------------------------------------------------------------------------------
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
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        var department = JSON.parse(sessionStorage.getItem('department'));;
        var location   = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const $all:       JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $prospect:  JQuery = FwMenu.generateDropDownViewBtn('Prospect', true, "PROSPECT");
        const $active:    JQuery = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $reserved:  JQuery = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
        const $ordered:   JQuery = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
        const $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed:    JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        FwMenu.addVerticleSeparator($menuObject);

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $prospect, $active, $reserved, $ordered, $cancelled, $closed);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location      = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");

        return $menuObject;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            var today      = FwFunc.getDate();
            var warehouse  = JSON.parse(sessionStorage.getItem('warehouse'));
            var office     = JSON.parse(sessionStorage.getItem('location'));
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
        }

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
        }

        this.events($form);
        this.renderPrintButton($form);
        this.renderSearchButton($form);
        //this.activityCheckboxEvents($form, mode);
        //if (typeof parentModuleInfo !== 'undefined' && mode !== 'NEW') {
            //this.renderFrames($form, parentModuleInfo.QuoteId);
            //this.dynamicColumns($form, parentModuleInfo.OrderTypeId);
        //}

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', function () {
            try {
                var $form       = jQuery(this).closest('.fwform');
                var quoteNumber = FwFormField.getValue($form, 'div[data-datafield="QuoteNumber"]');
                var quoteId     = FwFormField.getValue($form, 'div[data-datafield="QuoteId"]');
                var $report     = RwQuoteReportController.openForm();

                FwModule.openSubModuleTab($form, $report);

                FwFormField.setValue($report, 'div[data-datafield="QuoteId"]', quoteId, quoteNumber);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderSearchButton($form: any) {
        var self = this;
        var $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Search');
        $search.prepend('<i class="material-icons">search</i>');
        $search.on('click', function () {
            try {
                let $form   = jQuery(this).closest('.fwform');
                let orderId = FwFormField.getValueByDataField($form, 'QuoteId');
                
                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else {
                    let search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, self.Module);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let self        = this;
        var totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]

        var $orderStatusHistoryGrid        = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        var $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: $form.find('div.fwformfield[data-datafield="QuoteId"] input').val()
            };
        })
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);

        var $orderItemGridRental        = $form.find('div[data-grid="OrderItemGrid"]');
        var $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRental.addClass('R');
        $orderItemGridRentalControl.data('isSummary', false);

        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'R'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'R';
        });
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', ($control, dt) => {
            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });
        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        var $orderNoteGrid        = $form.find('div[data-grid="OrderNoteGrid"]');
        var $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
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

        var $orderContactGrid        = $form.find('div[data-grid="OrderContactGrid"]');
        var $orderContactGridControl = jQuery(jQuery('#tmpl-grids-OrderContactGridBrowse').html());
        $orderContactGrid.empty().append($orderContactGridControl);
        $orderContactGridControl.find('div[data-datafield="IsOrderedBy"]').attr('data-caption', 'Quoted For');
        $orderContactGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
            };
        });
        $orderContactGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($orderContactGridControl);
        FwBrowse.renderRuntimeHtml($orderContactGridControl);

        let itemGrids = [$orderItemGridRental];
        if ($form.attr('data-mode') === 'NEW') {
            for (var i = 0; i < itemGrids.length; i++) {
                itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
                    .off()
                    .on('click', function () {
                        self.saveForm($form, { closetab: false });
                    })
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        let status      = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes    = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab   = $form.find('[data-type="tab"][data-caption="Rental"]');

        if ($form.find('[data-datafield="CombineActivity"] input').val() === 'false') {
            if (!FwFormField.getValueByDataField($form, 'Rental')) { rentalTab.hide(), FwFormField.disable($form.find('[data-datafield="RentalSale"]')); }
        }

        if (status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED') {
            FwModule.setFormReadOnly($form);
        }

        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }

        //hide subworksheet and add LD items
        var $orderItemGridRental    = $form.find('[data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridRental);
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form
            .on('changeDate', 'div[data-datafield="PickDate"], div[data-datafield="EstimatedStartDate"], div[data-datafield="EstimatedStopDate"]', event => {
                var $element           = jQuery(event.currentTarget);
                var PickDate           = Date.parse(FwFormField.getValueByDataField($form, 'PickDate'));
                var EstimatedStartDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStartDate'));
                var EstimatedStopDate  = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStopDate'));

                if ($element.attr('data-datafield') === 'EstimatedStartDate' && EstimatedStartDate < PickDate) {
                    $form.find('div[data-datafield="EstimatedStartDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before 'Pick Date'.");
                } else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStartDate < PickDate) {
                    $form.find('div[data-datafield="PickDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'From Date'.");
                } else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStopDate < PickDate) {
                    $form.find('div[data-datafield="PickDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'To Date'.");
                } else if (EstimatedStopDate < EstimatedStartDate) {
                    $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                } else if (EstimatedStopDate < PickDate) {
                    $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'Pick Date'.");
                } else {
                    $form.find('div[data-datafield="PickDate"]').removeClass('error');
                    $form.find('div[data-datafield="EstimatedStartDate"]').removeClass('error');
                    $form.find('div[data-datafield="EstimatedStopDate"]').removeClass('error');
                }
            })
        ;
    }
    //----------------------------------------------------------------------------------------------
}

//----------------------------------------------------------------------------------------------
//Copy Quote
FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.CopyQuote.id] = function (event) {
    try {
        var $form           = jQuery(this).closest('.fwform');
        const $confirmation = FwConfirmation.renderConfirmation('Copy Request', '');
        const $yes          = FwConfirmation.addButton($confirmation, 'Copy', false);
        const $no           = FwConfirmation.addButton($confirmation, 'Cancel');
        const html          = [];
        html.push('<div class="fwform" data-controller="none">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="RequestNo" data-enabled="false" style="width:90px;float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="RequestDescription" data-enabled="false" style="width:340px;float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('  </div>');
        html.push('</div>');
        FwConfirmation.addControls($confirmation, html.join(''));

        FwFormField.setValue($confirmation, 'div[data-datafield="RequestNo"]', FwFormField.getValueByDataField($form, 'QuoteNumber'));
        FwFormField.setValue($confirmation, 'div[data-datafield="RequestDescription"]', FwFormField.getValueByDataField($form, 'Description'));
        FwFormField.setValue($confirmation, 'div[data-datafield="CopyDates"]', true);
        FwFormField.setValue($confirmation, 'div[data-datafield="CopyLineItemNotes"]', true);

        $yes.on('click', function makeACopy () {
            const request: any = {
                CopyToType:             'Q',
                CopyToDealId:           '',
                CopyDates:              (FwFormField.getValueByDataField($confirmation, 'CopyDates') === 'T' ? 'True' : 'False'),
                CopyLineItemNotes:      (FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes') === 'T' ? 'True' : 'False'),
                CopyRatesFromInventory: 'False',
                CombineSubs:            'False',
                CopyDocuments:          'False'
            };

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');
            const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            FwAppData.apiMethod(true, 'POST', `api/v1/quote/copytoquote/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Request Successfully Copied');
                FwConfirmation.destroyConfirmation($confirmation);
                const uniqueids: any = {
                    QuoteId:     response.QuoteId,
                    OrderTypeId: response.OrderTypeId
                };
                let $control = QuoteController.loadForm(uniqueids);
                FwModule.openModuleTab($control, "", true, 'FORM', true);
            }, function onError(response) {
                $yes.on('click', makeACopy);
                $yes.text('Copy');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
            }, $confirmation);
        });
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Search
FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.Search.id] = function (event) {
    let search, $form, quoteId;
    $form = jQuery(this).closest('.fwform');
    quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

    if ($form.attr('data-mode') === 'NEW') {
        QuoteController.saveForm($form, { closetab: false });
        return;
    }

    if (quoteId == "") {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    } else {
        search = new SearchInterface();
        search.renderSearchPopup($form, quoteId, 'Quote');
    }
};
//----------------------------------------------------------------------------------------------
//Print Quote
FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.PrintQuote.id] = function (e) {
    try {
        var $form       = jQuery(this).closest('.fwform');
        var quoteNumber = FwFormField.getValue($form, 'div[data-datafield="QuoteNumber"]');
        var quoteId     = FwFormField.getValue($form, 'div[data-datafield="QuoteId"]');
        var $report     = RwQuoteReportController.openForm();

        FwModule.openSubModuleTab($form, $report);

        FwFormField.setValue($report, 'div[data-datafield="QuoteId"]', quoteId, quoteNumber);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//New Version
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.NewVersion.id] = function (event) {
//    let $form         = jQuery(this).closest('.fwform');
//    let quoteNumber   = FwFormField.getValueByDataField($form, 'QuoteNumber');
//    let quoteId       = FwFormField.getValueByDataField($form, 'QuoteId');
//    var $confirmation = FwConfirmation.renderConfirmation('Create New Version', `<div>Create New Version for Quote ${quoteNumber}?</div>`);
//    var $yes          = FwConfirmation.addButton($confirmation, 'Create New Version', false);
//    var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//    $yes.on('click', function () {
//        FwAppData.apiMethod(true, 'POST', `api/v1/quote/createnewversion/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
//            FwNotification.renderNotification('SUCCESS', 'New Version Successfully Created.');
//            FwConfirmation.destroyConfirmation($confirmation);
//            let uniqueids: any = {
//                QuoteId: response.QuoteId
//            };
//            var $quoteform = QuoteController.loadForm(uniqueids);
//            FwModule.openModuleTab($quoteform, "", true, 'FORM', true);

//            FwModule.refreshForm($form, QuoteController);
//        }, null, $confirmation);
//    });
//};
//----------------------------------------------------------------------------------------------
//Cancel / Uncancel - Form
FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.CancelUncancel.id] = function (event) {
    try {
        let $confirmation, $yes, $no;
        let $form       = jQuery(this).closest('.fwform');
        var self        = this;
        var id          = FwFormField.getValueByDataField($form, 'QuoteId');
        var orderStatus = FwFormField.getValueByDataField($form, 'Status');

        if (id != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to un-cancel this request?</div>');
                $yes          = FwConfirmation.addButton($confirmation, 'Un-Cancel Request', false);
                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', function uncancelOrder() {
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Retrieving...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${id}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Request Successfully Retrieved');
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
                });
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to cancel this request?</div>');
                $yes          = FwConfirmation.addButton($confirmation, 'Cancel Request', false);
                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', function cancelOrder() {
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Canceling...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${id}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Request Successfully Cancelled');
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
                });
            }
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Create Order
FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.CreateOrder.id] = function (event) {
    let $form         = jQuery(this).closest('.fwform');
    let quoteNumber   = FwFormField.getValueByDataField($form, 'QuoteNumber');
    var $confirmation = FwConfirmation.renderConfirmation('Create Order', `<div>Create Order for Request ${quoteNumber}?</div>`);
    var $yes          = FwConfirmation.addButton($confirmation, 'Create Order', false);
    var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

    $yes.on('click', function () {
        var quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        FwAppData.apiMethod(true, 'POST', `api/v1/quote/createorder/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            FwNotification.renderNotification('SUCCESS', 'Order Successfully Created.');
            FwConfirmation.destroyConfirmation($confirmation);
            let uniqueids: any = {
                OrderId: response.OrderId
            };
            var $orderform = OrderController.loadForm(uniqueids);
            FwModule.openModuleTab($orderform, "", true, 'FORM', true);

            FwModule.refreshForm($form, QuoteController);
        }, null, $confirmation);
    });
};
//----------------------------------------------------------------------------------------------
//Cancel / Uncancel - Browse
FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.browse.menuItems.CancelUncancel.id] = function (event) {
    try {
        let $confirmation, $yes, $no;
        var $browse     = jQuery(this).closest('.fwbrowse');
        var quoteId     = $browse.find('.selected [data-browsedatafield="QuoteId"]').attr('data-originalvalue');
        var quoteStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');

        if (quoteId != null) {
            if (quoteStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to un-cancel this Quote?</div>');
                $yes          = FwConfirmation.addButton($confirmation, 'Un-Cancel Quote', false);
                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', function uncancelQuote() {
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Retrieving...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${quoteId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
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
                });
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to cancel this Quote?</div>');
                $yes          = FwConfirmation.addButton($confirmation, 'Cancel Quote', false);
                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', function cancelQuote() {
                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Canceling...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
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
                });
            }
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------

var QuoteController = new Quote();