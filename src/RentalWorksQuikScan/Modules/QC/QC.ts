class QCClass {
    getQCScreen(viewModel, properties) {
        let combinedViewModel = jQuery.extend({
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
        let screen: any = {};
        screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

        const $search: any    = screen.$view.find('.qc-search');
        const $item: any      = screen.$view.find('.qc-item');
        const $itemstatus: any = screen.$view.find('.qc-item-status');

        screen.$view.find('#qccontrol').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Submit',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       1,
                    buttonclick: function () {
                        var request, $images;
                        $images = $item.find('.item-photo[data-status="image"] img');
                        request = {
                            qcitem:      $item.data('recorddata'),
                            conditionid: FwFormField.getValue($item, 'div[data-datafield="condition"]'),
                            note:        FwFormField.getValue($item, 'div[data-datafield="note"]'),
                            images:      []
                        };
                        for (var i = 0; i < $images.length; i++) {
                            request.images.push($images.eq(i).data('base64'));
                        }
                        RwServices.callMethod('QC', 'UpdateQCItem', request, function(response) {
                            $item.resetitem();
                            $itemstatus.reset();
                        });
                    }
                }
            ]
        });

        screen.onScanBarcode = (barcode: string, barcodetype: string, callback: (response: any) => void) => {
            screen.resetscreen();
            screen.itemScan(barcode, callback);
        }

        screen.itemScan = (barcode: string, callback: (response: any) => void) => {;
            let request = {
                code: barcode
            };
            RwServices.callMethod('QC', 'ItemScan', request, function (response) {
                callback(response);
                $item.resetitem();
                $itemstatus.show();
                $itemstatus.loadvalues(response.qcitem.status, response.qcitem.msg);
                if (response.qcitem.status === 0) $item.showscreen(response);
            });
        }



        $search.on('change', '.fwmobilecontrol-value', function() {
            try {
                var $this = jQuery(this);
                if ($this.val() !== '') {
                    const barcode: string = <string>$this.val();
                    screen.onScanBarcode(barcode, null, () => {
                        try {
                            $this.val('');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $itemstatus.loadvalues = function(status, message) {
            if (status === 0) {
                $itemstatus.attr('data-status', 'success');
                $itemstatus.find('.qc-item-statusmessage').html('Item successfully QC\'d');
            } else {
                $itemstatus.attr('data-status', 'error');
                $itemstatus.find('.qc-item-statusmessage').html(message);
            }
        }
        $itemstatus.reset = function() {
            $itemstatus.removeAttr('data-status');
            $itemstatus.find('.qc-item-statusmessage').html('');
        };

        $item.showscreen = function(response) {
            $item.show();

            screen.$view.find('#qccontrol').fwmobilemodulecontrol('changeState', 1);
            $item.data('recorddata', response.qcitem);
            $item.iteminfo(response.itemstatus);
            $item.getfields(response.conditions);

            FwFormField.setValue2($item.find('div[data-datafield="condition"]'), response.itemstatus.conditionid);
        };
        $item.iteminfo = function(iteminfo) {
            var html = [], $itemdetails;

            html.push('<div class="itemdetailrow">');
            html.push('  <div class="itemdetailcol1">I-Code</div>');
            html.push('  <div class="itemdetailcol2">' + iteminfo.masterNo + '</div>');
            html.push('</div>');
            html.push('<div class="itemdetailrow">');
            html.push('  <div class="itemdetailcol1">Description</div>');
            html.push('  <div class="itemdetailcol2">' + iteminfo.description + '</div>');
            html.push('</div>');
            if (iteminfo.barcode !== '') {
                html.push('<div class="itemdetailrow">');
                html.push('  <div class="itemdetailcol1">Barcode</div>');
                html.push('  <div class="itemdetailcol2">' + iteminfo.barcode + '</div>');
                html.push('</div>');
            }
            if (iteminfo.mfgserial !== '') {
                html.push('<div class="itemdetailrow">');
                html.push('  <div class="itemdetailcol1">Serial No</div>');
                html.push('  <div class="itemdetailcol2">' + iteminfo.mfgserial + '</div>');
                html.push('</div>');
            }
            if (iteminfo.rfid !== '') {
                html.push('<div class="itemdetailrow">');
                html.push('  <div class="itemdetailcol1">RFID</div>');
                html.push('  <div class="itemdetailcol2">' + iteminfo.rfid + '</div>');
                html.push('</div>');
            }
            $itemdetails = jQuery(html.join(''));

            $item.find('.qc-item-details').append($itemdetails);
        };
        $item.getfields = function(conditions) {
            var html = [], $fields;

            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Condition" data-type="select" data-datafield="condition" />');
            html.push('</div>');
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Note" data-type="textarea" data-datafield="note" />');
            html.push('</div>');
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('  <div class="picturecaption">Item Images</div>');
            html.push('  <div class="pictures"></div>');
            html.push('</div>');
            $fields = jQuery(html.join(''));

            FwControl.renderRuntimeControls($fields.find('.fwcontrol'));

            FwFormField.loadItems($fields.find('div[data-datafield="condition"]'), conditions, false);

            $item.find('.qc-fields').append($fields);
            $item.find('.pictures').append($item.addblankimage());
        };
        $item.addblankimage = function() {
            var html = [];

            html.push('<div class="item-photo" data-status="empty">');
            html.push('  <i class="material-icons">add_a_photo</i>');
            html.push('</div>');

            return jQuery(html.join(''));
        };
        $item.resetitem = function() {
            $item.find('.qc-item-details').empty();
            $item.find('.qc-fields').empty();
            $item.data('recorddata', null);
            $item.hide();
            screen.$view.find('#qccontrol').fwmobilemodulecontrol('changeState', 0);
        };
        $item
            .on('click', '.item-photo[data-status="empty"]', function () {
                var $this;
                $this = jQuery(this);
                try {
                    if (typeof (<any>navigator).camera !== 'undefined') {
                        (<any>navigator).camera.getPicture(
                            //success
                            function (imageData) {
                                var $image;
                                try {
                                    if ((typeof window.screen === 'object') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                                        (<any>window).screen.lockOrientation('portrait-primary');
                                    }
                                    $image = jQuery('<img>')
                                        .attr('src', 'data:image/jpeg;base64,' + imageData)
                                        .data('base64', imageData);
                                    $this.attr('data-status', 'image');
                                    $this.empty().append($image);
                                    $item.find('.pictures').append($item.addblankimage());
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            },
                            //error
                            function (message) {
                                try {
                                    if ((typeof window.screen === 'object') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                                        (<any>window).screen.lockOrientation('portrait-primary');
                                    }
                                    FwNotification.renderNotification('ERROR', message);
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            },
                            {
                                destinationType:    (<any>window).Camera.DestinationType.DATA_URL,
                                sourceType:         (<any>window).Camera.PictureSourceType.CAMERA,
                                allowEdit:          false,
                                correctOrientation: true,
                                encodingType:       (<any>window).Camera.EncodingType.JPEG,
                                quality:            applicationConfig.photoQuality,
                                targetWidth:        applicationConfig.photoWidth,
                                targetHeight:       applicationConfig.photoHeight
                            }
                        );
                    } else {
                        FwNotification.renderNotification('ERROR', 'Only supported on mobile devices');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.item-photo[data-status="image"]', function () {
                var $this         = jQuery(this);
                var $confirmation = FwConfirmation.renderConfirmation('Remove Image?', '');
                var $remove       = FwConfirmation.addButton($confirmation, 'Remove', true);
                var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

                $remove.on('click', function () {
                    $this.remove();
                });
            })
            ;

        var $rfiditems = screen.$view.find('.qc-rfiditems');
        screen.resetscreen = function () {
            $rfiditems.empty();
            $rfiditems.hide();
            $itemstatus.hide();
            $item.hide();
            //if (typeof $rfiditems.$back !== 'undefined') $rfiditems.$back.remove();
            //$itemdetails.hide();
            //$error.hide();

        };
        screen.rfidscan = function (epcs) {
            screen.$view.find('.fwmobilecontrol-value').val('');
            if (epcs !== '') {
                screen.resetscreen();
                RwServices.callMethod('ItemStatus', 'ItemStatusRFID', { tags: epcs }, function (response) {
                    var $item, html: string[] | string = [];

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
                    html.push('    <div class="rfid-data qcrequired">');
                    html.push('      <div class="item-caption">QC Required:</div>');
                    html.push('      <div class="item-value"></div>');
                    html.push('    </div>');
                    html.push('  </div>');
                    html.push('</div>');

                    $rfiditems.show();
                    let exceptionCount = 0;

                    // sort the list so the qcrequired items are at the top
                    response.items.sort((a, b) => {
                        const qcrequiredForA = (a.qcrequired === 'T');
                        const qcrequiredForB = (b.qcrequired === 'T');
                        if (qcrequiredForA && !qcrequiredForB) {
                            return -1;
                        }
                        else if (qcrequiredForA && qcrequiredForB) {
                            return 0;
                        }
                        else if (!qcrequiredForA && qcrequiredForB) {
                            return 1;
                        }
                    })

                    html = html.join('\n');
                    const $itemtemmplate = jQuery(html);
                    for (var i = 0; i < response.items.length; i++) {
                        $item = $itemtemmplate.clone();
                        const hasException = (response.items[i].rentalstatus === '');
                        hasException ? $item.addClass('exception') : $item.addClass('item');
                        if (hasException) {
                            exceptionCount++;
                        }
                        const qcrequired = (response.items[i].qcrequired === 'T');
                        if (qcrequired) {
                            $item.addClass('qcrequired');
                        }

                        $item.find('.rfid-item-title').html(response.items[i].title);
                        $item.find('.rfid-data.rfid .item-value').html(response.items[i].tag);
                        $item.find('.rfid-data.barcode .item-value').html((response.items[i].barcode !== '') ? response.items[i].barcode : '-');
                        $item.find('.rfid-data.message .item-value').html(response.items[i].rentalstatus);
                        $item.find('.rfid-data.qcrequired .item-value').html(qcrequired ? "YES" : "NO");
                        

                        $item.data('recorddata', response.items[i]);

                        $rfiditems.append($item);
                    }

                    if (response.items.length === 0) {
                        $rfiditems.append('<div class="norecords">0 records found</div>');
                    }

                    if (exceptionCount > 0 && RwRFID.isRFIDAPI3) {
                        let toSay = `${response.items.length.toString()} tag`;
                        if (response.items.length > 1) {
                            toSay += 's';
                        }
                        toSay += `, ${exceptionCount.toString()} exception`;
                        if (exceptionCount > 1) {
                            toSay += 's'
                        }
                        window.ZebraRFIDAPI3.speak(toSay);
                    }

                    $rfiditems.on('click', '.rfid-item', (e: JQuery.ClickEvent) => {
                        const $rfiditem = jQuery(e.currentTarget);
                        const $contextMenu = FwContextMenu.render('RFID', null);
                        const qcrequired = $rfiditem.data('recorddata').qcrequired === 'T';
                        if (qcrequired) {
                            FwContextMenu.addMenuItem($contextMenu, 'QC Item', (e: JQuery.ClickEvent) => {
                                $rfiditems.hide();
                                screen.itemScan($rfiditem.data('recorddata').tag, (response: any) => {

                                });
                            });
                        }
                        FwContextMenu.addMenuItem($contextMenu, 'Tag Finder', (e: JQuery.ClickEvent) => {
                            RwRFID.startTagFinder($rfiditem.data('recorddata').tag);
                        });
                    });
                });
            }
        };

        screen.load = function() {
            program.setScanTarget('.fwmobilecontrol-value');
            program.setScanTargetLpNearfield('.fwmobilecontrol-value', true);
            RwRFID.registerRFIDEvents(screen.rfidscan);
        };

        screen.unload = function () {
            // reset scan target for LineaPro
            program.setScanTarget('#scanBarcodeView-txtBarcodeData');
            program.setScanTargetLpNearfield('');
            RwRFID.unregisterRFIDEvents();
        };
    
        return screen;
    }
}

var QC = new QCClass();
//----------------------------------------------------------------------------------------------