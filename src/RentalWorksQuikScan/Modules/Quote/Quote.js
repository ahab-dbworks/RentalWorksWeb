var RwQuote = {};
//----------------------------------------------------------------------------------------------
RwQuote.getQuoteScreen = function(viewModel, properties) {
    var locationdata = null, moduleproperties = null;
    var combinedViewModel = jQuery.extend({
        captionPageTitle:         (properties.ordertype == 'Q' ? RwLanguages.translate('Quote') : RwLanguages.translate('Order')) + ' ' + 'No: ' + properties.orderno,
        captionPageSubTitle:      '<div class="title">' + properties.description + '</div>',
        captionGrandTotal:        RwLanguages.translate('Grand Total'),
        htmlScanBarcode:          RwPartialController.getScanBarcodeHtml({
            captionInstructions:  RwLanguages.translate('Select Item...'),
            captionBarcodeICode:  RwLanguages.translate('Bar Code / I-Code')
        })
    }, viewModel);

    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-quote').html(), combinedViewModel, {});
    var screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);

    if (sessionStorage.getItem('userType') == 'USER') {
        var $moduleLock = jQuery('<div id="moduleLock"><i class="material-icons">lock_open</i></div>');
        screen.$view.find('#module-controls').append($moduleLock);
        $moduleLock.on('click', function() {
            if (sessionStorage.getItem('sessionLock') === 'true') {
                var $confirmation = FwConfirmation.renderConfirmation('Unlock Screen Navigation', '');
                var $ok           = FwConfirmation.addButton($confirmation, 'Ok', false);
                var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
                $confirmation.find('.body').css('color', '#000000');

                FwConfirmation.addControls($confirmation, '<div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Administrator Password" data-type="password" data-datafield="Password" />');

                $ok.on('click', function() {
                    try {
                        var request = {
                            email:    localStorage.getItem('email'),
                            password: FwFormField.getValue($confirmation, 'div[data-datafield="Password"]')
                        };
                        RwServices.account.authPassword(request, function(response) {
                            if (response.errNo != 0) {
                                FwFunc.showError(response.errMsg);
                            } else {
                                sessionStorage.setItem('sessionLock', 'false');
                                $moduleLock.html('<i class="material-icons">lock_open</i>');
                                FwConfirmation.destroyConfirmation($confirmation);
                            }
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else {
                sessionStorage.setItem('sessionLock', 'true');
                $moduleLock.html('<i class="material-icons">lock</i>');
            }
        });
    }

    var $quotemain = screen.$view.find('#quote-main');
    $quotemain.find('#quotecontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    if (sessionStorage.getItem('sessionLock') === 'true') {
                        FwFunc.showMessage('Navigation is locked.');
                    } else {
                        program.popScreen();
                    }
                }
            },
            {
                id:          'itemlist_menu',
                type:        'menu',
                orientation: 'right',
                icon:        '&#xE5D4;', //more_vert
                state:       0,
                menuoptions: [
                    {
                        id:      'submitcreatecontract',
                        caption: 'Submit & Create Contract',
                        buttonclick: function () {
                            var request = {
                                orderid:        properties.orderid,
                                createcontract: 'T'
                            };
                            if (sessionStorage.getItem('sessionLock') === 'true') {
                                FwFunc.showMessage('Navigation is locked.');
                            } else {
                                RwServices.callMethod("Quote", "SubmitQuote", request, function(response) {
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
                        }
                    },
                    {
                        id:      'cancel',
                        caption: 'Cancel' + ((properties.ordertype === 'Q') ? ' Quote' : ' Order'),
                        buttonclick: function() {
                            if (sessionStorage.getItem('sessionLock') === 'true') {
                                FwFunc.showMessage('Navigation is locked.');
                            } else {
                                var $confirmation = FwConfirmation.renderConfirmation('Cancel' + ((properties.ordertype === 'Q') ? ' Quote' : ' Order'), '');
                                var $yes          = FwConfirmation.addButton($confirmation, 'Yes', false);
                                var $no           = FwConfirmation.addButton($confirmation, 'No', true);

                                $yes.on('click', function () {
                                    var request = {
                                        orderid: properties.orderid
                                    };
                                    RwServices.callMethod("Quote", "CancelQuote", request, function(response) {
                                        try {
                                            if (response.cancel.errno != 0) {
                                                FwFunc.showError(response.cancel.errmsg);
                                            } else {
                                                FwConfirmation.destroyConfirmation($confirmation);
                                                program.navigate('home/home');
                                            }
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                });
                            }
                        }
                    },
                    {
                        id:      'searchdescription',
                        caption: 'Search By Description',
                        buttonclick: function() {
                            $quotemain.hide();
                            $itemsearch.showscreen();
                        }
                    },
                    {
                        id:      'selectorderlocation',
                        caption: 'Select Order Location',
                        buttonclick: function() {
                            $quotemain.hide();
                            $orderlocation.showscreen();
                        }
                    }
                ]
            },
            {
                caption:     'Submit',
                orientation: 'right',
                icon:        '&#xE161;', //save
                state:       0,
                buttonclick: function () {
                    var request = {
                        orderid:        properties.orderid,
                        createcontract: 'F'
                    };
                    if (sessionStorage.getItem('sessionLock') === 'true') {
                        FwFunc.showMessage('Navigation is locked.');
                    } else {
                        RwServices.callMethod("Quote", "SubmitQuote", request, function(response) {
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
                }
            }
        ]
    });
    $quotemain.find('#item-list').fwmobilesearch({
        service: 'Quote',
        method:  'LoadItems',
        getRequest: function() {
            var request = {
                orderid: properties.orderid
            };
            return request;
        },
        cacheItemTemplate: false,
        itemTemplate: function(model) {
            var html = [], masterclass;
            masterclass = 'item link itemclass-' + model.itemclass;
            html.push('<div class="' + masterclass + '">');
            html.push('  <div class="row1"><div class="title">{{description}}</div></div>');
            html.push('  <div class="row2">');
            html.push('    <div class="col1">');
            html.push('      <div class="datafield masterno">');
            html.push('        <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
            html.push('        <div class="value">{{masterno}}</div>');
            html.push('      </div>');
            if (model.trackedby === 'BARCODE' || model.trackedby === 'SERIALNO' || model.trackedby === 'RFID') {
                html.push('      <div class="datafield barcode">');
                html.push('        <div class="caption">' + RwLanguages.translate('Barcode') + ':</div>');
                html.push('        <div class="value">{{barcode}}</div>');
                html.push('      </div>');
            }
            html.push('    </div>');
            html.push('    <div class="col2">');
            html.push('      <div class="datafield qtyordered">');
            html.push('        <div class="caption">Qty:</div>');
            html.push('        <div class="value">{{qtyordered}}</div>');
            html.push('      </div>');
            html.push('      <div class="datafield rate">');
            html.push('        <div class="caption">' + RwLanguages.translate('Rate') + ':</div>');
            html.push('        <div class="value">' + numberWithCommas(parseFloat(model.price).toFixed(2)) + '</div>');
            html.push('      </div>');
            html.push('      <div class="datafield periodextended">');
            html.push('        <div class="caption">' + RwLanguages.translate('Period Total') + ':</div>');
            html.push('        <div class="value">' + numberWithCommas(parseFloat(model.periodextended).toFixed(2)) + '</div>');
            html.push('      </div>');
            html.push('    </div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('\n');
            return html;
        },
        recordClick: function(recorddata, $record) {
            try {
                if (recorddata.trackedby === 'QUANTITY') {
                    screen.promptUpdateItem(recorddata);
                } else {
                    screen.promptRemoveItem(recorddata);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        },
        afterLoad: function(plugin, response) {
            $quotemain.find('#quote-grandtotal-value').html(numberWithCommas(parseFloat(response.grandtotal).toFixed(2))).attr('data-grandtotal', response.grandtotal);
        }
    });
    $quotemain
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            try {
                var $txtBarcodeData = jQuery(this);
                var value           = RwAppData.stripBarcode($txtBarcodeData.val().toUpperCase());
                if (value !== '') {
                    var request = {
                        orderid:      properties.orderid,
                        enteredvalue: value,
                        locationdata: screen._locationdata()
                    };
                    RwServices.callMethod("Quote", "ScanItem", request, function(response) {
                        try {
                            if (response.iteminfo.trackedby === 'QUANTITY') {
                                screen.promptAddItem(response.iteminfo);
                            } else if (response.insert.errno != 0) {
                                FwFunc.showMessage(response.insert.errmsg);
                            } else {
                                $txtBarcodeData.val('');
                                $quotemain.find('#item-list').fwmobilesearch('search');
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.promptAddItem = function (iteminfo) {
        var $confirmation = FwConfirmation.renderConfirmation('How Many?', '');
        var $ok           = FwConfirmation.addButton($confirmation, 'OK', true);
        var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

        var html = [];
        html.push('<div class="item-info" style="background-color: #212121;color: #BDBDBD;padding: 5px;font-size: .8em;border-radius: 4px;">');
        html.push('  <div class="row" style="color: #2196F3;text-decoration: underline;">' + iteminfo.description + '</div>');
        html.push('  <div class="row">I-Code: ' + iteminfo.masterNo + '</div>');
        html.push('</div>');
        html.push('<div data-caption="Quantity" data-datafield="Quantity" data-control="FwFormField" data-type="number" class="fwcontrol fwformfield"></div>')
        FwConfirmation.addControls($confirmation, html.join(''));

        FwFormField.setValue($confirmation, 'div[data-datafield="Quantity"]', '1');

        $ok.on('click', function () {
            var qtyValue = Number(FwFormField.getValue($confirmation, 'div[data-datafield="Quantity"]'));
            if (qtyValue != 0) {
                var request = {
                    orderid:      properties.orderid,
                    masterno:     iteminfo.masterNo,
                    qty:          qtyValue,
                    locationdata: screen._locationdata()
                };
                RwServices.callMethod("Quote", "AddItem", request, function(response) {
                    try {
                        if (response.insert.errno != 0) {
                            FwFunc.showError(response.update.errmsg);
                        } else {
                            $quotemain.find('#scanBarcodeView-txtBarcodeData').val('');
                            $quotemain.find('#item-list').fwmobilesearch('search');
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        });
    };

    screen.promptRemoveItem = function (recorddata) {
        var $confirmation = FwConfirmation.renderConfirmation('Remove Item?', '');
        var $ok           = FwConfirmation.addButton($confirmation, 'OK', true);
        var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

        var html = [];
        html.push('<div class="item-info" style="background-color: #212121;color: #BDBDBD;padding: 5px;font-size: .8em;border-radius: 4px;">');
        html.push('  <div class="row" style="color: #2196F3;text-decoration: underline;">' + recorddata.description + '</div>');
        html.push('  <div class="row">Barcode: ' + recorddata.barcode + '</div>');
        html.push('  <div class="row">Price: ' + recorddata.price + '</div>');
        html.push('</div>');
        FwConfirmation.addControls($confirmation, html.join(''));

        $ok.on('click', function () {
            screen.removeItem(recorddata.masteritemid, recorddata.rentalitemid, 1);
        });
    };

    screen.promptUpdateItem = function (recorddata) {
        var $confirmation = FwConfirmation.renderConfirmation('Update Quantity?', '');
        var $ok           = FwConfirmation.addButton($confirmation, 'OK', true);
        var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

        var html = [];
        html.push('<div class="item-info" style="background-color: #212121;color: #BDBDBD;padding: 5px;font-size: .8em;border-radius: 4px;">');
        html.push('  <div class="row" style="color: #2196F3;text-decoration: underline;">' + recorddata.description + '</div>');
        html.push('  <div class="row">I-Code: ' + recorddata.masterno + '</div>');
        html.push('  <div class="row">Price: ' + recorddata.price + '</div>');
        html.push('</div>');
        html.push('<div data-caption="Quantity" data-datafield="Quantity" data-control="FwFormField" data-type="number" class="fwcontrol fwformfield"></div>')
        FwConfirmation.addControls($confirmation, html.join(''));

        FwFormField.setValue($confirmation, 'div[data-datafield="Quantity"]', recorddata.qtyordered);

        $ok.on('click', function () {
            var qtyValue = FwFormField.getValue($confirmation, 'div[data-datafield="Quantity"]');
            if (recorddata.qtyordered < qtyValue) {
                screen.updateItem(recorddata.masteritemid, recorddata.masterno, (qtyValue - recorddata.qtyordered));
            } else if (recorddata.qtyordered > qtyValue) {
                screen.removeItem(recorddata.masteritemid, recorddata.rentalitemid, (recorddata.qtyordered - qtyValue));
            }
        });
    };

    screen.removeItem = function(masteritemid, rentalitemid, qtyRemoved) {
        var request = {
            orderid:      properties.orderid,
            masteritemid: masteritemid,
            rentalitemid: rentalitemid,
            qtyremoved:   qtyRemoved
        };
        RwServices.callMethod("Quote", "DeleteItem", request, function(response) {
            try {
                if (response.deleteitem.errno != 0) {
                    FwFunc.showError(response.deleteitem.errmsg);
                }
                $quotemain.find('#item-list').fwmobilesearch('search');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.updateItem = function (masterItemId, masterNo, qtyValue) {
        var request = {
            orderid:      properties.orderid,
            masterno:     masterNo,
            qty:          qtyValue,
            masteritemid: masterItemId,
            locationdata: screen._locationdata()
        };
        RwServices.callMethod("Quote", "UpdateItem", request, function(response) {
            try {
                if (response.update.errno != 0) {
                    FwFunc.showError(response.update.errmsg);
                }
                $quotemain.find('#item-list').fwmobilesearch('search');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen._locationdata = function(locationData) {
        if (locationData) {
            locationdata = locationData;
        } else {
            return locationdata;
        }
    };

    var $itemsearch = screen.$view.find('#item-search-by-description');
    $itemsearch.find('#item-search-control').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    $itemsearch.hide();
                    $quotemain.show();
                }
            },
            {
                caption:     'Select',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       0,
                buttonclick: function () {
                    var itemdata = $itemsearch.find('.item.selected').data('recorddata');
                    if (itemdata != null) {
                        $itemsearch.hide();
                        $quotemain.show();
                        screen.promptAddItem({description: itemdata.master, masterNo: itemdata.masterno});
                    }
                }
            }
        ]
    });
    $itemsearch
        .on('change', '.fwmobilecontrol-value', function () {
            var value = jQuery(this).val();
            if (value != '') {
                var request = {
                    searchvalue: value,
                    warehouseid: properties.warehouseid
                };
                RwServices.callMethod("Quote", "SearchItems", request, function(response) {
                    $itemsearch.find('.item-search-items').empty();
                    if (response.items.length > 0) {
                        for (var item of response.items) {
                            var html = [];
                            html.push('<div class="item">');
                            html.push('  <div class="row1"><div class="title">' + item.master + '</div></div>');
                            html.push('  <div class="row2">');
                            html.push('    <div class="col1">');
                            html.push('      <div class="datafield masterno">');
                            html.push('        <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                            html.push('        <div class="value">' + item.masterno + '</div>');
                            html.push('      </div>');
                            html.push('    </div>');
                            html.push('    <div class="col2">');
                            html.push('      <div class="datafield rate">');
                            html.push('        <div class="caption">' + RwLanguages.translate('Rate') + ':</div>');
                            html.push('        <div class="value">' + numberWithCommas(parseFloat(item.price).toFixed(2)) + '</div>');
                            html.push('      </div>');
                            html.push('    </div>');
                            html.push('  </div>');
                            html.push('</div>');
                            var $item = jQuery(html.join(''));
                            $item.data('recorddata', item);

                            $itemsearch.find('.item-search-items').append($item);
                        }
                    } else {
                        var $zeroitems = jQuery('<div class="zeroitems">0 Items Found</div>');
                        $itemsearch.find('.item-search-items').append($zeroitems);
                    }
                });
            }
        })
        .on('click', '.item', function () {
            var $this = jQuery(this);
            $this.siblings().removeClass('selected');
            $this.addClass('selected');
        })
    ;
    $itemsearch.showscreen = function () {
        $itemsearch.find('.fwmobilecontrol-value').val('');
        $itemsearch.find('.item-search-items').empty();
        $itemsearch.show();
    }

    const $orderlocation = screen.$view.find('#quote-orderlocation');
    $orderlocation.find('#orderlocationcontroller').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    $orderlocation.hide();
                    $quotemain.show();
                }
            },
            {
                caption:     'Select Location',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       0,
                buttonclick: function () {
                    var locationdata = $orderlocation.find('.location.selected').data('recorddata');
                    if (locationdata != null) {
                        screen._locationdata(locationdata);
                        $orderlocation.hide();
                        $quotemain.show();
                    } else {
                        FwNotification.renderNotification('ERROR', 'Select a location.')
                    }
                }
            }
        ]
    });
    $orderlocation.showscreen = function () {
        $orderlocation.show();
        $orderlocation.searchlocation('');
    };
    $orderlocation.searchlocation = function (searchvalue) {
        var request = {
            orderid:     properties.orderid,
            searchvalue: searchvalue
        };
        RwServices.callMethod("Quote", "SearchLocations", request, function(response) {
            $orderlocation.find('.orderlocations').empty();
            if (response.locations.length > 0) {
                for (var item of response.locations) {
                    var html = [];
                    html.push('<div class="location">');
                    html.push(`  <div class="row1"><div class="title">${item.location}</div></div>`);
                    html.push('  <div class="row2">');
                    html.push('    <div class="col1">');
                    html.push('      <div class="datafield masterno">');
                    html.push('        <div class="caption">Building:</div>');
                    html.push(`        <div class="value">${item.building}</div>`);
                    html.push('      </div>');
                    html.push('    </div>');
                    html.push('    <div class="col2">');
                    html.push('      <div class="datafield rate">');
                    html.push('        <div class="caption">Floor:</div>');
                    html.push(`        <div class="value">${item.floor}</div>`);
                    html.push('      </div>');
                    html.push('    </div>');
                    html.push('  </div>');
                    html.push('  <div class="row3">');
                    html.push('    <div class="col1">');
                    html.push('      <div class="datafield masterno">');
                    html.push('        <div class="caption">Space:</div>');
                    html.push(`        <div class="value">${item.space}</div>`);
                    html.push('      </div>');
                    html.push('    </div>');
                    html.push('  </div>');
                    html.push('</div>');
                    var $item = jQuery(html.join(''));
                    $item.data('recorddata', item);

                    $orderlocation.find('.orderlocations').append($item);
                }
            } else {
                var $zeroitems = jQuery('<div class="zeroitems">0 Locations Found</div>');
                $orderlocation.find('.orderlocations').append($zeroitems);
            }
        });
    };
    $orderlocation
        .on('change', '.fwmobilecontrol-value', function () {
            var value = jQuery(this).val();
            if (value != '') {
                $orderlocation.searchlocation(value);
            }
        })
        .on('click', '.location', function () {
            var $this = jQuery(this);
            $this.siblings().removeClass('selected');
            $this.addClass('selected');
        })
    ;

    screen.load = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('#scanBarcodeView-txtBarcodeData', true);
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }

        RwServices.callMethod("Quote", "LoadModuleProperties", {}, function(response) {
            moduleproperties = response;
            var showhideselectorder = (moduleproperties.syscontrol.itemsinrooms == "T") ? 'showButton' : 'hideButton';
            $quotemain.find('#quotecontrol').fwmobilemodulecontrol(showhideselectorder, '#selectorderlocation');
        });

        $quotemain.find('#item-list').fwmobilesearch('search');
    };

    screen.unload = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');
    };

    return screen;
};