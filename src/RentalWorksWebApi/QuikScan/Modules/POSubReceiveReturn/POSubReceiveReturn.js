var POSubReceiveReturn = {};
//----------------------------------------------------------------------------------------------
POSubReceiveReturn.getPOReceiveReturnScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, contractType, captionBarcodeICode, captionOK, captionReceiveBy;
    
    if (typeof properties.moduleType === 'undefined') throw 'POSubReceiveReturn.getPOReceiveReturnScreen: properties.moduleType is required.';
    switch(properties.moduleType) {
        case RwConstants.moduleTypes.SubReceive:
            pageTitle = RwLanguages.translate('PO Sub-Receive');
            contractType = 'RECEIVE';
            captionBarcodeICode = RwLanguages.translate('I-Code');
            captionReceiveBy = RwLanguages.translate('Receive by');
            captionOK = RwLanguages.translate('Receive');
            break;
        case RwConstants.moduleTypes.SubReturn:  
            pageTitle = RwLanguages.translate('PO Sub-Return');
            contractType = 'RETURN';
            captionBarcodeICode = RwLanguages.translate('Bar Code / I-Code');
            captionReceiveBy = RwLanguages.translate('Return by');
            captionOK = RwLanguages.translate('Return');
            break;
        default: throw 'POSubReceiveReturn.getPOReceiveReturnScreen moduleType not supported';
    };
    combinedViewModel = jQuery.extend({
        captionPageTitle:         pageTitle
      , captionPageSubTitle:      '<div class="title">' + RwLanguages.translate('PO No') + ': ' + properties.webSelectPO.poNo + '&nbsp;&nbsp;&nbsp;&nbsp;'  + RwLanguages.translate('Session') + ': ' + properties.sessionNo + '<br/>' + properties.webSelectPO.poDesc+ '</div>'
      , htmlScanBarcode:          RwPartialController.getScanBarcodeHtml({
            captionInstructions: RwLanguages.translate('Select Item to Stage...')
          , captionBarcodeICode: captionBarcodeICode
        })
      , captionICode:               RwLanguages.translate('I-Code')
      , captionOrdered:             RwLanguages.translate('Ordered')
      , captionSession:             RwLanguages.translate('Session')
      , captionReceived:            RwLanguages.translate('Received')
      , captionRemaining:           RwLanguages.translate('Remaining')
      , captionReturned:            RwLanguages.translate('Returned')
      , captionSubReceiveHeader:    RwLanguages.translate('How many?')
      , captionDesc:                RwLanguages.translate('Desc')
      , captionQty:                 RwLanguages.translate('Qty')
      , captionOK:                  captionOK
      , captionCancel:              RwLanguages.translate('Cancel')
      , captionReceiveBy:           captionReceiveBy
      , captionBarcode:             RwLanguages.translate('Bar Code')
      , captionScanVendorBarcodes:  'Scan Vendor Bar Code(s)'
      , captionBtnItems:            RwLanguages.translate('Items')
      , captionBtnScan:             RwLanguages.translate('Scan')
      , captionBtnSessionList:      RwLanguages.translate('Session')
      , captionCreateContract:      RwLanguages.translate('Create Contract')
      , captionAll:                 RwLanguages.translate('All')
    }, viewModel);

    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-POSubReceiveReturn').html(), combinedViewModel, {});
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    
    screen.$btncreatecontract = FwMobileMasterController.addFormControl(screen, 'Create Contract', 'right', '&#xE5CC;', false, function() { //continue
        try {
            screen.createContract();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.createContract = function() {
        if ((typeof properties.contractId === 'undefined') || (properties.contractId.length === 0)) {
            throw 'contractid is required. {7EDF14A3-C136-4B7E-BA2D-A426101A33D3}';
        }
        properties.contract = {
              contractType:        contractType
            , contractId:          properties.contractId
            , orderId:             properties.webSelectPO.poId
            , responsiblePersonId: ''
        };
        program.pushScreen(RwOrderController.getContactSignatureScreen(viewModel, properties));
    };
    
    screen.$view.find('#poSubReceiveReturn-messages').toggle(applicationConfig.designMode || false);
    screen.$view.find('#poSubReceiveReturn-info').toggle(applicationConfig.designMode || false);

    var $tabpending = FwMobileMasterController.tabcontrols.addtab('Pending', true);
    $tabpending.on('click', function() {
        try {
            screen.getItems(false);
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
    var $taball  = FwMobileMasterController.tabcontrols.addtab('All', false);
    $taball.on('click', function() {
        try {
            screen.getItems(true);
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
    screen.$view
        .on('change', '.txticode', function() {
            var $txtBarcodeData, request;
            try {
                $txtBarcodeData = jQuery(this);
                if (typeof properties.webSelectPO      === 'undefined') throw 'RwOrderController.getPOSubReceiveScreen requires properties.webSelectPO';
                if (typeof properties.webSelectPO.poId === 'undefined') throw 'RwOrderController.getPOSubReceiveScreen requires properties.webSelectPO.poId';
                if (typeof properties.moduleType       === 'undefined') throw 'RwOrderController.getPOSubReceiveScreen requires properties.moduleType';
                if (typeof properties.contractId       === 'undefined') throw 'RwOrderController.getPOSubReceiveScreen requires properties.contractId';
                request = {
                    poId:        properties.webSelectPO.poId
                  , code:        RwAppData.stripBarcode($txtBarcodeData.val().toUpperCase())
                  , barcode:     ''
                  , qty:         0
                  , moduleType:  properties.moduleType
                  , assignBC:    false
                  , contractId:  properties.contractId
                };
                screen.webSubReceiveReturnItem(request, function() {});
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#poSubReceiveReturn-items-ul > li.link', function() {
            var $this, status, genericMsg, msg, masteritemid, masterno, description, trackedby, isbarcode, qtyordered, qtyreceived, qtyremaining, qtyreturned, qtysession, setReceiveReturnByVendorBarCode;
            try {
                $this = jQuery(this);
                status        = 0;
                genericMsg    = '';
                msg           = '';
                masteritemid  = $this.data('masteritemid');
                masterno      = $this.data('masterno');
                description   = $this.data('description');
                isbarcode     = ($this.data('trackedby') === 'BARCODE');
                netqtyordered = $this.data('netqtyordered');
                qtyreceived   = $this.data('qtyreceived');
                qtyremaining  = $this.data('qtyremaining');
                qtyreturned   = $this.data('qtyreturned');
                qtysession    = $this.data('qtysession');
                setReceiveReturnByVendorBarCode = false;
                screen.showPopupQty(status, genericMsg, msg, masteritemid, masterno, description, isbarcode, netqtyordered, qtyreceived, qtyremaining, qtyreturned, qtysession, false);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.getSuspendedSessionPopup = function(suspendedContracts) {'use strict';
        var result, html, statusdate, sessionno, orderno, orderdesc, deal, username, status, rowView, rowModel;
    
        result = {};
        rowView = jQuery('#tmpl-po-suspendedSession').html();
        html = [];
        html.push('<div class="po-suspendedsessions">');
        if (typeof suspendedContracts === 'object') {
            for (var rowno = 0; rowno < suspendedContracts.Rows.length; rowno++) {
                rowModel = {};
                rowModel.statusdate = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.statusdate];
                rowModel.sessionno  = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.sessionno];
                rowModel.orderno    = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.orderno];
                rowModel.orderdesc  = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.orderdesc];
                rowModel.deal       = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.deal];
                rowModel.username   = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.username];
                rowModel.status     = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.status];
                html.push(Mustache.render(rowView, rowModel));
            }
        }
        html.push('</div>');
        result.$popup = FwConfirmation.renderConfirmation(suspendedContracts.Rows.length.toString() + ' Suspended Session' + ((suspendedContracts.Rows.length === 1) ? '' : 's'), html.join(''));
        result.$popup.attr('data-nopadding', 'true');
        result.$btnJoinSession = FwConfirmation.addButton(result.$popup, 'Join Session', false);
        result.$btnJoinSession.on('click', function() {
            var $suspendedsession;
            try {
                $suspendedsession = result.$popup.find('.po-suspendedsession');
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
    
    screen.showPopupQty = function(status, genericMsg, msg, masteritemid, masterno, description, isBarCode, netqtyordered, qtyreceived, qtyremaining, qtyreturned, qtysession, setReceiveReturnByVendorBarCode) {
        screen.hidePopupQty();
        var template = Mustache.render(jQuery('#tmpl-POSubReceiveReturn-PopupQty').html(), {
            captionICode:              RwLanguages.translate('I-Code'),
            captionOrdered:            RwLanguages.translate('Ordered'),
            captionSession:            RwLanguages.translate('Session'),
            captionReceived:           RwLanguages.translate('Received'),
            captionRemaining:          RwLanguages.translate('Remaining'),
            captionReturned:           RwLanguages.translate('Returned'),
            captionSubReceiveHeader:   RwLanguages.translate('How many?'),
            captionDesc:               RwLanguages.translate('Desc'),
            captionBarcode:            RwLanguages.translate('Bar Code'),
            captionQty:                RwLanguages.translate('Qty'),
            captionBtnItems:           RwLanguages.translate('Items'),
            captionBtnScan:            RwLanguages.translate('Scan'),
            captionBtnSessionList:     RwLanguages.translate('Session'),
            captionCreateContract:     RwLanguages.translate('Create Contract'),
            captionAll:                RwLanguages.translate('All'),
            valueTxtQty:               '',
            captionAdd:                RwLanguages.translate('Add'),
            captionRemove:             RwLanguages.translate('Remove'),
            captionOK:                 captionOK,
            captionCancel:             RwLanguages.translate('Cancel'),
            captionReceiveBy:          captionReceiveBy,
            captionScanVendorBarcodes: 'Scan Vendor Bar Code(s)'
        });
        var $popupcontent = jQuery(template);
        screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
        FwPopup.showPopup(screen.$popupQty);
        screen.$popupQty
            .on('click', '#poSubReceiveReturn-ulReceiveBy > li > input[type="radio"]', function() {
                var receieveByBarcode;
                try {
                    receiveByBarcode = (jQuery(this).val() === 'T');
                    jQuery('#poSubReceiveReturn-fieldQty').toggle(!receiveByBarcode);
                    jQuery('#poSubReceiveReturn-barcodes').toggle(receiveByBarcode);
                    jQuery('#poSubReceiveReturn-btnOK').toggle(!receiveByBarcode);
                    jQuery('#poSubReceiveReturn-txtVendorBarCode').val('');
                    if (receiveByBarcode) {
                        program.setScanTarget('#poSubReceiveReturn-txtVendorBarCode');
                    } else {
                        program.setScanTarget('.txticode');
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('change', '#poSubReceiveReturn-txtQty', function() {
                var $txtQty, qty, html, isNotANumber;
                try {
                    $txtQty = jQuery(this);
                    isNotANumber = isNaN($txtQty.val());
                    if (isNotANumber) {
                        FwFunc.showError('Please enter a number.');
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('change', '#poSubReceiveReturn-txtVendorBarCode', function() {
                var request;
                try {
                    request = {
                        poId:         properties.webSelectPO.poId
                      , masterItemId: jQuery('#poSubReceiveReturn-popupQty').data('masteritemid')
                      , code:         jQuery('#poSubReceiveReturn-popupQty-masterNo').html().toUpperCase()
                      , barcode:      RwAppData.stripBarcode(this.value)
                      , qty:          1
                      , moduleType:   properties.moduleType
                      , contractId:   properties.contractId
                    };
                    screen.webSubReceiveReturnItem(request, function(response) {
                        if (response.webSubReceiveReturnItem.status === 0) {
                            screen.hidePopupQty();
                            screen.getItems(false);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#poSubReceiveReturn-btnOK', function() {
                var request;
                try {
                    request = {
                        poId:         properties.webSelectPO.poId
                      , masterItemId: jQuery('#poSubReceiveReturn-popupQty').data('masteritemid')
                      , code:         jQuery('#poSubReceiveReturn-popupQty-masterNo').html().toUpperCase()
                      , barcode:      RwAppData.stripBarcode(jQuery('#poSubReceiveReturn-txtVendorBarCode').val())
                      , qty:          jQuery('#poSubReceiveReturn-txtQty').val()
                      , moduleType:   properties.moduleType
                      , contractId:   properties.contractId
                    };
                    screen.webSubReceiveReturnItem(request, function(response) {
                        if (response.webSubReceiveReturnItem.status === 0) {
                            screen.hidePopupQty();
                            screen.getItems(false);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#poSubReceiveReturn-btnCancel', function() {
                try {
                    screen.hidePopupQty();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;

        
        var $liRemainingList, li;
        jQuery('#poSubReceiveReturn-popupQty').data('masteritemid', masteritemid);
        if (status === 0) {
            $liRemainingList = jQuery('#poSubReceiveReturn-items-ul li[data-masteritemid=' + masteritemid + ']');
            if ((qtyremaining <= 0) && (jQuery('#poSubReceiveReturn-items-ulRemainingView input:radio:checked[name=remainingview]').val() === 'REMAINING')) {
                $liRemainingList.remove().empty();
                if (jQuery('#poSubReceiveReturn-items-ul li').length === 0) {
                    li = [];
                    li.push('<li class="normal">');
                        li.push('<div class="empty">' + RwLanguages.translate('0 items found') + '</div>');
                    li.push('</li>');
                    jQuery('#poSubReceiveReturn-items-ul').html(li.join(''));
                }
            } else {
                $liRemainingList
                    .data('netqtyordered', netqtyordered)
                    .data('qtyreceived',   qtyreceived)
                    .data('qtyremaining',  qtyremaining)
                    .data('qtyreturned',   qtyreturned)
                    .data('qtysession',    qtysession)
                ;
                $liRemainingList.find('table > tbody > tr > td.netqtyordered.value').html(String(netqtyordered));
                $liRemainingList.find('table > tbody > tr > td.qtyreceived.value').html(String(qtyreceived));
                $liRemainingList.find('table > tbody > tr > td.qtyremaining.value').html(String(qtyremaining));
                $liRemainingList.find('table > tbody > tr > td.qtyreturned.value').html(String(qtyreturned));
                $liRemainingList.find('table > tbody > tr > td.qtysession.value').html(String(qtysession));
            }
            jQuery('#poSubReceiveReturn-divCaptionSubReceiveHeader')
                .html(description);
            jQuery('#poSubReceiveReturn-popupQty-masterNo').html(masterno);
            jQuery('#poSubReceiveReturn-popupQty-divDetails-tdNetQtyOrdered').html(String(netqtyordered));
            jQuery('#poSubReceiveReturn-popupQty-divDetails-tdQtyReceived').html(String(qtyreceived));
            jQuery('#poSubReceiveReturn-popupQty-divDetails-tdQtyRemaining').html(String(qtyremaining));
            jQuery('#poSubReceiveReturn-popupQty-divDetails-tdQtyReturned').html(String(qtyreturned));
            jQuery('#poSubReceiveReturn-popupQty-divDetails-tdQtySession').html(String(qtysession));
        
            if (setReceiveReturnByVendorBarCode) {
                jQuery('#poSubReceiveReturn-liReceiveByBarCode').click();
            } else {
                jQuery('#poSubReceiveReturn-liReceiveByQty').click();
            }
            jQuery('#poSubReceiveReturn-txtQty').val('');
            jQuery('#poSubReceiveReturn-txtVendorBarCode').val('');
        }
        jQuery('#poSubReceiveReturn-divCaptionSubReceiveHeader').hide();
        jQuery('#poSubReceiveReturn-btnOK').toggle(applicationConfig.designMode || (status === 0));
        jQuery('#poSubReceiveReturn-divReceiveBy').toggle(applicationConfig.designMode || ((status === 0) && isBarCode));
        jQuery('#poSubReceiveReturn-popupQty-messages')
            .toggle(applicationConfig.designMode || (genericMsg.length > 0) || (msg.length > 0));
        jQuery('#poSubReceiveReturn-popupQty-messages-genericMsg')
            .toggleClass('qserror', (status !== 0))
            .toggleClass('qssuccess', (status === 0))
            .html(genericMsg)
            .toggle(applicationConfig.designMode|| (genericMsg.length > 0));
        jQuery('#poSubReceiveReturn-popupQty-messages-msg')
            .html(msg)
            .toggle(applicationConfig.designMode || (msg.length > 0));
        jQuery('#poSubReceiveReturn-popupQty-divDetails').toggle(applicationConfig.designMode || (status === 0));
        jQuery('#poSubReceiveReturn-popupQty-divFieldPanel').toggle(applicationConfig.designMode || (status === 0));
        jQuery('#poSubReceiveReturn-txtQty').val(qtyremaining);

        
        jQuery('#poSubReceiveReturn-liReceiveByQty').click();
        //FwPopup.showPopup(screen.$popupQty);
    };

    screen.hidePopupQty = function() {
        if (typeof screen.$popupQty === 'object') {
            FwPopup.destroyPopup(screen.$popupQty);
        }
        jQuery('.txticode').val('');
    };

    screen.getItems = function(showAll) {
        var request;
        request = {
            moduleType: properties.moduleType
          , poId:       properties.webSelectPO.poId
          , contractId: properties.contractId
          , showAll:    showAll
        };
        RwServices.callMethod('POSubReceiveReturn', 'GetPOSubReceiveReturnPendingList', request, function(response) {
            var dt, ul, li, isAlternate, colIndex, masteritemid, masterno, description, trackedby, netqtyordered, qtyreceived, qtyremaining, qtyreturned, qtysession;
            var showCreateContract = false;
            isAlternate = false;
            dt = response.poSubReceiveReturnPendingList;
            colIndex = dt.ColumnIndex;
            ul = [];
            if (dt.Rows.length > 0) {
                screen.$btncreatecontract.toggle(sessionStorage.users_enablecreatecontract === 'T');
            }
            for (var i = 0; i < dt.Rows.length; i++) {
                masteritemid  = dt.Rows[i][colIndex.masteritemid];
                masterno      = dt.Rows[i][colIndex.masterno];
                description   = dt.Rows[i][colIndex.description];
                trackedby     = dt.Rows[i][colIndex.trackedby];
                netqtyordered = dt.Rows[i][colIndex.netqtyordered].toString().replace('.00', '');
                qtyreceived   = dt.Rows[i][colIndex.qtyreceived].toString().replace('.00', '');
                qtyremaining  = dt.Rows[i][colIndex.qtyremaining].toString().replace('.00', '');
                qtyreturned   = dt.Rows[i][colIndex.qtyreturned].toString().replace('.00', '');
                qtysession    = dt.Rows[i][colIndex.qtysession].toString().replace('.00', '');
                description   = dt.Rows[i][colIndex.description];
                li = [];
                if (((properties.moduleType === RwConstants.moduleTypes.SubReceive) && (qtyreceived > 0)) ||
                    ((properties.moduleType === RwConstants.moduleTypes.SubReturn)  && (qtyreturned > 0))){
                   showCreateContract = true;
                }
                if (qtyremaining > 0) {
                    cssClass = 'link';
                } else {
                    cssClass = 'nonlink';
                }
                if (!isAlternate) {
                    cssClass += ' normal';
                } else {
                    cssClass += ' alternate';
                }
                isAlternate = !isAlternate;
                li.push('<li class="' + cssClass + '" ' +
                            'data-masteritemid="'  + masteritemid +
                          '" data-masterno="'      + masterno +
                          '" data-description="'   + description   +
                          '" data-trackedby="'     + trackedby +
                          '" data-netqtyordered="' + netqtyordered +
                          '" data-qtyreceived="'   + qtyreceived +
                          '" data-qtyremaining="'  + qtyremaining +
                          '" data-qtyreturned="'   + qtyreturned +
                          '" data-qtysession="'    + qtysession + '">');
                    li.push('<div class="description">' + description + '</div>');
                    li.push('<table>');
                        li.push('<tbody>');
                            li.push('<tr>');
                                li.push('<td class="col1 masterno key">'       + RwLanguages.translate('I-Code')      + ':</td>');
                                li.push('<td class="col2 masterno value">'     + masterno                             + '</td>');
                                li.push('<td class="col3 qtyremaining key">'   + RwLanguages.translate('Remaining')   + ':</td>');
                                li.push('<td class="col4 qtyremaining value">' + String(qtyremaining)                 + '</td>');
                            li.push('</tr>');
                            li.push('<tr>');
                                li.push('<td class="col1 trackedby key">'      + RwLanguages.translate('Tracked By')  + ':</td>');
                                li.push('<td class="col2 trackedby value">'    + trackedby                            + '</td>');
                                li.push('<td class="col3 qtysession key">'     + RwLanguages.translate('Session')     + ':</td>');
                                li.push('<td class="col4 qtysession value">'   + String(qtysession)                   + '</td>');
                            li.push('</tr>');
                        li.push('</tbody>');
                    li.push('</table>');
                li.push('</li>');
                if (request.showAll || (!request.showAll && qtyremaining > 0)) {
                    ul.push(li.join(''));
                }
            }
            if (showCreateContract) {
                screen.$btncreatecontract.show();
            }
            if (dt.Rows.length === 0) {
                li = [];
                li.push('<li class="normal">');
                    li.push('<div class="empty">' + RwLanguages.translate('0 items found') + '</div>');
                li.push('</li>');
                ul.push(li.join(''));
            }
            jQuery('#poSubReceiveReturn-items-ul').html(ul.join(''));
            jQuery('#poSubReceiveReturn-items').show();
        });
    };

    screen.webSubReceiveReturnItem = function(request, onComplete) {
        RwServices.callMethod('POSubReceiveReturn', 'WebSubReceiveReturnItem', request, function(response) {
            var status, genericMsg, msg, masteritemid, masterno, description, isbarcode, netqtyordered, qtyreceived, qtyremaining, qtyreturned, qtysession, setReceiveReturnByVendorBarCode;
            try {  
                if ((typeof response === 'object') && (typeof response.webSubReceiveReturnItem === 'object')) {
                    properties.webSubReceiveReturnItem = response.webSubReceiveReturnItem;
                    status          = response.webSubReceiveReturnItem.status;
                    genericMsg      = response.webSubReceiveReturnItem.genericMsg;
                    msg             = response.webSubReceiveReturnItem.msg;
                    masteritemid    = response.request.masterItemId;
                    masterno        = response.request.code;
                    description     = response.webSubReceiveReturnItem.description;
                    isbarcode       = response.webSubReceiveReturnItem.isBarCode;
                    netqtyordered   = response.webSubReceiveReturnItem.qtyOrdered;
                    qtyreceived     = response.webSubReceiveReturnItem.qtyReceived;
                    qtyremaining    = response.webSubReceiveReturnItem.qtyRemaining;
                    qtyreturned     = response.webSubReceiveReturnItem.qtyReturned;
                    qtysession      = response.webSubReceiveReturnItem.qtySession;
                    setReceiveReturnByVendorBarCode  = (response.request.barcode.length > 0);
                    screen.showPopupQty(status, genericMsg, msg, masteritemid, masterno, description, isbarcode, netqtyordered, qtyreceived, qtyremaining, qtyreturned, qtysession, setReceiveReturnByVendorBarCode);
                } else {
                    throw 'An unknown error occured.';
                }
                onComplete(response);
            }
            catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.load = function() {
        program.setScanTarget('.txticode');
        if (!Modernizr.touch) {
            jQuery('.txticode').select();
        }
        $tabpending.click();
        screen.getItems(true);
    };

    screen.unload = function() {
        
    };

    screen.beforeNavigateAway = function(navigateAway) {
        var requestCancelContract = {
            contractId:                    properties.contractId,
            activityType:                  properties.activityType,
            dontCancelIfOrderTranExists:   true,
            failSilentlyOnOwnershipErrors: true
        };
        RwServices.order.cancelContract(requestCancelContract, function(responseCancelContract) {
            navigateAway(true);
        });
    };
    
    return screen;
};