//----------------------------------------------------------------------------------------------
RwInventoryController.getInventoryWebImageScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, $item, $status, $images;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Inventory Web Image')
      , captionICodeDesc:   RwLanguages.translate('I-Code')
      , captionICode:       RwLanguages.translate('I-Code')
      , captionAttach:      RwLanguages.translate('Attach')
      , captionTakePicture: RwLanguages.translate('Take Picture')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-inventoryWebImage').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    $item   = screen.$view.find('.iwi-item');
    $images = screen.$view.find('.iwi-item-images');
    $status = screen.$view.find('.iwi-status');

    properties.item = null;
    properties.mode = 'icode';

    if (typeof navigator.camera !== 'undefined' && program.hasCamera) {
        screen.$view.find('#scancontrol').fwmobilemodulecontrol({
            buttons: [
                { 
                    caption:     'Take Picture',
                    orientation: 'right',
                    icon:        '&#xE412;', //photo_camera
                    state:       0,
                    buttonclick: function () {
                        if (properties.item != null) {
                            try {
                                navigator.camera.getPicture(
                                    //success
                                    function(imageData) {
                                        var img, request, $images, i;
                                        try {
                                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                                window.screen.lockOrientation('portrait-primary');
                                            };
                                            request = {
                                                item:      properties.item,
                                                mode:      properties.mode,
                                                images:    [imageData]
                                            };
                                            RwServices.callMethod("InventoryWebImage", "AddInventoryWebImage", request, function(response) {
                                                try {
                                                    properties.item.images = response.images;
                                                    $item.refreshimages(response.images);
                                                    program.playStatus(true);
                                                } catch(ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
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
                                            //FwFunc.showError('Failed because: ' + message);
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
                        } else {
                            FwNotification.renderNotification('ERROR', 'An item must be selected to perform this action.');
                        }
                    }
                }
            ]
        });
    }

    screen.$view
        .on('change', '.fwmobilecontrol-value', function() {
            var $this;
            try {
                $this = jQuery(this);
                if ($this.val() !== '') {
                    screen.reset();
                    request = {
                        code:  RwAppData.stripBarcode($this.val().toUpperCase())
                    };
                    RwServices.callMethod("InventoryWebImage", "GetInventoryItem", request, function(response) {
                        try {
                            if (response.item.status == 0) {
                                $this.val('');
                                $item.showscreen(response.item);

                                program.playStatus(response.item.status === 0);
                            } else {
                                $status.show().html(response.item.genericError + '<br />' + response.item.msg);
                            }
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
    screen.reset = function() {
        $item.hide();
        $item.find('#iwi-item-images-modeselector').show();
        $status.hide().empty();
        properties.item = null;
    };

    $item.find('#iwi-item-images-modeselector').fwmobilemoduletabs({
        tabs: [
            {
                id:          'icodetab',
                caption:     'I-Code',
                buttonclick: function () {
                    properties.mode = 'icode';
                    $item.refreshimages(properties.item.images);
                }
            },
            {
                id:          'barcodetab',
                caption:     'Barcode',
                buttonclick: function () {
                    properties.mode = 'barcode';
                    $item.refreshimages(properties.item.images);
                }
            }
        ]
    });
    $item.showscreen = function(item) {
        properties.item = item;

        $item.find('.iwi-item-description').html(item.description);
        $item.find('.icode .value').html(item.masterNo);
        $item.find('.barcode').toggle(item.barcode != '');
        $item.find('.barcode .value').html(item.barcode);

        if (item.isICode) {
            properties.mode = 'icode';
            $item.find('#iwi-item-images-modeselector').hide();
        }
        if (properties.mode = 'icode') {
            $item.find('#icodetab').click();
        } else {
            $item.find('#barcodetab').click();
        }

        $item.show();
    };
    $item.refreshimages = function(images) {
        var imagestoload;
        $item.find('.iwi-item-images').empty();
        imagestoload = (properties.mode == 'icode') ? images.icode : images.barcode;
        if (imagestoload.length > 0) {
            for(i = 0; i < imagestoload.length; i++) {
                $item.find('.iwi-item-images').append(jQuery('<img>').attr('src', 'data:image/jpeg;base64,' + imagestoload[i].thumbnail).attr('data-appimageid', imagestoload[i].appimageid));
            }
        } else {
            $item.find('.iwi-item-images').append(jQuery('<div class="nopicturesfound">0 pictures found.</div>'));
        }
    };
    $item
        .on('click', '.iwi-item-images img', function() {
            var $img, request, $contextmenu;
            try {
                $img = jQuery(this);
                $contextmenu = FwContextMenu.render('');
                FwContextMenu.addMenuItem($contextmenu, 'View Image', function () {
                    try {
                        var html = [];
                        html.push('<img style="max-width:100%;" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'fwappimage.ashx?method=GetAppImage&appimageid=' + $img.attr('data-appimageid') + '&thumbnail=false' + '\" >');
                        html = html.join('\n');
                        var $confirmation = FwConfirmation.renderConfirmation('Image Viewer', html);
                        var $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwContextMenu.addMenuItem($contextmenu, 'Delete Image', function () {
                    request = {
                        item:       properties.item,
                        appimageid: $img.attr('data-appimageid')
                    };
                    RwServices.callMethod("InventoryWebImage", "DeleteAppImage", request, function(response) {
                        try {
                            properties.item.images = response.images;
                            $item.refreshimages(response.images);
                            program.playStatus(true);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                });
                FwContextMenu.addMenuItem($contextmenu, 'Make Primary Image', function() {
                    request = {
                        item:       properties.item,
                        mode:       properties.mode,
                        appimageid: $img.attr('data-appimageid')
                    };
                    RwServices.callMethod("InventoryWebImage", "MakePrimaryAppImage", request, function(response) {
                        try {
                            properties.item.images = response.images;
                            $item.refreshimages(response.images);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        program.setScanTarget('.fwmobilecontrol-value');
        program.setScanTargetLpNearfield('.fwmobilecontrol-value', true);
    };

    screen.unload = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');
    };

    return screen;
};