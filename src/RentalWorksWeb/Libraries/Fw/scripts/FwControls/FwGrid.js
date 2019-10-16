var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class FwGridClass {
    constructor() {
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
    init($grid, options) {
        var me = this;
        options = Object.assign({}, me.options, options);
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
    }
    _renderTitle($grid, options) {
        var me = this;
        var title = (typeof options.title == 'string') ? options.title : options.title();
        var $title = jQuery('<div>')
            .addClass('grid-title')
            .html(title)
            .appendTo($grid);
    }
    _renderMenu($grid, options) {
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
    }
    _addButton($grid, $menu, button) {
        var me = this;
        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(button.caption)
            .appendTo($menu)
            .on('click', jQuery.proxy(button.action, me, $grid));
        if (button.icon) {
            jQuery(`<i class="material-icons left">${button.icon}</i>`).prependTo($button);
        }
    }
    _addDropDownButton($grid, $menu, button) {
        var me = this;
        var $dropdownbutton = jQuery('<div>')
            .addClass('grid-menu-dropdownbutton')
            .appendTo($menu);
        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(`${button.caption}<i class="material-icons right">arrow_drop_down</i>`)
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
            let item = button.items[i];
            if (item === 'seperator') {
                jQuery('<div>')
                    .addClass('dropdown-seperator')
                    .appendTo($dropdown);
            }
            else {
                let $item = jQuery('<div>')
                    .addClass('dropdown-item')
                    .appendTo($dropdown)
                    .on('click', jQuery.proxy(item.action, me, $grid));
                let $caption = jQuery('<div>')
                    .addClass('dropdown-item-caption')
                    .html(item.caption)
                    .appendTo($item);
            }
        }
    }
    _addFilter($grid, $menu, filter) {
        var me = this;
        var $filter = jQuery('<div>')
            .addClass('grid-menu-filter')
            .attr('data-value', filter.value)
            .attr('data-type', filter.filtertype)
            .appendTo($menu);
        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(`${filter.caption}<i class="material-icons right">arrow_drop_down</i>`)
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
            let item = filter.items[i];
            if (item === 'seperator') {
                jQuery('<div>')
                    .addClass('dropdown-seperator')
                    .appendTo($dropdown);
            }
            else {
                let $item = jQuery('<div>')
                    .addClass('dropdown-item')
                    .attr('data-value', item.value)
                    .appendTo($dropdown);
                let $caption = jQuery('<div>')
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
    }
    _getFilters($grid) {
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
    }
    _renderHeader($grid, options) {
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
            let column = options.columns[i];
            if (!column.hidden) {
                let $col = jQuery('<col>')
                    .appendTo($colgroup);
                if (column.width) {
                    $col.css('width', column.width);
                }
                let $column = jQuery(`<th>`)
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
    }
    _addColumnFilter($grid, gridfilterable, $trfilter, $column, columnoptions, filterable) {
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
    }
    _setSort($grid, $column, sortdirection) {
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
            $column.append(`<span class="header-sort"><i class="material-icons">${icon}</i></span>`);
            me._addOrderBy($grid, $column.attr('data-column'), sortdirection);
        }
        $column.data('sort', sortdirection);
    }
    _columnHeaderClick($grid, e) {
        this._setSort($grid, jQuery(e.currentTarget));
        this.databind($grid);
    }
    _addOrderBy($grid, field, sortdirection) {
        var orderby = $grid.data('orderby');
        if (!orderby) {
            orderby = {};
        }
        orderby[field] = sortdirection;
        $grid.data('orderby', orderby);
    }
    _removeOrderBy($grid, field) {
        var orderby = $grid.data('orderby');
        if (!orderby) {
            orderby = {};
        }
        delete orderby[field];
        $grid.data('orderby', orderby);
    }
    _getOrderBy($grid) {
        var orderby = $grid.data('orderby');
        var orderbystring = [];
        for (var key in orderby) {
            if (orderby.hasOwnProperty(key)) {
                orderbystring.push(key + ' ' + orderby[key]);
            }
        }
        return orderbystring.join(',');
    }
    _renderBody($grid, options) {
        var me = this;
        let $body = jQuery('<div>')
            .addClass('grid-content')
            .appendTo($grid);
        let $table = jQuery('<table>')
            .attr('role', 'grid')
            .appendTo($body);
        var $colgroup = jQuery('<colgroup>')
            .appendTo($table);
        var $tbody = jQuery('<tbody>')
            .attr('role', 'rowgroup')
            .appendTo($table);
        for (var i = 0; i < options.columns.length; i++) {
            let column = options.columns[i];
            if (!column.hidden) {
                let $col = jQuery('<col>')
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
    }
    _select($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        if ((options.selectable !== 'multiple') || (options.selectable == 'multiple') && (!e.ctrlKey)) {
            $target.siblings().removeClass('state-selected');
        }
        $target.addClass('state-selected');
    }
    _singleclick($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        if (options.singleclick) {
            options.singleclick($grid, e);
        }
    }
    _doubleclick($grid, e) {
        var $target = jQuery(e.currentTarget);
        var options = $grid.data('options');
        if (options.doubleclick) {
            options.doubleclick($grid, e);
        }
    }
    _keydown($grid, e) {
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
    }
    _renderPager($grid, options) {
        var me = this;
        var pager = options.pager;
        var $pager = jQuery('<div>')
            .addClass('grid-pager')
            .appendTo($grid);
        if (pager.refresh) {
            let $refresh = jQuery('<div>')
                .addClass('pager-refresh')
                .attr({ 'aria-label': pager.messages.refresh, 'title': pager.messages.refresh })
                .html('<i class="material-icons">refresh</i>')
                .appendTo($pager)
                .on('click', jQuery.proxy(me._refreshClick, me, $grid));
        }
        let $info = jQuery('<div>')
            .addClass('pager-info')
            .html(FwFunc.stringFormat(pager.messages.display, '0', '0', '0'))
            .appendTo($pager);
        var pagercontrol = `<div class="pager-nav pager-nav-disabled pager-first" aria-label="${pager.messages.first}" title="${pager.messages.first}"><i class="material-icons">first_page</i></div>
                            <div class="pager-nav pager-nav-disabled pager-previous" aria-label="${pager.messages.previous}" title="${pager.messages.previous}"><i class="material-icons">chevron_left</i></div>
                            <div class="pager-pages"></div>
                            <div class="pager-nav pager-nav-disabled pager-next" aria-label="${pager.messages.next}" title="${pager.messages.next}"><i class="material-icons">chevron_right</i></div>
                            <div class="pager-nav pager-nav-disabled pager-last" aria-label="${pager.messages.last}" title="${pager.messages.last}"><i class="material-icons">last_page</i></div>`;
        let $control = jQuery('<div>')
            .addClass('pager-control')
            .html(pagercontrol)
            .appendTo($pager)
            .on('click', '.pager-nav, .pager-page', jQuery.proxy(me._pageChange, me, $grid));
        me._pageNumber($grid, 1);
        if (pager.pagesizes) {
            var pageSizes = pager.pagesizes.length ? pager.pagesizes : [5, 10, 25, 50, 100];
            var pageSizeOptions = jQuery.map(pageSizes, function (size) {
                return `<option value="${size}">${size}</option>`;
            });
            let $select = jQuery('<select>')
                .html(pageSizeOptions.join(''))
                .val(pager.pagesize)
                .on('change', jQuery.proxy(me._pageSizeChange, me, $grid));
            let $sizeselector = jQuery('<div>')
                .addClass('pager-sizes')
                .append($select)
                .append(pager.messages.itemsPerPage)
                .appendTo($pager);
            me._pageSize($grid, pager.pagesize);
        }
    }
    _refreshClick($grid, e) {
        this.databind($grid);
    }
    _pageChange($grid, e) {
        var $target = jQuery(e.currentTarget);
        if (!$target.hasClass('pager-nav-disabled')) {
            this._pageNumber($grid, parseInt($target.attr('data-page'), 10));
            this.databind($grid);
        }
    }
    _pageSizeChange($grid, e) {
        var value = e.currentTarget.value;
        var pagesize = parseInt(value, 10);
        this._pageSize($grid, pagesize);
        this.databind($grid);
    }
    _pageNumber($grid, pagenumber) {
        if (pagenumber) {
            $grid.data('pagenumber', pagenumber);
        }
        else {
            return $grid.data('pagenumber');
        }
    }
    _pageSize($grid, pagesize) {
        if (pagesize) {
            $grid.data('pagesize', pagesize);
        }
        else {
            return $grid.data('pagesize');
        }
    }
    databind($grid) {
        return __awaiter(this, void 0, void 0, function* () {
            var me = this;
            var options = $grid.data('options');
            if (options.beforedatabind) {
                options.beforedatabind($grid);
            }
            return new Promise((resolve, reject) => __awaiter(this, void 0, void 0, function* () {
                let request = me._getRequest($grid);
                FwServices.module.method(request, request.module, options.datasource, $grid, function (response) {
                    try {
                        me._databindCallback($grid, request, response);
                        resolve();
                    }
                    catch (ex) {
                        reject(ex);
                    }
                });
            }));
        });
    }
    _databindCallback($grid, request, response) {
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
                let column = options.columns[j];
                var columnIndex = response.ColumnIndex[column.field];
                if (!column.hidden) {
                    let $column = jQuery('<td>')
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
                pagerpageshtml.push(`<div class="pager-page" data-page="${start - 1}" aria-label="${pager.messages.morePages}" title="${pager.messages.morePages}">...</div>`);
            }
            for (idx = start; idx <= end; idx++) {
                pagerpageshtml.push(`<div class="pager-page${idx == page ? ' pager-page-selected' : ''}" data-page="${idx}">${idx}</div>`);
            }
            if (end < totalpages) {
                pagerpageshtml.push(`<div class="pager-page" data-page="${idx}" aria-label="${pager.messages.morePages}" title="${pager.messages.morePages}">...</div>`);
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
    }
    _getRequest($grid) {
        var me = this;
        var options = $grid.data('options');
        var request = new BrowseRequest;
        request.module = options.module;
        request.pageno = this._pageNumber($grid);
        request.pagesize = this._pageSize($grid);
        request.orderby = this._getOrderBy($grid);
        request.filterfields = this._getFilters($grid);
        if (options.filter) {
            request.filterfields = Object.assign({}, request.filterfields, options.filter($grid));
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
    }
}
var FwGrid = new FwGridClass();
//# sourceMappingURL=FwGrid.js.map