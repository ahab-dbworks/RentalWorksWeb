//----------------------------------------------------------------------------------------------
RwInventoryController.getInventoryWebImageScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, $item, $status;
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
    $status = screen.$view.find('.iwi-status');

    screen.$view.find('#scancontrol').fwmobilemodulecontrol({
        buttons: [
            { 
                caption:     'Take Picture',
                orientation: 'right',
                icon:        'photo_camera',
                state:       1,
                buttonclick: function () {
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
                                        barcode:   properties.barcode,
                                        item:      properties.webGetItemStatus,
                                        images:    [imageData]
                                    };
                                    RwServices.call("InventoryWebImage", "AddInventoryWebImage", request, function(response) {
                                        try {
                                            $item.refreshimages(response.appImages);
                                            application.playStatus(true);
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
                }
            }
        ]
    });

    screen.$view
        .on('change', '.fwmobilecontrol-value', function() {
            var $this;
            try {
                $this = jQuery(this);
                if ($this.val() !== '') {
                    request = {
                        barcode:  RwAppData.stripBarcode($this.val().toUpperCase())
                    };
                    RwServices.call("InventoryWebImage", "GetInventoryItem", request, function(response) {
                        try {
                            if (response.webGetItemStatus.status == 0) {
                                screen.$view.find('.fwmobilecontrol-value').val('');
                                $status.hide().empty();
                                if (typeof navigator.camera !== 'undefined') screen.$view.find('#scancontrol').fwmobilemodulecontrol('changeState', 1);
                                properties.barcode          = request.barcode;
                                properties.webGetItemStatus = response.webGetItemStatus;

                                $item.show();
                                $item.find('.iwi-item-description').html(response.webGetItemStatus.description);
                                $item.find('.iwi-item-icode .value').html(response.webGetItemStatus.masterNo);
                                $item.refreshimages(response.appImages);

                                application.playStatus(response.webGetItemStatus.status === 0);
                            } else {
                                $item.hide();
                                $status.show().html(response.webGetItemStatus.genericError + '<br />' + response.webGetItemStatus.msg);
                                screen.$view.find('#scancontrol').fwmobilemodulecontrol('changeState', 0);
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

    $item
        .on('click', '.iwi-item-images img', function() {
            var $img, request, $contextmenu;
            try {
                $img = jQuery(this);
                $contextmenu = FwContextMenu.render('');
                FwContextMenu.addMenuItem($contextmenu, 'Delete Image', function() {
                    request = {
                        barcode:    properties.barcode,
                        item:       properties.webGetItemStatus,
                        appimageid: $img.attr('data-appimageid')
                    };
                    RwServices.call("InventoryWebImage", "DeleteAppImage", request, function(response) {
                        try {
                            $item.refreshimages(response.appImages);
                            application.playStatus(true);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                });
                FwContextMenu.addMenuItem($contextmenu, 'Make Primary Image', function() {
                    request = {
                        barcode:    properties.barcode,
                        item:       properties.webGetItemStatus,
                        appimageid: $img.attr('data-appimageid')
                    };
                    RwServices.call("InventoryWebImage", "MakePrimaryAppImage", request, function(response) {
                        try {
                            $item.refreshimages(response.appImages);
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
    $item.refreshimages = function(images) {
        $item.find('.iwi-item-images').empty();
        if (images.length > 0) {
            for(i = 0; i < images.length; i++) {
                $item.find('.iwi-item-images').append(jQuery('<img>').attr('src', 'data:image/jpeg;base64,' + images[i].thumbnail).attr('data-appimageid', images[i].appimageid));
            }
        } else {
            $item.find('.iwi-item-images').append(jQuery('<div class="nopicturesfound">0 pictures found.</div>'));
        }
    };

    screen.load = function() {
        application.setScanTarget('.fwmobilecontrol-value');
    };

    return screen;
};