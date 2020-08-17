//----------------------------------------------------------------------------------------------
RwInventoryController.getRepairItemScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle;
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwLanguages.translate('Repair') + " (" + RwLanguages.translate(properties.repairMode) + ")",
        htmlScanBarcode:     RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Repair No. / BC')}),
        captionICode:        RwLanguages.translate('I-Code'),
        captionQty:          RwLanguages.translate('Qty'),
        captionInRepair:     RwLanguages.translate('In Repair'),
        captionReleased:     RwLanguages.translate('Released'),
        captionLastOrder:    RwLanguages.translate("Last Order"),
        captionLastDeal:     RwLanguages.translate('Deal'),
        captionSubmit:       RwLanguages.translate('Submit'),
        captionScanButton:   RwLanguages.translate('Scan'),
        captionRFIDButton:   RwLanguages.translate('RFID')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-repairItem').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.$view.find('#repairItemView-response').hide();
    screen.$view.find('#repairItemView').data('scanmode', 'SCAN');
    
    if (RwRFID.isConnected) {
        screen.$view.find('.modeSelector').show();
    }

    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                request = {
                   code:        RwAppData.stripBarcode($this.val().toUpperCase())
                 , repairMode:  properties.repairMode
                 , qty:         0
                };
                if ((request.code.length > 0) && (screen.$view.find('#repairItemView').data('scanmode') == 'SCAN')) {
                    RwServices.order.repairItem(request, function(response) {
                        try {
                            properties.response = response;
                            screen.getRepairItemResponseScreen(response, properties);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#repairItemView-btnSubmit', function() {
            var qty, requestRepairItem_ChangeQuantity;
            try {
                qty = jQuery('#repairItemView-qty input').val();
                if ( !isNaN(parseInt(qty))  && (parseInt(qty) >= 0) )
                {
                    requestRepairItem_ChangeQuantity = {
                        code:       properties.response.webRepairItem.repairNo
                      , repairMode: properties.repairMode
                      , qty:        parseInt(qty)
                    };
                    RwServices.order.repairItem(requestRepairItem_ChangeQuantity, function(responseRepairItem_ChangeQuantity) {
                        try {
                            screen.getRepairItemResponseScreen(responseRepairItem_ChangeQuantity, properties);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });

                    jQuery('#repairItemView-qty').find('input').val('');
                } else {
                    FwFunc.showError('Qty is invalid');
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnScan', function() {
            try {
                screen.$view.find('#repairItemView').data('scanmode', 'SCAN');
                screen.$view.find('.repairItemView-RFID').hide();
                screen.$view.find('.repairItemView-Scan').show();
                screen.$view.find('.modeSelector .modebtn').removeClass('selected');
                jQuery(this).addClass('selected');
                RwRFID.unregisterRFIDEvents();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnRFID', function() {
            try {
                screen.$view.find('#repairItemView').data('scanmode', 'RFID');
                screen.$view.find('.repairItemView-Scan').hide();
                screen.$view.find('.repairItemView-RFID').show();
                screen.$view.find('.modeSelector .modebtn').removeClass('selected');
                jQuery(this).addClass('selected');
                RwRFID.registerRFIDEvents(screen.rfidscan);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.rfid-item', function() {
            var $this, $confirmation, $release, $cancel, html = [], request;
            try {
                $this = jQuery(this);
                if ($this.attr('data-inrepair') == 'T') {
                    $confirmation = FwConfirmation.renderConfirmation($this.attr('data-title'), '');
                    $release      = FwConfirmation.addButton($confirmation, 'Release', true);
                    $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

                    html.push('<table id="itemStatusView-tblDetails">');
                        html.push('<tbody>');
                            html.push('<tr id="itemStatusView-trICode"         class="light"><td id="itemStatusView-captionICode"         class="key">I-Code</td>        <td id="itemStatusView-txtICode" class="value">'       + $this.attr('data-masterno') +'</td></tr>');
                            html.push('<tr id="itemStatusView-trDescription"   class="light"><td id="itemStatusView-captionDescription"   class="key">Description</td>   <td id="itemStatusView-txtDescription" class="value">' + $this.attr('data-master')   +'</td></tr>');
                            if ($this.attr('data-lastorderno') !== '') {
                                html.push('<tr id="itemStatusView-trLastOrderNo"   class="dark"> <td id="itemStatusView-captionLastOrderNo"   class="key">Last Order No</td> <td id="itemStatusView-txtLastOrderNo" class="value">' + $this.attr('data-lastorderno')   +'</td></tr>');
                                html.push('<tr id="itemStatusView-trLastOrder"     class="dark"> <td id="itemStatusView-captionLastOrder"     class="key">Last Order</td>    <td id="itemStatusView-txtLastOrder" class="value">'   + $this.attr('data-lastorderdesc') +'</td></tr>');
                            }
                            if ($this.attr('data-lastdealno') !== '') {
                                html.push('<tr id="itemStatusView-trLastDealNo"    class="dark"> <td id="itemStatusView-captionLastDealNo"    class="key">Last Deal No</td>  <td id="itemStatusView-txtLastDealNo" class="value">' + $this.attr('data-lastdealno')   +'</td></tr>');
                                html.push('<tr id="itemStatusView-trLastDeal"      class="dark"> <td id="itemStatusView-captionLastDeal"      class="key">Last Deal</td>     <td id="itemStatusView-txtLastDeal" class="value">'   + $this.attr('data-lastdealdesc') +'</td></tr>');
                            }
                        html.push('</tbody>');
                    html.push('</table>');

                    FwConfirmation.addControls($confirmation, html.join(''));

                    $release.on('click', function() {
                        try {
                            request = {
                                code:       $this.attr('data-rfid'),
                                repairMode: properties.repairMode,
                                qty:        0
                            };
                            RwServices.order.repairItem(request, function(response) {
                                try {
                                    if (response.webRepairItem.status == '0') {
                                        $this.attr('data-inrepair', 'F');
                                        $this.find('.rfid-item-message .item-value').html('Released');
                                        $this.removeClass('exception').addClass('processed');
                                    }
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.rfidscan = function (epcs) {
        if (epcs != '') {
            var request;
            screen.$view.find('.repairItemView-inrepair .rfid-placeholder').show();
            screen.$view.find('.repairItemView-inrepair .rfid-items').empty();
            screen.$view.find('.repairItemView-notinrepair .rfid-placeholder').show();
            screen.$view.find('.repairItemView-notinrepair .rfid-items').empty();
            request = {
                tags: epcs
            };
            RwServices.inventory.repairstatusrfid(request, function(response) {
                var $item;
                try {
                    for (var i = 0; i < response.items.length; i++) {
                        $item = screen.rfiditem('item');
                        $item.find('.rfid-item-title').html(response.items[i].title);
                        $item.find('.rfid-data.rfid .item-value').html(response.items[i].rfid);
                        $item.find('.rfid-data.message .item-value').html((response.items[i].rentalstatus != '') ? response.items[i].rentalstatus : '-');

                        $item.attr('data-title',         response.items[i].title);
                        $item.attr('data-masterno',      response.items[i].masterno);
                        $item.attr('data-master',        response.items[i].master);
                        $item.attr('data-barcode',       response.items[i].barcode);
                        $item.attr('data-rfid',          response.items[i].rfid);
                        $item.attr('data-lastorderid',   response.items[i].lastorderid);
                        $item.attr('data-lastorderno',   response.items[i].lastorderno);
                        $item.attr('data-lastorderdesc', response.items[i].lastorderdesc);
                        $item.attr('data-lastdealid',    response.items[i].lastdealid);
                        $item.attr('data-lastdealno',    response.items[i].lastdealno);
                        $item.attr('data-lastdealdesc',  response.items[i].lastdealdesc);
                        $item.attr('data-orderno',       response.items[i].orderno);
                        $item.attr('data-orderdesc',     response.items[i].orderdesc);
                        $item.attr('data-rentalstatus',  response.items[i].rentalstatus);
                        $item.attr('data-inrepair',      response.items[i].inrepair);

                        if (response.items[i].inrepair == 'T') {
                            screen.$view.find('.repairItemView-inrepair .rfid-placeholder').hide();
                            screen.$view.find('.repairItemView-inrepair .rfid-items').append($item);
                        } else {
                            screen.$view.find('.repairItemView-notinrepair .rfid-placeholder').hide();
                            screen.$view.find('.repairItemView-notinrepair .rfid-items').append($item);
                        }
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    };

    screen.rfiditem = function(itemtype) {
        var html;
        html = [];
        html.push('<div class="rfid-item ' + itemtype + '">');
            html.push('<div class="rfid-item-title"></div>');
            html.push('<div class="rfid-item-info">');
                html.push('<div class="rfid-data rfid">');
                    html.push('<div class="item-caption">RFID:</div>');
                    html.push('<div class="item-value"></div>');
                html.push('</div>');
                html.push('<div class="rfid-data message">');
                    html.push('<div class="item-caption">Status:</div>');
                    html.push('<div class="item-value"></div>');
                html.push('</div>');
            html.push('</div>');
        html.push('</div>');
        return jQuery(html.join(''));
    };

    screen.getRepairItemResponseScreen = function(response, properties) {
        jQuery('#repairItemView-ICodeBarcode').html(response.webRepairItem.masterNo + ((response.webRepairItem.itemType == "NonBarCoded") ? "" : " (BC: " + response.webRepairItem.barcode + ")"));
        jQuery('#repairItemView-master').html(response.webRepairItem.master);
        jQuery('#repairItemView-inRepair').html(response.webRepairItem.qtyInRepair);
        jQuery('#repairItemView-released').html(response.webRepairItem.qtyReleased);
        jQuery('#repairItemView-lastOrder').html(response.webRepairItem.orderNo + " - " + response.webRepairItem.orderDesc);
        jQuery('#repairItemView-deal').html(response.webRepairItem.deal);
        jQuery('#repairItemView-genericMsgValue').html(response.webRepairItem.genericMsg);
        jQuery('#repairItemView-msgValue').html(response.webRepairItem.msg);
        program.playStatus(response.webRepairItem.status == 0);

        jQuery('#repairItemView-response').show();
        jQuery('#repairItemView-info')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.master.length > 0));
        jQuery('#repairItemView-qty')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.itemType == "NonBarCoded" && properties.repairMode == "Release" && (response.webRepairItem.qtyInRepair != response.webRepairItem.qtyReleased)));
        jQuery('#repairItemView-summary')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.itemType == "NonBarCoded"));
        jQuery('#repairItemView-status')
            .toggle((applicationConfig.designMode) || ((response.webRepairItem.orderNo.length > 0) || (response.webRepairItem.deal.length > 0)));
        jQuery('#repairItemView-lastorder')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.orderNo.length > 0));
        jQuery('#repairItemView-lastdeal')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.deal.length > 0));
        jQuery('#repairItemView-genericMsg')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.genericMsg.length > 0))
            .attr('class', ((response.webRepairItem.status == 0) ? 'qssuccess' : 'qserror'))
        ;
        jQuery('#repairItemView-msg')
            .toggle((applicationConfig.designMode) || (response.webRepairItem.msg.length > 0));
        jQuery('#repairItemView-messages')
            .toggle((applicationConfig.designMode) || ((response.webRepairItem.genericMsg.length > 0) || (response.webRepairItem.msg.length > 0)));

    };

    screen.load = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('#scanBarcodeView-txtBarcodeData', true);
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_rwinventorycontrollerjs_getRepairItemScreen', function() {
                RwRFID.isConnected = true;
                screen.$view.find('.modeSelector').show();
                screen.$view.find('.btnRFID').click();
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_rwinventorycontrollerjs_getRepairItemScreen', function() {
                RwRFID.isConnected = false;
                screen.$view.find('.modeSelector').hide();
            });
        }
    };

    screen.unload = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_rwinventorycontrollerjs_getRepairItemScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_rwinventorycontrollerjs_getRepairItemScreen');
        }
    };

    return screen;
};