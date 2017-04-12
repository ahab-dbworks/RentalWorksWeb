//----------------------------------------------------------------------------------------------
RwInventoryController.getQCScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('QC'),
        captionBarCode:     RwLanguages.translate('Bar Code'),
        captionICodeDesc:   RwLanguages.translate('I-Code'),
        captionCondition:   RwLanguages.translate('Condition'),
        captionNote:        RwLanguages.translate('Note'),
        captionCompleteQC:  RwLanguages.translate('Complete QC'),
        captionPictures:    RwLanguages.translate('Pictures'),
        captionTakePicture: RwLanguages.translate('Take Picture'),
        htmlScanBarcode:    RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Bar Code')}),
        captionScanButton:   RwLanguages.translate('Scan'),
        captionRFIDButton:   RwLanguages.translate('RFID')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-QC').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.$view.find('#qc-response').hide();
    screen.$view.find('#qc').data('scanmode', 'SCAN');

    properties.isconditionsloaded = 'F';

    if (RwRFID.isConnected) {
        screen.$view.find('.modeSelector').show();
    }

    screen.getCompleteQCItemCallback = function(response) {
        var calledGetItemStatus, calledCompletedQC, noCompleteQCErrors;
        
        noCompleteQCErrors    = true;
        noGetItemStatusErrors = true;
        calledGetItemStatus   = (typeof response.webGetItemStatus === 'object');
        calledCompleteQC      = (typeof response.webCompleteQCItem === 'object');
        
        // show any error messages if completeqc was run during the request
        if (calledCompleteQC) {
            noCompleteQCErrors = (response.webCompleteQCItem.status === 0);
            jQuery('#qc-barcode').html(response.request.code);
            jQuery('#qc-masterNo').html(response.webCompleteQCItem.masterNo);
            jQuery('#qc-description').html(response.webCompleteQCItem.description);
            jQuery('#qc-txtGenericError').html((noCompleteQCErrors) ? 'Successs: Item QC\'d' : 'Error');
            jQuery('#qc-txtMsg').html(response.webCompleteQCItem.msg);
            jQuery('#qc-msg')
                .toggle(true)
                .attr('class', ((noCompleteQCErrors) ? 'panel qssuccess' : 'panel qserror'))
            ;
            jQuery('#qc-rentalConditions').val(response.getRentalItemCondition.conditionid);
            jQuery('#qc-message').toggle(true);
            properties.webCompleteQCItem = response.webCompleteQCItem;
        }
        else
        {
            delete properties.webCompleteQCItem;
        }
        // if getItemStatus was called, then show the conditions, notes, and button to take images
        if (calledGetItemStatus && ((!calledCompleteQC) || (calledCompleteQC && (noCompleteQCErrors)))) {
            noGetItemStatusErrors = (response.webGetItemStatus.status === 0);
            if (response.webGetItemStatus.isICode) {
                response.webGetItemStatus.status = 1;
                response.webGetItemStatus.genericError = 'INVALID BC';
                response.webGetItemStatus.msg = 'You need to use a BC No not an I-Code No here.';
            }
            jQuery('#qc-masterNo').html(response.webGetItemStatus.masterNo);
            jQuery('#qc-description').html(response.webGetItemStatus.description);
            if (!calledCompleteQC) {
                jQuery('#qc-txtGenericError').html(response.webGetItemStatus.genericError.replace(' / I-CODE',''));
                jQuery('#qc-txtMsg').html(response.webGetItemStatus.msg.replace('/I-Code',''));
                jQuery('#qc-msg')
                    .toggle((applicationConfig.designMode) || (calledCompleteQC) || (!noGetItemStatusErrors))
                    .attr('class', (noGetItemStatusErrors) ? 'qssuccess' : 'qserror')
                ;
            }
            jQuery('#qc-rentalConditions').val('');
            jQuery('#qc-txtNotes').val('');
            jQuery('#qc-images').empty();
            jQuery('#qc-pnlPictures').toggle(typeof navigator.camera !== 'undefined');
            jQuery('#qc-complete').toggle((applicationConfig.designMode) || (noGetItemStatusErrors));
        }

        application.playStatus(noCompleteQCErrors && noGetItemStatusErrors);
        jQuery('#qc-response').show();
    };
    
    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                jQuery('#qc-response').hide();
                jQuery('#qc-msg').hide();
                jQuery('#qc-complete').hide();
                request = {
                    action:             'scanbarcode',
                    code:               RwAppData.stripBarcode(jQuery('#scanBarcodeView-txtBarcodeData').val().toUpperCase()),
                    isconditionsloaded: properties.isconditionsloaded
                };
                if ((request.code.length > 0) && (screen.$view.find('#qc').data('scanmode') == 'SCAN')) {
                    RwServices.order.completeQCItem(request, function(response) {
                        try {
                            if (request.isconditionsloaded == 'F') {
                                var select = [];
                                properties.isconditionsloaded = 'T';
                                properties.conditions         = response.rentalconditions;
                                select.push('<option value="">-</option>');
                                for (var i = 0; i < properties.conditions.Rows.length; i++) {
                                    select.push('<option value="' + properties.conditions.Rows[i][properties.conditions.ColumnIndex.conditionid] + '">' + properties.conditions.Rows[i][properties.conditions.ColumnIndex.condition] + '</option>');
                                }
                                jQuery('#qc-rentalConditions').html(select.join(''));
                            }
                            screen.getCompleteQCItemCallback(response);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#qc-btnCompleteQC', function() {
            var $this, request, images, webCompleteQCItem;
            try {
                $this = jQuery(this);
                jQuery('#qc-response').hide();
                jQuery('#qc-message').hide();
                jQuery('#qc-complete').hide();
                $images = jQuery('#qc-images > img');
                request = {
                    action:             'completeqc',
                    code:               RwAppData.stripBarcode(jQuery('#scanBarcodeView-txtBarcodeData').val().toUpperCase()),
                    conditionid:        jQuery('#qc-rentalConditions').val(),
                    note:               jQuery('#qc-txtNotes').val(),
                    images:             [],
                    isconditionsloaded: properties.isconditionsloaded
                };
                if (typeof properties.webCompleteQCItem === 'object') {
                    request.webCompleteQCItem = properties.webCompleteQCItem;
                }
                for (var i = 0; i < $images.length; i++) {
                    request.images.push($images.eq(i).data('base64'));
                }
                RwServices.order.completeQCItem(request, function(response) {
                    try {
                        if (request.isconditionsloaded == 'F') {
                            var select = [];
                            properties.isconditionsloaded = 'T';
                            properties.conditions         = response.rentalconditions;
                            select.push('<option value="">-</option>');
                            for (var i = 0; i < properties.conditions.Rows.length; i++) {
                                select.push('<option value="' + properties.conditions.Rows[i][properties.conditions.ColumnIndex.conditionid] + '">' + properties.conditions.Rows[i][properties.conditions.ColumnIndex.condition] + '</option>');
                            }
                            jQuery('#qc-rentalConditions').html(select.join(''));
                        }
                        screen.getCompleteQCItemCallback(response);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#qc-btnTakePicture', function() {
            try {
                navigator.camera.getPicture(
                    //success
                    function(imageData) {
                        var image;
                        try {
                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                window.screen.lockOrientation('portrait-primary');
                            }
                            image = jQuery('<img>');
                            image
                                .attr('src', 'data:image/jpeg;base64,' + imageData)
                                .data('base64', imageData);
                            jQuery('#qc-images').append(image);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }
                    //error
                    , function(message) {
                        try {
                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                window.screen.lockOrientation('portrait-primary');
                            }
                            FwFunc.showError('Failed because: ' + message);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }
                    , { 
                        destinationType:    Camera.DestinationType.DATA_URL
                      , sourceType:         Camera.PictureSourceType.CAMERA
                      , allowEdit :         false
                      , correctOrientation: true
                      , encodingType:       Camera.EncodingType.JPEG
                      , quality:            applicationConfig.photoQuality
                      , targetWidth:        applicationConfig.photoWidth
                      , targetHeight:       applicationConfig.photoHeight 
                    }
                );
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#qc-images img', function() {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Confirm',  'Delete image?');
                var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                $btnok.focus();
                FwConfirmation.addButton($confirmation, 'Cancel', true);
                $btnok.on('click', function() {
                    try {
                        jQuery(this).remove();
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnScan', function() {
            try {
                screen.$view.find('#qc').data('scanmode', 'SCAN');
                screen.$view.find('.qc-RFID').hide();
                screen.$view.find('.qc-Scan').show();
                screen.$view.find('.modeSelector .modebtn').removeClass('selected');
                jQuery(this).addClass('selected');
                RwRFID.unregisterEvents();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnRFID', function() {
            try {
                screen.$view.find('#qc').data('scanmode', 'RFID');
                screen.$view.find('.qc-Scan').hide();
                screen.$view.find('.qc-RFID').show();
                screen.$view.find('.modeSelector .modebtn').removeClass('selected');
                jQuery(this).addClass('selected');
                RwRFID.registerEvents(screen.rfidscan);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.rfid-item', function() {
            var $this, $confirmation, $complete, $cancel, html = [], request, select = [];
            try {
                $this = jQuery(this);
                if ($this.attr('data-qcrequired') == 'T') {
                    $confirmation = FwConfirmation.renderConfirmation($this.attr('data-title'), '');
                    $complete     = FwConfirmation.addButton($confirmation, 'Complete', true);
                    $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

                    html.push('<div class="qc-iteminfo">');
                        html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield condition" data-caption="Condition" data-datafield=""></div>');
                        html.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Note" data-datafield=""></div>');
                        html.push('<div id="qc-pnlPictures" class="center">');
                            html.push('<div>');
                                html.push('<span id="qc-btnTakePicture" class="button default"><div class="pictureicon"></div>Take Picture</span>');
                            html.push('</div>');
                            html.push('<div id="qc-images"></div>');
                        html.push('</div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));

                    for (var i = 0; i < properties.conditions.Rows.length; i++) {
                        select.push({value: properties.conditions.Rows[i][properties.conditions.ColumnIndex.conditionid], text: properties.conditions.Rows[i][properties.conditions.ColumnIndex.condition]});
                    }
                    FwFormField.loadItems($confirmation.find('.condition'), select);
                    FwFormField.setValue($confirmation, '.condition', $this.attr('data-conditionid'));

                    $confirmation
                        .on('click', '#qc-btnTakePicture', function() {
                            try {
                                navigator.camera.getPicture(
                                    //success
                                    function(imageData) {
                                        var image;
                                        try {
                                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                                window.screen.lockOrientation('portrait-primary');
                                            }
                                            image = jQuery('<img>');
                                            image.attr('src', 'data:image/jpeg;base64,' + imageData).data('base64', imageData);
                                            $confirmation.find('#qc-images').append(image);
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }
                                    //error
                                    , function(message) {
                                        try {
                                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                                window.screen.lockOrientation('portrait-primary');
                                            }
                                            FwFunc.showError('Failed because: ' + message);
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }
                                    , { 
                                        destinationType:    Camera.DestinationType.DATA_URL
                                      , sourceType:         Camera.PictureSourceType.CAMERA
                                      , allowEdit :         false
                                      , correctOrientation: true
                                      , encodingType:       Camera.EncodingType.JPEG
                                      , quality:            applicationConfig.photoQuality
                                      , targetWidth:        applicationConfig.photoWidth
                                      , targetHeight:       applicationConfig.photoHeight 
                                    }
                                );
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        })
                        .on('click', '#qc-images img', function() {
                            try {
                                var $confirmation = FwConfirmation.renderConfirmation('Confirm',  'Delete image?');
                                var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                                $btnok.focus();
                                FwConfirmation.addButton($confirmation, 'Cancel', true);
                                $btnok.on('click', function() {
                                    try {
                                        jQuery(this).remove();
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        })
                    ;

                    $complete.on('click', function() {
                        try {
                            request = {
                                action:             'completeqc',
                                code:               $this.attr('data-tag'),
                                conditionid:        FwFormField.getValue($confirmation, '.condition'),
                                note:               FwFormField.getValue($confirmation, '.note'),
                                images:             [],
                                isconditionsloaded: properties.isconditionsloaded
                            };
                            RwServices.order.completeQCItem(request, function(response) {
                                try {
                                    application.playStatus(true);
                                    $this.addClass('processed').attr('data-qcrequired', 'F');
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
            screen.$view.find('.qc-qcreq .rfid-placeholder').show();
            screen.$view.find('.qc-qcreq .rfid-items').empty();
            screen.$view.find('.qc-noqcreq .rfid-placeholder').show();
            screen.$view.find('.qc-noqcreq .rfid-items').empty();
            request = {
                tags:               epcs,
                isconditionsloaded: properties.isconditionsloaded
            };
            RwServices.inventory.qcstatusrfid(request, function(response) {
                var $item;
                try {
                    for (var i = 0; i < response.items.length; i++) {
                        $item = screen.rfiditem('item');
                        $item.find('.rfid-item-title').html(response.items[i].title);
                        $item.find('.rfid-data.rfid .item-value').html((response.items[i].tag != '') ? response.items[i].tag : '-');
                        $item.find('.rfid-data.barcode .item-value').html((response.items[i].barcode != '') ? response.items[i].barcode : '-');

                        $item.attr('data-title',        response.items[i].title);
                        $item.attr('data-masterno',     response.items[i].masterno);
                        $item.attr('data-master',       response.items[i].master);
                        $item.attr('data-warehouse',    response.items[i].warehouse);
                        $item.attr('data-barcode',      response.items[i].barcode);
                        $item.attr('data-tag',          response.items[i].tag);
                        $item.attr('data-rentalstatus', response.items[i].rentalstatus);
                        $item.attr('data-orderno',      response.items[i].orderno);
                        $item.attr('data-orderdesc',    response.items[i].orderdesc);
                        $item.attr('data-qcrequired',   response.items[i].qcrequired);
                        $item.attr('data-conditionid',  response.items[i].conditionid);

                        if (response.items[i].qcrequired == 'T') {
                            screen.$view.find('.qc-qcreq .rfid-placeholder').hide();
                            screen.$view.find('.qc-qcreq .rfid-items').append($item);
                        } else {
                            screen.$view.find('.qc-noqcreq .rfid-placeholder').hide();
                            screen.$view.find('.qc-noqcreq .rfid-items').append($item);
                        }
                    }

                    if (request.isconditionsloaded == 'F') {
                        properties.isconditionsloaded = 'T';
                        properties.conditions         = response.rentalconditions;
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
                html.push('<div class="rfid-data barcode">');
                    html.push('<div class="item-caption">Barcode:</div>');
                    html.push('<div class="item-value"></div>');
                html.push('</div>');
            html.push('</div>');
        html.push('</div>');
        return jQuery(html.join(''));
    };

    screen.load = function() {
        application.setScanTarget('#scanBarcodeView-txtBarcodeData');
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
        //RwServices.order.getRentalConditions({}, function(rentalConditionsResponse) {
        //    var dt, select;
        //    dt = rentalConditionsResponse.getRentalConditions;
        //    select = [];
        //    select.push('<option value="">-</option>');
        //    for (var i = 0; i < dt.Rows.length; i++) {
        //        select.push('<option value="' + dt.Rows[i][dt.ColumnIndex.conditionid] + '">' + dt.Rows[i][dt.ColumnIndex.condition] + '</option>');
        //    }
        //    jQuery('#qc-rentalConditions')
        //        .html(select.join(''))
        //    ;
        //});
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_rwinventorycontrollerjs_getQCScreen', function() {
                RwRFID.isConnected = true;
                screen.$view.find('.modeSelector').show();
                screen.$view.find('.btnRFID').click();
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_rwinventorycontrollerjs_getQCScreen', function() {
                RwRFID.isConnected = false;
                screen.$view.find('.modeSelector').hide();
            });
        }
    };

    screen.unload = function () {
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_rwinventorycontrollerjs_getQCScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_rwinventorycontrollerjs_getQCScreen');
        }
    };
    
    return screen;
};