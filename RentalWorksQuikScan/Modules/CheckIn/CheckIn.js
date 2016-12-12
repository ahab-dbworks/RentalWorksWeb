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
    var combinedViewModel, screen, pageTitle, pageSubTitle, requestOrdertranExists, applicationOptions, showAisleShelf;
    applicationOptions = application.getApplicationOptions();
    showAisleShelf = true;
    if (typeof properties.orderId      === 'undefined') {properties.orderId      = '';}
    if (typeof properties.dealId       === 'undefined') {properties.dealId       = '';}
    if (typeof properties.departmentId === 'undefined') {properties.departmentId = '';}
    if (typeof properties.orderDesc    === 'undefined') {properties.orderDesc    = '';}
    if (typeof properties.contractId   === 'undefined') {properties.contractId   = '';}
    switch(properties.checkInMode) {
        case RwConstants.checkInModes.SingleOrder:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + RwLanguages.translate('Order') + ': ' + properties.webSelectOrder.orderNo + '&nbsp;&nbsp;&nbsp;&nbsp;'  + RwLanguages.translate('Session') + ': ' + properties.sessionNo + '<br/>' + properties.webSelectOrder.orderDesc + '</div>';
            properties.orderId      = properties.webSelectOrder.orderId;
            properties.dealId       = properties.webSelectOrder.dealId;
            properties.departmentId = properties.webSelectOrder.departmentId;
            properties.orderDesc    = properties.webSelectOrder.orderDesc;
            break;
        case RwConstants.checkInModes.MultiOrder:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + RwLanguages.translate('Multi-Order') + '<br/>'  + RwLanguages.translate('First scan must be a Bar Code...') + '</div>';
            break;
        case RwConstants.checkInModes.Session:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + RwLanguages.translate('Order') + ': ' + properties.webSelectSession.orderNo + '&nbsp;&nbsp;&nbsp;&nbsp;'  + RwLanguages.translate('Session') + ': ' + properties.webSelectSession.sessionNo + '<br/>' + properties.webSelectSession.orderDesc + '</div>';
            properties.orderId      = properties.webSelectSession.orderId;
            properties.dealId       = properties.webSelectSession.dealId;
            properties.departmentId = properties.webSelectSession.departmentId;
            properties.orderDesc    = properties.webSelectSession.orderDesc;
            properties.contractId   = properties.webSelectSession.contractId;
            break;
        case RwConstants.checkInModes.Deal:
            pageTitle               = RwLanguages.translate('Check-In');
            pageSubTitle            = '<div class="title">' + RwLanguages.translate('Deal No') + ': ' + properties.webSelectDeal.dealNo + '<br/>' + RwLanguages.translate('Desc') + ': ' + properties.webSelectDeal.dealdesc + '</div>';
            properties.dealId       = properties.webSelectDeal.dealId;
            properties.departmentId = properties.webSelectDeal.departmentId;
            break;  
    }
    combinedViewModel = jQuery.extend({
        captionPageTitle:          RwOrderController.getPageTitle(properties)
      , captionPageSubTitle:       pageSubTitle
      , htmlScanBarcode:           RwPartialController.getScanBarcodeHtml({
            captionInstructions: RwLanguages.translate('Select Item to Check-In...')
          , captionBarcodeICode: RwLanguages.translate('Bar Code / I-Code')
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

    screen.toggleRfid = function() {
        if (RwRFID.isConnected) {
            screen.$view.find('#checkIn-btnRFID').css('display', 'inline-block');
            screen.$view.find('#checkIn-btnRFID').click();
            var requestRFIDClear = {};
            requestRFIDClear.sessionid = screen.getContractId();
            RwServices.order.rfidClearSession(requestRFIDClear, function(response) {});
        } else {
            screen.$view.find('#checkIn-btnRFID').css('display', 'none');
            screen.$view.find('#checkIn-btnScan').click();
        }
    };
    screen.toggleRfid();

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

    screen.$view.find('#checkIn-pageSelectorInner').toggle((applicationConfig.designMode) || (screen.getContractId() !== ''));
    
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
        screen.$popupQty.find('#checkIn-popupQty-description').html(responseCheckInItem.webCheckInItem.description);
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
        jQuery('#checkIn-pageSelectorInner').toggle(properties.contractId !== '');
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
        var dt, ul, li, isAlternate, subbyqty, isTrackedByQty, captionVendor, valVendor, lineitemcount=0, totalitems=0, showapplyallqtyitems=false;
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
            isTrackedByQty = dt.Rows[i][dt.ColumnIndex.trackedby] === 'QUANTITY';
            if (isTrackedByQty) {
                showapplyallqtyitems = true;
            }
            if ((isTrackedByQty || dt.Rows[i][dt.ColumnIndex.subbyquantity]) && (qtystillout > 0)) {
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
                            screen.$view.find('#checkIn-btnSessionInList').click();
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
        .on('click', '#checkIn-btnPendingList', function() {
            var request;
            try {
                jQuery('#checkIn-scan').hide();
                jQuery('#checkIn-sessionInList').hide();
                jQuery('#checkIn-pendingList').show();
                jQuery('#checkIn-rfid').hide();
                jQuery('#checkIn-btnScan').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnSessionInList').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnPendingList').removeClass('unselected').addClass('selected');
                jQuery('#checkIn-btnRFID').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-scan').attr('data-mode', 'PENDINGLIST');
                RwRFID.unregisterEvents();
                request = {
                    contractId: screen.getContractId()
                };
                RwServices.order.getCheckInPendingList(request, screen.getCheckInPendingListCallback);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkIn-btnScan', function() {
            try {
                jQuery('#checkIn-pendingList').hide();
                jQuery('#checkIn-sessionInList').hide();
                jQuery('#checkIn-rfid').hide();
                jQuery('#checkIn-scan').show();
                jQuery('#checkIn-btnPendingList').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnSessionInList').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnScan').removeClass('unselected').addClass('selected');
                jQuery('#checkIn-btnRFID').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-scan').attr('data-mode', 'SCAN');
                RwRFID.unregisterEvents();
                screen.toggleFillContainerButton();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkIn-btnRFID', function() {
            try {
                var request;
                jQuery('#checkIn-sessionInList').hide();
                jQuery('#checkIn-pendingList').hide();
                jQuery('#checkIn-scan').hide();
                jQuery('#checkIn-rfid').show();
                jQuery('#checkIn-btnPendingList').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnSessionInList').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnScan').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnRFID').removeClass('unselected').addClass('selected');
                jQuery('#checkIn-scan').attr('data-mode', 'RFID');
                //jQuery('.rfid-rfidbuttons .btnstaging').click();
                RwRFID.registerEvents(screen.rfidscan);
                request = {
                    rfidmode:  'CHECKIN',
                    sessionid:  screen.getContractId(),
                    portal:    device.uuid
                };
                RwServices.order.getRFIDStatus(request, function(response) {
                    screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                    screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                    (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                    (response.pending.length > 0)    ? screen.$view.find('.btnpending').show()   : screen.$view.find('.btnpending').hide();
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkIn-btnSessionInList', function() {
            var request;
            try {
                jQuery('#checkIn-scan').hide();
                jQuery('#checkIn-pendingList').hide();
                jQuery('#checkIn-rfid').hide();
                jQuery('#checkIn-sessionInList').show();
                jQuery('#checkIn-btnPendingList').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnScan').removeClass('selected').addClass('unselected');
                jQuery('#checkIn-btnSessionInList').removeClass('unselected').addClass('selected');
                jQuery('#checkIn-btnRFID').removeClass('selected').addClass('unselected');
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
        RwServices.order.cancelContract(requestCancelContract, function(responseCancelContract) {
            // in case there is an error we still want to navigate away
        });
        navigateAway();
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