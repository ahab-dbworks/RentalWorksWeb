class FwMobileModuleTabs {
    tabs: any = {};
    $element: JQuery;
    _options: any;
    defaults = {
        tabs: []
    };
    
    constructor(element, options) {
        this._process_options(options);
        this.$element = jQuery(element);
    
        this._renderControl();
        this._bindEvents();
    }

    _process_options(options) {
        this._options = jQuery.extend({}, this._options, options);
    }

    _renderControl() {
        var plugin = this;
        this.$element.addClass('fwmobilemoduletabs');

        if (this._options.tabs.length > 0) {
            for (var i = 0; i < this._options.tabs.length; i++) {
                var $option = jQuery('<div/>', {
                    id:             this._options.tabs[i].id,
                    'class':        'tab',
                    'data-index':   i,
                    'data-caption': this._options.tabs[i].caption,
                    html:           this._options.tabs[i].caption
                }).appendTo(this.$element);
            }
            this.tabs = this.$element.find('.tab');
        }
    }

    _bindEvents() {
        var plugin    = this,
            $window   = jQuery(window),
            $document = jQuery(document);

        plugin.$element
            .on('click', '.tab', function() {
                var $this, $tabs;
                $this = jQuery(this);
                plugin.tabs.each(function(index, element) {
                    var $tab;
                    $tab = jQuery(element);
                    $tab.removeClass('active');
                });
                $this.addClass('active');
                plugin._options.tabs[jQuery(this).attr('data-index')].buttonclick.call(plugin);
            })
        ;
    }

    hideTab(id) {
        this.$element.find(id).hide();
    }

    showTab(id) {
        this.$element.find(id).show();
    }

    clickTab(id) {
        this.$element.find(id).click();
    }

    isActive(id) {
        return this.$element.find(id).hasClass('active');
    }
}

(<any>jQuery.fn).fwmobilemoduletabs = function(option) {
    var args = Array.apply(null, arguments);
    args.shift();
    var internal_return;
    this.each(function(){
        var $this = jQuery(this),
            data = $this.data('fwmobilemoduletabs'),
            options = typeof option === 'object' && option;
        if (!data) {
            var opts = jQuery.extend({}, this.defaults, options); // Options priority: js args, defaults
            $this.data('fwmobilemoduletabs', (data = new FwMobileModuleTabs(this, opts)));
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
