var FwBrowse = {};
//---------------------------------------------------------------------------------
FwBrowse.upgrade = function($control) {
    var properties, i, data_type;
    //sync properties
    data_type = $control.attr('data-type');
    properties = this.getDesignerProperties(data_type);
    for (i = 0; i < properties.length; i++) {
        if (typeof $control.attr(properties[i].attribute) === 'undefined') {
            $control.attr(properties[i].attribute, properties[i].defaultvalue);
        }
    }
}
//---------------------------------------------------------------------------------
FwBrowse.init = function($control) {
    var $columns;
    $control.on('mousewheel', '.txtPageNo', function(event) {
        //console.log(event.deltaX, event.deltaY, event.deltaFactor);
        if (jQuery('.pleasewait').length === 0) {
            if (event.deltaY < 0) {
                $control.find('.btnNextPage').click();
            } else if (event.deltaY > 0) {
                 $control.find('.btnPreviousPage').click();
            }
        }
    });

    //Default column attributes
    $columns = $control.find('div.column');
    $columns.each(function(index, element) {
        $column = jQuery(element);
        $field  = $column.find('div.field');
        if (typeof $field.attr('data-isuniqueid') === 'undefined') {
            $field.attr('data-isuniqueid', 'false');
        }
        if (typeof $field.attr('data-sort') === 'undefined') {
            $field.attr('data-sort', 'off');
        }
        if (typeof $field.attr('data-formreadonly') === 'undefined') {
            $field.attr('data-formreadonly', 'false');
        }
        if (typeof $field.attr('data-cssclass') === 'undefined') {
            $field.attr('data-cssclass', $field.attr('data-browsedatafield'));
        }
    });
    //Default control attributes
    if (typeof ($control.attr('data-mode') !== 'string')) {
        $control.attr('data-mode', 'VIEW');
    }
    if (typeof ($control.attr('data-pageno') !== 'string')) {
        $control.attr('data-pageno', '1');
    }
    if (typeof $control.attr('data-pagesize') !== 'string') {
        if ($control.attr('data-type') === 'Browse') {
            $control.attr('data-pagesize', sessionStorage.getItem('browsedefaultrows'));
        } else {
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
        nodeModule = FwApplicationTree.getNodeByController($control.attr('data-controller'));
        nodeBrowse = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function(node, args2) {
            return (node.properties.nodetype === 'Browse');
        });
        nodeView = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function(node, args2) {
            return (node.properties.nodetype === 'ViewMenuBarButton');
        });
        nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function(node, args2) {
            return (node.properties.nodetype === 'EditMenuBarButton');
        });
        if ((nodeView !== null) && (nodeBrowse !== null)) {
            $control.data('onrowdblclick', function() {
                FwBrowse.openSelectedRow($control);
            });
        }
    }

    $control
        .on('keydown', 'tbody tr', function(e) {
            var code;
            try {
                code = e.keyCode || e.which;
                switch(code) {
                    case 13: //Enter Key
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            e.preventDefault;
                            e.stopPropagation();
                            if (jQuery.inArray(e.target.offsetParent, $control.find('thead tr td .field div.search')) > -1) {
                                FwBrowse.search($control);
                            } else if (($control.attr('data-type') === 'Browse') && ($control.find('tbody tr.selected').length > 0)) {
                                FwBrowse.openSelectedRow($control);
                            } else if (($control.attr('data-type') === 'Validation') && ($control.find('tbody tr.selected').length > 0)) {
                                var $tr;

                                $tr = $control.find('tbody tr.selected');

                                $tr.dblclick();
                            }
                        }
                        break;
                    case 32: //Space Bar Key
                        if (($control.attr('data-type') === 'Validation') && ($control.attr('data-multiselectvalidation') === 'true')) {
                            e.preventDefault;
                            e.stopPropagation();
                            var $tr = $control.find('tbody tr:focus');
                            $tr.click();
                        }
                        break;
                    case 37: //Left Arrow Key
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            FwBrowse.prevPage($control);
                        }
                        return false;
                    case 38: //Up Arrow Key
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            FwBrowse.selectPrevRow($control);
                            return false;
                        }
                    case 39: //Right Arrow Key
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            FwBrowse.nextPage($control);
                            return false;
                        }
                    case 40: //Down Arrow Key
                        if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                            FwBrowse.selectNextRow($control);
                            return false;
                        }
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click focusin', 'tbody tr', function (e) {
            var $tr, $keyfields, compositekey, selectedrows;
            try {
                //e.stopPropagation();
                $tr = jQuery(this);

                if (($control.attr('data-type') === 'Validation') && ($control.attr('data-multiselectvalidation') === 'true')) {
                    if (e.type != 'focusin') {
                        if (!$tr.hasClass('selected')) {
                            if (typeof $control.data('selectedrows') !== 'object') {
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
                        } else if ($tr.hasClass('selected')) {
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
                                delete selectedrows[compositekey]
                            }
                        }
                    }
                } else if (!$tr.hasClass('empty')) {
                    var dontfocus = true;
                    if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                        FwBrowse.selectRow($control, $tr, dontfocus);
                    }
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', 'table thead tr td .field div.search .searchclear', function(e) {
            try {
                var $this;
                $this = jQuery(this);
                if ($this.siblings('input').val() != '') {
                    $this.siblings('input').val('').change();
                    $this.removeClass('visible');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('mouseenter', 'table thead tr td .field div.search', function() {
            try {
                var $this;
                $this = jQuery(this);
                if ($this.find('input').val() != '') {
                    $this.find('.searchclear').addClass('visible');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('mouseleave', 'table thead tr td .field div.search', function() {
            try {
                var $this;
                $this = jQuery(this);
                if (!$this.find('input').is(':focus')) {
                    $this.find('.searchclear').removeClass('visible');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        //.on('focus keyup', 'table thead tr td .field div.search input', function() {
        //    var $this;
        //    $this = jQuery(this);
        //    if ($this.val() != '') {
        //        $this.siblings('.searchclear').addClass('visible');
        //    }
        //})
        .on('blur', 'table thead tr td .field div.search input', function() {
            try {
                var $this;
                $this = jQuery(this);
                $this.siblings('.searchclear').removeClass('visible');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '> .designer > table > thead > tr > td.addcolumn', function() { // Designer: Add a column
            var $thAddColumn;
            try {
                $thAddColumn = jQuery(this);
                FwBrowse.addDesignerColumn($control, $thAddColumn, 'auto', true);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '> .designer > table > thead > tr > td.column > .columnhandle > .delete', function() { // Designer: Delete a column
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
                $trs.each(function(index, tr) {
                    $tr = jQuery(tr);
                    $tds = $tr.children('td');
                    $tds.eq(thIndex).remove();
                    $tds.eq(thAddColumnIndex).remove();
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '> .designer > table > thead > tr > td > .field > .deletefield', function() { // Designer: Click a field's delete button
            var $this, $field;
            try {
                $this = jQuery(this);
                $field = $this.closest('.field');
                $field.remove();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '> .designer > table > thead > tr > td > .addfield', function() {
            var $this, $th;
            try {
                $this = jQuery(this);
                $th = $this.closest('td');
                FwBrowse.addDesignerField($th, 'newfield', 'newfield', 'newfield', 'string');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('dblclick', '> .designer > table > thead > tr > td > .field > .caption', function() { // Designer: double-click to rename column
            var $caption, caption, newCaption, $field;
            try {
                $caption   = jQuery(this);
                caption    = $caption.html();
                newCaption = prompt('Rename field?', caption);
                if (newCaption != null) {
                    $caption.html(newCaption);
                    $field = $caption.closest('.field');
                    $field.attr('data-caption', newCaption);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('change', '> .runtime > table > thead > tr > td > .field > div.search > input', function(e) { // Runtime: Enter a search text into a columns searchbox
            try {
                FwBrowse.search($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        //.on('keydown', '> .runtime > table > thead > tr > td > .field > div.search > input', function(e) {
        //    var code;
        //    try {
        //        code = e.keyCode || e.which;
        //        if(code === 13) { //Enter Key
        //            FwBrowse.search($control);
        //            e.preventDefault;
        //            return false;
        //        }
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //})
        .on('change', '> .runtime > table > tfoot > tr > td > .pager div.pagesize > select.pagesize', function() {
            var $this, pagesize
            try {
                $this = jQuery(this);
                pagesize = $this.val();
                FwBrowse.setPageSize($control, pagesize);
                FwBrowse.search($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '> .runtime > table > tfoot > tr > td > .pager div.btnRefresh', function() {
            try {
                FwBrowse.databind($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('change', '.field[data-formnoduplicate="true"]', function() {
            var $field, value, originalvalue, $form, formuniqueids, formfields, request = {};
            $field = jQuery(this);
            $field.removeClass('error');
            $form         = $control.closest('.fwform');
            formuniqueids = FwModule.getFormUniqueIds($form);
            formfields    = FwModule.getFormFields($form, true);
            value         = $field.find('input.value').val().toUpperCase();
            originalvalue = ((typeof $field.attr('data-originalvalue') != 'undefined') ? $field.attr('data-originalvalue').toUpperCase() : '');
            if ((typeof $control.attr('data-name') !== 'undefined') && (value != originalvalue)) {
                request.module     = $control.attr('data-name');
                request.table      = $field.attr('data-formdatafield').split('.')[0];
                request.fields = {};
                request.fields[$field.attr('data-formdatafield')] = { datafield: $field.attr('data-formdatafield'), value: $field.find('input.value').val(), type: $field.attr('data-formdatatype') };
                request.miscfields = jQuery.extend({}, formuniqueids, formfields);
                FwServices.grid.method(request, $control.attr('data-name'), 'ValidateDuplicate', $control,
                    // onSuccess
                    function(response) {
                        try {
                            if (response.duplicate == true) {
                                $field.addClass('error');
                                FwNotification.renderNotification('ERROR', 'Duplicate ' + $field.attr('data-caption') + '(s) are not allowed.');
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                    // onError
                    , function(errorMessage) {
                        try {
                            FwFunc.showError(errorMessage);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                );
            }
        })
        .on('click', 'tbody .browsecontextmenu', function () {
            try {
                var menuItemCount = 0;
                var $browsecontextmenu = jQuery(this);
                var $tr = $browsecontextmenu.closest('tr');
                //FwBrowse.unselectAllRows($control);
                //FwBrowse.selectRow($control, $tr, true);
                var $contextmenu = FwContextMenu.render('Options', 'bottomleft', $browsecontextmenu);
                //$contextmenu.data('beforedestroy', function () {
                //    FwBrowse.unselectRow($control, $tr);
                //});
                var controller = $control.attr('data-controller');
                if (typeof controller === 'undefined') {
                    throw 'Attribute data-controller is not defined on Browse control.'
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
                                FwBrowse.deleteRow($control, $tr);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        menuItemCount++;
                    }
                }
                if (menuItemCount === 0) {
                    FwContextMenu.destroy($contextmenu);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    //Register Custom Events on grids and validations
    if ((($control.attr('data-type') == 'Grid') || ($control.attr('data-type') == 'Validation')) && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        if (typeof window[controller]['init'] === 'function') {
            window[controller]['init']($control);
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowse.getPageNo = function($control) {
    return parseInt($control.attr('data-pageno')); 
};
//---------------------------------------------------------------------------------
FwBrowse.setPageNo = function($control, pageno) {
    $control.attr('data-pageno', pageno);
};
//---------------------------------------------------------------------------------
FwBrowse.getPageSize = function($control) {
    return parseInt($control.attr('data-pagesize'));
};
//---------------------------------------------------------------------------------
FwBrowse.setPageSize = function($control, pagesize) {
    $control.attr('data-pagesize', pagesize);
};
//---------------------------------------------------------------------------------
FwBrowse.getTotalPages = function($control) {
    return parseInt($control.attr('data-totalpages'));
};
//---------------------------------------------------------------------------------
FwBrowse.setTotalPages = function($control, totalpages) {
    $control.attr('data-totalpages', totalpages);
};
//---------------------------------------------------------------------------------
FwBrowse.getSelectedIndex = function($control) {
    return parseInt($control.attr('data-selectedindex'));
};
//---------------------------------------------------------------------------------
FwBrowse.setSelectedIndex = function($control, selectedindex) {
    $control.attr('data-selectedindex', selectedindex);

    //cancel any unmodified rows in edit mode
    var $trsEditRow = $control.find('tbody tr.editrow');
    $trsEditRow.each(function (index, element) {
        var $trEditRow = jQuery(element);
        if (!FwBrowse.isRowModified($control, $trEditRow)) {
            FwBrowse.cancelEditMode($control, $trEditRow);
        }
    });
};
//---------------------------------------------------------------------------------
FwBrowse.isRowModified = function ($control, $tr) {
    if (!$tr.hasClass('editrow')) {
        return false;
    }
    var $fields = $tr.find('.field[data-formdatafield][data-formreadonly!="true"]');
    var isRowUnmodified = true;
    $fields.each(function (index, element) {
        var $field = jQuery(element);
        var field = {};
        if (typeof window['FwBrowseColumn_' + $field.attr('data-formdatatype')] !== 'undefined') {
            window['FwBrowseColumn_' + $field.attr('data-formdatatype')].getFieldValue($control, $tr, $field, field, $field.attr('data-originalvalue'));
        }
        var $inputValue = $field.find('.value');
        if ($inputValue.length > 0) {
            isRowUnmodified = isRowUnmodified && field.value.toString() === $field.attr('data-originalvalue');
        }
    });
    return !isRowUnmodified;
}
//---------------------------------------------------------------------------------
FwBrowse.getSelectedRow = function($control) {
    return $control.find('tbody tr.selected');
};
//---------------------------------------------------------------------------------
FwBrowse.getSelectedRowIndex = function($control) {
    return FwBrowse.getSelectedRow($control).index();
};
//---------------------------------------------------------------------------------
FwBrowse.getRows = function($control) {
    return $control.find('tbody tr');
};
//---------------------------------------------------------------------------------
FwBrowse.unselectAllRows = function($control) {
    $control.find('tbody tr.selected').removeClass('selected');
    FwBrowse.setSelectedIndex($control, -1);
};
//---------------------------------------------------------------------------------
FwBrowse.unselectRow = function($control, $tr) {
    $tr.removeClass('selected');
    if (FwBrowse.getSelectedIndex($control) === $tr.index()) {
        FwBrowse.setSelectedIndex($control, -1);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.selectRow = function($control, $row, dontfocus) {
    var $prevselectedrow = FwBrowse.getSelectedRow($control);
    $prevselectedrow.removeClass('selected');
    $row.addClass('selected');
    FwBrowse.setSelectedIndex($control, $row.index());
    if (dontfocus !== true) {
        $row.focus();
    }
};
//---------------------------------------------------------------------------------
FwBrowse.selectRowByIndex = function($control, index) {
    var $rows = FwBrowse.getRows($control);
    var $row  = $rows.eq(index);
    FwBrowse.selectRow($control, $row);
    return $row;
};
//---------------------------------------------------------------------------------
FwBrowse.getRowCount = function($control) {
    var $rows    = FwBrowse.getRows($control);
    var rowcount = $rows.length;
    return rowcount;
};
//---------------------------------------------------------------------------------
/** Select the prev row in the browse control. This will load the previous page if necessary.
    @param {object} $control - The browse control
*/
FwBrowse.selectPrevRow = function($control, afterrowselected) {
    var $selectedrow = FwBrowse.getSelectedRow($control);
    var pageno       = FwBrowse.getPageNo($control);
    var pagesize     = FwBrowse.getPageSize($control);
    var rowindex     = FwBrowse.getSelectedRowIndex($control);
    if (rowindex > 0) {
        rowindex = rowindex - 1;
        $selectedrow = FwBrowse.selectRowByIndex($control, rowindex);
        if (typeof afterrowselected === 'function') {
            afterrowselected();
        }
    } else if ((rowindex === 0) && (pageno > 1)) {
        rowindex = pagesize - 1;
        FwBrowse.setSelectedIndex($control, rowindex);
        FwBrowse.addEventHandler($control, 'afterdatabindcallback', function afterdatabindcallback_selectPrevRow() {
            FwBrowse.removeEventHandler($control, 'afterdatabindcallback', '', afterdatabindcallback_selectPrevRow);
            if (typeof afterrowselected === 'function') {
                afterrowselected();
            }
        });
        FwBrowse.prevPage($control);
    }
    return $selectedrow;
};
//---------------------------------------------------------------------------------
/** Select the next row in the browse control.
    @param {object} $control - The browse control
*/
FwBrowse.selectNextRow = function($control, afterrowselected) {
    var $selectedrow = FwBrowse.getSelectedRow($control);
    var pageno       = FwBrowse.getPageNo($control);
    var totalpages   = FwBrowse.getTotalPages($control);
    var rowindex     = FwBrowse.getSelectedRowIndex($control);
    var lastrowindex = $control.find('tbody tr').length - 1;
    if (rowindex < lastrowindex) {
        $selectedrow = $selectedrow.next();
        FwBrowse.selectRow($control, $selectedrow);
        if (typeof afterrowselected === 'function') {
            afterrowselected();
        }
    }
    else if ((rowindex === lastrowindex) && (pageno < totalpages)) {
        FwBrowse.setSelectedIndex($control, 0);
        FwBrowse.addEventHandler($control, 'afterdatabindcallback', function afterdatabindcallback_selectNextRow() {
            FwBrowse.removeEventHandler($control, 'afterdatabindcallback', afterdatabindcallback_selectNextRow);
            if (typeof afterrowselected === 'function') {
                afterrowselected();
            }
        });
        FwBrowse.nextPage($control);
    }
    $currenttr = $control.find('tbody tr.selected');
    return $currenttr;
};
//---------------------------------------------------------------------------------
/** Open the form of the prev row in the browse control.  Supports paging.
    @param {object} $control - The browse control
*/
//---------------------------------------------------------------------------------
FwBrowse.openPrevRow = function($control, $tab, $form) {
    if ((typeof $tab === 'object') && ($tab.length === 1) && (typeof $form === 'object') && ($form.length === 1)) {
        FwModule.closeForm($form, $tab, null, function afterCloseForm() {
            FwBrowse.selectPrevRow($control, function afterrowselected() {
                FwBrowse.openSelectedRow($control);
            });
        });
    }
};
//---------------------------------------------------------------------------------
/** Open the form of the next row in the browse control. Supports paging.
    @param {object} $control - The browse control
*/
//---------------------------------------------------------------------------------
FwBrowse.openNextRow = function($control, $tab, $form) {
    if ((typeof $tab === 'object') && ($tab.length === 1) && (typeof $form === 'object') && ($form.length === 1)) {
        FwModule.closeForm($form, $tab, null, function afterCloseForm() {
            FwBrowse.selectNextRow($control, function afterrowselected() {
                FwBrowse.openSelectedRow($control);
            });
        });
    }
};
//---------------------------------------------------------------------------------
/** Load the prev page of records from the database.
  @param {object} $control - The browse control
*/
FwBrowse.prevPage = function($control) {
    var pageno, $btnPreviousPage;
    $btnPreviousPage = $control.find('.btnPreviousPage');
    if ($btnPreviousPage.attr('data-enabled') === 'true') {
        pageno = FwBrowse.getPageNo($control) - 1;
        pageno = (pageno >= 1) ? pageno : 1;
        FwBrowse.setPageNo($control, pageno);
        FwBrowse.databind($control);
    }
};
//---------------------------------------------------------------------------------
/** Load the next page of records from the database.
  @param {object} $control - The browse control
*/
FwBrowse.nextPage = function($control) {
    var $btnNextPage, pageno, totalpages
    $btnNextPage = $control.find('.btnNextPage');
    if ($btnNextPage.attr('data-enabled') === 'true') {
        pageno     = FwBrowse.getPageNo($control) + 1;
        totalpages = FwBrowse.getTotalPages($control);
        pageno = (pageno <= totalpages) ? pageno : totalpages;
        FwBrowse.setPageNo($control, pageno);
        FwBrowse.databind($control);
    }
};
//---------------------------------------------------------------------------------
/** Opens the selected record into a tab.
    @param {object} $control - The browse control
*/
FwBrowse.openSelectedRow = function($control) {
    var $selectedrow, browseuniqueids, formuniqueids, $fwforms, dataType, $form, issubmodule, nodeModule, nodeBrowse, nodeView, nodeEdit;
    $selectedrow = FwBrowse.getSelectedRow($control);
    //$selectedrow.dblclick();
    dataType = (typeof $control.attr('data-type') === 'string') ? $control.attr('data-type') : '';
    switch (dataType)
    {
        case 'Browse':
            formuniqueids   = FwBrowse.getRowFormUniqueIds($control, $selectedrow);
            browseuniqueids = FwBrowse.getRowBrowseUniqueIds($control, $selectedrow);
            $form           = FwModule.getFormByUniqueIds(formuniqueids);
            if ((typeof $form === 'undefined') || ($form.length == 0)) {
                if (typeof $control.attr('data-controller') === 'undefined') {
                    throw 'FwBrowse: Missing attribute data-controller.  Set this attribute on the browse control to the name of the controller for this browse module.'
                }
                $form = window[$control.attr('data-controller')].loadForm(browseuniqueids);
                issubmodule = $control.closest('.tabpage').hasClass('submodule');
                if (!issubmodule) {
                    FwModule.openModuleTab($form, 'New ' + $form.attr('data-caption'), true, 'FORM', true);
                } else {
                    FwModule.openSubModuleTab($control, $form);
                }
            } else if ($form.length > 0) {
                tabid = $form.closest('div.tabpage').attr('data-tabid');
                jQuery('#' + tabid).click();
            }
            break;
        case 'Grid':
            break;
    }
};
//---------------------------------------------------------------------------------
/** Attach custom event handlers with arrays to the data collection of the browse control.  This allows multiple callback functions for the event. Useful for events like afterdatabinkcallback.
    @param {object} $control - The browse control.
    @param {string} eventName - Then name of the browse control event.
    @param {function} callbackfunction - The callback function to fire when the event occurs.
*/
FwBrowse.addEventHandler = function($control, eventName, callbackfunction) {
    var callbackfunctions = [];
    if (Array.isArray($control.data(eventName))) {
        callbackfunctions = $control.data(eventName);
    }
    callbackfunctions.push(callbackfunction);
    $control.data(eventName, callbackfunctions);
};
//---------------------------------------------------------------------------------
/** Remove custom event handlers from the browse control.
    @param {object} $control - The browse control.
    @param {string} eventName - Then name of the browse control event.
    @param {function} callbackfunction - The callback function to fire when the event occurs.
*/
FwBrowse.removeEventHandler = function($control, eventName, callbackfunction) {
    if (Array.isArray($control.data(eventName))) {
        var callbackfunctions = $control.data(eventName);
        for (var i = 0; i < callbackfunctions.length; i++) {
            if (callbackfunctions[i] === callbackfunction) {
                callbackfunctions.splice(i, 1);
            }
        }
    }
};
//---------------------------------------------------------------------------------
/** Get the relative url of the sort image in the framework.
    @param {string} sort - 'asc', 'desc', or 'off'
*/
FwBrowse.getSortImage = function(sort) {
    var result;
    switch(sort) {
        case 'asc':
            result = 'theme/fwimages/icons/9x5/SortDesc.001.png';
            break;
        case 'desc':
            result = 'theme/fwimages/icons/9x5/SortOff.001.png';
            break;
        case 'off':
        default:
            result = 'theme/fwimages/icons/9x5/SortAsc.001.png';
            break;
    }
};
//---------------------------------------------------------------------------------
/** Sets the pageno to 1 before loading records from the database.
    @param {object} $control - The browse control
*/
FwBrowse.search = function($control) {
    FwBrowse.setPageNo($control, 1);
    FwBrowse.databind($control);
};
//---------------------------------------------------------------------------------
FwBrowse.addDesignerField = function($column, cssclass, caption, datafield, datatype) {
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
    } else {
        $column.append($addfield);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.addDesignerColumn = function($control, $thAddColumn, width, visible) {
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
    $trHead = $control.find('> .designer > table > thead > tr');
    $th = jQuery(htmlTh);
    $th.insertAfter($thAddColumn);
    thAddColumnIndex = $thAddColumn.parent().children().index($thAddColumn);
    $thAddColumn.closest('table').find('> tbody > tr').each(function(index, field) {
        $trBody = jQuery(field);
        $tdAddColumn = $trBody.children('td')[thAddColumnIndex];
        $td = jQuery(htmlTd);
        $td.insertAfter($tdAddColumn);
    });
    FwBrowse.addDesignerField($control, 'newfield', 'newfield', '', 'string');
};
//---------------------------------------------------------------------------------
FwBrowse.getHtmlTag = function(data_type) {
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
//---------------------------------------------------------------------------------
FwBrowse.getDesignerProperties = function(data_type) {
    var properties = [], propId, propClass, propCaption, propDataControl, propDataType, propDataVersion, propDataCaption, propSearchField, propSearchFieldValue, propOrderBy, propHasAdd, propHasEdit, propHasDelete;
    //id
    propId = {
        caption: 'ID'
      , datatype: 'string'
      , attribute: 'id'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    propId.defaultvalue = FwControl.generateControlId('fwbrowse');
    //class
    propClass = {
        caption: 'CSS Class'
      , datatype: 'string'
      , attribute: 'class'
      , defaultvalue: 'fwcontrol fwbrowse'
      , visible: false
      , enabled: false
    };
    //class
    propCaption = {
        caption: 'Caption'
      , datatype: 'string'
      , attribute: 'data-caption'
      , defaultvalue: ''
      , visible: false
      , enabled: false
    };
    //data-control
    propDataControl = {
        caption: 'Control'
      , datatype: 'string'
      , attribute: 'data-control'
      , defaultvalue: 'FwBrowse'
      , visible: true
      , enabled: false
    };
    //data-type
    propDataType = {
        caption: 'Type'
      , datatype: 'Browse'
      , attribute: 'data-type'
      , defaultvalue: data_type
      , visible: true
      , enabled: false
    };
    //data-version
    propDataVersion = {
        caption: 'Version'
      , datatype: 'string'
      , attribute: 'data-version'
      , defaultvalue: '1'
      , visible: false
      , enabled: false
    };
    // data-rendermode
    propRenderMode = {
        caption: 'Render Mode'
      , datatype: 'string'
      , attribute: 'data-rendermode'
      , defaultvalue: 'template'
      , visible: false
      , enabled: false
    };
    propOrderBy = {
        caption: ''
      , datatype: 'string'
      , attribute: 'data-orderby'
      , defaultvalue: ''
      , visible: false
      , enabled: false
    };
    propHasAdd = {
        caption: ''
      , datatype: 'boolean'
      , attribute: 'data-hasadd'
      , defaultvalue: 'true'
      , visible: false
      , enabled: false
    };
    propHasEdit = {
        caption: ''
      , datatype: 'boolean'
      , attribute: 'data-hasedit'
      , defaultvalue: 'true'
      , visible: false
      , enabled: false
    };
    propHasDelete = {
        caption: ''
      , datatype: 'boolean'
      , attribute: 'data-hasdelete'
      , defaultvalue: 'true'
      , visible: false
      , enabled: false
    };
    properties.push(propDataControl);
    properties.push(propDataType);
    properties.push(propId);
    properties.push(propClass);
    properties.push(propCaption);
    properties.push(propDataVersion);
    properties.push(propRenderMode);
    properties.push(propOrderBy);

    switch (data_type) {
        case "Grid":
            properties.push(propHasAdd);
            properties.push(propHasEdit);
            properties.push(propHasDelete);
    }

    return properties;
};
//---------------------------------------------------------------------------------
FwBrowse.renderDesignerHtml = function($control) {
    var html, data_rendermode, $columns;
    data_rendermode = $control.attr('data-rendermode');
    switch(data_rendermode) {
        case 'designer':
        case 'runtime':
            FwBrowse.renderTemplateHtml($control);
            FwBrowse.renderDesignerHtml($control);
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
            $columns.each(function(index, column) {
                var $column, caption, browsedatafield, cssclass, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                $column = jQuery(column);
                width   = $column.attr('data-width');
                visible = $column.attr('data-visible');
                html.push('<td class="column" data-width="' + width + '" data-visible="' + visible + '" style="width:' + width + ';">');
                html.push('<div class="columnhandle" draggable="true">');
                    html.push('<span class="caption">column</span>');
                    html.push('<div class="delete">x</div>');
                html.push('</div>');
                $fields = $column.find('> .field');
                $fields.each(function(index, field) {
                    var $field;
                    $field          = jQuery(field);
                    caption         = $field.attr('data-caption');
                    browsedatafield = $field.attr('data-browsedatafield');
                    cssclass        = $field.attr('data-cssclass');
                    browsedatatype  = $field.attr('data-browsedatatype');
                    formdatafield   = $field.attr('data-formdatafield');
                    formdatatype    = $field.attr('data-datatype');
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
            for (var i = 1; i <= 10; i++) {
                html.push('<tr>');
                html.push('<td class="addcolumn"></td>');
                $columns.each(function(index, column) {
                    var $column, caption, browsedatafield, cssclass, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    $column = jQuery(column);
                    width   = $column.attr('data-width');
                    visible = $column.attr('data-visible');
                    html.push('<td class="column">');
                    $fields = $column.find('> .field');
                    $fields.each(function(index, field) {
                        var $field;
                        $field          = jQuery(field);
                        caption         = $field.attr('data-caption');
                        cssclass        = $field.attr('data-cssclass');
                        browsedatafield = $field.attr('data-browsedatafield');
                        browsedatatype  = $field.attr('data-browsedatatype');
                        browsedatafield = $field.attr('data-formdatafield');
                        browsedatafield = $field.attr('data-formdatatype');
                        html.push('<div class="field ' + cssclass + '">');
                        html.push(browsedatafield + i.toString());
                        html.push('</div>');
                    });
                    html.push('</td>');
                    html.push('<td class="addcolumn"></td>');
                });
                html.push('</tr>');
            }
            html.push('</tbody>');
            //html.push('<tfoot>');
            //html.push('</tfoot>');
            html.push('</table>');
            html.push('</div>');
            html = html.join('');
            $control.html(html);
            $control.attr('data-rendermode', 'designer');
            break;
    }
};
//---------------------------------------------------------------------------------
FwBrowse.renderRuntimeHtml = function($control) {
    var html, data_rendermode, $allfields, data_uniqueidname, colspan, $advancedoptions, $customvalidationbuttons, $columns, $columnoptions, $theadfields;
    data_rendermode = $control.attr('data-rendermode');
    switch(data_rendermode) {
        case 'designer':
        case 'runtime':
            FwBrowse.renderTemplateHtml($control);
            FwBrowse.renderRuntimeHtml($control);
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
                html.push($advancedoptions.wrap('<div/>').parent().html());       //MY 2/26/2015: Hack to get parent .advancedoption div as well as content
            }
            html.push('<table>');
            html.push('<thead>');
            // header row
            html.push('<tr class="fieldnames">');
            if ($control.attr('data-type') === 'Grid') {
                html.push('<td class="column tdselectrow" style="width:20px;"><div class="divselectrow"><input type="checkbox" class="cbselectrow"/><label class="lblselectrow"></label></div></td>');
            }
            $columns.each(function(index, column) {
                var $column, width, visible, $theadfields;
                $column = jQuery(column);
                width   = $column.attr('data-width');
                visible = (typeof $column.attr('data-visible') !== 'undefined') ? ($column.attr('data-visible') === 'true') : true;
                html.push('<td class="column" data-width="' + width + '" data-visible="' + visible + '" style="width:' + width + ';');
                if (!visible) {
                     html.push('display:none;');
                }
                html.push('">');
                $theadfields = $column.find('> .field');
                $theadfields.each(function(index, element) {
                    var $theadfield, $field, caption, sort;
                    $theadfield      = jQuery(element);
                    $field           = $theadfield.clone();
                    $field.empty();
                    sort       = $theadfield.attr('data-sort');
                    caption    = $theadfield.attr('data-caption');
                    html.push($field[0].outerHTML.replace('</div>',''));
                        html.push('<div class="fieldcaption" style="min-width:' + width + ';">');
                        html.push('<div class="caption" title="' + caption + '">' + caption + '</div>');
                            if (sort === 'asc') {
                                html.push('<div class="sort"><i class="material-icons">keyboard_arrow_up</i></div>');
                            } else if (sort === 'desc') {
                                html.push('<div class="sort"><i class="material-icons">keyboard_arrow_down</i></div>');
                            } else {
                                html.push('<div class="sort"><i class="material-icons"></i></div>');
                            }
                            html.push('<div class="columnoptions"></div>');
                        html.push('</div>');
                        html.push('<div class="search"');
                        if ($control.attr('data-showsearch') === 'false') {
                            html.push(' style="display:none;"');
                        } else if ($theadfield.attr('data-showsearch') === 'false') {
                            html.push(' style="visibility:hidden;"');
                        }
                        html.push('>');
                            html.push('<input type="text" placeholder="' + caption + '..." />');
                            html.push('<div class="searchclear" />');
                        html.push('</div>');
                    html.push('</div>');
                });
                html.push('</td>');
            });
            if ($control.attr('data-type') === 'Grid') {
                html.push('<td class="column browsecontextmenucell" style="width:26px;"></td>');
            }
            html.push('</tr>');
            html.push('</thead>');
            html.push('<tbody>');
            // empty body row
            html.push('<tr class="empty">');
            $columns.each(function(index, column) {
                var $column, caption, datafield, cssclass, datatype, width, visible, $fields;
                $column = jQuery(column);
                width   = $column.attr('data-width');
                visible = (typeof $column.attr('data-visible') !== 'undefined') ? ($column.attr('data-visible') === 'true') : true;
                html.push('<td class="column"');
                if (!visible) {
                    html.push(' style="display:none;"');
                }
                html.push('></td>');
            });
            html.push('</tr>');
            html.push('</tbody>');
            
            html.push('<tfoot>');
            colspan = $columns.filter('*[data-visible="true"]').length;
            html.push('<tr class="spacerrow">');
            html.push('<td colspan="' + (colspan + 2) + '">');
            html.push('<div>&nbsp;</div>');
            html.push('</td>');
            html.push('</tr>');

            html.push('<tr class="legendrow" style="display:none;">');
            html.push('<td colspan="' + (colspan + 2) + '">');
            html.push('<div class="legend"></div>');
            html.push('</td>');
            html.push('</tr>');
            
            html.push('<tr class="pagerrow">');
            html.push('<td colspan="' + (colspan + 2) + '">');
            html.push('<div class="pager"></div>');
            html.push('</td>');
            html.push('</tr>');
            html.push('</tfoot>');
            html.push('</table>');
            if ($control.attr('data-type') === 'Validation') {
                html.push('<div class="validationbuttons">');
                    html.push('<div class="fwbrowsebutton btnSelect">Select</div>');
                    html.push('<div class="fwbrowsebutton btnSelectAll" title="The report will run faster if you Select All from this button vs selecting individual rows." style="display:none;">Select All</div>');
                    html.push('<div class="fwbrowsebutton btnClear">Clear</div>');
                    html.push('<div class="fwbrowsebutton btnViewSelection" style="display:none;">View Selection</div>');
                    html.push('<div class="fwbrowsebutton btnCancel">Cancel</div>');
                    $customvalidationbuttons = $control.find('.customvalidationbuttons');
                    if ($customvalidationbuttons.length > 0) {
                        FwControl.renderRuntimeControls($customvalidationbuttons.find('.fwcontrol'));
                        html.push($customvalidationbuttons.html());       //MY 2/26/2015: Hack to get parent .customvalidationbuttons div as well as content
                    }
                html.push('</div>');
            }
            html.push('</div>');
            html = html.join('');
            $control.html(html);
            $control.attr('data-rendermode', 'runtime');

            $control.on('click', 'thead .cbselectrow', function () {
                try {
                    var $this = jQuery(this);
                    $control.find('tbody .cbselectrow').prop('checked', $this.prop('checked'));
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });

            function getcolumnoptionbutton(text, materialicon) {
                var  html;
                html = [];
                html.push('<div class="columnoptions-button">');
                    html.push('<div class="columnoptions-button-icon">' + ((materialicon != '') ? '<i class="material-icons">' + materialicon + '</i>' : '') + '</div>');
                    html.push('<div class="columnoptions-button-text">' + text + '</div>');
                html.push('</div>');

                return jQuery(html.join(''));
            }
            
            $theadfields = $control.find('thead .field');
            $theadfields.each(function(index, element) {
                var $theadfield, $columnoptions, showsort, showsearch;
                $theadfield      = jQuery(element);
                $columnoptions   = $theadfield.find('.columnoptions');
                showsort         = ($theadfield.attr('data-sort')       !== 'disabled');
                showsearch       = ($theadfield.attr('data-showsearch') !== 'false');
                if (showsort) {
                    var $sortascbtn, $sortdescbtn, $sortoffbtn;
                    $sortascbtn = getcolumnoptionbutton('Sort Ascending', '&#xE316;');
                    $sortascbtn.on('click', function () {
                        try {
                            var $this = jQuery(this);
                            var $field = $this.closest('.field'); 
                            $field.attr('data-sort', 'asc');
                            $field.find('.sort .material-icons').html('&#xE316;');
                            FwBrowse.databind($control);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    $columnoptions.append($sortascbtn);

                    $sortdescbtn = getcolumnoptionbutton('Sort Descending', '&#xE313;');
                    $sortdescbtn.on('click', function() {
                        try {
                            var $this = jQuery(this);
                            var $field = $this.closest('.field');
                            $field.attr('data-sort', 'desc');
                            $field.find('.sort .material-icons').html('&#xE313;');
                            FwBrowse.databind($control);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    $columnoptions.append($sortdescbtn);

                    $sortoffbtn = getcolumnoptionbutton('Sort Off', '');
                    $sortoffbtn.on('click', function() {
                        try {
                            var $this = jQuery(this);
                            var $field = $this.closest('.field');
                            var $thead = $field.closest('thead');
                            var $sortedfields = $thead.find('.field[data-sort="asc"],.field[data-sort="desc"]');
                            if ($sortedfields.length > 1) {
                                $field.attr('data-sort', 'off');
                                $field.find('.sort .material-icons').html('');
                                FwBrowse.databind($control);
                            } else {
                                FwNotification.renderNotification('WARNING', 'You must specify a sort order on a least 1 column.');
                            }
                            
                        } catch (ex) {
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
                    $clearbtn.on('click', function() {
                        $theadfield.find('.search input').val('');
                        FwBrowse.databind($control);
                    });
                    $columnoptions.append($clearbtn);

                    $clearallbtn = getcolumnoptionbutton('Clear All Filters', '');
                    $clearallbtn.on('click', function() {
                        $theadfields.find('.search input').val('');
                        FwBrowse.databind($control);
                    });
                    $columnoptions.append($clearallbtn);
                }

                if ((showsearch) || (showsort)) {
                    $theadfield.on('click', '.fieldcaption', function (e) {
                        var maxZIndex, $field;
                        e.preventDefault();
                        if ($control.find('tr.editrow').length == 0) {
                            $field = jQuery(this);
                            if (!$field.hasClass('active')) {
                                maxZIndex      = FwFunc.getMaxZ('*');
                                $field.find('.columnoptions').css('z-index', maxZIndex+1);
                                $field.addClass('active');

                                jQuery(document).one('click', function closeOptions(e) {
                                    if ($field.has(e.target).length === 0) {
                                        $field.removeClass('active');
                                        $field.find('.columnoptions').css('z-index', '0');
                                    } else if ($field.hasClass('active')) {
                                        jQuery(document).one('click', closeOptions);
                                    }
                                });
                            } else {
                                $field.removeClass('active');
                                $field.find('.columnoptions').css('z-index', '0');
                            }
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
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
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
                            $submenuitem.on('click', function () {
                                try {
                                    var $trs = $control.find('.cbselectrow:checked');
                                    if ($trs.length === 0) {
                                        FwFunc.showMessage('Select one or more rows to delete!');
                                    } else {
                                        var $confirmation = FwConfirmation.yesNo('Delete Record' + ($trs.length > 1 ? 's' : ''), 'Delete ' + $trs.length + ' record' + ($trs.length > 1 ? 's' : '') + '?', function onyes() {
                                            $trs.each(function (index, element) {
                                                try {
                                                    var $tr = jQuery(this).closest('tr');
                                                    FwBrowse.deleteRecord($control, $tr);
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
                                        }, function onno() { });
                                    }
                                } catch (ex) {
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
                                            $submenuitem.on('click', FwApplicationTree.clickEvents['{' + gridSubMenuItem.id + '}']);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    FwGridMenu.addCaption($menu, $control.attr('data-caption'));
                
                    var gridNewMenuBarButton    = FwApplicationTree.getChildByType(gridMenuBar, 'NewMenuBarButton');
                    var hasNew = (gridNewMenuBarButton !== null) && (gridNewMenuBarButton.properties.visible === 'T');
                    if (hasNew) {
                        var $new = FwGridMenu.addStandardBtn($menu, FwLanguages.translate('New'), gridNewMenuBarButton.id);
                        $new.attr('data-type', 'NewButton');
                        $new.on('click', function() {
                            var $form, mode;
                            try {
                                $form = $control.closest('.fwform');
                                mode  = $form.attr('data-mode');
                                if ($control.attr('data-enabled') != 'false') {
                                    if ((mode === 'EDIT') || ($new.closest('.fwconfirmation').length > 0)) {
                                        if (typeof $new.data('onclick') === 'function') {
                                            $new.data('onclick')($control);
                                        } else {
                                            FwBrowse.addRowNewMode($control);
                                        }
                                    } else {
                                        FwNotification.renderNotification('WARNING', 'Please save the record before performing this function.');
                                    }
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                
                    var gridEditMenuBarButton   = FwApplicationTree.getChildByType(gridMenuBar, 'EditMenuBarButton');
                    var hasEdit = (gridEditMenuBarButton !== null) && (gridEditMenuBarButton.properties.visible === 'T');
                    //if (hasEdit) {
                    //    var $edit = FwGridMenu.addStandardBtn($menu, FwLanguages.translate('Edit'), gridEditMenuBarButton.id);
                    //    $edit.attr('data-type', 'EditButton');
                    //    $edit.on('click', function() {
                    //        var $tr;
                    //        try {
                    //            if ($control.attr('data-enabled') != 'false') {
                    //                if (!$edit.hasClass('disabled')) {
                    //                    $tr = $control.find('table > tbody > tr.selected');
                    //                    if ($tr.length > 0) {
                    //                        FwBrowse.setRowEditMode($control, $tr);
                    //                    }
                    //                }
                    //            }
                    //        } catch (ex) {
                    //            FwFunc.showError(ex);
                    //        }
                    //    });
                    //}

                    var gridDeleteMenuBarButton = FwApplicationTree.getChildByType(gridMenuBar, 'DeleteMenuBarButton');
                    var hasDelete = (gridDeleteMenuBarButton !== null) && (gridDeleteMenuBarButton.properties.visible === 'T');
                    //if (hasDelete) {
                    //    var $delete = FwGridMenu.addStandardBtn($menu, FwLanguages.translate('Delete'), gridDeleteMenuBarButton.id);
                    //    $delete.attr('data-type', 'DeleteButton');
                    //    $delete.on('click', function() {
                    //        var $tr;
                    //        try {
                    //            if ($control.attr('data-enabled') != 'false') {
                    //                if (!$delete.hasClass('disabled')) {
                    //                    $tr = $control.find('table > tbody > tr.selected');
                    //                    FwBrowse.deleteRow($control, $tr);
                    //                }
                    //            }
                    //        } catch (ex) {
                    //            FwFunc.showError(ex);
                    //        }
                    //    });
                    //}

                    var hasSave = hasNew || hasEdit;
                    if (hasSave) {
                        var $save   = FwGridMenu.addStandardBtn($menu, FwLanguages.translate('Save'));
                        $save.attr('data-type', 'SaveButton');
                        $save.css('display', 'none').on('click', function() {
                            var $tr;
                            try {
                                $tr = $control.find('table > tbody > tr.editrow');
                                FwBrowse.saveRow($control, $tr);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }

                    var hasCancel = hasNew || hasEdit;
                    if (hasCancel) {
                        var $cancel = FwGridMenu.addStandardBtn($menu, 'Cancel');
                        $cancel.attr('data-type', 'CancelButton');
                        $cancel.css('display', 'none').on('click', function() {
                            try {
                                FwBrowse.cancelEditMode($control, $tr);
                            } catch (ex) {
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
    FwBrowse.setGridBrowseMode($control);
};
//---------------------------------------------------------------------------------
FwBrowse.renderTemplateHtml = function($control) {
    var html, data_rendermode, $ths;
    data_rendermode = $control.attr('data-rendermode');
    $control.attr('data-rendermode', 'template');
    switch(data_rendermode) {
        case 'designer':
            html = [];
            $ths = $control.find('> .designer > table > thead > tr > td.column')
            $ths.each(function(index, th) {
                var $th, caption, cssclass, browsedatafield, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                $th     = jQuery(th);
                //width   = $th.attr('data-width');
                width = "auto";
                visible = $th.attr('data-visible');
                html.push('<div class="column" data-width="' + width + '" data-visible="' + visible + '">');
                $fields = $th.find('> .field');
                $fields.each(function(index, field) {
                    var $field;
                    $field    = jQuery(field);
                    caption   = $field.attr('data-caption');
                    datafield = $field.attr('data-datafield');
                    cssclass  = $field.attr('data-cssclass');
                    datatype  = $field.attr('data-datatype');
                    html.push('<div class="field ' + cssclass + '" data-caption="' + caption + '" data-datafield="' + datafield + '" data-cssclass="' + cssclass + '" data-datatype="' + datatype + '"></div>');
                });
                html.push('</div>');
            });
            html = html.join('');
            $control.html(html);
            break;
        case 'runtime':
            html = [];
            $ths = $control.find('> .runtime > table > thead > tr > td.column')
            $ths.each(function(index, th) {
                var $th, caption, cssclass, browsedatafield, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                $th     = jQuery(th);
                //width   = $th.attr('data-width');
                width   = auto;
                visible = $th.attr('data-visible');
                html.push('<div class="column" ' + /*'data-width="' + width*/ + '" data-visible="' + visible + '">');
                $fields = $th.find('> .field');
                $fields.each(function(index, field) {
                    var $field;
                    $field          = jQuery(field);
                    caption         = $field.attr('data-caption');
                    cssclass        = $field.attr('data-cssclass');
                    browsedatafield = $field.attr('data-browsedatafield');
                    browsedatatype  = $field.attr('data-browsedatatype');
                    formdatafield   = $field.attr('data-formdatafield');
                    formdatatype    = $field.attr('data-formdatatype');
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
//---------------------------------------------------------------------------------
FwBrowse.screenload = function($control) {
    
};
//---------------------------------------------------------------------------------
FwBrowse.screenunload = function($control) {
    
};
//---------------------------------------------------------------------------------
FwBrowse.getRequest = function($control) {
    var request, $fields, orderby, $field, $txtSearch, browsedatafield, value, sort, module, controller;
    orderby = [];
    request = {
        module:               '',
        searchfields:         [],
        searchfieldoperators: [],
        searchfieldvalues:    [],
        miscfields:           !$control.closest('.fwform').length ? jQuery([]) : FwModule.getFormUniqueIds($control.closest('.fwform')),
        orderby:              '',
        pageno:               parseInt($control.attr('data-pageno')),
        pagesize:             parseInt($control.attr('data-pagesize')),
        options:              FwBrowse.getOptions($control)
    };
    if ($control.attr('data-type') === 'Grid') {
        request.module = $control.attr('data-name');
    } else if ($control.attr('data-type') === 'Validation') {
        request.module = $control.attr('data-name');
    } else if ($control.attr('data-type') === 'Browse') {
        if (typeof $control.attr('data-name') !== 'undefined') {
            request.module = $control.attr('data-name');
        } else {
            request.module = window[$control.attr('data-controller')].Module;
        }
    }
    controller = window[request.module + 'Controller'];
    if (typeof controller === 'undefined' && ($control.attr('data-type') === 'Grid' || $control.attr('data-type') === 'Browse')) {
        throw module + 'Controller is not defined.'
    }
    $fields = $control.find('> .runtime > table > thead > tr.fieldnames > td.column > div.field');
    $fields.each(function(index, element) {
        $field          = jQuery(element);
        $txtSearch      = $field.find('> div.search > input');
        value           = $txtSearch.val();
        sort            = $field.attr('data-sort');
        browsedatafield = $field.attr('data-browsedatafield');
        if (value.length > 0) {
            request.searchfields.push(browsedatafield);
            request.searchfieldoperators.push('like');
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
                if (typeof controller.apiurl === 'string') {
                    request.searchfields.push('Inactive');
                    request.searchfieldoperators.push('<>');
                    request.searchfieldvalues.push('T');
                } else {
                    request.searchfields.push('inactive');
                    request.searchfieldoperators.push('<>');
                    request.searchfieldvalues.push('T');
                }
                break;
            case 'inactive':
                if (typeof controller.apiurl === 'string') {
                    request.searchfields.push('Inactive');
                    request.searchfieldoperators.push('=');
                    request.searchfieldvalues.push('T');
                } else {
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
        $control.data('ondatabind')(request);  // you can attach an ondatabind function to the browse control if you need to add custom parameters to the request
    }
    return request;
};
//---------------------------------------------------------------------------------
FwBrowse.databind = function($control) {
    var request, caption;
    if ($control.length > 0) {
        request = FwBrowse.getRequest($control);
        if (typeof $control.data('calldatabind') === 'function') {
            $control.data('calldatabind')(request);
        } else {
            if ($control.attr('data-type') === 'Grid') {
                FwServices.grid.method(request, request.module, 'Browse', $control, function(response) {
                    try {
                        var controller = window[request.module + 'Controller'];
                        if (typeof controller === 'undefined') {
                            throw request.module + 'Controller is not defined.'
                        }
                        if (typeof controller.apiurl !== 'undefined') {
                            FwBrowse.databindcallback($control, response);
                        } else {
                            FwBrowse.databindcallback($control, response.browse);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else if ($control.attr('data-type') === 'Validation') {
                FwServices.validation.method(request, request.module, 'Browse', $control, function(response) {
                    try {
                        var controller = window[request.module + 'Controller'];
                        if (typeof controller !== 'undefined' && typeof controller.apiurl !== 'undefined') {
                            FwBrowse.databindcallback($control, response);
                        } else {
                            FwBrowse.databindcallback($control, response.browse);
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } else if ($control.attr('data-type') === 'Browse') {
                FwServices.module.method(request, request.module, 'Browse', $control, function(response) {
                    try {
                        var controller = window[request.module + 'Controller'];
                        if (typeof controller === 'undefined') {
                            throw request.module + 'Controller is not defined.'
                        }
                        if (typeof controller.apiurl !== 'undefined') {
                            FwBrowse.databindcallback($control, response);
                        } else {
                            FwBrowse.databindcallback($control, response.browse);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowse.databindcallback = function($control, dt) {
    var i, $tbody, htmlPager, columnIndex, rowIndex, scrollerCol, dtCol, dtColIndex, dtRow, dtCellValue, rowClass, columns, onrowdblclick, $ths, $pager, pageSize, controlType;
    try {
        //FwOverlay.hideOverlay($control);
        // build up a javascript object that has the column and field structure from the runtime version of the thead content in the runtime rendering of FwBrowse
        // we need this so we can build the body in the same format as the header
        FwBrowse.setGridBrowseMode($control);
        pageSize = FwBrowse.getPageSize($control);
        FwBrowse.setTotalPages($control, dt.TotalPages);
        $ths = $control.find('> .runtime > table > thead > tr.fieldnames > td');
        columns = [];
        $ths.each(function(columnIndex, element) {
            var $th, $fields, column;
            $th            = jQuery(element);
            $fields        = $th.children('.field');
            column         = {};
            column.width   = $th.attr('data-width');
            column.visible = (typeof $th.attr('data-visible') !== 'undefined') ? ($th.attr('data-visible') === 'true'): true;
            column.fields  = [];
            $fields.each(function(fieldIndex, element) {
                var $field, field;
                $field                               = jQuery(element);
                field                                = {};
                // add all the data-attributes to the field object
                for (var attrName in element.attributes) {
                    var attr = element.attributes[attrName];
                    if (/^data-/.test(attr.name)) {
                        var attrName = attr.name.substr(5);
                        field[attrName] = attr.value;
                    }
                }
                column.fields.push(field);
            });
            columns.push(column);
        });
        
        onrowdblclick  = $control.data('onrowdblclick');
        dt.ColumnIndex = {};
        for (i = 0; i < dt.Columns.length; i++) {
            dtCol = dt.Columns[i];
            dt.ColumnIndex[dtCol.DataField] = i;
        }
        $tbody = $control.find('> .runtime > table > tbody');
        $tbody.empty();
        for (rowIndex = 0; rowIndex < dt.Rows.length; rowIndex++) {
            var rowAttributes, $tr, $td;
            
            $tr = jQuery('<tr>');
            if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                $tr.attr('tabindex', '0')
            }
            $tr.addClass('viewmode');
            
            // build each column in a row
            for (columnIndex = 0; columnIndex < columns.length; columnIndex++) {
                scrollerCol = columns[columnIndex];
                // build each field in a column
                $td = jQuery('<td class="column">');
                $td.css({
                    'min-width': scrollerCol.width
                });
                if (!scrollerCol.visible) {
                    $td.hide();
                }

                // format the auto generated grid columns
                if ($control.attr('data-type') === 'Grid') {
                    if (columnIndex === 0) {
                        $td = jQuery('<td class="column tdselectrow"><div class="divselectrow"><input type="checkbox" class="cbselectrow" /><label class="lblselect"></label><div></td>');
                        $tr.append($td);
                    }
                    if (columnIndex === columns.length - 1) {
                        $td = jQuery('<td class="column browsecontextmenucell"><div class="browsecontextmenu"><i class="material-icons">more_vert</i><div></td>');
                        $tr.append($td);
                    }
                }

                // format the fields from the Browse template
                for (var fieldIndex = 0; fieldIndex < scrollerCol.fields.length; fieldIndex++) {
                    var field, cellHtml, displayFieldValue, $field, appdocumentid, documenttypeid, uniqueid1, uniqueid2, uniqueid3, filename, fileextension, miscfield;
                    field       = scrollerCol.fields[fieldIndex];
                    dtColIndex  = dt.ColumnIndex[field.browsedatafield];
                    dtCol       = dt.Columns[dtColIndex];
                    dtRow       = dt.Rows[rowIndex];
                    dtCellValue = dtRow[dtColIndex];
                    $field = jQuery('<div class="field">');
                    if (field.formreadonly !== 'true' && field.browsedatatype !== 'note') {
                        $field.addClass('editablefield');
                    }
                    if (field.isuniqueid) {
                       $field.attr('data-isuniqueid', field.isuniqueid);
                    }
                    // copy the data attributes over from the field to the $field
                    if (typeof dtCellValue !== 'undefined') {
                        $field.attr('data-originalvalue', dtCellValue.toString());
                    } else {
                        $field.attr('data-originalvalue', '');
                    }
                    for (var key in field) {
                       $field.attr('data-' + key, field[key]);
                    }
                    // these need to get moved into databind field in the correct browsecolumn files
                    
                    if ((typeof field.browsecellbackgroundcolorfield !== 'undefined') && (field.browsecellbackgroundcolorfield.length > 0)) {
                        var color, colorColumnIndex;
                        if (typeof dt.ColumnIndex[field.browsecellbackgroundcolorfield] !== 'number') {
                            throw 'FwBrowse.databindcallback: browsecellbackgroundcolorfield: "column ' + field.browsecellbackgroundcolorfield + '" was not returned by the web service.';
                        }
                        color = dtRow[dt.ColumnIndex[field.browsecellbackgroundcolorfield]];
                        $td.css({backgroundColor: color});
                    }
                    if ((typeof field.browsecelltextcolorfield !== 'undefined') && (field.browsecelltextcolorfield.length > 0)) {
                        var color, colorColumnIndex;
                        if (typeof dt.ColumnIndex[field.browsecelltextcolorfield] !== 'number') {
                            throw 'FwBrowse.databindcallback: browsecelltextcolorfield: "column ' + field.browsecelltextcolorfield + '" was not returned by the web service.';
                        }
                        color = dtRow[dt.ColumnIndex[field.browsecelltextcolorfield]];
                        $td.css({color: color});
                    }
                    if ((typeof field.browsecellwhitetextfield !== 'undefined') && (field.browsecellwhitetextfield.length > 0)) {
                        var color, colorColumnIndex;
                        if (typeof dt.ColumnIndex[field.browsecellwhitetextfield] !== 'number') {
                            throw 'FwBrowse.databindcallback: browsecellwhitetextfield: "column ' + field.browsecellwhitetextfield + '" was not returned by the web service.';
                        }
                        if (dtRow[dt.ColumnIndex[field.browsecellwhitetextfield]] === 'T') {
                            $td.css({color: '#ffffff'});
                        }
                    }
                    if (typeof window['FwBrowseColumn_' + field.browsedatatype] === 'object') {
                        if (typeof window['FwBrowseColumn_' + field.browsedatatype].databindfield === 'function') {
                            window['FwBrowseColumn_' + field.browsedatatype].databindfield($control, $field, dt, dtRow, field, $tr);
                        }
                    }
                    FwBrowse.setFieldViewMode($control, $field, $tr);
                    $td.append($field);
                }
                $tr.append($td)
            }
            if ((typeof dt.ColumnIndex['inactive'] === 'number') && (dt.Rows[rowIndex][dt.ColumnIndex['inactive']] === 'T')) {
                $tr.addClass('inactive');
            }
            else if ((typeof dt.ColumnIndex['Inactive'] === 'number') && (dt.Rows[rowIndex][dt.ColumnIndex['Inactive']] === true)) {  //jh 10/02/2017 - to work with new API 
                $tr.addClass('inactive');
            }
            $tbody.append($tr);
        }

        if ($control.attr('data-type') === 'Grid') {
            var $trs = $control.find('tbody tr');
            //$trs.on('blur', '.value', function (event) {
            //    try {
            //        var $tr = jQuery(event.delegateTarget);
            //        $control.data('blurredFrom', event.delegateTarget);
            //        $control.data('delayedFn', setTimeout(function () {
            //            try {
            //                //console.log('Blurred');
            //                FwBrowse.saveRow($control, $tr);
            //            } catch (ex) {
            //                FwFunc.showError(ex);
            //            }
            //        }, 0));
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //});
            //$trs.on('focus', '.value', function (event) {
            //    try {
            //        if ($control.data('blurredFrom') === event.delegateTarget) {
            //            clearTimeout($control.data('delayedFn'));
            //        }
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //});
            // put a Grid row into edit mode when a user clicks on a table cell

            $trs
                .on('click', function (e) {
                    try {
                        var $td = jQuery(this);
                        var $tr = $td.closest('tr');
                        if (!$tr.hasClass('selected')) {
                            FwBrowse.selectRow($control, $tr, true);
                            if (typeof $control.data('onselectedrowchanged') === 'function') {
                                $control.data('onselectedrowchanged')($control, $tr);
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '.editablefield', function (e) {
                    try {
                        var $td = jQuery(this);
                        var $tr = $td.closest('tr');
                        if (!$tr.hasClass('selected')) {
                            FwBrowse.selectRow($control, $tr, true);
                            if (typeof $control.data('onselectedrowchanged') === 'function') {
                                $control.data('onselectedrowchanged')($control, $tr);
                            }
                        }
                        if (!$tr.hasClass('empty')) {
                            if ($control.attr('data-type') === 'Grid' && !$tr.hasClass('editmode')) {
                                FwBrowse.setRowEditMode($control, $tr);
                                $td.find('.value').focus();
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
            //$trs.on('focusout', function (e) {
            //    try {

            //        var $tr = jQuery(this);
            //        FwBrowse.saveRow($control, $tr);
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //});
        }

        // set the spacer row height;
        if (pageSize <= 15) {
            $control.find('> .runtime > table > tfoot tr.spacerrow > td > div').height(25 * (pageSize - dt.Rows.length));
        } else {
            $control.find('> .runtime > table > tfoot tr.spacerrow > td > div').height(25 * (15 - dt.Rows.length));
        }

        // build pager
        controlType = $control.attr('data-type');
        htmlPager = [];
        var rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
        var rownoend   = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
        switch(controlType) {
            case 'Browse':
                htmlPager.push('<div class="col1" style="width:33%;overflow:hidden;float:left;">');
                    htmlPager.push('<div class="btnRefresh" title="Refresh" tabindex="0">');
                        htmlPager.push('<i class="material-icons">&#xE5D5;</i>');
                    htmlPager.push('</div>');
                    if ((rownoend === 0) && (dt.TotalRows === 0)) {
                        htmlPager.push('<div class="count">' + dt.TotalRows + ' rows</div>');
                    } else {
                        if (dt.TotalPages == 1) {
                            htmlPager.push('<div class="count">' + dt.TotalRows + ' rows</div>');
                        } else {
                            htmlPager.push('<div class="count">' + rownostart + ' to ' + rownoend  + ' of ' + dt.TotalRows + ' rows</div>');
                        }
                    }
                htmlPager.push('</div>');
                htmlPager.push('<div class="col2" style="width:34%;overflow:hidden;float:left;height:32px;text-align:center;">');
                    if (dt.TotalPages > 1) {
                        htmlPager.push('<div class="buttons">');
                            if ((pageSize > 0) && (dt.PageNo > 1)) {
                                htmlPager.push('<div tabindex="0" class="button btnFirstPage" data-enabled="true" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                                htmlPager.push('<div tabindex="0" class="button btnPreviousPage" data-enabled="true" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                            } else {
                                htmlPager.push('<div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                                htmlPager.push('<div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                            }
                            htmlPager.push('<div class="page">');
                                if (dt.TotalPages > 0) {
                                    htmlPager.push('<input class="txtPageNo" type="text" value="' + dt.PageNo + '" />');
                                } else {
                                    htmlPager.push('<input class="txtPageNo" type="text" value="0" />');
                                }
                                htmlPager.push('<div class="of">of</div>');
                                htmlPager.push('<div class="txtTotalPages">' + dt.TotalPages + '</div>');
                            htmlPager.push('</div>');
                            if ((pageSize > 0) &&(dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                                htmlPager.push('<div class="button btnNextPage" data-enabled="true" title="Next" alt="Next"><i class="material-icons">&#xE5CC;</i></div>');
                                htmlPager.push('<div class="button btnLastPage" data-enabled="true" title="Last" alt="Last"><i class="material-icons">&#xE5DD;</i></div>');
                            } else {
                                htmlPager.push('<div class="button btnNextPage" disabled="disabled" data-enabled="false" title="Next" alt="Next"><i class="material-icons">&#xE5CC;</i></div>');
                                htmlPager.push('<div class="button btnLastPage" disabled="disabled" data-enabled="false" title="Last" alt="Last"><i class="material-icons">&#xE5DD;</i></div>');
                            }
                        htmlPager.push('</div>');
                    }
                htmlPager.push('</div>');
                htmlPager.push('<div class="col3" style="width:33%;overflow:hidden;float:left;">');
                    htmlPager.push('<div class="pagesize">');
                        htmlPager.push('<select class="pagesize">');
                            htmlPager.push('<option value="5">5</option>');
                            htmlPager.push('<option value="10">10</option>');
                            htmlPager.push('<option value="15">15</option>');
                            htmlPager.push('<option value="20">20</option>');
                            htmlPager.push('<option value="25">25</option>');
                            htmlPager.push('<option value="30">30</option>');
                            htmlPager.push('<option value="35">35</option>');
                            htmlPager.push('<option value="40">40</option>');
                            htmlPager.push('<option value="45">45</option>');
                            htmlPager.push('<option value="50">50</option>');
                            htmlPager.push('<option value="100">100</option>');
                            htmlPager.push('<option value="200">200</option>');
                            htmlPager.push('<option value="500">500</option>');
                            htmlPager.push('<option value="1000">1000</option>');
                            //htmlPager.push('<option value="0">All</option>');
                        htmlPager.push('</select>');
                        htmlPager.push('<span class="caption">rows per page</span>');
                    htmlPager.push('</div>');
                htmlPager.push('</div>');
                break;
            case 'Grid':
                htmlPager.push('<div class="btnRefresh" title="Refresh" tabindex="0">');
                    htmlPager.push('<i class="material-icons">&#xE5D5;</i>');
                htmlPager.push('</div>');
            case 'Validation':
                if (dt.TotalPages > 1) {
                    htmlPager.push('<div class="buttons" style="float:left;">');
                        if ((pageSize > 0) && (dt.PageNo > 1)) {
                            htmlPager.push('<div tabindex="0" class="button btnFirstPage" data-enabled="true" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                            htmlPager.push('<div tabindex="0" class="button btnPreviousPage" data-enabled="true" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                        } else {
                            htmlPager.push('<div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                            htmlPager.push('<div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                        }
                        htmlPager.push('<input class="txtPageNo" style="display:none;" type="text" value="' + dt.PageNo + '"/>');
                        htmlPager.push('<span class="of" style="display:none;"> of </span>');
                        htmlPager.push('<span class="txtTotalPages" style="display:none;">' + dt.TotalPages + '</span>');
                        if ((pageSize > 0) &&(dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                            htmlPager.push('<div class="button btnNextPage" data-enabled="true" title="Next" alt="Next"><i class="material-icons">&#xE5CC;</i></div>');
                            htmlPager.push('<div class="button btnLastPage" data-enabled="true" title="Last" alt="Last"><i class="material-icons">&#xE5DD;</i></div>');
                        } else {
                            htmlPager.push('<div class="button btnNextPage" disabled="disabled" data-enabled="false" title="Next" alt="Next"><i class="material-icons">&#xE5CC;</i></div>');
                            htmlPager.push('<div class="button btnLastPage" disabled="disabled" data-enabled="false" title="Last" alt="Last"><i class="material-icons">&#xE5DD;</i></div>');
                        }
                    htmlPager.push('</div>');
                }
                htmlPager.push('<div class="count">' + dt.TotalRows + ' row(s)</div>');
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
        htmlPager = htmlPager.join('');
        $pager = $control.find('> .runtime > table > tfoot > tr > td > .pager');
        $pager.html(htmlPager);
        $pager.find('select.pagesize').val($control.attr('data-pagesize'));
        $pager.find('select.activeinactiveview').val($control.attr('data-activeinactiveview'));
        $pager.show();

        if ((typeof onrowdblclick !== 'undefined') && ($control.attr('data-multiselectvalidation') !== 'true')) {
            $control.find('> .runtime > table > tbody > tr').on('dblclick', onrowdblclick);
        }
        $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .btnFirstPage')
            .on('click', function() {
                var pageno, $thisbtn;
                try {
                    $btnFirstPage = jQuery(this);
                    if ($btnFirstPage.attr('data-enabled') === 'true') {
                        $control.attr('data-pageno', '1');
                        FwBrowse.databind($control);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
        $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .btnPreviousPage')
            .on('click', function() {
                try {
                    FwBrowse.prevPage($control);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
        $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .txtPageNo')
            .on('change', function() {
                var pageno, originalpageno, originalpagenoStr, $txtPageNo, totalPages;
                try {
                    $txtPageNo = jQuery(this);
                    originalpagenoStr = $txtPageNo.val();
                    if (!isNaN(originalpagenoStr)) {
                        pageno = parseInt(originalpagenoStr);
                        originalpageno = pageno;
                        totalPages = parseInt($control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .txtTotalPages').html());
                        pageno = (pageno >= 1) ? pageno : 1;
                        pageno = (pageno <= totalPages) ? pageno : totalPages;
                        if (pageno === originalpageno) {
                            FwBrowse.setPageNo($control, pageno);
                            FwBrowse.databind($control);
                        } else {
                            $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .txtTotalPages').val(pageno);
                        }
                    } else {

                    }
                } catch(ex) {
                    $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .txtTotalPages').val(originalpagenoStr);
                    FwFunc.showError(ex);
                }
            })
        ;
        $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .btnNextPage')
            .on('click', function() {
                try {
                    FwBrowse.nextPage($control);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
        $control.find('> .runtime > table > tfoot > tr > td > .pager div.buttons .btnLastPage')
            .on('click', function() {
                var pageno, $thisbtn;
                try {
                    $btnLastPage = jQuery(this);
                    if ($btnLastPage.attr('data-enabled') === 'true') {
                        pageno = FwBrowse.getTotalPages($control);
                        FwBrowse.setPageNo($control, pageno);
                        FwBrowse.databind($control);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
        $control.find('> .runtime .pager select.activeinactiveview')
            .on('change', function() {
                var $selectActiveInactiveView, view;
                try {
                    $selectActiveInactiveView = jQuery(this);
                    view = $selectActiveInactiveView.val();
                    $control.attr('data-activeinactiveview', view);
                    FwBrowse.search($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
        if ((typeof $control.attr('data-type') === 'string') && ($control.attr('data-type') === 'Validation')) {
            FwValidation.validateSearchCallback($control);
        }

        setTimeout(function() {
            var selectedindex = FwBrowse.getSelectedIndex($control);
            var rowcount = FwBrowse.getRowCount($control);
            if (rowcount > FwBrowse.getSelectedIndex($control) && selectedindex !== -1) {
                FwBrowse.selectRowByIndex($control, selectedindex);
            } else if (rowcount < selectedindex) {
                var lastrowindex = rowcount - 1;
                FwBrowse.selectRowByIndex($control, lastrowindex);
            }
            if (typeof $control.data('afterdatabindcallback') === 'function') {
                $control.data('afterdatabindcallback')($control, dt);
            } else if (Array.isArray($control.data('afterdatabindcallback'))) {
                var afterdatabindcallbacks = $control.data('afterdatabindcallback');
                for (var i = 0; i < afterdatabindcallbacks.length; i++) {
                    afterdatabindcallbacks[i]($control, dt);
                }
            }
        }, 250);
    } catch(ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.setGridBrowseMode = function($control) {
    var $table, $trNewMode, $trEditMode;
    $control.attr('data-mode', 'VIEW');
    $table = $control.find('table');
    $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').show();
    $control.find('.gridmenu .buttonbar div[data-type="EditButton"]').show();
    $control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').show();
    $control.find('.gridmenu .buttonbar div[data-type="SaveButton"]').hide();
    $control.find('.gridmenu .buttonbar div[data-type="CancelButton"]').hide();

    // remove any rows in new mode
    $trNewMode = $table.find('> tbody > tr.newmode');
    $trNewMode.remove();

    // set any rows in edit mode back to view mode
    $trEditMode = $table.find('> tbody > tr.editmode');
    if ($trEditMode.length > 0) {
        FwBrowse.setRowViewMode($control, $trEditMode);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.addRowNewMode = function($control) {
    var $table;
    $table = $control.find('> .runtime > table');
    if ($table.find('> tbody > tr.editrow.newmode').length === 0) {
        $tr = jQuery('<tr class="editrow newmode">');
        $tbody = $table.find('> tbody');
        $tbody.prepend($tr);
        FwBrowse.setRowNewMode($control, $tr);
        FwBrowse.addSaveAndCancelButtonToRow($control, $tr);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.setRowNewMode = function($control, $tr) {
    var $theadtds, $table, $tr, $inputs;
    $control.attr('data-mode', 'EDIT');
    $table = $tr.closest('table');
    $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
    $control.find('.gridmenu .buttonbar div[data-type="EditButton"]').hide();
    $control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').hide();
    //$control.find('.gridmenu .buttonbar div[data-type="SaveButton"]').show();
    //$control.find('.gridmenu .buttonbar div[data-type="CancelButton"]').show();
    $theadtds = $table.find('> thead > tr.fieldnames > td.column');
    $theadtds.each(function(index, element) {
        var $theadtd, $td, width, $fields;
        $theadtd = jQuery(element);
        $td = $theadtd.clone().empty();
        $theadtd.attr('data-width')
        $td.css('width', width);
        $tr.append($td);
        $theadfields = $theadtd.children('.field');
        $theadfields.each(function(index, element) {
            var $theadfield, $field, $field_newmode, formdatatype;
            $theadfield    = jQuery(element);
            $field         = $theadfield.clone().empty();
            $td.append($field);
            if ($field.attr('data-formreadonly') === 'true') {
                FwBrowse.setFieldViewMode($control, $field, $tr);
            } else {
                FwBrowse.setFieldEditMode($control, $field, $tr);
            }
        });
    });
    $inputs = $tr.find('input[type!="hidden"]:visible,select:visible,textarea:visible');
    if ($inputs.length > 0) {
        $inputs.eq(0).select();
    }

    // Applies custom defaults to a grid control
    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        if (typeof window[controller]['onRowNewMode'] === 'function') {
            window[controller]['onRowNewMode']($control, $tr);
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowse.setRowViewMode = function($control, $tr) {
    var $trEditModeRows = $control.find('tbody tr.editmode');
    $tr.find('.divsaverow').remove();
    $tr.find('.divcancelsaverow').remove();
    $tr.removeClass('editmode').removeClass('editrow').addClass('viewmode');
    var $table = $tr.closest('table');
    //$control.find('.gridmenu .buttonbar div[data-type="EditButton"]').show();
    //$control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').show();
    //$control.find('.gridmenu .buttonbar div[data-type="SaveButton"]').hide();
    //$control.find('.gridmenu .buttonbar div[data-type="CancelButton"]').hide();
    $tr.find('> td > .field').each(function(index, field) {
        var $field, html;
        $field = jQuery(field);
        FwBrowse.setFieldViewMode($control, $field, $tr);
    });
    
    if ($trEditModeRows.length <= 1) {
        $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').show();
        $control.find('tbody .divselectrow').show();
        $control.find('tbody .browsecontextmenu').show();
    }
};
//---------------------------------------------------------------------------------
FwBrowse.setFieldViewMode = function($control, $field, $tr) {
    var browsedatatype = (typeof $field.attr('data-browsedatatype') === 'string') ? $field.attr('data-browsedatatype') : '';
    //var originalvalue  = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    //var originaltext   = (typeof $field.attr('data-originaltext')   === 'string') ? $field.attr('data-originaltext') : '';
    //var appdocumentid  = (typeof $field.attr('data-appdocumentid')  === 'string') ? $field.attr('data-appdocumentid') : '';
    //var appimageid     = originalvalue;
    //var filename       = (typeof $field.attr('data-filename')       === 'string') ? $field.attr('data-filename') : '';
    //var fileextension  = (typeof $field.attr('data-fileextension')  === 'string') ? $field.attr('data-fileextension').toLowerCase() : '';
    
    var html = [];
    if (typeof window['FwBrowseColumn_' + browsedatatype] === 'object') {
        if (typeof window['FwBrowseColumn_' + browsedatatype].setFieldViewMode === 'function') {
            window['FwBrowseColumn_' + browsedatatype].setFieldViewMode($control, $field, $tr, html);
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowse.cancelEditMode = function ($control, $tr) {
    var $inputFile;
    $inputFile = $tr.find('input[type="file"]');
    if (($inputFile.length > 0) && ($inputFile.val().length > 0)) {
        FwBrowse.search($control);
    } else {
        if ($tr.hasClass('newmode')) {
            $tr.remove();
        }

        //hide save button
        var $tdselectrow = $tr.find('.tdselectrow');
        $tdselectrow.find('.divsaverow').remove();
        //$tdselectrow.find('.divselectrow').show();

        //hide cancel button
        var $browsecontextmenucell = $tr.find('.browsecontextmenucell');
        $browsecontextmenucell.find('.divcancelsaverow').remove();
        //$browsecontextmenucell.find('.browsecontextmenu').show();

        FwBrowse.setRowViewMode($control, $tr);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.setRowEditMode = function ($control, $tr) {
    var $table, $inputs;
    $control.attr('data-mode', 'EDIT');
    $tr.removeClass('viewmode').addClass('editmode').addClass('editrow');
    $table = $tr.closest('table');
    $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
    $control.find('.gridmenu .buttonbar div[data-type="EditButton"]').hide();
    $control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').hide();
    //$control.find('.gridmenu .buttonbar div[data-type="SaveButton"]').show();
    //$control.find('.gridmenu .buttonbar div[data-type="CancelButton"]').show();

    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        if (typeof window[controller]['beforeRowEditMode'] === 'function') {
            window[controller]['beforeRowEditMode']($control, $tr);
        }
    }

    $tr.find('> td > .field').each(function(index, element) {
        var $field;
        $field = jQuery(element);
        if ($field.attr('data-formreadonly') === 'true') {
            FwBrowse.setFieldViewMode($control, $field, $tr);
        } else {
            FwBrowse.setFieldEditMode($control, $field, $tr);
        }
    });
    $inputs = $tr.find('input[type!="hidden"]:visible,select:visible,textarea:visible');
    if ($inputs.length > 0) {
        $inputs.eq(0).select();
    }

    FwBrowse.addSaveAndCancelButtonToRow($control, $tr);

    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        if (typeof window[controller]['afterRowEditMode'] === 'function') {
            window[controller]['afterRowEditMode']($control, $tr);
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowse.addSaveAndCancelButtonToRow = function ($control, $tr) {
    // add the save button
    var $browsecontextmenucell = $tr.find('.browsecontextmenucell');
    $tr.closest('tbody').find('.divselectrow').hide();
    var $divsaverow = jQuery('<div class="divsaverow"><i class="material-icons">&#xE161;</i></div>'); //save
    $divsaverow.on('click', function () {
        try {
            var $this = jQuery(this);
            var $tr = $this.closest('tr');
            FwBrowse.saveRow($control, $tr);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });
    $browsecontextmenucell.append($divsaverow); 

    // add the cancel button
    var $tdselectrow = $tr.find('.tdselectrow');
    $tr.closest('tbody').find('.browsecontextmenu').hide();
    var $divcancelsaverow = jQuery('<div class="divcancelsaverow"><i class="material-icons">&#xE5C9;</i></div>'); //cancel
    $divcancelsaverow.on('click', function () {
        try {
            var $this = jQuery(this);
            var $tr = $this.closest('tr');
            FwBrowse.cancelEditMode($control, $tr);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });
    $tdselectrow.append($divcancelsaverow); 
};
//---------------------------------------------------------------------------------
FwBrowse.setFieldEditMode = function($control, $field, $tr) {
    var html, formdatatype/*, originalvalue, originaltext, appdocumentid, documenttypeid, uniqueid1, uniqueid2, appimageid, formmaxlength*/;

    //originalvalue  = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    //originaltext   = (typeof $field.attr('data-originaltext')   === 'string') ? $field.attr('data-originaltext') : '';
    //appdocumentid  = (typeof $field.attr('data-appdocumentid')  === 'string') ? $field.attr('data-appdocumentid') : '';
    //documenttypeid = (typeof $field.attr('data-documenttypeid') === 'string') ? $field.attr('data-documenttypeid') : '';
    //uniqueid1      = (typeof $field.attr('data-uniqueid1')      === 'string') ? $field.attr('data-uniqueid1') : '';
    //uniqueid2      = (typeof $field.attr('data-uniqueid2')      === 'string') ? $field.attr('data-uniqueid2') : '';
    //appimageid     = originalvalue;
    //filename       = (typeof $field.attr('data-filename')       === 'string') ? $field.attr('data-filename') : '';
    //formmaxlength  = (typeof $field.attr('data-formmaxlength')  === 'string') ? $field.attr('data-formmaxlength') : '';
    formdatatype   = (typeof $field.attr('data-formdatatype')   === 'string') ? $field.attr('data-formdatatype') : '';
    html = [];
    
    if (typeof window['FwBrowseColumn_' + formdatatype] === 'object') {
        if (typeof window['FwBrowseColumn_' + formdatatype].setFieldEditMode === 'function') {
            window['FwBrowseColumn_' + formdatatype].setFieldEditMode($control, $field, $tr, html);
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowse.appdocumentimageLoadFile = function($control, $field, file) {
    var $file, file, reader, filename;
    try {
        reader = new FileReader();
        reader.onloadend = function () {
            $field.data('filedataurl', reader.result);
            $field.attr('data-filepath', file.name);
            if (reader.result.indexOf('data:application/pdf;') == 0) {
                $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-pdf.png');
            } else if (reader.result.indexOf('data:image/') == 0) {
                $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-image.png');
            } else if ((reader.result.indexOf('data:application/vnd.ms-excel;') == 0) || (reader.result.indexOf('data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;') == 0)) {
                $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-spreadsheet.png');
            } else if (((reader.result.indexOf('data:application/msword;') == 0)) || (reader.result.indexOf('data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;') == 0)) {
                $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-document.png');
            } else {
                $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-generic.png');
            }
        }
        // this actually happens first, then when the file is done loading, reader.onloadend fires
        if (file) {
            reader.readAsDataURL(file);
        }
    } catch(ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
FwBrowse.getRowBrowseUniqueIds = function($control, $tr) {
    var uniqueids, $uniqueidfields;
    uniqueids = {};
    $uniqueidfields = $tr.find('> td.column > div.field[data-isuniqueid="true"]');
    $uniqueidfields.each(function(index, element) {
        var $field, browsedatafield, originalvalue;
        $field          = jQuery(element);
        browsedatafield = $field.attr('data-browsedatafield');
        originalvalue   = $field.attr('data-originalvalue');
        uniqueids[browsedatafield] = originalvalue;
    });
    return uniqueids;
};
//---------------------------------------------------------------------------------
FwBrowse.getRowFormUniqueIds = function($control, $tr) {
    var uniqueids, uniqueid, $uniqueidfields;
    uniqueids = {};
    $uniqueidfields = $tr.find('> td.column > div.field[data-isuniqueid="true"]');
    $uniqueidfields.each(function(index, element) {
        var $field, formdatafield, formdatatype, value, originalvalue;
        $field        = jQuery(element);
        formdatafield = $field.attr('data-formdatafield');
        formdatatype  = $field.attr('data-formdatatype');
        originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        uniqueid = {
            datafield: formdatafield,
            value: originalvalue
        };
        // provides an opportunity to override the originalvalue for the uniqueid
        if (typeof window['FwBrowseColumn_' + formdatatype] === 'object') {
            if (typeof window['FwBrowseColumn_' + formdatatype].getFieldUniqueId === 'function') {
                window['FwBrowseColumn_' + formdatatype].getFieldUniqueId($control, $tr, $field, uniqueid, originalvalue);
            }
        }
        uniqueids[formdatafield] = uniqueid;
    });
    return uniqueids;
};
//---------------------------------------------------------------------------------
FwBrowse.getRowFormDataFields = function($control, $tr, getmiscfields) {
    var fields, field, $fields;
    fields = {};
    if (!getmiscfields) {
        $fields = $tr.find('> td.column > div.field[data-formdatafield][data-formdatafield!=""]');
    } else {
        $fields = $tr.find('> td.column > div.field[data-miscfield]');
    }
    $fields.each(function(index, element) {
        var $field, formdatafield, originalvalue, miscfield;
        $field        = jQuery(element);
        formdatafield = (typeof $field.attr('data-formdatafield') === 'string') ? $field.attr('data-formdatafield') : '';
        formdatatype  = (typeof $field.attr('data-formdatatype')  === 'string') ? $field.attr('data-formdatatype')  : '';
        originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        miscfield     = (typeof $field.attr('data-miscfield')     === 'string') ? $field.attr('data-miscfield')     : '';
        field = {
            datafield: formdatafield,
            value: originalvalue
        };
        if (typeof window['FwBrowseColumn_' + formdatatype] === 'object') {
            if (typeof window['FwBrowseColumn_' + formdatatype].getFieldValue === 'function') {
                window['FwBrowseColumn_' + formdatatype].getFieldValue($control, $tr, $field, field, originalvalue);
            }
        }
        if (!getmiscfields) {
            fields[formdatafield] = field;
        } else {
            fields[miscfield] = field;
        }
    });
    return fields;
};
//----------------------------------------------------------------------------------------------
FwBrowse.getWebApiRowFields = function($control, $tr) {
    var fields = {};
    var $fields = $tr.find('> td.column > div.field[data-formdatafield][data-formdatafield!=""]');
    $fields.each(function(index, element) {
        var $field        = jQuery(element);
        var formdatafield = (typeof $field.attr('data-formdatafield') === 'string') ? $field.attr('data-formdatafield') : '';
        var formdatatype  = (typeof $field.attr('data-formdatatype')  === 'string') ? $field.attr('data-formdatatype')  : '';
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var field = {
            datafield: formdatafield,
            value: originalvalue
        };
        if (typeof window['FwBrowseColumn_' + formdatatype] === 'object') {
            if (typeof window['FwBrowseColumn_' + formdatatype].getFieldValue === 'function') {
                window['FwBrowseColumn_' + formdatatype].getFieldValue($control, $tr, $field, field, originalvalue);
            }
        }
        fields[formdatafield] = field.value;
    });
    return fields;
};
//----------------------------------------------------------------------------------------------
FwBrowse.saveRow = function ($control, $tr) {
    var fields, rowuniqueids, rowfields, formuniqueids, formfields, mode, name, $form, isvalid = true, miscfields;
    
    var name = $control.attr('data-name');
    var mode = $tr.hasClass('newmode') ? 'Insert' : $tr.hasClass('editmode') ? 'Update' : '';
    if (mode === '') throw 'FwBrowse.saveRow: Invalid mode';
    isvalid = FwBrowse.validateRow($control, $tr);
    if (isvalid) {
        var isUsingWebApi = FwBrowse.isUsingWebApi($control);
        var request;
        $form = $control.closest('.fwform');
        if (isUsingWebApi) {
            // set request for web api
            var allparentformfields = FwModule.getWebApiFields($form, true);
            var parentformfields = {};
            var whitelistedFields = (typeof $control.attr('data-parentformdatafields') !== 'undefined') ? $control.attr('data-parentformdatafields') : '';
            if (whitelistedFields.length > 0) {
                whitelistedFields = whitelistedFields.split(',');
                for (var fieldname in allparentformfields) {
                    for (var i = 0; i < whitelistedFields.length; i++) {
                        var whitelistedField = whitelistedFields[i];
                        var indexOfEquals = whitelistedField.indexOf('=');
                        if (indexOfEquals === -1) {
                            if (fieldname === whitelistedFields[i]) {
                                parentformfields[fieldname] = allparentformfields[fieldname];
                            }
                        } else {
                            var apiName = whitelistedField.substr(0, indexOfEquals - 1);
                            var parentFormFieldName = whitelistedField.substr(indexOfEquals);
                            if (fieldname === apiName) {
                                parentformfields[fieldname] = allparentformfields[fieldname];
                            }
                        }
                    }
                }
            }
            
            var gridfields = FwBrowse.getWebApiRowFields($control, $tr);
            request = jQuery.extend({}, parentformfields, gridfields);
            if (typeof $control.data('beforesave') === 'function') {
                $control.data('beforesave')(request);
            }
        }
        else {
            // set request for old api
            rowuniqueids  = FwBrowse.getRowFormUniqueIds($control, $tr);
            rowfields     = FwBrowse.getRowFormDataFields($control, $tr);
            miscfields    = FwBrowse.getRowFormDataFields($control, $tr, true);
            if ($form.length > 0) {
                formuniqueids = FwModule.getFormUniqueIds($form);
                formfields    = FwModule.getFormFields($form, true);
            }
            // remove any uniqueids that are in the fields in NEW mode only
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
                module:     name,
                mode:       mode,
                ids:        rowuniqueids,
                fields:     rowfields,
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
        FwServices.grid.method(request, name, mode, $control, function (response) {
            // if the server reloaded the fields, then databind them into the row
            var $fields = $tr.find('.field');
            for (var fieldno = 0; fieldno < $fields.length; fieldno++) {
                var $field = $fields.eq(fieldno);
                if (typeof $field.attr('data-formdatafield') !== 'undefined' && typeof response[$field.attr('data-formdatafield')] !== 'undefined') {
                    $field.attr('data-originalvalue', response[$field.attr('data-formdatafield')]);
                }
            }

            FwBrowse.setRowViewMode($control, $tr);
            if ($control.find('tbody tr.editmode').length === 0) {
                FwBrowse.search($control);
            }
            $control.attr('data-mode', 'VIEW');
            if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                var controller;
                controller = $control.attr('data-controller');
                if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
                if (typeof window[controller]['afterSave'] === 'function') {
                    window[controller]['afterSave']($control, $tr);
                }
            }
        });
    }

    return isvalid;
};
//----------------------------------------------------------------------------------------------
FwBrowse.deleteRow = function($control, $tr) {
    var rowuniqueids, formuniqueids, name, $form, $confirmation, $ok, $cancel, candelete, miscfields;
    candelete = true;
    miscfields = {};
    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        if (typeof window[controller]['beforeDelete'] === 'function') {
            candelete = window[controller]['beforeDelete']($control, $tr);
        }
    }
    if (($tr.length == 1) && (candelete)) {
        $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Delete Record?');
        $ok           = FwConfirmation.addButton($confirmation, 'OK');
        $cancel       = FwConfirmation.addButton($confirmation, 'Cancel');
        
        $ok.on('click', function() {
            FwBrowse.deleteRecord($control, $tr);
        });
    }
};
//----------------------------------------------------------------------------------------------
FwBrowse.deleteRecord = function($control, $tr) {
    var rowuniqueids, formuniqueids, name, $form, candelete, miscfields;
    miscfields   = {};
    name         = $control.attr('data-name');
    $form        = $control.closest('.fwform');
    rowuniqueids = FwBrowse.getRowFormUniqueIds($control, $tr);
    request = {
        module:     name,
        ids:        rowuniqueids,
        miscfields: miscfields
    };
    if ($form.length > 0) {
        formuniqueids      = ($form.length > 0) ? FwModule.getFormUniqueIds($form) : [];
        request.miscfields = jQuery.extend({}, miscfields, formuniqueids);
    }
    FwServices.grid.method(request, name, 'Delete', $control, function(response) {
        //perform after delete
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['afterDelete'] === 'function') {
                window[controller]['afterDelete']($control, $tr);
            }
        }
        FwBrowse.search($control);
    });
};
//---------------------------------------------------------------------------------
FwBrowse.addLegend = function($control, caption, color) {
    var html, $legenditem;
    $control.find('tr.legendrow').show();
    html = [];
    html.push('<div class="legenditem">');
    html.push('  <div class="color" style="background-color:' + color + '"></div>');
    html.push('  <div class="caption">' + caption + '</div>');
    html.push('</div>');
    html = html.join('\n');
    $legenditem = jQuery(html);
    $control.find('div.legend').append($legenditem);
};
//----------------------------------------------------------------------------------------------
FwBrowse.getGridData = function($object, request, responseFunc) {
    var webserviceurl, controller, module;
    controller = $object.attr('data-controller');
    module = window[controller].Module;
    request.module = module;
    webserviceurl = 'services.ashx?path=/grid/' + module + '/GetData';
    FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $object);
};
//----------------------------------------------------------------------------------------------
FwBrowse.disableGrid = function($control) {
    $control.attr('data-enabled', false);
};
//----------------------------------------------------------------------------------------------
FwBrowse.enableGrid = function($control) {
    $control.attr('data-enabled', true);
};
//---------------------------------------------------------------------------------
FwBrowse.validateRow = function($control, $tr) {
    var isvalid, $fields;

    isvalid = true;

    $fields = $tr.find('.field');
    //$fields = $tr.find('.field[data-isuniqueid!="true"]');
    $fields.each(function(index) {
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

    //validate grid values before saves
    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '') && (isvalid)) {
        var controller;
        controller = $control.attr('data-controller');
        if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        if (typeof window[controller]['validateRow'] === 'function') {
            isvalid = window[controller]['validateRow']($control, $tr);
        }
    }

    if (!isvalid)
    {
        FwNotification.renderNotification('ERROR', 'Please correct the error(s) before saving this row.');
    }

    return isvalid;
};
//----------------------------------------------------------------------------------------------
FwBrowse.getOptions = function($control) {
    var $fwformfields, fields, field;
    
    $fwformfields = $control.find('.advancedoptions .fwformfield');
    fields        = {};
    $fwformfields.each(function(index, element) {
        var $fwformfield, dataField, value;
        
        $fwformfield   = jQuery(element);
        dataField      = $fwformfield.attr('data-datafield');
        value          = FwFormField.getValue2($fwformfield);

        field = {
            datafield: dataField,
            value:     value
        };
        fields[dataField] = field;
    });

    return fields;
};
//----------------------------------------------------------------------------------------------
FwBrowse.getValidationData = function($object, request, responseFunc) {
    var webserviceurl, controller, module;
    controller = $object.attr('data-controller');
    module = window[controller].Module;
    request.module = module;
    webserviceurl = 'services.ashx?path=/validation/' + module + '/GetData';
    FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $object);
};
//---------------------------------------------------------------------------------
FwBrowse.getController = function ($control) {
    var controllername;
    var controller; // default value of controller will be undefined if not found
    if (typeof $control.attr('data-name') === 'string' && $control.attr('data-name').length > 0) {
        controllername = $control.attr('data-name') + 'Controller';
    }
    else if (typeof $control.attr('data-controller') === 'string' && $control.attr('data-controller').length > 0) {
        controllername = $control.attr('data-controller');
    }
    if (typeof controllername !== 'undefined') {
        controller = window[controllername];
    }
    return controller
};
//--------------------------------------------------------------------------------- 
FwBrowse.isUsingWebApi = function ($control) { 
    var useWebApi = false; 
    var controller = FwBrowse.getController($control); 
    if (typeof controller.apiurl !== 'undefined') { 
        useWebApi = true; 
    } 
    return useWebApi; 
};
//---------------------------------------------------------------------------------
FwBrowse.loadBrowseFromTemplate = function (modulename) {
    var $control = jQuery(jQuery('#tmpl-modules-' + modulename + 'Browse').html());
    return $control;
};
//---------------------------------------------------------------------------------
FwBrowse.loadGridFromTemplate = function (modulename) {
    var $control = jQuery(jQuery('#tmpl-grids-' + modulename + 'Browse').html());
    return $control;
};
//---------------------------------------------------------------------------------
