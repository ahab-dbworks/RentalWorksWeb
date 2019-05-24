;(function ( $, window, document, undefined ) {

    "use strict";

    var FwMobileSearch = function(element, options) {
        this._process_options(options);
        this.$element = $(element);

        this._renderControl();
        this._bindEvents();
    };

    FwMobileSearch.prototype = {
        constructor:        FwMobileSearch,
        _searchCalled:      false,
        _throttleTimer:     null,
        _throttleDelay:     100,
        _totalPages:        0,
        _pageNo:            1,
        _builtItemTemplate: null,
        _searchModesLookup: {},
        _process_options: function(options) {
            this._options = $.extend({}, this._options, options);
            if (this._options.cacheItemTemplate) {
                this._builtItemTemplate = this._options.itemTemplate();
            }
            this._searchModesLookup = {};
            for(var i = 0; i < this._options.searchModes.length; i++) {
                this._searchModesLookup[this._options.searchModes[i].value] = this._options.searchModes[i];
            }
        },
        _renderControl: function() {
            var html = [];
            this.$element.addClass('fwmobilesearch');

            if (this._options.searchModes.length > 0) {
                html.push('<div class="searchinput">');
                html.push('  <i class="material-icons md-dark">&#xE8B6;</i>'); //search
                html.push('  <input class="searchbox" type="text" placeholder="' + this._options.placeholder + '" />');
                html.push('  <i class="material-icons md-dark clear">&#xE14C;</i>'); //clear
                html.push('</div>');
                html.push('<div class="options"></div>');
            }
            html.push('<div class="searchresults"></div>');
            html.push('<div class="searchfooter">');
            html.push('  <div class="recordcount"></div>');
            html.push('  <div class="pager"></div>');
            html.push('</div>');

            this.$element.append(html.join(''));

            if (this._options.searchModes.length > 0) {
                for (var i = 0; i < this._options.searchModes.length; i++) {
                    var isvisible = (typeof this._options.searchModes[i].visible === 'undefined') || this._options.searchModes[i].visible;
                    var placeholder = (typeof this._options.searchModes[i].placeholder !== 'undefined') ? this._options.searchModes[i].placeholder : this._options.searchModes[i].caption;
                    var $option = jQuery('<div class="option" data-value="' + this._options.searchModes[i].value + '" data-placeholder="' + placeholder + '">' + this._options.searchModes[i].caption + '</div>');
                    if (!isvisible || this._options.searchModes.length === 1) {
                        $option.hide();
                    }
                    this.$element.find('.options').append($option);
                }

                //Set the first option
                jQuery(this.$element.find('.option')[0]).addClass('active');
                this._options.currentMode = jQuery(this.$element.find('.option')[0]).attr('data-value');
                this.$element.find('.searchbox').attr('placeholder', jQuery(this.$element.find('.option')[0]).attr('data-placeholder'));
            }

            if (this._options.upperCase) {
                this.$element.find('.searchbox').addClass('toupper');
            }
        },
        _bindEvents: function() {
            var plugin    = this,
                $window   = jQuery(window),
                $document = jQuery(document);

            plugin.$element
                .on('click', '.option', function() {
                    try {
                        var $this = jQuery(this);
                        $this.siblings().removeClass('active');
                        $this.addClass('active');
                        plugin._options.currentMode = $this.attr('data-value');
                        plugin.$element.attr('data-mode', plugin._options.currentMode);
                        plugin.$element.find('.searchbox').attr('placeholder', $this.attr('data-placeholder'))
                                                          .select();
                        //plugin._clearSearchResults();
                        if (typeof plugin._searchModesLookup[plugin._options.currentMode].click === 'function') {
                            plugin._searchModesLookup[plugin._options.currentMode].click();
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('change', '.searchbox', function() {
                    try {
                        if (this.value !== '') {
                            plugin.$element.find('.clear').addClass('visible');
                        } else {
                            plugin.$element.find('.clear').removeClass('visible');
                        }
                        if (plugin._searchCalled !== true) {
                            plugin._clearSearchResults();
                            plugin._search();
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('blur', '.searchbox', function() {
                    try {
                        plugin._searchCalled = false;
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('input', '.searchbox', function() {
                    try {
                        if (this.value !== '') {
                            plugin.$element.find('.clear').addClass('visible');
                        } else {
                            plugin.$element.find('.clear').removeClass('visible');
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('keydown', function(event) {
                    try {
                        if (event.keyCode === 13) {
                            plugin._searchCalled = true;
                            plugin._clearSearchResults();
                            plugin._search();
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '.pager', function() {
                    try {
                        plugin._nextpage();
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '.clear', function() {
                    try {
                        plugin.$element.find('.searchbox').val('').focus();
                        $(this).removeClass('visible');
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
        },
        _load: function(searchresults) {
            var $record, plugin;
            plugin = this;
            if (searchresults !== null) {
                this._totalPages = searchresults.TotalPages;
                jQuery(window).off('scroll');
                if (this._totalPages > 1) {
                    jQuery(window).on('scroll', function() {
                        var $window = jQuery(window);
                        var $document = jQuery(document);
                        clearTimeout(plugin._throttleTimer);
                        plugin._throttleTimer = setTimeout(function () {
                            if ($window.scrollTop() + $window.height() > $document.height() - 100) {
                                plugin._nextpage();
                            }
                        }, plugin._throttleDelay);
                    });
                }
                for (var rowno = 0; rowno < searchresults.Rows.length; rowno++) {
                    var itemmodel = {};
                    for (var colname in searchresults.ColumnIndex) {
                        itemmodel[colname] = searchresults.Rows[rowno][searchresults.ColumnIndex[colname]];
                    }
                    if (this._options.cacheItemTemplate) {
                        $record = jQuery(Mustache.render(this._builtItemTemplate, itemmodel));
                    } else {
                        $record = jQuery(Mustache.render(this._options.itemTemplate(itemmodel), itemmodel));
                    }
                    if (typeof plugin._options.recordClick === 'function') {
                        $record.css({ 'cursor': 'pointer' });
                    }
                    $record.data('recorddata', itemmodel);
                    $record.on('click', function(e) {
                        try {
                            jQuery(window).off('scroll');
                            if (typeof plugin._options.recordClick === 'function') {
                                plugin._options.recordClick.call(plugin, $(this).data('recorddata'), $(this), e);
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    this.$element.find('.searchresults').append($record);
                }
            }
            this._updateSearchFooter(searchresults);
        },
        _clearSearchResults: function() {
            this._totalPages = 0;
            this._pageNo     = 1;
            this.$element.find('.searchresults').empty();
            this.$element.find('.searchfooter .recordcount').empty();
            this.$element.find('.searchfooter .pager').empty();
        },
        _updateSearchFooter: function(searchresults) {
            if (searchresults.PageNo < searchresults.TotalPages) {
                this.$element.find('.searchfooter .recordcount').html(searchresults.PageNo * searchresults.PageSize + ' of ' + searchresults.TotalRows + ' items');
                var remaining = searchresults.TotalRows - (searchresults.PageNo * searchresults.PageSize);
                if (searchresults.PageSize >= remaining) {
                    this.$element.find('.searchfooter .pager').html('Load last ' + remaining + ' records...');
                } else {
                    this.$element.find('.searchfooter .pager').html('Load next ' + searchresults.PageSize + ' records...');
                }
            } else {
                this.$element.find('.searchfooter .recordcount').html(searchresults.TotalRows + ' items');
                this.$element.find('.searchfooter .pager').empty();
            }
        },
        _search: function() {
            var plugin = this;
            plugin.$element.find('.searchbox').blur();
            plugin._options.beforeSearch();
            var funcCustomSearch = null;
            if ((typeof plugin._options.currentMode === 'string') && (typeof plugin._searchModesLookup[typeof plugin._options.currentMode] === 'object')) {
                plugin._searchModesLookup[plugin._options.currentMode].search;
            }
            var hasCustomSearch  = typeof funcCustomSearch === 'function';
            var searchvalue      = plugin.$element.find('.searchbox').val();
            if (hasCustomSearch) {
                funcCustomSearch(searchvalue);
            } else {
                plugin._options.request             = plugin._options.getRequest();
                plugin._options.request.searchvalue = searchvalue;
                plugin._options.request.searchmode  = plugin._options.currentMode;
                plugin._options.request.pagesize    = plugin._options.pageSize;
                plugin._options.request.pageno      = plugin._pageNo;
                FwServices.callMethod(plugin._options.service, plugin._options.method, plugin._options.request, plugin._options.queryTimeout, null, function(response) {
                    try {
                        plugin._load(response.searchresults);
                        plugin._options.afterLoad(plugin, response);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        },
        _nextpage: function() {
            var plugin = this;
            var pageno     = plugin._pageNo + 1;
            var totalpages = plugin._totalPages;
            if (pageno <= totalpages) {
                plugin._pageNo = pageno;
                plugin._search();
            }
        },

        search: function() {
            this._clearSearchResults();
            this._search();
        },
        load: function(searchresults) {
            this._clearSearchResults();
            this._load(searchresults);
        },
        destroy: function() {
            jQuery(window).off('scroll');
        },
        setsearchmode: function(searchmode) {
            this.$element.find('.option[data-value="' + searchmode + '"]').trigger('click');
        },
        clearsearchresults: function() {
            this._clearSearchResults();
        },
        clearsearchbox: function () {
            var plugin = this;
            plugin.$element.find('.searchbox').val('');
        },
        getSearchText: function () {
            var searchText = this.$element.find('.searchbox').val();
            return searchText;
        },
        getSearchOption: function () {
            var searchOption = this.$element.find('.option.active').attr('data-value');
            return searchOption;
        }
    };

    $.fn.fwmobilesearch = function(option) {
        var args = Array.apply(null, arguments);
        args.shift();
        var internal_return;
        this.each(function(){
            var $this = $(this),
                data = $this.data('fwmobilesearch'),
                options = typeof option === 'object' && option;
            if (!data) {
                var opts = $.extend({}, defaults, options); // Options priority: js args, defaults
                $this.data('fwmobilesearch', (data = new FwMobileSearch(this, opts)));
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

    var defaults = $.fn.fwmobilesearch.defaults = {
        service:           '',
        method:            '',
        placeholder:       '',
        currentMode:       '',
        searchModes:       [],
        upperCase:         false,
        getRequest:        function() { return {}; },
        request:           {},
        cacheItemTemplate: false,
        itemTemplate:      function(itemmodel) { return ''; },
        beforeSearch:      function() {},
        pageSize:          100,
        recordClick:       null,
        queryTimeout:      null,
        afterLoad: function (plugin, response) { }
    };

})( jQuery, window, document );
 