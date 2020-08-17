//----------------------------------------------------------------------------------------------
RwInventoryController.getRepairOrderScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, $fwcontrols;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Repair Order')
      , htmlScanBarcode:    RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Repair No. / BC')})
      , captionICodeDesc:   RwLanguages.translate('I-Code')
      , captionICode:       RwLanguages.translate('I-Code')
      , captionLastDeal:    RwLanguages.translate('Deal')
      , captionAttach:      RwLanguages.translate('Attach')
      , captionTakePicture: RwLanguages.translate('Take Picture')
      , captionPhotoDescription: RwLanguages.translate('Photo Description')
      , captionClose:            RwLanguages.translate('Close')
      , captionDamage:          RwLanguages.translate('Damage')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-repairorder').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    screen.$view.find('.browserepairorders-container').hide();

    screen.loadRepairOrders = function() {
        var request = {
            pageno: 1,
            pagesize: 1000
        }
        RwServices.callMethod('RepairOrder', 'GetRepairOrders', request, function(response) {
            var itemtemplate, rowhtml, itemmodel, dt, html;
            dt = response.repairorders;
            html = [];
            if (dt !== null) {
                itemtemplate = jQuery('#tmpl-repairorderbrowseitem').html();
                for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
                    itemmodel = {
                        valueMaster:       dt.Rows[rowno][dt.ColumnIndex.master],
                        captionDamageDeal: RwLanguages.translate('Deal'),
                        valueDamageDeal:   dt.Rows[rowno][dt.ColumnIndex.damagedeal],
                        captionRepairNo:   RwLanguages.translate('Repair No'),
                        valueRepairNo:     dt.Rows[rowno][dt.ColumnIndex.repairno],
                        captionStatus:     RwLanguages.translate('Status'),
                        valueStatus:       dt.Rows[rowno][dt.ColumnIndex.status],
                        captionMasterNo:   RwLanguages.translate('I-Code'),
                        valueMasterNo:     dt.Rows[rowno][dt.ColumnIndex.masterno],
                        captionAsOf:       RwLanguages.translate('As Of'),
                        valueAsOf:         dt.Rows[rowno][dt.ColumnIndex.statusdate],
                        captionBarcode:    RwLanguages.translate('Bar Code'),
                        valueBarcode:      dt.Rows[rowno][dt.ColumnIndex.barcode]
                    }
                    rowhtml = Mustache.render(itemtemplate, itemmodel);
                    html.push(rowhtml);
                }
            }
            screen.$view.find('.browserepairorders').html(html.join('\n'));
            screen.$view.find('.browserepairorders-container').show();
        });
    };

    screen.$view.on('click', '.browserepairorders > li', function() {
        var $li, repairno; 
        $li              = jQuery(this);
        repairno    = $li.attr('data-repairno');
        screen.selectRepairOrder(repairno);
    });
    
    
    screen.resetRepairOrder = function() {
        program.setScanTarget('.repairorder-search .fwmobilecontrol-value');
        program.setScanTargetLpNearfield('.repairorder-search .fwmobilecontrol-value', true);
        if (properties.mode === 'repairorder') {
            screen.$btncancel.hide();
            screen.loadRepairOrders();
        }
        screen.$btnsave.hide();
        screen.$view.find('.repairorder-search .fwmobilecontrol-value').val('');
        screen.$view.find('.repairorder-search').show();
        //screen.$view.find('#repairorder-msg').hide();
        screen.$view.find('.selectrepairorder').hide();
        screen.$view.find('.appdocuments').empty();
        screen.$view.find('.btnaddappdocument').show();
    };

    if (properties.mode === 'sendtorepair') {
        screen.$btnback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', true, function() { //back
            program.popScreen();
        });
    }

    if (properties.mode === 'repairorder') {
        screen.$btncancel = FwMobileMasterController.addFormControl(screen, 'Cancel', 'left', '&#xE14C;', true, function() { //cancel
            screen.resetRepairOrder();
        });
    }

    screen.$btnsave = FwMobileMasterController.addFormControl(screen, 'Save', 'right', '&#xE161;', false, function() { //save
        var request, $appdocument, $appimages, $deleteimages;
        try {
            $appdocument = screen.$view.find('.newappdocument');
            $appimages = $appdocument.find('.appimage');
            //$deleteimages = screen.$view.find('.deleteimage');
            request = {
                repairId: properties.webSelectRepairOrder.repairId
                , documentdescription: $appdocument.find('.descriptionrow input').val()
                , images:   []
                , imagedescriptions: []
                , deleteimages: []
                , damage: FwFormField.getValue(screen.$view, '.damage')
            };
            for (var i = 0; i < $appimages.length; i++) {
                var $appimage = $appimages.eq(i);
                request.images.push($appimage.data('base64'));
                request.imagedescriptions.push($appimage.find('.imagedesccontent input').val());
            }
            //for (var i = 0; i < $deleteimages.length; i++) {
            //    request.deleteimages.push($deleteimages.eq(i).attr('data-appdocumentid'));
            //}
            RwServices.callMethod("RepairOrder", "UpdateRepairOrder", request, function(response) {
                try {
                    if (properties.mode === 'sendtorepair') {
                        program.popScreen();
                    } else if (properties.mode == "repairorder") {
                        screen.resetRepairOrder();
                        //jQuery('#repairorder-txtGenericError').html('SUCCESS');
                        //jQuery('#repairorder-txtMsg').html("Repair Order Updated.");
                        //jQuery('#repairorder-msg')
                        //    .attr('class', 'qssuccess')
                        //    .show();
                        FwNotification.renderNotification('SUCCESS', 'Repair Order Updated');
                        program.playStatus(true);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
    
    screen.selectRepairOrder = function(barcode) {
        program.setScanTarget('');
        program.setScanTargetLpNearfield('');
        var requestSelectRepairOrder
        screen.$view.find('.browserepairorders-container').hide();
        jQuery('#repairorder-msg').hide();
        if (barcode !== '') {
            requestSelectRepairOrder = {
                code: barcode.toUpperCase()
            };
            RwServices.callMethod('RepairOrder', 'GetRepairOrder', requestSelectRepairOrder, function(response) {
                try {
                    screen.$view.find('.repairorder-search').toggle(response.webSelectRepairOrder.status !== 0);
                    properties.webSelectRepairOrder = response.webSelectRepairOrder;
                    //jQuery('#repairorder-msg')
                    //    .toggle(response.webSelectRepairOrder.status !== 0)
                    //    .attr('class', (response.webSelectRepairOrder.status !== 0) ? 'qserror' : 'qssuccess')
                    //;
                    //jQuery('#repairorder-txtGenericError').html(response.webSelectRepairOrder.genericMsg);
                    //jQuery('#repairorder-txtMsg').html(response.webSelectRepairOrder.msg);
                    if (response.webSelectRepairOrder.status !== 0) {
                        FwNotification.renderNotification('ERROR', '<div>' + response.webSelectRepairOrder.genericMsg + '</div><div>' + response.webSelectRepairOrder.msg + '</div>');
                    }
                    
                    screen.$view.find('.selectrepairorder').toggle(response.webSelectRepairOrder.status === 0);
                    if (response.webSelectRepairOrder.status === 0) {
                        if (properties.mode === 'repairorder') {
                            screen.$btncancel.show();
                        }
                        screen.$btnsave.show();
                        if (response.webSelectRepairOrder.repairId.length > 0) {
                            FwFormField.setValue(screen.$view, '.masterno', response.webSelectRepairOrder.masterNo);
                            FwFormField.setValue(screen.$view, '.master', response.webSelectRepairOrder.master);
                            jQuery('.lastdealrow').toggle(response.webSelectRepairOrder.deal !== '');
                            FwFormField.setValue(screen.$view, '.deal', response.webSelectRepairOrder.deal);
                            FwFormField.setValue(screen.$view, '.damage', response.repair.damage);
                        }
                        if (typeof response.appdocuments === 'object') {
                            var adhtml: string | string[] = [];
                            for (var i = 0; i < response.appdocuments.length; i++) {
                                var appdocument = response.appdocuments[i];
                                adhtml.push('<div class="appdocument dbappdocument" data-appdocumentid="' + appdocument.appdocumentid + '">');
                                adhtml.push('  <div class="descriptionrow flexrow flexalignitemscenter"><div class="descriptioncaption">Document:</div><div class="descriptionvalue">' + appdocument.description + '</div></div>');
                                adhtml.push('  <div class="imagerow">');
                                for (var j = 0; j < appdocument.images.length; j++) {
                                    var image = appdocument.images[j];
                                    adhtml.push('<div class="appimage dbappimage">');
                                    adhtml.push('  <div class="imagecontent"><img data-appimageid="' + image.appimageid + '" src="' + 'data:image/jpeg;base64,' + image.thumbnail + '"></div>');
                                    adhtml.push('  <div class="imagedesccontent">' + image.imagedesc + '</div>');
                                    adhtml.push('  <div class="imagenocontent">' + image.imageno + '</div>');
                                    adhtml.push('</div>');
                                }
                                adhtml.push('  </div>');
                                //adhtml.push('  <div class="pnlTakePicture">');
                                //adhtml.push('    <span class="button default btntakepicture"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/iphone-camera.002.png" alt="Take Picture" />Take Picture</span>');
                                //adhtml.push('  </div>');
                                adhtml.push('</div>');
                            }
                            adhtml = adhtml.join('\n');
                            screen.$view.find('.appdocuments').html(adhtml);
                        }
                    }
                    program.playStatus(response.webSelectRepairOrder.status === 0);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    };
    
    screen.$view
        .on('change', '.repairorder-search .fwmobilecontrol-value', function() {
            var $this;
            try {
                $this = jQuery(this);
                screen.selectRepairOrder($this.val());
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btntakepicture', function() {
            var $appdocument;
            try {
                if (typeof (<any>navigator).camera === 'undefined' || !program.hasCamera) {
                    throw 'Camera is not supported in the current environment.';
                }
                $appdocument = jQuery(this).closest('.appdocument');
                (<any>navigator).camera.getPicture(
                    //success
                    function(imageData) {
                        var $appimage, aihtml: string | string[] = [];
                        try {
                            if ((typeof window.screen === 'object') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                                (<any>window).screen.lockOrientation('portrait-primary');
                            }
                            aihtml.push('<div class="appimage newappimage">');
                            aihtml.push('  <div class="imagecontent"><img /></div>');
                            aihtml.push('  <div class="imagedesccontent"><input type="textbox" placeholder="Image Description" /></div>');
                            aihtml.push('  <div class="imagenocontent"><input type="textbox" /></div>');
                            aihtml.push('</div>');
                            aihtml = aihtml.join('\n');
                            $appimage = jQuery(aihtml);
                            $appimage.data('base64', imageData);
                            $appimage.find('.imagecontent img').attr('src', 'data:image/jpeg;base64,' + imageData);
                            $appdocument.find('.imagerow').append($appimage);
                            $appimage.find('.imagedesccontent input').focus();
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }
                    //error
                    , function(message) {
                        try {
                            if ((typeof window.screen === 'object') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                                (<any>window).screen.lockOrientation('portrait-primary');
                            }
                            FwFunc.showError('Failed because: ' + message);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }
                    , { 
                        destinationType:    (<any>window).Camera.DestinationType.DATA_URL
                      , sourceType:         (<any>window).Camera.PictureSourceType.CAMERA
                      , allowEdit :         false
                      , correctOrientation: true
                      , encodingType:       (<any>window).Camera.EncodingType.JPEG
                      , quality:            applicationConfig.photoQuality
                      , targetWidth:        applicationConfig.photoWidth
                      , targetHeight:       applicationConfig.photoHeight 
                    }
                );
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnaddappdocument', function() {
            var adhtml: string | string[] = [];
            adhtml.push('<div class="appdocument newappdocument">');
            adhtml.push('  <div class="descriptionrow flexrow flexalignitemscenter"><div class="descriptioncaption">Document:</div><div class="descriptionvalue"><input type="textbox" placeholder="Document Description" /></div></div>');
            adhtml.push('  <div class="imagerow"></div>');
            adhtml.push('  <div class="pnlTakePicture">');
            adhtml.push('    <span class="button default btntakepicture"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/iphone-camera.002.png" alt="Take Picture" />Take Picture</span>');
            adhtml.push('  </div>');
            adhtml.push('</div>');
            adhtml = adhtml.join('\n');
            var $appdocument = jQuery(adhtml);
            screen.$view.find('.appdocuments').append($appdocument);
            jQuery(this).hide();
        })
        .on('click', '.newappdocument .appimage .imagecontent img', function() {
            try {
                var me = this;
                var $confirmation = FwConfirmation.renderConfirmation('Confirm',  'Delete image?');
                var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                $btnok.focus();
                FwConfirmation.addButton($confirmation, 'Cancel', true);
                $btnok.on('click', function() {
                    var $appimage
                    try {
                        $appimage = jQuery(me).closest('.appimage');
                        if ($appimage.hasClass('newappimage')) {
                            // uncommited photos can just be removed locally
                            $appimage.remove();
                        } else if ($appimage.hasClass('dbappimage')) {
                            // flag the image to be deleted on Save
                            $appimage.addClass('deleteimage');
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        screen.resetRepairOrder();
        if (!Modernizr.touch) {
            jQuery('.repairorder-search .fwmobilecontrol-value').select();
        }
        screen.$view.find('.repairorder-search').toggle(properties.mode === 'repairorder');
        if (properties.mode === 'sendtorepair') {
            setTimeout(function() {
                screen.selectRepairOrder(properties.repairno);
            }, 1);
        } else {
            screen.loadRepairOrders();
        }
    };

    screen.unload = function() {
        // reset scan target for LineaPro
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');
    }

    return screen;
};