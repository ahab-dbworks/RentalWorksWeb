//----------------------------------------------------------------------------------------------
RwOrderController.getOrderSuspendedSessionPopup = function(suspendedInContracts) {'use strict';
    var result, html, statusdate, sessionno, orderno, orderdesc, deal, username, status, rowView, rowModel;
    
    result = {};
    rowView = jQuery('#tmpl-checkIn-suspendedSession').html();
    html = [];
    html.push('<div class="checkin-suspendedsessions">');
    if (typeof suspendedInContracts === 'object') {
        for (var rowno = 0; rowno < suspendedInContracts.Rows.length; rowno++) {
            rowModel = {};
            rowModel.statusdate = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.statusdate];
            rowModel.sessionno  = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.sessionno];
            rowModel.orderno    = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.orderno];
            rowModel.orderdesc  = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.orderdesc];
            rowModel.deal       = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.deal];
            rowModel.username   = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.username];
            rowModel.status     = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.status];
            html.push(Mustache.render(rowView, rowModel));
        }
    }
    html.push('</div>');
    result.$popup = FwConfirmation.renderConfirmation(suspendedInContracts.Rows.length.toString() + ' Suspended Session' + ((suspendedInContracts.Rows.length === 1) ? '' : 's'), html.join(''));
    result.$popup.attr('data-nopadding', 'true');
    result.$btnJoinSession = FwConfirmation.addButton(result.$popup, 'Join Session', false);
    result.$btnJoinSession.on('click', function() {
        var $suspendedsession;
        try {
            $suspendedsession = result.$popup.find('.checkin-suspendedsession');
            if ($suspendedsession.length === 1) {
                $suspendedsession.click();
            } else if ($suspendedsession.length > 1) {
                FwFunc.showMessage('Please click one of the Suspended Sessions in the popup below.');
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
    result.$btnNewSession  = FwConfirmation.addButton(result.$popup, 'New Session', true);
    FwConfirmation.addButton(result.$popup, 'Cancel', true);

    return result;
};
//---------------------------------------------------------------------------------------------- 
RwOrderController.getCheckInScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, pageSubTitle, requestOrdertranExists, applicationOptions, $checkInSerial;
    applicationOptions = application.getApplicationOptions();
    if (typeof properties.orderId      === 'undefined') {properties.orderId      = '';}
    if (typeof properties.dealId       === 'undefined') {properties.dealId       = '';}
    if (typeof properties.departmentId === 'undefined') {properties.departmentId = '';}
    if (typeof properties.orderDesc    === 'undefined') {properties.orderDesc    = '';}
    if (typeof properties.contractId   === 'undefined') {properties.contractId   = '';}
    switch(properties.checkInMode) {
        case RwConstants.checkInModes.SingleOrder:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + properties.selectedorder.orderno + ' - ' + properties.selectedorder.orderdesc + ' (' + RwLanguages.translate('Session') + ': ' + properties.selectedorder.sessionno + ')</div>';
            properties.orderId      = properties.selectedorder.orderid;
            properties.dealId       = properties.selectedorder.dealid;
            properties.departmentId = properties.selectedorder.departmentid;
            properties.orderDesc    = properties.selectedorder.orderdesc;
            properties.contractId   = properties.selectedorder.contractid;
            break;
        case RwConstants.checkInModes.MultiOrder:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + RwLanguages.translate('Multi-Order') + '<br/>'  + RwLanguages.translate('First scan must be a Bar Code...') + '</div>';
            break;
        case RwConstants.checkInModes.Session:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + properties.selectedsession.orderno + ' - ' + properties.selectedsession.orderdesc + ' (' + RwLanguages.translate('Session') + ': ' + properties.selectedsession.sessionno + ')</div>';
            properties.orderId      = properties.selectedsession.orderid;
            properties.dealId       = properties.selectedsession.dealid;
            properties.departmentId = properties.selectedsession.departmentid;
            properties.orderDesc    = properties.selectedsession.orderdesc;
            properties.contractId   = properties.selectedsession.contractid;
            break;
        case RwConstants.checkInModes.Deal:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + properties.selecteddeal.dealno + ' - ' + properties.selecteddeal.deal + '</div>';
            properties.dealId       = properties.selecteddeal.dealid;
            properties.departmentId = properties.selecteddeal.departmentid;
            break;  
    }
    combinedViewModel = jQuery.extend({
        captionPageTitle:          RwOrderController.getPageTitle(properties)
      , captionPageSubTitle:       pageSubTitle
      , htmlScanBarcode:           RwPartialController.getScanBarcodeHtml({
            captionInstructions: RwLanguages.translate('Select Item to Check-In...'),
            captionBarcodeICode: RwLanguages.translate('Bar Code / I-Code')
        })
      , captionDesc:               RwLanguages.translate('Description')
      , captionQty:                RwLanguages.translate('Qty')
      , captionSummary:            RwLanguages.translate('Summary')
      , captionCreateContract:     RwLanguages.translate('Create Contract')
      , captionPendingListButton:  RwLanguages.translate('Pending')
      , captionScanButton:         RwLanguages.translate('Scan')
      , captionRFIDButton:         RwLanguages.translate('RFID')
      , captionSessionInButton:    RwLanguages.translate('Session In')
      , captionApplyAllQtyItems:   RwLanguages.translate('Apply All Qty Items')
      , captionFillContainer:      RwLanguages.translate('Fill Container')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-checkIn').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    screen.properties = properties;

    screen.properties.aisle = '';
    screen.properties.shelf = '';

    screen.getOrderId = function() {
        return properties.orderId;
    };

    screen.getDealId = function() {
        return properties.dealId;
    };

    screen.getDepartmentId = function() {
        return properties.departmentId;
    };

    screen.getOrderDesc = function() {
        return properties.orderDesc;
    };

    screen.getContractId = function() {
        return properties.contractId;
    };
    
    screen.$view.find('#checkin-pendingList-pnlApplyAllQtyItems').toggle(false);

    //if ((sessionStorage.userType === 'USER') && (typeof applicationOptions.container !== 'undefined') && (applicationOptions.container.enabled)) {
    //    screen.$view.find('.container .fillcontainertoolbar').show();
    //}

    screen.$btnclose = FwMobileMasterController.addFormControl(screen, 'Close', 'left', 'back', true, function () {
        application.navigate('order/checkinmenu');
    });

    screen.$btncreatecontract = FwMobileMasterController.addFormControl(screen, 'Create Contract', 'right', 'continue', true, function () {
        try {
            var request = {
                contractid: screen.getContractId()
            };
            RwServices.call('CheckIn', 'GetShowCreateContract', request, function (response) {
                try {
                    if (response.showcreatecontract) {
                        properties.contract = {
                            contractType: 'IN'
                            , contractId: screen.getContractId()
                            , orderId: screen.getOrderId()
                            , responsiblePersonId: ''
                        };
                        application.pushScreen(RwOrderController.getContactSignatureScreen(viewModel, properties));
                    } else {
                        FwFunc.showMessage("There is no activity on this Check-In Session!");
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabpending = FwMobileMasterController.tabcontrols.addtab('Pending', true);
    screen.$tabpending.on('click', function () {
        try {
            var request;
            jQuery('#checkIn-scan').hide();
            jQuery('#checkIn-sessionInList').hide();
            jQuery('#checkIn-pendingList').show();
            jQuery('#checkIn-rfid').hide();
            jQuery('#checkIn-scan').attr('data-mode', 'PENDINGLIST');
            request = {
                contractId: screen.getContractId()
            };
            RwServices.order.getCheckInPendingList(request, screen.getCheckInPendingListCallback);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabrfid = FwMobileMasterController.tabcontrols.addtab('RFID', false).hide();
    screen.$tabrfid.on('click', function () {
        try {
            var request;
                jQuery('#checkIn-sessionInList').hide();
                jQuery('#checkIn-pendingList').hide();
                jQuery('#checkIn-scan').hide();
                jQuery('#checkIn-rfid').show();
                jQuery('#checkIn-scan').attr('data-mode', 'RFID');
                RwRFID.registerEvents(screen.rfidscan);
                request = {
                    rfidmode:  'CHECKIN',
                    sessionid:  screen.getContractId(),
                    portal:     device.uuid
                };
                RwServices.order.getRFIDStatus(request, function(response) {
                    screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                    screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                    (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                    (response.pending.length > 0)    ? screen.$view.find('.btnpending').show()   : screen.$view.find('.btnpending').hide();
                });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabsessionin = FwMobileMasterController.tabcontrols.addtab('Session In', false);
    screen.$tabsessionin.on('click', function () {
        var request;
        try {
            jQuery('#checkIn-scan').hide();
            jQuery('#checkIn-pendingList').hide();
            jQuery('#checkIn-rfid').hide();
            jQuery('#checkIn-sessionInList').show();
            jQuery('#checkIn-scan').attr('data-mode', 'SESSIONINLIST');
            RwRFID.unregisterEvents();
            request = {
                contractId:  screen.getContractId()
            };
            RwServices.order.getCheckInSessionInList(request, function(response) {
                var items, ul, li, isHeaderRow, cssClass, lineitemcount=0, totalitems=0;
                items = response.getCheckInSessionInList; 
                ul = [];
                for (var i = 0; i < items.length; i++) {
                    isHeaderRow = ((items[i].itemclass === 'N') || (items[i].sessionin === 0));
                    if (!isHeaderRow) {
                        lineitemcount++;
                    }
                    totalitems += items[i].sessionin;
                    cssClass = '';
                    if (!isHeaderRow) {
                        if (cssClass.length > 0) {
                            cssClass += ' ';
                        }
                        cssClass += 'link';
                    }
                    if (cssClass.length > 0) {
                        cssClass += ' ';
                    }
                    cssClass += ' itemclass-' + items[i].itemclass;
                    li = [];
                    li.push('<li class="' + cssClass +  
                        '" data-rentalitemid="' + items[i].rentalitemid + 
                        '" data-orderid="'      + items[i].orderid + 
                        '" data-masteritemid="' + items[i].masteritemid + 
                        '" data-masterid="'     + items[i].masterid + 
                        '" data-vendorid="'     + items[i].vendorid +
                        '" data-consignorid="'  + items[i].consignorid +
                        '" data-description="'  + FwFunc.htmlEscape(items[i].description) +
                        '" data-ordertranid="'  + items[i].ordertranid +
                        '" data-internalchar="' + items[i].internalchar +
                        '" data-sessionin="'    + items[i].sessionin +
                        '" data-trackedby="'    + items[i].trackedby +
                        '">');
                        li.push('<div class="description">' + items[i].description + '</div>');
                        if (isHeaderRow) {
                            li.push('<table style="display:none;">');
                        } else {
                            li.push('<table>');
                        }
                            li.push('<tbody>');
                                li.push('<tr>');
                                    li.push('<td class="col1 key masterno">' + RwLanguages.translate('I-Code') + ':</td>');
                                    li.push('<td class="col2 value masterno">' + items[i].masterno + '</td>');
                                    li.push('<td class="col3 key sessionin">' + RwLanguages.translate('Session In') + ':</td>');
                                    li.push('<td class="col4 value sessionin">' + String(items[i].sessionin) + '</td>');
                                li.push('</tr>');
                                if (items[i].barcode !== '') {
                                    li.push('<tr class="">');
                                        li.push('<td class="col1 key barcode">' + RwLanguages.translate('Bar Code') + ':</td>');
                                        li.push('<td colspan="3" class="value barcode">' + items[i].barcode + '</td>');
                                    li.push('</tr>');
                                }
                                li.push('<tr>');
                                    li.push('<td class="col1 key orderno">' + RwLanguages.translate('Order No') + ':</td>');
                                    li.push('<td class="col2 value orderno">' + items[i].orderno + '</td>');
                                    li.push('<td class="col3 key qtyordered">' + RwLanguages.translate('Ordered') + ':</td>');
                                    li.push('<td class="col4 value qtyordered">' + String(items[i].qtyordered) + '</td>');
                                li.push('</tr>');
                                if (items[i].vendorid !== '') {
                                    li.push('<tr>');
                                        li.push('<td class="col1 key vendor">' + RwLanguages.translate('Vendor/Consignor') + ':</td>');
                                        li.push('<td class="col2 value vendor" colspan="3"><div class="vendor">' + items[i].vendor + '</div></td>');
                                    li.push('</tr>');
                                }
                            li.push('</tbody>');
                        li.push('</table>');
                    li.push('</li>');
                    ul.push(li.join(''));
                }
                ul.push(screen.getRowCountItem(lineitemcount, totalitems));
                jQuery('#checkIn-sessionInList-ul').html(ul.join(''));
                //screen.$btncreatecontract.toggle((applicationConfig.designMode) || ((sessionStorage.users_enablecreatecontract === 'T') && (items.length > 0)));
            });
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.toggleRfid = function() {
        if (RwRFID.isConnected) {
            //screen.$view.find('#checkIn-btnRFID').css('display', 'inline-block');
            //screen.$view.find('#checkIn-btnRFID').click();
            screen.$tabrfid.show().click();
            var requestRFIDClear = {};
            requestRFIDClear.sessionid = screen.getContractId();
            RwServices.order.rfidClearSession(requestRFIDClear, function(response) {});
        } else {
            //screen.$view.find('#checkIn-btnRFID').css('display', 'none');
            //screen.$view.find('#checkIn-btnScan').click();
            screen.$tabrfid.hide();
            screen.$tabpending.click();
        }
    };
    //screen.toggleRfid();

    if ((screen.getContractId() === '') && (properties.checkInMode === RwConstants.checkInModes.SingleOrder)) { 
        RwServices.order.createInContract({
                orderId:      screen.getOrderId()
              , dealId:       screen.getDealId()
              , departmentId: screen.getDepartmentId()
            }, function(response) {
                properties.contractId = response.createInContract.contractId;
            })
        ;
    }

    screen.$view.find('#master-header-row4').toggle((applicationConfig.designMode) || (screen.getContractId() !== ''));
    
    screen.showPopupQty = function() {
        FwPopup.showPopup(screen.$popupQty);
    };
    screen.hidePopupQty = function() {
        FwPopup.destroyPopup(screen.$popupQty);
        jQuery('#checkIn-txtBarcodeData').val('');
    };
    
    screen.checkInItem = function(orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId) {
        var requestCheckInItem = {
              moduleType:     properties.moduleType
            , checkInMode:    properties.checkInMode
            , contractId:     screen.getContractId()
            , orderId:        orderId
            , dealId:         screen.getDealId()
            , departmentId:   screen.getDepartmentId()
            , vendorId:       vendorId
            , consignorId:    ''
            , description:    ''
            , internalChar:   ''
            , masterItemId:   masterItemId
            , masterId:       masterId
            , parentId:       ''
            , code:           RwAppData.stripBarcode(code.toUpperCase())
            , qty:            qty
            , newOrderAction: newOrderAction
            , aisle:          aisle
            , shelf:          shelf 
            , playStatus:     playStatus
            //, getSuspenededSessions: true
        };
        RwServices.order.checkInItem(requestCheckInItem, function(responseCheckInItem) {
            properties.responseCheckInItem = responseCheckInItem;
            screen.checkInItemCallback(responseCheckInItem);
        });
    };

    screen.checkInItemCallback = function(responseCheckInItem) {
        try {
            if ((typeof properties.contract !== 'object') && (typeof responseCheckInItem.suspendedInContracts === 'object') && (responseCheckInItem.suspendedInContracts.Rows.length > 0)) {
                suspendedContractsPopup = RwOrderController.getOrderSuspendedSessionPopup(responseCheckInItem.suspendedInContracts);
                suspendedContractsPopup.$popup.on('click', '.checkin-suspendedsession', function() {
                    var requestSelectSession, $suspendedsession, sessionno;
                    try {
                        $suspendedsession = jQuery(this);
                        requestSelectSession = {};
                        requestSelectSession.sessionNo  = $suspendedsession.find('.checkin-suspendedsession-sessionno-value').text();
                        requestSelectSession.moduleType = properties.moduleType;
                        properties.checkInMode = 'Session';
                        RwServices.order.selectSession(requestSelectSession, function(responseSelectSession) {
                            var requestCheckInItem2;
                            try {
                                properties.webSelectSession = responseSelectSession.webSelectSession;
                                if (((properties.moduleType === RwConstants.moduleTypes.Order) || (properties.moduleType === RwConstants.moduleTypes.Transfer)) && 
                                    (properties.activityType === RwConstants.activityTypes.CheckIn)) {
                                    checkInItemScreen_viewModel = {};
                                    checkInItemScreen_properties = jQuery.extend({}, properties, {
                                        webSelectSession: properties.webSelectSession
                                    });
                                    checkInItemScreen = RwOrderController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                    application.updateScreen(checkInItemScreen);
                                    requestCheckInItem2 = responseCheckInItem.request;
                                    requestCheckInItem2.contractId = responseSelectSession.webSelectSession.contractId;
                                    RwServices.order.checkInItem(responseCheckInItem.request, function(responseCheckInItem2) {
                                        properties.responseCheckInItem = responseCheckInItem2;
                                        screen.checkInItemCallback(responseCheckInItem2);
                                    });
                                } else {
                                    throw 'Not implemented for moduleType: ' + properties.moduleType;
                                }
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
                suspendedContractsPopup.$btnNewSession.on('click', function() {
                    var requestCreateSession;
                    try {
                        requestCreateSession = {
                            orderid: '',
                            dealid: '',
                            departmentid: ''
                        };
                        if (typeof properties.webSelectOrder === 'object') {
                            requestCreateSession.orderid      = properties.webSelectOrder.orderId;
                            requestCreateSession.dealid       = properties.webSelectOrder.dealId;
                            requestCreateSession.departmentid = properties.webSelectOrder.departmentId;
                        }
                        else if (typeof responseCheckInItem.webGetItemStatus === 'object') {
                            requestCreateSession.orderid      = responseCheckInItem.webGetItemStatus.orderid;
                            requestCreateSession.dealid       = responseCheckInItem.webGetItemStatus.dealid;
                            requestCreateSession.departmentid = responseCheckInItem.webGetItemStatus.departmentid;
                        }
                        RwServices.order.createNewInContractAndSuspend(requestCreateSession, function(responseCreateSession) {
                            var requestSelectSession, $suspendedsession, sessionno;
                            try {
                                $suspendedsession = jQuery(this);
                                requestSelectSession = {};
                                requestSelectSession.sessionNo  = responseCreateSession.contract.sessionNo;
                                requestSelectSession.moduleType = properties.moduleType;
                                properties.checkInMode = 'Session';
                                RwServices.order.selectSession(requestSelectSession, function(responseSelectSession) {
                                    var requestCheckInItem2;
                                    try {
                                        properties.webSelectSession = responseSelectSession.webSelectSession;
                                        if (((properties.moduleType === RwConstants.moduleTypes.Order) || (properties.moduleType === RwConstants.moduleTypes.Transfer)) && 
                                            (properties.activityType === RwConstants.activityTypes.CheckIn)) {
                                            checkInItemScreen_viewModel = {};
                                            checkInItemScreen_properties = jQuery.extend({}, properties, {
                                                webSelectSession: properties.webSelectSession
                                            });
                                            checkInItemScreen = RwOrderController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                            application.updateScreen(checkInItemScreen);
                                            requestCheckInItem2 = responseCheckInItem.request;
                                            requestCheckInItem2.contractId = responseSelectSession.webSelectSession.contractId;
                                            RwServices.order.checkInItem(responseCheckInItem.request, function(responseCheckInItem2) {
                                                properties.responseCheckInItem = responseCheckInItem2;
                                                screen.checkInItemCallback(responseCheckInItem2);
                                            });
                                        } else {
                                            throw 'Not implemented for moduleType: ' + properties.moduleType;
                                        }
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else {
                screen.checkInItemCallback2(responseCheckInItem);
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    };

    screen.checkInItemCallback2 = function(responseCheckInItem) {
        var $liCheckInPending, valTxtQty, isScannedICode, suspendedContractsPopup;
        screen.renderPopupQty();
        if (responseCheckInItem.request.playStatus) {
            application.playStatus(responseCheckInItem.webCheckInItem.status === 0);
        }
        isScannedICode = (responseCheckInItem.webCheckInItem.isICode) && (responseCheckInItem.request.orderId.length === 0);
        if (screen.$view.find('#checkIn-btnPendingList').hasClass('selected')) {
            screen.$view.find('#checkIn-btnPendingList').click();
        }
        valTxtQty = (isScannedICode) ? '0' : String(responseCheckInItem.webCheckInItem.stillOut);
        screen.$popupQty.find('#checkIn-qty-txtQty').val(valTxtQty);
        screen.$popupQty.find('#checkIn-popupQty-genericMsg').html(responseCheckInItem.webCheckInItem.genericMsg);
        screen.$popupQty.find('#checkIn-popupQty-msg').html(responseCheckInItem.webCheckInItem.msg);
        screen.$popupQty.find('#checkIn-popupQty-masterNo').html(responseCheckInItem.webCheckInItem.masterNo);
        screen.$popupQty.find('#checkIn-popupQty-description').html(responseCheckInItem.webCheckInItem.description).show();
        screen.$popupQty.find('#checkIn-popupQty-qtyOrdered').html(String(responseCheckInItem.webCheckInItem.qtyOrdered));
        screen.$popupQty.find('#checkIn-popupQty-sessionIn').html(String(responseCheckInItem.webCheckInItem.sessionIn));
        screen.$popupQty.find('#checkIn-popupQty-subQty').html(String(responseCheckInItem.webCheckInItem.subQty));
        screen.$popupQty.find('#checkIn-popupQty-stillOut').html(String(responseCheckInItem.webCheckInItem.stillOut));
        screen.$popupQty.find('#checkIn-popupQty-totalIn').html(String(responseCheckInItem.webCheckInItem.totalIn));
        screen.$popupQty.find('#checkIn-popupQty-summary-table-trVendor').toggle(responseCheckInItem.webCheckInItem.vendorId.length > 0);
        if (responseCheckInItem.webCheckInItem.vendorId.length > 0) {
            screen.$popupQty.find('#checkIn-popupQty-vendor').html(responseCheckInItem.webCheckInItem.vendor);
        }
        if (properties.contractId === '') {
            properties.contractId = responseCheckInItem.webCheckInItem.contractId;
            //jQuery('#checkIn-pageSelectorInner').show();
        }
        if (properties.dealId === '') {
            properties.dealId = responseCheckInItem.webCheckInItem.dealId;
        }
        if (properties.departmentId === '') {
            properties.departmentId = responseCheckInItem.webCheckInItem.departmentId;
        }
        if (((typeof responseCheckInItem.webCheckInItem.sessionNo === 'string') && (responseCheckInItem.webCheckInItem.sessionNo.length > 0)) && ((typeof properties.sessionNo === 'undefined') || (properties.sessionNo.length === 0))) {
            properties.sessionNo = responseCheckInItem.webCheckInItem.sessionNo;
            //jQuery('#masterLoggedInView-captionPageSubTitle').html(RwLanguages.translate('Session') + ': ' + properties.sessionNo);
            FwMobileMasterController.setTitle(RwLanguages.translate('Session') + ': ' + properties.sessionNo);
        }
        screen.$popupQty.find('#checkIn-newOrder')
            .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.showNewOrder));
        screen.$popupQty.find('#checkIn-newOrder-btnSwap')
            .toggle(responseCheckInItem.webCheckInItem.allowSwap);
        if (responseCheckInItem.webCheckInItem.status === 0) {
            screen.$popupQty.find('#checkIn-popupQty-genericMsg').removeClass('qserror').addClass('qssuccess');
        }
        screen.$popupQty.find('#checkIn-popupQty-msg')
            .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.msg.length > 0));
        screen.$popupQty.find('#checkIn-popupQty-fields')
            .toggle((applicationConfig.designMode) || ((responseCheckInItem.webCheckInItem.status === 0) && (!isScannedICode)));
        screen.$popupQty.find('#checkIn-popupQty-genericMsg')
            .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0));
        screen.$popupQty.find('#checkIn-popupQty-messages')
            .toggle((responseCheckInItem.webCheckInItem.msg.length > 0) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0));
        screen.$popupQty.find('#checkIn-popupQty-qtyPanel')
            .toggle((applicationConfig.designMode) || ((responseCheckInItem.request.qty === 0) && (responseCheckInItem.webCheckInItem.isICode) && responseCheckInItem.webCheckInItem.status === 0));
        jQuery('#master-header-row4').toggle(properties.contractId !== '');
        if ((responseCheckInItem.request.qty === 0) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0) || (responseCheckInItem.webCheckInItem.msg.length > 0)) {
            screen.showPopupQty();
            if (responseCheckInItem.webCheckInItem.status === 0) {
                if (responseCheckInItem.request.qty === 0) {
                        screen.showPopupQty();
                } else {
                    screen.showPopupQty();
                    setTimeout(
                        function() {
                            screen.hidePopupQty();
                        }
                        , 3000);
                }
            } else {
                screen.showPopupQty();
            }
        } 
        else {
            screen.hidePopupQty();
        }
    };

    screen.getRowCountItem = function(lineitemcount, totalitems) {
        var li;
        li = [];
        li.push('<li class="normal lineitemcount">');
            li.push('<div>' + lineitemcount.toString() + '  lines / ' + totalitems + ' items</div>');
        li.push('</li>');
        return li.join('');
    };

    screen.getCheckInPendingListCallback = function(response) {
        var dt, ul, li, isAlternate, subbyqty, isTrackedByQty, captionVendor, valVendor, lineitemcount=0, totalitems=0, showapplyallqtyitems=false, isTrackedBySerial;
        isAlternate   = false;
        dt            = response.getCheckInPendingList;
        ul            = [];
        captionVendor = RwLanguages.translate('Vendor/Consignor');
        for (var i = 0; i < dt.Rows.length; i++) {
            var itemclass = dt.Rows[i][dt.ColumnIndex.itemclass];
            var ispackage   = dt.Rows[i][dt.ColumnIndex.ispackage];
            var qtystillout = dt.Rows[i][dt.ColumnIndex.qtystillout];
            if (qtystillout > 0) {
                lineitemcount++;
            }
            totalitems += qtystillout;
            li = [];
            valVendor = dt.Rows[i][dt.ColumnIndex.vendor];
            if (!isAlternate) {
                cssClass = 'normal';
            } else {
                cssClass = 'alternate';
            }
            isTrackedByQty    = dt.Rows[i][dt.ColumnIndex.trackedby] === 'QUANTITY';
            isTrackedBySerial = dt.Rows[i][dt.ColumnIndex.trackedby] === 'SERIALNO';
            if (isTrackedByQty) {
                showapplyallqtyitems = true;
            }
            if ((isTrackedBySerial || isTrackedByQty || dt.Rows[i][dt.ColumnIndex.subbyquantity]) && (qtystillout > 0)) {
                cssClass += ' link';
            }
            cssClass +=  ' itemclass-' + itemclass;
            isAlternate = !isAlternate;
            li.push('<li class="' + cssClass);
            li.push('" data-orderid="'      + dt.Rows[i][dt.ColumnIndex.orderid]);
            li.push('" data-masteritemid="' + dt.Rows[i][dt.ColumnIndex.masteritemid]);
            li.push('" data-masterid="'     + dt.Rows[i][dt.ColumnIndex.masterid]);
            li.push('" data-vendorid="'     + dt.Rows[i][dt.ColumnIndex.vendorid]);
            li.push('">');
                li.push('<div class="description">' + dt.Rows[i][dt.ColumnIndex.description] + '</div>');
                if (ispackage && qtystillout === 0) {
                    li.push('<table style="display:none;">');
                } else {
                    li.push('<table>');
                }
                    li.push('<tbody>');
                        li.push('<tr>');
                            li.push('<td class="col1 masterno key">' + RwLanguages.translate('I-Code') + ':</td>');
                            li.push('<td class="col2 masterno value">' + dt.Rows[i][dt.ColumnIndex.masterno] + '</td>');
                            li.push('<td class="col3 qtystillout key">' + RwLanguages.translate('Remaining') + ':</td>');
                            li.push('<td class="col4 qtystillout value">' + String(dt.Rows[i][dt.ColumnIndex.qtystillout]) + '</td>');
                        li.push('</tr>');
                        li.push('<tr>');
                            if (dt.Rows[i][dt.ColumnIndex.vendorid] === '') {
                                li.push('<td class="col1 trackedby key">' + RwLanguages.translate('Tracked By') + ':</td>');
                                li.push('<td class="col2 trackedby value">' + dt.Rows[i][dt.ColumnIndex.trackedby] + '</td>');
                            } else {
                                subbyqty = (dt.Rows[i][dt.ColumnIndex.subbyquantity]) ? 'QUANTITY' : 'BARCODE';
                                li.push('<td class="col1 trackedby key">' + RwLanguages.translate('Sub By') + ':</td>');
                                li.push('<td class="col2 trackedby value">');
                                li.push(subbyqty);
                                li.push('</td>');
                            }
                            li.push('<td class="col3 sessionin key">' + RwLanguages.translate('Session In') + ':</td>');
                            li.push('<td class="col4 sessionin value">' + String(dt.Rows[i][dt.ColumnIndex.qtyin]) + '</td>');
                        li.push('</tr>');
                        li.push('<tr>');
                                li.push('<td class="col1 orderno key">' + RwLanguages.translate('Order No') + ':</td>');
                                li.push('<td class="col2 orderno value" colspan="3">' + dt.Rows[i][dt.ColumnIndex.orderno] + '</td>');
                        li.push('</tr>');
                        if (dt.Rows[i][dt.ColumnIndex.vendorid] !== '') {
                            li.push('<tr>');
                                li.push('<td class="col1 vendor key">' + captionVendor + ':</td>');
                                li.push('<td class="col2 vendor value" colspan="3"><div class="vendor">' + valVendor + '</div></td>');
                            li.push('</tr>');
                        }
                    li.push('</tbody>');
                li.push('</table>');
            li.push('</li>');
            ul.push(li.join(''));
        }
        screen.$view.find('#checkin-pendingList-pnlApplyAllQtyItems').toggle((sessionStorage.getItem('users_qsallowapplyallqtyitems') === 'T') && showapplyallqtyitems);
        ul.push(screen.getRowCountItem(lineitemcount, totalitems));
        jQuery('#checkIn-pendingList-ul').html(ul.join(''));
    };
    screen.rfiditem = function(itemtype) {
        var html;
        html = [];
        html.push('<div class="rfid-item ' + itemtype + '">');
            html.push('<div class="rfid-item-title"></div>');
            html.push('<div class="rfid-item-info">');
                if (itemtype === 'processed' || itemtype === 'exception') {
                    html.push('<div class="rfid-data rfid">');
                        html.push('<div class="item-caption">RFID:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                    html.push('<div class="rfid-data barcode">');
                        html.push('<div class="item-caption">Barcode:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                    html.push('<div class="rfid-data serial">');
                        html.push('<div class="item-caption">Serial No:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                }
                if (itemtype === 'exception') {
                    html.push('<div class="rfid-data message">');
                        html.push('<div class="item-caption">Message:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                }
                if (itemtype === 'pending') {
                    html.push('<div class="rfid-data ordered">');
                        html.push('<div class="item-caption">Out:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                    html.push('<div class="rfid-data staged">');
                        html.push('<div class="item-caption">Session In:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                    html.push('<div class="rfid-data remaining">');
                        html.push('<div class="item-caption">Still Out:</div>');
                        html.push('<div class="item-value"></div>');
                    html.push('</div>');
                }
            html.push('</div>');
        html.push('</div>');
        return jQuery(html.join(''));
    };
    screen.rfidscan = function(epcs) {
        if (epcs !== '') {
            screen.$view.find('.rfid-rfidstatus').show();
            screen.$view.find('.rfid-items').empty();
            screen.$view.find('.rfid-placeholder').show().html('0 tags scanned.');
            screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html('0');
            screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
            screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
            screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
            screen.$view.find('.rfid-rfidbuttons .btnstaging').hide();
            requestCheckInItem = {
                rfidmode:  'CHECKIN',
                sessionid: screen.getContractId(),
                portal:    device.uuid,
                tags:      epcs,
                aisle:     screen.properties.aisle,
                shelf:     screen.properties.shelf
            };
            RwServices.order.rfidScan(requestCheckInItem, function(response) {
                var $item;

                for (var i = 0; i < response.processed.length; i++) {
                    $item = screen.rfiditem('processed');
                    $item.find('.rfid-item-title').html(response.processed[i].title);
                    $item.find('.rfid-data.rfid .item-value').html(response.processed[i].rfid);
                    $item.find('.rfid-data.barcode .item-value').html((response.processed[i].barcode !== '' ) ? response.processed[i].barcode : '-');
                    $item.find('.rfid-data.serial .item-value').html((response.processed[i].serialno !== '') ? response.processed[i].serialno : '-');
                    if (response.processed[i].duplicatescan === 'T') {
                        $item.addClass('duplicate');
                    }

                    screen.$view.find('.rfid-items').append($item);
                }

                screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html(response.processed.length);
                screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                (response.pending.length > 0)    ? screen.$view.find('.btnpending').show()   : screen.$view.find('.btnpending').hide();
                (response.processed.length > 0)  ? screen.$view.find('.btnclear').show()     : screen.$view.find('.btnclear').hide();
                if (response.processed.length > 0) {
                    screen.$view.find('.rfid-placeholder').hide();
                }
            });
        }
    };

    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var barcode, $txtBarcodeData, requestCheckInItem, orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId, isAisleShelfBarcode;
            try {
                $txtBarcodeData = jQuery(this);
                barcode = $txtBarcodeData.val();
                if (barcode.length > 0) {
                    isAisleShelfBarcode = /^[A-z0-9]{4}-[A-z0-9]{4}$/.test(barcode);  // Format: AAAA-SSSS
                    if (isAisleShelfBarcode) {
                        screen.properties.aisle = $txtBarcodeData.val().substring(0,4).toUpperCase();
                        screen.properties.shelf = $txtBarcodeData.val().split('-')[1].toUpperCase();
                        screen.$view.find('.aisle .value').html(screen.properties.aisle);
                        screen.$view.find('.shelf .value').html(screen.properties.shelf);
                        screen.$view.find('.aisleshelf').show();
                        $txtBarcodeData.val('');
                    } else {
                        orderId         = screen.getOrderId();
                        masterItemId    = '';
                        masterId        = '';
                        code            = RwAppData.stripBarcode($txtBarcodeData.val().toUpperCase());
                        qty             = 0;
                        newOrderAction  = '';
                        aisle           = screen.properties.aisle;
                        shelf           = screen.properties.shelf;
                        playStatus      = true;
                        vendorId        = '';
                        screen.checkInItem(orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId);
                    }
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnfillcontainer', function() {
            var fillcontainerproperties;
            try {
                fillcontainerproperties = jQuery.extend({}, properties, {
                    mode:          'checkin',
                    modulecaption: jQuery('#master-header-caption').html(),
                    pagetitle:     pageSubTitle,
                    checkincontractid: screen.getContractId(),
                    orderid:       screen.getOrderId(),
                    dealid:        screen.getDealId(),
                    departmentid:  screen.getDepartmentId(),
                    aisle:         screen.properties.aisle,
                    shelf:         screen.properties.shelf
                });
                application.pushScreen(RwFillContainer.getFillContainerScreen({}, fillcontainerproperties));
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkIn-pendingList-ul > li.link', function() {
            var $this, orderId, masterItemId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId;
            try {
                $this = jQuery(this);
                if ($this.find('td.trackedby.value').html() === 'QUANTITY') {
                    orderId        = $this.attr('data-orderid');
                    masterItemId   = $this.attr('data-masteritemid');
                    masterId       = $this.attr('data-masterid');
                    code           = $this.find('td.masterno.value').html();
                    qty            = 0;
                    newOrderAction = '';
                    aisle          = screen.properties.aisle;
                    shelf          = screen.properties.shelf;
                    playStatus     = false;
                    vendorId       = $this.attr('data-vendorid');
                    screen.checkInItem(orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId);
                } else if ($this.find('td.trackedby.value').html() === 'SERIALNO') {
                    $checkInSerial.showscreen($this);
                    //screen.loadSerialScreen($this);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkIn-sessionInList-ul > li.link', function() {
            var $this, $contextmenu;
            $this        = jQuery(this);
            $contextmenu = FwContextMenu.render($this.attr('data-description'));
            FwContextMenu.addMenuItem($contextmenu, 'Cancel Item', function() {
                var request;
                request = {
                    contractid:   screen.getContractId(),
                    masteritemid: $this.attr('data-masteritemid'),
                    masterid:     $this.attr('data-masterid'),
                    vendorid:     $this.attr('data-vendorid'),
                    consignorid:  $this.attr('data-consignorid'),
                    description:  $this.attr('data-description'),
                    ordertranid:  $this.attr('data-ordertranid'),
                    internalchar: $this.attr('data-internalchar'),
                    qty:          parseFloat($this.attr('data-sessionin')),
                    trackedby:    $this.attr('data-trackedby'),
                    aisle:        screen.properties.aisle,
                    shelf:        screen.properties.shelf,
                    orderid:      $this.attr('data-orderid')
                };
                try {
                    RwServices.order.checkInItemCancel(request, function(response) {
                        try {
                            screen.$tabsessionin.click();
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
            FwContextMenu.addMenuItem($contextmenu, 'Send To Repair', function() {
                var request;
                request = {
                    contractid:   screen.getContractId(),
                    orderid:      $this.attr('data-orderid'),
                    masteritemid: $this.attr('data-masteritemid'),
                    rentalitemid: $this.attr('data-rentalitemid'),
                    qty:          parseFloat($this.attr('data-sessionin'))
                };
                try {
                    if ($this.attr('data-trackedby') === 'QUANTITY') {
                        var $confirmation = FwConfirmation.showMessage('How many?', '<input class="qty" type="number" style="font-size:16px;padding:5px;border:1pxc solid #bdbdbd;box-sizing:border-box;width:100%;" value="' + request.qty + '" />', true, false, 'OK', function() {
                            try {
                                var userqty = $confirmation.find('input.qty').val();
                                if (userqty === '') {
                                    throw 'Qty is required.';
                                }
                                if (isNaN(userqty)) {
                                    throw 'Please enter a valid qty.';
                                }
                                userqty = parseFloat(userqty);
                                if (userqty > request.qty) {
                                    throw 'Qty cannot exceed ' + request.qty.toString() + '.';
                                }
                                if (userqty <= 0) {
                                    throw 'Qty must be > 0.';
                                }
                                request.qty = userqty;
                                RwServices.order.checkInItemSendToRepair(request, function(response) {
                                    try {
                                        FwConfirmation.destroyConfirmation($confirmation);
                                        var repairOrderScreen = RwInventoryController.getRepairOrderScreen({}, {mode:'sendtorepair', repairno:response.repairno});
                                        application.pushScreen(repairOrderScreen);
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } else {
                        RwServices.order.checkInItemSendToRepair(request, function(response) {
                            try {
                                var repairOrderScreen = RwInventoryController.getRepairOrderScreen({}, {mode:'sendtorepair', repairno:response.repairno, qty:1});
                                application.pushScreen(repairOrderScreen);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        })
        .on('click', '#checkin-pendingList-btnApplyAllQtyItems', function() {
            var request;
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Confirm',  RwLanguages.translate('Apply All Qty Items') + '?');
                var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                $btnok.focus();
                FwConfirmation.addButton($confirmation, 'Cancel', true);
                $btnok.on('click', function() {
                    try {
                        request = {
                            contractId: screen.getContractId()
                        };
                        RwServices.order.checkInAllQtyItems(request, function(response) {
                            if (typeof response.getCheckInPendingList !== 'undefined') {
                                screen.getCheckInPendingListCallback(response);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.rfid-item', function() {
            var $this, $selectedrfiditem;
            $this             = jQuery(this);
            $selectedrfiditem = screen.$view.find('.rfid-item.selected');
            if (typeof $selectedrfiditem !== 'undefined') {
                $selectedrfiditem.removeClass('selected');
            }
            $this.addClass('selected');
            if ($this.hasClass('exception')) {
                var $confirmation, $cancel, request = {};
                $confirmation = FwConfirmation.renderConfirmation('Exception', '<div class="exceptionbuttons"></div>');
                $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
                switch ($this.attr('data-exceptiontype')) {
                    case '1005': //Item count exceeds quantity ordered - Item
                    case '1019':
                        $confirmation.find('.exceptionbuttons').append('<div class="addordertosession">Add Order to check-in Session?</div>');
                        break;
                    case '104': //Item is Staged on Order
                    case '301': //I-Code / Bar Code not found in Inventory.
                        break;
                }
                $confirmation.find('.exceptionbuttons').append('<div class="clear">Clear item?</div>');

                $confirmation.find('.exceptionbuttons')
                    .on('click', '.addordertosession, .clear', function() {
                        if (jQuery(this).hasClass('addordertosession')) {
                            request.method = 'AddOrderToSession';
                        } else if (jQuery(this).hasClass('clear')) {
                            request.method = 'Clear';
                        }
                        request.sessionid    = screen.getContractId();
                        request.rfid         = $this.find('.rfid-data.rfid .item-value').html();
                        request.portal       = device.uuid;
                        RwServices.order.processrfidexception(request, function(response) {
                            screenStageItem.$view.find('.rfid-items').empty();
                            for (var i = 0; i < response.exceptions.length; i++) {
                                $item = screenStageItem.rfiditem('exception');
                                $item.find('.rfid-item-title').html(response.exceptions[i].title);
                                $item.find('.rfid-data.rfid .item-value').html(response.exceptions[i].rfid);
                                $item.find('.rfid-data.barcode .item-value').html((response.exceptions[i].barcode !== '' ) ? response.exceptions[i].barcode : '-');
                                $item.find('.rfid-data.serial .item-value').html((response.exceptions[i].serialno !== '') ? response.exceptions[i].serialno : '-');
                                $item.find('.rfid-data.message .item-value').html(response.exceptions[i].message);
                                $item.attr('data-exceptiontype', response.exceptions[i].exceptiontype);
                                screenStageItem.$view.find('.rfid-items').append($item);
                            }
                        });
                        FwConfirmation.destroyConfirmation($confirmation);
                    })
                ;
            }
        })
        .on('click', '.rfid-rfidbuttons .btnclear', function() {
            screen.$view.find('.rfid-items').empty();
            screen.$view.find('.rfid-placeholder').hide();
            screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
            screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html('0');
        })
        .on('click', '.rfid-rfidbuttons .btnpending', function() {
            var $item, request;
            screen.$view.find('.rfid-rfidstatus').hide();
            screen.$view.find('.rfid-items').empty();
            screen.$view.find('.rfid-placeholder').hide();
            screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
            screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
            screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
            screen.$view.find('.rfid-rfidbuttons .btnstaging').show();

            request = {
                rfidmode:  'CHECKIN',
                sessionid: screen.getContractId()
            };
            RwServices.order.loadRFIDPending(request, function(response) {
                for (var i = 0; i < response.pending.length; i++) {
                    $item = screen.rfiditem('pending');
                    $item.find('.rfid-item-title').html(response.pending[i].description);
                    $item.find('.rfid-data.ordered .item-value').html(response.pending[i].qtyout);
                    $item.find('.rfid-data.staged .item-value').html(response.pending[i].qtyin);
                    $item.find('.rfid-data.remaining .item-value').html(response.pending[i].qtystillout);
                    screen.$view.find('.rfid-items').append($item);
                }
            });
        })
        .on('click', '.rfid-rfidbuttons .btnexception', function() {
            var $item, request;
            screen.$view.find('.rfid-rfidstatus').hide();
            screen.$view.find('.rfid-items').empty();
            screen.$view.find('.rfid-placeholder').hide();
            screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
            screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
            screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
            screen.$view.find('.rfid-rfidbuttons .btnstaging').show();

            request = {
                sessionid: screen.getContractId(),
                portal:    device.uuid
            };
            RwServices.order.loadRFIDExceptions(request, function(response) {
                for (var i = 0; i < response.exceptions.length; i++) {
                    $item = screen.rfiditem('exception');
                    $item.find('.rfid-item-title').html(response.exceptions[i].title);
                    $item.find('.rfid-data.rfid .item-value').html(response.exceptions[i].rfid);
                    $item.find('.rfid-data.barcode .item-value').html((response.exceptions[i].barcode !== '' ) ? response.exceptions[i].barcode : '-');
                    $item.find('.rfid-data.serial .item-value').html((response.exceptions[i].serialno !== '') ? response.exceptions[i].serialno : '-');
                    $item.find('.rfid-data.message .item-value').html(response.exceptions[i].message);
                    $item.attr('data-exceptiontype', response.exceptions[i].exceptiontype);
                    screen.$view.find('.rfid-items').append($item);
                }
            });
        })
        .on('click', '.rfid-rfidbuttons .btnstaging', function() {
            var request;
            screen.$view.find('.rfid-rfidstatus').show();
            screen.$view.find('.rfid-items').empty();
            screen.$view.find('.rfid-placeholder').show().html('0 tags scanned.');
            screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html('0');
            screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
            screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
            screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
            screen.$view.find('.rfid-rfidbuttons .btnstaging').hide();

            request = {
                rfidmode:  'CHECKIN',
                sessionid: screen.getContractId(),
                portal:    device.uuid
            };
            RwServices.order.getRFIDStatus(request, function(response) {
                screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                (response.pending.length > 0)    ? screen.$view.find('.btnpending').show()   : screen.$view.find('.btnpending').hide();
            });
        })
        .on('click', '.btnclear', function() {
            screen.$view.find('.aisle .value').html('');
            screen.$view.find('.shelf .value').html('');
            screen.$view.find('.aisleshelf').hide();
            screen.properties.aisle = '';
            screen.properties.shelf = '';
        })
    ;

    $checkInSerial = screen.$view.find('#checkIn-serial');
    $checkInSerial.showscreen = function($itemclicked) {
        var request;
        request = {
            orderid:      $itemclicked.attr('data-orderid'),
            masteritemid: $itemclicked.attr('data-masteritemid'),
            masterid:     $itemclicked.attr('data-masterid'),
            contractid:   screen.getContractId()
        }
        $checkInSerial.data('recorddata', request);
        RwServices.call('CheckIn', 'GetSerialInfo', request, function (response) {
            var html = [];
            screen.$view.find('#scanBarcodeView').hide();
            screen.$view.find('#checkIn-pendingList').hide();
            screen.$view.find('#master-header-row4').hide();
            screen.$view.find('#checkIn-serial').show();
            screen.$btncreatecontract.hide();
            screen.$btnclose.hide();
            $checkInSerial.$btnback.show();
            if (response.serial.metered == 'T') $checkInSerial.$btnserialmeters.show();

            html.push('<div class="serial-title">');
            html.push('  <div class="serial-title-object">I-Code: ' + response.serial.masterno + '</div>');
            html.push('  <div class="serial-title-object">' + response.serial.description + '</div>');
            html.push('</div>');
            html.push('<div class="serial-details">');
            html.push('  <div class="serial-details-row">');
            html.push('    <div class="serial-details-row-caption">Remaining:</div>');
            html.push('    <div class="serial-details-row-value qtyremaining">' + response.serial.qtyout + '</div>');
            html.push('    <div class="serial-details-row-caption">Out:</div>');
            html.push('    <div class="serial-details-row-value">' + response.serialitems.length + '</div>');
            html.push('  </div>');
            html.push('  <div class="serial-details-row">');
            html.push('    <div class="serial-details-row-caption">Ordered:</div>');
            html.push('    <div class="serial-details-row-value">' + response.serial.qtyordered + '</div>');
            html.push('    <div class="serial-details-row-caption">In:</div>');
            html.push('    <div class="serial-details-row-value qtyin">' + response.serial.qtyin + '</div>');
            html.push('  </div>');
            html.push('</div>');
            html.push('<div class="serialitems"></div>');
            html.push('<div class="serialitem-meters"></div>');
            $checkInSerial.append(jQuery(html.join('')));

            $checkInSerial.loadSerialItems();
        });
    };
    $checkInSerial.loadSerialItems = function() {
        $checkInSerial.find('.serialitems').empty();
        RwServices.call('CheckIn', 'GetSerialItems', $checkInSerial.data('recorddata'), function (response) {
            for (var i = 0; i < response.serialitems.length; i++) {
                var html = [], $serialitem;
                html.push('<div class="serialitem standard' + ((response.serialitems[i].itemstatus == 'O') ? '' : ' in') + '">');
                html.push('  <div class="serialitem-caption">Serial No:</div>');
                html.push('  <div class="serialitem-value">' + response.serialitems[i].mfgserial + '</div>');
                html.push('</div>');
                $serialitem = jQuery(html.join(''));
                $serialitem.data('recorddata', response.serialitems[i]);
                $checkInSerial.find('.serialitems').append($serialitem);
            }
        });
    };
    $checkInSerial.loadSerialMeters = function() {
        $checkInSerial.find('.serialitem-meters').empty();
        RwServices.call('CheckIn', 'GetSerialItems', $checkInSerial.data('recorddata'), function (response) {
            var zerosetflg = true;
            for (var i = 0; i < response.serialitems.length; i++) {
                var html = [], $serialitem, meterinvalue, notsetflg;
                if (response.serialitems[i].itemstatus == 'I') {
                    zerosetflg = false;
                    if (response.serialitems[i].meterin == '0.00') {
                        meterinvalue = response.serialitems[i].meterout;
                        notsetflg    = false;
                    } else {
                        meterinvalue = response.serialitems[i].meterin;
                        notsetflg    = true;
                    }
                    html.push('<div class="serialitem metered' + (notsetflg ? ' valueset' : '')  + '">');
                    html.push('  <div class="serialitem-row">');
                    html.push('    <div class="serialitem-caption">Serial No:</div>');
                    html.push('    <div class="serialitem-value">' + response.serialitems[i].mfgserial + '</div>');
                    html.push('    <div class="serialitem-metericon"><i class="material-icons">expand_more</i></div>');
                    html.push('  </div>');
                    html.push('  <div class="serialitem-meter-dropdown">');
                    html.push('    <div class="serialitem-meter-dropdown-row meteredout">');
                    html.push('      <div class="serialitem-meter-dropdown-caption">Out Value: </div>');
                    html.push('      <div class="serialitem-meter-dropdown-value meteroutvalue">' + numberWithCommas(parseFloat(response.serialitems[i].meterout).toFixed(2)) + '</div>');
                    html.push('    </div>');
                    html.push('    <div class="serialitem-meter-dropdown-row">');
                    html.push('      <div class="serialitem-meter-dropdown-caption">In Value: </div>');
                    html.push('      <div class="serialitem-meter-dropdown-value meterinvalue' + (notsetflg ? ' valueset' : '')  + '">');
                    html.push(         numberWithCommas(parseFloat(meterinvalue).toFixed(2)));
                    html.push('      </div>');
                    html.push('    </div>');
                    html.push('  </div>');
                    html.push('</div>');
                    $serialitem = jQuery(html.join(''));
                    $serialitem.data('recorddata', response.serialitems[i]);
                    $checkInSerial.find('.serialitem-meters').append($serialitem);
                }
            }
            if (zerosetflg) {
                html.push('<div class="zeroitems">0 items sessioned in</div>');
                $checkInSerial.find('.serialitem-meters').append(jQuery(html.join('')));
            }
        });
    };
    $checkInSerial.$btnback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', false, function () {
        $checkInSerial.hidescreen();
    });
    $checkInSerial.$btnserialmeters = FwMobileMasterController.addFormControl(screen, 'Set Meters', 'right', 'continue', false, function () {
        $checkInSerial.find('.serial-details').hide();
        $checkInSerial.find('.serialitems').hide();
        $checkInSerial.find('.serialitem-meters').show();
        $checkInSerial.$btnserialmeters.hide();
        $checkInSerial.$btnback.hide();
        $checkInSerial.$btnmetersback.show();
        $checkInSerial.$btnmetersfinish.show();
        $checkInSerial.loadSerialMeters();
    });
    $checkInSerial.$btnmetersback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', false, function () {
        $checkInSerial.$btnserialmeters.show();
        $checkInSerial.$btnback.show();
        $checkInSerial.$btnmetersback.hide();
        $checkInSerial.$btnmetersfinish.hide();
        $checkInSerial.find('.serialitems').show();
        $checkInSerial.find('.serial-details').show();
        $checkInSerial.find('.serialitem-meters').hide();
        $checkInSerial.loadSerialItems();
    });
    $checkInSerial.$btnmetersfinish = FwMobileMasterController.addFormControl(screen, 'Finish', 'right', 'continue', false, function () {
        $checkInSerial.hidescreen();
        $checkInSerial.$btnmetersback.hide();
        $checkInSerial.$btnmetersfinish.hide();
    });
    $checkInSerial.hidescreen = function() {
        var request;
        screen.$view.find('#scanBarcodeView').show();
        screen.$view.find('#checkIn-pendingList').show();
        screen.$view.find('#master-header-row4').show();
        $checkInSerial.empty().hide();
        screen.$btncreatecontract.show();
        screen.$btnclose.show();
        $checkInSerial.$btnback.hide();
        $checkInSerial.data('recorddata', '');
        if (typeof $checkInSerial.$btnserialmeters !== 'undefined') $checkInSerial.$btnserialmeters.hide();
        request = {
            contractId: screen.getContractId()
        };
        RwServices.order.getCheckInPendingList(request, screen.getCheckInPendingListCallback);
    };
    $checkInSerial
        .on('click', '.serialitem.standard', function() {
            var request, $this;
            $this = jQuery(this);
            request = {
                orderid:      jQuery(this).data('recorddata').orderid,
                masteritemid: jQuery(this).data('recorddata').masteritemid,
                masterid:     jQuery(this).data('recorddata').masterid,
                rentalitemid: jQuery(this).data('recorddata').rentalitemid,
                contractid:   screen.getContractId(),
                meter:        '0',
                toggledelete: 'T'
            };
            RwServices.call('CheckIn', 'SerialSessionIn', request, function (response) {
                $checkInSerial.find('.qtyremaining').html(response.serial.qtyout);
                $checkInSerial.find('.qtyin').html(response.serial.qtyin);
                $this.toggleClass('in');
            });
        })
        .on('click', '.serialitem-row', function() {
            var $serialitem;
            $serialitem = jQuery(this).parent();
            var visible = $serialitem.find('.serialitem-meter-dropdown').is(':visible');
            $checkInSerial.find('.serialitem-meter-dropdown').hide();
            $checkInSerial.find('.serialitem-metericon .material-icons').html('expand_more');
            if (!visible) {
                $serialitem.find('.serialitem-meter-dropdown').show();
                $serialitem.find('.serialitem-metericon .material-icons').html('expand_less');
            }
        })
        .on('click', '.meterinvalue', function() {
            var $confirmation, $ok, $cancel, html = [], $this;
            $this         = jQuery(this);
            $confirmation = FwConfirmation.renderConfirmation('Enter Meter In Value', '');
            $ok           = FwConfirmation.addButton($confirmation, 'Ok', false);
            $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

            html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="In Value" data-datafield="meterinvalue" data-minvalue="' + $this.closest('.metered').data('recorddata').meterout + '" data-formatnumeric="true"></div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            FwFormField.setValue($confirmation, 'div[data-datafield="meterinvalue"]', parseFloat($this.html()));

            $ok.on('click', function() {
                request = {
                    orderid:      $this.closest('.metered').data('recorddata').orderid,
                    masteritemid: $this.closest('.metered').data('recorddata').masteritemid,
                    masterid:     $this.closest('.metered').data('recorddata').masterid,
                    rentalitemid: $this.closest('.metered').data('recorddata').rentalitemid,
                    contractid:   screen.getContractId(),
                    meter:        FwFormField.getValue($confirmation, 'div[data-datafield="meterinvalue"]'),
                    toggledelete: 'F'
                };
                RwServices.call('CheckIn', 'SerialSessionIn', request, function (response) {
                    FwConfirmation.destroyConfirmation($confirmation);
                    $this.addClass('valueset').html(numberWithCommas(parseFloat(request.meter).toFixed(2)));
                    $this.closest('.metered').addClass('valueset');
                });
            });
        })
    ;

    screen.renderPopupQty = function() {
        var template = Mustache.render(jQuery('#tmpl-CheckIn-PopupQty').html(), {
            captionICodeDesc:          RwLanguages.translate('I-Code'),
            captionRemaining:          RwLanguages.translate('Remaining'),
            captionSessionIn:          RwLanguages.translate('Session In'),
            captionQtyOrdered:         RwLanguages.translate('Ordered'),
            captionTotalIn:            RwLanguages.translate('In'),
            captionSubQty:             RwLanguages.translate('Sub'),
            captionSubVendor:          RwLanguages.translate('Vendor/Consignor'),
            valueTxtQty:               '',
            captionCheckIn:            RwLanguages.translate('Check-In'),
            captionNewOrder:           RwLanguages.translate('New Order'),
            captionSwap:               RwLanguages.translate('Swap'),
            captionCancel:             RwLanguages.translate('Cancel')
        });
        var $popupcontent = jQuery(template);
        if (typeof screen.$popupQty === 'object' && screen.$popupQty.length > 0) {
            FwPopup.destroyPopup(screen.$popupQty);
        }
        screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
        screen.$popupQty.find('#checkIn-popupQty-description').hide();
        screen.$popupQty.find('#checkIn-popupQty-messages').hide();
        screen.$popupQty.find('#checkIn-popupQty-fields').hide();
        screen.$popupQty.find('#checkIn-popupQty-qtyPanel').hide();
        screen.$popupQty.find('#checkIn-newOrder').hide();
        FwPopup.showPopup(screen.$popupQty);
        screen.$popupQty
            .on('click', '#checkIn-qty-btnCheckIn', function() {
                var $txtQty, requestChangeQty, remaining;
                try {
                    $txtQty = jQuery('#checkIn-qty-txtQty');
                    requestChangeQty = jQuery.extend({}, properties.responseCheckInItem.request);
                    requestChangeQty.qty = parseInt($txtQty.val());
                    remaining = Number(jQuery('#checkIn-popupQty-stillOut').html());
                    if ( (isNaN(requestChangeQty.qty) || (requestChangeQty.qty <= 0) || (requestChangeQty.qty > remaining)) ) {
                        throw 'Invalid qty.';
                    }
                    RwServices.order.checkInItem(requestChangeQty, function(responseChangeQty) {
                        screen.checkInItemCallback(responseChangeQty);
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#checkIn-newOrder-btnSwap', function() {
                var requestSwap;
                try {
                    requestSwap = jQuery.extend({}, properties.responseCheckInItem.request);
                    requestSwap.newOrderAction = 'S';
                    RwServices.order.checkInItem(requestSwap, function(responseSwap) {
                        screen.checkInItemCallback(responseSwap);
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#checkIn-newOrder-btnCancel', function() {
                try {
                    FwPopup.destroyPopup($popup);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#checkIn-newOrder-btnNewOrder', function() {
                var requestNewOrder;
                try {
                    requestNewOrder = jQuery.extend({}, properties.responseCheckInItem.request);
                    requestNewOrder.newOrderAction = 'Y';
                    RwServices.order.checkInItem(requestNewOrder, function(responseNewOrder) {
                        screen.checkInItemCallback(responseNewOrder);
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#checkIn-qty-btnSubtract', function() {
                var quantity;
                quantity = Number(jQuery('#checkIn-qty-txtQty').val()) - 1;
                if (quantity >= 0) {
                    jQuery('#checkIn-qty-txtQty').val(quantity);
                }
            })
            .on('click', '#checkIn-qty-btnAdd', function() {
                var quantity, remaining;
                remaining = Number(jQuery('#checkIn-popupQty-stillOut').html());
                quantity = Number(jQuery('#checkIn-qty-txtQty').val()) + 1;
                if (quantity <= remaining) {
                    jQuery('#checkIn-qty-txtQty').val(quantity);
                }
            })
        ;
    };

    screen.load = function() {
        application.setScanTarget('#scanBarcodeView-txtBarcodeData');
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
        if (screen.getContractId() !== '') {
            screen.$tabpending.click();
        }

        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_rwordercontrollerjs_getCheckInScreen', function() {
                RwRFID.isConnected = true;
                screen.toggleRfid();
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_rwordercontrollerjs_getCheckInScreen', function() {
                RwRFID.isConnected = false;
                screen.toggleRfid();
            });
        }
        screen.toggleRfid();
        screen.toggleFillContainerButton();
    };

    screen.unload = function() {
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_rwordercontrollerjs_getCheckInScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_rwordercontrollerjs_getCheckInScreen');
        }
    };

    screen.beforeNavigateAway = function(navigateAway) {
        var requestCancelContract;
        requestCancelContract = {
            contractId:                    screen.getContractId(),
            activityType:                  'CheckIn',
            dontCancelIfOrderTranExists:   true,
            failSilentlyOnOwnershipErrors: true
        };
        RwServices.order.cancelContract(requestCancelContract, 
            function doneCallback(responseCancelContract) {
                // in case there is an error we still want to navigate away
                navigateAway();
            }),
            null,
            function failCallback() {
                RwAppData.error(jqXHR, textStatus, errorThrown);
                navigateAway();
            }
        ;
    };

    screen.toggleFillContainerButton = function() {
        if ((screen.getContractId().length > 0) && (sessionStorage.userType === 'USER') && (typeof applicationOptions.container !== 'undefined') && (applicationOptions.container.enabled)) {
            var requestHasCheckinFillContainerButton = {
                contractid: screen.getContractId()
            };
            RwServices.FillContainer.HasCheckinFillContainerButton(requestHasCheckinFillContainerButton, function(response) {
                screen.$view.find('.fillcontainertoolbar').toggle(response.hasCheckinFillContainerButton);
            });
        } else {
            screen.$view.find('.fillcontainertoolbar').toggle(false);
        }
    };

    return screen;
};