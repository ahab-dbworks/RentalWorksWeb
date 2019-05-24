;(function ( $, window, document, undefined ) {

    "use strict";

    var FwMobileModuleControl = function(element, options) {
        this._process_options(options);
        this.$element = $(element);

        this._renderControl();
        this._bindEvents();
        this._stateChange();
    };

    FwMobileModuleControl.prototype = {
        constructor:        FwMobileModuleControl,
        _state:             0,
        _buttons:           {},
        _process_options: function(options) {
            this._options = $.extend({}, this._options, options);
        },
        _renderControl: function() {
            var plugin = this;
            this.$element.addClass('fwmobilemodulecontrol');

            if (this._options.buttons.length > 0) {
                for (var i = 0; i < this._options.buttons.length; i++) {
                    var button = this._options.buttons[i];
                    if (typeof button.id === 'undefined') {
                        button.id = this._generateUID();
                    }
                    if (button.type == 'menu') {
                        var $menu = $('<div/>', {
                            id:           button.id,
                            'class':      'btn menu ' + button.orientation,
                            'data-index': i,
                            'data-state': button.state,
                            html:         '<i class="material-icons">' + button.icon + '</i><div class="menu-dropdown"></div>'
                        }).appendTo(this.$element);
                        for (var j = 0; j < button.menuoptions.length; j++) {
                            var menuoption  = button.menuoptions[j];
                            var $menuoption = $('<div/>', {
                                id:             menuoption.id,
                                'class':        'menu-dropdown-btn',
                                'data-index':   j,
                                'data-caption': menuoption.caption,
                                html:           menuoption.caption
                            }).appendTo($menu.find('.menu-dropdown'));
                        }
                    } else if ((button.type == 'standard') || (typeof button.type == 'undefined')) {
                        var $option = $('<div/>', {
                            id:             button.id,
                            'class':        'btn standard ' + button.orientation,
                            'data-index':   i,
                            'data-state':   button.state,
                            'data-caption': button.caption,
                            html:           '<i class="material-icons">' + button.icon + '</i><div class="btncaption">' + button.caption + '</div>'
                        }).appendTo(this.$element);
                    }
                }
                this._buttons = this.$element.find('.btn');
            }
        },
        _generateUID: function() {
            //https://stackoverflow.com/questions/6248666/how-to-generate-short-uid-like-ax4j9z-in-js
            // I generate the UID from two parts here 
            // to ensure the random number provide enough bits.
            var firstPart = (Math.random() * 46656) | 0;
            var secondPart = (Math.random() * 46656) | 0;
            firstPart = ("000" + firstPart.toString(36)).slice(-3);
            secondPart = ("000" + secondPart.toString(36)).slice(-3);
            return firstPart + secondPart;
        },
        _bindEvents: function() {
            var plugin    = this,
                $window   = $(window),
                $document = $(document);

            plugin.$element
                .on('click', '.btn.standard', function() {
                    plugin._options.buttons[jQuery(this).attr('data-index')].buttonclick.call(plugin);
                })
                .on('click', '.btn.menu .material-icons', function() {
                    var $this, maxZIndex;
                    $this = jQuery(this);
                    if (!$this.parent().hasClass('active')) {
                        maxZIndex = FwFunc.getMaxZ('*');
                        $this.parent().find('.menu-dropdown').css('z-index', maxZIndex+1);
                        $this.parent().addClass('active');

                        jQuery(document).one('click', function closeMenu(e) {
                            if ($this.parent().has(e.target).length === 0) {
                                $this.parent().removeClass('active');
                                $this.parent().find('.menu-dropdown').css('z-index', '0');
                            } else if ($this.parent().hasClass('active')) {
                                jQuery(document).one('click', closeMenu);
                            }
                        });
                    } else {
                        $this.parent().removeClass('active');
                        $this.parent().find('.menu-dropdown').css('z-index', '0');
                    }
                })
                .on('click', '.btn.menu .menu-dropdown-btn', function() {
                    jQuery(this).closest('.btn.menu').removeClass('active');
                    jQuery(this).closest('.btn.menu').find('.menu-dropdown').css('z-index', '0');
                    plugin._options.buttons[jQuery(this).closest('.btn.menu').attr('data-index')].menuoptions[jQuery(this).attr('data-index')].buttonclick.call(plugin);
                })
            ;
        },
        _stateChange: function() {
            this._buttons.css('display', 'none');
            for (var buttonno = 0; buttonno < this._options.buttons.length; buttonno++) {
                var button = this._options.buttons[buttonno];
                if (button.state === this._state) {
                    var isVisible = true;
                    if (typeof button.isVisible === 'function') {
                        isVisible = button.isVisible();   
                    }
                    if (isVisible) {
                        this.$element.find(`#${button.id}`).removeAttr('style');
                    }
                }
            }
        },
        _toggleItemlistMenu: function (id) {
            var plugin = this;
            var $menu = plugin.$element.find(id).closest('.menu');
            if ($menu.length) {
                $menu.hide();
                $menu.find('.menu-dropdown-btn').each(function(index, element) {
                     // mv 5/6/19 needed to track the visible state this way for iPod 5 devices, since they were treating css 'display' as 'none' when the menu was 'display:none'
                    if (jQuery(this).data('visible') === true) {
                        $menu.show();
                    }
                });
            }
        },

        changeState: function(state) {
            this._state = state;
            this._stateChange();
        },
        nextState: function() {
            this._state = this._state + 1;
            this._stateChange();
        },
        previousState: function() {
            this._state = this._state - 1;
            this._stateChange();
        },
        hideButton: function(id) {
            this.$element.find(id).data('visible', false); // don't remove
            this.$element.find(id).hide();
            this._toggleItemlistMenu(id);
        },
        showButton: function(id) {
            this.$element.find(id).data('visible', true);  // don't remove
            this.$element.find(id).show();
            this._toggleItemlistMenu(id);
        }
    };

    $.fn.fwmobilemodulecontrol = function(option) {
        var args = Array.apply(null, arguments);
        args.shift();
        var internal_return;
        this.each(function(){
            var $this = $(this),
                data = $this.data('fwmobilemodulecontrol'),
                options = typeof option === 'object' && option;
            if (!data) {
                var opts = $.extend({}, defaults, options); // Options priority: js args, defaults
                $this.data('fwmobilemodulecontrol', (data = new FwMobileModuleControl(this, opts)));
            }
            if (typeof option === 'string' && typeof data[option] === 'function'){
                internal_return = data[option].apply(data, args);
                if (internal_return !== undefined)
                    return false;
            }
        });
        if (internal_return !== undefined)
            return internal_return;
        else
            return this;
    };

    var defaults = $.fn.fwmobilemodulecontrol.defaults = {
        buttons: []
    };

})( jQuery, window, document );
