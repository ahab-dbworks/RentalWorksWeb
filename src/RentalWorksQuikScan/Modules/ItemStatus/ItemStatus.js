//----------------------------------------------------------------------------------------------
RwOrderController.getItemStatusScreen = function(viewModel, properties) {
    var applicationOptions = program.getApplicationOptions();
    var combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Item Status'),
        htmlScanBarcode:    RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Bar Code / I-Code')}),
        captionBC:          RwLanguages.translate('BC'),
        captionAsOf:        RwLanguages.translate('As Of'),
        captionBarcode:     RwLanguages.translate('Barcode'),
        captionSerialNo:    RwLanguages.translate('Serial No'),
        captionRFID:        RwLanguages.translate('RFID'),
        captionICode:       RwLanguages.translate('I-Code'),
        captionLastDeal:    RwLanguages.translate('Last Deal'),
        captionLastOrder:   RwLanguages.translate('Last Order'),
        captionDescription: RwLanguages.translate('Description'),
        captionAisle:       RwLanguages.translate('Aisle'),
        captionShelf:       RwLanguages.translate('Shelf'),
        captionLastDealNo:  RwLanguages.translate('Last Deal No'),
        captionLastOrderNo: RwLanguages.translate('Last Order No'),
        captionScanButton:  RwLanguages.translate('Scan'),
        captionRFIDButton:  RwLanguages.translate('RFID'),
        captionVendor:      RwLanguages.translate('Vendor')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-itemStatus').html(), combinedViewModel);
    var screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);

    var $error       = screen.$view.find('.is-error');
    var $itemdetails = screen.$view.find('.is-itemdetails');
    var $rfiditems   = screen.$view.find('.is-rfiditems');

    screen.$view
        .on('keypress', '.fwmobilecontrol-value', function(e) {
            try {
                if (e.which === '15') {
                    var $this = jQuery(this);
                    $this.change();
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('change', '.fwmobilecontrol-value', function() {
            try {
                var $this = jQuery(this);
                var request = {
                    barcode: RwAppData.stripBarcode($this.val().toUpperCase())
                };
                if (request.barcode.length > 0) {
                    screen.resetscreen();
                    RwServices.callMethod('ItemStatus', 'GetItemStatus', request, function(response) {
                        screen.loaditemdata(response.itemdata);
                        program.playStatus(response.itemdata.status === 0);

                        //if (response.itemdata.status === 0) $this.val('');
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.rfid-item', function() {
            var $this = jQuery(this);
            if ($this.data('recorddata').rentalstatus !== '') {
                $rfiditems.hide();
                $rfiditems.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', true, function() { //back
                    $rfiditems.show();
                    $itemdetails.hide();
                    jQuery(this).remove();
                });
                screen.$view.find('.fwmobilecontrol-value').val($this.data('recorddata').barcode);
                var request = {
                    barcode: $this.data('recorddata').tag
                };
                RwServices.callMethod('ItemStatus', 'GetItemStatus', request, function(response) {
                    screen.loaditemdata(response.itemdata);
                });
            }
        })
    ;
    screen.resetscreen = function() {
        $rfiditems.empty();
        $rfiditems.hide();
        if (typeof $rfiditems.$back !== 'undefined') $rfiditems.$back.remove();
        $itemdetails.hide();
        $error.hide();

    };
    screen.loaditemdata = function(itemdata) {
        $error.toggle(itemdata.status !== 0);
        $error.find('.genericError').html(itemdata.genericError);
        $error.find('.msg').html(itemdata.msg);

        $itemdetails.toggle((itemdata.status === 0) || (itemdata.status === 401)); //401 = item at another warehouse

        $itemdetails.find('.is-itemstatus').toggle(!itemdata.isICode);
        $itemdetails.find('.is-itemstatus').css({backgroundColor: itemdata.color, color: itemdata.textcolor});
        $itemdetails.find('.is-itemstatus #is-txtDetail01').html(itemdata.detail01);
        $itemdetails.find('.is-itemstatus #is-txtStatusDate').html(itemdata.statusDate);

        if (itemdata.rentalStatus === 'OUT') {
            $itemdetails.find('#is-captionLastDealNo').html(RwLanguages.translate('Deal No'));
            $itemdetails.find('#is-captionLastDeal').html(RwLanguages.translate('Deal'));
            $itemdetails.find('#is-captionLastOrderNo').html(RwLanguages.translate('Order No'));
            $itemdetails.find('#is-captionLastOrder').html(RwLanguages.translate('Order'));
        } else {
            $itemdetails.find('#is-captionLastDealNo').html(RwLanguages.translate('Last Deal No'));
            $itemdetails.find('#is-captionLastDeal').html(RwLanguages.translate('Last Deal'));
            $itemdetails.find('#is-captionLastOrderNo').html(RwLanguages.translate('Last Order No'));
            $itemdetails.find('#is-captionLastOrder').html(RwLanguages.translate('Last Order'));
        }

        $itemdetails.find('#is-trBarcode').toggle(itemdata.barcode !== '');
        $itemdetails.find('#is-txtBarcode').html(itemdata.barcode);
        $itemdetails.find('#is-trSerialNo').toggle(itemdata.mfgserial !== '');
        $itemdetails.find('#is-txtSerialNo').html(itemdata.mfgserial);
        $itemdetails.find('#is-trRFID').toggle(itemdata.rfid !== '');
        $itemdetails.find('#is-txtRFID').html(itemdata.rfid);
        $itemdetails.find('#is-txtICode').html(itemdata.masterNo);
        $itemdetails.find('#is-txtDescription').html(itemdata.description);
        $itemdetails.find('#is-trAisle').toggle(!itemdata.isICode);
        $itemdetails.find('#is-txtAisle').html(itemdata.aisleloc);
        $itemdetails.find('#is-trShelf').toggle(!itemdata.isICode);
        $itemdetails.find('#is-txtShelf').toggle(!itemdata.isICode).html(itemdata.shelfloc);
        $itemdetails.find('#is-txtLastDealNo').html(itemdata.dealNo);
        $itemdetails.find('#is-txtLastDeal').html((itemdata.deal !== '') ? itemdata.deal : 'N/A');
        $itemdetails.find('#is-txtLastOrderNo').html(itemdata.orderNo);
        $itemdetails.find('#is-txtLastOrder').html((itemdata.orderDesc !== '') ? itemdata.orderDesc : 'N/A');

        $itemdetails.find('#is-trLastDealNo').toggle(!itemdata.isICode);
        $itemdetails.find('#is-trLastDeal').toggle(!itemdata.isICode);
        $itemdetails.find('#is-trLastOrderNo').toggle(!itemdata.isICode);
        $itemdetails.find('#is-trLastOrder').toggle(!itemdata.isICode);
        $itemdetails.find('#is-txtBarcode').html(itemdata.barcode);
        $itemdetails.find('#is-trVendor').toggle(itemdata.vendor !== '');
        $itemdetails.find('#is-txtVendor').html(itemdata.vendor);

        var $warehouse;
        $itemdetails.find('.is-warehouses').empty();
        for (var whno = 0; whno < itemdata.warehousedata.length; whno++) {
            $warehouse = jQuery(Mustache.render(jQuery('#tmpl-itemStatusWarehouse').html(), {
                captionWarehouse:   RwLanguages.translate('Warehouse'),
                valueWarehouse:     itemdata.warehousedata[whno].warehouse,
                captionTotal:       RwLanguages.translate('Total'),
                valueTotal:         itemdata.warehousedata[whno].qty,
                captionAisleloc:    RwLanguages.translate('Aisle'),
                valueAisleloc:      itemdata.warehousedata[whno].aisleloc,
                captionShelfloc:    RwLanguages.translate('Shelf'),
                valueShelfloc:      itemdata.warehousedata[whno].shelfloc,
                captionIn:          RwLanguages.translate('In'),
                valueIn:            itemdata.warehousedata[whno].qtyin,
                captionQCRqd:       RwLanguages.translate('QC Rq\'d'),
                valueQCRqd:         itemdata.warehousedata[whno].qtyqcrequired,
                captionStaged:      RwLanguages.translate('Staged'),
                valueStaged:        itemdata.warehousedata[whno].qtystaged,
                captionOut:         RwLanguages.translate('Out'),
                valueOut:           itemdata.warehousedata[whno].qtyout,
                captionInRepair:    RwLanguages.translate('In Repair'),
                valueInRepair:      itemdata.warehousedata[whno].qtyinrepair,
                captionOnPO:        RwLanguages.translate('On PO'),
                valueOnPO:          itemdata.warehousedata[whno].qtyonpo
            }));
            if (itemdata.warehousedata[whno].aisleloc.length === 0) {
                $warehouse.find('.captionAisleloc').empty();
            }
            if (itemdata.warehousedata[whno].shelfloc.length === 0) {
                $warehouse.find('.captionShelfloc').empty();
            }
            $itemdetails.find('.is-warehouses').append($warehouse);
        }

        $itemdetails.find('.is-images').empty();
        for(i = 0; i < itemdata.images.length; i++) {
            var $img = jQuery('<img>')
                    .attr('src', 'data:image/jpeg;base64,' + itemdata.images[i].thumbnail)
                    .attr('data-appimageid', itemdata.images[i].appimageid);
            $itemdetails.find('.is-images').append($img);
            $img.on('click', function () {
                var $this = jQuery(this);
                var html = [];
                html.push('<img style="max-width:100%;" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'fwappimage.ashx?method=GetAppImage&appimageid=' + $this.attr('data-appimageid') + '&thumbnail=false' + '\" >');
                html = html.join('\n');
                var $confirmation = FwConfirmation.renderConfirmation('Image Viewer', html);
                var $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
            });
        }
    };

    screen.rfidscan = function (epcs) {
        screen.$view.find('.fwmobilecontrol-value').val('');
        if (epcs !== '') {
            screen.resetscreen();
            RwServices.callMethod('ItemStatus', 'ItemStatusRFID', { tags: epcs }, function (response) {
                if (response.items.length === 1) {
                    var request = {
                        barcode: response.items[0].tag
                    };
                    RwServices.callMethod('ItemStatus', 'GetItemStatus', request, function(response) {
                        screen.loaditemdata(response.itemdata);
                    });
                } else {
                    var $item, html = [];

                    html.push('<div class="rfid-item">');
                    html.push('  <div class="rfid-item-title"></div>');
                    html.push('  <div class="rfid-item-info">');
                    html.push('    <div class="rfid-data rfid">');
                    html.push('      <div class="item-caption">RFID:</div>');
                    html.push('      <div class="item-value"></div>');
                    html.push('    </div>');
                    html.push('    <div class="rfid-data barcode">');
                    html.push('      <div class="item-caption">Barcode:</div>');
                    html.push('      <div class="item-value"></div>');
                    html.push('    </div>');
                    html.push('    <div class="rfid-data message">');
                    html.push('      <div class="item-caption">Status:</div>');
                    html.push('      <div class="item-value"></div>');
                    html.push('    </div>');
                    html.push('  </div>');
                    html.push('</div>');

                    $rfiditems.show();

                    for (var i = 0; i < response.items.length; i++) {
                        $item = jQuery(html.join(''));
                        
                        (response.items[i].rentalstatus === '') ? $item.addClass('exception') : $item.addClass('item');

                        $item.find('.rfid-item-title').html(response.items[i].title);
                        $item.find('.rfid-data.rfid .item-value').html(response.items[i].tag);
                        $item.find('.rfid-data.barcode .item-value').html((response.items[i].barcode !== '') ? response.items[i].barcode : '-');
                        $item.find('.rfid-data.message .item-value').html(response.items[i].rentalstatus);

                        $item.data('recorddata', response.items[i]);

                        $rfiditems.append($item);
                    }

                    if (response.items.length === 0) {
                        $rfiditems.append('<div class="norecords">0 records found</div>');
                    }
                }
            });
        }
    };

    screen.load = function() {
        // setup Linea Pro Barcode Scanner
        program.setScanTarget('.fwmobilecontrol-value');
        program.setScanTargetLpNearfield('.fwmobilecontrol-value');

        // setup TSL RFID Reader
        RwRFID.registerEvents(screen.rfidscan);

        // setup Linea Pro HF RFID Reader
        if (program.hasHfRfidApplicationOption === true) {
            //alert('initing HF RFID...');
            if (typeof window.DTDevices !== 'undefined' && typeof window.DTDevices.rfInitWithFieldGain === 'function') {
                DTDevices.rfInitWithFieldGain('ISO15', -1000,
                    function () {
                        FwNotification.renderNotification('SUCCESS', 'Enabled nearfield scanner.');
                    },
                    function () {
                        FwNotification.renderNotification('ERROR', 'Can\'t enable nearfield scanner.');
                    }
                );
            }
            if (typeof window.DTDevices !== 'undefined' && typeof window.DTDevices.rfInitWithFieldGain === 'function') {
                DTDevices.registerListener('rfCardDetected', 'rfCardDetected_applicationjs',
                    function (returnUid, returnType, cardIndex) {
                        jQuery('.scanTarget').val(returnUid).change();
                        FwNotification.renderNotification('INFO', returnUid);
                        DTDevices.rfRemoveCard(cardIndex,
                            function success() {
                                FwNotification.renderNotification('SUCCESS', 'rfRemoveCard success');
                            },
                            function fail() {
                                FwNotification.renderNotification('ERROR', 'rfRemoveCard failed');
                            });
                    }
                );
            }
        }

        //if (typeof window.TslReader !== 'undefined') {
        //    window.TslReader.registerListener('deviceConnected', 'deviceConnected_rwordercontrollerjs_getItemStatusScreen', function() {
        //        RwRFID.isConnected = true;
        //        FwNotification.renderNotification('INFO', 'RFID Reader Connected');
        //    });
        //    window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_rwordercontrollerjs_getItemStatusScreen', function() {
        //        RwRFID.isConnected = false;
        //        FwNotification.renderNotification('INFO', 'RFID Reader Disconnected');
        //    });
        //}

        //if (typeof window.ZebraRFIDScanner !== 'undefined') {
        //    ZebraRFIDScanner.registerListener('srfidEventCommunicationSessionEstablished', 'srfidEventCommunicationSessionEstablished_getItemStatusScreen', function() {
        //        RwRFID.isConnected = true;
        //    });
        //    ZebraRFIDScanner.registerListener('srfidEventCommunicationSessionTerminated', 'srfidEventCommunicationSessionTerminated_rwordercontrollerjs_getItemStatusScreen', function() {
        //        RwRFID.isConnected = false;
        //    });
        //    ZebraRFIDScanner.isConnected(function(isConnected) {
        //        RwRFID.isConnected = true;
        //    });
        //}
    };

    screen.unload = function() {
        // reset scan target for LineaPro
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');

        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_rwordercontrollerjs_getItemStatusScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_rwordercontrollerjs_getItemStatusScreen');
        }

        RwRFID.unregisterEvents();

        // setup Linea Pro HF RFID Reader
        if (program.hasHfRfidApplicationOption === true) {

        }

        //if (typeof window.ZebraRFIDScanner !== 'undefined') {
        //    window.ZebraRFIDScanner.unregisterListener('srfidEventCommunicationSessionEstablished', 'srfidEventCommunicationSessionEstablished_rwordercontrollerjs_getItemStatusScreen');
        //    window.ZebraRFIDScanner.unregisterListener('srfidEventCommunicationSessionTerminated', 'srfidEventCommunicationSessionTerminated_rwordercontrollerjs_getItemStatusScreen');
        //}
    };
    
    return screen;
};