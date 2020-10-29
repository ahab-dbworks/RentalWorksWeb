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
        options = {...this.options, ...options};

        if (options.title) {
            this._renderTitle($grid, options);
        }

        if (options.menu) {
            this._renderMenu($grid, options);
        }

        this._renderHeader($grid, options);

        this._renderBody($grid, options);

        if (options.pager) {
            if (options.pager === true) options.pager = this.options.pager;
            this._renderPager($grid, options);
        }

        $grid.data('options', options)
            .addClass(this.GRID);
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
        var me             = this;
        var menu: GridMenu = options.menu as GridMenu;

        var $menu = jQuery('<div>')
            .addClass('grid-menu')
            .appendTo($grid);

        if (menu.options) {
            this._addOptions($grid, $menu, menu.options);
        }

        if (menu.objects) {
            for (let object of menu.objects) {
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
    private _addOptions($grid: JQuery, $menu: JQuery, options: (GridMenuOption | GridMenuExcel)[]) {
        var me = this;
        var $dropdownbutton = jQuery('<div>')
            .addClass('grid-menu-dropdownbutton')
            .attr('tabindex', '-1')
            .on('focusout', (e) => {
                setTimeout(function() {
                    if (document.hasFocus() && !jQuery.contains($dropdownbutton[0], e.currentTarget) && e) {
                        $dropdown.removeClass('active');
                    }
                }, 0);
            });

        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html('<i class="material-icons">more_vert</i>')
            .appendTo($dropdownbutton)
            .on('click', function(e) {
                //e.stopPropagation();
                if ($dropdown.is(':visible')) {
                    $dropdown.removeClass('active');
                } else {
                    $dropdown.addClass('active');
                }
            });

        var $dropdown = jQuery('<div>')
            .addClass('grid-menu-dropdown')
            .appendTo($dropdownbutton);
        let hasChildItems = false;
        for (let option of options) {
            if (FwApplicationTree.isVisibleInSecurityTree(option.securityid)) {
                hasChildItems = true;
                if (option.type === 'option') {
                    let $item = jQuery('<div>')
                        .addClass('dropdown-item')
                        .appendTo($dropdown)
                        .on('click', (e) => {
                            (option as GridMenuOption).action($grid, e);
                            $dropdown.removeClass('active');
                        });

                    let $caption = jQuery('<div>')
                        .addClass('dropdown-item-caption')
                        .html(option.caption)
                        .appendTo($item);
                } else if (option.type === 'excel') {
                    let $item = jQuery('<div>')
                        .addClass('dropdown-item')
                        .appendTo($dropdown)
                        .on('click', (e) => {
                            this._downloadExcelWorkbook($grid);
                            $dropdown.removeClass('active');
                        });

                    let $caption = jQuery('<div>')
                        .addClass('dropdown-item-caption')
                        .html('Export to Excel')
                        .appendTo($item);
                }
            }
        }
        if (hasChildItems) {
            $dropdownbutton.appendTo($menu)
        }
    }
    //---------------------------------------------------------------------------------
    private _addButton($grid: JQuery, $menu: JQuery, button: GridMenuButton) {
        if (FwApplicationTree.isVisibleInSecurityTree(button.securityid)) {
            const $button = jQuery('<div>')
                .addClass('grid-menu-button')
                .html(button.caption)
                .appendTo($menu)
                .on('click', (e) => button.action($grid, e));

            if (button.icon) {
                jQuery(`<i class="material-icons left">${button.icon}</i>`).prependTo($button);
            }
        }
    }
    //---------------------------------------------------------------------------------
    private _addDropDownButton($grid: JQuery, $menu: JQuery, button: GridMenuDropDownButton) {
        var me = this;

        var $dropdownbutton = jQuery('<div>')
            .addClass('grid-menu-dropdownbutton')
            .attr('tabindex', '-1')
            .on('focusout', (e) => {
                setTimeout(function() {
                    if (document.hasFocus() && !jQuery.contains($dropdownbutton[0], e.currentTarget) && e) {
                        $dropdown.removeClass('active');
                    }
                }, 0);
            });

        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(`${button.caption}<i class="material-icons right">arrow_drop_down</i>`)
            .appendTo($dropdownbutton)
            .on('click', function(e) {
                //e.stopPropagation();
                if ($dropdown.is(':visible')) {
                    $dropdown.removeClass('active');
                } else {
                    $dropdown.addClass('active');
                }
            });

        if (button.icon) {
            jQuery(`<i class="material-icons left">${button.icon}</i>`).prependTo($button);
        }

        var $dropdown = jQuery('<div>')
            .addClass('grid-menu-dropdown')
            .appendTo($dropdownbutton);
        let hasChildItems = false;
        for (let item of button.items) {
            if (item === 'seperator') {
                jQuery('<div>')
                    .addClass('dropdown-seperator')
                    .appendTo($dropdown);
            } else {
                const dropDownButtonItem = <GridMenuDropDownButtonItem>item;
                if (FwApplicationTree.isVisibleInSecurityTree(dropDownButtonItem.securityid)) {
                    hasChildItems = true;
                    let $item = jQuery('<div>')
                        .addClass('dropdown-item')
                        .appendTo($dropdown)
                        .on('click', (e) => {
                            (item as GridMenuDropDownButtonItem).action($grid, e);
                            $dropdown.removeClass('active');
                        });


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
            .attr('tabindex', '-1')
            .appendTo($menu)
            .on('focusout', (e) => {
                setTimeout(function() {
                    if (document.hasFocus() && !jQuery.contains($filter[0], e.currentTarget) && e) {
                        $dropdown.removeClass('active');
                    }
                }, 0);
            });

        var $button = jQuery('<div>')
            .addClass('grid-menu-button')
            .html(`${filter.caption}<i class="material-icons right">arrow_drop_down</i>`)
            .appendTo($filter)
            .on('click', function(e) {
                //e.stopPropagation();
                if ($dropdown.is(':visible')) {
                    $dropdown.removeClass('active');
                } else {
                    $dropdown.addClass('active');
                }
            });

        var $dropdown = jQuery('<div>')
            .addClass('grid-menu-dropdown')
            .appendTo($filter);

        for (let item of filter.items) {
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

        $grid.data('thead', $thead);

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

        if (options.selectable === 'checkbox') {
            let $col = jQuery('<col>')
                .css('width', '52px')
                .appendTo($colgroup);

            let $column = jQuery(`<th>`)
                .addClass('header')
                .attr('style', 'text-align:center;')
                .appendTo($tr);

            let $headercheckbox = jQuery('<input>')
                .attr('type', 'checkbox')
                .attr('aria-label', 'Select all rows')
                .attr('aria-checked', 'false')
                .addClass('fw-checkbox toggleall-select')
                .appendTo($column)
                .on('change', function(e) {
                    me._headerCheckboxClick($grid, e);
                });

            if (options.filterable !== false) {
                me._addColumnFilter($grid, options.filterable, $trfilter, $column, null, false);
            }
        }

        for (let column of options.columns) {
            if (!column.hidden) {
                let $col = jQuery('<col>')
                    .appendTo($colgroup);

                if (column.width) {
                    $col.css('width', column.width);
                }

                let $column = jQuery(`<th>`)
                    .addClass('header')
                    .attr('data-column', column.field)
                    .appendTo($tr);

                let $headerbody = jQuery('<div>')
                    .addClass('header-body')
                    .html(column.title)
                    .appendTo($column);

                if (column.sort !== false) {
                    $column.addClass('sortable');
                    $headerbody.on('click', (e) => {
                        //me._columnHeaderClick($grid, e)
                        me._setSort($grid, jQuery(e.currentTarget).parent());
                        me.databind($grid);
                    });
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
                        me.applyFilter($grid);
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
                //.html('<i class="material-icons">filter_list</i>')
                .attr('tabindex', '-1')
                .appendTo($column)
                .on('focusout', (e) => {
                    setTimeout(function() {
                        if (document.hasFocus() && !jQuery.contains($filter[0], e.currentTarget) && e) {
                            $filter.removeClass('active');
                        }
                    }, 0);
                });

            var $filterbtn = jQuery('<div>')
                .addClass('filter-menu-button')
                .html('<i class="material-icons">filter_alt</i>')
                .appendTo($filter)
                .on('click', function(e) {
                    //e.stopPropagation();
                    if ($filterdropdown.is(':visible')) {
                        $filter.removeClass('active');
                    } else {
                        $filter.addClass('active');
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

        $column.find('.header-body .header-sort').remove();
        me._removeOrderBy($grid, $column.attr('data-column'));

        if (sortdirection === 'asc' || sortdirection === 'desc') {
            var icon = sortdirection === 'asc' ? 'arrow_upward' : 'arrow_downward';
            $column.find('.header-body').append(`<span class="header-sort"><i class="material-icons">${icon}</i></span>`);

            me._addOrderBy($grid, $column.attr('data-column'), sortdirection);
        }
        $column.data('sort', sortdirection);
    }
    //---------------------------------------------------------------------------------
    //private _columnHeaderClick($grid: JQuery, e: JQuery.ClickEvent) {
    //    this._setSort($grid, jQuery(e.currentTarget));
    //    this.databind($grid);
    //}
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

        $grid.data('table', $table);
        $grid.data('tbody', $tbody);

        if (options.selectable === 'checkbox') {
            let $col = jQuery('<col>')
                .css('width', '52px')
                .appendTo($colgroup);
        } else {
            $table.attr('tabindex', '0');
        }

        for (let column of options.columns) {
            if (!column.hidden) {
                let $col = jQuery('<col>')
                    .appendTo($colgroup);

                if (column.width) {
                    $col.css('width', column.width);
                }
            }
        }

        $table.on('keydown',  (e) => me._tableKeyDown($grid, e))
              .on('focus',    (e) => me._tableFocus($grid, e))
              .on('focusout', (e) => me._tableBlur($grid, e));
    }
    //---------------------------------------------------------------------------------
    focus($grid: JQuery, $element?: JQuery) {
        return this._setFocus($grid, $element);
    }
    //---------------------------------------------------------------------------------
    private _setFocus($grid: JQuery, $element: JQuery) {
        var $current = $grid.data('focusedElement');
        if ($element.length) {
            if (!$current || $current !== $element) {
                this._updateFocus($grid, $current, $element);
            }
        }

        return $grid.data('focusedElement');
    }
    //---------------------------------------------------------------------------------
    private _updateFocus($grid: JQuery, $current: JQuery, $next: JQuery) {
        $grid.data('focusedElement', $next);
    }
    //---------------------------------------------------------------------------------
    private _removeFocus($grid: JQuery) {
        if ($grid.data('focusedElement')) {
            $grid.data('focusedElement', null);
        }
    }
    //---------------------------------------------------------------------------------
    select($grid: JQuery, $rows: JQuery) {
        var me                   = this;
        var options: GridOptions = $grid.data('options');

        if (($rows.length) && (options.selectable)) {
            if (options.selectable === true) {
                me.clearSelection($grid);
                $rows.first().addClass('state-selected');
            } else if (options.selectable === 'multiple') {
                $rows.each(function () {
                    jQuery(this).addClass('state-selected');
                })
            } else if (options.selectable === 'checkbox') {
                me._checkRows($grid, $rows);
                if ($grid.data('tbody').find('tr').length === $grid.data('tbody').find('tr.state-selected').length) {
                    me._toggleHeaderCheckState($grid, true);
                }
            }
            this._scrollSelected($grid, $rows.first())
        }

        $grid.data('selected', $grid.data('tbody').find('tr.state-selected'));
    }
    //---------------------------------------------------------------------------------
    clearSelection($grid: JQuery, $rows?: JQuery) {
        var me                   = this;
        var options: GridOptions = $grid.data('options');

        if (!$rows) {
            $rows = $grid.data('tbody').find('tr');
        }

        if (options.selectable && options.selectable !== 'checkbox') {
            $rows.each(function () {
                jQuery(this).removeClass('state-selected');
            });
        } else if (options.selectable === 'checkbox') {
            me._deselectCheckRows($grid, $rows);
        }

        $grid.data('selected', null);
    }
    //---------------------------------------------------------------------------------
    private _scrollSelected($grid: JQuery, $row: JQuery) {
        var me                   = this;
        var options: GridOptions = $grid.data('options');
        var $gridContent         = $grid.data('table').parent();
        var containerScroll      = $gridContent[0].scrollTop;
        var containerHeight      = $gridContent[0].clientHeight;
        var elementOffset        = $row[0].offsetTop;
        var elementHeight        = $row[0].offsetHeight;
        var bottomDistance       = elementOffset + elementHeight;
        var result               = 0;

        if (containerScroll > elementOffset) {
            result = elementOffset;
        } else if (bottomDistance > containerScroll + containerHeight) {
            if (elementHeight <= containerHeight) {
                result = bottomDistance - containerHeight;
            } else {
                result = elementOffset;
            }
        } else {
            result = containerScroll;
        }
        $gridContent.scrollTop(result);
    }
    //---------------------------------------------------------------------------------
    private _rowSingleClick($grid: JQuery, e: JQuery.ClickEvent) {
        var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');
        var $row                 = jQuery(e.target).closest('tr');

        this._setFocus($grid, $row);

        if ((options.selectable) && (options.selectable !== 'checkbox')) {
            if (options.selectable === true) {
                this.select($grid, $row);
            } else if (options.selectable === 'multiple') {
                if (!e.ctrlKey) {
                    this.clearSelection($grid);
                }
                if ($row.hasClass('state-selected')) {
                    this.clearSelection($grid, $row);
                } else {
                    this.select($grid, $row);
                }
            }
        }

        if (options.singleclick) {
            options.singleclick($grid, e);
        }
    }
    //---------------------------------------------------------------------------------
    private _rowDoubleClick($grid: JQuery, e: JQuery.DoubleClickEvent) {
        var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');

        if (options.doubleclick) {
            options.doubleclick($grid, e);
        }
    }
    //---------------------------------------------------------------------------------
    private _tableFocus($grid: JQuery, e: JQuery.FocusEvent) {
        var $current             = $grid.data('focusedElement');
        var options: GridOptions = $grid.data('options');

        if ((!$current) && ($grid.data('tbody').find('tr').length)) {
            this._setFocus($grid, $grid.data('tbody').find('tr').first());

            setTimeout(() => {
                if (((options.selectable === true) || (options.selectable === 'multiple')) && (!$grid.data('selected'))) {
                    this.select($grid, $grid.data('tbody').find('tr').first());
                }
            }, 125);
        }
    }
    //---------------------------------------------------------------------------------
    private _tableBlur($grid: JQuery, e: JQuery.FocusOutEvent) {

    }
    //---------------------------------------------------------------------------------
    private _tableKeyDown($grid: JQuery, e: JQuery.KeyDownEvent) {
        //var $target: JQuery      = jQuery(e.currentTarget);
        var options: GridOptions = $grid.data('options');
        var $currentFocus        = $grid.data('focusedElement');
        var handled              = false;

        switch (e.keyCode) {
            case FwFunc.keys.ENTER:
                handled = this._handleEnterKey($grid, $currentFocus)
                break;
            case FwFunc.keys.LEFT:
                
                break;
            case FwFunc.keys.UP:
                handled = this._handleUpKey($grid, $currentFocus, e.shiftKey);
                break;
            case FwFunc.keys.RIGHT:
                
                break;
            case FwFunc.keys.DOWN:
                handled = this._handleDownKey($grid, $currentFocus, e.shiftKey);
                break;
        }

        if (handled) {
            e.preventDefault();
            e.stopPropagation();
        }
    }
    //---------------------------------------------------------------------------------
    //#region Key Bind Functions
    private _handleEnterKey($grid: JQuery, $currentFocus: JQuery): boolean {
        var options: GridOptions = $grid.data('options');

        return false;
    }
    //---------------------------------------------------------------------------------
    private _handleUpKey($grid: JQuery, $currentFocus: JQuery, shiftKey: boolean): boolean {
        var options: GridOptions = $grid.data('options');
        var $tbody               = $grid.data('tbody');
        var $nextElement;

        if (options.selectable) {
            $nextElement = this._previousVerticalRow($grid, $tbody, $currentFocus);
            if ($nextElement.length) {
                if (options.selectable === true) {
                    this.select($grid, $nextElement);
                } else if (options.selectable === 'multiple') {
                    if (!shiftKey) {
                        this.clearSelection($grid);
                    }
                    if ($nextElement.hasClass('state-selected')) {
                        this.clearSelection($grid, $currentFocus);
                    } else {
                        this.select($grid, $nextElement);
                    }
                }
            }
        } else if (options.editable) {

        }

        this._setFocus($grid, $nextElement);
        return true;
    }
    //---------------------------------------------------------------------------------
    private _handleDownKey($grid: JQuery, $currentFocus: JQuery, shiftKey: boolean): boolean {
        var options: GridOptions = $grid.data('options');
        var $tbody               = $grid.data('tbody');
        var $nextElement;

        if (options.selectable) {
            $nextElement = this._nextVerticalRow($grid, $tbody, $currentFocus);
            if ($nextElement.length) {
                if (options.selectable === true) {
                    this.select($grid, $nextElement);
                } else if (options.selectable === 'multiple') {
                    if (!shiftKey) {
                        this.clearSelection($grid);
                    }
                    if ($nextElement.hasClass('state-selected')) {
                        this.clearSelection($grid, $currentFocus);
                    } else {
                        this.select($grid, $nextElement);
                    }
                }
            }
        } else if (options.editable) {

        }

        this._setFocus($grid, $nextElement);
        return true;
    }
    //#endregion
    //---------------------------------------------------------------------------------
    private _previousVerticalRow($grid: JQuery, $tbody: JQuery, $currentRow: JQuery) {
        return $currentRow.prev();
    }
    //---------------------------------------------------------------------------------
    private _nextVerticalRow($grid: JQuery, $tbody: JQuery, $currentRow: JQuery) {
        return $currentRow.next();
    }
    //---------------------------------------------------------------------------------
    private _renderPager($grid: JQuery, options: GridOptions) {
        var me               = this;
        var pager: GridPager = options.pager as GridPager;

        var $pager  = jQuery('<div>')
            .addClass('grid-pager')
            .appendTo($grid);

        $grid.data('pager', $pager);

        if (pager.refresh) {
            let $refresh = jQuery('<div>')
                .addClass('pager-refresh')
                .attr({'aria-label': pager.messages.refresh, 'title': pager.messages.refresh})
                .html('<i class="material-icons">refresh</i>')
                .appendTo($pager)
                .on('click', (e) => me._refreshClick($grid, e));
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
            .on('click', '.pager-nav, .pager-page', (e) => me._pageChange($grid, e));
        me._pageNumber($grid, 1);

        if (pager.pagesizes) {
            var pageSizes       = pager.pagesizes.length ? pager.pagesizes : [5, 10, 25, 50, 100];
            var pageSizeOptions = jQuery.map(pageSizes, function (size) {
                return `<option value="${size}">${size}</option>`;
            });
            let $select = jQuery('<select>')
                .html(pageSizeOptions.join(''))
                .val(pager.pagesize)
                .on('change', (e) => me._pageSizeChange($grid, e));

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
    private _totalRows($grid: JQuery, totalrows?: number) {
        if (totalrows) {
            $grid.data('totalrows', totalrows);
        } else {
            return $grid.data('totalrows');
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
            FwServices.module.method(request, request.module, `${options.datasource === request.module.toLowerCase() ? '' : options.datasource + '/'}browse`, $grid, function (response) {
                try {
                    me.clearSelection($grid);
                    me._removeFocus($grid);
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

        me._totalRows($grid, response.TotalRows);

        $tbody.empty();
        for (let row of response.Rows) {
            var $row = jQuery('<tr>')
                .attr('role', 'row')
                .attr('data-rowid', row[response.ColumnIndex[options.rowid]])
                .appendTo($tbody)
                .on('click',    (e) => me._rowSingleClick($grid, e))
                .on('dblclick', (e) => me._rowDoubleClick($grid, e));

            if (options.selectable === 'checkbox') {
                let $column = jQuery(`<td>`)
                    .attr('role', 'gridcell')
                    .attr('style', 'text-align:center;')
                    .appendTo($row);

                let $checkbox = jQuery('<input>')
                    .attr('type', 'checkbox')
                    .attr('aria-label', 'Select row')
                    .attr('aria-checked', 'false')
                    .addClass('fw-checkbox row-select')
                    .appendTo($column)
                    .on('change', (e) => me._checkboxClick($grid, e));
            }

            for (let column of options.columns) {
                if (!column.hidden) {
                    let $column = jQuery('<td>')
                        .attr('role', 'gridcell')
                        .attr('data-column', column.field)
                        .appendTo($row);

                    if (column.datatype == 'tag') {
                        for (let tag of column.tags) {
                            var columnIndex = response.ColumnIndex[tag.field];
                            tag.taglogic($grid, $column, row[columnIndex]);
                        }
                    } else {
                        var columnIndex = response.ColumnIndex[column.field];
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
            if ((typeof response.ColumnIndex['Inactive'] === 'number') && (row[response.ColumnIndex['Inactive']] === true)) {
                $row.addClass('inactive');
            }
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
    private _headerCheckboxClick($grid: JQuery, e: JQuery.ChangeEvent) {
        var me        = this;
        var $checkbox = jQuery(e.target);
        var checked   = $checkbox.prop('checked');
        var $rows     = $grid.data('tbody').find('tr');

        if (checked) {
            me.select($grid, $rows)
        } else {
            me.clearSelection($grid);
        }
    }
    //---------------------------------------------------------------------------------
    private _toggleHeaderCheckState($grid: JQuery, checked: boolean) {
        var me = this;
        if (checked) {
            $grid.data('thead').find('input.toggleall-select').attr('aria-checked', 'true').prop('checked', true).attr('aria-label', 'Deselect all rows');
        } else {
            $grid.data('thead').find('input.toggleall-select').attr('aria-checked', 'false').prop('checked', false).attr('aria-label', 'Select all rows');
        }
    }
    //---------------------------------------------------------------------------------
    private _checkboxClick($grid: JQuery, e: JQuery.ChangeEvent) {
        var me          = this;
        var $row        = jQuery(e.target).closest('tr');
        var isSelecting = !$row.hasClass('state-selected');

        if (isSelecting) {
            me.select($grid, $row);
        } else {
            me._deselectCheckRows($grid, $row);
        }
    }
    //---------------------------------------------------------------------------------
    private _checkRows($grid: JQuery, $rows: JQuery) {
        $rows.each(function () {
            jQuery(this).addClass('state-selected').find('input.row-select').attr('aria-checked', 'true').prop('checked', true).attr('aria-label', 'Deselect row');
        })
    }
    //---------------------------------------------------------------------------------
    private _deselectCheckRows($grid: JQuery, $rows: JQuery) {
        var me = this;
        $rows.each(function () {
            jQuery(this).removeClass('state-selected').find('input.row-select').attr('aria-checked', 'false').prop('checked', false).attr('aria-label', 'Select row');
        });
        me._toggleHeaderCheckState($grid, false);
    }
    //---------------------------------------------------------------------------------
    addTag($grid: JQuery, $column: JQuery, htmlvalue: string, backgroundcolor: string, color: string) {
        var $tag = jQuery('<div>')
            .addClass('tag')
            .css({'background-color': backgroundcolor, 'color': color})
            .html(htmlvalue)
            .appendTo($column);
    }
    //---------------------------------------------------------------------------------
    async applyFilter($grid: JQuery): Promise<any> {
        this._pageNumber($grid, 1);
        return await this.databind($grid);
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
    private _downloadExcelWorkbook($grid: JQuery): void {
        var me                   = this;
        var options: GridOptions = $grid.data('options');
        var totalrows            = me._totalRows($grid);

        if (totalrows >= 1) {
            const $confirmation = FwConfirmation.renderConfirmation('Download Excel Workbook', '');
            const $yes          = FwConfirmation.addButton($confirmation, 'Download', false);
            const $no           = FwConfirmation.addButton($confirmation, 'Cancel');

            let html = `<div class="fwform" data-controller="none">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Download All ${totalrows.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")} Records" data-datafield="allrecords"></div>
                          </div>
                          <div class="formrow" style="width:100%;display:flex;align-content:flex-start;align-items:center;padding-bottom:13px;">
                            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-datafield="userdefinedrecords" style="float:left;width:30px;"></div>
                            </div>
                            <span style="margin:18px 0px 0px 0px;">First</span>
                            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin:0px 0px 0px 0px;">
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="" data-datafield="userdefinedrecordscount" style="width:80px;float:left;margin:0px 0px 0px 0px;"></div>
                            </div>
                            <span style="margin:18px 0px 0px 0px;">Records</span>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include All Fields" data-datafield="allfields" style="float:left;width:100px;"></div>
                          </div>
                          <div class="flexrow fieldsrow" style="display:none;">
                            <div class="flexcolumn">
                              <div class="flexrow" style="margin-left:8px;width:350px;">
                                <div class="check-uncheck" style="color:#2626f3;cursor:pointer;flex:1;">Uncheck All Fields</div>
                                <div class="sort-list" style="color:#2626f3;cursor:pointer;flex:1;text-align:right;">Sort List By Name</div>
                              </div>
                              <div class="flexrow">
                                <div data-control="FwFormField" class="fwcontrol fwformfield" data-checkboxlist="persist" data-type="checkboxlist" data-sortable="true" data-orderby="false" data-caption="Include these fields" data-datafield="FieldList" style="flex:1 1 550px;"></div>
                              </div>
                            </div>
                          </div>
                        </div>`;
            FwConfirmation.addControls($confirmation, html);

            FwFormField.setValue($confirmation, 'div[data-datafield="userdefinedrecordscount"]', (me._pageSize($grid) > totalrows) ? totalrows : me._pageSize($grid));
            FwFormField.setValue($confirmation, 'div[data-datafield="allrecords"]', true);
            FwFormField.setValue($confirmation, 'div[data-datafield="allfields"]', true);

            $confirmation
                .on('change', 'div[data-datafield="allrecords"] input.fwformfield-value', function() {
                    FwFormField.setValue($confirmation, 'div[data-datafield="userdefinedrecords"]', !(FwFormField.getValue($confirmation, 'div[data-datafield="allrecords"]') === 'T'));
                })
                .on('change', 'div[data-datafield="userdefinedrecords"] input.fwformfield-value', function() {
                    FwFormField.setValue($confirmation, 'div[data-datafield="allrecords"]', !(FwFormField.getValue($confirmation, 'div[data-datafield="userdefinedrecords"]') === 'T'));
                })
                .on('change', 'div[data-datafield="userdefinedrecordscount"] input.fwformfield-value', function() {
                    FwFormField.setValue($confirmation, 'div[data-datafield="userdefinedrecords"]', true);
                    FwFormField.setValue($confirmation, 'div[data-datafield="allrecords"]', false);
                })
                .on('change', 'div[data-datafield="allfields"] input.fwformfield-value', function () {
                    if (FwFormField.getValueByDataField($confirmation, 'allfields') === 'T') {
                        $confirmation.find('.fieldsrow').hide();
                    } else {
                        $confirmation.find('.fieldsrow').show();
                        if ($confirmation.find('div[data-datafield="FieldList"]').attr('api-req') !== 'true') {
                            FwAppData.apiMethod(true, 'GET', `${(<any>window[options.module + 'Controller']).apiurl}/${options.datasource === options.module.toLowerCase() ? '' : options.datasource + '/'}emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                                const fieldsAllCheckedUnsorted = [];
                                const fieldsNoneCheckedUnsorted = [];

                                for (let key in response) {
                                    if (!key.startsWith('_')) {
                                        fieldsAllCheckedUnsorted.push({
                                            'value': key,
                                            'text': key,
                                            'selected': 'T',
                                        });
                                        fieldsNoneCheckedUnsorted.push({
                                            'value': key,
                                            'text': key,
                                            'selected': 'F',
                                        })
                                    }
                                }
                                FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fieldsAllCheckedUnsorted, false);
                                $confirmation.find('div[data-datafield="FieldList"]').attr('api-req', 'true');
                                $confirmation.find('.fieldsrow').data('fieldsAllCheckedUnsorted', fieldsAllCheckedUnsorted)
                                $confirmation.find('.fieldsrow').data('fieldsNoneCheckedUnsorted', fieldsNoneCheckedUnsorted);

                                const allChecked = fieldsAllCheckedUnsorted.slice();
                                $confirmation.find('.fieldsrow').data('fieldsAllCheckedSorted', allChecked.sort(function (a, b) { return (a.text > b.text) ? 1 : ((b.text > a.text) ? -1 : 0); }));
                                const noneChecked = fieldsNoneCheckedUnsorted.slice();
                                $confirmation.find('.fieldsrow').data('fieldsNoneCheckedSorted', noneChecked.sort(function (a, b) { return (a.text > b.text) ? 1 : ((b.text > a.text) ? -1 : 0); }));

                            }, function onError(response) {
                                FwFunc.showError(response);
                            }, null);
                        }
                    }
                })
                .on('click', '.check-uncheck', e => {
                    if ($confirmation.find('.check-uncheck').text() === 'Check All Fields') {
                        // caption uncheck all
                        $confirmation.find('.check-uncheck').text('Uncheck All Fields');
                        if ($confirmation.find('.sort-list').text() === 'Sort List By Name') {
                            // check all, sorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedUnsorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        } else {
                            // check all, unsorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedSorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        }
                    } else {
                        //caption check all
                        $confirmation.find('.check-uncheck').text('Check All Fields');
                        if ($confirmation.find('.sort-list').text() === 'Sort List By Name') {
                            // uncheck all, unsorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedUnsorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        } else {
                            // uncheck all, unsorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedSorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        }
                    }
                })
                .on('click', '.sort-list', e => {
                    if ($confirmation.find('.sort-list').text() === 'Sort List By Name') {
                        //caption unsort
                        $confirmation.find('.sort-list').text('Unsort List By Name')
                        if ($confirmation.find('.check-uncheck').text() === 'Check All Fields') {
                            // uncheck all, sorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedSorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        } else {
                            // check all, sorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedSorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        }
                    } else {
                        //caption sort
                        $confirmation.find('.sort-list').text('Sort List By Name')
                        if ($confirmation.find('.check-uncheck').text() === 'Check All Fields') {
                            // uncheck all, unsorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedUnsorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        } else {
                            // check all, unsorted
                            const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedUnsorted');
                            FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                        }
                    }
                })
            ;

            $yes.on('click', () => {
                const request: any = me._getRequest($grid);
                request.pagesize = (FwFormField.getValue($confirmation, 'div[data-datafield="userdefinedrecords"]') === 'T') ? Number(FwFormField.getValue($confirmation, 'div[data-datafield="userdefinedrecordscount"]')) : totalrows;
                if (FwFormField.getValueByDataField($confirmation, 'allfields') === 'T') {
                    request.includeallcolumns = true;
                } else {
                    request.includeallcolumns = false;
                    request.excelfields = FwFormField.getValueByDataField($confirmation, 'FieldList');
                }

                FwServices.module.method(request, request.module, `${options.datasource === request.module.toLowerCase() ? '' : options.datasource + '/'}exportexcelxlsx`, $grid, function (response) {
                    try {
                        const $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${response.downloadUrl}" style="display:none;"></iframe>`);
                        jQuery('#application').append($iframe);
                        setTimeout(function () {
                            $iframe.remove();
                        }, 500);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwConfirmation.destroyConfirmation($confirmation);
                FwNotification.renderNotification('INFO', 'Downloading Excel Workbook...');
            });
        } else {
            FwNotification.renderNotification('WARNING', 'There are no records to export.');
        }
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
    selectable?: boolean|'multiple'|'checkbox'; //string;
    menu?: boolean|GridMenu;
    filterable?: boolean|GridFilterable;
    filter?:($grid: JQuery) => Object;
    singleclick?($grid: JQuery, e: JQuery.ClickEvent): void;
    doubleclick?($grid: JQuery, e: JQuery.DoubleClickEvent): void;
    beforedatabind?($grid: JQuery): void;
    afterdatabind?($grid: JQuery): void;
}

interface GridFilterable {
    mode: 'row'|'menu';
}

interface GridColumn {
    datatype?: string;
    field: string;
    hidden?: boolean;
    sort?: 'asc'|'desc'|boolean;
    title: string;
    width?: string|number;
    filterable?: boolean|GridColumnFilterable;
    tags?: GridTag[];
}

interface GridColumnFilterable {
    datatype: string;
}

interface GridTag {
    field: string;
    taglogic($grid: JQuery, $column: JQuery, datavalue): void;
}

interface GridEditable {

}

interface GridMenu {
    options?: (GridMenuOption | GridMenuExcel)[];
    objects?: (GridMenuButton | GridMenuFilter | GridMenuDropDownButton | 'seperator')[];
}

interface GridMenuOption {
    type: 'option';
    caption: string;
    securityid?: string;
    action?($grid: JQuery, e: JQuery.ClickEvent): void;
    validateSecurity?($grid: JQuery): boolean;
}

interface GridMenuExcel {
    type: 'excel';
    securityid: string;
}

interface GridMenuButton {
    type: 'button';
    caption?: string;
    securityid?: string;
    icon?: string;
    id?: string;
    action?($grid: JQuery, e: JQuery.ClickEvent): void;
    validateSecurity?($grid: JQuery): boolean;
}

interface GridMenuDropDownButton {
    type: 'dropdownbutton';
    caption?: string;
    icon?: string;
    items?: (GridMenuDropDownButtonItem | 'seperator')[];
}

interface GridMenuDropDownButtonItem {
    caption?: string;
    securityid?: string;
    icon?: string;
    id?: string;
    action?($grid: JQuery, e: JQuery.ClickEvent): void;
    validateSecurity?($grid: JQuery): boolean;
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
