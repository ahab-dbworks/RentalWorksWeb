function FwPopup(options, $this) {
    var me;

    me = this;
    me.settings = jQuery.extend({}, options);
    me.methods = {
        init: function(options, $this) {            
            me.methods.updateOptions(options, $this);
            me.$divOverlay = jQuery('<div class="FwPopup-divOverlay">')
                .attr('id', $this.attr('id') + '-divOverlay')
                .hide()
                .on('click', function() {
                    me.methods.hide(options, $this);
                })
            ;
            me.$divPopup = jQuery('<div class="FwPopup-divPopup">')
                .attr('id', $this.attr('id') + '-divPopup')
                .append($this)
            ;
            me.$divPopup.on('click', function(event) {
                event.stopPropagation();
            });
            jQuery('body').append(me.$divOverlay);
            me.$divOverlay.append(me.$divPopup);
            $this.show();
        }
      , updateOptions: function(options, $this) {
            me.settings = jQuery.extend(me.settings, options);
        }
      , show: function(options, $this) {
            var maxZIndex, $divOverlay, $divPopup;

            if (typeof me.settings.beforeShow !== 'undefined') {  
                me.settings.beforeShow();
            }

            maxZIndex      = FwFunc.getMaxZ('*');
            $divOverlay = jQuery('#' + $this.attr('id') + '-divOverlay');
            $divOverlay.css({
                'z-index': maxZIndex
            });
            if (window.Modernizr.flexbox) {
                $divOverlay.css('display', 'flex');
            } else {
                $divOverlay.css('display', 'block');
            }
            
            maxZIndex++;
            $divPopup = jQuery('#' + $this.attr('id') + '-divPopup');
            $divPopup.css('z-index', maxZIndex);
        }
      , hide: function(options, $this) {
            if (typeof me.settings.beforeHide !== 'undefined') {
                me.settings.beforeHide();
            }
            jQuery('#' + $this.attr('id') + '-divOverlay').hide();
        }
      , destroy: function(options, $this) {
            jQuery('#' + $this.attr('id') + '-divOverlay').remove();
            jQuery('#' + $this.attr('id') + '-divPopup').remove();
        }
    };
}


jQuery.fn.FwPopup = function (method, options) {
    return this.each(function() {
        var $this, context;
        $this = jQuery(this);
        if (!$this.data('plugin-context')) {
            context = new FwPopup(options, '$this');
            $this.data('plugin-context', context);
        }
        else {
            context = $this.data('plugin-context');
            context.settings = jQuery.extend(context.settings, options);
        }
        if ( context.methods[method] ) {
            return context.methods[method](options, $this);
        } else if (typeof method === 'object' || !method) {
            return context.methods.init(options, $this);
        } else {
            jQuery.error('Method ' + method + ' does not exist on jQuery.FwPopup');
        }
    });
}
