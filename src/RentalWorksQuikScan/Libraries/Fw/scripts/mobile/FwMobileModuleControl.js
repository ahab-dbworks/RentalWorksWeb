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
                    if (this._options.buttons[i].type == 'menu') {
                        var $menu = $('<div/>', {
                            id:           this._options.buttons[i].id,
                            'class':      'btn menu ' + this._options.buttons[i].orientation,
                            'data-index': i,
                            'data-state': this._options.buttons[i].state,
                            html:         '<i class="material-icons">' + this._options.buttons[i].icon + '</i><div class="menu-dropdown"></div>'
                        }).appendTo(this.$element);
                        for (var j = 0; j < this._options.buttons[i].menuoptions.length; j++) {
                            var menuoption  = this._options.buttons[i].menuoptions[j];
                            var $menuoption = $('<div/>', {
                                id:             menuoption.id,
                                'class':        'menu-dropdown-btn',
                                'data-index':   j,
                                'data-caption': menuoption.caption,
                                html:           menuoption.caption
                            }).appendTo($menu.find('.menu-dropdown'));
                        }
                    } else if ((this._options.buttons[i].type == 'standard') || (typeof this._options.buttons[i].type == 'undefined')) {
                        var $option = $('<div/>', {
                            id:             this._options.buttons[i].id,
                            'class':        'btn standard ' + this._options.buttons[i].orientation,
                            'data-index':   i,
                            'data-state':   this._options.buttons[i].state,
                            'data-caption': this._options.buttons[i].caption,
                            html:           '<i class="material-icons">' + this._options.buttons[i].icon + '</i><div class="btncaption">' + this._options.buttons[i].caption + '</div>'
                        }).appendTo(this.$element);
                    }
                }
                this._buttons = this.$element.find('.btn');
            }
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
            this.$element.find('.btn[data-state="' + this._state + '"]').removeAttr('style');
        },
        _toggleItemlistMenu: function (id) {
            var plugin = this;
            var $menu = plugin.$element.find(id).closest('.menu');
            if ($menu.length) {
                $menu.hide();
                $menu.find('.menu-dropdown-btn').each(function(index, element) {
                    if (jQuery(this).css('display') != 'none') {
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
            this.$element.find(id).hide();
            this._toggleItemlistMenu(id);
        },
        showButton: function(id) {
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
