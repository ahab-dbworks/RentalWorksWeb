class RwVirtualNumpadClass {
    init(inputSelector: string) {
        jQuery(inputSelector)
           .keyboard({
                openOn : '',
                stayOpen : true,
                autoAccept: false,
                usePreview: true,
                initialFocus: false,
                noFocus: true,
                display: {
                    'bksp': "\u232b"
                },
                layout: 'custom',
                customLayout: {
                    'normal': [
                    '1 2 3',
                    '4 5 6',
                    '7 8 9',
                    '{sp:3} 0 {bksp}',
                    '{accept} {sp:1} {cancel}'
                    ]
                },
                initialized: function (e, keyboard, el) {
                    (<any>keyboard).$el.prop('readonly', false);
                },
                beforeVisible: function(e, keyboard, el) {
                    keyboard.$el.prop('readonly', true);
                    keyboard.$preview
                        .prop('readonly', true)
                        .removeClass('textbox')
                        .removeClass('scanTarget');
                },
                visible: function(e, keyboard, el) {
                    keyboard.el.blur();
                },
                beforeClose: function(e, keyboard, el) {
                    keyboard.$el.prop('readonly', false);
                    keyboard.$preview.prop('readonly', true)
                }
            });
        if (inputSelector === '#scanBarcodeView-txtBarcodeData') {
            var $numPadButton = jQuery('<div class="numpad" style="display: block;"><i class="material-icons">\uE0BC</i><!--cancel--></div>')
                .on('click', function() {
                    var kb = jQuery('#scanBarcodeView-txtBarcodeData').val('').getkeyboard();
	                // close the keyboard if the keyboard is visible and the button is clicked a second time
	                if ( kb.isOpen ) {
		                kb.close();
	                } else {
		                kb.reveal();
	                }
                });
            let $barcodewrapper = jQuery('#scanBarcodeView .barcodewrapper');
            if ($barcodewrapper.find('.numpad').length === 0) {
                jQuery('#scanBarcodeView .barcodewrapper').append($numPadButton);
            }
        }
    }
}

var RwVirtualNumpad = new RwVirtualNumpadClass();