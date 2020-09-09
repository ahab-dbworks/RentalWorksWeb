var RwFillContainer: any = {};
//----------------------------------------------------------------------------------------------
RwFillContainer.getFillContainerScreen = function(viewModel, properties) {
    var combinedViewModel, screen, useResponsiblePerson, applicationOptions, pageTitle, pageSubTitle;
    applicationOptions = program.getApplicationOptions();
    pageTitle    = RwLanguages.translate('Fill Container');
    pageSubTitle = '';
    
    combinedViewModel = jQuery.extend({
        captionPageTitle:         pageTitle,
        captionPageSubTitle:      pageSubTitle,
        htmlScanBarcode:          RwPartialController.getScanBarcodeHtml({
            captionInstructions: RwLanguages.translate('Select Item to Stage...'),
            captionBarcodeICode: RwLanguages.translate('Bar Code / I-Code')
        }),
        captionAdd:                RwLanguages.translate('Add'),
        captionRemove:             RwLanguages.translate('Remove'),
        captionCancel:             RwLanguages.translate('Cancel'),
        captionUnstageMode:        RwLanguages.translate('Mode'),
        captionResponsiblePerson:  RwLanguages.translate('Responsible Person'),
        valueTxtQty:               '',
        captionQty:                RwLanguages.translate('Qty'),
        captionSummary:            RwLanguages.translate('Summary'),
        captionOrdered:            RwLanguages.translate('Ordered'),
        captionICodeDesc:          RwLanguages.translate('I-Code'),
        captionSub:                RwLanguages.translate('Sub'),
        captionOut:                RwLanguages.translate('Out'),
        captionIn:                 RwLanguages.translate('In'),
        captionStaged:             RwLanguages.translate('Staged'),
        captionRemaining:          RwLanguages.translate('Remaining'),
        captionAddToOrder:         RwLanguages.translate('Would you like to add this to the order?'),
        captionAddComplete:        RwLanguages.translate('Add Complete'),
        captionAddItem:            RwLanguages.translate('Add Item'),
        captionYes:                RwLanguages.translate('Yes'),
        captionDesc:               RwLanguages.translate('Description'),
        captionPendingListButton:  RwLanguages.translate('Pending'),
        captionScanButton:         RwLanguages.translate('Scan'),
        captionRFIDButton:         RwLanguages.translate('RFID'),
        captionStagedListButton:   RwLanguages.translate('Container'),
        captionCreateContract:     RwLanguages.translate('Create Contract'),
        captionSerialNoSelection:  RwLanguages.translate('Serial No. Selection'),
        captionAddAllQtyItems:     RwLanguages.translate('Add All Qty Items'),
        captionFillContainer:      RwLanguages.translate('Fill Container'),
        captionScannableBrItem:    RwLanguages.translate('Scannable<br/>Item'),
        captionContainerBrItem:    RwLanguages.translate('Container<br/>Item'),
        captionContainerBrDesc:    RwLanguages.translate('Container<br/>Desc'),
        captionContainerBrNo:      RwLanguages.translate('Container<br/>No'),
        captionContainerNo:        RwLanguages.translate('Container No'),
        captionNewContainer:       RwLanguages.translate('New'),
        captionCloseContainer:     RwLanguages.translate('Close'),
        captionRequired:           RwLanguages.translate('Required'),
        captionSetContainerNo:     RwLanguages.translate('Set Container No'),
        captionCreateContainer:    RwLanguages.translate('Create Container')
    }, viewModel);

    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-fillcontainer').html(), combinedViewModel, {});
    screen       = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    FwControl.renderRuntimeControls(screen.$view.find('.fwcontrol'));
    screen.$view.find('#fillcontainer-pendingitems-pnladdallqtyitems').toggle(sessionStorage.getItem('users_qsallowapplyallqtyitems') === 'true');

    screen.$view.find('.containerdesc').data('beforegetmany', (request: GetManyRequest): void => {
        //// Add WarehouseId filter to ContainerDescription validation request
        //let filterWarehouseId = new GetManyFilter();
        //const warehouseJson = sessionStorage.getItem('warehouse');
        //let warehouse = null;
        //let warehouseId = '';
        //if (warehouseJson !== null) {
        //    warehouse = JSON.parse(warehouseJson);
        //    if (typeof warehouse === 'object' && typeof warehouse.warehouseid === 'string') {
        //        warehouseId = warehouse.warehouseid;
        //    }
        //}
        //if (warehouseId.length === 0) {
        //    throw 'WarehouseId is required.';
        //}
        //filterWarehouseId.fieldName = 'WarehouseId';
        //filterWarehouseId.comparisonOperator = 'eq';
        //filterWarehouseId.fieldValue = warehouseId;
        //request.filters.push(filterWarehouseId);

        // Add ScannableMasterId filter to ContainerDescription validation request
        let filterScannableMasterId = new GetManyFilter();
        let scannableMasterId = '';
        if (screen.$view.find('.scannablemasterid .fwformfield-value').length > 0) {
            scannableMasterId = screen.$view.find('.scannablemasterid .fwformfield-value').val();
        }
        if (scannableMasterId.length === 0) {
            throw 'ScannableMasterId is required.';
        }
        filterScannableMasterId.fieldName = 'ScannableInventoryId';
        filterScannableMasterId.comparisonOperator = 'eq';
        filterScannableMasterId.fieldValue = scannableMasterId;
        request.filters.push(filterScannableMasterId);
    });

    screen.$btnback = FwMobileMasterController.addFormControl(screen, 'Close', 'left', '&#xE5CB;', false, function() { //back
        try {
            if (screen.$view.find('.fillcontainerheader .containeritem').is(':visible')) {
                var $confirmation = FwConfirmation.renderConfirmation(RwLanguages.translate('Confirm'), RwLanguages.translate('Are you finished filling this Container?'));
                var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                FwConfirmation.addButton($confirmation, 'Cancel', true);
                $btnok.on('click', function() {
                    screen.closeFillContainer();
                });
            } else {
                screen.closeFillContainer();
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btncontinue = FwMobileMasterController.addFormControl(screen, 'Continue', 'right', '&#xE5CC;', false, function() { //continue
        var request, barcode, containerid;
        try {
            containerid = FwFormField.getValue(screen.$view, '.containerdesc');
            if (containerid === '') {
                throw 'Container Type is required.';
            }
            if (properties.mode === 'fillcontainer') {
                screen.confirmMakeContainer();
            } else if (properties.mode === 'checkin') {
                barcode = screen.getScannableItemBarcode();
                request = {
                    mode: properties.mode,
                    barcode: barcode
                };
                request.checkin = {
                    moduleType:     properties.moduleType,
                    checkInMode:    properties.checkInMode,
                    code:           barcode,
                    neworderaction: '',
                    aisle:          screen.getAisle(),
                    shelf:          screen.getShelf(),
                    vendorid:       '',
                    contractid:     screen.getCheckInContractId(),
                    orderid:        properties.orderid,
                    dealid:         screen.getDealId(),
                    departmentid:   screen.getDepartmentId(),
                    containerid:    containerid,
                    containerdesc:  FwFormField.getText(screen.$view, '.containerdesc')
                };
                RwServices.callMethod('FillContainer', 'SelectContainer', request, function(response) {
                    var masterno, description, containerno, containerid, containeritemid, outcontractid, rentalitemid, usecontainerno, serviceerrormessage;
                    try {
                        if (response.serviceerrormessage.length > 0) {
                            FwFunc.showError(response.serviceerrormessage);
                        } else {
                            masterno        = response.selectcontainer.container.masterno;
                            description     = response.selectcontainer.container.description;
                            containerno     = response.selectcontainer.container.containerno;
                            containerid     = response.selectcontainer.container.containerid;
                            containeritemid = response.selectcontainer.container.containeritemid;
                            outcontractid   = response.selectcontainer.container.outcontractid;
                            rentalitemid    = response.selectcontainer.container.containerrentalitemid;
                            usecontainerno  = false;
                            screen.selectContainer(barcode, masterno, description, containerno, containerid, containeritemid, outcontractid, rentalitemid, usecontainerno);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.hascreatecontainer = (applicationConfig.designMode) || ((properties.mode === 'fillcontainer') && (sessionStorage.users_enablecreatecontract === 'true'));
    screen.$btncreatecontainer = FwMobileMasterController.addFormControl(screen, 'Create Container', 'right', '&#xE5CC;', false, function () { //continue
        try {
            var request = {
                mode: screen.getMode(),
                contractid: screen.getCheckInContractId(),
                containeritemid: screen.getContainerItemId()
            };
            RwServices.callMethod('FillContainer', 'GetContainerItems', request, function (response) {
                if (response.containeritems.Rows.length > 0) {
                    var requestCreateContainer;
                    try {
                        requestCreateContainer = {
                            containeritemid: screen.getContainerItemId()
                        };
                        RwServices.FillContainer.CreateContainer(requestCreateContainer, function (responseCreateContainer) {
                            try {
                                screen.containerCreated = true;
                                program.navigate('home/home');
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                } else {
                    
                }
            });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.getMode = function () {
        return properties.mode;
    };
    
    screen.getContainerId = function() {
        var containerid = '';
        containerid = screen.containerid;
        return containerid;
    };

    screen.getOrderDesc = function() {
        return screen.orderdesc;
    };
    
    screen.getContainerItemId = function() {
        var containeritemid = '';
        if (typeof screen.containeritemid === 'string') {
            containeritemid = screen.containeritemid;
        }
        return containeritemid;
    };

    screen.getContainerOutContractId = function() {
        var containeroutcontractid = '';
        if (typeof screen.containeroutcontractid === 'string') {
            containeroutcontractid = screen.containeroutcontractid;
        }
        return containeroutcontractid;
    };

    screen.getCheckInContractId = function() {
        var checkincontractid = '';
        if (properties.mode === 'checkin') {
            checkincontractid = properties.checkincontractid;
        }
        return checkincontractid;
    };

    screen.getCheckInOrderId = function() {
        var checkinorderid = '';
        if (properties.mode === 'checkin') {
            checkinorderid = properties.orderid;
        }
        return checkinorderid;
    };

    screen.getDealId = function() {
        var dealid = '';
        if (properties.mode === 'checkin') {
            dealid = properties.dealid;
        }
        return dealid;
    };

    screen.getDepartmentId = function() {
        var departmentId = '';
        if (properties.mode === 'checkin') {
            departmentId = properties.departmentId;
        }
        return departmentId;
    };

    screen.getAisle = function() {
        var aisle = '';
        if (properties.mode === 'checkin') {
            aisle = properties.aisle;
        }
        return aisle;
    };

    screen.getShelf = function() {
        var shelf = '';
        if (properties.mode === 'checkin') {
            shelf = properties.shelf;
        }
        return shelf;
    };

    screen.getRentalitemid = function() {
        var rentalitemid = '';
        if (typeof screen.rentalitemid === 'string') {
            rentalitemid = screen.rentalitemid;
        }
        return rentalitemid;
    };

    screen.resetVariables = function() {
        screen.containerid  = '';
        screen.orderdesc    = '';
        screen.containeroutcontractid   = '';
        screen.rentalitemid = '';
    };

    screen.toggleRfid = function() {
        if (RwRFID.isConnected) {
            screen.$view.find('#fillcontainer-btnrfid').css('display', 'inline-block');
            screen.$view.find('#fillcontainer-btnrfid').click();
            var requestRFIDClear: any = {};
            requestRFIDClear.sessionid = screen.getContainerItemId();     // this looks wrong mv 2016-05-01 doesn't seem to be used
            RwServices.order.rfidClearSession(requestRFIDClear, function(response) {});
        } else {
            screen.$view.find('#fillcontainer-btnrfid').css('display', 'none');
            screen.$view.find('#fillcontainer-btnscan').click();
        }
    };

    screen.showPopupQty = function() {
        FwPopup.showPopup(screen.$popupQty);
    };

    screen.hidePopupQty = function() {
        FwPopup.destroyPopup(screen.$popupQty);
        jQuery('#scanBarcodeView-txtBarcodeData').val('');
    };

    screen.showPopupSelectSerialNo = function() {
        FwPopup.showPopup(screen.$popupSelectSerialNo);
    };

    screen.hidePopupSelectSerialNo = function() {
        FwPopup.destroyPopup(screen.$popupSelectSerialNo);
        jQuery('#scanBarcodeView-txtBarcodeData').val('');
    };

    screen.pdastageitemCallback = function(responseStageItem) {
        var $liPendingItem;
        
        screen.renderPopupQty();
        jQuery('#fillcontainer-popupqty')
            .data('code',         responseStageItem.request.code)
            .data('masterno',     responseStageItem.webStageItem.masterNo)
            .data('masteritemid', responseStageItem.webStageItem.masterItemId)
        ;
        if (responseStageItem.webStageItem.itemType === 'NonBarCoded') {
            jQuery('#fillcontainer-popupqty').data('code', responseStageItem.webStageItem.masterNo);
        }
        
        $liPendingItem = jQuery('#fillcontainer-pendingitems-ul > li[data-masteritemid="' + responseStageItem.webStageItem.masterItemId + '"]');
        if (responseStageItem.webStageItem.qtyRemaining !== 0)  {
            $liPendingItem
                .data('missingqty',      (responseStageItem.webStageItem.qtyRemaining))
                .data('qtyordered',      String(responseStageItem.webStageItem.qtyOrdered))
                .data('qtystagedandout', String(responseStageItem.webStageItem.qtyStaged + responseStageItem.webStageItem.qtyOut))
            ;
            $liPendingItem.find('table > tbody > tr > td.qtyrequired.value')    .html(String(responseStageItem.webStageItem.qtyRemaining));
            $liPendingItem.find('table > tbody > tr > td.qtyordered.value')     .html(String(responseStageItem.webStageItem.qtyOrdered));
            $liPendingItem.find('table > tbody > tr > td.qtystagedandout.value').html(String(responseStageItem.webStageItem.qtyStaged + responseStageItem.webStageItem.qtyOut));
        } else {
            $liPendingItem.remove();
            if (jQuery('#fillcontainer-pendingitems-ul > li').length === 0) {
                jQuery('#fillcontainer-pendingitems-ul').append(screen.getEmptyListItem());
            }
        }

        screen.$popupQty.find('#fillcontainer-popupqty-genericmsg')  .html(responseStageItem.webStageItem.genericMsg);
        screen.$popupQty.find('#fillcontainer-popupqty-msg')         .html(responseStageItem.webStageItem.msg);
        screen.$popupQty.find('#fillcontainer-popupqty-masterno')    .html(responseStageItem.webStageItem.masterNo);
        screen.$popupQty.find('#fillcontainer-popupqty-description') .html(responseStageItem.webStageItem.description);
        screen.$popupQty.find('#fillcontainer-popupqty-orderdesc')   .html(screen.getOrderDesc());
        screen.$popupQty.find('#fillcontainer-popupqty-qtyrequired') .html(String(responseStageItem.webStageItem.qtyOrdered));
        screen.$popupQty.find('#fillcontainer-popupqty-qtyin')       .html(String(responseStageItem.webStageItem.qtyIn));
        screen.$popupQty.find('#fillcontainer-popupqty-qtyremaining').html(String(responseStageItem.webStageItem.qtyRemaining));
        if (responseStageItem.request.playStatus) {
            program.playStatus(responseStageItem.webStageItem.status === 0);
        }
        if (responseStageItem.webStageItem.status === 0) {
            screen.$popupQty.find('#fillcontainer-popupqty-genericmsg').removeClass('qserror').addClass('qssuccess');
            screen.$popupQty.find('#fillcontainer-popupqty-qty-txtqty')  .val(String(responseStageItem.webStageItem.qtyRemaining));
        } else {
            screen.$popupQty.find('#fillcontainer-popupqty-genericmsg').removeClass('qssuccess').addClass('qserror');
        }
        screen.$popupQty.find('#fillcontainer-popupqty-messages')      .toggle((applicationConfig.designMode) || ((responseStageItem.webStageItem.genericMsg.length > 0) || (responseStageItem.webStageItem.msg.length > 0)) && !((responseStageItem.webStageItem.isICode) && (responseStageItem.request.qty === 0)));  // hides the error so it doesn't show staged 0 message the first time you scan a qty item and it's prompting for the qty.
        screen.$popupQty.find('#fillcontainer-popupqty-genericmsg')    .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.genericMsg.length > 0));
        screen.$popupQty.find('#fillcontainer-popupqty-msg')           .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.msg.length > 0));
        screen.$popupQty.find('#fillcontainer-popupqty-qty')           .toggle((applicationConfig.designMode) || ((responseStageItem.request.qty === 0) && (responseStageItem.webStageItem.isICode) && !(responseStageItem.webStageItem.showAddCompleteToOrder || responseStageItem.webStageItem.showAddItemToOrder)));
        screen.$popupQty.find('#fillcontainer-popupqty-qty-btnaddtocontainer').toggle(true);
        screen.$popupQty.find('#fillcontainer-popupqty-qty-btnremovefromcontainer').toggle(false);
        screen.$popupQty.find('#fillcontainer-popupqty-fields')        .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.masterNo.length > 0));
        screen.$popupQty.find('#fillcontainer-popupqty-pnladdtoorder') .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddCompleteToOrder || responseStageItem.webStageItem.showAddItemToOrder));
        screen.$popupQty.find('#fillcontainer-popupqty-btnaddcomplete').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddCompleteToOrder));
        screen.$popupQty.find('#fillcontainer-popupqty-btnadditem')    .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddItemToOrder));
        screen.$popupQty.find('#fillcontainer-popupqty-unstagebarcode').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showUnstage));
        if (responseStageItem.webStageItem.status === 0) {
            if (responseStageItem.webStageItem.masterNo.length === 0) {
                screen.hidePopupQty();
            } else {
                if (responseStageItem.request.qty === 0) {
                    screen.showPopupQty();
                } else {
                    screen.showPopupQty();
                    setTimeout(
                        function() {
                            screen.hidePopupQty();
                        }
                      , 3000);
                }
            }
        } else {
            screen.showPopupQty();
        }
    };

    screen.getEmptyListItem = function() {
        var li;
        li = [];
        li.push('<li class="normal">');
            li.push('<div class="empty">' + RwLanguages.translate('0 items found') + '</div>');
        li.push('</li>');
        return li;
    };

    screen.getRequestCheckInItem = function(orderId, masterItemId, masterId, code, qty, vendorId, parentid) {
        var requestCheckInItem;
        requestCheckInItem = {
              moduleType:     properties.moduleType
            , checkInMode:    properties.checkInMode
            , contractId:     screen.getCheckInContractId()
            , orderId:        orderId
            , dealId:         screen.getDealId()
            , departmentId:   screen.getDepartmentId()
            , vendorId:       vendorId
            , consignorId:    ''
            , description:    ''
            , internalChar:   ''
            , masterItemId:   masterItemId
            , masterId:       masterId
            , parentId:       parentid
            , code:           RwAppData.stripBarcode(code.toUpperCase())
            , qty:            qty
            , newOrderAction: ''
            , containeritemid:        screen.getContainerItemId()
            , containeroutcontractid: screen.getContainerOutContractId()
            , aisle:          '' //properties.aisle
            , shelf:          '' //properties.shelf
            , playStatus:     true
        };
        return requestCheckInItem;
    };
    
    screen.checkInItem = function(orderId, masterItemId, masterId, code, qty, vendorId, parentid) {
        var requestCheckInItem = screen.getRequestCheckInItem(orderId, masterItemId, masterId, code, qty, vendorId, parentid);
        RwServices.order.checkInItem(requestCheckInItem, function(responseCheckInItem) {
            properties.responseCheckInItem = responseCheckInItem;
            screen.checkInItemCallback(responseCheckInItem);
        });
    };

    screen.checkInItemCallback = function(responseCheckInItem) {
        var $liCheckInPending, valTxtQty, isScannedICode, suspendedContractsPopup;
        
        if (responseCheckInItem.request.playStatus) {
            if ((responseCheckInItem.request.qty > 0) || responseCheckInItem.webCheckInItem.status !== 0) {
                program.playStatus(responseCheckInItem.webCheckInItem.status === 0);
            }
        }
        screen.renderPopupQty();
        jQuery('#fillcontainer-popupqty')
            .data('masterno',     responseCheckInItem.webCheckInItem.masterNo)
            .data('masteritemid', responseCheckInItem.webCheckInItem.masterItemId)
            .data('masterid',     responseCheckInItem.webCheckInItem.masterId)
            .data('orderid',      responseCheckInItem.request.orderId)
            .data('vendorid',     responseCheckInItem.webCheckInItem.vendorId)
            .data('parentid',     responseCheckInItem.webCheckInItem.parentid)
        ;

        isScannedICode = (responseCheckInItem.webCheckInItem.isICode) && (responseCheckInItem.request.orderId.length === 0);
        $liCheckInPending = jQuery('#fillcontainer-pendingitems-ul > li[data-masteritemid="' + responseCheckInItem.webCheckInItem.masterItemId + '"]');
        $liCheckInPending.find('table > tbody > tr > td.qtystillout.value').html(String(responseCheckInItem.webCheckInItem.stillOut));
        $liCheckInPending.find('table > tbody > tr > td.sessionin.value').html(String(responseCheckInItem.webCheckInItem.sessionIn));
        $liCheckInPending.data('qtyordered', responseCheckInItem.webCheckInItem.qtyOrdered);
        $liCheckInPending.data('qtyin', responseCheckInItem.webCheckInItem.totalIn);

        valTxtQty = String(responseCheckInItem.webCheckInItem.stillOut);
        jQuery('#fillcontainer-popupqty-qtyrequired').html(String(responseCheckInItem.webCheckInItem.outqty));
        jQuery('#fillcontainer-popupqty-qtyin').html(String(responseCheckInItem.webCheckInItem.sessionIn));
        jQuery('#fillcontainer-popupqty-qtyremaining').html(String(responseCheckInItem.webCheckInItem.stillOut));

        jQuery('#fillcontainer-popupqty-qty-txtqty').val(valTxtQty);
        jQuery('#fillcontainer-popupqty-genericmsg').html(responseCheckInItem.webCheckInItem.genericMsg);
        jQuery('#fillcontainer-popupqty-msg').html(responseCheckInItem.webCheckInItem.msg);
        jQuery('#fillcontainer-popupqty-masterno').html(responseCheckInItem.webCheckInItem.masterNo);
        jQuery('#fillcontainer-popupqty-description').html(responseCheckInItem.webCheckInItem.description);
        jQuery('#fillcontainer-popupqty-summary-table-trvendor').toggle(responseCheckInItem.webCheckInItem.vendorId.length > 0);
        jQuery('#fillcontainer-popupqty-pnladdtoorder').hide();
        jQuery('#fillcontainer-popupqty-unstagebarcode').hide();
        jQuery('#fillcontainer-popupqty-qty-addtocontainer').show();
        jQuery('#fillcontainer-popupqty-qty-btnremovefromcontainer').hide();
        if (responseCheckInItem.webCheckInItem.vendorId.length > 0) {
            jQuery('#fillcontainer-popupqty-vendor').html(responseCheckInItem.webCheckInItem.vendor);
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
        //if (((typeof responseCheckInItem.webCheckInItem.sessionNo === 'string') && (responseCheckInItem.webCheckInItem.sessionNo.length > 0)) && ((typeof properties.sessionNo === 'undefined') || (properties.sessionNo.length === 0))) {
        //    properties.sessionNo = responseCheckInItem.webCheckInItem.sessionNo;
        //    jQuery('#masterLoggedInView-captionPageSubTitle').html(RwLanguages.translate('Session') + ': ' + properties.sessionNo);
        //}
        //jQuery('#fillcontainer-newOrder')
        //    .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.showNewOrder));
        //jQuery('#fillcontainer-newOrder-btnSwap')
        //    .toggle(responseCheckInItem.webCheckInItem.allowSwap);
        if (responseCheckInItem.webCheckInItem.status === 0) {
            jQuery('#fillcontainer-popupqty-genericmsg').removeClass('qserror').addClass('qssuccess');
        }
        jQuery('#fillcontainer-popupqty-msg')
            .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.msg.length > 0));
        jQuery('#fillcontainer-popupqty-fields')
            .toggle(applicationConfig.designMode || (responseCheckInItem.webCheckInItem.status === 0));
        jQuery('#fillcontainer-popupqty-genericmsg')
            .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0));
        jQuery('#fillcontainer-popupqty-messages')
            .toggle((responseCheckInItem.webCheckInItem.msg.length > 0) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0));
        jQuery('#fillcontainer-popupqty-qty')
            .toggle((applicationConfig.designMode) || ((responseCheckInItem.request.qty === 0) && (responseCheckInItem.webCheckInItem.isICode) && responseCheckInItem.webCheckInItem.status === 0));
        jQuery('#fillcontainer-pageselectorinner').toggle(properties.contractId !== '');
        if ((responseCheckInItem.request.qty === 0) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0) || (responseCheckInItem.webCheckInItem.msg.length > 0)) {
            screen.showPopupQty();
            if (responseCheckInItem.webCheckInItem.status === 0) {
                if (responseCheckInItem.request.qty === 0) {
                        screen.showPopupQty();
                } else {
                    screen.showPopupQty();
                    setTimeout(function() {
                        screen.hidePopupQty();
                    }, 3000);
                }
            } else {
                screen.getPendingItems();
                screen.showPopupQty();
            }
        } else {
            screen.getPendingItems();
            screen.hidePopupQty();
        }
    };

    screen.getPendingItemsCallback = function(response) {
        var dt, ul, li, isAlternate, colIndex, isHeaderRow, lineitemcount = 0, totalitems = 0, cssClass='';
        try {
            isAlternate = false;
            dt = response.pendingitems;
            colIndex = response.pendingitems.ColumnIndex;
            ul = [];
            for (var i = 0; i < dt.Rows.length; i++) {
                var missingqty = dt.Rows[i][colIndex.missingqty];
                isHeaderRow = dt.Rows[i][colIndex.ispackage];
                if (!isHeaderRow) {
                    lineitemcount++;
                }
                totalitems += dt.Rows[i][colIndex.missingqty];
                li = [];
                if (!isAlternate) {
                    cssClass = 'normal';
                } else {
                    cssClass = 'alternate';
                }
                if (!isHeaderRow || (isHeaderRow && missingqty > 0)) {
                    switch(dt.Rows[i][colIndex.trackedby]) {
                        case 'QUANTITY':
                        case 'SERIALNO':
                            cssClass += ' link';
                    }
                }
                cssClass +=  ' itemclass-' + dt.Rows[i][colIndex.itemclass];
                isAlternate = !isAlternate;
                li.push('<li class="' + cssClass + '" data-masteritemid="' + dt.Rows[i][colIndex.masteritemid] + '" data-masterid="' + dt.Rows[i][colIndex.masterid] + '" data-orderid="' + dt.Rows[i][colIndex.orderid] + '" data-parentid="' + dt.Rows[i][colIndex.parentid] + '">');
                    li.push('<div class="description">' + dt.Rows[i][colIndex.description] + '</div>');
                    if (isHeaderRow && missingqty === 0) {
                        li.push('<table style="display:none;">');
                    } else {
                        li.push('<table>');
                        li.push('<tbody>');
                            li.push('<tr>');
                                li.push('<td class="col1 key masterno">' + RwLanguages.translate('I-Code') + ':</td>');
                                li.push('<td class="col2 value masterno">' + dt.Rows[i][colIndex.masterno] + '</td>');
                                if (properties.mode === 'fillcontainer') {
                                    li.push('<td class="col3 key qtyordered">' + RwLanguages.translate('Required') + ':</td>');
                                } else if (properties.mode === 'checkin') {
                                    li.push('<td class="col3 key qtyordered">' + RwLanguages.translate('Out') + ':</td>');
                                }
                                li.push('<td class="col4 value qtyordered">' + String(dt.Rows[i][colIndex.requiredqty]) + '</td>');
                            li.push('</tr>');
                            li.push('<tr>');
                                li.push('<td class="col1 key trackedby">' + RwLanguages.translate('Tracked By') + ':</td>');
                                li.push('<td class="col2 value trackedby">' + dt.Rows[i][colIndex.trackedby] + '</td>');
                                if (properties.mode === 'fillcontainer') {
                                    li.push('<td class="col3 key qtyin">' + RwLanguages.translate('In') + ':</td>');
                                } else if (properties.mode === 'checkin') {
                                    li.push('<td class="col3 key qtyin">' + RwLanguages.translate('Session In') + ':</td>');
                                }
                                li.push('<td class="col4 value qtyin">' + String(dt.Rows[i][colIndex.incontainerqty]) + '</td>');
                            li.push('</tr>');
                            li.push('<tr>');
                                li.push('<td class="col1 key"></td>');
                                li.push('<td class="col2 value"></td>');
                                if (properties.mode === 'fillcontainer') {
                                    li.push('<td class="col3 key missingqty">' + RwLanguages.translate('Remaining') + ':</td>');
                                } else if (properties.mode === 'checkin') {
                                    li.push('<td class="col3 key missingqty">' + RwLanguages.translate('Still Out') + ':</td>');
                                }
                                li.push('<td class="col4 value missingqty">' + String(dt.Rows[i][colIndex.missingqty]) + '</td>');
                            li.push('</tr>');
                        li.push('</tbody>');
                    }
                    li.push('</table>');
                li.push('</li>');
                ul.push(li.join(''));
            }
            ul.push(screen.getRowCountItem(lineitemcount, totalitems));
            jQuery('#fillcontainer-pendingitems-ul').html(ul.join(''));
        } catch(ex) {
            FwFunc.showError(ex);
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

    screen.getContainerItemsCallback = function(response) {
        var dt, ul, li, isAlternate, isHeaderRow, lineitemcount=0, totalitems=0, cssClass='';
        
        try {
            isAlternate = false;
            dt = response.containeritems;
            ul = [];
            for (var i = 0; i < dt.Rows.length; i++) {
                isHeaderRow = ((dt.Rows[i][dt.ColumnIndex.itemclass] === 'N') || (dt.Rows[i][dt.ColumnIndex.qty] === 0));
                if (!isHeaderRow) {
                    lineitemcount++;
                }
                totalitems += dt.Rows[i][dt.ColumnIndex.outqty];
                li = [];
                if (!isAlternate) {
                    cssClass = 'normal';
                } else {
                    cssClass = 'alternate';
                }
                isAlternate = !isAlternate;
                cssClass +=  ' itemclass-' + dt.Rows[i][dt.ColumnIndex.itemclass];
                li.push('<li class="' + cssClass + '"');
                li.push(' data-rentalitemid="'  + dt.Rows[i][dt.ColumnIndex.rentalitemid] + '"');
                li.push(' data-masteritemid="'  + dt.Rows[i][dt.ColumnIndex.masteritemid] + '"');
                li.push(' data-masterid="'      + dt.Rows[i][dt.ColumnIndex.masterid] + '"');
                li.push(' data-description="'   + dt.Rows[i][dt.ColumnIndex.description] + '"');
                li.push(' data-vendorid="'      + dt.Rows[i][dt.ColumnIndex.vendorid] + '"');
                li.push(' data-qty="'           + dt.Rows[i][dt.ColumnIndex.qtyordered] + '"');
                li.push(' data-trackedby="'     + dt.Rows[i][dt.ColumnIndex.trackedby] + '"');
                li.push(' data-outcontractid="' + dt.Rows[i][dt.ColumnIndex.outcontractid] + '"');
                li.push('>');
                    li.push('<div class="description">' + dt.Rows[i][dt.ColumnIndex.description] + '</div>');
                    if (isHeaderRow) {
                        li.push('<table style="display:none;">');
                    } else {
                        li.push('<table>');
                    }
                        li.push('<tbody>');
                            li.push('<tr>');
                                li.push('<td class="col1 key masterno">' + RwLanguages.translate('I-Code') + ':</td>');
                                li.push('<td class="col2 value masterno">' + dt.Rows[i][dt.ColumnIndex.masterno] + '</td>');
                                if (properties.mode === 'fillcontainer') {
                                    li.push('<td class="col3 key required">' + RwLanguages.translate('Required') + ':</td>');
                                } else if (properties.mode === 'checkin') {
                                    li.push('<td class="col3 key required">' + RwLanguages.translate('Ordered') + ':</td>');
                                }
                                li.push('<td class="col4 value required">' + dt.Rows[i][dt.ColumnIndex.qtyordered] + '</td>');
                            li.push('</tr>');
                            li.push('<tr>');
                                li.push('<td class="col1 key trackedby">' + RwLanguages.translate('Tracked By') + ':</td>');
                                li.push('<td class="col2 value trackedby">' + dt.Rows[i][dt.ColumnIndex.trackedby] + '</td>');
                                if (properties.mode === 'fillcontainer') {
                                    li.push('<td class="col3 key in">' + RwLanguages.translate('In') + ':</td>');
                                    li.push('<td class="col4 value in">' + String(dt.Rows[i][dt.ColumnIndex.outqty]) + '</td>');
                                } else if (properties.mode === 'checkin') {
                                    li.push('<td class="col3 key in">' + RwLanguages.translate('Session In') + ':</td>');
                                    li.push('<td class="col4 value in">' + String(dt.Rows[i][dt.ColumnIndex.sessionin]) + '</td>');
                                }
                            li.push('</tr>');
                            if ((dt.Rows[i][dt.ColumnIndex.barcode] !== '') || (dt.Rows[i][dt.ColumnIndex.qtyremaining] > 0)) {
                                li.push('<tr>');            
                                    if (dt.Rows[i][dt.ColumnIndex.barcode] !== '') {
                                        li.push('<td class="col1 key barcode">Code:</td>');
                                        li.push('<td class="col2 value barcode">' + dt.Rows[i][dt.ColumnIndex.barcode] + '</td>');
                                    } else {
                                        li.push('<td class="col1 key"></td>');
                                        li.push('<td class="col2 value"></td>');
                                    }
                                    if (dt.Rows[i][dt.ColumnIndex.qtyremaining] > 0) {
                                        li.push('<td class="col3 key qtyremaining">' + RwLanguages.translate('Remaining') + ':</td>');
                                        li.push('<td class="col4 value qtyremaining">' + String(dt.Rows[i][dt.ColumnIndex.qtyremaining]) + '</td>');
                                    }
                                li.push('</tr>');
                            }
                        li.push('</tbody>');
                    li.push('</table>');
                li.push('</li>');
                ul.push(li.join(''));
            }
            ul.push(screen.getRowCountItem(lineitemcount, totalitems));
            jQuery('#fillcontainer-containeritems-ul').html(ul.join(''));

            jQuery('#fillcontainer-containeritems-buttonpanel').toggle((applicationConfig.designMode) || (dt.Rows.length > 0));
        } catch(ex) {
            FwFunc.showError(ex);
        }
    };

    screen.onClickPendingItem = function() {
        var $this, requestStageItem, requestCheckInItem, requestSelectSerialNo, trackedBy;
        try {
            $this = jQuery(this);
            trackedBy = $this.find('table > tbody > tr > td.trackedby.value').html();
            switch(trackedBy) {
                case 'QUANTITY':
                    if (properties.mode === 'fillcontainer') {
                        requestStageItem = {
                            orderid:               screen.getContainerItemId(),
                            code:                  '',
                            masteritemid:          $this.attr('data-masteritemid'),
                            qty:                   0,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            spaceid:               '',
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContainerOutContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           '',
                            consignoragreementid:  '',
                            playStatus:            false
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            properties.responseStageItem = responseStageItem;
                            screen.pdastageitemCallback(responseStageItem);
                        });
                    } else if (properties.mode === 'checkin') {
                        var orderid, masteritemid, masterid, code, qty, vendorid, description, qtyordered, qtyin, missingqty, parentid;
                        orderid      = $this.attr('data-orderid');  // mv 2016-03-16 the pending list is an orderstatus on the container so orderid is containerid
                        masteritemid = $this.attr('data-masteritemid');
                        masterid     = $this.attr('data-masterid');
                        code         = $this.find('td.masterno.value').text();
                        qty          = 0;
                        vendorid     = '';
                        description  = $this.find('.description').text();
                        qtyordered   = $this.find('.qtyordered.value').text();
                        qtyin        = $this.find('.qtyin.value').text();
                        missingqty   = $this.find('.missingqty.value').text();
                        parentid     = $this.attr('data-parentid'),
                        screen.checkInItem(orderid, masteritemid, masterid, code, qty, vendorid, parentid);
                    }
                    break;
                case 'SERIALNO':
                    requestSelectSerialNo = {
                        orderId:      screen.getContainerItemId()
                        , masterId:     $this.data('masterid')
                        , masterItemId: $this.data('masteritemid')
                    };
                    RwServices.order.getSelectSerialNo(requestSelectSerialNo, function(responseSelectSerialNo) {
                        var divSerialNos, divSerialNo;
                        divSerialNos = jQuery('#fillcontainer-popupselectserialno-serialnos')
                            .empty()
                        ;
                        for(var i = 0; i < responseSelectSerialNo.getSelectSerialNo.length; i++) {
                            divSerialNo = jQuery('<div>')
                                .attr('id', 'fillcontainer-popupselectserialno-divserialno' + i.toString())
                                .attr('class', 'fillcontainer-popupselectserialno-divserialno')
                                .html(responseSelectSerialNo.getSelectSerialNo[i].mfgserial)
                                .click(function() {
                                    screen.hidePopupSelectSerialNo();
                                    jQuery('#scanBarcodeView-txtBarcodeData').val(jQuery(this).html()).change();
                                })
                            ;
                            divSerialNos.append(divSerialNo);
                        }
                        screen.showPopupSelectSerialNo();
                    });
                    break;
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    };
    screen.$view.on('click', '#fillcontainer-pendingitems-ul > li.link', screen.onClickPendingItem);

    screen.$view.on('click', '#fillcontainer-containeritems-ul > li.link', function() {
        var $this, qty, requestStageItem, $tdBarcodeValue, code, $confirmation, $btnok, confirmhtml;
        try {
            $this = jQuery(this);
            code = '';
            $tdBarcodeValue = $this.find('td.barcode.value');
            if ($tdBarcodeValue.length > 0) {
                code = $this.find('td.barcode.value').html();
            }
            confirmhtml = [];
            confirmhtml.push('<table style="font-size:10px;width:100%;border-collapse:collapse;">');
            confirmhtml.push('<tr>');
            confirmhtml.push('  <td style="font-weight:bold;">Description:</td>');
            confirmhtml.push('  <td>' + $this.attr('data-description') + '</td>');
            confirmhtml.push('</tr>');
            confirmhtml.push('<tr>');
            confirmhtml.push('  <td style="font-weight:bold;">Tracked By:</td>');
            confirmhtml.push('  <td>' + $this.find('td.trackedby.value').html() + '</td>');
            confirmhtml.push('</tr>');
            if ($this.attr('data-trackedby') === 'QUANTITY') {
                confirmhtml.push('<tr>');
            } else {
                confirmhtml.push('<tr style="display:none;">');
            }
            confirmhtml.push('  <td style="font-weight:bold;">Qty:</td>');
            confirmhtml.push('  <td><input class="txtqty" style="text-align:center;width:100px;" type="number" value="' + $this.attr('data-qty') + '" /></td>');
            confirmhtml.push('</tr>');
            if (code.length > 0) {
                confirmhtml.push('<tr>');
                confirmhtml.push('  <td style="font-weight:bold;">Code:</td>');
                confirmhtml.push('  <td>' + code + '</td>');
                confirmhtml.push('</tr>');
            }
            confirmhtml.push('</table>');
            $confirmation = FwConfirmation.renderConfirmation(RwLanguages.translate('Remove Item from Container?'),  confirmhtml.join('\n'));
            $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
            $btnok.focus();
            FwConfirmation.addButton($confirmation, 'Cancel', true);
            $btnok.on('click', function() {
                var requestRemoveItemFromContainer;
                try {
                    requestRemoveItemFromContainer = {
                        mode:            properties.mode,
                        contractid:      screen.getContainerOutContractId(),
                        outcontractid:   $this.attr('data-outcontractid'),
                        vendorid:        $this.attr('data-vendorid'),
                        masteritemid:    $this.attr('data-masteritemid'),
                        qty:             parseFloat($confirmation.find('.txtqty').val()),
                        containeritemid: screen.getContainerItemId()
                    };
                    RwServices.callMethod('FillContainer', 'RemoveItemFromContainer', requestRemoveItemFromContainer, function(responseRemoveItemFromContainer) {
                        try {
                            screen.getContainerItemsCallback(responseRemoveItemFromContainer);
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

    screen.getPendingItems = function() {
        var request;
        jQuery('#fillcontainer-containeritems').hide();
        jQuery('#fillcontainer-scan').hide();
        jQuery('#fillcontainer-rfid').hide();
        jQuery('#fillcontainer-pendingitems').show();
        jQuery('#fillcontainer-btncontaineritems').removeClass('selected').addClass('unselected');
        jQuery('#fillcontainer-btnscan').removeClass('selected').addClass('unselected');
        jQuery('#fillcontainer-btnrfid').removeClass('selected').addClass('unselected');
        jQuery('#fillcontainer-btnpendingitems').removeClass('unselected').addClass('selected');
        jQuery('#fillcontainer-scan').attr('data-mode', 'PENDING');
        RwRFID.unregisterRFIDEvents();
        if (properties.mode === 'fillcontainer') {
            request = {
                mode: properties.mode,
                containeritemid: screen.getContainerItemId()
            };
        }
        else if (properties.mode === 'checkin') {
            request = {
                mode: properties.mode, 
                contractid:      screen.getCheckInContractId(),
                containeritemid: screen.getContainerItemId(),
                dealid:          screen.getDealId(),
                departmentid:    screen.getDepartmentId(),
                orderid:         screen.getCheckInOrderId()
            };
        }
        RwServices.callMethod('FillContainer', 'GetContainerPendingItems', request, screen.getPendingItemsCallback);
    };
    
    screen.$view.on('click', '#fillcontainer-btnpendingitems', function() {
        try {
            screen.getPendingItems();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$view.on('click', '#fillcontainer-btnscan', function() {
        try {
            jQuery('#fillcontainer-containeritems').hide();
            jQuery('#fillcontainer-pendingitems').hide();
            jQuery('#fillcontainer-rfid').hide();
            jQuery('#fillcontainer-scan').show();
            jQuery('#fillcontainer-btnpendingitems').removeClass('selected').addClass('unselected');
            jQuery('#fillcontainer-btncontaineritems').removeClass('selected').addClass('unselected');
            jQuery('#fillcontainer-btnrfid').removeClass('selected').addClass('unselected');
            jQuery('#fillcontainer-btnscan').removeClass('unselected').addClass('selected');
            jQuery('#fillcontainer-scan').attr('data-mode', 'SCAN');
            RwRFID.unregisterRFIDEvents();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$view.on('click', '#fillcontainer-btncontaineritems', function() {
        var request;
        try {
            jQuery('#fillcontainer-pendingitems').hide();
            jQuery('#fillcontainer-scan').hide();
            jQuery('#fillcontainer-rfid').hide();
            jQuery('#fillcontainer-containeritems').show();
            jQuery('#fillcontainer-btnpendingitems').removeClass('selected').addClass('unselected');
            jQuery('#fillcontainer-btnscan').removeClass('selected').addClass('unselected');
            jQuery('#fillcontainer-btnrfid').removeClass('selected').addClass('unselected');
            jQuery('#fillcontainer-btncontaineritems').removeClass('unselected').addClass('selected');
            jQuery('#fillcontainer-scan').attr('data-mode', 'STAGEDLIST');
            RwRFID.unregisterRFIDEvents();
            
            if (properties.mode === 'fillcontainer') {
                request = {
                    mode: properties.mode,
                    containeritemid: screen.getContainerItemId()
                };
            } else if (properties.mode === 'checkin') {
                request = {
                    mode: properties.mode,
                    contractid: screen.getCheckInContractId(),
                    containeritemid: screen.getContainerItemId()
                };
            }
            RwServices.FillContainer.GetContainerItems(request, screen.getContainerItemsCallback);
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    //screen.$view.on('click', '#fillcontainer-containeritems-btncreatecontainer', function() {
    //    var requestCreateContainer;
    //    try {
    //        requestCreateContainer = {
    //            containeritemid: screen.getContainerItemId()
    //        };
    //        RwServices.FillContainer.CreateContainer(requestCreateContainer, function(responseCreateContainer) {
    //            try {
    //                screen.containerCreated = true;
    //                program.navigate('home/home');
    //            } catch(ex) {
    //                FwFunc.showError(ex);
    //            }
    //        });
    //    } catch(ex) {
    //        FwFunc.showError(ex);
    //    }
    //});

    screen.$view.on('click', '#fillcontainer-pendingitems-btnaddallqtyitems', function() {
        var request, $confirmation, $btnok;
        try {
            $confirmation = FwConfirmation.renderConfirmation('Confirm', RwLanguages.translate('Add All Qty Items') + '?');
            $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
            FwConfirmation.addButton($confirmation, 'Cancel', true);
            $btnok.on('click', function() {
                if (properties.mode === 'fillcontainer') {
                    request = {
                        mode:                   properties.mode,
                        contractid:             '',
                        containeroutcontractid: screen.getContainerOutContractId(),
                        containeritemid:        screen.getContainerItemId(),
                        dealid:                 screen.getDealId(),
                        departmentid:           screen.getDepartmentId()
                    };
                    RwServices.FillContainer.AddAllQtyItemsToContainer(request, screen.getPendingItemsCallback);
                } else if (properties.mode === 'checkin') {
                    request = {
                        mode:                   properties.mode,
                        orderid:                properties.orderid,
                        contractid:             screen.getCheckInContractId(),
                        containeroutcontractid: screen.getContainerOutContractId(),
                        containeritemid:        screen.getContainerItemId(),
                        dealid:                 screen.getDealId(),
                        departmentid:           screen.getDepartmentId()
                    };
                    RwServices.FillContainer.AddAllQtyItemsToContainer(request, screen.getPendingItemsCallback);
                }
            });
            $btnok.focus();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.getScannableItemBarcode = function() {
        var barcode;
        barcode = RwAppData.stripBarcode(screen.$view.find('.scannableitem .fwformfield-value').val());
        return barcode;
    };

    screen.selectContainer = function(barcode, masterno, orderdesc, containerno, containerid, containeritemid, containeroutcontractid, rentalitemid, usecontainerno) {
        var $titlerow, $subtitlerow;
        screen.orderdesc       = orderdesc;
        screen.containerid     = containerid;
        screen.containeritemid = containeritemid;
        screen.containeroutcontractid = containeroutcontractid;
        screen.rentalitemid    = rentalitemid;
        if (properties.mode === 'fillcontainer') {
            // show container info in page title and subtitle
            screen.$btncontinue.hide();
            jQuery('#fillcontainer-pageselector').show();
            $titlerow = jQuery(Mustache.render(
                '<div class="titlerow1"><span class="titlefillcontainer">{{FillContainer}}</span> <span class="titlebarcode">{{barcode}}</span></div>', {
                FillContainer: RwLanguages.translate('Fill Container'),
                barcode:       barcode
            }));
            $titlerow.append(jQuery(Mustache.render(
                '<div>\n' + 
                '  <div class="subtitlerow1"><span class="subtitleicode">{{masterno}}</span> <span class="subtitledesc" title="{{orderdesc}}">{{orderdesc}}</span></div>' + 
                '  <div class="subtitlerow2"><span class="subtitlecontainernocaption">{{captioncontainerno}}</span>: <span class="subtitlecontainerno">{{containerno}}</span></div>' +
                '</div>\n', {
                masterno:           masterno,
                orderdesc:          orderdesc,
                captioncontainerno: RwLanguages.translate('Container No'),
                containerno:        containerno
            })));
            FwMobileMasterController.setTitle($titlerow);
            $titlerow.find('.subtitlerow2').toggle(containerno.length > 0);
            screen.$view.find('.containertoolbar .btnsetcontainernorow').toggle(usecontainerno);
            screen.$view.find('.containertoolbar .btnnewcontainerrow').hide();
            //screen.$view.find('.containertoolbar .btnclosecontainerrow').hide();
            screen.$view.find('.containertoolbar').toggle(usecontainerno);
            screen.$btncreatecontainer.toggle(screen.hascreatecontainer);
        } else if (properties.mode === 'checkin') {
            // show container info in the orange box
            jQuery('#fillcontainer-pageselector').show();
            screen.$view.find('.fillcontainerheader .containerbarcode').text(barcode).show();
            screen.$view.find('.fillcontainerheader .containericode').text(masterno).show();
            screen.$view.find('.fillcontainerheader .containerdescription').text(orderdesc).show();
            screen.$view.find('.fillcontainerheader .containeritemrow').show();
            screen.$view.find('.fillcontainerheader .containernovalue').text(containerno).show();
            screen.$view.find('.fillcontainerheader .containernorow').toggle(containerno.length > 0);
            screen.$view.find('.btnsetcontainernorow').toggle(usecontainerno);
            //screen.$view.find('.btnselectcontainerrow').hide();
            screen.$btncontinue.hide();
            screen.$view.find('.btnnewcontainerrow').hide();
            //screen.$view.find('.btnclosecontainerrow').show();
            screen.$view.find('.containertoolbar').show();
            screen.$btncreatecontainer.hide();
        }
        screen.$view.find('.scannableitemrow').hide();
        screen.$view.find('.containeritemrow').show();
        program.setScanTarget('.container .containeritem .fwformfield-value');
        program.setScanTargetLpNearfield('.container .containeritem .fwformfield-value', true);
        if (!Modernizr.touch) {
            screen.$view.find('.containeritem .fwformfield-value').select();
        }
        screen.$view.find('.containerdescrow').hide();
        //screen.$view.find('.containertoolbar').show();
        screen.$view.find('.containeritem .fwformfield-value').val('');
    };

    screen.confirmMakeContainer = function() {
        var barcode, rentalitemid, $confirmation, $btnOK, $btnCancel;
        barcode = screen.getScannableItemBarcode();
        screen.$view.find('.scannableitem .fwformfield-value').prop('disabled', true);
        $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Item ' + barcode + ' is not yet established as a container.  Do you want to make it a container?');
        $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
        $btnOK.on('click', function() {
            var request;
            try {
                request = {
                    mode:         properties.mode,
                    barcode:      barcode,
                    containerid:  FwFormField.getValue(screen.$view, '.containerdesc')
                };
                RwServices.callMethod('FillContainer', 'InstantiateContainer', request, function(response) {
                    var masterno, description, containerno, containerid, containeritemid, outcontractid, rentalitemid, usecontainerno, serviceerrormessage;
                    try {
                        if (response.serviceerrormessage.length > 0) {
                            FwFunc.showError(response.serviceerrormessage);
                        } else {
                            masterno        = response.selectcontainer.container.masterno;
                            description     = response.selectcontainer.container.description;
                            containerno     = response.selectcontainer.container.containerno;
                            containerid     = response.selectcontainer.container.containerid;
                            containeritemid = response.selectcontainer.container.containeritemid;
                            outcontractid   = response.selectcontainer.container.outcontractid;
                            rentalitemid    = response.selectcontainer.container.rentalitemid;
                            usecontainerno  = false;
                            screen.selectContainer(barcode, masterno, description, containerno, containerid, containeritemid, outcontractid, rentalitemid, usecontainerno);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
        $btnCancel = FwConfirmation.addButton($confirmation, 'Cancel', false);
        $btnCancel.on('click', function() {
            try {
                screen.$view.find('.scannableitem .fwformfield-value').val('').prop('disabled', false);
                screen.$view.find('.containerdesc').hide();
                screen.$view.find('.containertoolbar').hide();
                screen.$btncontinue.hide();
                FwConfirmation.destroyConfirmation($confirmation);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };
    
    screen.$view.on('change', '.scannableitem .fwformfield-value', function() {
        var request, barcode;
        try {
            barcode = screen.getScannableItemBarcode();
            if (barcode !== '') {
                request = {
                    mode: properties.mode,
                    barcode: barcode
                };
                if (properties.mode === 'checkin') {
                    request.checkin = {
                        moduleType:     properties.moduleType,
                        checkInMode:    properties.checkInMode,
                        code:           barcode,
                        neworderaction: '',
                        aisle:          screen.getAisle(),
                        shelf:          screen.getShelf(),
                        vendorid:       '',
                        contractid:     screen.getCheckInContractId(),
                        orderid:        properties.orderid,
                        dealid:         screen.getDealId(),
                        departmentid:   screen.getDepartmentId()
                    };
                }
                RwServices.callMethod('FillContainer', 'SelectContainer', request, function(response) {
                    var masterno, description, containerno, containerid, containeritemid, outcontractid, rentalitemid, usecontainerno, containerdescoptions=[], serviceerrormessage;
                    try {
                        // Case 1: An error occured, show the message
                        if (response.serviceerrormessage.length > 0) {
                            FwFunc.showError(response.serviceerrormessage);
                        }
                        else if (response.selectcontainer.errormessage.length > 0) {
                            FwFunc.showError(response.selectcontainer.errormessage);
                        }
                        // Case 2: This is the final case, where the container is selected
                        else if (typeof response.selectcontainer.container === 'object') {
                            masterno        = response.selectcontainer.container.masterno;
                            description     = response.selectcontainer.container.description;
                            containerno     = response.selectcontainer.container.containerno;
                            containerid     = response.selectcontainer.container.containerid;
                            containeritemid = response.selectcontainer.container.containeritemid;
                            outcontractid   = response.selectcontainer.container.outcontractid;
                            rentalitemid    = response.selectcontainer.container.containerrentalitemid;
                            usecontainerno  = response.selectcontainer.container.usecontainerno;
                            screen.selectContainer(barcode, masterno, description, containerno, containerid, containeritemid, outcontractid, rentalitemid, usecontainerno);
                        }
                        // Case 3: User is doing a Check-In: so need to prompt them to select a container type
                        else if (properties.mode === 'checkin') {
                            screen.$btncontinue.show();
                            jQuery('.scannableitem .fwformfield-value').prop('disabled', true);
                            FwFormField.setValue(screen.$view, '.scannablemasterid', response.selectcontainer.scannablemasterid);
                            FwFormField.setValue(screen.$view, '.containerdesc', response.selectcontainer.defaultcontainerdesc.containerid, response.selectcontainer.defaultcontainerdesc.master);
                            screen.$view.find('.containerdescrow').show();
                            program.setScanTarget('');
                            program.setScanTargetLpNearfield('', true);
                        }
                        // Case 4: User is doing a Fill Container: so need to prompt them to select a container type
                        else if (properties.mode === 'fillcontainer') {
                            screen.$btncontinue.show();
                            jQuery('.scannableitem .fwformfield-value').prop('disabled', true);
                            screen.$view.find('.btnsetcontainernorow').hide();
                            screen.$view.find('.btnnewcontainerrow').hide();
                            screen.$view.find('.containertoolbar').hide();
                            program.setScanTarget('');
                            program.setScanTargetLpNearfield('', true);
                            if (typeof response.selectcontainer.selectcontainers === 'object') {
                                // There are no container definitions
                                if (response.selectcontainer.selectcontainers.length === 0) {
                                    FwFunc.showError('Barcode is not the Scannable Item of a Container.');
                                } 
                                // There is only 1 container definition, so prompt user to make container
                                else if (response.selectcontainer.selectcontainers.length === 1) {
                                    FwFormField.setValue(screen.$view, '.containerdesc', response.selectcontainer.selectcontainers[0].containerid, response.selectcontainer.selectcontainers[0].description);
                                    screen.confirmMakeContainer();
                                } 
                                // User needs to pick a container definition, so show the container type field
                                else if (response.selectcontainer.selectcontainers.length > 1) {
                                    FwFormField.setValue(screen.$view, '.scannablemasterid', response.selectcontainer.scannablemasterid);
                                    screen.$view.find('.containerdescrow').show();
                                }
                            }
                        }
                        else {
                            FwFunc.showError('Barcode is not the Scannable Item of a Container.');
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    //screen.$view.on('keydown', '.scannableitem .fwformfield-value', function(event) {
    //    var $this;
    //    try {
    //        $this = jQuery(this);
    //        if (event.which === 13) {
    //            $this.change();
    //        }
    //    } catch(ex) {
    //        FwFunc.showError(ex);
    //    }
    //});

    //screen.$view.on('change', '.containerdesc .fwformfield-value', function() {
    //    if (properties.mode === 'fillcontainer') {
    //        if (this.value !== '') {
    //            screen.$view.find('.containertoolbar').show();
    //            //screen.$view.find('.btnselectcontainerrow').show();
    //            $btncontinue.show();
    //        } else {
    //            screen.$view.find('.containertoolbar').hide();
    //            //screen.$view.find('.btnselectcontainerrow').hide();
    //            $btncontinue.hide();
    //        }
    //    }
    //});

    screen.$view.on('change', '.containeritem .fwformfield-value', function() {
        var requestStageItem, code;
        try {
            code = RwAppData.stripBarcode(jQuery(this).val().toString().toUpperCase());
            if (code.length > 0) {
                if (properties.mode === 'fillcontainer') {
                    requestStageItem = {
                        orderid:               screen.getContainerItemId(),
                        code:                  code,
                        masteritemid:          '',
                        qty:                   0,
                        additemtoorder:        false,
                        addcompletetoorder:    false,
                        releasefromrepair:     false,
                        unstage:               false,
                        vendorid:              '',
                        meter:                 0,
                        location:              '',
                        spaceid:               '',
                        addcontainertoorder:   false,
                        overridereservation:   false,
                        stageconsigned:        false,
                        transferrepair:        false,
                        removefromcontainer:   false,
                        contractid:            screen.getContainerOutContractId(),
                        ignoresuspendedin:     false,
                        consignorid:           '',
                        consignoragreementid:  '',
                        playStatus:            true
                    };
                    RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                        properties.responseStageItem = responseStageItem;
                        screen.pdastageitemCallback(responseStageItem);
                    });
                }
                else if (properties.mode === 'checkin') {
                    var orderid, masteritemid, masterid, qty, vendorid, parentid;
                    orderid                = properties.orderid;
                    masteritemid           = '';
                    masterid               = '';
                    code                   = RwAppData.stripBarcode(code.toUpperCase());
                    qty                    = 0;
                    vendorid               = '';
                    parentid               = ''
                    screen.checkInItem(orderid, masteritemid, masterid, code, qty, vendorid, parentid);
                }
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$view.on('keydown', '.containeritem .fwformfield-value', function(event) {
        var $this;
        try {
            $this = jQuery(this);
            if (event.which === 13) {
                $this.change();
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    //screen.$view.on('click', '.btnselectcontainer', function() {
    //    var request, $this, barcode;
    //    try {
    //        if (properties.mode === 'fillcontainer') {
    //            screen.confirmMakeContainer();
    //        } else if (properties.mode === 'checkin') {
    //            $this = jQuery(this);
    //            barcode                = screen.getScannableItemBarcode();
    //            request = {
    //                mode: properties.mode,
    //                barcode: screen.getScannableItemBarcode()
    //            };
    //            request.checkin = {
    //                moduleType:     properties.moduleType,
    //                checkInMode:    properties.checkInMode,
    //                code:           barcode,
    //                neworderaction: '',
    //                aisle:          screen.getAisle(),
    //                shelf:          screen.getShelf(),
    //                vendorid:       '',
    //                contractid:     screen.getCheckInContractId(),
    //                orderid:        properties.orderid,
    //                dealid:         screen.getDealId(),
    //                departmentid:   screen.getDepartmentId(),
    //                containerid:    FwFormField.getValue(screen.$view, '.containerdesc'),
    //                containerdesc:  FwFormField.getText(screen.$view, '.containerdesc')
    //            };
    //            RwServices.callMethod('FillContainer', 'SelectContainer', request, function(response) {
    //                var masterno, description, containerno, orderid, containeritemid, containeroutcontractid, rentalitemid, usecontainerno, serviceerrormessage;
    //                try {
    //                    if (response.serviceerrormessage.length > 0) {
    //                        FwFunc.showError(response.serviceerrormessage);
    //                    } else {
    //                        masterno               = response.selectcontainer.container.masterno;
    //                        description            = response.selectcontainer.container.description;
    //                        containerno            = response.selectcontainer.container.containerno;
    //                        orderid                = response.selectcontainer.container.orderid;
    //                        containeritemid        = response.selectcontainer.container.containeritemid
    //                        containeroutcontractid = response.selectcontainer.container.outcontractid;
    //                        rentalitemid           = response.selectcontainer.container.containerrentalitemid;
    //                        usecontainerno         = false;
    //                        screen.selectContainer(barcode, masterno, description, containerno, orderid, containeritemid, containeroutcontractid, rentalitemid, usecontainerno);
    //                    }
    //                } catch(ex) {
    //                    FwFunc.showError(ex);
    //                }
    //            });
    //        }
    //    } catch(ex) {
    //        FwFunc.showError(ex);
    //    }
    //});
    
    screen.$view.on('click', '.btnnewcontainer', function() {
        var $confirmation, $btnok;
        try {
            $confirmation = FwConfirmation.renderConfirmation(RwLanguages.translate('Confirm'), RwLanguages.translate('Are you finished filling this Container?'));
            $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
            FwConfirmation.addButton($confirmation, 'Cancel', true);
            $btnok.on('click', function() {
                var requestCloseContainer;
                try {
                    requestCloseContainer = {
                        contractid: screen.getContainerOutContractId()
                    };
                    RwServices.callMethod('FillContainer', 'CloseContainer', requestCloseContainer, function(responseCloseContainer) {
                        try {
                            screen.resetVariables();
                            screen.$view.find('.scannableitem .fwformfield-value').val('');
                            screen.$view.find('.scannableitem').show();
                            screen.$view.find('.containeritem .fwformfield-value').val('');
                            screen.$view.find('.containeritemrow').hide();
                            screen.$view.find('.containerdesc select').empty();
                            screen.$view.find('.containerdescrow').hide();
                            jQuery('#fillcontainer-pageselector').hide();
                            if (properties.mode === 'fillcontainer') {
                                screen.$view.find('.containertoolbar').hide();
                                //jQuery('#masterLoggedInView-captionPageTitle').empty().text(RwLanguages.translate('Fill Container'));
                                //jQuery('#masterLoggedInView-captionPageSubTitle').empty();
                                FwMobileMasterController.setTitle(RwLanguages.translate('Fill Container'));
                            } else if (properties.mode === 'checkin') {
                                // show container info in the orange box
                                screen.$view.find('.containertoolbar .btnsetcontainernorow').hide();
                                screen.$view.find('.containertoolbar .btnnewcontainerrow').hide();
                                //screen.$view.find('.containertoolbar .btnclosecontainerrow').show();
                                screen.$view.find('.containerbarcode').text('').hide();
                                screen.$view.find('.containericode').text('').hide();
                                screen.$view.find('.containerdesc').text('').hide();
                                screen.$view.find('.fillcontainerheader .containeritem').hide();
                                screen.$view.find('.containernovalue').text('').hide;
                                screen.$view.find('.fillcontainerheader .containernorow').hide();
                            }
                            program.setScanTarget('.container .scannableitem .fwformfield-value');
                            program.setScanTargetLpNearfield('.container .scannableitem .fwformfield-value', true);
                            if (!Modernizr.touch) {
                                screen.$view.find('.scannableitem .fwformfield-value').select();
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

    //screen.$view.on('click', '.btnclosecontainer', function() {
    //    try {
    //        if (screen.$view.find('.fillcontainerheader .containeritem').is(':visible')) {
    //            $confirmation = FwConfirmation.renderConfirmation(RwLanguages.translate('Confirm'), RwLanguages.translate('Are you finished filling this Container?'));
    //            $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
    //            FwConfirmation.addButton($confirmation, 'Cancel', true);
    //            $btnok.on('click', function() {
    //                screen.closeFillContainer();
    //            });
    //        } else {
    //            screen.closeFillContainer();
    //        }
    //    } catch(ex) {
    //        FwFunc.showError(ex);
    //    }
    //});

    screen.closeFillContainer = function() {
        var requestCloseContainer;
        try {
            if ((typeof screen.getContainerOutContractId() === 'string') && (screen.getContainerOutContractId().length > 0)) {
                requestCloseContainer = {
                    contractid: screen.getContainerOutContractId()
                };
                RwServices.callMethod('FillContainer', 'CloseContainer', requestCloseContainer, function(responseCloseContainer) {
                    try {
                        program.popScreen();
                        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
                        program.setScanTargetLpNearfield('');
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else {
                program.popScreen();
                program.setScanTarget('#scanBarcodeView-txtBarcodeData');
                program.setScanTargetLpNearfield('');
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    };

    screen.$view.on('click', '.btnsetcontainerno', function() {
        var html=[], $confirmation, $btnok, containerno;
        try {
            if (properties.mode === 'fillcontainer') {
                containerno = screen.$view.find('.subtitlecontainerno').text();
            } else if (properties.mode === 'checkin') {
                containerno = screen.$view.find('.containernovalue').text();
            }
            html.push('<div>');
            html.push('<label>Container No: </label><input class="txtcontainerno" type="text" value="' + FwFunc.htmlEscape(containerno) + '" />');
            html.push('</div>');
            $confirmation = FwConfirmation.renderConfirmation(RwLanguages.translate('Set Container No'), html.join('\n'));
            if (!Modernizr.touch) {
                $confirmation.find('.txtcontainerno').select();
            }
            $btnok = FwConfirmation.addButton($confirmation, RwLanguages.translate('OK'), false);
            FwConfirmation.addButton($confirmation, RwLanguages.translate('Cancel'), true);
            $btnok.on('click', function() {
                var requestSetContainerNo;
                try {
                    requestSetContainerNo = {
                        rentalitemid: screen.getRentalitemid(),
                        containerno:  $confirmation.find('.txtcontainerno').val()
                    };
                    RwServices.callMethod('FillContainer', 'SetContainerNo', requestSetContainerNo, function(response) {
                        try {
                            if (properties.mode === 'fillcontainer') {
                                screen.$view.find('.subtitlecontainerno').text(response.containerno);
                                screen.$view.find('.subtitlerow2').toggle(response.containerno.length > 0);
                            } else if (properties.mode === 'checkin') {
                                screen.$view.find('.containernovalue').text(response.containerno);
                                screen.$view.find('.containernorow').toggle(response.containerno.length > 0);
                            }
                            FwConfirmation.destroyConfirmation($confirmation);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.renderPopupQty = function() {
        var model = {
            captionQty:                RwLanguages.translate('Qty'),
            valueTxtQty:               '',
            captionAdd:                RwLanguages.translate('Add'),
            captionRemove:             RwLanguages.translate('Remove'),
            captionCancel:             RwLanguages.translate('Cancel'),
            captionUnstageMode:        RwLanguages.translate('Mode'),
            captionResponsiblePerson:  RwLanguages.translate('Responsible Person'),
            captionSummary:            RwLanguages.translate('Summary'),
            captionOrdered:            RwLanguages.translate('Ordered'),
            captionICodeDesc:          RwLanguages.translate('I-Code'),
            captionSub:                RwLanguages.translate('Sub'),
            captionOut:                RwLanguages.translate('Out'),
            captionIn:                 RwLanguages.translate('In'),
            captionStaged:             RwLanguages.translate('Staged'),
            captionRemaining:          RwLanguages.translate('Remaining'),
            captionAddToOrder:         RwLanguages.translate('Would you like to add this to the order?'),
            captionAddComplete:        RwLanguages.translate('Add Complete'),
            captionAddItem:            RwLanguages.translate('Add Item'),
            captionYes:                RwLanguages.translate('Yes'),
            captionDesc:               RwLanguages.translate('Description'),
            captionPendingListButton:  RwLanguages.translate('Pending'),
            captionScanButton:         RwLanguages.translate('Scan'),
            captionRFIDButton:         RwLanguages.translate('RFID'),
            captionStagedListButton:   RwLanguages.translate('Container'),
            captionCreateContract:     RwLanguages.translate('Create Contract'),
            captionSerialNoSelection:  RwLanguages.translate('Serial No. Selection'),
            captionAddAllQtyItems:     RwLanguages.translate('Add All Qty Items'),
            captionFillContainer:      RwLanguages.translate('Fill Container'),
            captionScannableBrItem:    RwLanguages.translate('Scannable<br/>Item'),
            captionContainerBrItem:    RwLanguages.translate('Container<br/>Item'),
            captionContainerBrDesc:    RwLanguages.translate('Container<br/>Desc'),
            captionContainerBrNo:      RwLanguages.translate('Container<br/>No'),
            captionContainerNo:        RwLanguages.translate('Container No'),
            captionNewContainer:       RwLanguages.translate('New'),
            captionCloseContainer:     RwLanguages.translate('Close'),
            captionRequired:           RwLanguages.translate('Required'),
            captionSetContainerNo:     RwLanguages.translate('Set Container No'),
            captionCreateContainer:    RwLanguages.translate('Create Container')
        };

        if (properties.mode === 'checkin') {
            model.captionRequired  = RwLanguages.translate('Out');
            model.captionIn        = RwLanguages.translate('Session In');
            model.captionRemaining = RwLanguages.translate('Still Out');
        }
        
        var template = Mustache.render(jQuery('#tmpl-FillContainer-PopupQty').html(), model);
        var $popupcontent = jQuery(template);
        if (typeof screen.$popupQty === 'object' && screen.$popupQty.length > 0) {
            FwPopup.destroyPopup(screen.$popupQty);
        }
        screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
        FwPopup.showPopup(screen.$popupQty);
        screen.$popupQty
            .on('click', '#fillcontainer-btnsubtract', function() {
                var quantity;
                quantity = Number(jQuery('#fillcontainer-popupqty-qty-txtqty').val()) - 1;
                if (quantity > 0) {
                    jQuery('#fillcontainer-popupqty-qty-txtqty').val(quantity);
                }
            })
            .on('click', '#fillcontainer-btnadd', function() {
                var quantity, remaining;
                remaining = Number(jQuery('#fillcontainer-popupqty-qtyremaining').html());
                quantity = Number(jQuery('#fillcontainer-popupqty-qty-txtqty').val()) + 1;
                if (quantity <= remaining) {
                    jQuery('#fillcontainer-popupqty-qty-txtqty').val(quantity);
                }
            })
            .on('click', '#fillcontainer-popupqty-btnunstagebarcode', function() {
                var qty, requestStageItem;
                try {
                    requestStageItem = {
                        orderid:               screen.getContainerItemId(),
                        code:                  RwAppData.stripBarcode(jQuery('#fillcontainer-popupqty').data('code')),
                        masteritemid:          jQuery('#fillcontainer-popupqty').data('masteritemid'),
                        qty:                   0,
                        additemtoorder:        false,
                        addcompletetoorder:    false,
                        releasefromrepair:     false,
                        unstage:               true,
                        vendorid:              '',
                        meter:                 0,
                        location:              '',
                        spaceid:               '',
                        addcontainertoorder:   false,
                        overridereservation:   false,
                        stageconsigned:        false,
                        transferrepair:        false,
                        removefromcontainer:   false,
                        contractid:            screen.getContainerOutContractId(),
                        ignoresuspendedin:     false,
                        consignorid:           '',
                        consignoragreementid:  '',
                        playStatus:            false,
                        trackedBy:             'BARCODE'
                    };
                    RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                        try {
                            screen.pdastageitemCallback(responseStageItem);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#fillcontainer-popupqty-qty-btncancel', function() {
                try {
                    FwPopup.destroyPopup(screen.$popupQty);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#fillcontainer-popupqty-qty-btnaddtocontainer', function() {
                var qty, requestStageItem, requestCheckinItem;
                try {
                    qty = parseFloat(jQuery('#fillcontainer-popupqty-qty-txtqty').val().toString());
                    if (isNaN(qty)) {
                        FwFunc.showError('Qty is required.');
                    } else {
                        if (properties.mode === 'fillcontainer') {
                            requestStageItem = {
                                orderid:               screen.getContainerItemId(),
                                code:                  RwAppData.stripBarcode(jQuery('#fillcontainer-popupqty').data('code')),
                                masteritemid:          jQuery('#fillcontainer-popupqty').data('masteritemid'),
                                qty:                   qty,
                                additemtoorder:        false,
                                addcompletetoorder:    false,
                                releasefromrepair:     false,
                                unstage:               false,
                                vendorid:              '',
                                meter:                 0,
                                location:              '',
                                spaceid:               '',
                                addcontainertoorder:   false,
                                overridereservation:   false,
                                stageconsigned:        false,
                                transferrepair:        false,
                                removefromcontainer:   false,
                                contractid:            screen.getContainerOutContractId(),
                                ignoresuspendedin:     false,
                                consignorid:           '',
                                consignoragreementid:  '',
                                playStatus:            false,
                                trackedBy:             'QUANTITY'
                            };
                            RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                                try {
                                    screen.pdastageitemCallback(responseStageItem);
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else if (properties.mode === 'checkin') {
                            var orderid      = jQuery('#fillcontainer-popupqty').data('orderid');
                            var masteritemid = jQuery('#fillcontainer-popupqty').data('masteritemid');
                            var masterid     = jQuery('#fillcontainer-popupqty').data('masterid');
                            var code         = jQuery('#fillcontainer-popupqty').data('masterno');
                            var vendorid     = jQuery('#fillcontainer-popupqty').data('vendorid');
                            var parentid     = jQuery('#fillcontainer-popupqty').data('parentid');
                            screen.checkInItem(orderid, masteritemid, masterid, code, qty, vendorid, parentid);
                        }
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#fillcontainer-popupqty-qty-btnunstageqtyitem', function() {
                var qty, requestStageItem;
                try {
                    qty = parseFloat(jQuery('#fillcontainer-popupqty-qty-txtqty').val().toString());
                    if (isNaN(qty)) {
                        FwFunc.showError('Qty is required.');
                    } else {
                        requestStageItem = {
                            orderid:               screen.getContainerItemId(),
                            code:                  RwAppData.stripBarcode(jQuery('#fillcontainer-popupqty').data('code')),
                            masteritemid:          jQuery('#fillcontainer-popupqty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               true,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            spaceid:               '',
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContainerOutContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           '',
                            consignoragreementid:  '',
                            playStatus:            false,
                            trackedBy:             'QUANTITY'
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#fillcontainer-popupqty-btnaddcomplete', function() {
                var qty, requestStageItem;
                try {
                    qty = parseFloat(jQuery('#fillcontainer-popupqty-qty-txtqty').val().toString());
                    if (isNaN(qty)) {
                        qty = 0;
                    }
                    requestStageItem = {
                        orderId:            screen.getContainerItemId()
                      , code:               RwAppData.stripBarcode(jQuery('#fillcontainer-popupqty').data('code'))
                      , masterItemId:       jQuery('#fillcontainer-popupqty').data('masteritemid')
                      , qty:                qty
                      , addItemToOrder:     false
                      , addCompleteToOrder: true
                      , unstageMode:        false
                    };
                    RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                        try {
                            screen.pdastageitemCallback(responseStageItem);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#fillcontainer-popupqty-btnadditem', function() {
                var qty, requestStageItem;
                try {
                    qty = parseFloat(jQuery('#fillcontainer-popupqty-qty-txtqty').val().toString());
                    if (isNaN(qty)) {
                        qty = 0;
                    }
                    requestStageItem = {
                        orderid:               screen.getContainerItemId(),
                        code:                  RwAppData.stripBarcode(jQuery('#fillcontainer-popupqty').data('code')),
                        masteritemid:          jQuery('#fillcontainer-popupqty').data('masteritemid'),
                        qty:                   qty,
                        additemtoorder:        true,
                        addcompletetoorder:    false,
                        releasefromrepair:     false,
                        unstage:               false,
                        vendorid:              '',
                        meter:                 0,
                        location:              '',
                        spaceid:               '',
                        addcontainertoorder:   false,
                        overridereservation:   false,
                        stageconsigned:        false,
                        transferrepair:        false,
                        removefromcontainer:   false,
                        contractid:            screen.getContainerOutContractId(),
                        ignoresuspendedin:     false,
                        consignorid:           '',
                        consignoragreementid:  ''
                    };
                    RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                        try {
                            screen.pdastageitemCallback(responseStageItem);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    };

    screen.renderPopupSelectSerialNo = function() {
        var template = Mustache.render(jQuery('#tmpl-FillContainer-PopupSelectSerialNo').html(), {
            captionSerialNoSelection:  RwLanguages.translate('Serial No. Selection')
        });
        var $popupcontent = jQuery(template);
        screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
        FwPopup.showPopup(screen.$popupQty);
    };

    jQuery('.navHome')
        .off('click')
        .one('click', function() {
            var requestCloseContainer;
            if ((typeof screen.getContainerOutContractId() === 'string') && (screen.getContainerOutContractId().length > 0)) {
                requestCloseContainer = {
                    contractid: screen.getContainerOutContractId()
                };
                RwServices.FillContainer.CloseContainer(requestCloseContainer, function(responseCloseContainer) {});
            }
            program.navigate('home/home');
        })
    ;

    screen.load = function() {
        screen.containerCreated = false;
        //jQuery('#masterLoggedInView-btnHome img').attr('src', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/mainmenu-white.001.png');
        //jQuery('#masterLoggedInView-btnHome').on('click', function() {
            
        //});
        if (properties.mode === 'fillcontainer') {
            //jQuery('#scanBarcodeView').hide();
            screen.$view.find('#fillcontainer-pageselector').hide();
            screen.$view.find('.fillcontainerheader').hide();
            screen.$view.find('.fillcontainerfields').show();
            screen.$view.find('.scannableitemrow').show();
            screen.$view.find('.containeritemrow').hide();
            screen.$view.find('.containerdescrow').hide();
            screen.$view.find('.containertoolbar').hide();
            //screen.$view.find('.btnselectcontainerrow').hide();
            //screen.$view.find('.btnclosecontainerrow').hide();
            screen.$view.find('.containeritem .fwformfield-value').val('');
            program.setScanTarget('.container .scannableitem .fwformfield-value');
            program.setScanTargetLpNearfield('.container .scannableitem .fwformfield-value', true);
            if (!Modernizr.touch) {
                jQuery('.container .scannableitem .fwformfield-value').select();
            }
        }
        else if (properties.mode === 'checkin') {
            screen.$btnback.show();
            //jQuery('#masterLoggedInView-captionPageTitle').html(properties.pagetitle);
            //jQuery('#masterLoggedInView-captionPageSubTitle').html(properties.pagesubtitle);
            FwMobileMasterController.setModuleCaption(properties.modulecaption);
            FwMobileMasterController.setTitle(properties.pagetitle);
            screen.$view.find('#fillcontainer-pageselector').hide();
            screen.$view.find('.fillcontainerheader').show();
            screen.$view.find('.containernorow').hide();
            screen.$view.find('.fillcontainerfields').show();
            screen.$view.find('.scannableitemrow').show();
            screen.$view.find('.containeritemrow').hide();
            screen.$view.find('.containerdescrow').hide();
            screen.$view.find('.containertoolbar').show();
            screen.$view.find('.btnsetcontainernorow').hide();
            screen.$view.find('.btnnewcontainerrow').hide();
            //screen.$view.find('.btnclosecontainerrow').show();
            screen.$btncreatecontainer.hide();
            program.setScanTarget('.container .scannableitem .fwformfield-value');
            program.setScanTargetLpNearfield('.container .scannableitem .fwformfield-value', true);
            if (!Modernizr.touch) {
                jQuery('.container .scannableitem .fwformfield-value').select();
            }
        }

        screen.toggleRfid();
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_fillcontainer2_getFillContainerScreen', function() {
                RwRFID.isConnected = true;
                screen.toggleRfid();
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceConnected_fillcontainer2_getFillContainerScreen', function() {
                RwRFID.isConnected = false;
                screen.toggleRfid();
            });
        }
    };

    screen.unload = function() {
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_fillcontainer2_getFillContainerScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_fillcontainer2_getFillContainerScreen');
        }
        if ((!screen.containerCreated) && (screen.getContainerOutContractId().length > 0)) {
            var requestCancelContract;
            requestCancelContract = {
                contractId:                    screen.getContainerOutContractId(),
                activityType:                  'Staging',
                dontCancelIfOrderTranExists:   true,
                failSilentlyOnOwnershipErrors: true
            };
            RwServices.order.cancelContract(requestCancelContract, function(responseCancelContract) {});
        }
    };

    return screen;
};
//----------------------------------------------------------------------------------------------