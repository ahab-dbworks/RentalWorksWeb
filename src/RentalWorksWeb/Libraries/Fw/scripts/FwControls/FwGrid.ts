class FwGridClass {
    private GRID: string = 'fw-grid';
    private options: GridOptions = {
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
    //---------------------------------------------------------------------------------
    init($grid: JQuery, options: GridOptions) {
        var me = this;
        options = {...me.options, ...options};

        if (options.title) {
            this._renderTitle($grid, options);
        }

        if (options.menu) {
            this._renderMenu($grid, options);
        }

        this._renderHeader($grid, options);

        this._renderBody($grid, options);

        if (options.pager) {
            if (options.pager === true) options.pager = me.options.pager;
            this._renderPager($grid, options);
        }

        $grid.data('options', options)
            .addClass(me.GRID);
    }
    //---------------------------------------------------------------------------------
    private _renderTitle($grid: JQuery, options: GridOptions) {
        var me    = this;
        var title = (typeof options.title == 'string') ? options.title : options.title();

        var $title = jQuery('<div>')
            .addClass('grid-title')
            .html(title)
            .appendTo($grid);
    }
    //---------------------------------------------------------------------------------
    private _renderMenu($grid: JQuery, options: GridOptions) {
        var me                    = this;
        var definedmenu: GridMenu = options.menu as GridMenu;

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
                } else if (object.type === 'button') {
                    this._addButton($grid, $menu, object);
                } else if (object.type === 'dropdownbutton') {
                    this._addDropDownButton($grid, $menu, object);
                } else if (object.type === 'filter') {
                    this._addFilter($grid, $menu, object);
                }
            }
        }
    }
    //---------------------------------------------------------------------------------
    private _addButton($grid: JQuery, $menu: JQuery, button: GridMenuButton) {
        if (FwApplicationTree.isVisibleInSecurityTree(button.secid)) {
            const $button = jQuery('<div>')
                .addClass('grid-menu-button')
                .html(button.caption)
                .appendTo($menu)
                .on('click', jQuery.proxy(button.action, this, $grid));

            if (button.icon) {
                jQuery(`<i class="material-icons left">${button.icon}</i>`).prependTo($button);
            }
        }
    }
    //---------------------------------------------------------------------------------
    private _addDropDownButton($grid: JQuery, $menu: JQuery, button: GridMenuDropDownButton) {
        var me = this;

        var $dropdownbutton = jQuery('<div>')
            .addClass('grid-menu-dropdownbutton');

        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(`${button.caption}<i class="material-icons right">arrow_drop_down</i>`)
            .appendTo($dropdownbutton)
            .on('click', function(e) {
                e.stopPropagation();
                if ($dropdown.is(':visible')) {
                    $dropdown.removeClass('active');
                } else {
                    $dropdown.addClass('active');

                    jQuery(document).one('click', function closeMenu(e: JQuery.ClickEvent) {
                        if (!$dropdownbutton.has(e.target).length) {
                            $dropdown.removeClass('active');
                        } else {
                            jQuery(document).one('click', closeMenu);
                        }
                    });
                }
            });

        var $dropdown = jQuery('<div>')
            .addClass('grid-menu-dropdown')
            .appendTo($dropdownbutton);
        let hasChildItems = false;
        for (var i = 0; i < button.items.length; i++) {
            const item = button.items[i];
            if (item === 'seperator') {
                jQuery('<div>')
                    .addClass('dropdown-seperator')
                    .appendTo($dropdown);
            } else {
                const dropDownButtonItem = <GridMenuDropDownButtonItem>item;
                if (FwApplicationTree.isVisibleInSecurityTree(dropDownButtonItem.secid)) {
                    hasChildItems = true;
                    let $item = jQuery('<div>')
                        .addClass('dropdown-item')
                        .appendTo($dropdown)
                        .on('click', jQuery.proxy(item.action, me, $grid))

                    let $caption = jQuery('<div>')
                        .addClass('dropdown-item-caption')
                        .html(item.caption)
                        .appendTo($item);
                }
            }
        }
        if (hasChildItems) {
            $dropdownbutton.appendTo($menu)
        }
    }
    //---------------------------------------------------------------------------------
    private _addFilter($grid: JQuery, $menu: JQuery, filter: GridMenuFilter) {
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
            .on('click', function(e) {
                e.stopPropagation();
                if ($dropdown.is(':visible')) {
                    $dropdown.removeClass('active');
                } else {
                    $dropdown.addClass('active');

                    jQuery(document).one('click', function closeMenu(e: JQuery.ClickEvent) {
                        if ($filter.has(e.target).length === 0) {
                            $dropdown.removeClass('active');
                        } else {
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
            } else {
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
                        .on('click', function (e: JQuery.ClickEvent) {
                            var $this = jQuery(e.currentTarget);
                            var istrue = $this.attr('data-selected') === 'true';
                            $this.attr('data-selected', istrue ? 'false' : 'true');

                            me.databind($grid);
                        });
                } else {
                    $item
                        .attr('data-type', 'exclusive')
                        .attr('data-selected', item.defaultselected === true ? 'true' : 'false')
                        .on('click', function (e: JQuery.ClickEvent) {
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
    //---------------------------------------------------------------------------------
    private _getFilters($grid: JQuery) {
        var me       = this;
        var $filters = $grid.find('.grid-menu-filter');
        var filters  = {};

        //For nested filter fields when checkbox -- Would like to pass in this way
        //var value;
        //for (var i = 0; i < $filters.length; i++) {
        //    var $filter = jQuery($filters[i]);
        //
        //    if ($filter.attr('data-type') === 'checkbox') {
        //        var $items = $filter.find('.dropdown-item');
        //        value = {};
        //        for (var j = 0; j < $items.length; j++) {
        //            var $item = jQuery($items[j]);
        //
        //            value[$item.attr('data-value')] = $item.attr('data-selected');
        //        }
        //    } else {
        //        value = $filter.find('.dropdown-item[data-selected]').attr('data-value');
        //    }
        //
        //    filters[$filter.attr('data-value')] = value;
        //}

        for (var i = 0; i < $filters.length; i++) {
            var $filter = jQuery($filters[i]);

            if ($filter.attr('data-type') === 'checkbox') {
                var $items = $filter.find('.dropdown-item');
                for (var j = 0; j < $items.length; j++) {
                    var $item = jQuery($items[j]);

                    filters[$item.attr('data-value')] = $item.attr('data-selected');
                }
            } else {
                filters[$filter.attr('data-value')] = $filter.find('.dropdown-item[data-selected="true"]').attr('data-value');
            }
        }

        return filters;
    }
    //---------------------------------------------------------------------------------
    private _renderHeader($grid: JQuery, options: GridOptions) {
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
    //---------------------------------------------------------------------------------
    private _addColumnFilter($grid: JQuery, gridfilterable: GridFilterable, $trfilter: JQuery, $column: JQuery, columnoptions: GridColumn, filterable: boolean|GridColumnFilterable) {
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
                            $filtercontainer.addClass('filtered')
                        } else {
                            $filtercontainer.removeClass('filtered')
                        }
                        me._pageNumber($grid, 1);
                        me.databind($grid);
                    });

                var $filterclear = jQuery('<span>')
                    .addClass('filterclear')
                    .html('<i class="material-icons">clear</i>')
                    .appendTo($filtercontainer)
                    .on('click', function() {
                        $filterinput.val('').change();
                    });
            }
        } else if ((gridfilterable.mode === 'menu') && (filterable !== false)) {
            var $filter = jQuery('<span>')
                .addClass('filter-menu')
                .attr('data-column', columnoptions.field)
                .html('<i class="material-icons">filter_list</i>')
                .appendTo($column)
                .on('click', function(e) {
                    e.stopPropagation();
                    if ($filterdropdown.is(':visible')) {
                        $filter.removeClass('active');
                    } else {
                        $filter.addClass('active');

                        jQuery(document).one('click', function closeMenu(e: JQuery.ClickEvent) {
                            if ($filterdropdown.has(e.target).length === 0) {
                                $filter.removeClass('active');
                            } else {
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
    //---------------------------------------------------------------------------------
    private _setSort($grid: JQuery, $column: JQuery, sortdirection?: string) {
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
            } else if (column_sort === 'desc') {
                sortdirection = 'off';
            } else if (column_sort === 'asc') {
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
    //---------------------------------------------------------------------------------
    private _columnHeaderClick($grid: JQuery, e: JQuery.ClickEvent) {
        this._setSort($grid, jQuery(e.currentTarget));
        this.databind($grid);
    }
    //---------------------------------------------------------------------------------
    private _addOrderBy($grid: JQuery, field: string, sortdirection: string) {
        var orderby = $grid.data('orderby');
        if (!orderby) {orderby = {}}

        orderby[field] = sortdirection;

        $grid.data('orderby', orderby);
    }
    //---------------------------------------------------------------------------------
    private _removeOrderBy($grid: JQuery, field: string) {
        var orderby = $grid.data('orderby');
        if (!orderby) {orderby = {}}

        delete orderby[field];

        $grid.data('orderby', orderby);
    }
    //---------------------------------------------------------------------------------
    private _getOrderBy($grid: JQuery) {
        var orderby       = $grid.data('orderby');
        var orderbystring = [];

        for (var key in orderby) {
            if (orderby.hasOwnProperty(key)) {
                orderbystring.push(key + ' ' + orderby[key]);
            }
        }

        return orderbystring.join(',');
    }
    //---------------------------------------------------------------------------------
    private _renderBody($grid: JQuery, options: GridOptions) {
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
    //---------------------------------------------------------------------------------
    private _select($grid: JQuery, e: JQuery.ClickEvent) {
        var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');

        if ((options.selectable !== 'multiple') || (options.selectable == 'multiple') && (!e.ctrlKey)) {
            $target.siblings().removeClass('state-selected');
        }

        $target.addClass('state-selected');
    }
    //---------------------------------------------------------------------------------
    private _singleclick($grid: JQuery, e: JQuery.ClickEvent) {
        var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');

        if (options.singleclick) {
            options.singleclick($grid, e);
        }
    }
    //---------------------------------------------------------------------------------
    private _doubleclick($grid: JQuery, e: JQuery.DoubleClickEvent) {
        var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');

        if (options.doubleclick) {
            options.doubleclick($grid, e);
        }
    }
    //---------------------------------------------------------------------------------
    private _keydown($grid: JQuery, e: JQuery.KeyDownEvent) {
        var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');
        var keycode: number      = e.keyCode;

        switch (keycode) {
            case FwFunc.keys.ENTER:
                //this._handleEnerKey($grid, e)
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
    //---------------------------------------------------------------------------------
    private _renderPager($grid: JQuery, options: GridOptions) {
        var me               = this;
        var pager: GridPager = options.pager as GridPager;

        var $pager  = jQuery('<div>')
            .addClass('grid-pager')
            .appendTo($grid);

        if (pager.refresh) {
            let $refresh = jQuery('<div>')
                .addClass('pager-refresh')
                .attr({'aria-label': pager.messages.refresh, 'title': pager.messages.refresh})
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
            var pageSizes       = pager.pagesizes.length ? pager.pagesizes : [5, 10, 25, 50, 100];
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
    //---------------------------------------------------------------------------------
    private _refreshClick($grid: JQuery, e: JQuery.ClickEvent) {
        this.databind($grid);
    }
    //---------------------------------------------------------------------------------
    private _pageChange($grid: JQuery, e: JQuery.ClickEvent) {
        var $target: JQuery = jQuery(e.currentTarget);

        if (!$target.hasClass('pager-nav-disabled')) {
            this._pageNumber($grid, parseInt($target.attr('data-page'), 10));
            this.databind($grid);
        }
    }
    //---------------------------------------------------------------------------------
    private _pageSizeChange($grid: JQuery, e: JQuery.ChangeEvent) {
        var value    = e.currentTarget.value;
        var pagesize = parseInt(value, 10);

        this._pageSize($grid, pagesize);
        this.databind($grid);
    }
    //---------------------------------------------------------------------------------
    private _pageNumber($grid: JQuery, pagenumber?: number) {
        if (pagenumber) {
            $grid.data('pagenumber', pagenumber);
        } else {
            return $grid.data('pagenumber');
        }
    }
    //---------------------------------------------------------------------------------
    private _pageSize($grid: JQuery, pagesize?: number) {
        if (pagesize) {
            $grid.data('pagesize', pagesize);
        } else {
            return $grid.data('pagesize');
        }
    }
    //---------------------------------------------------------------------------------
    async databind($grid: JQuery): Promise<any> {
        var me                   = this;
        var options: GridOptions = $grid.data('options');

        if (options.beforedatabind) {
            options.beforedatabind($grid);
        }

        return new Promise<any>(async (resolve, reject) => {
            let request = me._getRequest($grid);
            FwServices.module.method(request, request.module, options.datasource, $grid, function (response) {
                try {
                    me._databindCallback($grid, request, response);
                    resolve();
                } catch (ex) {
                    //FwFunc.showError(ex);
                    reject(ex);
                }
            });
        });
    }
    //---------------------------------------------------------------------------------
    private _databindCallback($grid: JQuery, request: any, response: FwJsonDataTable) {
        var me                   = this;
        var options: GridOptions = $grid.data('options');
        var page                 = me._pageNumber($grid);
        var pagesize             = me._pageSize($grid);
        var totalrows            = response.TotalRows;
        var totalpages           = response.TotalPages;
        var $tbody               = $grid.find('.grid-content tbody');

        //setting grid data for excelsheetdownload call in GateInterface - JG
        $grid.data('ondatabind', (request: any) => {
            request.filterfields = this._getFilters($grid);
            request.orderby = this._getOrderBy($grid);
        });
        $grid.data('totalRowCount', totalrows);
        $grid.attr('data-pageno', page);
        $grid.attr('data-pagesize', pagesize);
        

        $tbody.empty();
        for (var i = 0; i < response.Rows.length; i++) {
            var row  = response.Rows[i];
            var $row = jQuery('<tr>')
                .attr('role', 'row')
                .attr('data-rowid', row[response.ColumnIndex[options.rowid]])
                .appendTo($tbody);

            for (var j = 0; j < options.columns.length; j++) {
                let column      = options.columns[j];
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
                    } else {
                        $column.html(row[columnIndex]);
                    }
                }
            }

            //Remove when able to use getmany call--------------------
            var rowdata = {};
            for (var k = 0; k < response.Columns.length; k++) {
                rowdata[response.Columns[k].Name] = row[k];
            }
            $row.data('recorddata', rowdata);
            //--------------------------------------------------------
        }

        if (options.pager !== false) {
            var pager: GridPager = options.pager as GridPager;
            if (response.TotalRows > 0) {
                $grid.find('.grid-pager .pager-info').html(FwFunc.stringFormat(pager.messages.display,
                                                                               Math.min((page - 1) * (pagesize || 0) + 1, totalrows),
                                                                               Math.min(page * pagesize, totalrows),
                                                                               totalrows));
            } else {
                $grid.find('.grid-pager .pager-info').html(pager.messages.empty);
            }

            var pagerpageshtml = [];
            var buttoncount    = pager.buttoncount;
            var start          = 1;
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
    //---------------------------------------------------------------------------------
    private _getRequest($grid: JQuery): BrowseRequest {
        var me                   = this;
        var options: GridOptions = $grid.data('options');
        var request              = new BrowseRequest;

        request.module       = options.module;
        request.pageno       = this._pageNumber($grid);
        request.pagesize     = this._pageSize($grid);
        request.orderby      = this._getOrderBy($grid);
        request.filterfields = this._getFilters($grid); 
            

        if (options.filter) {
            request.filterfields = {...request.filterfields, ...options.filter($grid)};
        }

        if (options.filterable !== false) {
            var filterable = options.filterable as GridFilterable;
            var $filteredfields;
            if (filterable.mode === 'row') {
                $filteredfields = $grid.find('.grid-header .filtercell.filtered');
            } else if (filterable.mode === 'menu') {
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
    //---------------------------------------------------------------------------------
}

var FwGrid = new FwGridClass();


interface GridOptions {
    module?: string;
    datasource?: string;
    title?: string|Function;
    columns?: GridColumn[];
    pager?: boolean|GridPager;
    editable?: boolean|GridEditable;
    rowid?: string;
    selectable?: boolean|string;
    menu?: boolean|GridMenu;
    filterable?: boolean|GridFilterable;

    filter?: ($grid: JQuery) => Object;
    singleclick?($grid: JQuery, e: JQuery.ClickEvent): void;
    doubleclick?($grid: JQuery, e: JQuery.DoubleClickEvent): void;
    beforedatabind?($grid: JQuery): void;
    afterdatabind?($grid: JQuery): void;
}

interface GridFilterable {
    mode?: 'row'|'menu';
}

interface GridColumn {
    datatype?: string;
    field: string;
    hidden?: boolean;
    sort?: 'asc'|'desc'|boolean;
    title: string;
    width?: string|number;
    filterable?: boolean|GridColumnFilterable;
}

interface GridColumnFilterable {
    datatype?: string;
}

interface GridEditable {

}

interface GridMenu {
    //objects?: (GridMenuButton | GridMenuFilter | GridMenuDropDownButton | 'seperator')[];
    objects?: GridMenuObject[];
}

type GridMenuObject = GridMenuButton | GridMenuFilter | GridMenuDropDownButton | 'seperator';

interface GridMenuButton {
    type: 'button';
    caption?: string;
    secid?: string;
    validateSecurity?($grid: JQuery): boolean;
    icon?: string;
    id?: string;
    action?($grid: JQuery, e: JQuery.ClickEvent): void;
}

interface GridMenuDropDownButton {
    type: 'dropdownbutton';
    caption?: string;
    items?: (GridMenuDropDownButtonItem | 'seperator')[];
}

interface GridMenuDropDownButtonItem {
    caption?: string;
    secid?: string;
    validateSecurity?($grid: JQuery): boolean;
    icon?: string;
    id?: string;
    action?($grid: JQuery, e: JQuery.ClickEvent): void;
}

interface GridMenuFilter {
    type: 'filter';
    caption?: string;
    value?: string;
    filtertype?: 'standard'|'checkbox';
    items?: (GridMenuFilterItem | 'seperator')[];
}

interface GridMenuFilterItem {
    caption?: string;
    value?: string;
    defaultselected?: boolean;
}

interface GridPager {
    pagesize?: number;
    pagesizes?: boolean|any;
    refresh?: boolean;
    buttoncount?: number;
    messages?: GridPagerMessages;
}

interface GridPagerMessages {
    allPages?: string,
    display?: string;
    empty?: string;
    page?: string;
    of?: string;
    itemsPerPage?: string;
    first?: string;
    last?: string;
    next?: string;
    previous?: string;
    refresh?: string;
    morePages?: string;
}
