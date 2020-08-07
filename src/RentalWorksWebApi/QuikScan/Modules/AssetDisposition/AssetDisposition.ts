//----------------------------------------------------------------------------------------------
RwInventoryController.getAssetDispositionScreen = function(viewModel, properties) {
    var combinedViewModel, screen, applicationOptions;
    applicationOptions = program.getApplicationOptions();
    combinedViewModel = jQuery.extend({
      captionPageTitle:           RwLanguages.translate('Asset Disposition'),
      htmlScanBarcode:            RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Bar Code / I-Code')}),
      captionBC:                  RwLanguages.translate('BC'),
      
      // screenItemStatus
      captionCheckInDate:         RwLanguages.translate('Check-In Date'),
      captionSetCharacterName:    RwLanguages.translate('Set Character Name'),
      captionSetCharacter:        RwLanguages.translate('Set Character'),
      captionOnOrder:             RwLanguages.translate('On Order'),
      captionSetLocation:         RwLanguages.translate('Set Location'),
      captionSetReceiveDate:      RwLanguages.translate('Set Receive Date'),
      captionAsOf:                RwLanguages.translate('As Of'),
      captionICode:               RwLanguages.translate('I-Code'),
      captionLastDeal:            RwLanguages.translate('Last Deal'),
      captionLastOrder:           RwLanguages.translate('Last Order'),
      captionDepartment:          RwLanguages.translate('Department'),
      captionProduction:          RwLanguages.translate('Production'),
      captionDescription:         RwLanguages.translate('Description'),
      captionAisle:               RwLanguages.translate('Aisle'),
      captionShelf:               RwLanguages.translate('Shelf'),
      captionWarehouse:           RwLanguages.translate('Warehouse'),
      captionTotal:               RwLanguages.translate('Total'),
      captionIn:                  RwLanguages.translate('In'),
      captionQCRqd:               RwLanguages.translate('QC Rq\'d'),
      captionStaged:              RwLanguages.translate('Staged'),
      captionOut:                 RwLanguages.translate('Out'),
      captionInRepair:            RwLanguages.translate('In Repair'),
      captionOnPO:                RwLanguages.translate('On PO'),
      captionVendor:              RwLanguages.translate('Vendor'),
      captionPrimaryVendor:       RwLanguages.translate('Primary Vendor'),
      captionManufacturer:        RwLanguages.translate('Manufacturer'),
      captionPONo:                RwLanguages.translate('PO No.'),
      captionScanButton:          RwLanguages.translate('Scan'),
      captionRFIDButton:          RwLanguages.translate('RFID'),
      captionContinue:            RwLanguages.translate('Continue'),
      captionRetireFrom:          RwLanguages.translate('Retire Items'),
      captionRetireItemsOnOrder:  RwLanguages.translate('on Set'),
      captionRetireInWarehouse:   RwLanguages.translate('in Warehouse'),

      // screenSelectOrder
      captionMatchingOrders:      RwLanguages.translate('Matching Orders'),
      
      // screenRetire
      captionOrderNo:             RwLanguages.translate('Order No'),
      captionRetireQty:           RwLanguages.translate('Retire Quantity'),
      captionRetireReason:        RwLanguages.translate('Retire Reason'),
      captionTrackLossAmount:     RwLanguages.translate('Track Loss Amount'),
      captionItemValue:           RwLanguages.translate('ITEM VALUE'),
      captionCustom:              RwLanguages.translate('CUSTOM'),
      captionNotes:               RwLanguages.translate('Notes'),
      captionRetire:              RwLanguages.translate('Retire')

    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assetdisposition').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    
    var $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    screen.$btnback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function() { //back
        try {
            if (screen.$view.find('.screenItemStatus').is(':visible')) {
                screen.showScanBarcodeScreen(false);
            }else if (screen.$view.find('.screenRetire').is(':visible')) {
                screen.showItemStatusScreen(false);
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btncontinue = FwMobileMasterController.addFormControl(screen, 'Continue', 'right', '&#xE5CC;', false, function() { //continue
        var request, barcode, containerid;
        try {
            screen.showRetireScreen();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btnretire = FwMobileMasterController.addFormControl(screen, 'Retire', 'right', '&#xE161;', false, function() { //save
        var request, barcode, containerid;
        try {
            screen.retireItem();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
    
    screen.$view.find('.txtRetireQty').inputmask("numeric", {
        autoUnmask:     true,
        min:            0,
        max:            999999999.999,
        digits:         3,
        radixPoint:     '.',
        groupSeparator: ',',
        autoGroup:      true,
        rightAlign:     false
    });
    screen.$view.find('.txtLossAmount').inputmask("currency", {
        autoUnmask:     true,
        min:            0,
        max:            999999999.99,
        digits:         2,
        radixPoint:     '.',
        groupSeparator: ',',
        autoGroup:      true,
        rightAlign:     false
    });
    screen.$view.find('.fieldsetcharacter .fwformfield-value').on('change', function() {
        try {
            screen.setOrderId(this.value);
            if (this.value.length > 0) {
                var request = {
                    masterid: screen.getMasterId(),
                    orderid: this.value
                };
                RwServices.callMethod('AssetDisposition', 'GetMaxRetireQty', request, function(response) {
                    try {
                        FwFormField.enable(screen.$view.find('.fieldretireqty'));
                        if (response.maxretireqty > 0) {
                            screen.setRetireQty(1);
                        } else {
                            screen.setRetireQty(0);
                        }
                        screen.setMaxRetireQty(response.maxretireqty);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else {
                FwFormField.disable(screen.$view.find('.fieldretireqty'));
                screen.setRetireQty(0);
                screen.setMaxRetireQty(0);
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
    //----------------------------------------------------------------------------------------------------
    screen.clearProperties = function() {
        screen.properties = {
            barcode: '',
            webGetItemStatus: null,
            orderid: '',
            isRentalItemOut: false,
            isRentalItemIn: false,
            isIcode: false,
            isRetireOnOrder: false,
            isRetireOnSet: false
        };
    };
    //----------------------------------------------------------------------------------------------------
    // simple getter/setter type functions
    screen.setWebGetItemStatus     = function(value) { screen.properties.webGetItemStatus = value; };
    screen.getWebGetItemStatus     = function() { return screen.properties.webGetItemStatus; };
    screen.setFuncMasterWh         = function(value) { screen.properties.funcMasterWh = value; };
    screen.getFuncMasterWh         = function() { return screen.properties.funcMasterWh; };
    screen.setBarcode              = function(barcode) { screen.properties.barcode = RwAppData.stripBarcode(barcode.toUpperCase()); };
    screen.getBarcode              = function() { return screen.properties.barcode; };
    screen.getMasterId             = function() { return screen.getWebGetItemStatus().masterId; };
    screen.getMasterNo             = function() { return screen.getWebGetItemStatus().masterNo; };
    screen.getDescription          = function() { return screen.getWebGetItemStatus().description; };
    screen.getIsRentalItemOut      = function() { return (screen.getWebGetItemStatus().rentalStatus === 'OUT'); };
    screen.getIsRentalItemIn       = function() { return (screen.getWebGetItemStatus().rentalStatus === 'IN'); };
    screen.getIsICode              = function() { return (screen.getWebGetItemStatus().isICode      === true); };
    screen.setOrderId              = function(orderid) { screen.properties.orderid = orderid; };
    screen.getOrderId              = function() { return screen.properties.orderid; };
    screen.getRetireQty            = function() { return FwFormField.getValue(screen.$view, '.fieldretireqty'); };
    screen.setRetireQty            = function(value) { return FwFormField.setValue(screen.$view, '.fieldretireqty', value); };
    screen.setMaxRetireQty         = function(value) { return screen.$view.find('.fieldretireqty').attr('data-maxvalue', value); };
    screen.getRetiredReasonId       = function() { return FwFormField.getValue(screen.$view, '.fieldretiredreason'); };
    screen.getLossAmountType       = function() { return screen.$view.find('input[name="lossamount"]:checked').val(); };
    screen.setLossAmountType       = function(lossAmountType) { 
        if (lossAmountType === 'ItemValue') {
            screen.setLossAmount(screen.getUnitCost());
            screen.toggleEnabledLossAmount(false);
        } else if (lossAmountType === 'Custom') {
            screen.toggleEnabledLossAmount(true);
        }
        screen.$view.find('input[name="lossamount"][value="' + lossAmountType + '"]').prop('checked', true); 
    };
    screen.getLossAmount           = function() { return screen.$view.find('.txtLossAmount').val(); };
    screen.setLossAmount           = function(value) { screen.$view.find('.txtLossAmount').val(value); };
    screen.getRetireNote           = function() { return FwFormField.getValue(screen.$view, '.fieldnotes'); };
    screen.getUnitCost             = function() { return FwFunc.round(screen.getWebGetItemStatus().manifestvalue, 2); };
    screen.toggleEnabledLossAmount = function(value) { screen.$view.find('.txtLossAmount').prop('disabled', !value); };
    //----------------------------------------------------------------------------------------------------
    screen.hideEverything = function() {
        screen.$btnback.hide();
        screen.$btncontinue.hide();
        screen.$btnretire.hide();
        screen.$view.find('.scanbarcode').hide();
        screen.$view.find('.Error').hide();
        screen.$view.find('.screenItemStatus').hide();
        screen.$view.find('.screenRetire').hide();
    };
    screen.hideEverything();
    //----------------------------------------------------------------------------------------------------
    screen.showScanBarcodeScreen = function(goForward) {
        if (goForward) {
            screen.clearProperties();
        }
        screen.hideEverything();
        screen.$btncontinue.hide();
        screen.$view.find('.scanbarcode').show();
        
    };
    //----------------------------------------------------------------------------------------------------
    screen.showItemStatusScreen = function(goForward) {
        var request, itemStatusResponseScreen, isRentalItemOut, isRentalItemIn, isICode, showWarehouseInfo, showTrackedOutInfo, showStatus;
        screen.hideEverything();
        screen.$btnretire.hide();
        if (goForward) {
            screen.$view.find('.screenItemStatus .pnlStatus').hide();
            screen.$view.find('.screenItemStatus .pnlLocation1').hide();
            screen.$view.find('.screenItemStatus .pnlLocation2').hide();
            request = {
                barcode: screen.getBarcode()
            };
            if (request.barcode.length > 0) {
                RwServices.callMethod('AssetDisposition', 'GetItemStatus', request, function(response) {
                    try {
                        screen.setWebGetItemStatus(response.webGetItemStatus);
                        screen.setFuncMasterWh(response.funcMasterWh);
                        isRentalItemOut    = screen.getIsRentalItemOut();
                        isRentalItemIn     = screen.getIsRentalItemIn();
                        
                        isICode            = screen.getIsICode();
                        if (!isICode && isRentalItemOut) {
                            screen.setOrderId(response.webGetItemStatus.orderid);
                        }
                        showWarehouseInfo  = ((!isICode && isRentalItemIn) || isICode) && (response.funcMasterWh.length > 0);
                        showTrackedOutInfo = (!isICode && isRentalItemOut);
                        showStatus         = (response.webGetItemStatus.status === 0) || (response.webGetItemStatus.status === 401 /*item at another warehouse*/);
                        screen.$view.find('.txtGenericError').text(response.webGetItemStatus.genericError);
                        screen.$view.find('.txtMsg').text(response.webGetItemStatus.msg);
                
                        if (response.webGetItemStatus.status === 0) {
                            screen.$view.find('.screenItemStatus').toggle(showStatus);
                            screen.$view.find('.screenRetire').toggle(false);
                            screen.$view.find('.screenSelectOrder').toggle(false);
                            screen.$view.find('.pnlGenericStatus').toggle(!isICode && !isRentalItemOut && !isRentalItemIn);
                            screen.$view.find('.pnlGenericStatus,.pnlInStatus,.pnlOutStatus').css({
                                backgroundColor: response.webGetItemStatus.color, 
                                color: response.webGetItemStatus.textcolor
                            });
                            screen.$view.find('.pnlLocation1').toggle(showTrackedOutInfo);
                            screen.$view.find('.txtOnOrder .value').text(response.webGetItemStatus.orderDesc);
                            screen.$view.find('.pnlLocation2').toggle(showTrackedOutInfo);
                            screen.$view.find('.pnlInStatus').toggle(!isICode && isRentalItemIn);
                            screen.$view.find('.pnlOutStatus').toggle(!isICode && isRentalItemOut);
                            
                            screen.$view.find('.txtStatus').text(response.webGetItemStatus.rentalStatus);
                            screen.$view.find('.txtSetCharacterName').text(response.webGetItemStatus.setcharacter);
                            screen.$view.find('.txtAssetLocation').text(response.webGetItemStatus.location);
                            screen.$view.find('.txtStatusDate').text(response.webGetItemStatus.statusDate);
                            screen.$view.find('.txtDepartment').text(response.webGetItemStatus.department);
                            screen.$view.find('.txtProduction').text(response.webGetItemStatus.deal);
                            screen.$view.find('.txtICode').text(response.webGetItemStatus.masterNo);
                            screen.$view.find('.txtDescription').text(response.webGetItemStatus.description);
                            if (response.funcMasterWh.length > 0) {
                                screen.$view.find('.txtAisle').text(isICode ? response.funcMasterWh[0].aisleloc : response.webGetItemStatus.aisleloc);
                                screen.$view.find('.txtShelf').text(isICode ? response.funcMasterWh[0].shelfloc : response.webGetItemStatus.shelfloc);
                                screen.$view.find('.txtWarehouse').text(response.funcMasterWh[0].warehouse);
                                screen.$view.find('.txtQty').text(response.funcMasterWh[0].qty);
                                screen.$view.find('.txtQtyIn').text(response.funcMasterWh[0].qtyin);
                                screen.$view.find('.txtQtyQCRequired').text(response.funcMasterWh[0].qtyqcrequired);
                                screen.$view.find('.txtStaged').text(response.funcMasterWh[0].staged);
                                screen.$view.find('.txtQtyOut').text(response.funcMasterWh[0].qtyout);
                                screen.$view.find('.txtInRepair').text(response.funcMasterWh[0].inrepair);
                                screen.$view.find('.txtQtyOnPO').text(response.funcMasterWh[0].qtyonpo);
                            }
                            screen.$view.find('.trDepartment').toggle(!isICode && isRentalItemOut);
                            screen.$view.find('.trProduction').toggle(!isICode && isRentalItemOut);
                            screen.$view.find('.trAisle').toggle(isICode && isRentalItemIn);
                            screen.$view.find('.trShelf').toggle(isICode && isRentalItemIn);
                            screen.$view.find('.trWarehouse').toggle(showWarehouseInfo);
                            screen.$view.find('.trQty').toggle(showWarehouseInfo);
                            screen.$view.find('.trQtyIn').toggle(showWarehouseInfo);
                            screen.$view.find('.trQtyQCRequired').toggle(showWarehouseInfo);
                            screen.$view.find('.trStaged').toggle(showWarehouseInfo);
                            screen.$view.find('.trQtyOut').toggle(showWarehouseInfo);
                            screen.$view.find('.trInRepair').toggle(showWarehouseInfo);
                            screen.$view.find('.trQtyOnPO').toggle(showWarehouseInfo);

                            screen.$view.find('.trVendor').toggle(showTrackedOutInfo);
                            screen.$view.find('.txtVendor').text(response.webGetItemStatus.vendor);
                            screen.$view.find('.trPrimaryVendor').toggle(showTrackedOutInfo);
                            screen.$view.find('.txtPrimaryVendor').text(response.webGetItemStatus.buyer);
                            screen.$view.find('.trManufacturer').toggle(showTrackedOutInfo);
                            screen.$view.find('.txtManufacturer').text(response.webGetItemStatus.manufacturer);
                            screen.$view.find('.trPONo').toggle(showTrackedOutInfo);
                            screen.$view.find('.txtPONo').text(response.webGetItemStatus.pono);
                            //screen.$view.find('.btnContinue').toggle(!isICode);
                            screen.$btncontinue.show();
                            
                            // retirefrom
                            screen.$view.find('.retirefrom').toggle(isICode);
                            if (isICode) {
                                var hasretirefromorder     = !((response.funcMasterWh.length === 0) || response.funcMasterWh[0].qtyout <= 0);
                                var hasretirefromwarehouse = !((response.funcMasterWh.length === 0) || response.funcMasterWh[0].qtyin <= 0);
                                screen.$view.find('#retirefromorder')
                                    .prop('disabled', !hasretirefromorder)
                                    .prop('checked', hasretirefromorder);
                                screen.$view.find('#retirefromwarehouse')
                                    .prop('disabled', !hasretirefromwarehouse)
                                    .prop('checked', hasretirefromwarehouse && !hasretirefromorder);
                            }
                            
                            FwFormField.setValueByDataField(screen.$view, 'masterid', screen.getMasterId());
                            screen.$view.find('.txtICodeNo').text(screen.getMasterNo());
                            screen.$view.find('.txtICodeDescription').text(screen.getDescription());
                            screen.$view.find('.fieldOrderNo').toggle(false);
                            screen.$view.find('.fieldRetireQty').toggle(isICode);
                            screen.$view.find('.fieldTrackLossAmount').toggle(isRentalItemOut || isICode);
                            screen.$view.find('.pnlImages').empty();
                            for(i = 0; i < response.appImages.length; i++) {
                                var img = jQuery('<img>')
                                        .attr('src', 'data:image/jpeg;base64,' + response.appImages[i].thumbnail)
                                        .attr('data-appimageid', response.appImages[i].appimageid)
                                ;
                                screen.$view.find('.pnlImages').append(img);
                            }
                            screen.$view.find('.screenItemStatus').show();
                            screen.$btnback.show();
                            if (isICode) {
                                screen.$btncontinue.toggle(hasretirefromorder || hasretirefromwarehouse);
                            } else {
                                screen.$btncontinue.toggle(isRentalItemIn || isRentalItemOut);
                            }
                        } else { //if (response.webGetItemStatus.status !== 0)
                            screen.showScanBarcodeScreen(true);
                            screen.$view.find('.Error').show();
                        }
                        application.playStatus(response.webGetItemStatus.status === 0);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } else {
            screen.$view.find('.screenItemStatus').show();
            screen.$btnback.show();
            screen.$btncontinue.show();
        }
    };
    //----------------------------------------------------------------------------------------------------
    screen.showRetireScreen = function() {
        var isinwarehouse = (screen.getIsICode() && screen.$view.find('#retirefromwarehouse').prop('checked')) || (!screen.getIsICode() && screen.getIsRentalItemIn());
        var isonorder     = (screen.getIsICode() && screen.$view.find('#retirefromorder').prop('checked')) || (!screen.getIsICode() && screen.getIsRentalItemOut());
        screen.hideEverything();
        screen.$btnback.show();
        screen.$btnretire.show();
        screen.$view.find('.screenRetire .warehouse').toggle(isinwarehouse);
        FwFormField.setValue(screen.$view, '.fieldsetcharacter', '', '');
        if (screen.getIsICode()) {
            if (screen.$view.find('#retirefromwarehouse').prop('checked')) {
                FwFormField.enable(screen.$view.find('.fieldretireqty'));
                screen.setRetireQty(1);
                screen.setMaxRetireQty(screen.getFuncMasterWh()[0].qtyin);
            } else if (screen.$view.find('#retirefromorder').prop('checked')) {
                FwFormField.disable(screen.$view.find('.fieldretireqty'));
                screen.setRetireQty(0);
                screen.setMaxRetireQty(0);
            }
            screen.$view.find('.fieldretireqty').show();
        } else { //barcode
            screen.setRetireQty(1);
            screen.setMaxRetireQty(1);
            screen.$view.find('.fieldretireqty').hide();
        }
        FwFormField.setValue(screen.$view, '.fieldretiredreason', '', '');
        screen.$view.find('.fieldsetcharacter').toggle(screen.getIsICode() && screen.$view.find('#retirefromorder').prop('checked'));
        
        screen.$view.find('.fieldTrackLossAmount').toggle(isonorder);
        screen.setLossAmountType('ItemValue');
        screen.$view.find('.screenRetire').show();
    };
    //----------------------------------------------------------------------------------------------------
    screen.getGetRetiredReason = function(callback) {
        var requestGetRetiredReason = {};
        RwServices.inventory.getRetiredReason(requestGetRetiredReason, function(response) {
            try {
                var html = [], item;
                html.push('<option value=""></option>');
                for (var index = 0; index < response.GetRetiredReason.length; index++) {
                    item = response.GetRetiredReason[index];
                    html.push('<option value="' + item.retiredreasonid + '">' + item.retiredreason + '</option>');
                }
                screen.$view.find('.screenRetire .retirereason .fwformfield-value').html(html.join(''));
                if (typeof callback === 'function') callback();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };
    //----------------------------------------------------------------------------------------------------
    screen.retireItem = function() {
        var retirefromorder = screen.$view.find('#retirefromorder').prop('checked');
        if (screen.getIsICode()) {
            if (retirefromorder) {
                if (screen.getOrderId().length === 0) throw 'Please select a ' + RwLanguages.translate('Set Character') + '.';
            }
        }
        if (screen.getRetireQty().length === 0) throw RwLanguages.translate('Retire Quantity') + ' is required.'; 
        if (+screen.getRetireQty() === 0) {
            if (+screen.$view.find('.fieldretireqty').attr('data-maxvalue') === 0) {
                throw 'There are no items that can be retired.';
            } else {
                throw RwLanguages.translate('Retire Quantity') + ' cannot be 0.';
            }
        }
        if (+screen.getRetireQty() < +screen.$view.find('.fieldretireqty').attr('data-minvalue')) throw RwLanguages.translate('Retire Quantity') + ' cannot be less than ' + screen.$view.find('.fieldretireqty').attr('data-minvalue') + '.'; 
        if (+screen.getRetireQty() > +screen.$view.find('.fieldretireqty').attr('data-maxvalue')) throw RwLanguages.translate('Retire Quantity') + ' cannot be greater than ' + screen.$view.find('.fieldretireqty').attr('data-minvalue') + '.';
        if (screen.getRetiredReasonId().length === 0) throw 'Please select a Retire Reason';
        var requestRetireItem = {
            isicode:             screen.getIsICode(),
            barcode:             screen.getIsICode() ? '' : screen.getBarcode(),
            orderid:             screen.getOrderId(),
            masterno:            screen.getMasterNo(),
            qty:                 screen.getRetireQty(),
            retiredreasonid:     screen.getRetiredReasonId(),
            lossamount:          screen.getLossAmount(),
            notes:               screen.getRetireNote()
        };
        RwServices.callMethod('AssetDisposition', 'RetireItem', requestRetireItem, function(response) {
            try {
                application.navigate('home/home');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };
    //----------------------------------------------------------------------------------------------------
    screen.$view
        .on('change', '.pnlOrderNoBarcode-txtBarcode', function() {
            try {
                screen.getOrders(this.value.toUpperCase());
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.pnlOrderNoBarcode-imgFind', function() {
            try {
                screen.getOrders('');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.rbgLossAmountType > li > input[type="radio"]', function() {
            try {
                screen.setLossAmountType(this.value);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    //----------------------------------------------------------------------------------------------------
    screen.selectItem = function(code) {
        screen.clearProperties();
        //screen.$view.find('.search').fwmobilesearch('clearsearchresults');
        screen.setBarcode(code);
        screen.showItemStatusScreen(true);
    };
    //----------------------------------------------------------------------------------------------------
    screen.load = function() {
        application.setScanTarget('');
        if (typeof window.DTDevices !== 'undefined') {
            window.DTDevices.registerListener('barcodeData', 'barcodeData_assetdisposition', function(barcode, barcodeType) {
                try {
                    screen.$view.find('.search').fwmobilesearch('setsearchmode', 'code');
                    screen.$view.find('.search .searchbox').val(barcode);
                    screen.selectItem(barcode);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if (!Modernizr.touch) {
            screen.$view.find('.search input').select();
        }
        var $search = screen.$view.find('.search').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'AssetDisposition', 
            method: 'GetSearchResults',
            queryTimeout: 30,
            searchModes:  [
                {caption:'Barcode/I-Code', placeholder:'Barcode/I-Code', value:'code', search: function(code) {
                    if (code.length > 0) {
                        screen.selectItem(code);
                    }
                }},
                {caption:'Description', value:'description'},
                {caption:'Department', value:'department'},
                {caption:'Production', value:'production'},
                {caption:'Set Character', value:'setcharacter'},
                {caption:'Set No', value:'setno'}
            ],
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html: string | string[] = [];
                html.push('<div class="item">');
                //html.push('  <div class="row1"><span class="masterno">{{masterno}}</span> - <span class="master">{{master}}</span></div>');
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                html.push('      <div class="masterno">{{masterno}}</div>');
                if (model.thumbnail.length > 0) {
                    html.push('      <div class="image"><img src="{{thumbnail}}" /></div>');
                }
                html.push('    </div>');
                html.push('    <div class="col2">');
                html.push('      <div class="master">{{master}}</div>');
                if (model.setcharacter.length > 0) {
                    html.push('      <div class="caption setcharacter">' + RwLanguages.translate('Set Character') + ':</div>');
                    html.push('      <div class="value setcharacter">{{setcharacter}}</div>');
                }
                if (model.barcode.length > 0) {
                    html.push('      <div class="caption barcode">' + RwLanguages.translate('Barcode') + ':</div>');
                    html.push('      <div class="value barcode">{{barcode}}</div>');
                }
                if (model.manufacturer.length > 0) {
                    html.push('      <div class="caption manufacturer">' + RwLanguages.translate('Manufacturer') + '</div>');
                    html.push('      <div class="value manufacturer">{{manufacturer}}</div>');
                }
                if (model.department.length > 0) {
                    html.push('      <div class="caption department">' + RwLanguages.translate('Department') + '</div>');
                    html.push('      <div class="value department">{{department}}</div>');
                }
                if (model.production.length > 0) {
                    html.push('      <div class="caption production">' + RwLanguages.translate('Production') + '</div>');
                    html.push('      <div class="value production">{{production}}</div>');
                }
                if (model.refnobox.length > 0) {
                    html.push('      <div class="caption refnobox">' + RwLanguages.translate('Ref No Box') + '</div>');
                    html.push('      <div class="value refnobox">{{refnobox}}</div>');
                }
                if (model.refnopallet.length > 0) {
                    html.push('      <div class="caption refnopallet">' + RwLanguages.translate('Ref No Pallet') + '</div>');
                    html.push('      <div class="value refnopallet">{{refnopallet}}</div>');
                }
                html.push('    </div>');
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function() {
                screen.showScanBarcodeScreen(true);
            },
            recordClick: function(model) {
                var code = '';
                if (model.barcode.length > 0) {
                    code = model.barcode;
                } else {
                    code = model.masterno;
                }
                screen.selectItem(code);
            }
        });
        screen.showScanBarcodeScreen(true);
    };
    //----------------------------------------------------------------------------------------------------
    screen.unload = function() {
        screen.$view.find('.search').fwmobilesearch('destroy');
        if (typeof window.DTDevices !== 'undefined') {
            window.DTDevices.unregisterListener('barcodeData', 'barcodeData_assetdisposition');
        }
    };
    //----------------------------------------------------------------------------------------------------
    screen.showScanBarcodeScreen(true);
    
    return screen;
};