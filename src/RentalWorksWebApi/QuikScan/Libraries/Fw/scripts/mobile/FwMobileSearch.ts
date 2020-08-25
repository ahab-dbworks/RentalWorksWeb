class FwMobileSearch {
    _searchCalled: boolean = false;
    _throttleTimer: any = null;
    _throttleDelay: number = 100;
    _totalPages: number =  0;
    _pageNo: number = 1;
    _builtItemTemplate: any = null;
    _searchModesLookup: any = {};
    $element: JQuery = null;
    _options: any = null;
    static defaults: any = {
        service: '',
        method: '',
        placeholder: '',
        currentMode: '',
        searchModes: [],
        upperCase: false,
        getRequest: function () { return {}; },
        request: {},
        cacheItemTemplate: false,
        itemTemplate: function (itemmodel) { return ''; },
        beforeSearch: function () { },
        pageSize: 25,
        hasRecordClick: function (itemmodel) { return true; },
        recordClick: null,
        queryTimeout: null,
        afterLoad: function (plugin, response) { }
    };

    constructor(element, options) {
        this._process_options(options);
        this.$element = jQuery(element);
    
        this._renderControl();
        this._bindEvents();
    }

    _process_options(options) {
        this._options = jQuery.extend({}, this._options, options);
        if (this._options.cacheItemTemplate) {
            this._builtItemTemplate = this._options.itemTemplate();
        }
        this._searchModesLookup = {};
        for (var i = 0; i < this._options.searchModes.length; i++) {
            this._searchModesLookup[this._options.searchModes[i].value] = this._options.searchModes[i];
            if (typeof this._searchModesLookup[this._options.searchModes[i].value].placeholder === 'undefined' && typeof this._searchModesLookup[this._options.searchModes[i].value].caption === 'string') {
                this._searchModesLookup[this._options.searchModes[i].value].placeholder = this._searchModesLookup[this._options.searchModes[i].value].caption;
            }
        }
    }

    _renderControl() {
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

        html.push('<div class="searchheader" style="color:#ffffff;background-color: #333333;">');
        html.push('  <div class="pager">');
        html.push('    <div class="pagingcontrols" style="flex:1 1 0;display:flex;align-items:center;justify-content:center;flex-wrap:wrap;">');
        html.push('      <div style="flex:1 1 0">');
        html.push('        <i class="material-icons btnrefresh" style="font-size:2em;padding:.1em;cursor:pointer;color:#ffffff;">&#xE5D5;</i>');
        html.push('      </div > ');
        html.push('      <i class="material-icons btnfirst" data-enabled="false" style="flex:0 0 auto;font-size:2em;padding:.1em;">&#xE5DC;</i>');
        html.push('      <i class="material-icons btnprev" data-enabled="false" style="flex:0 0 auto;font-size:2em;padding:.1em;">&#xE5CB;</i>');
        html.push('      <input class="pageno" value="-" style="flex:0 0 auto;width:30px;text-align:center;" />');
        html.push('      <span style="flex:0 0 auto;padding:0 .5em;">of</span> <span class="totalpages">-</span>');
        html.push('      <i class="material-icons btnnext" data-enabled="false" style="flex:0 0 auto;font-size:2em;padding:.1em;">&#xE5CC;</i>');
        html.push('      <i class="material-icons btnlast" data-enabled="false" style="flex:0 0 auto;font-size:2em;padding:.1em;">&#xE5DD;</i>');
        html.push('      <div style="flex:1 1 0;text-align:right;">');
        html.push('        <i class="material-icons btntogglepaginginfo" data-expanded="false" style="font-size:2em;padding:.1em;cursor:pointer;color:#ffffff;">&#xE313;</i>');
        html.push('      </div>');
        html.push('    </div >');
        html.push('    <div class="paginginfo" style="display:none;">');
        html.push('      <div style="flex:0 0 auto;min-width:170px;box-sizing:border-box;padding:.2em;display:flex;align-items:center;justify-content:center;flex-wrap:wrap;color:#aaaaaa;font-size:.8em;">');
        html.push('        <span class="rowstart">-</span>');
        html.push('        <span style="padding:0 .5em;">to</span>');
        html.push('        <span class="rowend">-</span>');
        html.push('        <span style="padding:0 .5em;">of</span>');
        html.push('        <span class="totalrows">-</span>');
        html.push('        <select class="pagesize" style="margin: 0 1em 0 1em;"><option value="5">5</option><option value="10">10</option><option value="15">15</option><option value="20">20</option><option value="25">25</option><option value="30">30</option><option value="35">35</option><option value="40">40</option><option value="45">45</option><option value="50">50</option><option value="100">100</option><option value="200">200</option><option value="500">500</option><option value="1000">1000</option></select>');
        //html.push('        <i class="material-icons btntogglesearch" style="font-size:1.8em;padding:.05em;">&#xE313</i>');
        html.push('      </div >');
        html.push('    </div >');
        html.push('  </div >');
        html.push('</div>');

        html.push('<div class="searchresults"></div>');
        this.$element.append(html.join(''));

        this._updatePagerColors();

        this.$element.find('.searchheader .pagesize').val(this._options.pageSize);

        this.$element.on('click', '.searchheader .btntogglepaginginfo', (e) => {
            try {
                const $btn = jQuery(e.currentTarget);
                const $paginginfo = this.$element.find('.paginginfo');
                const isEnabled = $btn.attr('data-enabled') === 'true';
                $btn.attr('data-enabled', (!isEnabled).toString());
                if (isEnabled) {
                    $btn.html('&#xE313;');
                    $paginginfo.hide();
                } else {
                    $btn.html('&#xE316;');
                    $paginginfo.show();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('click', '.btnrefresh', (e) => {
            try {
                //this._blankOutSearchHeader();
                //this.$element.find('.searchheader .pagingcontrols .pageno').val('-');
                this._search();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('click', '.btnfirst', (event) => {
            try {
                const $this = jQuery(event.currentTarget);
                if ($this.data('enabled')) {
                    this._firstpage();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('click', '.btnprev', (event) => {
            try {
                const $this = jQuery(event.currentTarget);
                if ($this.data('enabled')) {
                    this._previouspage();
                }

            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('change', '.pageno', (event) => {
            try {
                const $this = jQuery(event.currentTarget);
                if ($this.data('enabled')) {
                    this._firstpage();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('click', '.btnnext', (event) => {
            try {
                const $this = jQuery(event.currentTarget);
                if ($this.data('enabled')) {
                    this._nextpage();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('click', '.btnlast', (event) => {
            try {
                const $this = jQuery(event.currentTarget);
                if ($this.data('enabled')) {
                    this._lastpage();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        this.$element.on('change', '.pagesize', (event) => {
            try {
                const $this = jQuery(event.currentTarget);
                if ($this.val().toString().length > 0) {
                    const pageSize = parseInt($this.val().toString());
                    if (!isNaN(pageSize)) {
                        this._options.pageSize = pageSize;
                        this._search();
                    }
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

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
            this.setsearchmode(jQuery(this.$element.find('.option')[0]).attr('data-value'));
            this.$element.find('.searchbox').attr('placeholder', jQuery(this.$element.find('.option')[0]).attr('data-placeholder'));
        }

        if (this._options.upperCase) {
            this.$element.find('.searchbox').addClass('toupper');
        }
    }

    _bindEvents() {
        var plugin = this,
            $window = jQuery(window),
            $document = jQuery(document);

        plugin.$element
            .on('click', '.option', function (e) {
                try {
                    const $this = jQuery(e.currentTarget);
                    const searchMode = $this.attr('data-value');
                    plugin.setsearchmode(searchMode);
                    if (typeof plugin._searchModesLookup[plugin._options.currentMode].click === 'function') {
                        plugin._searchModesLookup[plugin._options.currentMode].click(plugin);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('change', '.searchbox', function () {
                try {
                    plugin.setSearchText(this.value);
                    if (plugin._searchCalled !== true) {
                        //plugin._clearSearchResults();
                        plugin._search();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('blur', '.searchbox', function () {
                try {
                    plugin._searchCalled = false;
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('input', '.searchbox', function () {
                try {
                    plugin.setSearchText(this.value);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('keydown', '.searchbox', function (event) {
                try {
                    if (event.keyCode === 13) {
                        plugin._searchCalled = true;
                        //plugin._clearSearchResults();
                        plugin._search();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.clear', function () {
                try {
                    plugin.$element.find('.searchbox').val('').focus();
                    jQuery(this).removeClass('visible');
                    //plugin._clearSearchResults();
                    plugin._search();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;
        if (typeof plugin._options.recordClick === 'function') {
            this.$element.find('.searchresults').on('click', '.item.link', function (e) {
                try {
                    jQuery(window).off('scroll');
                    plugin._options.recordClick.call(plugin, jQuery(this).data('recorddata'), jQuery(this), e);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    }

    _load(searchresults) {
        var $record, plugin;
        plugin = this;
        if (searchresults !== null) {
            this._totalPages = searchresults.TotalPages;
            if (searchresults.PageNo === 0 && searchresults.PageSize === 0) {
                this.$element.find('.searchheader').hide();
            } else {
                this._updateSearchHeader(searchresults);
                this.$element.find('.searchheader').show()
            }
            //jQuery(window).off('scroll');
            //if (this._totalPages > 1) {
            //jQuery(window).on('scroll', function () {
            //    var $window = jQuery(window);
            //    var $document = jQuery(document);
            //    clearTimeout(plugin._throttleTimer);
            //    plugin._throttleTimer = setTimeout(function () {
            //        if ($window.scrollTop() + $window.height() > $document.height() - 100) {
            //            plugin._nextpage();
            //        }
            //    }, plugin._throttleDelay);
            //});
            //}
            this.$element.find('.searchresults').empty().toggle(searchresults.Rows.length > 0);
            for (var rowno = 0; rowno < searchresults.Rows.length; rowno++) {
                var itemmodel = {};
                for (var colname in searchresults.ColumnIndex) {
                    itemmodel[colname] = searchresults.Rows[rowno][searchresults.ColumnIndex[colname]];
                }
                if (this._options.cacheItemTemplate) {
                    $record = jQuery(Mustache.render(this._builtItemTemplate, itemmodel));
                } else {
                    $record = jQuery(Mustache.render(this._options.itemTemplate(itemmodel, searchresults, rowno), itemmodel));
                }
                if (typeof $record.attr('class') === 'undefined' || $record.attr('class').length === 0) {
                    $record.attr('class', 'item');
                } else {
                    $record.attr('class', `item ${$record.attr('class')}`);
                }
                if (typeof plugin._options.recordClick === 'function' && plugin._options.hasRecordClick(itemmodel, searchresults, rowno)) {
                    $record.addClass(`link  ${$record.attr('class')}`);
                    $record.css({ 'cursor': 'pointer' });
                }
                $record.data('recorddata', itemmodel);
                this.$element.find('.searchresults').append($record);
            }
        }
        //this._updateSearchFooter(searchresults);
    }

    _toggleWaitIcons(isWaiting) {
        if (!isWaiting) {
            this.$element.find('.searchheader .btnfirst').html('&#xE5DC;');
            this.$element.find('.searchheader .btnprev').html('&#xE5CB;');
            this.$element.find('.searchheader .btnnext').html('&#xE5CC;');
            this.$element.find('.searchheader .btnlast').html('&#xE5DD;');
            //this.$element.find('.searchheader .btnrefresh').html('&#xE5D5;');
        } else {
            this.$element.find('.searchheader .btnfirst').html('&#xE15B;');
            this.$element.find('.searchheader .btnprev').html('&#xE15B;');
            this.$element.find('.searchheader .btnnext').html('&#xE15B;');
            this.$element.find('.searchheader .btnlast').html('&#xE15B;');
            //this.$element.find('.searchheader .btnrefresh').html('&#xE15B;');
        }
    }

    _blankOutSearchHeader() {
        this.$element.find('.searchheader .pageno').val('-');
        this.$element.find('.searchheader .totalpages').text('-');
        this.$element.find('.searchheader .rowstart').text('-');
        this.$element.find('.searchheader .rowend').text('-');
        this.$element.find('.searchheader .totalrows').text('-');
    }

    _clearSearchResults() {
        this._totalPages = 0;
        this._pageNo = 1;

        this._blankOutSearchHeader();

        this.$element.find('.searchresults').empty();
        this.$element.find('.searchfooter .recordcount').empty();
        this.$element.find('.searchfooter .pager').empty();
    }

    _updatePagerColors() {
        var plugin = this;
        const pageNo = plugin._pageNo;
        const pagesize = plugin._options.pageSize;
        const disabledCss = {
            color: '#555555',
            cursor: 'default'
        };
        const enabledCss = {
            color: '#ffffff',
            cursor: 'pointer'
        };
        if (pageNo === 1) {
            plugin.$element.find('.searchheader .btnfirst').data('enabled', false).css(disabledCss);
            plugin.$element.find('.searchheader .btnprev').data('enabled', false).css(disabledCss);
        } else {
            plugin.$element.find('.searchheader .btnfirst').data('enabled', true).css(enabledCss);
            plugin.$element.find('.searchheader .btnprev').data('enabled', true).css(enabledCss);
        }
        if (pageNo === plugin._totalPages) {
            plugin.$element.find('.searchheader .btnnext').data('enabled', false).css(disabledCss);
            plugin.$element.find('.searchheader .btnlast').data('enabled', false).css(disabledCss);
        } else {
            plugin.$element.find('.searchheader .btnnext').data('enabled', true).css(enabledCss);
            plugin.$element.find('.searchheader .btnlast').data('enabled', true).css(enabledCss);
        }
    }

    _updateSearchHeader(searchresults) {
        const totalPages = Math.ceil(searchresults.TotalRows / searchresults.PageSize);
        this._totalPages = totalPages;
        let pageNo = searchresults.PageNo;
        this._updatePagerColors();
        if (totalPages === 0) {
            pageNo = 0;
        }
        this.$element.find('.searchheader .pageno').val(pageNo);
        this.$element.find('.searchheader .totalpages').text(totalPages);

        let rowstart = pageNo;
        if (pageNo > 1) {
            rowstart = (pageNo - 1) * searchresults.PageSize + 1;
        }
        let rowend = rowstart + searchresults.PageSize - 1;
        if (rowend > searchresults.TotalRows) {
            rowend = searchresults.TotalRows;
        }
        this.$element.find('.searchheader .rowstart').text(rowstart);
        this.$element.find('.searchheader .rowend').text(rowend);
        this.$element.find('.searchheader .totalrows').text(searchresults.TotalRows);
    }

    _search() {
        var plugin = this;
        this._toggleWaitIcons(true);
        if (plugin._pageNo === 0) {
            plugin.$element.find('.searchheader .pageno').val(1).change();
        }
        plugin.$element.find('.searchbox').blur();
        plugin._options.beforeSearch();
        var funcCustomSearch = null;
        if ((typeof plugin._options.currentMode === 'string') && (typeof plugin._searchModesLookup[plugin._options.currentMode] === 'object')) {
            funcCustomSearch = plugin._searchModesLookup[plugin._options.currentMode].search;
        }
        var hasCustomSearch = typeof funcCustomSearch === 'function';
        var searchvalue = plugin.$element.find('.searchbox').val();
        let doNormalSearch = true;
        if (hasCustomSearch) {
            const result = funcCustomSearch(searchvalue, this);
            doNormalSearch = (typeof result === 'boolean' && result)
        }
        if (doNormalSearch) {
            plugin._options.request = plugin._options.getRequest();
            plugin._options.request.searchvalue = searchvalue;
            plugin._options.request.searchmode = plugin._options.currentMode;
            plugin._options.request.pagesize = plugin._options.pageSize;
            plugin._options.request.pageno = plugin._pageNo;
            FwServices.callMethod(plugin._options.service, plugin._options.method, plugin._options.request, plugin._options.queryTimeout, null, (response) => {
                try {
                    this._toggleWaitIcons(false);
                    plugin._load(response.searchresults);
                    plugin._options.afterLoad(plugin, response);
                } catch (ex) {
                    this._toggleWaitIcons(false);
                    FwFunc.showError(ex);
                }
            },
            (ex) => {
                FwFunc.showError(ex);
            });
        }
    }

    _firstpage() {
        var plugin = this;
        plugin._pageNo = 1;
        plugin._search();
    }

    _previouspage() {
        var plugin = this;
        var pageno = plugin._pageNo;
        if (pageno > 1) {
            plugin._pageNo = pageno - 1;
        }
        plugin._search();
    }

    _nextpage() {
        var plugin = this;
        var pageno = plugin._pageNo;
        var totalpages = plugin._totalPages;
        if (pageno < totalpages) {
            plugin._pageNo = plugin._pageNo + 1;
        }
        plugin._search();
    }

    _lastpage() {
        var plugin = this;
        var totalpages = plugin._totalPages;
        plugin._pageNo = totalpages;
        plugin._search();
    }

    refresh() {
        this._search();
    }

    search() {
        this._clearSearchResults();
        this._search();
    }

    load(searchresults) {
        this._clearSearchResults();
        this._load(searchresults);
    }

    destroy() {
        jQuery(window).off('scroll');
    }

    setsearchmode(searchmodeValue) {
        this._options.currentMode = searchmodeValue;
        const searchMode = this._searchModesLookup[this._options.currentMode];
        this.$element.attr('data-mode', this._options.currentMode);

        var $option = this.$element.find(`.option[data-value="${searchmodeValue}"]`);
        $option.siblings().removeClass('active');
        $option.addClass('active');

        //this.$element.find('.option[data-value="' + searchmodeValue + '"]').trigger('click');
        this.$element.find('.searchbox').attr('placeholder', searchMode.placeholder);
        //.select();
        if (typeof searchMode.hasVirtualNumpad !== 'undefined' && searchMode.hasVirtualNumpad === true) {
            RwVirtualNumpad.init(this.$element.find('.searchbox'));
        } else {
            this.$element.find('.numpad').remove();
        }
        if (typeof this._searchModesLookup[this._options.currentMode].onActivate === 'function') {
            this._searchModesLookup[this._options.currentMode].onActivate(this);
        }
        this.togglePager(typeof searchMode.hasPager === 'undefined' || searchMode.hasPager === 'true');
        this.toggleSearchResults(typeof searchMode.hasSearchResults === 'undefined' || searchMode.hasSearchResults === 'true');
    }

    clearsearchresults() {
        this._clearSearchResults();
    }

    clearsearchbox() {
        this.setSearchText('', false);
    }

    getSearchText() {
        var searchText = this.$element.find('.searchbox').val();
        return searchText;
    }

    setSearchText(text: string, triggerChange?: boolean) {
        var $searchbox = this.$element.find('.searchbox');
        $searchbox.val(text);
        if (text !== '') {
            this.$element.find('.clear').addClass('visible');
        } else {
            this.$element.find('.clear').removeClass('visible');
        }
        if (triggerChange) {
            $searchbox.change();
        }
    }

    getSearchOption() {
        var searchOption = this.$element.find('.option.active').attr('data-value');
        return searchOption;
    }

    togglePager(isVisible) {
        this.$element.find('.pager').toggle(isVisible);
    }

    toggleSearchResults(isVisible) {
        this.$element.find('.searchresults').toggle(isVisible);
    }
}

(<any>jQuery.fn).fwmobilesearch = function (option) {
    var args = Array.apply(null, arguments);
    args.shift();
    var internal_return;
    this.each(() => {
        var $this = jQuery(this),
            data = $this.data('fwmobilesearch'),
            options = typeof option === 'object' && option;
        if (!data) {
            var opts = jQuery.extend({}, FwMobileSearch.defaults, options); // Options priority: js args, defaults
            $this.data('fwmobilesearch', (data = new FwMobileSearch(this, opts)));
        }
        if (typeof option === 'string' && typeof data[option] === 'function') {
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
