//----------------------------------------------------------------------------------------------
RwOrderController.getContactSignatureScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $canvas, canvas, $contractsignaturecontroller, $printimagecapture, $signaturecapture;
    combinedViewModel = jQuery.extend({
        captionPageTitle:      RwOrderController.getPageTitle(properties),
        captionPageSubTitle:   '',
        captionClearSignature: RwLanguages.translate('Clear Signature')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-contractSignature').html(), combinedViewModel);
    screen                         = {};
    screen.$view                   = FwMobileMasterController.getMasterView(combinedViewModel);

    FwControl.renderRuntimeControls(screen.$view.find('.fwcontrol'));

    $contractsignaturecontroller = screen.$view.find('#contractsignaturecontroller');
    $printimagecapture           = screen.$view.find('#cs-printimagecapture');
    $signaturecapture            = screen.$view.find('#cs-signaturecapture');

    $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    program.popScreen();
                }
            },
            {
                caption:     'Create Contract',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       0,
                buttonclick: function () {
                    //if ($printimagecapture.find('.signaturecapture').attr('data-status') === 'image') {
                        try {
                            var signatureImage = $printimagecapture.find('.signaturecapture img').data('base64');
                            var $images = $printimagecapture.find('.contract-photo[data-status="image"] img');
                            var request = {
                                contractId:          properties.contract.contractId,
                                contractType:        properties.contract.contractType,
                                orderId:             properties.contract.orderId,
                                responsiblePersonId: properties.contract.responsiblePersonId,
                                printname:           FwFormField.getValue($printimagecapture, 'div[data-datafield="printname"]'),
                                signatureImage:      signatureImage,
                                images:              []
                            };
                            for (var i = 0; i < $images.length; i++) {
                                request.images.push($images.eq(i).data('base64'));
                            }
                            RwServices.callMethod('ContractSignature', 'CreateContract', request, function (response) {
                                if (response.createcontract.status === 0) {
                                    properties.contract.contractId = response.createcontract.contractId;
                                    var $confirmation = FwConfirmation.renderConfirmation('Message', response.createcontract.msg);
                                    var $ok           = FwConfirmation.addButton($confirmation, 'OK', true);
                                    $ok.on('click', function () {
                                        program.navigate('home/home');
                                    });
                                    if (applicationConfig.apiurl.length > 0) {
                                        var $email = FwConfirmation.addButton($confirmation, 'E-Mail', true);
                                        $email.on('click', function () {
                                            $printimagecapture.find('#printimagecapture').hide();
                                            $printimagecapture.find('#pic-email').show();
                                            $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol('changeState', 2);
                                            FwFormField.setValue($printimagecapture, 'div[data-datafield="from"]', response.from);
                                            FwFormField.setValue($printimagecapture, 'div[data-datafield="subject"]', response.subject);
                                        });
                                    }
                                } else {
                                    FwFunc.showError(response.createcontract.msg);
                                }
                            });
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    //} else {
                    //    FwNotification.renderNotification('ERROR', 'Signature is required');
                    //}
                }
            },
            {
                caption:     'Cancel',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       1,
                buttonclick: function () {
                    $printimagecapture.signaturecapturedone();
                }
            },
            {
                caption:     'Done',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       1,
                buttonclick: function () {
                    var signatureImage = $printimagecapture.signaturePad().getSignatureImage('image/jpeg').replace('data:image/jpeg;base64,', '');
                    if ($printimagecapture.blanksignature != signatureImage) {
                        var $image = jQuery('<img>')
                            .attr('src', 'data:image/jpeg;base64,' + signatureImage)
                            .data('base64', signatureImage);
                        $printimagecapture.find('.signaturecapture').attr('data-status', 'image');
                        $printimagecapture.find('.signaturecapture').empty().append($image);
                        $printimagecapture.signaturecapturedone();
                        setTimeout(function () {
                            $image.css('width', $printimagecapture.find('.signaturecapture').width())
                        }, 100);
                    } else {
                        FwNotification.renderNotification('ERROR', 'Signature is required');
                    }
                }
            },
            {
                caption:     'Cancel E-Mail',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       2,
                buttonclick: function () {
                    program.navigate('home/home');
                }
            },
            {
                caption:     'Send Email',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       2,
                buttonclick: function () {
                    if (FwFormField.getValue($printimagecapture, 'div[data-datafield="to"]') !== '') {
                        var request = {
                            contractId: properties.contract.contractId,
                            from:       FwFormField.getValue($printimagecapture, 'div[data-datafield="from"]'),
                            to:         FwFormField.getValue($printimagecapture, 'div[data-datafield="to"]'),
                            cc:         FwFormField.getValue($printimagecapture, 'div[data-datafield="cc"]'),
                            subject:    FwFormField.getValue($printimagecapture, 'div[data-datafield="subject"]'),
                            body:       FwFormField.getValue($printimagecapture, 'div[data-datafield="body"]')
                        };
                        RwServices.callMethod('ContractSignature', 'SendEmail', request,
                            function resolve(response) {
                                program.navigate('home/home');
                                FwNotification.renderNotification('SUCCESS', 'E-Mail sent');
                            });
                    } else {
                        FwNotification.renderNotification('ERROR', 'Enter an email address');
                    }
                }
            },
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       3,
                buttonclick: function () {
                    $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol('changeState', 2);
                    $printimagecapture.find('#pic-email').show();
                    $printimagecapture.find('#pic-emailto').hide();
                }
            }
        ]
    });
    $printimagecapture.showscreen = function () {
        $printimagecapture.find('.pictures').append($printimagecapture.addblankimage());
        $printimagecapture.find('#pic-signaturecapture-canvasSignature')[0].onselectstart = function () { return false; }
        $printimagecapture.signaturePad();
        $printimagecapture.show();
        $printimagecapture.blanksignature = $printimagecapture.signaturePad().getSignatureImage('image/jpeg').replace('data:image/jpeg;base64,', '');
    };
    $printimagecapture.addblankimage = function() {
        var html = [];

        html.push('<div class="contract-photo" data-status="empty">');
        html.push('  <i class="material-icons">add_a_photo</i>');
        html.push('</div>');

        return jQuery(html.join(''));
    };
    $printimagecapture.onWindowResize = function() {
        var width = screen.$view.find('#contractSignature').width();
        if (width > window.innerWidth) {
            width = window.innerWidth;
        }
        $printimagecapture.find('#pic-signaturecapture-canvasSignature').attr('width', width);
        $printimagecapture.find('#pic-signaturecapture-clear').click();
    };
    $printimagecapture.signaturecapturedone = function () {
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('portrait-primary');
        }
        window.removeEventListener('resize', $printimagecapture.onWindowResize, false);
        $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol('changeState', 0);
        $printimagecapture.find('#pic-signaturecapture').hide();
        $printimagecapture.find('#printimagecapture').show();
    };
    $printimagecapture
        .on('click', '.signaturecapture', function () {
            window.addEventListener('resize', $printimagecapture.onWindowResize, false);
            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                window.screen.lockOrientation('landscape-primary');
                window.setTimeout(function() {
                    $printimagecapture.onWindowResize();
                }, 500);
            } else {
                $printimagecapture.onWindowResize();
            }
            $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol('changeState', 1);
            $printimagecapture.find('#pic-signaturecapture').show();
            $printimagecapture.find('#printimagecapture').hide();
        })
        .on('click', '.contract-photo[data-status="empty"]', function () {
            var $this = jQuery(this);
            try {
                if (typeof navigator.camera !== 'undefined' && program.hasCamera) {
                    navigator.camera.getPicture(
                        //success
                        function (imageData) {
                            try {
                                if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                    window.screen.lockOrientation('portrait-primary');
                                }
                                var $image = jQuery('<img>')
                                    .attr('src', 'data:image/jpeg;base64,' + imageData)
                                    .data('base64', imageData);
                                $this.attr('data-status', 'image');
                                $this.empty().append($image);
                                $printimagecapture.find('.pictures').append($printimagecapture.addblankimage());
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        },
                        //error
                        function (message) {
                            try {
                                if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                    window.screen.lockOrientation('portrait-primary');
                                }
                                FwNotification.renderNotification('ERROR', message);
                            } catch (ex) {
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
                    FwNotification.renderNotification('ERROR', 'Taking pictures is not supported on this device.');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.contract-photo[data-status="image"]', function () {
            var $this         = jQuery(this);
            var $confirmation = FwConfirmation.renderConfirmation('Remove Image?', '');
            var $remove       = FwConfirmation.addButton($confirmation, 'Remove', true);
            var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

            $remove.on('click', function () {
                $this.remove();
            });
        })
        .on('click', '.emailsearch', function() {
            var $this   = jQuery(this);
            var request = {
                contractid:          properties.contract.contractId,
                orderid:             properties.contract.orderId
            };
            RwServices.callMethod('ContractSignature', 'LoadContactEmails', request, function (response) {
                if (response.contacts.length > 0) {
                    $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol('changeState', 3);
                    $printimagecapture.find('#pic-email').hide();
                    $printimagecapture.find('#pic-emailto').empty().show();

                    for (var i = 0; i < response.contacts.length; i++) {
                        var html = [];
                        html.push('<div class="record">');
                        html.push('  <div class="data"><div class="caption">Name:</div>' + response.contacts[i].namefml + '</div>');
                        html.push('  <div class="data"><div class="caption">Title:</div>' + response.contacts[i].jobtitle + '</div>');
                        html.push('  <div class="data"><div class="caption">E-Mail:</div>' + response.contacts[i].email + '</div>');
                        html.push('</div>');

                        var $record = jQuery(html.join(''));
                        $record.data('recorddata', response.contacts[i]);

                        $printimagecapture.find('#pic-emailto').append($record);

                        $record.on('click', function () {
                            var $targetfield;
                            if ($this.hasClass('to')) {
                                $targetfield = $printimagecapture.find('div[data-datafield="to"]');
                            } else if ($this.hasClass('cc')) {
                                $targetfield = $printimagecapture.find('div[data-datafield="cc"]');
                            }
                            FwFormField.setValue2($targetfield, FwFormField.getValue2($targetfield) + jQuery(this).data('recorddata').email + ';');
                            $printimagecapture.find('#printimagecaptureecontroller').fwmobilemodulecontrol('changeState', 2);
                            $printimagecapture.find('#pic-email').show();
                            $printimagecapture.find('#pic-emailto').hide();
                        });
                    }
                } else {
                    FwNotification.renderNotification('ERROR', 'No contacts found on order');
                }
            });
        })
    ;

    $signaturecapture.find('#signaturecaptureecontroller').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    $signaturecapture.leavescreen();
                    program.popScreen();
                }
            },
            {
                caption:     'Create Contract',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       0,
                buttonclick: function () {
                    try {
                        var signatureImage = $signaturecapture.signaturePad().getSignatureImage('image/jpeg').replace('data:image/jpeg;base64,', '');
                        var request = {
                            contractId:          properties.contract.contractId,
                            contractType:        properties.contract.contractType,
                            orderId:             properties.contract.orderId,
                            responsiblePersonId: properties.contract.responsiblePersonId,
                            signatureImage:      signatureImage
                        };
                        RwServices.callMethod('ContractSignature', 'CreateContract', request, function (response) {
                            if (response.createcontract.status === 0) {
                                if (properties.contract.contractType !== 'OUT') {
                                    $signaturecapture.leavescreen();
                                }
                                FwFunc.showMessage(response.createcontract.msg, function() {
                                    program.navigate('home/home');
                                });
                            } else {
                                FwFunc.showError(response.createcontract.msg);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                }
            }
        ]
    });
    $signaturecapture.showscreen = function () {
        $signaturecapture.find('#signaturecapture-canvasSignature')[0].onselectstart = function () { return false; }
        $signaturecapture.signaturePad();
        window.addEventListener('resize', $signaturecapture.onWindowResize, false);
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('landscape-primary');
            window.setTimeout(function() {
                $signaturecapture.onWindowResize();
            }, 500);
        } else {
            $signaturecapture.onWindowResize();
        }

        $signaturecapture.show();
    };
    $signaturecapture.onWindowResize = function() {
        var width = screen.$view.find('#contractSignature').width() - 6;
        if (width > window.innerWidth) {
            width = window.innerWidth - 6;
        }
        $signaturecapture.find('#signaturecapture-canvasSignature')[0].width = width;
        $signaturecapture.find('#signaturecapture-clear').click();
    };
    $signaturecapture.leavescreen = function () {
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('portrait-primary');
        }
        window.removeEventListener('resize', $signaturecapture.onWindowResize, false);
    };

    screen.load = function() {
        if (properties.contract.contractType === 'OUT') {
            $printimagecapture.showscreen();
        } else {
            $signaturecapture.showscreen();
        }
    };

    screen.unload = function() {

    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------



RwOrderController.debugContactSignatureScreen = function (contracttype) {
    var properties = {};
    properties.contract = {contractType: contracttype};
    program.pushScreen(RwOrderController.getContactSignatureScreen({}, properties))
};