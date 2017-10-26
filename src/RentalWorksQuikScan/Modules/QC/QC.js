﻿//----------------------------------------------------------------------------------------------
RwInventoryController.getQCScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $search, $item, $itemstatus;
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

    $search     = screen.$view.find('.qc-search');
    $item       = screen.$view.find('.qc-item');
    $itemstatus = screen.$view.find('.qc-item-status');

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

    $search.on('change', '.fwmobilecontrol-value', function() {
        var $this, request;
        $this = jQuery(this);
        if ($this.val() !== '') {
            request = {
                code: $this.val()
            };
            RwServices.callMethod('QC', 'ItemScan', request, function(response) {
                $this.val('');
                $item.resetitem();
                $itemstatus.loadvalues(response.qcitem.status, response.qcitem.msg);
                if (response.qcitem.status === 0) $item.showscreen(response);
            });
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

        FwFormField.setValue($item.find('div[data-datafield="condition"]'), response.itemstatus.conditionid);
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
        .on('click', '.item-photo', function() {
            var $this;
            $this = jQuery(this);
            try {
                if (typeof navigator.camera !== 'undefined') {
                    navigator.camera.getPicture(
                        //success
                        function(imageData) {
                            var $image;
                            try {
                                if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                    window.screen.lockOrientation('portrait-primary');
                                }
                                $image = jQuery('<img>')
                                    .attr('src', 'data:image/jpeg;base64,' + imageData)
                                    .data('base64', imageData);
                                $this.attr('data-status', 'image');
                                $this.empty().append($image);
                                $item.find('.pictures').append($item.addblankimage());
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        },
                        //error
                        function(message) {
                            try {
                                if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                    window.screen.lockOrientation('portrait-primary');
                                }
                                FwNotification.renderNotification('ERROR', message);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        },
                        {
                            destinationType:    Camera.DestinationType.DATA_URL,
                            sourceType:         Camera.PictureSourceType.CAMERA,
                            allowEdit:          false,
                            correctOrientation: true,
                            encodingType:       Camera.EncodingType.JPEG,
                            quality:            applicationConfig.photoQuality,
                            targetWidth:        applicationConfig.photoWidth,
                            targetHeight:       applicationConfig.photoHeight
                        }
                    );
                } else {
                    FwNotification.renderNotification('ERROR', 'Only supported on mobile devices');
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        application.setScanTarget('.fwmobilecontrol-value');
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_rwinventorycontrollerjs_getQCScreen', function() {
                RwRFID.isConnected = true;
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_rwinventorycontrollerjs_getQCScreen', function() {
                RwRFID.isConnected = false;
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