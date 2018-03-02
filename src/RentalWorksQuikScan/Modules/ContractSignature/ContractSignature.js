//----------------------------------------------------------------------------------------------
RwOrderController.getContactSignatureScreen = function(viewModel, properties) {
    var combinedViewModel, screen, request, pageSubTitle, $canvas, canvas, context, drawer;
    pageSubTitle = '';
    combinedViewModel = jQuery.extend({
        captionPageTitle:      RwOrderController.getPageTitle(properties)
      , captionPageSubTitle:   pageSubTitle
      , captionClearSignature: RwLanguages.translate('Clear Signature')
      , captionCancel:         RwLanguages.translate('Cancel')
      , captionCreateContract: RwLanguages.translate('Create Contract')
      , captionSignature:      RwLanguages.translate('Signature')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-contractSignature').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    $canvas = screen.$view.find('#contractSignatureView-canvasSignature');
    canvas = $canvas[0];
    canvas.onselectstart = function () { return false; }

    screen.$btncancel = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', true, function() { //back
        //program.navigate('home/home');
        program.popScreen();
    });

    screen.$btnsave = FwMobileMasterController.addFormControl(screen, 'Create Contract', 'right', '&#xE161;', true, function() { //save
        var requestCreateContract, signatureOnTransparentCanvas, signatureImage, context, compositeOperation;
        try {
            signatureImage = jQuery('#contractSignatureView').signaturePad().getSignatureImage('image/jpeg').replace('data:image/jpeg;base64,', '');
            requestCreateContract = {
                contractId:          properties.contract.contractId
                , contractType:        properties.contract.contractType
                , orderId:             properties.contract.orderId
                , responsiblePersonId: properties.contract.responsiblePersonId
                , signatureImage:      signatureImage
            };
            RwServices.order.createContract(requestCreateContract, function(responseCreateContract) {
                    if (responseCreateContract.webCreateContract.status === 0) {
                        FwFunc.showMessage(responseCreateContract.webCreateContract.msg, function() {
                            program.navigate('home/home');
                        });
                    } else {
                        FwFunc.showError(responseCreateContract.webCreateContract.msg);
                    }
                }
            );
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.onWindowResize = function() {
        screen.$view.find('#contractSignatureView').show();
        var width = screen.$view.find('#contractSignatureView').width() - 6;
        if (width > window.innerWidth) {
            width = window.innerWidth - 6;
        }
        canvas.width = width;
        screen.$view.find('#contractSignatureView-btnClear').click();
    };

    screen.load = function() {
        window.addEventListener('resize', screen.onWindowResize, false);
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('landscape-primary');
            window.setTimeout(function() {
                screen.onWindowResize();
            }, 500);
        } else {
            screen.onWindowResize();
        }
        screen.$view.find('#contractSignatureView').signaturePad();
    };

    screen.unload = function() {
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('portrait-primary');
        }
        window.removeEventListener('resize', screen.onWindowResize, false);
    };
    
    return screen;
};