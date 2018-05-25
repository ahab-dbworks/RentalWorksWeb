﻿//----------------------------------------------------------------------------------------------
RwAccountController.getPreferencesScreen = function(viewModel, properties) {
    var combinedViewModel, screen,$fwcontrols;
    combinedViewModel = jQuery.extend({
        captionPageTitle: RwLanguages.translate('Settings'),
        captionScanMode:  RwLanguages.translate('Scan Mode')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-account-preferences').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    $scanmode = screen.$view.find('#preferencesView-scanMode');
    FwFormField.loadItems($scanmode, [
        {value:'MODE_SINGLE_SCAN',              text:'Single Scan'},
        {value:'MODE_MULTI_SCAN',               text:'Multi-Scan'},
        //{value:'MODE_MOTION_DETECT',            text:'Motion Detect'},
        //{value:'MODE_SINGLE_SCAN_RELEASE',      text:'Single Scan Release'},
        {value:'MODE_MULTI_SCAN_NO_DUPLICATES', text:'Multi-Scan No Duplicates'}
    ], true);

    screen.$view
        .on('change', '#preferencesView-scanMode .fwformfield-value', function() {
            try {
                localStorage.setItem('barcodeScanMode', jQuery(this).val());
                if (typeof window.DTDevices === 'object') {
                    window.DTDevices.barcodeSetScanMode(jQuery(this).val());
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    screen.load = function() {
        if (typeof localStorage.getItem('barcodeScanMode') !== 'string') {
            localStorage.setItem('barcodeScanMode', 'MODE_SINGLE_SCAN');
        }
        FwFormField.setValue(screen.$view, '#preferencesView-scanMode', localStorage.getItem('barcodeScanMode'));

        if (typeof window.TslReader === 'object' && typeof window.TslReader.getPowerLevel === 'function') {
            window.TslReader.getPowerLevel(
                function success(args) {
                    var outputPower = args[1];
                    var minPower = args[2];
                    var maxPower = args[3];
                    var rfidPowerLevelSlider = document.getElementById('slider');
                    noUiSlider.create(rfidPowerLevelSlider, {
                        start: [outputPower],
                        range: {
                            'min': [minPower],
                            'max': [maxPower]
                        },
                        tooltips: [wNumb({ decimals: 0 })]
                    });
                    rfidPowerLevelSlider.noUiSlider.on('change', function () {
                        var rfidPowerLevel = parseFloat(rfidPowerLevelSlider.noUiSlider.get());
                        if (typeof window.TslReader === 'object') {
                            window.TslReader.setPowerLevel(rfidPowerLevel);
                        }
                    });
                    jQuery('.tslsettings').show();
                })
            ;
        }

        if (typeof window.DTDevices === 'object' && typeof window.DTDevices.barcodeSetScanMode === 'function') {
            jQuery('.lineaprosettings').show();
        }
    };

    return screen;
}