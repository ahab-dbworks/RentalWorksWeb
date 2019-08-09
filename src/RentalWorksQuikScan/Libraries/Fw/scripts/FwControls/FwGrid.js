var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var FwGridClass = (function () {
    function FwGridClass() {
        this.GRID = 'fw-grid';
        this.options = {
            editable: false,
            selectable: false,
            filterable: false,
            pager: {
                refresh: true,
                pagesize: 25,
                pagesizes: true,
                buttoncount: 10,
                messages: {
                    allPages: "All",
                    display: "{0} - {1} of {2} rows",
                    empty: "No items to display",
                    page: "Page",
                    of: "of {0}",
                    itemsPerPage: "rows per page",
                    first: "Go to the first page",
                    previous: "Go to the previous page",
                    next: "Go to the next page",
                    last: "Go to the last page",
                    refresh: "Refresh",
                    morePages: "More pages"
                }
            }
        };
    }
    FwGridClass.prototype.init = function ($grid, options) {
        var me = this;
        options = __assign({}, me.options, options);
        if (options.title) {
            this._renderTitle($grid, options);
        }
        if (options.menu) {
            this._renderMenu($grid, options);
        }
        this._renderHeader($grid, options);
        this._renderBody($grid, options);
        if (options.pager) {
            if (options.pager === true)
                options.pager = me.options.pager;
            this._renderPager($grid, options);
        }
        $grid.data('options', options)
            .addClass(me.GRID);
    };
    FwGridClass.prototype._renderTitle = function ($grid, options) {
        var me = this;
        var title = (typeof options.title == 'string') ? options.title : options.title();
        var $title = jQuery('<div>')
            .addClass('grid-title')
            .html(title)
            .appendTo($grid);
    };
    FwGridClass.prototype._renderMenu = function ($grid, options) {
        var me = this;
        var definedmenu = options.menu;
        var $menu = jQuery('<div>')
            .addClass('grid-menu')
            .appendTo($grid);
        if (definedmenu.objects) {
            for (var i = 0; i < definedmenu.objects.length; i++) {
                var object = definedmenu.objects[i];
                if (object === 'seperator') {
                    jQuery('<div>')
                        .addClass('grid-menu-seperator')
                        .appendTo($menu);
                }
                else if (object.type === 'button') {
                    this._addButton($grid, $menu, object);
                }
                else if (object.type === 'dropdownbutton') {
                    this._addDropDownButton($grid, $menu, object);
                }
                else if (object.type === 'filter') {
                    this._addFilter($grid, $menu, object);
                }
            }
        }
    };
    FwGridClass.prototype._addButton = function ($grid, $menu, button) {
        var me = this;
        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(button.caption)
            .appendTo($menu)
            .on('click', jQuery.proxy(button.action, me, $grid));
        if (button.icon) {
            jQuery("<i class=\"material-icons left\">" + button.icon + "</i>").prependTo($button);
        }
    };
    FwGridClass.prototype._addDropDownButton = function ($grid, $menu, button) {
        var me = this;
        var $dropdownbutton = jQuery('<div>')
            .addClass('grid-menu-dropdownbutton')
            .appendTo($menu);
        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(button.caption + "<i class=\"material-icons right\">arrow_drop_down</i>")
            .appendTo($dropdownbutton)
            .on('click', function (e) {
            e.stopPropagation();
            if ($dropdown.is(':visible')) {
                $dropdown.removeClass('active');
            }
            else {
                $dropdown.addClass('active');
                jQuery(document).one('click', function closeMenu(e) {
                    if (!$dropdownbutton.has(e.target).length) {
                        $dropdown.removeClass('active');
                    }
                    else {
                        jQuery(document).one('click', closeMenu);
                    }
                });
            }
        });
        var $dropdown = jQuery('<div>')
            .addClass('grid-menu-dropdown')
            .appendTo($dropdownbutton);
        for (var i = 0; i < button.items.length; i++) {
            var item = button.items[i];
            if (item === 'seperator') {
                jQuery('<div>')
                    .addClass('dropdown-seperator')
                    .appendTo($dropdown);
            }
            else {
                var $item = jQuery('<div>')
                    .addClass('dropdown-item')
                    .appendTo($dropdown)
                    .on('click', jQuery.proxy(item.action, me, $grid));
                var $caption = jQuery('<div>')
                    .addClass('dropdown-item-caption')
                    .html(item.caption)
                    .appendTo($item);
            }
        }
    };
    FwGridClass.prototype._addFilter = function ($grid, $menu, filter) {
        var me = this;
        var $filter = jQuery('<div>')
            .addClass('grid-menu-filter')
            .attr('data-value', filter.value)
            .attr('data-type', filter.filtertype)
            .appendTo($menu);
        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(filter.caption + "<i class=\"material-icons right\">arrow_drop_down</i>")
            .appendTo($filter)
            .on('click', function (e) {
            e.stopPropagation();
            if ($dropdown.is(':visible')) {
                $dropdown.removeClass('active');
            }
            else {
                $dropdown.addClass('active');
                jQuery(document).one('click', function closeMenu(e) {
                    if ($filter.has(e.target).length === 0) {
                        $dropdown.removeClass('active');
                    }
                    else {
                        jQuery(document).one('click', closeMenu);
                    }
                });
            }
        });
        var $dropdown = jQuery('<div>')
            .addClass('grid-menu-dropdown')
            .appendTo($filter);
        for (var i = 0; i < filter.items.length; i++) {
            var item = filter.items[i];
            if (item === 'seperator') {
                jQuery('<div>')
                    .addClass('dropdown-seperator')
                    .appendTo($dropdown);
            }
            else {
                var $item = jQuery('<div>')
                    .addClass('dropdown-item')
                    .attr('data-value', item.value)
                    .appendTo($dropdown);
                var $caption = jQuery('<div>')
                    .addClass('dropdown-item-caption')
                    .html(item.caption)
                    .appendTo($item);
                if (filter.filtertype === 'checkbox') {
                    $item
                        .attr('data-type', 'checkbox')
                        .attr('data-selected', item.defaultselected === false ? 'false' : 'true')
                        .on('click', function (e) {
                        var $this = jQuery(e.currentTarget);
                        var istrue = $this.attr('data-selected') === 'true';
                        $this.attr('data-selected', istrue ? 'false' : 'true');
                        me.databind($grid);
                    });
                }
                else {
                    $item
                        .attr('data-type', 'exclusive')
                        .attr('data-selected', item.defaultselected === true ? 'true' : 'false')
                        .on('click', function (e) {
                        var $this = jQuery(e.currentTarget);
                        $this.siblings('.dropdown-item').attr('data-selected', 'false');
                        $this.attr('data-selected', 'true');
                        me._pageNumber($grid, 1);
                        $dropdown.removeClass('active');
                        me.databind($grid);
                    });
                }
            }
        }
        if (filter.filtertype === 'checkbox') {
            jQuery('<div>')
                .addClass('dropdown-seperator')
                .appendTo($dropdown);
            var $selectall = jQuery('<div>')
                .addClass('dropdown-item')
                .html('Select All')
                .appendTo($dropdown)
                .on('click', function () {
                $dropdown.find('.dropdown-item[data-type="checkbox"]').attr('data-selected', 'true');
                me.databind($grid);
            });
            var $selectnone = jQuery('<div>')
                .addClass('dropdown-item')
                .html('Select None')
                .appendTo($dropdown)
                .on('click', function () {
                $dropdown.find('.dropdown-item[data-type="checkbox"]').attr('data-selected', 'false');
                me.databind($grid);
            });
        }
    };
    FwGridClass.prototype._getFilters = function ($grid) {
        var me = this;
        var $filters = $grid.find('.grid-menu-filter');
        var filters = {};
        for (var i = 0; i < $filters.length; i++) {
            var $filter = jQuery($filters[i]);
            if ($filter.attr('data-type') === 'checkbox') {
                var $items = $filter.find('.dropdown-item');
                for (var j = 0; j < $items.length; j++) {
                    var $item = jQuery($items[j]);
                    filters[$item.attr('data-value')] = $item.attr('data-selected');
                }
            }
            else {
                filters[$filter.attr('data-value')] = $filter.find('.dropdown-item[data-selected="true"]').attr('data-value');
            }
        }
        return filters;
    };
    FwGridClass.prototype._renderHeader = function ($grid, options) {
        var me = this;
        var $header = jQuery('<div>')
            .addClass('grid-header')
            .appendTo($grid);
        var $table = jQuery('<table>')
            .attr('role', 'grid')
            .appendTo($header);
        var $colgroup = jQuery('<colgroup>')
            .appendTo($table);
        var $thead = jQuery('<thead>')
            .attr('role', 'rowgroup')
            .appendTo($table);
        var $tr = jQuery('<tr>')
            .attr('role', 'row')
            .appendTo($thead);
        if (options.filterable !== false) {
            if ((typeof options.filterable === 'boolean') && (options.filterable === true)) {
                options.filterable = {
                    mode: 'row'
                };
            }
            if (options.filterable.mode === 'row') {
                var $trfilter = jQuery('<tr>')
                    .addClass('filter-row')
                    .appendTo($thead);
            }
        }
        for (var i = 0; i < options.columns.length; i++) {
            var column = options.columns[i];
            if (!column.hidden) {
                var $col = jQuery('<col>')
                    .appendTo($colgroup);
                if (column.width) {
                    $col.css('width', column.width);
                }
                var $column = jQuery("<th>")
                    .addClass('header')
                    .attr('data-column', column.field)
                    .html(column.title)
                    .appendTo($tr);
                if (column.sort !== false) {
                    $column.addClass('sortable');
                    $column.on('click', jQuery.proxy(me._columnHeaderClick, me, $grid));
                    if (typeof column.sort === 'string') {
                        me._setSort($grid, $column, column.sort);
                    }
                }
                if (options.filterable !== false) {
                    me._addColumnFilter($grid, options.filterable, $trfilter, $column, column, column.filterable);
                }
            }
        }
    };
    FwGridClass.prototype._addColumnFilter = function ($grid, gridfilterable, $trfilter, $column, columnoptions, filterable) {
        var me = this;
        if ((typeof filterable === 'undefined') || ((typeof filterable === 'boolean') && (filterable === true))) {
            filterable = {
                datatype: 'text'
            };
        }
        if (gridfilterable.mode === 'row') {
            var $columnfilter = jQuery('<th>')
                .appendTo($trfilter);
            if (filterable !== false) {
                var $filtercontainer = jQuery('<span>')
                    .addClass('filtercell')
                    .attr('data-column', columnoptions.field)
                    .attr('role', 'filtercell')
                    .appendTo($columnfilter);
                var $filterinput = jQuery('<input>')
                    .addClass('filterinput')
                    .attr('type', 'text')
                    .appendTo($filtercontainer)
                    .on('change', function () {
                    if (jQuery(this).val() !== '') {
                        $filtercontainer.addClass('filtered');
                    }
                    else {
                        $filtercontainer.removeClass('filtered');
                    }
                    me._pageNumber($grid, 1);
                    me.databind($grid);
                });
                var $filterclear = jQuery('<span>')
                    .addClass('filterclear')
                    .html('<i class="material-icons">clear</i>')
                    .appendTo($filtercontainer)
                    .on('click', function () {
                    $filterinput.val('').change();
                });
            }
        }
        else if ((gridfilterable.mode === 'menu') && (filterable !== false)) {
            var $filter = jQuery('<span>')
                .addClass('filter-menu')
                .attr('data-column', columnoptions.field)
                .html('<i class="material-icons">filter_list</i>')
                .appendTo($column)
                .on('click', function (e) {
                e.stopPropagation();
                if ($filterdropdown.is(':visible')) {
                    $filter.removeClass('active');
                }
                else {
                    $filter.addClass('active');
                    jQuery(document).one('click', function closeMenu(e) {
                        if ($filterdropdown.has(e.target).length === 0) {
                            $filter.removeClass('active');
                        }
                        else {
                            jQuery(document).one('click', closeMenu);
                        }
                    });
                }
            });
            var $filterdropdown = jQuery('<div>')
                .addClass('filter-menu-dropdown')
                .appendTo($filter);
            var $filtercontent = jQuery('<div>')
                .addClass('filter-content')
                .appendTo($filterdropdown);
            switch (filterable.datatype) {
                case 'text':
                    jQuery('<input>')
                        .addClass('filterinput')
                        .attr('type', 'text')
                        .appendTo($filtercontent);
                    break;
                case 'checkbox':
                    break;
            }
            var $actionbuttons = jQuery('<div>')
                .addClass('action-buttons')
                .appendTo($filterdropdown);
            var $filterbutton = jQuery('<div>')
                .addClass('filter-button primary')
                .html('Filter')
                .appendTo($actionbuttons);
            var $clearbutton = jQuery('<div>')
                .addClass('filter-button')
                .html('Clear')
                .appendTo($actionbuttons);
        }
    };
    FwGridClass.prototype._setSort = function ($grid, $column, sortdirection) {
        var me = this;
        if (!sortdirection) {
            var column_sort = $column.data('sort');
            var lastsorted = true;
            $column.siblings('.sortable').each(function (index, element) {
                var $sibling = jQuery(element);
                var sibling_sort = $sibling.data('sort');
                if (sibling_sort === 'asc' || sibling_sort === 'desc') {
                    lastsorted = false;
                    return false;
                }
            });
            if (!column_sort || column_sort === 'off' || (lastsorted && column_sort === 'desc')) {
                sortdirection = 'asc';
            }
            else if (column_sort === 'desc') {
                sortdirection = 'off';
            }
            else if (column_sort === 'asc') {
                sortdirection = 'desc';
            }
        }
        $column.find('.header-sort').remove();
        me._removeOrderBy($grid, $column.attr('data-column'));
        if (sortdirection === 'asc' || sortdirection === 'desc') {
            var icon = sortdirection === 'asc' ? 'arrow_upward' : 'arrow_downward';
            $column.append("<span class=\"header-sort\"><i class=\"material-icons\">" + icon + "</i></span>");
            me._addOrderBy($grid, $column.attr('data-column'), sortdirection);
        }
        $column.data('sort', sortdirection);
    };
    FwGridClass.prototype._columnHeaderClick = function ($grid, e) {
        this._setSort($grid, jQuery(e.currentTarget));
        this.databind($grid);
    };
    FwGridClass.prototype._addOrderBy = function ($grid, field, sortdirection) {
        var orderby = $grid.data('orderby');
        if (!orderby) {
            orderby = {};
        }
        orderby[field] = sortdirection;
        $grid.data('orderby', orderby);
    };
    FwGridClass.prototype._removeOrderBy = function ($grid, field) {
        var orderby = $grid.data('orderby');
        if (!orderby) {
            orderby = {};
        }
        delete orderby[field];
        $grid.data('orderby', orderby);
    };
    FwGridClass.prototype._getOrderBy = function ($grid) {
        var orderby = $grid.data('orderby');
        var orderbystring = [];
        for (var key in orderby) {
            if (orderby.hasOwnProperty(key)) {
                orderbystring.push(key + ' ' + orderby[key]);
            }
        }
        return orderbystring.join(',');
    };
    FwGridClass.prototype._renderBody = function ($grid, options) {
        var me = this;
        var $body = jQuery('<div>')
            .addClass('grid-content')
            .appendTo($grid);
        var $table = jQuery('<table>')
            .attr('role', 'grid')
            .appendTo($body);
        var $colgroup = jQuery('<colgroup>')
            .appendTo($table);
        var $tbody = jQuery('<tbody>')
            .attr('role', 'rowgroup')
            .appendTo($table);
        for (var i = 0; i < options.columns.length; i++) {
            var column = options.columns[i];
            if (!column.hidden) {
                var $col = jQuery('<col>')
                    .appendTo($colgroup);
                if (column.width) {
                    $col.css('width', column.width);
                }
            }
        }
        if (options.selectable) {
            $table.on('click', 'tr', jQuery.proxy(me._select, me, $grid));
        }
        $table.on('keydown', 'tr', jQuery.proxy(me._keydown, me, $grid));
        $table.on('click', 'tr', jQuery.proxy(me._singleclick, me, $grid));
        $table.on('dblclick', 'tr', jQuery.proxy(me._doubleclick, me, $grid));
    };
    FwGridClass.prototype._select = function ($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        if ((options.selectable !== 'multiple') || (options.selectable == 'multiple') && (!e.ctrlKey)) {
            $target.siblings().removeClass('state-selected');
        }
        $target.addClass('state-selected');
    };
    FwGridClass.prototype._singleclick = function ($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        if (options.singleclick) {
            options.singleclick($grid, e);
        }
    };
    FwGridClass.prototype._doubleclick = function ($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        if (options.doubleclick) {
            options.doubleclick($grid, e);
        }
    };
    FwGridClass.prototype._keydown = function ($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        var keycode = e.keyCode;
        switch (keycode) {
            case FwFunc.keys.ENTER:
                break;
            case FwFunc.keys.LEFT:
                break;
            case FwFunc.keys.UP:
                break;
            case FwFunc.keys.RIGHT:
                break;
            case FwFunc.keys.DOWN:
                break;
        }
    };
    FwGridClass.prototype._renderPager = function ($grid, options) {
        var me = this;
        var pager = options.pager;
        var $pager = jQuery('<div>')
            .addClass('grid-pager')
            .appendTo($grid);
        if (pager.refresh) {
            var $refresh = jQuery('<div>')
                .addClass('pager-refresh')
                .attr({ 'aria-label': pager.messages.refresh, 'title': pager.messages.refresh })
                .html('<i class="material-icons">refresh</i>')
                .appendTo($pager)
                .on('click', jQuery.proxy(me._refreshClick, me, $grid));
        }
        var $info = jQuery('<div>')
            .addClass('pager-info')
            .html(FwFunc.stringFormat(pager.messages.display, '0', '0', '0'))
            .appendTo($pager);
        var pagercontrol = "<div class=\"pager-nav pager-nav-disabled pager-first\" aria-label=\"" + pager.messages.first + "\" title=\"" + pager.messages.first + "\"><i class=\"material-icons\">first_page</i></div>\n                            <div class=\"pager-nav pager-nav-disabled pager-previous\" aria-label=\"" + pager.messages.previous + "\" title=\"" + pager.messages.previous + "\"><i class=\"material-icons\">chevron_left</i></div>\n                            <div class=\"pager-pages\"></div>\n                            <div class=\"pager-nav pager-nav-disabled pager-next\" aria-label=\"" + pager.messages.next + "\" title=\"" + pager.messages.next + "\"><i class=\"material-icons\">chevron_right</i></div>\n                            <div class=\"pager-nav pager-nav-disabled pager-last\" aria-label=\"" + pager.messages.last + "\" title=\"" + pager.messages.last + "\"><i class=\"material-icons\">last_page</i></div>";
        var $control = jQuery('<div>')
            .addClass('pager-control')
            .html(pagercontrol)
            .appendTo($pager)
            .on('click', '.pager-nav, .pager-page', jQuery.proxy(me._pageChange, me, $grid));
        me._pageNumber($grid, 1);
        if (pager.pagesizes) {
            var pageSizes = pager.pagesizes.length ? pager.pagesizes : [5, 10, 25, 50, 100];
            var pageSizeOptions = jQuery.map(pageSizes, function (size) {
                return "<option value=\"" + size + "\">" + size + "</option>";
            });
            var $select = jQuery('<select>')
                .html(pageSizeOptions.join(''))
                .val(pager.pagesize)
                .on('change', jQuery.proxy(me._pageSizeChange, me, $grid));
            var $sizeselector = jQuery('<div>')
                .addClass('pager-sizes')
                .append($select)
                .append(pager.messages.itemsPerPage)
                .appendTo($pager);
            me._pageSize($grid, pager.pagesize);
        }
    };
    FwGridClass.prototype._refreshClick = function ($grid, e) {
        this.databind($grid);
    };
    FwGridClass.prototype._pageChange = function ($grid, e) {
        var $target = jQuery(e.currentTarget);
        if (!$target.hasClass('pager-nav-disabled')) {
            this._pageNumber($grid, parseInt($target.attr('data-page'), 10));
            this.databind($grid);
        }
    };
    FwGridClass.prototype._pageSizeChange = function ($grid, e) {
        var value = e.currentTarget.value;
        var pagesize = parseInt(value, 10);
        this._pageSize($grid, pagesize);
        this.databind($grid);
    };
    FwGridClass.prototype._pageNumber = function ($grid, pagenumber) {
        if (pagenumber) {
            $grid.data('pagenumber', pagenumber);
        }
        else {
            return $grid.data('pagenumber');
        }
    };
    FwGridClass.prototype._pageSize = function ($grid, pagesize) {
        if (pagesize) {
            $grid.data('pagesize', pagesize);
        }
        else {
            return $grid.data('pagesize');
        }
    };
    FwGridClass.prototype.databind = function ($grid) {
        return __awaiter(this, void 0, void 0, function () {
            var me, options;
            var _this = this;
            return __generator(this, function (_a) {
                me = this;
                options = $grid.data('options');
                if (options.beforedatabind) {
                    options.beforedatabind($grid);
                }
                return [2, new Promise(function (resolve, reject) { return __awaiter(_this, void 0, void 0, function () {
                        var request;
                        return __generator(this, function (_a) {
                            request = me._getRequest($grid);
                            FwServices.module.method(request, request.module, options.datasource, $grid, function (response) {
                                try {
                                    me._databindCallback($grid, request, response);
                                    resolve();
                                }
                                catch (ex) {
                                    reject(ex);
                                }
                            });
                            return [2];
                        });
                    }); })];
            });
        });
    };
    FwGridClass.prototype._databindCallback = function ($grid, request, response) {
        var me = this;
        var options = $grid.data('options');
        var page = me._pageNumber($grid);
        var pagesize = me._pageSize($grid);
        var totalrows = response.TotalRows;
        var totalpages = response.TotalPages;
        var $tbody = $grid.find('.grid-content tbody');
        $tbody.empty();
        for (var i = 0; i < response.Rows.length; i++) {
            var row = response.Rows[i];
            var $row = jQuery('<tr>')
                .attr('role', 'row')
                .attr('data-rowid', row[response.ColumnIndex[options.rowid]])
                .appendTo($tbody);
            for (var j = 0; j < options.columns.length; j++) {
                var column = options.columns[j];
                var columnIndex = response.ColumnIndex[column.field];
                if (!column.hidden) {
                    var $column = jQuery('<td>')
                        .attr('role', 'gridcell')
                        .attr('data-column', column.field)
                        .appendTo($row);
                    if (column.datatype == 'tag') {
                        var $tag = jQuery('<div>')
                            .addClass('tag')
                            .attr('data-tagtype', row[columnIndex])
                            .html(row[columnIndex])
                            .appendTo($column);
                    }
                    else {
                        $column.html(row[columnIndex]);
                    }
                }
            }
            var rowdata = {};
            for (var k = 0; k < response.Columns.length; k++) {
                rowdata[response.Columns[k].Name] = row[k];
            }
            $row.data('recorddata', rowdata);
        }
        if (options.pager !== false) {
            var pager = options.pager;
            if (response.TotalRows > 0) {
                $grid.find('.grid-pager .pager-info').html(FwFunc.stringFormat(pager.messages.display, Math.min((page - 1) * (pagesize || 0) + 1, totalrows), Math.min(page * pagesize, totalrows), totalrows));
            }
            else {
                $grid.find('.grid-pager .pager-info').html(pager.messages.empty);
            }
            var pagerpageshtml = [];
            var buttoncount = pager.buttoncount;
            var start = 1;
            var end, reminder, idx;
            if (page > buttoncount) {
                reminder = (page % buttoncount);
                start = (reminder === 0) ? (page - buttoncount) + 1 : (page - reminder) + 1;
            }
            end = Math.min((start + buttoncount) - 1, totalpages);
            if (start > 1) {
                pagerpageshtml.push("<div class=\"pager-page\" data-page=\"" + (start - 1) + "\" aria-label=\"" + pager.messages.morePages + "\" title=\"" + pager.messages.morePages + "\">...</div>");
            }
            for (idx = start; idx <= end; idx++) {
                pagerpageshtml.push("<div class=\"pager-page" + (idx == page ? ' pager-page-selected' : '') + "\" data-page=\"" + idx + "\">" + idx + "</div>");
            }
            if (end < totalpages) {
                pagerpageshtml.push("<div class=\"pager-page\" data-page=\"" + idx + "\" aria-label=\"" + pager.messages.morePages + "\" title=\"" + pager.messages.morePages + "\">...</div>");
            }
            $grid.find('.grid-pager .pager-pages').html(pagerpageshtml.join(''));
            $grid.find('.grid-pager .pager-first').attr('data-page', 1).toggleClass('pager-nav-disabled', page <= 1);
            $grid.find('.grid-pager .pager-previous').attr('data-page', Math.max(1, page - 1)).toggleClass('pager-nav-disabled', page <= 1);
            $grid.find('.grid-pager .pager-next').attr('data-page', Math.min(totalpages, page + 1)).toggleClass('pager-nav-disabled', page >= totalpages);
            $grid.find('.grid-pager .pager-last').attr('data-page', totalpages).toggleClass('pager-nav-disabled', page >= totalpages);
        }
        if (options.afterdatabind) {
            options.afterdatabind($grid);
        }
    };
    FwGridClass.prototype._getRequest = function ($grid) {
        var me = this;
        var options = $grid.data('options');
        var request = new BrowseRequest;
        request.module = options.module;
        request.pageno = this._pageNumber($grid);
        request.pagesize = this._pageSize($grid);
        request.orderby = this._getOrderBy($grid);
        request.filterfields = this._getFilters($grid);
        if (options.filter) {
            request.filterfields = __assign({}, request.filterfields, options.filter($grid));
        }
        if (options.filterable !== false) {
            var filterable = options.filterable;
            var $filteredfields;
            if (filterable.mode === 'row') {
                $filteredfields = $grid.find('.grid-header .filtercell.filtered');
            }
            else if (filterable.mode === 'menu') {
                $filteredfields = $grid.find('.grid-header .filter-menu.filtered');
            }
            if ($filteredfields.length > 0) {
                $filteredfields.each(function (index, value) {
                    var $filteredfield = jQuery(value);
                    request.searchfields.push($filteredfield.attr('data-column'));
                    request.searchfieldvalues.push($filteredfield.find('.filterinput').val().toString());
                    request.searchfieldtypes.push($filteredfield.find('.filterinput').attr('type'));
                    request.searchseparators.push(',');
                    request.searchfieldoperators.push('like');
                    request.searchcondition.push("and");
                });
            }
        }
        return request;
    };
    return FwGridClass;
}());
var FwGrid = new FwGridClass();
//# sourceMappingURL=FwGrid.js.map