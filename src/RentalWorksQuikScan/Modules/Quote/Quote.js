var RwQuote = {};
//----------------------------------------------------------------------------------------------
RwQuote.getQuoteScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, contractType, captionBarcodeICode, captionOK, captionReceiveBy;
    
    combinedViewModel = jQuery.extend({
        captionPageTitle:         (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order')) + ' ' + 'No: ' + properties.orderno
      , captionPageSubTitle:      '<div class="title">' + properties.description + '</div>'
      , htmlScanBarcode:          RwPartialController.getScanBarcodeHtml({
            captionInstructions:  RwLanguages.translate('Select Item...')
          , captionBarcodeICode:  RwLanguages.translate('Bar Code / I-Code')
        })
      , captionICode:               RwLanguages.translate('I-Code')
      , CaptionHowManyHeader:       RwLanguages.translate('How many?')
      , captionDesc:                RwLanguages.translate('Desc')
      , captionQty:                 RwLanguages.translate('Quantity')
      , captionGrandTotal:          RwLanguages.translate('Grand Total')
      , captionBarcode:             RwLanguages.translate('Bar Code')
      , captionBtnItems:            RwLanguages.translate('Items')
      , captionBtnScan:             RwLanguages.translate('Scan')
      , captionAll:                 RwLanguages.translate('All')
      , captionAdminPassword:       RwLanguages.translate('Admin Password')
      , captionBack:                '<'
      , captionOK:                  RwLanguages.translate('OK')
      , captionCancel:              RwLanguages.translate('Cancel')
      , captionRate:                RwLanguages.translate('Rate')
      , captionPeriodTotal:         RwLanguages.translate('Period Total')
      , captionSubmit:              RwLanguages.translate('Submit')
      , captionRemoveItem:          RwLanguages.translate('Remove Item')
      , captionCancelQuote:         RwLanguages.translate('Cancel') + ' ' + (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order'))
      , captionYes:                 RwLanguages.translate('Yes')
      , captionNo:                  RwLanguages.translate('No')
    }, viewModel);

    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-quote').html(), combinedViewModel, {});
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    //screen.$view.find('#quote-messages').toggle(applicationConfig.designMode || false);
    //screen.$view.find('#quote-info').toggle(applicationConfig.designMode || false);

    screen.$btnback = FwMobileMasterController.addFormControl(screen, RwLanguages.translate('Back'), 'left', '&#xE5CB;', true, function() { //back
        try {
            if (sessionStorage.getItem('sessionLock') === 'true') {
                FwFunc.showMessage('Navigation is locked.');
            } else {
                program.popScreen();
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btnsubmit = FwMobileMasterController.addFormControl(screen, RwLanguages.translate('Submit'), 'right', '&#xE161;', true, function() { //save
        try {
            var request = {
                orderid: properties.orderid
            };
            if (sessionStorage.getItem('sessionLock') === 'true') {
                FwFunc.showMessage('Navigation is locked.');
            } else {
                RwServices.quote.submitQuote(request, function (response) {
                    try {
                        if (response.submit.errno != 0) {
                            FwFunc.showError(response.submit.errmsg);
                        } else {
                            var ordertypetext = (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order'));
                            FwFunc.showMessage(ordertypetext + ' Submitted.', function() {
                                program.navigate('home/home');
                            });
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });
    
    if (sessionStorage.getItem('userType') == 'USER') {
        var $moduleLock = jQuery('<div id="moduleLock"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/lock_off.001.png" alt=""></div>');
        screen.$view.find('#module-controls').append($moduleLock);
        $moduleLock.on('click', function() {
            if(sessionStorage.getItem('sessionLock') === 'true') {
                var $confirmation, $ok, $cancel;
                $confirmation = FwConfirmation.renderConfirmation('Unlock Screen Navigation', '');
                $ok           = FwConfirmation.addButton($confirmation, 'Ok', false);
                $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
                $confirmation.find('.body').css('color', '#000000');

                FwConfirmation.addControls($confirmation, '<div data-control="FwFormField" class="fwcontrol fwformfield password" data-caption="Administrator Password" data-type="password" data-datafield="" />');

                $ok.on('click', function() {
                    var request;
                    try {
                        request = {
                            email:    localStorage.getItem('email'),
                            password: FwFormField.getValue($confirmation, '.password')
                        };
                        RwServices.account.authPassword(request, function(response) {
                            if (response.errNo != 0) {
                                FwFunc.showError(response.errMsg);
                            } else {
                                sessionStorage.setItem('sessionLock', 'false');
                                $moduleLock.removeClass('on').find('img').attr('src', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/lock_off.001.png');
                                FwConfirmation.destroyConfirmation($confirmation);
                            }
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else {
                sessionStorage.setItem('sessionLock', 'true');
                $moduleLock.addClass('on').find('img').attr('src', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/lock_on.001.png');
            }
        });
    }

    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $txtBarcodeData, request;
            try {
                $txtBarcodeData = jQuery(this);
                request = {
                    orderid:     properties.orderid
                  , masterno:    RwAppData.stripBarcode($txtBarcodeData.val().toUpperCase())
                  , qty:         1
                };
                screen.scanItem(request);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#quote-items-ul > li.link', function() {
            var $this, masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby;
            try {
                $this = jQuery(this);
                masteritemid   = $this.data('masteritemid');
                rentalitemid   = $this.data('rentalitemid');
                masterno       = $this.data('masterno');
                description    = $this.data('description');
                isquantity     = ($this.data('trackedby') === 'QUANTITY');
                qtyordered     = $this.data('qtyordered');
                price          = $this.data('price');
                periodextended = $this.data('periodextended');
                barcode        = $this.data('barcode');
                masterid       = $this.data('masterid');
                if (isquantity) {
                    screen.renderPopupQty();
                    screen.updatePopupQty(masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby);
                    screen.showPopupQty();
                } else {
                    screen.renderPopupRemoveItem();
                    screen.updateRemoveItem(masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby);
                    screen.showRemoveItem();
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        
        .on('click', '#quote-pnlButtons-btnCancel', function() {
            try {
                if (sessionStorage.getItem('sessionLock') === 'true') {
                    FwFunc.showMessage('Navigation is locked.');
                } else {
                    screen.renderPopupCancelQuote();
                    screen.showCancelQuote();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.updatePopupQty = function(masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby) {
        jQuery('#quote-popupQty').data('masteritemid', masteritemid);
        jQuery('#quote-popupQty').data('rentalitemid', rentalitemid);
        jQuery('#quote-popupQty').data('masterno', masterno);
        jQuery('#quote-popupQty').data('qtyordered', qtyordered);
        jQuery('#quote-popupQty-details-description').find('.label').html(description);
        jQuery('#quote-popupQty-divDetails-tdICode').html(barcode);
        jQuery('#quote-popupQty-divDetails-tdRate').html(price);
        jQuery('#quote-txtQty').val(qtyordered);
    };

    screen.updateRemoveItem = function(masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby) {
        jQuery('#quote-popupRemoveItem').data('masteritemid', masteritemid);
        jQuery('#quote-popupRemoveItem').data('rentalitemid', rentalitemid);
        jQuery('#quote-popupRemoveItem-details-description').find('.label').html(description);
        jQuery('#quote-popupRemoveItem-divDetails-tdICode').html(barcode);
        jQuery('#quote-popupRemoveItem-divDetails-tdRate').html(price);
    };
    
    screen.showPopupQty = function() {
        FwPopup.showPopup(screen.$popupQty);
    };

    screen.hidePopupQty = function() {
        FwPopup.destroyPopup(screen.$popupQty);
        jQuery('#scanBarcodeView-txtBarcodeData').val('');
    };

    screen.showRemoveItem = function() {
        FwPopup.showPopup(screen.$popupRemoveItem);
    };

    screen.hideRemoveItem = function() {
        FwPopup.destroyPopup(screen.$popupRemoveItem);
    };

    screen.showCancelQuote = function() {
        FwPopup.showPopup(screen.$popupCancelQuote);
    };

    screen.hideCancelQuote = function() {
        FwPopup.destroyPopup(screen.$popupCancelQuote);
    };

    screen.getItems = function() {
        var request;
        jQuery('#quote-btnScan').removeClass('selected').addClass('unselected');
        jQuery('#quote-btnItems').removeClass('unselected').addClass('selected');
        request = {
            orderid: properties.orderid
        };
        RwServices.quote.loadQuoteItems(request, function(response) {
            var ul, li, masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby;
            try {
                ul = [];
                for (var i = 0; i < response.getQuoteItems.length; i++) {
                    masteritemid   = response.getQuoteItems[i].masteritemid;
                    rentalitemid   = response.getQuoteItems[i].rentalitemid;
                    masterno       = response.getQuoteItems[i].masterno;
                    description    = response.getQuoteItems[i].description;
                    qtyordered     = response.getQuoteItems[i].qtyordered;
                    price          = parseFloat(response.getQuoteItems[i].price).toFixed(2);
                    periodextended = parseFloat(response.getQuoteItems[i].periodextended).toFixed(2);
                    barcode        = response.getQuoteItems[i].barcode;
                    masterid       = response.getQuoteItems[i].masterid;
                    trackedby      = response.getQuoteItems[i].trackedby;
                    li = [];
                    li = screen.buildLi(masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby);
                    ul.push(li.join(''));
                }
                if (response.getQuoteItems.length === 0) {
                    li = [];
                    li = screen.getEmptyListItem();
                    ul.push(li.join(''));
                }
                jQuery('#quote-items-ul').html(ul.join(''));
                screen.calculateGrandTotal();
                jQuery('#quote-items').show();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.scanItem = function(request) {
        try {
            RwServices.quote.addItem(request, function(response) {
                var li, masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby, errno, errmsg;
                try {
                    errmsg         = response.insert.errmsg;
                    errno          = response.insert.errno;
                    if (errno != 0) {
                        FwFunc.showMessage(errmsg);
                    }
                    screen.getItems();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        } catch(ex) {
            FwFunc.showError(ex);
        }
    };

    screen.removeItem = function(masteritemid, rentalitemid, qtyRemoved) {
        var request = {
            orderid:      properties.orderid,
            masteritemid: masteritemid,
            rentalitemid: rentalitemid,
            qtyremoved:   qtyRemoved
        };
        RwServices.quote.deleteItem(request, function(response) {
            try {
                if (response.deleteitem.errno != 0) {
                    FwFunc.showError(response.deleteitem.errmsg);
                }
                screen.getItems();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.updateItem = function (masterItemId, masterNo, qtyValue) {
        request = {
            orderid:     properties.orderid
          , masterno:    masterNo
          , qty:         qtyValue
          , masteritemid: masterItemId
        };
        RwServices.quote.updateItemQty(request, function(response) {
            var masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby, errno, errmsg;
            try {
                errmsg         = response.update.errmsg;
                errno          = response.update.errno;
                if (errno != 0) {
                    FwFunc.showError(errmsg);
                }
                screen.getItems();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.buildLi = function (masteritemid, rentalitemid, masterno, description, qtyordered, price, periodextended, barcode, masterid, trackedby) {
        var li;

        li = [];
        li.push('<li class="link" ' +
                      'data-masteritemid="'   + masteritemid +
                    '" data-rentalitemid="'   + rentalitemid +
                    '" data-masterno="'       + masterno +
                    '" data-description="'    + description +
                    '" data-qtyordered="'     + qtyordered +
                    '" data-price="'          + price +
                    '" data-periodextended="' + periodextended +
                    '" data-barcode="'        + barcode +
                    '" data-masterid="'       + masterid +
                    '" data-trackedby="'      + trackedby + '">');
            li.push('<div class="row1">');
                li.push('<div class="description">' + description + '</div>');
            li.push('</div>');
            li.push('<div class="row2">');
                li.push('<div class="icode-caption">'       + RwLanguages.translate('I-Code')       + ':</div>');
                li.push('<div class="icode-value">'         + masterno                              + '</div>');
                if (trackedby != 'QUANTITY') {
                    li.push('<div class="barcode-caption">' + RwLanguages.translate('Bar Code')     + ':</div>');
                    li.push('<div class="barcode-value">'   + barcode                               + '</div>');
                }
            li.push('</div>');
            li.push('<div class="row3">');
                li.push('<div class="qty-caption">'      + RwLanguages.translate('Qty')          + ':</div>');
                li.push('<div class="qty-value">'        + qtyordered                            + '</div>');
                li.push('<div class="rate-caption">'     + RwLanguages.translate('Rate')         + ':</div>');
                li.push('<div class="rate-value">'       + numberWithCommas(price)               + '</div>');
                li.push('<div class="period-caption">'   + RwLanguages.translate('Period Total') + ':</div>');
                li.push('<div class="period-value">'     + numberWithCommas(periodextended)      + '</div>');
            li.push('</div>');
        li.push('</li>');

        return li
    };

    screen.getEmptyListItem = function() {
        var li;
        li = [];
        li.push('<li class="normal">');
            li.push('<div class="empty">' + RwLanguages.translate('0 items found') + '</div>');
        li.push('</li>');
        return li;
    };

    screen.calculateGrandTotal = function() {
        var grandtotal, $items, itemperiodextended;
        grandtotal     = 0;
        $items         = [];
        $items         = jQuery('#quote-items-ul').find('li');
        for (var i = 0; i < $items.length; i++) {
            itemperiodextended = Number(jQuery($items[i]).data('periodextended'));
            if (!isNaN(itemperiodextended)) {
                grandtotal = grandtotal + itemperiodextended;
            }
        }
        grandtotal = parseFloat(grandtotal).toFixed(2);
        jQuery('#quote-grandtotal-value').html(numberWithCommas(grandtotal)).attr('data-grandtotal', grandtotal);
    };

    screen.renderPopupCancelQuote = function() {
        var template = Mustache.render(jQuery('#tmpl-Quote-PopupCancelQuote').html(), {
              captionICode:               RwLanguages.translate('I-Code')
            , CaptionHowManyHeader:       RwLanguages.translate('How many?')
            , captionDesc:                RwLanguages.translate('Desc')
            , captionQty:                 RwLanguages.translate('Quantity')
            , captionGrandTotal:          RwLanguages.translate('Grand Total')
            , captionBarcode:             RwLanguages.translate('Bar Code')
            , captionBtnItems:            RwLanguages.translate('Items')
            , captionBtnScan:             RwLanguages.translate('Scan')
            , captionAll:                 RwLanguages.translate('All')
            , captionAdminPassword:       RwLanguages.translate('Admin Password')
            , captionBack:                '<'
            , captionOK:                  RwLanguages.translate('OK')
            , captionCancel:              RwLanguages.translate('Cancel')
            , captionRate:                RwLanguages.translate('Rate')
            , captionPeriodTotal:         RwLanguages.translate('Period Total')
            , captionSubmit:              RwLanguages.translate('Submit')
            , captionRemoveItem:          RwLanguages.translate('Remove Item')
            , captionCancelQuote:         RwLanguages.translate('Cancel') + ' ' + (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order'))
            , captionYes:                 RwLanguages.translate('Yes')
            , captionNo:                  RwLanguages.translate('No')
        });
        var $popupcontent = jQuery(template);
        $popupcontent.find('#quote-popupCancelQuote-messages').hide();
        screen.$popupCancelQuote = FwPopup.renderPopup($popupcontent, {ismodal:false});
        FwPopup.showPopup(screen.$popupCancelQuote);
        screen.$popupCancelQuote
            .on('click', '#quote-popupCancelQuote-btnYes', function() {
                var request;
                try {
                    request = {
                        orderid: properties.orderid
                    };
                    RwServices.quote.cancelQuote(request, function (response) {
                        try {
                            if (response.cancel.errno != 0) {
                                FwFunc.showError(response.cancel.errmsg);
                            } else {
                                program.navigate('home/home');
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#quote-popupCancelQuote-btnNo', function() {
                try {
                    screen.hideCancelQuote();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    };

    screen.renderPopupQty = function() {
        var template = Mustache.render(jQuery('#tmpl-Quote-PopupQty').html(), {
            captionICode:               RwLanguages.translate('I-Code')
            , CaptionHowManyHeader:       RwLanguages.translate('How many?')
            , captionDesc:                RwLanguages.translate('Desc')
            , captionQty:                 RwLanguages.translate('Quantity')
            , captionGrandTotal:          RwLanguages.translate('Grand Total')
            , captionBarcode:             RwLanguages.translate('Bar Code')
            , captionBtnItems:            RwLanguages.translate('Items')
            , captionBtnScan:             RwLanguages.translate('Scan')
            , captionAll:                 RwLanguages.translate('All')
            , captionAdminPassword:       RwLanguages.translate('Admin Password')
            , captionBack:                '<'
            , captionOK:                  RwLanguages.translate('OK')
            , captionCancel:              RwLanguages.translate('Cancel')
            , captionRate:                RwLanguages.translate('Rate')
            , captionPeriodTotal:         RwLanguages.translate('Period Total')
            , captionSubmit:              RwLanguages.translate('Submit')
            , captionRemoveItem:          RwLanguages.translate('Remove Item')
            , captionCancelQuote:         RwLanguages.translate('Cancel') + ' ' + (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order'))
            , captionYes:                 RwLanguages.translate('Yes')
            , captionNo:                  RwLanguages.translate('No')
        });
        var $popupcontent = jQuery(template);
        $popupcontent.find('#quote-popupQty-messages').hide();
        screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
        FwPopup.showPopup(screen.$popupQty);
        screen.$popupQty
            .on('change', '#quote-txtQty', function() {
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
            .on('click', '#quote-btnOK', function() {
                var request, qtyOrdered, qtyValue, masterItemId, rentalItemId, masterNo;
                try {
                    masterItemId = String(jQuery('#quote-popupQty').data('masteritemid'));
                    rentalItemId = '';
                    masterNo     = String(jQuery('#quote-popupQty').data('masterno'));
                    qtyOrdered   = Number(jQuery('#quote-popupQty').data('qtyordered'));
                    qtyValue     = Number(jQuery('#quote-txtQty').val());
                    //if ((qtyOrdered != qtyValue) && (qtyValue > 0)) {
                    if (qtyOrdered < qtyValue) {
                        screen.updateItem(masterItemId, masterNo, (qtyValue - qtyOrdered));
                    } else if (qtyOrdered > qtyValue) {
                        screen.removeItem(masterItemId, rentalItemId, (qtyOrdered - qtyValue));
                    }
                    screen.hidePopupQty();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#quote-btnCancel', function() {
                try {
                    screen.hidePopupQty();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#quote-btnSubtract', function() {
                var quantity;
                try {
                    quantity = Number(jQuery('#quote-txtQty').val()) - 1;
                    if (quantity >= 0) {
                        jQuery('#quote-txtQty').val(quantity);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#quote-btnAdd', function() {
                var quantity;
                try {
                    quantity = Number(jQuery('#quote-txtQty').val()) + 1;
                    jQuery('#quote-txtQty').val(quantity);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    };

    screen.renderPopupRemoveItem = function() {
        var template = Mustache.render(jQuery('#tmpl-Quote-PopupRemoveItem').html(), {
            captionICode:               RwLanguages.translate('I-Code')
            , CaptionHowManyHeader:       RwLanguages.translate('How many?')
            , captionDesc:                RwLanguages.translate('Desc')
            , captionQty:                 RwLanguages.translate('Quantity')
            , captionGrandTotal:          RwLanguages.translate('Grand Total')
            , captionBarcode:             RwLanguages.translate('Bar Code')
            , captionBtnItems:            RwLanguages.translate('Items')
            , captionBtnScan:             RwLanguages.translate('Scan')
            , captionAll:                 RwLanguages.translate('All')
            , captionAdminPassword:       RwLanguages.translate('Admin Password')
            , captionBack:                '<'
            , captionOK:                  RwLanguages.translate('OK')
            , captionCancel:              RwLanguages.translate('Cancel')
            , captionRate:                RwLanguages.translate('Rate')
            , captionPeriodTotal:         RwLanguages.translate('Period Total')
            , captionSubmit:              RwLanguages.translate('Submit')
            , captionRemoveItem:          RwLanguages.translate('Remove Item')
            , captionCancelQuote:         RwLanguages.translate('Cancel') + ' ' + (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order'))
            , captionYes:                 RwLanguages.translate('Yes')
            , captionNo:                  RwLanguages.translate('No')
        });
        var $popupcontent = jQuery(template);
        $popupcontent.find('#quote-popupRemoveItem-messages').hide();
        screen.$popupRemoveItem = FwPopup.renderPopup($popupcontent, {ismodal:false});
        FwPopup.showPopup(screen.$popupRemoveItem);
        screen.$popupRemoveItem
            .on('click', '#quote-popupRemoveItem-btnRemove', function() {
                var masteritemid, rentalitemid;
                try {
                    masteritemid = jQuery('#quote-popupRemoveItem').data('masteritemid');
                    rentalitemid = jQuery('#quote-popupRemoveItem').data('rentalitemid');
                    screen.removeItem(masteritemid, rentalitemid, 1);
                    screen.hideRemoveItem();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#quote-popupRemoveItem-btnCancel', function() {
                try {
                    screen.hideRemoveItem();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    };

    screen.load = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('#scanBarcodeView-txtBarcodeData', true);
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
        screen.getItems();
    };

    screen.unload = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');
    };
    
    return screen;
};