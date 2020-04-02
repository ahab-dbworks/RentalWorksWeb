class RwVirtualNumpadClass {
    init(inputSelector: string|JQuery) {
        let $txt = null;
        if (typeof inputSelector === 'string') {
            $txt = jQuery(inputSelector);
        }
        else if (typeof inputSelector === 'object') {
            $txt = inputSelector;
        }
        $txt
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
            let $barcodewrapper = jQuery('#scanBarcodeView .barcodewrapper');
            this.addNumPadButton($barcodewrapper, $txt);
        }
        else if ($txt.closest('.fwmobilesearch').length > 0) {
            const $element = $txt.closest('.searchinput');
            this.addNumPadButton($element, $txt);
        }
    }

    addNumPadButton($element: JQuery, $txt: JQuery) {
        if (typeof $element === 'object' && typeof $element.length === 'number' && $element.length > 0 && $element.find('.numpad').length === 0) {
            var $numPadButton = jQuery('<div class="numpad" style="display: block;color:#000000;"><i class="material-icons">\uE0BC</i><!--cancel--></div>')
                .on('click', function () {
                    var kb = $txt.val('').getkeyboard();
                    // close the keyboard if the keyboard is visible and the button is clicked a second time
                    if (kb.isOpen) {
                        kb.close();
                    } else {
                        kb.reveal();
                    }
                });
            $element.append($numPadButton);
        }    
    }
}

var RwVirtualNumpad = new RwVirtualNumpadClass();