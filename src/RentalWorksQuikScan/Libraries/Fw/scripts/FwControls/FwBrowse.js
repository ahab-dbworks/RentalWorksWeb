var FwBrowseClass = (function () {
    function FwBrowseClass() {
        this.setAfterRenderRowCallback = function ($control, callback) {
            $control.data('afterrenderrow', callback);
        };
        this.setAfterRenderFieldCallback = function ($control, callback) {
            $control.data('afterrenderfield', callback);
        };
    }
    FwBrowseClass.prototype.upgrade = function ($control) {
        var properties, i, data_type;
        data_type = $control.attr('data-type');
        properties = this.getDesignerProperties(data_type);
        for (i = 0; i < properties.length; i++) {
            if (typeof $control.attr(properties[i].attribute) === 'undefined') {
                $control.attr(properties[i].attribute, properties[i].defaultvalue);
            }
        }
    };
    FwBrowseClass.prototype.init = function ($control) {
        var me = this;
        var $columns;
        $control.on('mousewheel', '.txtPageNo', function (event) {
            if (jQuery('.pleasewait').length === 0) {
                if (event.deltaY < 0) {
                    $control.find('.btnNextPage').click();
                }
                else if (event.deltaY > 0) {
                    $control.find('.btnPreviousPage').click();
                }
            }
        });
        $columns = $control.find('div.column');
        for (var colno = 0; colno < $columns.length; colno++) {
            var $column = $columns.eq(colno);
            var $field = $column.find('div.field');
            if (typeof $field.attr('data-isuniqueid') === 'undefined') {
                $field.attr('data-isuniqueid', 'false');
            }
            if (typeof $field.attr('data-sort') === 'undefined') {
                $field.attr('data-sort', 'off');
            }
            if (typeof $field.attr('data-formreadonly') === 'undefined') {
                $field.attr('data-formreadonly', 'false');
            }
            if (typeof $field.attr('data-datafield') !== 'undefined') {
                $field.attr('data-browsedatafield', $field.attr('data-datafield'));
                $field.attr('data-formdatafield', $field.attr('data-datafield'));
                $field.removeAttr('data-datafield');
            }
            if (typeof $field.attr('data-datatype') !== 'undefined') {
                $field.attr('data-browsedatatype', $field.attr('data-datatype'));
                $field.attr('data-formdatatype', $field.attr('data-datatype'));
                $field.removeAttr('data-datatype');
            }
            if (typeof $field.attr('data-cssclass') === 'undefined') {
                $field.attr('data-cssclass', $field.attr('data-browsedatafield'));
            }
        }
        if (typeof ($control.attr('data-mode') !== 'string')) {
            $control.attr('data-mode', 'VIEW');
        }
        if (typeof ($control.attr('data-pageno') !== 'string')) {
            $control.attr('data-pageno', '1');
        }
        if (typeof $control.attr('data-pagesize') !== 'string') {
            if ($control.attr('data-type') === 'Browse') {
                $control.attr('data-pagesize', sessionStorage.getItem('browsedefaultrows'));
            }
            else {
                $control.attr('data-pagesize', '15');
            }
        }
        if (typeof $control.attr('data-version') === 'undefined') {
            $control.attr('data-version', '1');
        }
        if (typeof $control.attr('data-rendermode') === 'undefined') {
            $control.attr('data-rendermode', 'template');
        }
        if ((($control.attr('data-type') === 'Browse') || ($control.attr('data-type') === 'Grid')) && (typeof $control.data('onrowdblclick') !== 'function') && ($control.attr('data-multiselectvalidation') !== 'true')) {
            var args;
            args = {};
            var nodeModule = FwApplicationTree.getNodeByController($control.attr('data-controller'));
            var nodeBrowse = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function (node, args2) {
                return (node.properties.nodetype === 'Browse');
            });
            var nodeView = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function (node, args2) {
                return (node.properties.nodetype === 'ViewMenuBarButton');
            });
            var nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function (node, args2) {
                return (node.properties.nodetype === 'EditMenuBarButton');
            });
            if ((nodeView !== null) && (nodeBrowse !== null)) {
                $control.data('onrowdblclick', function () {
                    try {
                        me.openSelectedRow($control);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        }
        $control
            .on('keydown', 'tbody tr', function (e) {
            var code;
            try {
                code = e.keyCode || e.which;
                switch (code) {
                    case 13:
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            e.preventDefault();
                            e.stopPropagation();
                            if (jQuery.inArray(e.target.offsetParent, $control.find('thead tr td .field div.search')) > -1) {
                                me.search($control);
                            }
                            else if (($control.attr('data-type') === 'Browse') && ($control.find('tbody tr.selected').length > 0)) {
                                me.openSelectedRow($control);
                            }
                            else if (($control.attr('data-type') === 'Validation') && ($control.find('tbody tr.selected').length > 0)) {
                                var $tr;
                                $tr = $control.find('tbody tr.selected');
                                $tr.dblclick();
                            }
                        }
                        break;
                    case 32:
                        if (($control.attr('data-type') === 'Validation') && ($control.attr('data-multiselectvalidation') === 'true')) {
                            e.preventDefault();
                            e.stopPropagation();
                            var $tr = $control.find('tbody tr:focus');
                            $tr.click();
                        }
                        break;
                    case 37:
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            me.prevPage($control);
                        }
                        return false;
                    case 38:
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            me.selectPrevRow($control);
                            return false;
                        }
                    case 39:
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            me.nextPage($control);
                            return false;
                        }
                    case 40:
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            me.selectNextRow($control);
                            return false;
                        }
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click focusin', 'tbody tr', function (e) {
            var $tr, $keyfields, compositekey, selectedrows;
            try {
                $tr = jQuery(this);
                if (($control.attr('data-type') === 'Validation') && ($control.attr('data-multiselectvalidation') === 'true')) {
                    if (e.type != 'focusin') {
                        if (!$tr.hasClass('selected')) {
                            if (typeof $control.data('selectedrows') === 'undefined') {
                                $control.data('selectedrows', {});
                            }
                            $keyfields = $tr.find('div.field[data-isuniqueid="true"]');
                            compositekey = [];
                            $keyfields.each(function (index, element) {
                                var $keyfield;
                                $keyfield = jQuery(element);
                                compositekey.push($keyfield.html());
                            });
                            compositekey = compositekey.join(',');
                            selectedrows = $control.data('selectedrows');
                            $tr.addClass('selected');
                            selectedrows[compositekey] = $tr;
                        }
                        else if ($tr.hasClass('selected')) {
                            $keyfields = $tr.find('div.field[data-isuniqueid="true"]');
                            compositekey = [];
                            $keyfields.each(function (index, element) {
                                var $keyfield;
                                $keyfield = jQuery(element);
                                compositekey.push($keyfield.html());
                            });
                            compositekey = compositekey.join(',');
                            selectedrows = $control.data('selectedrows');
                            if (typeof selectedrows[compositekey] !== 'undefined') {
                                $tr.removeClass('selected');
                                delete selectedrows[compositekey];
                            }
                        }
                    }
                }
                else if (!$tr.hasClass('empty')) {
                    var dontfocus = true;
                    if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                        me.selectRow($control, $tr, dontfocus);
                    }
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', 'table thead tr td .field div.search .searchclear', function (e) {
            try {
                e.stopPropagation();
                var $this = jQuery(this);
                $this.siblings('input').val('').change();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.designer thead > tr > td.addcolumn', function (e) {
            try {
                var $thAddColumn = jQuery(this);
                me.addDesignerColumn($control, $thAddColumn, 'auto', true);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.designer thead .columnhandle .delete', function (e) {
            var $delete, $th, $ths, $thead, thIndex, thAddColumnIndex, $trs, $tr, $tds;
            try {
                $delete = jQuery(this);
                $th = $delete.closest('td');
                $ths = $th.closest('thead').children('tr').eq(0).children('td');
                thIndex = $ths.index($th);
                $trs = $th.closest('table').find('> tbody > tr');
                $th.remove();
                thAddColumnIndex = thIndex - 1;
                $ths.eq(thAddColumnIndex).remove();
                $trs.each(function (index, tr) {
                    $tr = jQuery(tr);
                    $tds = $tr.children('td');
                    $tds.eq(thIndex).remove();
                    $tds.eq(thAddColumnIndex).remove();
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.designer thead .field > .deletefield', function () {
            var $this, $field;
            try {
                $this = jQuery(this);
                $field = $this.closest('.field');
                $field.remove();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.designer thead .addfield', function () {
            var $this, $th;
            try {
                $this = jQuery(this);
                $th = $this.closest('td');
                me.addDesignerField($th, 'newfield', 'newfield', 'newfield', 'string');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('dblclick', '.designer thead .field .caption', function () {
            var $caption, caption, newCaption, $field;
            try {
                $caption = jQuery(this);
                caption = $caption.html();
                newCaption = prompt('Rename field?', caption);
                if (newCaption != null) {
                    $caption.html(newCaption);
                    $field = $caption.closest('.field');
                    $field.attr('data-caption', newCaption);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('change', '.runtime thead .search > input', function (e) {
            try {
                if (($control.attr('data-type') != 'Validation') || (jQuery('html').hasClass('mobile'))) {
                    var $this = jQuery(this);
                    if ($this.val() === '') {
                        $this.siblings('.searchclear').removeClass('visible');
                    }
                    else if ($this.val() !== '') {
                        $this.siblings('.searchclear').addClass('visible');
                    }
                    me.search($control);
                    $this.focus();
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('change', '.runtime .pager select.pagesize', function () {
            var $this, pagesize;
            try {
                $this = jQuery(this);
                pagesize = $this.val();
                me.setPageSize($control, pagesize);
                me.search($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.runtime .pager div.btnRefresh', function (e) {
            try {
                e.stopPropagation();
                me.databind($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control
            .on('click', '.runtime .pager div.buttons .btnFirstPage', function (e) {
            try {
                e.stopPropagation();
                var $btnFirstPage = jQuery(this);
                if ($btnFirstPage.attr('data-enabled') === 'true') {
                    $control.attr('data-pageno', '1');
                    me.databind($control);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control
            .on('click', '.runtime .pager div.buttons .btnPreviousPage', function (e) {
            try {
                e.stopPropagation();
                me.prevPage($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control
            .on('change', '.runtime .pager div.buttons .txtPageNo', function () {
            var pageno, originalpageno, originalpagenoStr, $txtPageNo, totalPages;
            try {
                $txtPageNo = jQuery(this);
                originalpagenoStr = $txtPageNo.val();
                if (!isNaN(originalpagenoStr)) {
                    pageno = parseInt(originalpagenoStr);
                    originalpageno = pageno;
                    totalPages = parseInt($control.find('.runtime .pager div.buttons .txtTotalPages').html());
                    pageno = (pageno >= 1) ? pageno : 1;
                    pageno = (pageno <= totalPages) ? pageno : totalPages;
                    if (pageno === originalpageno) {
                        me.setPageNo($control, pageno);
                        me.databind($control);
                    }
                    else {
                        $control.find('.runtime .pager div.buttons .txtTotalPages').val(pageno);
                    }
                }
                else {
                }
            }
            catch (ex) {
                $control.find('.runtime .pager div.buttons .txtTotalPages').val(originalpagenoStr);
                FwFunc.showError(ex);
            }
        });
        $control
            .on('click', '.runtime .pager div.buttons .btnNextPage', function (e) {
            try {
                e.stopPropagation();
                me.nextPage($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control
            .on('click', '.runtime .pager div.buttons .btnLastPage', function (e) {
            try {
                e.stopPropagation();
                var $btnLastPage = jQuery(this);
                if ($btnLastPage.attr('data-enabled') === 'true') {
                    var pageno = me.getTotalPages($control);
                    me.setPageNo($control, pageno);
                    me.databind($control);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control
            .on('change', '.runtime .pager select.activeinactiveview', function () {
            var $selectActiveInactiveView, view;
            try {
                $selectActiveInactiveView = jQuery(this);
                view = $selectActiveInactiveView.val();
                $control.attr('data-activeinactiveview', view);
                me.search($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        var controller = window[$control.attr('data-controller')];
        if (($control.attr('data-type') == 'Grid') && (typeof controller.apiurl === 'undefined')) {
            $control.on('change', '.field[data-formnoduplicate="true"]', function () {
                var $field, value, originalvalue, $form, formuniqueids, formfields, request = {};
                $field = jQuery(this);
                $field.removeClass('error');
                $form = $control.closest('.fwform');
                formuniqueids = FwModule.getFormUniqueIds($form);
                formfields = FwModule.getFormFields($form, true);
                value = $field.find('input.value').val().toUpperCase();
                originalvalue = ((typeof $field.attr('data-originalvalue') != 'undefined') ? $field.attr('data-originalvalue').toUpperCase() : '');
                if ((typeof $control.attr('data-name') !== 'undefined') && (value != originalvalue)) {
                    request.module = $control.attr('data-name');
                    request.table = $field.attr('data-formdatafield').split('.')[0];
                    request.fields = {};
                    request.fields[$field.attr('data-formdatafield')] = { datafield: $field.attr('data-formdatafield'), value: $field.find('input.value').val(), type: $field.attr('data-formdatatype') };
                    request.miscfields = jQuery.extend({}, formuniqueids, formfields);
                    FwServices.grid.method(request, $control.attr('data-name'), 'ValidateDuplicate', $control, function (response) {
                        try {
                            if (response.duplicate == true) {
                                $field.addClass('error');
                                FwNotification.renderNotification('ERROR', 'Duplicate ' + $field.attr('data-caption') + '(s) are not allowed.');
                            }
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }, function (errorMessage) {
                        try {
                            FwFunc.showError(errorMessage);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            });
        }
        if ((($control.attr('data-type') == 'Grid') || ($control.attr('data-type') == 'Validation')) && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['init'] === 'function') {
                window[controller]['init']($control);
            }
        }
    };
    FwBrowseClass.prototype.getPageNo = function ($control) {
        return parseInt($control.attr('data-pageno'));
    };
    FwBrowseClass.prototype.setPageNo = function ($control, pageno) {
        $control.attr('data-pageno', pageno);
    };
    FwBrowseClass.prototype.getPageSize = function ($control) {
        return parseInt($control.attr('data-pagesize'));
    };
    FwBrowseClass.prototype.setPageSize = function ($control, pagesize) {
        $control.attr('data-pagesize', pagesize);
    };
    FwBrowseClass.prototype.getTotalPages = function ($control) {
        return parseInt($control.attr('data-totalpages'));
    };
    FwBrowseClass.prototype.setTotalPages = function ($control, totalpages) {
        $control.attr('data-totalpages', totalpages);
    };
    FwBrowseClass.prototype.getSelectedIndex = function ($control) {
        return parseInt($control.attr('data-selectedindex'));
    };
    FwBrowseClass.prototype.setSelectedIndex = function ($control, selectedindex) {
        $control.attr('data-selectedindex', selectedindex);
    };
    FwBrowseClass.prototype.getSelectedRowMode = function ($control) {
        var selectedRowMode = typeof $control.data('selectedrowmode') === 'string' ? $control.data('selectedrowmode') : '';
        return selectedRowMode;
    };
    FwBrowseClass.prototype.setSelectedRowMode = function ($control, mode) {
        $control.data('selectedrowmode', mode);
    };
    FwBrowseClass.prototype.isRowModified = function ($control, $tr) {
        if (!$tr.hasClass('editrow')) {
            return false;
        }
        var $fields = $tr.find('.field[data-formdatafield][data-formreadonly!="true"]');
        var isRowUnmodified = true;
        for (var i = 0; i < $fields.length; i++) {
            var $field = $fields.eq(i);
            var field = { datafield: $field.attr('data-browsedatafield'), value: '' };
            if (typeof window['FwBrowseColumn_' + $field.attr('data-formdatatype')] !== 'undefined') {
                var isModifiedFunction = window['FwBrowseColumn_' + $field.attr('data-formdatatype')].isModified;
                var isFieldUnmodified = !isModifiedFunction($control, $tr, $field, field, $field.attr('data-originalvalue'));
                isRowUnmodified = isRowUnmodified && isFieldUnmodified;
                if (isRowUnmodified === false) {
                    break;
                }
            }
        }
        return !isRowUnmodified;
    };
    FwBrowseClass.prototype.getSelectedRow = function ($control) {
        return $control.find('tbody tr.selected');
    };
    FwBrowseClass.prototype.getSelectedRowIndex = function ($control) {
        return this.getSelectedRow($control).index();
    };
    FwBrowseClass.prototype.getRows = function ($control) {
        return $control.find('tbody tr');
    };
    FwBrowseClass.prototype.unselectAllRows = function ($control) {
        $control.find('tbody tr.selected').removeClass('selected');
        this.setSelectedIndex($control, -1);
    };
    FwBrowseClass.prototype.unselectRow = function ($control, $tr) {
        $tr.removeClass('selected');
        if (this.getSelectedIndex($control) === $tr.index()) {
            this.setSelectedIndex($control, -1);
        }
    };
    FwBrowseClass.prototype.selectRow = function ($control, $row, dontfocus) {
        var $prevselectedrow = this.getSelectedRow($control);
        $prevselectedrow.removeClass('selected');
        $row.addClass('selected');
        this.setSelectedIndex($control, $row.index());
        if (dontfocus !== true) {
            $row.focus();
        }
    };
    FwBrowseClass.prototype.selectRowByIndex = function ($control, index) {
        var $rows = this.getRows($control);
        var $row = $rows.eq(index);
        this.selectRow($control, $row);
        return $row;
    };
    FwBrowseClass.prototype.getRowCount = function ($control) {
        var $rows = this.getRows($control);
        var rowcount = $rows.length;
        return rowcount;
    };
    FwBrowseClass.prototype.getTotalRowCount = function ($control) {
        var totalRowCount = $control.data('totalRowCount');
        return totalRowCount;
    };
    FwBrowseClass.prototype.selectPrevRow = function ($control, afterrowselected) {
        var $selectedrow = this.getSelectedRow($control);
        var pageno = this.getPageNo($control);
        var pagesize = this.getPageSize($control);
        var rowindex = this.getSelectedRowIndex($control);
        if (rowindex > 0) {
            rowindex = rowindex - 1;
            $selectedrow = this.selectRowByIndex($control, rowindex);
            if (typeof afterrowselected === 'function') {
                afterrowselected();
            }
        }
        else if ((rowindex === 0) && (pageno > 1)) {
            rowindex = pagesize - 1;
            this.setSelectedIndex($control, rowindex);
            this.addEventHandler($control, 'afterdatabindcallback', function afterdatabindcallback_selectPrevRow() {
                this.removeEventHandler($control, 'afterdatabindcallback', afterdatabindcallback_selectPrevRow);
                if (typeof afterrowselected === 'function') {
                    afterrowselected();
                }
            });
            this.prevPage($control);
        }
        return $selectedrow;
    };
    FwBrowseClass.prototype.selectNextRow = function ($control, afterrowselected) {
        var $selectedrow = this.getSelectedRow($control);
        var pageno = this.getPageNo($control);
        var totalpages = this.getTotalPages($control);
        var rowindex = this.getSelectedRowIndex($control);
        var lastrowindex = $control.find('tbody tr').length - 1;
        if (rowindex < lastrowindex) {
            $selectedrow = $selectedrow.next();
            this.selectRow($control, $selectedrow);
            if (typeof afterrowselected === 'function') {
                afterrowselected();
            }
        }
        else if ((rowindex === lastrowindex) && (pageno < totalpages)) {
            this.setSelectedIndex($control, 0);
            this.addEventHandler($control, 'afterdatabindcallback', function afterdatabindcallback_selectNextRow() {
                this.removeEventHandler($control, 'afterdatabindcallback', afterdatabindcallback_selectNextRow);
                if (typeof afterrowselected === 'function') {
                    afterrowselected();
                }
            });
            this.nextPage($control);
        }
        var $trselected = $control.find('tbody tr.selected');
        return $trselected;
    };
    FwBrowseClass.prototype.openPrevRow = function ($control, $tab, $form) {
        if ((typeof $tab !== 'undefined') && ($tab.length === 1) && (typeof $form !== 'undefined') && ($form.length === 1)) {
            FwModule.closeForm($form, $tab, null, function afterCloseForm() {
                this.selectPrevRow($control, function afterrowselected() {
                    this.openSelectedRow($control);
                });
            });
        }
    };
    FwBrowseClass.prototype.openNextRow = function ($control, $tab, $form) {
        if ((typeof $tab !== 'undefined') && ($tab.length === 1) && (typeof $form !== 'undefined') && ($form.length === 1)) {
            FwModule.closeForm($form, $tab, null, function afterCloseForm() {
                this.selectNextRow($control, function afterrowselected() {
                    this.openSelectedRow($control);
                });
            });
        }
    };
    FwBrowseClass.prototype.prevPage = function ($control) {
        var pageno, $btnPreviousPage;
        $btnPreviousPage = $control.find('.btnPreviousPage');
        if ($btnPreviousPage.attr('data-enabled') === 'true') {
            pageno = this.getPageNo($control) - 1;
            pageno = (pageno >= 1) ? pageno : 1;
            this.setPageNo($control, pageno);
            this.databind($control);
        }
    };
    FwBrowseClass.prototype.nextPage = function ($control) {
        var $btnNextPage, pageno, totalpages;
        $btnNextPage = $control.find('.btnNextPage');
        if ($btnNextPage.attr('data-enabled') === 'true') {
            pageno = this.getPageNo($control) + 1;
            totalpages = this.getTotalPages($control);
            pageno = (pageno <= totalpages) ? pageno : totalpages;
            this.setPageNo($control, pageno);
            this.databind($control);
        }
    };
    FwBrowseClass.prototype.openSelectedRow = function ($control) {
        var $selectedrow, browseuniqueids, formuniqueids, $fwforms, dataType, $form, issubmodule, nodeModule, nodeBrowse, nodeView, nodeEdit;
        $selectedrow = this.getSelectedRow($control);
        dataType = (typeof $control.attr('data-type') === 'string') ? $control.attr('data-type') : '';
        switch (dataType) {
            case 'Browse':
                formuniqueids = this.getRowFormUniqueIds($control, $selectedrow);
                browseuniqueids = this.getRowBrowseUniqueIds($control, $selectedrow);
                $form = FwModule.getFormByUniqueIds(formuniqueids);
                if ((typeof $form === 'undefined') || ($form.length == 0)) {
                    if (typeof $control.attr('data-controller') === 'undefined') {
                        throw 'this: Missing attribute data-controller.  Set this attribute on the browse control to the name of the controller for this browse module.';
                    }
                    $form = window[$control.attr('data-controller')].loadForm(browseuniqueids);
                    issubmodule = $control.closest('.tabpage').hasClass('submodule');
                    if (!issubmodule) {
                        FwModule.openModuleTab($form, 'New ' + $form.attr('data-caption'), true, 'FORM', true);
                    }
                    else {
                        FwModule.openSubModuleTab($control, $form);
                    }
                }
                else if ($form.length > 0) {
                    var tabid = $form.closest('div.tabpage').attr('data-tabid');
                    jQuery('#' + tabid).click();
                }
                break;
            case 'Grid':
                break;
        }
    };
    FwBrowseClass.prototype.addEventHandler = function ($control, eventName, callbackfunction) {
        var callbackfunctions = [];
        if (Array.isArray($control.data(eventName))) {
            callbackfunctions = $control.data(eventName);
        }
        callbackfunctions.push(callbackfunction);
        $control.data(eventName, callbackfunctions);
    };
    FwBrowseClass.prototype.removeEventHandler = function ($control, eventName, callbackfunction) {
        if (Array.isArray($control.data(eventName))) {
            var callbackfunctions = $control.data(eventName);
            for (var i = 0; i < callbackfunctions.length; i++) {
                if (callbackfunctions[i] === callbackfunction) {
                    callbackfunctions.splice(i, 1);
                }
            }
        }
    };
    FwBrowseClass.prototype.getSortImage = function (sort) {
        var result;
        switch (sort) {
            case 'asc':
                result = 'theme/fwimages/icons/9x5/SortDesc.001.png';
                break;
            case 'desc':
                result = 'theme/fwimages/icons/9x5/SortOff.001.png';
                break;
            case 'off':
                result = 'theme/fwimages/icons/9x5/SortAsc.001.png';
            default:
                throw 'Invalid sort: ' + sort;
        }
    };
    FwBrowseClass.prototype.search = function ($control) {
        this.setPageNo($control, 1);
        return this.databind($control);
    };
    FwBrowseClass.prototype.addDesignerField = function ($column, cssclass, caption, datafield, datatype) {
        var html, $field, $addfield;
        html = [];
        html.push('<div draggable="true"');
        html.push(' class="field ' + cssclass + '"');
        html.push(' data-caption="' + caption + '"');
        html.push(' data-cssclass="' + cssclass + '"');
        html.push(' data-browsedatafield="' + datafield + '"');
        html.push(' data-browsedatatype="' + datatype + '"');
        html.push('>');
        html.push('<span class="caption">' + caption + '</span>');
        html.push('<div class="deletefield">x</div>');
        html.push('</div>');
        html = html.join('');
        $field = jQuery(html);
        $addfield = $column.find('> .addfield');
        if ($addfield.length > 0) {
            $field.insertBefore($addfield);
        }
        else {
            $column.append($addfield);
        }
    };
    FwBrowseClass.prototype.addDesignerColumn = function ($control, $thAddColumn, width, visible) {
        var htmlTh, htmlTd, $trHead, $trBody, $th, $td, $tdAddColumn, thAddColumnIndex, $tds, $tdAddColumn;
        htmlTh = [];
        htmlTh.push('<td class="column" data-width="' + width + '" data-visible="' + visible + '" style="width:' + width + ';">');
        htmlTh.push('<div class="columnhandle">');
        htmlTh.push('<span class="caption">column</span>');
        htmlTh.push('<div class="delete">x</div>');
        htmlTh.push('</div>');
        htmlTh.push('<div class="addfield"></div>');
        htmlTh.push('</td>');
        htmlTh.push('<td class="addcolumn">');
        htmlTh.push('</td>');
        htmlTh = htmlTh.join('');
        htmlTd = [];
        htmlTd.push('<td class="column"></td>');
        htmlTd.push('<td class="addcolumn"></td>');
        htmlTd = htmlTd.join('');
        $trHead = $control.find('.designer thead > tr');
        $th = jQuery(htmlTh);
        $th.insertAfter($thAddColumn);
        thAddColumnIndex = $thAddColumn.parent().children().index($thAddColumn);
        $thAddColumn.closest('table').find('> tbody > tr').each(function (index, field) {
            $trBody = jQuery(field);
            $tdAddColumn = $trBody.children('td')[thAddColumnIndex];
            $td = jQuery(htmlTd);
            $td.insertAfter($tdAddColumn);
        });
        this.addDesignerField($control, 'newfield', 'newfield', '', 'string');
    };
    FwBrowseClass.prototype.getHtmlTag = function (data_type) {
        var template, html, properties, i;
        template = [];
        template.push('<div');
        properties = this.getDesignerProperties(data_type);
        for (i = 0; i < properties.length; i++) {
            template.push(' ' + properties[i].attribute + '="' + properties[i].defaultvalue + '"');
        }
        template.push('></div>');
        html = template.join('');
        return html;
    };
    FwBrowseClass.prototype.getDesignerProperties = function (data_type) {
        var propId = { caption: 'ID', datatype: 'string', attribute: 'id', defaultvalue: FwControl.generateControlId('fwbrowse'), visible: true, enabled: true };
        var propClass = { caption: 'CSS Class', datatype: 'string', attribute: 'class', defaultvalue: 'fwcontrol fwbrowse', visible: false, enabled: false };
        var propCaption = { caption: 'Caption', datatype: 'string', attribute: 'data-caption', defaultvalue: '', visible: false, enabled: false };
        var propDataControl = { caption: 'Control', datatype: 'string', attribute: 'data-control', defaultvalue: 'FwBrowse', visible: true, enabled: false };
        var propDataType = { caption: 'Type', datatype: 'Browse', attribute: 'data-type', defaultvalue: data_type, visible: true, enabled: false };
        var propDataVersion = { caption: 'Version', datatype: 'string', attribute: 'data-version', defaultvalue: '1', visible: false, enabled: false };
        var propRenderMode = { caption: 'Render Mode', datatype: 'string', attribute: 'data-rendermode', defaultvalue: 'template', visible: false, enabled: false };
        var propOrderBy = { caption: '', datatype: 'string', attribute: 'data-orderby', defaultvalue: '', visible: false, enabled: false };
        var propHasAdd = { caption: '', datatype: 'boolean', attribute: 'data-hasadd', defaultvalue: 'true', visible: false, enabled: false };
        var propHasEdit = { caption: '', datatype: 'boolean', attribute: 'data-hasedit', defaultvalue: 'true', visible: false, enabled: false };
        var propHasDelete = { caption: '', datatype: 'boolean', attribute: 'data-hasdelete', defaultvalue: 'true', visible: false, enabled: false };
        var properties = [propDataControl, propDataType, propId, propClass, propCaption, propDataVersion, propRenderMode, propOrderBy];
        switch (data_type) {
            case "Grid":
                properties.push(propHasAdd);
                properties.push(propHasEdit);
                properties.push(propHasDelete);
        }
        return properties;
    };
    FwBrowseClass.prototype.renderDesignerHtml = function ($control) {
        var html, data_rendermode, $columns;
        data_rendermode = $control.attr('data-rendermode');
        switch (data_rendermode) {
            case 'designer':
            case 'runtime':
                this.renderTemplateHtml($control);
                this.renderDesignerHtml($control);
                break;
            case 'template':
                html = [];
                html.push('<div class="designer">');
                html.push(FwControl.generateDesignerHandle('scroller', $control.attr('id')));
                html.push('<table>');
                html.push('<thead>');
                html.push('<tr class="fieldnames">');
                html.push('<td class="addcolumn"></td>');
                $columns = $control.find('> .column');
                $columns.each(function (index, column) {
                    var $column, caption, browsedatafield, cssclass, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    $column = jQuery(column);
                    width = $column.attr('data-width');
                    visible = $column.attr('data-visible');
                    html.push('<td class="column" data-width="' + width + '" data-visible="' + visible + '" style="width:' + width + ';">');
                    html.push('<div class="columnhandle" draggable="true">');
                    html.push('<span class="caption">column</span>');
                    html.push('<div class="delete">x</div>');
                    html.push('</div>');
                    $fields = $column.find('> .field');
                    $fields.each(function (index, field) {
                        var $field;
                        $field = jQuery(field);
                        caption = $field.attr('data-caption');
                        browsedatafield = $field.attr('data-browsedatafield');
                        cssclass = $field.attr('data-cssclass');
                        browsedatatype = $field.attr('data-browsedatatype');
                        formdatafield = $field.attr('data-formdatafield');
                        formdatatype = $field.attr('data-datatype');
                        html.push('<div draggable="true" class="field ' + cssclass + '" data-caption="' + caption + '" data-browsedatafield="' + browsedatafield + '" data-cssclass="' + cssclass + '" data-browsedatatype="' + browsedatatype + '" data-formdatafield="' + formdatafield + '" data-formdatatype="' + formdatatype + '">');
                        html.push('<span class="caption">' + caption + '</span>');
                        html.push('<div class="deletefield">x</div>');
                        html.push('</div>');
                    });
                    html.push('<div class="addfield"></div>');
                    html.push('</td>');
                    html.push('<td class="addcolumn"></td>');
                });
                html.push('</tr>');
                html.push('</thead>');
                html.push('<tbody>');
                for (var rowno = 1; rowno <= 10; rowno++) {
                    html.push('<tr>');
                    html.push('<td class="addcolumn"></td>');
                    for (var colno = 0; colno < $columns.length; colno++) {
                        var $column = $columns.eq(colno);
                        var width = $column.attr('data-width');
                        var visible = $column.attr('data-visible');
                        html.push('<td class="column">');
                        var $fields = $column.find('> .field');
                        for (var fieldno = 0; fieldno < $fields.length; fieldno++) {
                            var $field = $fields.eq(fieldno);
                            var cssclass = $field.attr('data-cssclass');
                            var browsedatafield = $field.attr('data-browsedatafield');
                            html.push('<div class="field ' + cssclass + '">');
                            html.push(browsedatafield + rowno.toString());
                            html.push('</div>');
                        }
                        $fields.each(function (index, field) {
                        });
                        html.push('</td>');
                        html.push('<td class="addcolumn"></td>');
                    }
                    html.push('</tr>');
                }
                html.push('</tbody>');
                html.push('</table>');
                html.push('</div>');
                html = html.join('');
                $control.html(html);
                $control.attr('data-rendermode', 'designer');
                break;
        }
    };
    FwBrowseClass.prototype.renderRuntimeHtml = function ($control) {
        var me = this;
        var html, data_rendermode, $allfields, data_uniqueidname, colspan, $advancedoptions, $customvalidationbuttons, $columns, $columnoptions, $theadfields;
        data_rendermode = $control.attr('data-rendermode');
        switch (data_rendermode) {
            case 'designer':
            case 'runtime':
                this.renderTemplateHtml($control);
                this.renderRuntimeHtml($control);
                break;
            case 'template':
                $columns = $control.find('> .column');
                html = [];
                html.push('<div class="runtime">');
                if ($control.attr('data-type') === 'Browse') {
                    html.push('<div class="fwbrowse-menu"></div>');
                }
                if ($control.attr('data-type') === 'Grid') {
                    html.push('<div class="gridmenu"></div>');
                }
                else if ($control.attr('data-type') === 'Validation') {
                    html.push('<div class="browsecaption">Select ' + $control.attr('data-caption') + '...</div>');
                }
                $advancedoptions = $control.find('.advancedoptions');
                if ($advancedoptions.length > 0) {
                    FwControl.renderRuntimeControls($advancedoptions.find('.fwcontrol'));
                    html.push($advancedoptions.wrap('<div/>').parent().html());
                }
                html.push('<div class="fwbrowsefilter" style="display:none;"></div>');
                html.push('<div class="tablewrapper">');
                html.push('<table>');
                html.push('<thead>');
                html.push('<tr class="fieldnames">');
                if (($control.attr('data-type') === 'Grid') || (($control.attr('data-type') === 'Browse') && ($control.attr('data-hasmultirowselect') === 'true'))) {
                    var cbuniqueId = FwApplication.prototype.uniqueId(10);
                    html.push('<td class="column tdselectrow" style="width:20px;"><div class="divselectrow"><input id="' + cbuniqueId + '" type="checkbox" class="cbselectrow"/><label for="' + cbuniqueId + '" class="lblselectrow"></label></div></td>');
                }
                for (var colno = 0; colno < $columns.length; colno++) {
                    var $column = $columns.eq(colno);
                    var width = $column.attr('data-width');
                    var visible = (typeof $column.attr('data-visible') !== 'undefined') ? ($column.attr('data-visible') === 'true') : true;
                    html.push('<td class="column" data-visible="' + visible + '" style="');
                    if (!visible) {
                        html.push('display:none;');
                    }
                    html.push('">');
                    var $theadfields = $column.find('> .field');
                    for (var fieldno = 0; fieldno < $theadfields.length; fieldno++) {
                        var $theadfield = $theadfields.eq(fieldno);
                        var $field = $theadfield.clone();
                        $field.empty();
                        var sort = $theadfield.attr('data-sort');
                        var caption = $theadfield.attr('data-caption');
                        html.push($field[0].outerHTML.replace('</div>', ''));
                        html.push('<div class="fieldcaption" style="min-width:' + width + ';">');
                        html.push('<div class="caption" title="' + caption + '">' + caption + '</div>');
                        if (sort === 'asc') {
                            html.push('<div class="sort"><i class="material-icons">keyboard_arrow_up</i></div>');
                        }
                        else if (sort === 'desc') {
                            html.push('<div class="sort"><i class="material-icons">keyboard_arrow_down</i></div>');
                        }
                        else if (sort === 'off') {
                            html.push('<div class="sort"><i class="material-icons"></i></div>');
                        }
                        html.push('<div class="columnoptions"></div>');
                        html.push('</div>');
                        html.push('<div class="search"');
                        if ($control.attr('data-showsearch') === 'false') {
                            html.push(' style="display:none;"');
                        }
                        else if ($theadfield.attr('data-showsearch') === 'false') {
                            html.push(' style="visibility:hidden;"');
                        }
                        html.push('>');
                        if ($theadfield.attr('data-browsedatatype') === 'date') {
                            html.push('<input class="value" type="text"/>');
                            html.push('<i class="material-icons btndate" style="position:absolute; right:0px; top:5px;">&#xE8DF;</i>');
                            html.push('<span class="searchclear" title="clear" style="right:20px;"><i class="material-icons">clear</i></span>');
                        }
                        else {
                            html.push('<input type="text" />');
                            html.push('<span class="searchclear" title="clear"><i class="material-icons">clear</i></span>');
                        }
                        html.push('</div>');
                        html.push('</div>');
                    }
                    ;
                    html.push('</td>');
                }
                if ($control.attr('data-type') === 'Grid') {
                    html.push('<td class="column browsecontextmenucell" style="width:26px;"></td>');
                }
                html.push('</tr>');
                html.push('</thead>');
                html.push('<tbody>');
                html.push('<tr class="empty">');
                for (var colno = 0; colno < $columns.length; colno++) {
                    var $column = $columns.eq(colno);
                    var visible = (typeof $column.attr('data-visible') !== 'undefined') ? ($column.attr('data-visible') === 'true') : true;
                    html.push('<td class="column"');
                    if (!visible) {
                        html.push(' style="display:none;"');
                    }
                    html.push('></td>');
                }
                html.push('</tr>');
                html.push('</tbody>');
                html.push('<tfoot>');
                colspan = $columns.filter('*[data-visible="true"]').length;
                html.push('<tr class="spacerrow">');
                html.push('<td colspan="' + (colspan + 2) + '">');
                html.push('<div>&nbsp;</div>');
                html.push('</td>');
                html.push('</tr>');
                html.push('</tfoot>');
                html.push('</table>');
                html.push('</div>');
                html.push('<div class="legend" style="display:none;"></div>');
                html.push('<div class="pager"></div>');
                if ($control.attr('data-type') === 'Validation') {
                    html.push('<div class="validationbuttons">');
                    html.push('<div class="fwbrowsebutton btnSelect">Select</div>');
                    html.push('<div class="fwbrowsebutton btnSelectAll" title="The report will run faster if you Select All from this button vs selecting individual rows." style="display:none;">Select All</div>');
                    html.push('<div class="fwbrowsebutton btnClear">Clear</div>');
                    html.push('<div class="fwbrowsebutton btnViewSelection" style="display:none;">View Selection</div>');
                    html.push('<div class="fwbrowsebutton btnCancel">Cancel</div>');
                    html.push('<div class="fwbrowsebutton btnNew">New</div>');
                    $customvalidationbuttons = $control.find('.customvalidationbuttons');
                    if ($customvalidationbuttons.length > 0) {
                        FwControl.renderRuntimeControls($customvalidationbuttons.find('.fwcontrol'));
                        html.push($customvalidationbuttons.html());
                    }
                    html.push('</div>');
                }
                html.push('</div>');
                html = html.join('');
                $control.html(html);
                $control.attr('data-rendermode', 'runtime');
                var controlType = $control.attr('data-type');
                var htmlPager = [];
                switch (controlType) {
                    case 'Browse':
                        htmlPager.push('<div class="col1" style="width:33%;overflow:hidden;float:left;">');
                        htmlPager.push('  <div class="btnRefresh" title="Refresh" tabindex="0"><i class="material-icons">&#xE5D5;</i></div>');
                        htmlPager.push('  <div class="count"></div>');
                        htmlPager.push('</div>');
                        htmlPager.push('<div class="col2" style="width:34%;overflow:hidden;float:left;height:32px;text-align:center;">');
                        htmlPager.push('  <div class="buttons">');
                        htmlPager.push('    <div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                        htmlPager.push('    <div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                        htmlPager.push('    <div class="page">');
                        htmlPager.push('      <input class="txtPageNo" type="text" value="0" />');
                        htmlPager.push('      <div class="of">of</div>');
                        htmlPager.push('      <div class="txtTotalPages">0 row(s)</div>');
                        htmlPager.push('    </div>');
                        htmlPager.push('    <div class="button btnNextPage" disabled="disabled" data-enabled="false" title="Next" alt="Next"><i class="material-icons">&#xE5CC;</i></div>');
                        htmlPager.push('    <div class="button btnLastPage" disabled="disabled" data-enabled="false" title="Last" alt="Last"><i class="material-icons">&#xE5DD;</i></div>');
                        htmlPager.push('  </div>');
                        htmlPager.push('</div>');
                        htmlPager.push('<div class="col3" style="width:33%;overflow:hidden;float:left;">');
                        htmlPager.push('  <div class="pagesize">');
                        htmlPager.push('    <select class="pagesize">');
                        htmlPager.push('      <option value="5">5</option>');
                        htmlPager.push('      <option value="10">10</option>');
                        htmlPager.push('      <option value="15">15</option>');
                        htmlPager.push('      <option value="20">20</option>');
                        htmlPager.push('      <option value="25">25</option>');
                        htmlPager.push('      <option value="30">30</option>');
                        htmlPager.push('      <option value="35">35</option>');
                        htmlPager.push('      <option value="40">40</option>');
                        htmlPager.push('      <option value="45">45</option>');
                        htmlPager.push('      <option value="50">50</option>');
                        htmlPager.push('      <option value="100">100</option>');
                        htmlPager.push('      <option value="200">200</option>');
                        htmlPager.push('      <option value="500">500</option>');
                        htmlPager.push('      <option value="1000">1000</option>');
                        htmlPager.push('    </select>');
                        htmlPager.push('    <span class="caption">rows per page</span>');
                        htmlPager.push('  </div>');
                        htmlPager.push('</div>');
                        break;
                    case 'Grid':
                        htmlPager.push('<div class="btnRefresh" title="Refresh" tabindex="0"><i class="material-icons">&#xE5D5;</i></div>');
                    case 'Validation':
                        htmlPager.push('<div class="buttons" style="float:left;">');
                        htmlPager.push('  <div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                        htmlPager.push('  <div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                        htmlPager.push('  <input class="txtPageNo" style="display:none;" type="text" value="0"/>');
                        htmlPager.push('  <span class="of" style="display:none;"> of </span>');
                        htmlPager.push('  <span class="txtTotalPages" style="display:none;">0</span>');
                        htmlPager.push('  <div class="button btnNextPage" disabled="disabled" data-enabled="false" title="Next" alt="Next"><i class="material-icons">&#xE5CC;</i></div>');
                        htmlPager.push('  <div class="button btnLastPage" disabled="disabled" data-enabled="false" title="Last" alt="Last"><i class="material-icons">&#xE5DD;</i></div>');
                        htmlPager.push('</div>');
                        htmlPager.push('<div class="count">0 row(s)</div>');
                        if ((controlType === 'Grid') && (typeof $control.attr('data-activeinactiveview') === 'string') && (FwSecurity.isUser())) {
                            htmlPager.push('<div class="activeinactiveview" style="float:right;">');
                            htmlPager.push('  <select class="activeinactiveview">');
                            htmlPager.push('    <option value="active">Show Active</option>');
                            htmlPager.push('    <option value="inactive">Show Inactive</option>');
                            htmlPager.push('    <option value="all">Show All</option>');
                            htmlPager.push('</div>');
                        }
                        break;
                }
                var htmlPagerStr = htmlPager.join('');
                var $pager = $control.find('.runtime .pager');
                $pager.html(htmlPagerStr);
                $pager.find('select.pagesize').val($control.attr('data-pagesize'));
                $pager.find('select.activeinactiveview').val($control.attr('data-activeinactiveview'));
                $pager.show();
                $control.find('.value').datepicker({
                    endDate: (($control.attr('data-nofuture') == 'true') ? '+0d' : Infinity),
                    autoclose: true,
                    format: "mm/dd/yyyy",
                    todayHighlight: true,
                    todayBtn: 'linked'
                }).off('focus');
                $control.on('click', '.btndate', function (e) {
                    jQuery(e.currentTarget).siblings('.value').datepicker('show');
                });
                $control.on('click', 'thead .cbselectrow', function () {
                    try {
                        var $this = jQuery(this);
                        $control.find('tbody .cbselectrow').prop('checked', $this.prop('checked'));
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                var getcolumnoptionbutton = function (text, materialicon) {
                    var html;
                    html = [];
                    html.push('<div class="columnoptions-button">');
                    html.push('<div class="columnoptions-button-icon">' + ((materialicon != '') ? '<i class="material-icons">' + materialicon + '</i>' : '') + '</div>');
                    html.push('<div class="columnoptions-button-text">' + text + '</div>');
                    html.push('</div>');
                    return jQuery(html.join(''));
                };
                $theadfields = $control.find('thead .field');
                $theadfields.each(function (index, element) {
                    var $theadfield, $columnoptions, showsort, showsearch;
                    $theadfield = jQuery(element);
                    $columnoptions = $theadfield.find('.columnoptions');
                    showsort = ($theadfield.attr('data-sort') !== 'disabled');
                    showsearch = ($theadfield.attr('data-showsearch') !== 'false');
                    if (showsort) {
                        var $sortascbtn, $sortdescbtn, $sortoffbtn;
                        $sortascbtn = getcolumnoptionbutton('Sort Ascending', '&#xE316;');
                        $sortascbtn.on('click', function () {
                            try {
                                var $this = jQuery(this);
                                var $field = $this.closest('.field');
                                $field.attr('data-sort', 'asc');
                                $field.find('.sort .material-icons').html('&#xE316;');
                                me.databind($control);
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $columnoptions.append($sortascbtn);
                        $sortdescbtn = getcolumnoptionbutton('Sort Descending', '&#xE313;');
                        $sortdescbtn.on('click', function () {
                            try {
                                var $this = jQuery(this);
                                var $field = $this.closest('.field');
                                $field.attr('data-sort', 'desc');
                                $field.find('.sort .material-icons').html('&#xE313;');
                                me.databind($control);
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $columnoptions.append($sortdescbtn);
                        $sortoffbtn = getcolumnoptionbutton('Sort Off', '');
                        $sortoffbtn.on('click', function () {
                            try {
                                var $this = jQuery(this);
                                var $field = $this.closest('.field');
                                var $thead = $field.closest('thead');
                                var $sortedfields = $thead.find('.field[data-sort="asc"],.field[data-sort="desc"]');
                                if ($sortedfields.length > 1) {
                                    $field.attr('data-sort', 'off');
                                    $field.find('.sort .material-icons').html('');
                                    me.databind($control);
                                }
                                else {
                                    FwNotification.renderNotification('WARNING', 'You must specify a sort order on a least 1 column.');
                                }
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $columnoptions.append($sortoffbtn);
                    }
                    if ((showsearch) && (($control.attr('data-type') !== 'Grid') && (showsearch))) {
                        $columnoptions.append('<div class="columnoptionshr"></div>');
                    }
                    if (($control.attr('data-type') !== 'Grid') && (showsearch)) {
                        var $clearbtn, $clearallbtn;
                        $clearbtn = getcolumnoptionbutton('Clear Filter', '');
                        $clearbtn.on('click', function () {
                            try {
                                $theadfield.find('.search input').val('');
                                me.databind($control);
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $columnoptions.append($clearbtn);
                        $clearallbtn = getcolumnoptionbutton('Clear All Filters', '');
                        $clearallbtn.on('click', function () {
                            try {
                                $theadfields.find('.search input').val('');
                                me.databind($control);
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $columnoptions.append($clearallbtn);
                    }
                    if ((showsearch) || (showsort)) {
                        $theadfield.on('click', '.fieldcaption', function (e) {
                            try {
                                var maxZIndex, $field;
                                e.preventDefault();
                                if ($control.find('tr.editrow').length == 0) {
                                    $field = jQuery(this);
                                    if (!$field.hasClass('active')) {
                                        maxZIndex = FwFunc.getMaxZ('*');
                                        $field.find('.columnoptions').css('z-index', maxZIndex + 1);
                                        $field.addClass('active');
                                        jQuery(document).one('click', function closeOptions(e) {
                                            if ($field.has(e.target).length === 0) {
                                                $field.removeClass('active');
                                                $field.find('.columnoptions').css('z-index', '0');
                                            }
                                            else if ($field.hasClass('active')) {
                                                jQuery(document).one('click', closeOptions);
                                            }
                                        });
                                    }
                                    else {
                                        $field.removeClass('active');
                                        $field.find('.columnoptions').css('z-index', '0');
                                    }
                                }
                            }
                            catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                });
                if ($control.attr('data-type') === 'Grid') {
                    var $menu = FwGridMenu.getMenuControl('grid');
                    $control.find('.gridmenu').append($menu);
                }
                break;
        }
        if ((typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller, $browse;
            $browse = $control.closest('.fwbrowse');
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined')
                throw 'Missing javascript module: ' + controller;
            if (($control.attr('data-type') === 'Grid') && (typeof window[controller]['addGridSubMenu'] === 'function')) {
                window[controller]['addGridSubMenu']($control, $menu);
            }
            var gridNode = FwApplicationTree.getNodeByController(controller);
            if (gridNode !== null) {
                var gridMenuBar = FwApplicationTree.getChildByType(gridNode, 'MenuBar');
                var nodeDeleteAction = FwApplicationTree.getChildByType(gridMenuBar, 'DeleteMenuBarButton');
                if (gridMenuBar !== null) {
                    if (gridMenuBar.properties.visible === 'T') {
                        var gridSubMenu = FwApplicationTree.getChildByType(gridMenuBar, 'SubMenu');
                        var $submenubtn = null;
                        if (gridSubMenu !== null && gridSubMenu.properties.visible === 'T') {
                            $submenubtn = FwGridMenu.addSubMenu($menu);
                        }
                        if ($submenubtn === null && nodeDeleteAction !== null && nodeDeleteAction.properties['visible'] === 'T') {
                            $submenubtn = FwGridMenu.addSubMenu($menu);
                        }
                        if ($submenubtn !== null) {
                            var $submenucolumn = FwGridMenu.addSubMenuColumn($submenubtn);
                            var $rowactions = FwGridMenu.addSubMenuGroup($submenucolumn, 'Actions', '');
                            if (nodeDeleteAction !== null && nodeDeleteAction.properties['visible'] === 'T') {
                                var $submenuitem = FwGridMenu.addSubMenuBtn($rowactions, 'Delete Selected', nodeDeleteAction.id);
                                $submenuitem.on('click', function (e) {
                                    try {
                                        if ($browse.attr('data-enabled') !== 'false') {
                                            try {
                                                e.stopPropagation();
                                                var $selectedCheckBoxes = $control.find('.cbselectrow:checked');
                                                if ($selectedCheckBoxes.length === 0) {
                                                    FwFunc.showMessage('Select one or more rows to delete!');
                                                }
                                                else {
                                                    var $confirmation = FwConfirmation.yesNo('Delete Record' + ($selectedCheckBoxes.length > 1 ? 's' : ''), 'Delete ' + $selectedCheckBoxes.length + ' record' + ($selectedCheckBoxes.length > 1 ? 's' : '') + '?', function onyes() {
                                                        try {
                                                            var lastCheckBoxIndex = $selectedCheckBoxes.length - 1;
                                                            for (var i = 0; i < $selectedCheckBoxes.length; i++) {
                                                                var $tr = $selectedCheckBoxes.eq(i).closest('tr');
                                                                me.deleteRecord($control, $tr, i === lastCheckBoxIndex);
                                                            }
                                                        }
                                                        catch (ex) {
                                                            FwFunc.showError(ex);
                                                        }
                                                    }, function onno() { });
                                                }
                                            }
                                            catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        }
                                        else {
                                            FwFunc.showMessage('This grid is disabled.');
                                        }
                                    }
                                    catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            }
                            if (gridSubMenu !== null) {
                                for (var gridSubMenuGroupIndex = 0; gridSubMenuGroupIndex < gridSubMenu.children.length; gridSubMenuGroupIndex++) {
                                    var gridSubMenuGroup = gridSubMenu.children[gridSubMenuGroupIndex];
                                    if ((gridSubMenuGroup && gridSubMenuGroup.properties.nodetype === 'SubMenuGroup') && (gridSubMenuGroup.properties.visible === 'T')) {
                                        var $submenucolumn = FwGridMenu.addSubMenuColumn($submenubtn);
                                        var $optiongroup = FwGridMenu.addSubMenuGroup($submenucolumn, gridSubMenuGroup.properties.caption, gridSubMenuGroup.id);
                                        for (var gridSubMenuItemIndex = 0; gridSubMenuItemIndex < gridSubMenuGroup.children.length; gridSubMenuItemIndex++) {
                                            var gridSubMenuItem = gridSubMenuGroup.children[gridSubMenuItemIndex];
                                            if ((gridSubMenuItem && gridSubMenuItem.properties.nodetype === 'SubMenuItem') && (gridSubMenuItem.properties.visible === 'T')) {
                                                var $submenuitem = FwGridMenu.addSubMenuBtn($optiongroup, gridSubMenuItem.properties.caption, gridSubMenuItem.id);
                                                $submenuitem.on('click', function (e) {
                                                    try {
                                                        e.stopPropagation();
                                                        var securityid = jQuery(e.target).closest('.submenu-btn').attr('data-securityid');
                                                        var func = FwApplicationTree.clickEvents['{' + securityid + '}'];
                                                        func.apply(this, [e]);
                                                    }
                                                    catch (ex) {
                                                        FwFunc.showError(ex);
                                                    }
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        FwGridMenu.addCaption($menu, $control.attr('data-caption'));
                        var gridNewMenuBarButton = FwApplicationTree.getChildByType(gridMenuBar, 'NewMenuBarButton');
                        var hasNew = (gridNewMenuBarButton !== null) && (gridNewMenuBarButton.properties.visible === 'T');
                        if (hasNew) {
                            var $new = FwGridMenu.addStandardBtn($menu, FwLanguages.translate('New'), gridNewMenuBarButton.id);
                            $new.attr('data-type', 'NewButton');
                            $new.on('click', function (e) {
                                try {
                                    e.stopPropagation();
                                    var $form = $control.closest('.fwform');
                                    var mode = $form.attr('data-mode');
                                    if ($control.attr('data-enabled') !== 'false') {
                                        if ((mode === 'EDIT') || ($new.closest('.fwconfirmation').length > 0)) {
                                            if (typeof $new.data('onclick') === 'function') {
                                                $new.data('onclick')($control);
                                            }
                                            else {
                                                me.addRowNewMode($control);
                                            }
                                        }
                                        else {
                                            FwNotification.renderNotification('WARNING', 'Please save the record before performing this function.');
                                        }
                                    }
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                        var gridEditMenuBarButton = FwApplicationTree.getChildByType(gridMenuBar, 'EditMenuBarButton');
                        var hasEdit = (gridEditMenuBarButton !== null) && (gridEditMenuBarButton.properties.visible === 'T');
                        var gridDeleteMenuBarButton = FwApplicationTree.getChildByType(gridMenuBar, 'DeleteMenuBarButton');
                        var hasDelete = (gridDeleteMenuBarButton !== null) && (gridDeleteMenuBarButton.properties.visible === 'T');
                        var hasSave = hasNew || hasEdit;
                        if (hasSave) {
                            var $save = FwGridMenu.addStandardBtn($menu, FwLanguages.translate('Save'));
                            $save.attr('data-type', 'SaveButton');
                            $save.css('display', 'none').on('click', function (e) {
                                try {
                                    e.stopPropagation();
                                    var $tr = $control.find('table > tbody > tr.editrow');
                                    var saveRowPromise = me.saveRow($control, $tr);
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                        var hasCancel = hasNew || hasEdit;
                        if (hasCancel) {
                            var $cancel = FwGridMenu.addStandardBtn($menu, 'Cancel');
                            $cancel.attr('data-type', 'CancelButton');
                            $cancel
                                .css('display', 'none')
                                .on('click', function (e) {
                                try {
                                    e.stopPropagation();
                                    var $tr = $control.find('table > tbody > tr.editrow');
                                    me.cancelEditMode($control, $tr);
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                    }
                }
            }
            if (typeof window[controller]['setDefaultOptions'] === 'function') {
                window[controller]['setDefaultOptions']($control);
            }
            if (typeof window[controller]['addLegend'] === 'function') {
                window[controller]['addLegend']($control);
            }
        }
        me.setGridBrowseMode($control);
    };
    FwBrowseClass.prototype.addFilterPanel = function ($control, $filterpanel) {
        $control.find('.fwbrowsefilter').empty().append($filterpanel).show();
        var fwcontrols = $filterpanel.find('.fwcontrol');
        FwControl.renderRuntimeControls(fwcontrols);
    };
    FwBrowseClass.prototype.renderTemplateHtml = function ($control) {
        var html, data_rendermode, $ths;
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
        switch (data_rendermode) {
            case 'designer':
                html = [];
                $ths = $control.find('.designer > thead td.column');
                $ths.each(function (index, th) {
                    var $th, caption, cssclass, browsedatafield, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    $th = jQuery(th);
                    width = "auto";
                    visible = $th.attr('data-visible');
                    html.push('<div class="column" data-width="' + width + '" data-visible="' + visible + '">');
                    $fields = $th.find('> .field');
                    $fields.each(function (index, field) {
                        var $field;
                        $field = jQuery(field);
                        caption = $field.attr('data-caption');
                        var datafield = $field.attr('data-datafield');
                        cssclass = $field.attr('data-cssclass');
                        var datatype = $field.attr('data-datatype');
                        html.push('<div class="field ' + cssclass + '" data-caption="' + caption + '" data-datafield="' + datafield + '" data-cssclass="' + cssclass + '" data-datatype="' + datatype + '"></div>');
                    });
                    html.push('</div>');
                });
                html = html.join('');
                $control.html(html);
                break;
            case 'runtime':
                html = [];
                $ths = $control.find('.runtime thead td.column');
                $ths.each(function (index, th) {
                    var $th, caption, cssclass, browsedatafield, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    $th = jQuery(th);
                    width = 'auto';
                    visible = $th.attr('data-visible');
                    html.push('<div class="column" ' + +'" data-visible="' + visible + '">');
                    $fields = $th.find('> .field');
                    $fields.each(function (index, field) {
                        var $field;
                        $field = jQuery(field);
                        caption = $field.attr('data-caption');
                        cssclass = $field.attr('data-cssclass');
                        browsedatafield = $field.attr('data-browsedatafield');
                        browsedatatype = $field.attr('data-browsedatatype');
                        formdatafield = $field.attr('data-formdatafield');
                        formdatatype = $field.attr('data-formdatatype');
                        html.push('<div class="field ' + cssclass + '" data-caption="' + caption + '" data-cssclass="' + cssclass + '" data-browsedatafield="' + browsedatafield + '" data-browsedatatype="' + browsedatatype + '" data-browsedatafield="' + browsedatafield + '" data-browsedatatype="' + browsedatatype + '"></div>');
                    });
                    html.push('</div>');
                });
                html = html.join('');
                $control.html(html);
                break;
            case 'template':
                break;
        }
    };
    FwBrowseClass.prototype.screenload = function ($control) {
    };
    FwBrowseClass.prototype.screenunload = function ($control) {
    };
    FwBrowseClass.prototype.getRequest = function ($control) {
        var request, $fields, orderby, $field, $txtSearch, browsedatafield, value, sort, module, controller, fieldtype;
        orderby = [];
        request = {
            module: '',
            searchfields: [],
            searchfieldtypes: [],
            searchfieldoperators: [],
            searchfieldvalues: [],
            miscfields: !$control.closest('.fwform').length ? jQuery([]) : FwModule.getFormUniqueIds($control.closest('.fwform')),
            orderby: '',
            pageno: parseInt($control.attr('data-pageno')),
            pagesize: parseInt($control.attr('data-pagesize')),
            options: this.getOptions($control)
        };
        if ($control.attr('data-type') === 'Grid') {
            request.module = $control.attr('data-name');
        }
        else if ($control.attr('data-type') === 'Validation') {
            request.module = $control.attr('data-name');
        }
        else if ($control.attr('data-type') === 'Browse') {
            if (typeof $control.attr('data-name') !== 'undefined') {
                request.module = $control.attr('data-name');
            }
            else {
                request.module = window[$control.attr('data-controller')].Module;
            }
        }
        controller = window[request.module + 'Controller'];
        if (typeof controller === 'undefined' && ($control.attr('data-type') === 'Grid' || $control.attr('data-type') === 'Browse')) {
            throw module + 'Controller is not defined.';
        }
        $fields = $control.find('.runtime thead > tr.fieldnames > td.column > div.field');
        $fields.each(function (index, element) {
            $field = jQuery(element);
            $txtSearch = $field.find('> div.search > input');
            value = $txtSearch.val();
            sort = $field.attr('data-sort');
            fieldtype = $field.attr('data-browsedatatype');
            if (typeof $field.attr('data-datafield') !== 'undefined') {
                browsedatafield = $field.attr('data-datafield');
            }
            else if (typeof $field.attr('data-browsedatafield') !== 'undefined') {
                browsedatafield = $field.attr('data-browsedatafield');
            }
            if (value.length > 0) {
                request.searchfields.push(browsedatafield);
                request.searchfieldtypes.push(fieldtype);
                if ($field.attr('data-searchfieldoperators') === 'startswith') {
                    request.searchfieldoperators.push('startswith');
                }
                else {
                    request.searchfieldoperators.push('like');
                }
                request.searchfieldvalues.push(value);
            }
            if (sort === 'asc') {
                orderby.push(browsedatafield);
            }
            if (sort === 'desc') {
                orderby.push(browsedatafield + ' desc');
            }
        });
        if (typeof $control.attr('data-hasinactive') === 'string' && $control.attr('data-hasinactive') === 'true') {
            if (typeof $control.attr('data-activeinactiveview') !== 'string') {
                $control.attr('data-activeinactiveview', 'active');
            }
            var activeinactiveview = $control.attr('data-activeinactiveview');
            switch (activeinactiveview) {
                case 'all':
                    break;
                case 'active':
                    if ((typeof controller !== 'undefined') && (typeof controller.apiurl === 'string')) {
                        request.searchfields.push('Inactive');
                        request.searchfieldoperators.push('<>');
                        request.searchfieldvalues.push('T');
                    }
                    else {
                        request.searchfields.push('inactive');
                        request.searchfieldoperators.push('<>');
                        request.searchfieldvalues.push('T');
                    }
                    break;
                case 'inactive':
                    if ((typeof controller !== 'undefined') && (typeof controller.apiurl === 'string')) {
                        request.searchfields.push('Inactive');
                        request.searchfieldoperators.push('=');
                        request.searchfieldvalues.push('T');
                    }
                    else {
                        request.searchfields.push('inactive');
                        request.searchfieldoperators.push('=');
                        request.searchfieldvalues.push('T');
                    }
                    break;
            }
        }
        orderby = orderby.join(',');
        request.orderby = orderby;
        if (typeof $control.data('ondatabind') === 'function') {
            $control.data('ondatabind')(request);
        }
        return request;
    };
    FwBrowseClass.prototype.databind = function ($control) {
        var _this = this;
        var me = this;
        return new Promise(function (resolve, reject) {
            jQuery(window).off('click.FwBrowse');
            var request, caption;
            if ($control.length > 0) {
                request = _this.getRequest($control);
                if (typeof $control.data('calldatabind') === 'function') {
                    $control.data('calldatabind')(request, function (response) {
                        resolve();
                    });
                }
                else {
                    if ($control.attr('data-type') === 'Grid') {
                        FwServices.grid.method(request, request.module, 'Browse', $control, function (response) {
                            try {
                                me.beforeDataBindCallBack($control, request, response);
                                resolve();
                            }
                            catch (ex) {
                                reject(ex);
                            }
                        });
                    }
                    else if ($control.attr('data-type') === 'Validation') {
                        FwServices.validation.method(request, request.module, 'Browse', $control, function (response) {
                            try {
                                me.beforeDataBindCallBack($control, request, response);
                                resolve();
                            }
                            catch (ex) {
                                reject(ex);
                            }
                        });
                    }
                    else if ($control.attr('data-type') === 'Browse') {
                        FwServices.module.method(request, request.module, 'Browse', $control, function (response) {
                            try {
                                me.beforeDataBindCallBack($control, request, response);
                                resolve();
                            }
                            catch (ex) {
                                reject(ex);
                            }
                        });
                    }
                    else {
                        reject('Unknown Browse Control type.');
                    }
                }
            }
        });
    };
    FwBrowseClass.prototype.beforeDataBindCallBack = function ($control, request, response) {
        var controller = window[request.module + 'Controller'];
        if (typeof controller === 'undefined') {
            throw request.module + 'Controller is not defined.';
        }
        this.databindcallback($control, response);
    };
    FwBrowseClass.prototype.databindcallback = function ($control, dt) {
        var me = this;
        var i, $tbody, htmlPager, columnIndex, dtCol, rowIndex, scrollerCol, rowClass, columns, onrowdblclick, $ths, $pager, pageSize, totalRowCount, controlType, $fields;
        try {
            this.setGridBrowseMode($control);
            pageSize = this.getPageSize($control);
            this.setTotalPages($control, dt.TotalPages);
            totalRowCount = $control.data('totalRowCount', dt.TotalRows);
            var nodeModule = FwApplicationTree.getNodeByController($control.attr('data-controller'));
            var nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, function (node, args2) {
                return (node.properties.nodetype === 'EditMenuBarButton');
            });
            onrowdblclick = $control.data('onrowdblclick');
            dt.ColumnIndex = {};
            for (i = 0; i < dt.Columns.length; i++) {
                dtCol = dt.Columns[i];
                dt.ColumnIndex[dtCol.DataField] = i;
            }
            $tbody = $control.find('.runtime tbody');
            $tbody.empty();
            for (rowIndex = 0; rowIndex < dt.Rows.length; rowIndex++) {
                var $tr;
                $tr = this.generateRow($control);
                if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                    $tr.attr('tabindex', '0');
                }
                $tr.addClass('viewmode');
                $fields = $tr.find('.field');
                for (var j = 0; j < $fields.length; j++) {
                    var $field, dtColIndex, dtRow, dtCellValue, $td;
                    $field = jQuery($fields[j]);
                    $td = $field.parent('td');
                    dtColIndex = dt.ColumnIndex[$field.attr('data-browsedatafield')];
                    dtRow = dt.Rows[rowIndex];
                    dtCellValue = dtRow[dtColIndex];
                    if ($field.attr('data-formreadonly') !== 'true') {
                        if (typeof $control.data('isfieldeditable') === 'function' && $control.data('isfieldeditable')($field, dt, rowIndex)) {
                        }
                        else if (nodeEdit !== null) {
                            $field.addClass('editablefield');
                        }
                    }
                    if (typeof dtCellValue !== 'undefined') {
                        $field.attr('data-originalvalue', dtCellValue.toString());
                    }
                    else {
                        $field.attr('data-originalvalue', '');
                    }
                    var cellcolor = $field.attr('data-cellcolor');
                    if (typeof cellcolor !== 'undefined') {
                        $td.children().css('padding-left', '10px');
                        if ((cellcolor.length > 0) && ((dtRow[dt.ColumnIndex[cellcolor]]) !== null) && ((dtRow[dt.ColumnIndex[cellcolor]]) != "")) {
                            if (typeof dt.ColumnIndex[cellcolor] !== 'number') {
                                throw 'FwBrowse.databindcallback: cellcolor: "column ' + cellcolor + '" was not returned by the web service.';
                            }
                            var css_1 = {
                                'position': 'relative',
                                'border-top-color': dtRow[dt.ColumnIndex[cellcolor]],
                                'border-top-style': 'none',
                            };
                            $td.addClass('cellColor').css(css_1);
                        }
                    }
                    var halfcellcolor = $field.attr('data-halfcellcolor');
                    if (typeof halfcellcolor !== 'undefined') {
                        $td.children().css('padding-left', '10px');
                        if ((halfcellcolor.length > 0) && ((dtRow[dt.ColumnIndex[halfcellcolor]]) !== null) && ((dtRow[dt.ColumnIndex[halfcellcolor]]) != "")) {
                            if (typeof dt.ColumnIndex[halfcellcolor] !== 'number') {
                                throw 'FwBrowse.databindcallback: halfcellcolor: "column ' + halfcellcolor + '" was not returned by the web service.';
                            }
                            if ($field.attr('data-formreadonly') === 'true') {
                                var css = {
                                    'position': 'relative',
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[halfcellcolor]] + ', rgba(245, 245, 245, 1)50%)'
                                };
                            }
                            else {
                                var css = {
                                    'position': 'relative',
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[halfcellcolor]] + ', rgba(255, 255, 255, 0)50%)'
                                };
                            }
                            $td.children().css(css).addClass('cellgradient');
                        }
                    }
                    var fullcellcolor = $field.attr('data-fullcellcolor');
                    if (typeof fullcellcolor !== 'undefined') {
                        $td.children().css('padding-left', '10px');
                        if ((fullcellcolor.length > 0) && ((dtRow[dt.ColumnIndex[fullcellcolor]]) !== null) && ((dtRow[dt.ColumnIndex[fullcellcolor]]) != "")) {
                            if (typeof dt.ColumnIndex[fullcellcolor] !== 'number') {
                                throw 'FwBrowse.databindcallback: fullcellcolor: "column ' + fullcellcolor + '" was not returned by the web service.';
                            }
                            if ($field.attr('data-formreadonly') === 'true') {
                                var css = {
                                    'position': 'relative',
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[fullcellcolor]] + ', rgba(245, 245, 245, 1))'
                                };
                            }
                            else {
                                var css = {
                                    'position': 'relative',
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[fullcellcolor]] + ', rgba(255, 255, 255, 0))'
                                };
                            }
                            $td.children().css(css).addClass('cellgradient');
                        }
                    }
                    var browsecellbackgroundcolorfield = $field.attr('data-browsecellbackgroundcolorfield');
                    if ((typeof browsecellbackgroundcolorfield !== 'undefined') && (browsecellbackgroundcolorfield.length > 0)) {
                        if (typeof dt.ColumnIndex[browsecellbackgroundcolorfield] !== 'number') {
                            throw 'FwBrowse.databindcallback: browsecellbackgroundcolorfield: "column ' + browsecellbackgroundcolorfield + '" was not returned by the web service.';
                        }
                        $td.css({ 'background-color': dtRow[dt.ColumnIndex[browsecellbackgroundcolorfield]] });
                    }
                    var browsecelltextcolorfield = $field.attr('data-browsecelltextcolorfield');
                    if ((typeof browsecelltextcolorfield !== 'undefined') && (browsecelltextcolorfield.length > 0)) {
                        if (typeof dt.ColumnIndex[browsecelltextcolorfield] !== 'number') {
                            throw 'FwBrowse.databindcallback: browsecelltextcolorfield: "column ' + browsecelltextcolorfield + '" was not returned by the web service.';
                        }
                        $td.css({ color: dtRow[dt.ColumnIndex[browsecelltextcolorfield]] });
                    }
                    var browsecellwhitetextfield = $field.attr('data-browsecellwhitetextfield');
                    if ((typeof browsecellwhitetextfield !== 'undefined') && (browsecellwhitetextfield.length > 0)) {
                        if (typeof dt.ColumnIndex[browsecellwhitetextfield] !== 'number') {
                            throw 'FwBrowse.databindcallback: browsecellwhitetextfield: "column ' + browsecellwhitetextfield + '" was not returned by the web service.';
                        }
                        if (dtRow[dt.ColumnIndex[browsecellwhitetextfield]] === 'T') {
                            $td.css({ color: '#ffffff' });
                        }
                    }
                    if (typeof window['FwBrowseColumn_' + $field.attr('data-browsedatatype')] !== 'undefined') {
                        if (typeof window['FwBrowseColumn_' + $field.attr('data-browsedatatype')].databindfield === 'function') {
                            window['FwBrowseColumn_' + $field.attr('data-browsedatatype')].databindfield($control, $field, dt, dtRow, $tr);
                        }
                    }
                    this.setFieldViewMode($control, $tr, $field);
                    var AFTER_RENDER_FIELD = 'afterrenderfield';
                    if (typeof $control.data(AFTER_RENDER_FIELD) === 'function') {
                        var funcAfterRenderField = ($control.data(AFTER_RENDER_FIELD));
                        funcAfterRenderField($tr, $td, $field, dt, rowIndex, dtColIndex);
                    }
                }
                if (((typeof dt.ColumnIndex['inactive'] === 'number') && (dt.Rows[rowIndex][dt.ColumnIndex['inactive']] === 'T')) ||
                    ((typeof dt.ColumnIndex['Inactive'] === 'number') && (dt.Rows[rowIndex][dt.ColumnIndex['Inactive']] === true))) {
                    $tr.addClass('inactive');
                }
                $tbody.append($tr);
                var AFTER_RENDER_ROW = 'afterrenderrow';
                if (typeof $control.data(AFTER_RENDER_ROW) === 'function') {
                    var funcAfterRenderRow = $control.data(AFTER_RENDER_ROW);
                    funcAfterRenderRow($tr, dt, rowIndex);
                }
            }
            if ($control.attr('data-type') === 'Grid') {
                var $trs = $control.find('tbody tr');
                $control.find('tbody tr').on('click', function (e) {
                    try {
                        e.stopPropagation();
                        var $td = jQuery(this);
                        var $tr = $td.closest('tr');
                        if (!$tr.hasClass('selected')) {
                            me.selectRow($control, $tr, true);
                            if (typeof $control.data('onselectedrowchanged') === 'function') {
                                $control.data('onselectedrowchanged')($control, $tr);
                            }
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                $control.find('tbody .browsecontextmenu').on('click', function (e) {
                    try {
                        e.stopPropagation();
                        var $browse = jQuery(this).closest('.fwbrowse');
                        if ($browse.attr('data-enabled') !== 'false') {
                            var $fwcontextmenus = $browse.find('tbody .fwcontextmenu');
                            for (var i_1 = 0; i_1 < $fwcontextmenus.length; i_1++) {
                                FwContextMenu.destroy($fwcontextmenus.eq(i_1));
                            }
                            var menuItemCount = 0;
                            var $browsecontextmenu = jQuery(this);
                            var $tr = $browsecontextmenu.closest('tr');
                            var $contextmenu = FwContextMenu.render('Options', 'bottomleft', $browsecontextmenu);
                            var controller = $control.attr('data-controller');
                            if (typeof controller === 'undefined') {
                                throw 'Attribute data-controller is not defined on Browse control.';
                            }
                            var nodeController = FwApplicationTree.getNodeByController(controller);
                            if (nodeController !== null) {
                                var deleteActions = FwApplicationTree.getChildrenByType(nodeController, 'DeleteMenuBarButton');
                                if (deleteActions.length > 1) {
                                    throw 'Invalid Security Tree configuration.  Only 1 DeleteMenuBarButton is permitted on a Controller.';
                                }
                                if (deleteActions.length === 1 && deleteActions[0].properties['visible'] === 'T') {
                                    FwContextMenu.addMenuItem($contextmenu, 'Delete', function () {
                                        try {
                                            var $tr = jQuery(this).closest('tr');
                                            me.deleteRow($control, $tr);
                                        }
                                        catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                    menuItemCount++;
                                }
                            }
                            if (menuItemCount === 0) {
                                FwContextMenu.destroy($contextmenu);
                            }
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                $control.find('tbody tr .btnpeek').on('click', function (e) {
                    try {
                        var $td_1 = jQuery(this).parent();
                        FwValidation.validationPeek($control, $td_1.data('validationname').slice(0, -10), $td_1.data('originalvalue'), $td_1.data('browsedatafield'), null, $td_1.data('originaltext'));
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                    e.stopPropagation();
                });
                $control.find('tbody tr .editablefield').on('click', function (e) {
                    try {
                        var $field = jQuery(this);
                        var $tr = $field.closest('tr');
                        if (!$tr.hasClass('selected')) {
                            me.selectRow($control, $tr, true);
                            if (typeof $control.data('onselectedrowchanged') === 'function') {
                                $control.data('onselectedrowchanged')($control, $tr);
                            }
                        }
                        if ($control.attr('data-type') === 'Grid' && $control.attr('data-enabled') !== 'false' && !$tr.hasClass('editmode')) {
                            me.setRowEditMode($control, $tr);
                            $field.find('.value').focus();
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
            var spacerHeight = 0;
            if (pageSize <= 15) {
                spacerHeight = 25 * (pageSize - dt.Rows.length);
            }
            else {
                spacerHeight = 25 * (15 - dt.Rows.length);
            }
            if (spacerHeight > 0) {
                $control.find('.runtime tfoot tr.spacerrow > td > div').show().height(spacerHeight);
            }
            else {
                $control.find('.runtime tfoot tr.spacerrow > td > div').hide();
            }
            var rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
            var rownoend = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
            if (dt.TotalPages > 1) {
                if ((pageSize > 0) && (dt.PageNo > 1)) {
                    $control.find('.pager .btnFirstPage')
                        .attr('data-enabled', 'true')
                        .prop('disabled', false);
                    $control.find('.pager .btnPreviousPage')
                        .attr('data-enabled', 'true')
                        .prop('disabled', false);
                }
                else {
                    $control.find('.pager .btnFirstPage')
                        .attr('data-enabled', 'false')
                        .prop('disabled', true);
                    $control.find('.pager .btnPreviousPage')
                        .attr('data-enabled', 'false')
                        .prop('disabled', true);
                }
                if (dt.TotalPages > 0) {
                    $control.find('.txtPageNo').val(dt.PageNo);
                }
                else {
                    $control.find('.txtPageNo').val('0');
                }
                $control.find('.pager .txtTotalPages').text(dt.TotalPages);
                if ((pageSize > 0) && (dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                    $control.find('.pager .btnNextPage')
                        .attr('data-enabled', 'true')
                        .prop('disabled', false);
                    $control.find('.pager .btnLastPage')
                        .attr('data-enabled', 'true')
                        .prop('disabled', false);
                }
                else {
                    $control.find('.pager .btnNextPage')
                        .attr('data-enabled', 'false')
                        .prop('disabled', true);
                    $control.find('.pager .btnLastPage')
                        .attr('data-enabled', 'false')
                        .prop('disabled', true);
                }
            }
            var controlType_1 = $control.attr('data-type');
            switch (controlType_1) {
                case 'Browse':
                    if ((rownoend === 0) && (dt.TotalRows === 0)) {
                        $control.find('.pager .count').text(dt.TotalRows + ' rows');
                    }
                    else {
                        if (dt.TotalPages == 1) {
                            $control.find('.pager .count').text(dt.TotalRows + ' rows');
                        }
                        else {
                            $control.find('.pager .count').text(rownostart + ' to ' + rownoend + ' of ' + dt.TotalRows + ' rows');
                        }
                    }
                    break;
                case 'Grid':
                case 'Validation':
                    $control.find('.pager .count').text(dt.TotalRows + ' row(s)');
                    break;
            }
            if ((typeof onrowdblclick !== 'undefined') && ($control.attr('data-multiselectvalidation') !== 'true')) {
                $control.find('.runtime tbody > tr').on('dblclick', onrowdblclick);
            }
            if ((typeof $control.attr('data-type') === 'string') && ($control.attr('data-type') === 'Validation')) {
                FwValidation.validateSearchCallback($control);
            }
            setTimeout(function () {
                var selectedindex = me.getSelectedIndex($control);
                var rowcount = me.getRowCount($control);
                if (rowcount > me.getSelectedIndex($control) && selectedindex !== -1) {
                    var $tr_1 = me.selectRowByIndex($control, selectedindex);
                    var selectedRowMode = me.getSelectedRowMode($control);
                    switch (selectedRowMode) {
                        case 'view':
                            me.setRowViewMode($control, $tr_1);
                            break;
                        case 'new':
                            me.setRowNewMode($control, $tr_1);
                            break;
                        case 'edit':
                            me.setRowEditMode($control, $tr_1);
                            break;
                    }
                }
                else if (rowcount < selectedindex) {
                    var lastrowindex = rowcount - 1;
                    me.selectRowByIndex($control, lastrowindex);
                }
                if (typeof $control.data('afterdatabindcallback') === 'function') {
                    $control.data('afterdatabindcallback')($control, dt);
                }
                else if (Array.isArray($control.data('afterdatabindcallback'))) {
                    var afterdatabindcallbacks = $control.data('afterdatabindcallback');
                    for (var i = 0; i < afterdatabindcallbacks.length; i++) {
                        afterdatabindcallbacks[i]($control, dt);
                    }
                }
            }, 250);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    FwBrowseClass.prototype.generateRow = function ($control) {
        var $table, $theadtds, $tr;
        $table = $control.find('table');
        $tr = jQuery('<tr>');
        $theadtds = $table.find('> thead > tr.fieldnames > td.column');
        var _loop_1 = function (i) {
            var $theadtd = $theadtds.eq(i);
            var $td = $theadtd.clone().empty();
            $tr.append($td);
            $theadfields = $theadtd.children('.field');
            $theadfields.each(function (index, element) {
                var $theadfield, $field, $field_newmode, formdatatype;
                $theadfield = jQuery(element);
                $field = $theadfield.clone().empty();
                $td.append($field);
            });
        };
        var $theadfields;
        for (var i = 0; i < $theadtds.length; i++) {
            _loop_1(i);
        }
        if (($control.attr('data-type') === 'Browse') && ($control.attr('data-hasmultirowselect') === 'true')) {
            var cbuniqueId = FwApplication.prototype.uniqueId(10);
            $tr.find('.tdselectrow').append('<div class="divselectrow"><input id="' + cbuniqueId + '" type="checkbox" class="cbselectrow" /><label for="' + cbuniqueId + '" class="lblselect"></label><div>');
        }
        if ($control.attr('data-type') === 'Grid') {
            var cbuniqueId = FwApplication.prototype.uniqueId(10);
            $tr.find('.tdselectrow').append('<div class="divselectrow"><input id="' + cbuniqueId + '" type="checkbox" class="cbselectrow" /><label for="' + cbuniqueId + '" class="lblselect"></label><div>');
            $tr.find('.browsecontextmenucell').append('<div class="browsecontextmenu"><i class="material-icons">more_vert</i><div>');
        }
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['generateRow'] === 'function') {
                window[controller]['generateRow']($control, $tr);
            }
        }
        return $tr;
    };
    FwBrowseClass.prototype.setGridBrowseMode = function ($control) {
        var $table, $trNewMode, $trEditMode;
        $control.attr('data-mode', 'VIEW');
        $table = $control.find('table');
        $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').show();
        $control.find('.gridmenu .buttonbar div[data-type="EditButton"]').show();
        $control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').show();
        $control.find('.gridmenu .buttonbar div[data-type="SaveButton"]').hide();
        $control.find('.gridmenu .buttonbar div[data-type="CancelButton"]').hide();
        $trNewMode = $table.find('> tbody > tr.newmode');
        $trNewMode.remove();
        $trEditMode = $table.find('> tbody > tr.editmode');
        if ($trEditMode.length > 0) {
            this.setRowViewMode($control, $trEditMode);
        }
    };
    FwBrowseClass.prototype.addRowNewMode = function ($control) {
        var $table, $tr, $tbody;
        $table = $control.find('.runtime table');
        if ($table.find('> tbody > tr.editrow.newmode').length === 0) {
            $tr = this.generateRow($control);
            $tr.addClass('editrow newmode');
            $tbody = $table.find('> tbody');
            $tbody.prepend($tr);
            this.setRowNewMode($control, $tr);
            this.addSaveAndCancelButtonToRow($control, $tr);
        }
    };
    FwBrowseClass.prototype.setRowNewMode = function ($control, $tr) {
        var me = this;
        this.beforeNewOrEditRow($control, $tr);
        var $fields, $inputs;
        $control.attr('data-mode', 'EDIT');
        $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
        $fields = $tr.find('.field');
        $fields.each(function (index, element) {
            var $field = jQuery(element);
            $field.attr('data-originalvalue', '');
            if ($field.attr('data-formreadonly') === 'true') {
                me.setFieldViewMode($control, $tr, $field);
            }
            else {
                me.setFieldEditMode($control, $tr, $field);
            }
        });
        $inputs = $tr.find('input[type!="hidden"]:visible,select:visible,textarea:visible');
        if ($inputs.length > 0) {
            $inputs.eq(0).select();
        }
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['onRowNewMode'] === 'function') {
                window[controller]['onRowNewMode']($control, $tr);
            }
        }
    };
    FwBrowseClass.prototype.setRowViewMode = function ($control, $tr) {
        var me = this;
        jQuery(window).off('click.FwBrowse');
        $tr.find('.divsaverow').remove();
        $tr.find('.divcancelsaverow').remove();
        $tr.find('.divselectrow').show();
        $tr.find('.browsecontextmenu').show();
        $tr.removeClass('editmode').removeClass('editrow').addClass('viewmode');
        $tr.find('> td > .field').each(function (index, field) {
            var $field, html;
            $field = jQuery(field);
            me.setFieldViewMode($control, $tr, $field);
        });
        var $trEditModeRows = $control.find('tbody tr.editmode');
        if ($trEditModeRows.length === 0) {
            $control.find('thead .tdselectrow .divselectrow').show();
            $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').show();
            $control.find('tbody tr .divselectrow').show();
            $control.find('tbody tr .browsecontextmenu').show();
        }
    };
    FwBrowseClass.prototype.setFieldViewMode = function ($control, $tr, $field) {
        var browsedatatype = (typeof $field.attr('data-browsedatatype') === 'string') ? $field.attr('data-browsedatatype') : '';
        if (typeof window['FwBrowseColumn_' + browsedatatype] !== 'undefined') {
            if (typeof window['FwBrowseColumn_' + browsedatatype].setFieldViewMode === 'function') {
                window['FwBrowseColumn_' + browsedatatype].setFieldViewMode($control, $tr, $field);
            }
        }
    };
    FwBrowseClass.prototype.cancelEditMode = function ($control, $tr) {
        var $inputFile;
        $inputFile = $tr.find('input[type="file"]');
        if (($inputFile.length > 0) && ($inputFile.val().length > 0)) {
            this.search($control);
        }
        else {
            if ($tr.hasClass('newmode')) {
                $tr.remove();
            }
            var $tdselectrow = $tr.find('.tdselectrow');
            $tdselectrow.find('.divsaverow').remove();
            $tdselectrow.find('.divselectrow').show();
            var $browsecontextmenucell = $tr.find('.browsecontextmenucell');
            $browsecontextmenucell.find('.divcancelsaverow').remove();
            $browsecontextmenucell.find('.browsecontextmenu').show();
            this.setRowViewMode($control, $tr);
        }
    };
    FwBrowseClass.prototype.autoSave = function ($control, $trToExclude) {
        var $trsNewMode = $control.find('tr.newmode').not($trToExclude);
        var promises = [];
        for (var i = 0; i < $trsNewMode.length; i++) {
            var $trNewMode = $trsNewMode.eq(i);
            var saveRowPromise = this.saveRow($control, $trNewMode);
            promises.push(saveRowPromise);
        }
        var $trsEditMode = $control.find('tr.editmode').not($trToExclude);
        for (var i = 0; i < $trsEditMode.length; i++) {
            var $trEditMode = $trsEditMode.eq(i);
            var saveRowPromise = this.saveRow($control, $trEditMode);
            promises.push(saveRowPromise);
        }
        return Promise.all(promises);
    };
    ;
    FwBrowseClass.prototype.beforeNewOrEditRow = function ($control, $tr) {
        var _this = this;
        var me = this;
        return new Promise(function (resolve, reject) {
            _this.autoSave($control, $tr)
                .then(function () {
                if (typeof $control.attr('data-autosave') === 'undefined' || $control.attr('data-autosave') === 'true') {
                    $control.find('thead .tdselectrow .divselectrow').hide();
                    jQuery(window)
                        .off('click.FwBrowse')
                        .on('click.FwBrowse', function (e) {
                        try {
                            var triggerAutoSave = true;
                            var clockPicker = jQuery(document.body).find('.clockpicker-popover');
                            if (jQuery(e.target).closest('.fwconfirmation').length > 0 || jQuery(e.target).closest('body').length === 0) {
                                triggerAutoSave = false;
                            }
                            else if ((jQuery(e.target).closest('body').length === 0 && jQuery(e.target).find('body').length > 0) || (jQuery(e.target).closest('body').length > 0 && jQuery(e.target).find('body').length === 0)) {
                                triggerAutoSave = true;
                            }
                            if ($control.find('.tablewrapper tbody').get(0).contains(e.target)) {
                                triggerAutoSave = false;
                            }
                            if (clockPicker.length > 0) {
                                for (var i = 0; i < clockPicker.length; i++) {
                                    if (clockPicker.css('display') === 'none' && !clockPicker.get(i).contains(e.target)) {
                                        triggerAutoSave = true;
                                    }
                                    else if (clockPicker.get(i).contains(e.target)) {
                                        triggerAutoSave = false;
                                    }
                                }
                            }
                            if (triggerAutoSave) {
                                me.saveRow($control, $tr);
                            }
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                resolve();
            })
                .catch(function (reason) {
                reject(reason);
            });
        });
    };
    FwBrowseClass.prototype.setRowEditMode = function ($control, $tr) {
        var me = this;
        var rowIndex = $tr.index();
        this.beforeNewOrEditRow($control, $tr)
            .then(function () {
            $tr = $control.find('tbody tr').eq(rowIndex);
            $control.attr('data-mode', 'EDIT');
            $tr.removeClass('viewmode').addClass('editmode').addClass('editrow');
            $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
            if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                var controller;
                controller = $control.attr('data-controller');
                if (typeof window[controller] === 'undefined')
                    throw 'Missing javascript module: ' + controller;
                if (typeof window[controller]['beforeRowEditMode'] === 'function') {
                    window[controller]['beforeRowEditMode']($control, $tr);
                }
            }
            $tr.find('> td > .field').each(function (index, element) {
                var $field;
                $field = jQuery(element);
                if ($field.attr('data-formreadonly') === 'true') {
                    me.setFieldViewMode($control, $tr, $field);
                }
                else {
                    me.setFieldEditMode($control, $tr, $field);
                }
            });
            if ($tr.hasClass('newmode')) {
                var $inputs = $tr.find('input[type!="hidden"]:visible,select:visible,textarea:visible');
                if ($inputs.length > 0) {
                    $inputs.eq(0).select();
                }
            }
            me.addSaveAndCancelButtonToRow($control, $tr);
            if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                var controller;
                controller = $control.attr('data-controller');
                if (typeof window[controller] === 'undefined')
                    throw 'Missing javascript module: ' + controller;
                if (typeof window[controller]['afterRowEditMode'] === 'function') {
                    window[controller]['afterRowEditMode']($control, $tr);
                }
            }
        });
    };
    FwBrowseClass.prototype.addSaveAndCancelButtonToRow = function ($control, $tr) {
        var me = this;
        var $browsecontextmenucell = $tr.find('.browsecontextmenucell');
        $tr.closest('tbody').find('.divselectrow').hide();
        var $divsaverow = jQuery('<div class="divsaverow"><i class="material-icons">&#xE161;</i></div>');
        $divsaverow.on('click', function () {
            try {
                var $this = jQuery(this);
                var $tr = $this.closest('tr');
                var saveRowPromise = me.saveRow($control, $tr);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $browsecontextmenucell.append($divsaverow);
        var $tdselectrow = $tr.find('.tdselectrow');
        $tr.closest('tbody').find('.browsecontextmenu').hide();
        var $divcancelsaverow = jQuery('<div class="divcancelsaverow"><i class="material-icons">&#xE5C9;</i></div>');
        $divcancelsaverow.on('click', function () {
            try {
                var $this = jQuery(this);
                var $tr = $this.closest('tr');
                me.cancelEditMode($control, $tr);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $tdselectrow.append($divcancelsaverow);
    };
    FwBrowseClass.prototype.setFieldEditMode = function ($control, $tr, $field) {
        var formdatatype = (typeof $field.attr('data-formdatatype') === 'string') ? $field.attr('data-formdatatype') : '';
        if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
            if (typeof window['FwBrowseColumn_' + formdatatype].setFieldEditMode === 'function') {
                window['FwBrowseColumn_' + formdatatype].setFieldEditMode($control, $tr, $field);
            }
        }
    };
    FwBrowseClass.prototype.appdocumentimageLoadFile = function ($control, $field, file) {
        try {
            var reader_1 = new FileReader();
            reader_1.onloadend = function () {
                $field.data('filedataurl', reader_1.result);
                $field.attr('data-filepath', file.name);
                if (reader_1.result.indexOf('data:application/pdf;') == 0) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-pdf.png');
                }
                else if (reader_1.result.indexOf('data:image/') == 0) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-image.png');
                }
                else if ((reader_1.result.indexOf('data:application/vnd.ms-excel;') == 0) || (reader_1.result.indexOf('data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;') == 0)) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-spreadsheet.png');
                }
                else if (((reader_1.result.indexOf('data:application/msword;') == 0)) || (reader_1.result.indexOf('data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;') == 0)) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-document.png');
                }
                else {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-generic.png');
                }
            };
            if (file) {
                reader_1.readAsDataURL(file);
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    FwBrowseClass.prototype.getRowBrowseUniqueIds = function ($control, $tr) {
        var uniqueids = {};
        var $uniqueidfields = $tr.find('> td.column > div.field[data-isuniqueid="true"]');
        $uniqueidfields.each(function (index, element) {
            var $field, browsedatafield, originalvalue;
            $field = jQuery(element);
            browsedatafield = $field.attr('data-browsedatafield');
            originalvalue = $field.attr('data-originalvalue');
            uniqueids[browsedatafield] = originalvalue;
        });
        return uniqueids;
    };
    FwBrowseClass.prototype.getRowFormUniqueIds = function ($control, $tr) {
        var uniqueids = {};
        var $uniqueidfields = $tr.find('> td.column > div.field[data-isuniqueid="true"]');
        $uniqueidfields.each(function (index, element) {
            var $field, formdatafield, formdatatype, value, originalvalue;
            $field = jQuery(element);
            formdatafield = $field.attr('data-formdatafield');
            formdatatype = $field.attr('data-formdatatype');
            originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
            var uniqueid = {
                datafield: formdatafield,
                value: originalvalue
            };
            if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
                if (typeof window['FwBrowseColumn_' + formdatatype].getFieldUniqueId === 'function') {
                    window['FwBrowseColumn_' + formdatatype].getFieldUniqueId($control, $tr, $field, uniqueid, originalvalue);
                }
            }
            uniqueids[formdatafield] = uniqueid;
        });
        return uniqueids;
    };
    FwBrowseClass.prototype.getRowFormDataFields = function ($control, $tr, getmiscfields) {
        var $fields;
        var fields = {};
        if (getmiscfields === false) {
            $fields = $tr.find('> td.column > div.field[data-formreadonly!="true"][data-formdatafield][data-formdatafield!=""]');
        }
        else {
            $fields = $tr.find('> td.column > div.field[data-miscfield]');
        }
        $fields.each(function (index, element) {
            var $field = jQuery(element);
            var formdatafield = (typeof $field.attr('data-formdatafield') === 'string') ? $field.attr('data-formdatafield') : '';
            var formdatatype = (typeof $field.attr('data-formdatatype') === 'string') ? $field.attr('data-formdatatype') : '';
            var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
            var miscfield = (typeof $field.attr('data-miscfield') === 'string') ? $field.attr('data-miscfield') : '';
            var field = {
                datafield: formdatafield,
                value: originalvalue
            };
            if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
                if (typeof window['FwBrowseColumn_' + formdatatype].getFieldValue === 'function') {
                    window['FwBrowseColumn_' + formdatatype].getFieldValue($control, $tr, $field, field, originalvalue);
                }
            }
            if (getmiscfields === false) {
                fields[formdatafield] = field;
            }
            else {
                fields[miscfield] = field;
            }
        });
        return fields;
    };
    FwBrowseClass.prototype.getWebApiRowFields = function ($control, $tr) {
        var fields = {};
        var $fields = $tr.find('> td.column > div.field[data-formdatafield][data-formdatafield!=""]');
        $fields.each(function (index, element) {
            var $field = jQuery(element);
            var formdatafield = (typeof $field.attr('data-formdatafield') === 'string') ? $field.attr('data-formdatafield') : '';
            var formdatatype = (typeof $field.attr('data-formdatatype') === 'string') ? $field.attr('data-formdatatype') : '';
            var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
            var field = {
                datafield: formdatafield,
                value: originalvalue
            };
            if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
                if (typeof window['FwBrowseColumn_' + formdatatype].getFieldValue === 'function') {
                    window['FwBrowseColumn_' + formdatatype].getFieldValue($control, $tr, $field, field, originalvalue);
                }
            }
            fields[formdatafield] = field.value;
        });
        return fields;
    };
    FwBrowseClass.prototype.saveRow = function ($control, $tr) {
        var _this = this;
        var me = this;
        return new Promise(function (resolve, reject) {
            var isvalid = true;
            if (_this.isRowModified($control, $tr)) {
                var fields, rowuniqueids, rowfields, formuniqueids, formfields, $form, miscfields;
                var name = $control.attr('data-name');
                var mode = $tr.hasClass('newmode') ? 'Insert' : $tr.hasClass('editmode') ? 'Update' : '';
                if (typeof $control.attr('data-name') === 'undefined') {
                    throw 'Attrtibute data-name is missing on the Browser controller with html: ' + $control[0].outerHTML;
                }
                var controller = window[$control.attr('data-name') + 'Controller'];
                if (typeof controller === 'undefined') {
                    throw 'Controller: ' + $control.attr('data-name') + ' is not defined';
                }
                if (mode === '')
                    throw 'FwBrowse.saveRow: Invalid mode';
                isvalid = _this.validateRow($control, $tr);
                if (isvalid) {
                    var isUsingWebApi = _this.isUsingWebApi($control);
                    var request;
                    $form = $control.closest('.fwform');
                    if (isUsingWebApi) {
                        var allparentformfields = FwModule.getWebApiFields($form, true);
                        var parentformfields = {};
                        var whitelistedFields = (typeof $control.attr('data-parentformdatafields') !== 'undefined') ? $control.attr('data-parentformdatafields') : '';
                        if (whitelistedFields.length > 0) {
                            var whitelistedFieldsArray = whitelistedFields.split(',');
                            for (var fieldname in allparentformfields) {
                                for (var i = 0; i < whitelistedFieldsArray.length; i++) {
                                    var whitelistedField = whitelistedFieldsArray[i];
                                    var indexOfEquals = whitelistedField.indexOf('=');
                                    if (indexOfEquals === -1) {
                                        if (fieldname === whitelistedFieldsArray[i]) {
                                            parentformfields[fieldname] = allparentformfields[fieldname];
                                        }
                                    }
                                    else {
                                        var apiName = whitelistedField.substr(0, indexOfEquals - 1);
                                        var parentFormFieldName = whitelistedField.substr(indexOfEquals);
                                        if (fieldname === apiName) {
                                            parentformfields[fieldname] = allparentformfields[fieldname];
                                        }
                                    }
                                }
                            }
                        }
                        var gridfields = _this.getWebApiRowFields($control, $tr);
                        request = jQuery.extend({}, parentformfields, gridfields);
                        if (typeof $control.data('beforesave') === 'function') {
                            $control.data('beforesave')(request);
                        }
                    }
                    else {
                        rowuniqueids = _this.getRowFormUniqueIds($control, $tr);
                        rowfields = _this.getRowFormDataFields($control, $tr, false);
                        miscfields = _this.getRowFormDataFields($control, $tr, true);
                        if ($form.length > 0) {
                            formuniqueids = FwModule.getFormUniqueIds($form);
                            formfields = FwModule.getFormFields($form, true);
                        }
                        if (mode === 'Insert') {
                            for (var rowfield in rowfields) {
                                for (var rowuniqueid in rowuniqueids) {
                                    if (rowfield === rowuniqueid) {
                                        delete rowuniqueids[rowuniqueid];
                                    }
                                }
                            }
                        }
                        request = {
                            module: name,
                            mode: mode == 'Insert' ? 'NEW' : 'EDIT',
                            ids: rowuniqueids,
                            fields: rowfields,
                            miscfields: miscfields
                        };
                        if ($form.length > 0) {
                            request.miscfields = jQuery.extend({}, request.miscfields, formuniqueids);
                            request.miscfields = jQuery.extend({}, request.miscfields, formfields);
                        }
                    }
                    if (typeof $control.data('beforesave') === 'function') {
                        $control.data('beforesave')(request);
                    }
                    if (typeof controller.apiurl === 'undefined') {
                        mode = 'Save';
                    }
                    FwServices.grid.method(request, name, mode, $control, function (response) {
                        try {
                            var $fields = $tr.find('.field');
                            for (var fieldno = 0; fieldno < $fields.length; fieldno++) {
                                var $field = $fields.eq(fieldno);
                                if (typeof $field.attr('data-formdatafield') !== 'undefined' && typeof response[$field.attr('data-formdatafield')] !== 'undefined') {
                                    $field.attr('data-originalvalue', response[$field.attr('data-formdatafield')]);
                                }
                                if (typeof $field.attr('data-browsedisplayfield') !== 'undefined' && typeof response[$field.attr('data-browsedisplayfield')] !== 'undefined') {
                                    $field.attr('data-originaltext', response[$field.attr('data-browsedisplayfield')]);
                                }
                            }
                            me.setRowViewMode($control, $tr);
                            $control.attr('data-mode', 'VIEW');
                            if (($control.attr('data-type') === 'Grid') && (typeof $control.data('aftersave') === 'function')) {
                                $control.data('aftersave')($control, $tr);
                            }
                            else if (($control.attr('data-type') === 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                                var controller;
                                controller = $control.attr('data-controller');
                                if (typeof window[controller] === 'undefined')
                                    throw 'Missing javascript module: ' + controller;
                                if (typeof window[controller]['afterSave'] === 'function') {
                                    window[controller]['afterSave']($control, $tr);
                                }
                            }
                            if ($control.attr('data-refreshaftersave') === 'true' && (typeof $control.attr('data-autosave') === 'undefined' || $control.attr('data-autosave') === 'true')) {
                                me.search($control)
                                    .then(function () {
                                    resolve();
                                })
                                    .catch(function (reason) {
                                    reject(reason);
                                });
                            }
                            else {
                                resolve();
                            }
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                            reject();
                        }
                    });
                }
            }
            else {
                _this.cancelEditMode($control, $tr);
                resolve();
            }
        });
    };
    FwBrowseClass.prototype.deleteRow = function ($control, $tr) {
        var me = this;
        var rowuniqueids, formuniqueids, name, $form, $confirmation, $ok, $cancel, candelete, miscfields;
        candelete = true;
        miscfields = {};
        if (($control.attr('data-type') === 'Grid') && (typeof $control.data('beforedelete') === 'function')) {
            $control.data('beforedelete')($control, $tr);
        }
        else if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['beforeDelete'] === 'function') {
                candelete = window[controller]['beforeDelete']($control, $tr);
            }
        }
        if (($tr.length == 1) && (candelete)) {
            $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Delete Record?');
            $ok = FwConfirmation.addButton($confirmation, 'OK');
            $cancel = FwConfirmation.addButton($confirmation, 'Cancel');
            $ok.on('click', function () {
                try {
                    me.deleteRecord($control, $tr, true);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    };
    FwBrowseClass.prototype.deleteRecord = function ($control, $tr, refreshAfterDelete) {
        var me = this;
        this.autoSave($control, $tr);
        var miscfields = {};
        var name = $control.attr('data-name');
        var $form = $control.closest('.fwform');
        var rowuniqueids = this.getRowFormUniqueIds($control, $tr);
        var request = {
            module: name,
            ids: rowuniqueids,
            miscfields: miscfields
        };
        if ($form.length > 0) {
            var formuniqueids = ($form.length > 0) ? FwModule.getFormUniqueIds($form) : [];
            request.miscfields = jQuery.extend({}, miscfields, formuniqueids);
        }
        FwServices.grid.method(request, name, 'Delete', $control, function (response) {
            if (($control.attr('data-type') === 'Grid') && (typeof $control.data('afterdelete') === 'function')) {
                $control.data('afterdelete')($control, $tr);
            }
            else if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                var controller = $control.attr('data-controller');
                if (typeof window[controller] === 'undefined')
                    throw 'Missing javascript module: ' + controller;
                if (typeof window[controller]['afterDelete'] === 'function') {
                    window[controller]['afterDelete']($control, $tr);
                }
            }
            if (refreshAfterDelete) {
                me.search($control);
            }
        });
    };
    FwBrowseClass.prototype.addLegend = function ($control, caption, color) {
        var html = [];
        html.push('<div class="legenditem">');
        html.push('  <div class="color" style="background-color:' + color + '"></div>');
        html.push('  <div class="caption">' + caption + '</div>');
        html.push('</div>');
        var htmlString = html.join('\n');
        var $legenditem = jQuery(htmlString);
        var $legend = $control.find('.legend');
        $legend.append($legenditem);
        $legend.show();
    };
    FwBrowseClass.prototype.getGridData = function ($object, request, responseFunc) {
        var webserviceurl, controller, module;
        controller = $object.attr('data-controller');
        module = window[controller].Module;
        request.module = module;
        webserviceurl = 'services.ashx?path=/grid/' + module + '/GetData';
        FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $object);
    };
    FwBrowseClass.prototype.disableGrid = function ($control) {
        $control.attr('data-enabled', 'false');
    };
    FwBrowseClass.prototype.enableGrid = function ($control) {
        $control.attr('data-enabled', 'true');
    };
    FwBrowseClass.prototype.validateRow = function ($control, $tr) {
        var isvalid, $fields;
        isvalid = true;
        $fields = $tr.find('.field');
        $fields.each(function (index) {
            var $field = jQuery(this);
            if ($field.attr('data-formrequired') == 'true') {
                if ($field.find('.value').val() == '') {
                    isvalid = false;
                    $field.addClass('error');
                }
            }
            if (($field.attr('data-formnoduplicate') == 'true') && ($field.hasClass('error'))) {
                isvalid = false;
            }
            if (isvalid) {
                $field.removeClass('error');
            }
        });
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '') && (isvalid)) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['validateRow'] === 'function') {
                isvalid = window[controller]['validateRow']($control, $tr);
            }
        }
        if (!isvalid) {
            FwNotification.renderNotification('ERROR', 'Please correct the error(s) before saving this row.');
        }
        return isvalid;
    };
    FwBrowseClass.prototype.getOptions = function ($control) {
        var $fwformfields, fields, field;
        $fwformfields = $control.find('.advancedoptions .fwformfield');
        fields = {};
        $fwformfields.each(function (index, element) {
            var $fwformfield, dataField, value;
            $fwformfield = jQuery(element);
            dataField = $fwformfield.attr('data-datafield');
            value = FwFormField.getValue2($fwformfield);
            field = {
                datafield: dataField,
                value: value
            };
            fields[dataField] = field;
        });
        return fields;
    };
    FwBrowseClass.prototype.getValidationData = function ($object, request, responseFunc) {
        var webserviceurl, controller, module;
        controller = $object.attr('data-controller');
        module = window[controller].Module;
        request.module = module;
        webserviceurl = 'services.ashx?path=/validation/' + module + '/GetData';
        FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $object);
    };
    FwBrowseClass.prototype.getController = function ($control) {
        var controllername;
        var controller;
        if (typeof $control.attr('data-name') === 'string' && $control.attr('data-name').length > 0) {
            controllername = $control.attr('data-name') + 'Controller';
        }
        else if (typeof $control.attr('data-controller') === 'string' && $control.attr('data-controller').length > 0) {
            controllername = $control.attr('data-controller');
        }
        if (typeof controllername !== 'undefined') {
            controller = window[controllername];
        }
        return controller;
    };
    FwBrowseClass.prototype.isUsingWebApi = function ($control) {
        var useWebApi = false;
        var controller = this.getController($control);
        if (typeof controller.apiurl !== 'undefined') {
            useWebApi = true;
        }
        return useWebApi;
    };
    FwBrowseClass.prototype.loadBrowseFromTemplate = function (modulename) {
        var $control = jQuery(jQuery('#tmpl-modules-' + modulename + 'Browse').html());
        FwBrowse.loadCustomBrowseFields($control, modulename);
        return $control;
    };
    FwBrowseClass.prototype.loadCustomBrowseFields = function ($control, modulename) {
        if (sessionStorage.getItem('customFieldsBrowse') !== null) {
            var customBrowse = JSON.parse(sessionStorage.getItem('customFieldsBrowse'));
            var customBrowseHtml = [];
            if (customBrowse !== 'undefined' && customBrowse.length > 0) {
                for (var i = 0; i < customBrowse.length; i++) {
                    if (modulename === customBrowse[i].moduleName) {
                        customBrowseHtml.push("<div class=\"column\" data-width=\"" + customBrowse[i].browsewidth + "px\" data-visible=\"true\"><div class=\"field\" data-caption=\"" + customBrowse[i].fieldName + "\" data-datafield=\"" + customBrowse[i].fieldName + "\" data-digits=\"" + customBrowse[i].digits + "\" data-datatype=\"" + customBrowse[i].datatype + "\" data-browsedatatype=\"" + customBrowse[i].datatype + "\" data-sort=\"off\"></div></div>");
                    }
                }
            }
            if ($control.find('.spacer').length > 0) {
                jQuery(customBrowseHtml.join('')).insertBefore($control.find('.spacer'));
            }
            else {
                $control.append(customBrowseHtml.join(''));
            }
        }
    };
    FwBrowseClass.prototype.loadGridFromTemplate = function (modulename) {
        var $control = jQuery(jQuery('#tmpl-grids-' + modulename + 'Browse').html());
        return $control;
    };
    FwBrowseClass.prototype.setBeforeSaveCallback = function ($control, callback) {
        $control.data('beforesave', callback);
    };
    FwBrowseClass.prototype.setAfterSaveCallback = function ($control, callback) {
        $control.data('aftersave', callback);
    };
    FwBrowseClass.prototype.setBeforeDeleteCallback = function ($control, callback) {
        $control.data('beforedelete', callback);
    };
    FwBrowseClass.prototype.setAfterDeleteCallback = function ($control, callback) {
        $control.data('afterdelete', callback);
    };
    FwBrowseClass.prototype.setFieldValue = function ($control, $tr, datafield, data) {
        var $field = $tr.find(".field[data-browsedatafield=\"" + datafield + "\"]");
        var datatype = $field.attr('data-browsedatatype');
        if ($tr.hasClass('newmode')) {
            $field.attr('data-originalvalue', data.value);
        }
        window["FwBrowseColumn_" + datatype].setFieldValue($control, $tr, $field, data);
    };
    return FwBrowseClass;
}());
var FwBrowse = new FwBrowseClass();
var FwBrowse_SetFieldValueData = (function () {
    function FwBrowse_SetFieldValueData() {
    }
    return FwBrowse_SetFieldValueData;
}());
//# sourceMappingURL=FwBrowse.js.map