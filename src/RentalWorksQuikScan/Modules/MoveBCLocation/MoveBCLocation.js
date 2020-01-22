//----------------------------------------------------------------------------------------------
RwInventoryController.getMoveBCLocationScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $aisleshelfinfo, $movebclocationstatus, screendata = {};
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Move To Aisle/Shelf')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-moveBCLocation').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    $aisleshelfinfo       = screen.$view.find('.aisleshelf-info');
    $movebclocationstatus = screen.$view.find('.movebclocation-status');
    
    screen.$view
        .on('change', '.fwmobilecontrol-value', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                screen.$view.find('#moveBCLocationView-msg').hide().removeClass('qserror qssuccess');;
                screen.$view.find('#moveBCLocationView-msg .msg').html('');
                if (($this.val() !== '') && ($this.val().indexOf('-') === -1)) {
                    if ((screendata.aisle !== '') && (screendata.shelf != '')) {
                        request = {
                            barcode:  RwAppData.stripBarcode($this.val().toUpperCase()),
                            aisle:    screendata.aisle,
                            shelf:    screendata.shelf
                        };
                        RwServices.callMethod("MoveBCLocation", "BarcodeMove", request, function(response) {
                            $movebclocationstatus.removeClass('success error');
                            if (response.barcodemove.status == '0') {
                                $movebclocationstatus.show().addClass('success');
                                $movebclocationstatus.find('.msg').html('Item (' + request.barcode + ') moved to aisle: ' + request.aisle + ' shelf: ' + request.shelf);
                                program.playStatus(true);
                            } else if (response.barcodemove.status !== '0') {
                                $movebclocationstatus.show().addClass('error');
                                $movebclocationstatus.find('.msg').html(response.barcodemove.msg);
                                program.playStatus(false);
                            }
                        });
                    } else {
                        $movebclocationstatus.show().addClass('error');
                        $movebclocationstatus.find('.msg').html('You must set a aisle/shelf before scanning an item.');
                        program.playStatus(false);
                    }
                } else {
                    screendata.aisle = $this.val().split('-')[0].toUpperCase();
                    screendata.shelf = $this.val().split('-')[1].toUpperCase();
                    $aisleshelfinfo.removeClass('notset');
                    $aisleshelfinfo.find('.aisle .value').html(screendata.aisle);
                    $aisleshelfinfo.find('.shelf .value').html(screendata.shelf);
                    $this.val('');
                    $movebclocationstatus.hide();
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        program.setScanTarget('.fwmobilecontrol-value');
        program.setScanTargetLpNearfield('');
        screendata.aisle = '';
        screendata.shelf = '';
    };

    screen.unload = function () {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        program.setScanTargetLpNearfield('');
    }

    return screen;
};