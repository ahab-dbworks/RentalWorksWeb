class FwBrowseClass {
    //---------------------------------------------------------------------------------
    upgrade($control) {
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
    init($control) {
        var controller = $control.attr('data-controller');
        let me = this;
        var $columns;
        $control.on('mousewheel', '.txtPageNo', function (event) {
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
        for (var colno = 0; colno < $columns.length; colno++) {
            var $column = $columns.eq(colno);
            var $fields = $column.find('div.field');
            for (let i = 0; i < $fields.length; i++) {
                let $field = $fields.eq(i);
                if (typeof $field.attr('data-isuniqueid') === 'undefined') {
                    $field.attr('data-isuniqueid', 'false');
                }
                if (typeof $field.attr('data-sort') === 'undefined') {
                    $field.attr('data-sort', 'off');
                }
                if (typeof $field.attr('data-formreadonly') === 'undefined' && $control.attr('data-type') === 'Grid') {
                    $field.attr('data-formreadonly', 'false');
                }
                if (typeof $field.attr('data-formreadonly') === 'undefined' && $control.attr('data-type') === 'Browse') {
                    $field.attr('data-formreadonly', 'true');
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
        }

        //Default control attributes
        if (typeof ($control.attr('data-mode') !== 'string')) {
            $control.attr('data-mode', 'VIEW');
        }
        if (typeof ($control.attr('data-pageno') !== 'string')) {
            $control.attr('data-pageno', '1');
        }
        if (typeof $control.attr('data-pagesize') !== 'string') {
            //if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Grid') {
            //    $control.attr('data-pagesize', sessionStorage.getItem('browsedefaultrows'));
            //} else {
            //    $control.attr('data-pagesize', '15');  
            //}

            //justin hoffman 01/25/2020 RWW#1659
            if ($control.attr('data-type') === 'Browse') {
                $control.attr('data-pagesize', sessionStorage.getItem('browsedefaultrows'));
            } else if ($control.attr('data-type') === 'Grid') {
                $control.attr('data-pagesize', '50');
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
            //var args;
            //args = {};
            //var nodeModule = FwApplicationTree.getNodeByController($control.attr('data-controller'));
            //var nodeModule = null;
            //if (controller !== 'AuditHistoryGridController' && window[controller] !== null ) {
            //    nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, (<any>window)[controller].id);
            //}
            //var nodeBrowse = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function (node, args2) {
            //    return (node.nodetype === 'Browse');
            //});
            //var nodeView = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function (node, args2) {
            //    return (node.nodetype === 'ViewMenuBarButton');
            //});
            //var nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, args, function (node, args2) {
            //    return (node.nodetype === 'EditMenuBarButton');
            //});
            //if ((nodeModule !== null) && (nodeModule.properties.visible === 'T')) {
            //    $control.data('onrowdblclick', function () {
            //        try {
            //            me.openSelectedRow($control);
            //        } catch (ex) {
            //            FwFunc.showError(ex);
            //        }
            //    });
            //}
            $control.data('onrowdblclick', function () {
                try {
                    me.openSelectedRow($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        $control
            .on('keydown', 'tbody tr', function (e) {
                var code;
                try {
                    code = e.keyCode || e.which;
                    switch (code) {
                        case 13: //Enter Key
                            if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                                e.preventDefault();
                                e.stopPropagation();
                                if (jQuery.inArray(e.target.offsetParent, $control.find('thead tr td .field div.search')) > -1) {
                                    me.search($control);
                                } else if (($control.attr('data-type') === 'Browse') && ($control.find('tbody tr.selected').length > 0)) {
                                    me.openSelectedRow($control);
                                } else if (($control.attr('data-type') === 'Validation') && ($control.find('tbody tr.selected').length > 0)) {
                                    var $tr;

                                    $tr = $control.find('tbody tr.selected');

                                    $tr.dblclick();
                                }
                            }
                            break;
                        case 32: //Space Bar Key
                            if (($control.attr('data-type') === 'Validation') && ($control.attr('data-multiselectvalidation') === 'true')) {
                                e.preventDefault();
                                e.stopPropagation();
                                var $tr = $control.find('tbody tr:focus');
                                $tr.click();
                            }
                            break;
                        case 37: //Left Arrow Key
                            if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                                me.prevPage($control);
                            }
                            return false;
                        case 38: //Up Arrow Key
                            if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                                me.selectPrevRow($control);
                                return false;
                            } else if ($control.attr('data-type') === 'Grid') {
                                const $tr = jQuery(e.currentTarget);
                                const $cell = jQuery(e.target);
                                const fieldName = jQuery(e.target).parent('div').attr('data-browsedatafield');
                                $control.data('selectedfield', fieldName);
                                const rowindex = me.getSelectedRowIndex($control);
                                const pageno = me.getPageNo($control);
                                if ($control.attr('data-multisave') == 'true') {
                                    if ((rowindex === 0) && (pageno > 1)) {
                                        FwConfirmation.yesNo('Save', 'Save row(s)?', function onyes() {
                                            const $trs = $control.find('tr.editmode.editrow');
                                            me.multiSaveRow($control, $trs)
                                                .then(() => {
                                                    $control.data('selectedrowmode', 'edit');
                                                    $control.find('tbody tr:first-of-type').addClass('selected');
                                                    me.selectPrevRow($control);
                                                })
                                        }, function onno() { });
                                    } else if (rowindex > 0) {
                                        const $prevRow = me.selectPrevRow($control);
                                        if (($prevRow.length > 0) && (!$prevRow.hasClass('editmode'))) {
                                            me.setRowEditMode($control, $prevRow);
                                        } else {
                                            if (typeof $control.data('selectedfield') === 'string') {
                                                const fieldName = $control.data('selectedfield');
                                                $prevRow.find(`[data-browsedatafield="${fieldName}"] input`).select();
                                                $control.data('selectedfield', []);
                                            }
                                        }
                                    }
                                } else {
                                    me.saveRow($control, $tr)
                                        .then(() => {
                                            if (rowindex === 0) {
                                                $control.data('selectedrowmode', 'edit');
                                                me.selectPrevRow($control);
                                            } else if (rowindex > 0) {
                                                const $prevRow = me.selectPrevRow($control);
                                                if ($prevRow.length > 0) {
                                                    me.setRowEditMode($control, $prevRow);
                                                }
                                            }
                                        })
                                        .catch(() => {
                                            $cell.select();
                                        });
                                }
                                return false;
                            }
                        case 39: //Right Arrow Key
                            if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                                me.nextPage($control);
                            }
                            return false;
                        case 40: //Down Arrow Key
                            if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                                me.selectNextRow($control);
                                return false;
                            } else if ($control.attr('data-type') === 'Grid') {
                                const $tr = jQuery(e.currentTarget);
                                const $cell = jQuery(e.target);
                                const fieldName = jQuery(e.target).parent('div').attr('data-browsedatafield');
                                $control.data('selectedfield', fieldName);
                                const rowindex = me.getSelectedRowIndex($control);
                                const lastrowindex = $control.find('tbody tr').length - 1;
                                const pageno = me.getPageNo($control);
                                const totalpages = me.getTotalPages($control);
                                if ($control.attr('data-multisave') == 'true') {
                                    if ((rowindex === lastrowindex) && (pageno < totalpages)) {
                                        FwConfirmation.yesNo('Save', 'Save rows?', function onyes() {
                                            const $trs = $control.find('tr.editmode.editrow');
                                            me.multiSaveRow($control, $trs)
                                                .then(() => {
                                                    $control.data('selectedrowmode', 'edit');
                                                    $control.find('tbody tr:last-of-type').addClass('selected');
                                                    me.selectNextRow($control);
                                                })
                                        }, function onno() { });
                                    } else if (rowindex < lastrowindex) {
                                        const $nextRow = me.selectNextRow($control);
                                        if (($nextRow.length > 0) && (!$nextRow.hasClass('editmode'))) {
                                            me.setRowEditMode($control, $nextRow);
                                        } else {
                                            if (typeof $control.data('selectedfield') === 'string') {
                                                const fieldName = $control.data('selectedfield');
                                                $nextRow.find(`[data-browsedatafield="${fieldName}"] input`).select();
                                                $control.data('selectedfield', []);
                                            }
                                        }
                                    }
                                } else {
                                    me.saveRow($control, $tr)
                                        .then(() => {
                                            if (rowindex === lastrowindex) {
                                                $control.data('selectedrowmode', 'edit');
                                                me.selectNextRow($control);
                                            } else if (rowindex < lastrowindex) {
                                                const $nextRow = me.selectNextRow($control);
                                                if ($nextRow.length > 0) {
                                                    me.setRowEditMode($control, $nextRow);
                                                }
                                            }
                                        })
                                        .catch(() => {
                                            $cell.select();
                                        });
                                }
                                return false;
                            }
                        case 45: //Insert key
                            $tr = jQuery(e.currentTarget);
                            const inEditMode = $tr.hasClass('editmode');
                            const inNewMode = $tr.hasClass('newmode');
                            const hasNew = $control.find('.buttonbar [data-type="NewButton"]:visible');
                            const hasMultiSave = $control.attr('data-multisave') === 'true' && $control.find('.grid-multi-save:visible').length > 0;
                            if (hasNew.length > 0 || hasMultiSave || inNewMode) {
                                if ((inEditMode) || (inNewMode)) {
                                    if (hasMultiSave) {
                                        const $trs = $control.find('tr.editmode.editrow');
                                        me.multiSaveRow($control, $trs).then(() => {
                                            me.addRowNewMode($control);
                                        });
                                    } else {
                                        me.saveRow($control, $tr).then(() => {
                                            me.addRowNewMode($control);
                                        });
                                    }
                                } else {
                                    me.addRowNewMode($control);
                                }
                            }
                            return false;
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
                            } else if ($tr.hasClass('selected')) {
                                $keyfields = $tr.find('div.field[data-isuniqueid="true"]');
                                compositekey = [];
                                $keyfields.each(function (index, element) {
                                    var $keyfield;
                                    $keyfield = jQuery(element);
                                    compositekey.push($keyfield.html());
                                });
                                compositekey = compositekey.join(',');
                                if (typeof $control.data('selectedrows') === 'undefined') {
                                    $control.data('selectedrows', {});
                                }
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
                            me.selectRow($control, $tr, dontfocus);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', 'table thead tr td .field div.search .searchclear', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    var $this = jQuery(this);
                    $this.siblings('input').val('').change();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.designer thead > tr > td.addcolumn', function (e: JQuery.Event) { // Designer: Add a column
                try {
                    let $thAddColumn = jQuery(this);
                    me.addDesignerColumn($control, $thAddColumn, 'auto', true);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.designer thead .columnhandle .delete', function (e: JQuery.Event) { // Designer: Delete a column
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.designer thead .field > .deletefield', function () { // Designer: Click a field's delete button
                var $this, $field;
                try {
                    $this = jQuery(this);
                    $field = $this.closest('.field');
                    $field.remove();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.designer thead .addfield', function () {
                var $this, $th;
                try {
                    $this = jQuery(this);
                    $th = $this.closest('td');
                    me.addDesignerField($th, 'newfield', 'newfield', 'newfield', 'string');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('dblclick', '.designer thead .field .caption', function () { // Designer: double-click to rename column
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('change', '.runtime thead .search > input', function (e) { // Runtime: Enter a search text into a columns searchbox
                try {
                    if (($control.attr('data-type') != 'Validation') || (jQuery('html').hasClass('mobile'))) {
                        var $this = jQuery(this);
                        if ($this.val() === '') {
                            $this.siblings('.searchclear').removeClass('visible');
                        } else if ($this.val() !== '') {
                            $this.siblings('.searchclear').addClass('visible');
                        }
                        me.search($control);
                        $this.focus();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            //.on('keydown', '.runtime thead .search > input', function(e) {
            //    var code;
            //    try {
            //        code = e.keyCode || e.which;
            //        if(code === 13) { //Enter Key
            //            me.search($control);
            //            e.preventDefault();
            //            return false;
            //        }
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //})
            .on('change', '.runtime .pager select.pagesize', function () {
                var $this, pagesize
                try {
                    $this = jQuery(this);
                    pagesize = $this.val();
                    me.setPageSize($control, pagesize);
                    me.search($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.runtime .pager div.btnRefresh', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    if ($control.attr('data-multisave') == 'true') {
                        $control.find('.grid-multi-save').hide();
                    }
                    me.databind($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.runtime .pager div.btn-manualsort', function (e: JQuery.Event) {
                try {
                    if ($control.attr('data-enabled') != 'false') {
                        const $sort = $control.find('td.manual-sort');
                        const $newBtn = $control.find('.buttonbar [data-type="NewButton"]');
                        $sort.toggle();
                        if ($sort.is(':visible')) {
                            $control.addClass('sort-mode');
                            $newBtn.hide();
                        } else {
                            $control.removeClass('sort-mode');
                            $newBtn.show();
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        //.on('click', 'tbody .browsecontextmenu', function () {
        //    try {
        //        let $browse = jQuery(this).closest('.fwbrowse');
        //        if ($browse.attr('data-enabled') !== 'false') {
        //            var menuItemCount = 0;
        //            var $browsecontextmenu = jQuery(this);
        //            var $tr = $browsecontextmenu.closest('tr');
        //            //me.unselectAllRows($control);
        //            //me.selectRow($control, $tr, true);
        //            var $contextmenu = FwContextMenu.render('Options', 'bottomleft', $browsecontextmenu);
        //            //$contextmenu.data('beforedestroy', function () {
        //            //    me.unselectRow($control, $tr);
        //            //});

        //            var controller = $control.attr('data-controller');
        //            if (typeof controller === 'undefined') {
        //                throw 'Attribute data-controller is not defined on Browse control.'
        //            }
        //            var nodeController = FwApplicationTree.getNodeByController(controller);
        //            if (nodeController !== null) {
        //                var deleteActions = FwApplicationTree.getChildrenByType(nodeController, 'DeleteMenuBarButton');
        //                if (deleteActions.length > 1) {
        //                    throw 'Invalid Security Tree configuration.  Only 1 DeleteMenuBarButton is permitted on a Controller.';
        //                }
        //                if (deleteActions.length === 1 && deleteActions[0].properties['visible'] === 'T') {
        //                    FwContextMenu.addMenuItem($contextmenu, 'Delete', function () {
        //                        try {
        //                            var $tr = jQuery(this).closest('tr');
        //                            me.deleteRow($control, $tr);
        //                        } catch (ex) {
        //                            FwFunc.showError(ex);
        //                        }
        //                    });
        //                    menuItemCount++;
        //                }
        //            }
        //            if (menuItemCount === 0) {
        //                FwContextMenu.destroy($contextmenu);
        //            }
        //        }
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});

        $control
            .on('click', '.runtime .pager div.buttons .btnFirstPage', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    var $btnFirstPage = jQuery(this);
                    $control.data('selectedrowmode', []);
                    if ($btnFirstPage.attr('data-enabled') === 'true') {
                        $control.attr('data-pageno', '1');
                        me.databind($control);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        $control
            .on('click', '.runtime .pager div.buttons .btnPreviousPage', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    $control.data('selectedrowmode', []);
                    me.prevPage($control);
                } catch (ex) {
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
                        } else {
                            $control.find('.runtime .pager div.buttons .txtTotalPages').val(pageno);
                        }
                    } else {

                    }
                } catch (ex) {
                    $control.find('.runtime .pager div.buttons .txtTotalPages').val(originalpagenoStr);
                    FwFunc.showError(ex);
                }
            });
        $control
            .on('click', '.runtime .pager div.buttons .btnNextPage', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    $control.data('selectedrowmode', []);
                    me.nextPage($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        $control
            .on('click', '.runtime .pager div.buttons .btnLastPage', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    var $btnLastPage = jQuery(this);
                    $control.data('selectedrowmode', []);
                    if ($btnLastPage.attr('data-enabled') === 'true') {
                        var pageno = me.getTotalPages($control);
                        me.setPageNo($control, pageno);
                        me.databind($control);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        $control
            .on('click', '.runtime .pager div.show-all', function (e: JQuery.Event) {
                try {
                    e.stopPropagation();
                    $control.attr('data-pagesize', 9999);
                    $control.addClass('show-all');
                    me.databind($control);
                } catch (ex) {
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

        //Events only attached when the API is not defined for the control.
        var controllerInstance = (<any>window)[$control.attr('data-controller')];
        if (($control !== undefined) && ($control.attr('data-type') === 'Grid') && (controllerInstance !== undefined) && (typeof controllerInstance.apiurl === 'undefined')) {
            $control.on('change', '.field[data-formnoduplicate="true"]', function () {
                var $field, value, originalvalue, $form, formuniqueids, formfields, request: any = {};
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
                    FwServices.grid.method(request, $control.attr('data-name'), 'ValidateDuplicate', $control,
                        // onSuccess
                        function (response) {
                            try {
                                if (response.duplicate == true) {
                                    $field.addClass('error');
                                    FwNotification.renderNotification('ERROR', 'Duplicate ' + $field.attr('data-caption') + '(s) are not allowed.');
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        },
                        // onError
                        function (errorMessage) {
                            try {
                                FwFunc.showError(errorMessage);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        }
                    );
                }
            });
        }

        //Register Custom Events on grids and validations
        if (controller !== 'AuditHistoryGrid' && (($control.attr('data-type') == 'Grid') || ($control.attr('data-type') == 'Validation')) && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            if (typeof window[controller] === 'undefined') throw new Error('Missing javascript module: ' + controller);
            if (typeof (<any>window)[controller]['init'] === 'function') {
                (<any>window)[controller]['init']($control);
            }
        }
    }
    //---------------------------------------------------------------------------------
    getPageNo($control) {
        return parseInt($control.attr('data-pageno'));
    }
    //---------------------------------------------------------------------------------
    setPageNo($control, pageno) {
        $control.attr('data-pageno', pageno);
    }
    //---------------------------------------------------------------------------------
    getPageSize($control) {
        return parseInt($control.attr('data-pagesize'));
    }
    //---------------------------------------------------------------------------------
    setPageSize($control, pagesize) {
        $control.attr('data-pagesize', pagesize);
    }
    //---------------------------------------------------------------------------------
    getTotalPages($control) {
        return parseInt($control.attr('data-totalpages'));
    }
    //---------------------------------------------------------------------------------
    setTotalPages($control, totalpages) {
        $control.attr('data-totalpages', totalpages);
    }
    //---------------------------------------------------------------------------------
    getSelectedIndex($control: JQuery): number {
        return parseInt($control.attr('data-selectedindex'));
    }
    //---------------------------------------------------------------------------------
    setSelectedIndex($control, selectedindex) {
        $control.attr('data-selectedindex', selectedindex);

        //cancel any unmodified rows in edit mode
        //let me = this;
        //var $trsEditRow = $control.find('tbody tr.editrow');
        //$trsEditRow.each(function (index, element) {
        //    var $trEditRow = jQuery(element);
        //    if (!me.isRowModified($control, $trEditRow)) {
        //        me.cancelEditMode($control, $trEditRow);
        //    }
        //});
    }
    //---------------------------------------------------------------------------------
    getSelectedRowMode($control: JQuery): string {
        let selectedRowMode: string = typeof $control.data('selectedrowmode') === 'string' ? $control.data('selectedrowmode') : '';
        return selectedRowMode;
    }
    //---------------------------------------------------------------------------------
    setSelectedRowMode($control: JQuery, mode: string): void {
        $control.data('selectedrowmode', mode);
    }
    //---------------------------------------------------------------------------------
    isRowModified($control: JQuery, $tr: JQuery) {
        if (!$tr.hasClass('editrow')) {
            return false;
        }
        var $fields = $tr.find('.field[data-formdatafield][data-formreadonly!="true"]');
        var isRowUnmodified = true;
        for (let i = 0; i < $fields.length; i++) {
            var $field = $fields.eq(i);
            var field = { datafield: $field.attr('data-browsedatafield'), value: '' };
            if (typeof window['FwBrowseColumn_' + $field.attr('data-formdatatype')] !== 'undefined') {
                let isModifiedFunction = window['FwBrowseColumn_' + $field.attr('data-formdatatype')].isModified;
                let isFieldUnmodified = !isModifiedFunction($control, $tr, $field, field, $field.attr('data-originalvalue'));
                isRowUnmodified = isRowUnmodified && isFieldUnmodified;
                //var $inputValue = $field.find('.value');
                //if ($inputValue.length > 0) {
                //    isRowUnmodified = isRowUnmodified && $inputValue.val() === $field.attr('data-originalvalue');
                //}
                if (isRowUnmodified === false) {
                    break;
                }
            }
        }
        return !isRowUnmodified;
    }
    //---------------------------------------------------------------------------------
    getSelectedRow($control: JQuery): JQuery {
        return $control.find('tbody tr.selected');
    }
    //---------------------------------------------------------------------------------
    getSelectedRowIndex($control: JQuery): number {
        return this.getSelectedRow($control).index();
    }
    //---------------------------------------------------------------------------------
    getRows($control: JQuery): JQuery {
        return $control.find('tbody tr');
    }
    //---------------------------------------------------------------------------------
    unselectAllRows($control: JQuery): void {
        $control.find('tbody tr.selected').removeClass('selected');
        this.setSelectedIndex($control, -1);
    }
    //---------------------------------------------------------------------------------
    unselectRow($control: JQuery, $tr: JQuery): void {
        $tr.removeClass('selected');
        if (this.getSelectedIndex($control) === $tr.index()) {
            this.setSelectedIndex($control, -1);
        }
    }
    //---------------------------------------------------------------------------------
    selectRow($control: JQuery, $row: JQuery, dontfocus?: boolean): void {
        var $prevselectedrow = this.getSelectedRow($control);
        $prevselectedrow.removeClass('selected');
        $row.addClass('selected');
        this.setSelectedIndex($control, $row.index());
        if (dontfocus !== true) {
            $row.focus();
        }
    }
    //---------------------------------------------------------------------------------
    selectRowByIndex($control: JQuery, index: number): JQuery {
        var $rows = this.getRows($control);
        var $row = $rows.eq(index);
        this.selectRow($control, $row);
        return $row;
    }
    //---------------------------------------------------------------------------------
    getRowCount($control: JQuery): number {
        var $rows = this.getRows($control);
        var rowcount = $rows.length;
        return rowcount;
    }
    //---------------------------------------------------------------------------------
    getTotalRowCount($control: JQuery): number {
        let totalRowCount = $control.data('totalRowCount');
        return totalRowCount;
    }
    //---------------------------------------------------------------------------------
    /** Select the prev row in the browse control. This will load the previous page if necessary.
        @param {object} $control - The browse control
    */
    selectPrevRow($control: JQuery, afterrowselected?: Function): JQuery {
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
        } else if ((rowindex === 0) && (pageno > 1)) {
            var self = this;
            rowindex = pagesize - 1;
            this.setSelectedIndex($control, rowindex);
            this.addEventHandler($control, 'afterdatabindcallback', function afterdatabindcallback_selectPrevRow() {
                self.removeEventHandler($control, 'afterdatabindcallback', afterdatabindcallback_selectPrevRow);
                if (typeof afterrowselected === 'function') {
                    afterrowselected();
                }
            });
            this.prevPage($control);
        }
        return $selectedrow;
    }
    //---------------------------------------------------------------------------------
    /** Select the next row in the browse control.
        @param {object} $control - The browse control
    */
    selectNextRow($control: JQuery, afterrowselected?: Function): JQuery {
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
            var self = this;
            this.setSelectedIndex($control, 0);
            this.addEventHandler($control, 'afterdatabindcallback', function afterdatabindcallback_selectNextRow() {
                self.removeEventHandler($control, 'afterdatabindcallback', afterdatabindcallback_selectNextRow);
                if (typeof afterrowselected === 'function') {
                    afterrowselected();
                }
            });
            this.nextPage($control);
        }
        const $trselected = $control.find('tbody tr.selected');
        return $trselected;
    }
    //---------------------------------------------------------------------------------
    /** Open the form of the prev row in the browse control.  Supports paging.
        @param {object} $control - The browse control
    */
    //---------------------------------------------------------------------------------
    openPrevRow($control: JQuery, $tab: JQuery, $form: JQuery): void {
        if ((typeof $tab !== 'undefined') && ($tab.length === 1) && (typeof $form !== 'undefined') && ($form.length === 1)) {
            FwModule.closeForm($form, $tab, null, function afterCloseForm() {
                this.selectPrevRow($control, function afterrowselected() {
                    this.openSelectedRow($control);
                });
            });
        }
    }
    //---------------------------------------------------------------------------------
    /** Open the form of the next row in the browse control. Supports paging.
        @param {object} $control - The browse control
    */
    //---------------------------------------------------------------------------------
    openNextRow($control: JQuery, $tab: JQuery, $form: JQuery): void {
        if ((typeof $tab !== 'undefined') && ($tab.length === 1) && (typeof $form !== 'undefined') && ($form.length === 1)) {
            FwModule.closeForm($form, $tab, null, function afterCloseForm() {
                this.selectNextRow($control, function afterrowselected() {
                    this.openSelectedRow($control);
                });
            });
        }
    }
    //---------------------------------------------------------------------------------
    /** Load the prev page of records from the database.
      @param {object} $control - The browse control
    */
    prevPage($control: JQuery): void {
        var pageno, $btnPreviousPage;
        $btnPreviousPage = $control.find('.btnPreviousPage');
        if ($btnPreviousPage.attr('data-enabled') === 'true') {
            pageno = this.getPageNo($control) - 1;
            pageno = (pageno >= 1) ? pageno : 1;
            this.setPageNo($control, pageno);
            this.databind($control);
        }
    }
    //---------------------------------------------------------------------------------
    /** Load the next page of records from the database.
      @param {object} $control - The browse control
    */
    nextPage($control: JQuery): void {
        var $btnNextPage, pageno, totalpages
        $btnNextPage = $control.find('.btnNextPage');
        if ($btnNextPage.attr('data-enabled') === 'true') {
            pageno = this.getPageNo($control) + 1;
            totalpages = this.getTotalPages($control);
            pageno = (pageno <= totalpages) ? pageno : totalpages;
            this.setPageNo($control, pageno);
            this.databind($control);
        }
    }
    //---------------------------------------------------------------------------------
    /** Opens the selected record into a tab.
        @param {object} $control - The browse control
    */
    openSelectedRow($control: JQuery): void {
        var $selectedrow, browseuniqueids, formuniqueids, $fwforms, dataType, $form, issubmodule, nodeModule, nodeBrowse, nodeView, nodeEdit;
        $selectedrow = this.getSelectedRow($control);
        //$selectedrow.dblclick();
        dataType = (typeof $control.attr('data-type') === 'string') ? $control.attr('data-type') : '';
        switch (dataType) {
            case 'Browse':
                formuniqueids = this.getRowFormUniqueIds($control, $selectedrow);
                browseuniqueids = this.getRowBrowseUniqueIds($control, $selectedrow);
                $form = FwModule.getFormByUniqueIds(formuniqueids);
                if ((typeof $form === 'undefined') || ($form.length == 0)) {
                    if (typeof $control.attr('data-controller') === 'undefined') {
                        throw 'this: Missing attribute data-controller.  Set this attribute on the browse control to the name of the controller for this browse module.'
                    }
                    $form = window[$control.attr('data-controller')].loadForm(browseuniqueids);
                    issubmodule = $control.closest('.tabpage').hasClass('submodule');
                    if (!issubmodule) {
                        FwModule.openModuleTab($form, `${$form.attr('data-caption')} (loading)`, true, 'FORM', true);
                    } else {
                        FwModule.openSubModuleTab($control, $form);
                    }
                } else if ($form.length > 0) {
                    var tabid = $form.closest('div.tabpage').attr('data-tabid');
                    jQuery('#' + tabid).click();
                }
                break;
            case 'Grid':
                break;
        }
    }
    //---------------------------------------------------------------------------------
    /** Attach custom event handlers with arrays to the data collection of the browse control.  This allows multiple callback functions for the event. Useful for events like afterdatabinkcallback.
        @param {object} $control - The browse control.
        @param {string} eventName - Then name of the browse control event.
        @param {function} callbackfunction - The callback function to fire when the event occurs.
    */
    addEventHandler($control: JQuery, eventName: 'afterdatabindcallback' | string, callbackfunction: Function): void {
        var callbackfunctions = [];
        if (Array.isArray($control.data(eventName))) {
            callbackfunctions = $control.data(eventName);
        }
        callbackfunctions.push(callbackfunction);
        $control.data(eventName, callbackfunctions);
    }
    //---------------------------------------------------------------------------------
    /** Remove custom event handlers from the browse control.
        @param {object} $control - The browse control.
        @param {string} eventName - Then name of the browse control event.
        @param {function} callbackfunction - The callback function to fire when the event occurs.
    */
    removeEventHandler($control: JQuery, eventName: 'afterdatabindcallback' | string, callbackfunction: Function): void {
        if (Array.isArray($control.data(eventName))) {
            var callbackfunctions = $control.data(eventName);
            for (var i = 0; i < callbackfunctions.length; i++) {
                if (callbackfunctions[i] === callbackfunction) {
                    callbackfunctions.splice(i, 1);
                }
            }
        }
    }
    //---------------------------------------------------------------------------------
    /** Get the relative url of the sort image in the framework.
        @param {string} sort - 'asc', 'desc', or 'off'
    */
    getSortImage(sort: 'asc' | 'desc' | 'off') {
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
    }
    //---------------------------------------------------------------------------------
    /** Sets the pageno to 1 before loading records from the database.
        @param {object} $control - The browse control
    */
    search($control: JQuery): Promise<any> {
        this.setPageNo($control, 1);
        return this.databind($control);
    }
    //---------------------------------------------------------------------------------
    addDesignerField($column: JQuery, cssclass: string, caption: string, datafield: string, datatype: string): void {
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
    }
    //---------------------------------------------------------------------------------
    addDesignerColumn($control: JQuery, $thAddColumn: JQuery, width: string, visible: boolean): void {
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
    }
    //---------------------------------------------------------------------------------
    getHtmlTag(data_type: string): string {
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
    }
    //---------------------------------------------------------------------------------
    getDesignerProperties(data_type: string): Array<any> {
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
    }
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery): void {
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
                    for (let colno = 0; colno < $columns.length; colno++) {
                        let $column = $columns.eq(colno);
                        let width = $column.attr('data-width');
                        let visible = $column.attr('data-visible');
                        html.push('<td class="column">');
                        let $fields = $column.find('> .field');
                        for (let fieldno = 0; fieldno < $fields.length; fieldno++) {
                            let $field = $fields.eq(fieldno);
                            //let caption = $field.attr('data-caption');
                            let cssclass = $field.attr('data-cssclass');
                            let browsedatafield = $field.attr('data-browsedatafield');
                            //let browsedatatype = $field.attr('data-browsedatatype');
                            //let formdatafield = $field.attr('data-formdatafield');
                            //let formdatatype = $field.attr('data-formdatatype');
                            html.push('<div class="field ' + cssclass + '">');
                            html.push(browsedatafield + rowno.toString());
                            html.push('</div>');
                        }
                        $fields.each(function (index, field) {

                        });
                        html.push('</td>');
                        html.push('<td class="addcolumn"></td>');
                    }
                    //$columns.each(function (index, column) {
                    //    var $column, caption, browsedatafield, cssclass, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    //    $column = jQuery(column);
                    //    width = $column.attr('data-width');
                    //    visible = $column.attr('data-visible');
                    //    html.push('<td class="column">');
                    //    $fields = $column.find('> .field');
                    //    $fields.each(function (index, field) {
                    //        var $field;
                    //        $field = jQuery(field);
                    //        caption = $field.attr('data-caption');
                    //        cssclass = $field.attr('data-cssclass');
                    //        browsedatafield = $field.attr('data-browsedatafield');
                    //        browsedatatype = $field.attr('data-browsedatatype');
                    //        browsedatafield = $field.attr('data-formdatafield');
                    //        browsedatafield = $field.attr('data-formdatatype');
                    //        html.push('<div class="field ' + cssclass + '">');
                    //        html.push(browsedatafield + i.toString());
                    //        html.push('</div>');
                    //    });
                    //    html.push('</td>');
                    //    html.push('<td class="addcolumn"></td>');
                    //});
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
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery): void {
        let me = this;
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
                    html.push($advancedoptions.wrap('<div/>').parent().html());       //MY 2/26/2015: Hack to get parent .advancedoption div as well as content
                }
                html.push('<div class="fwbrowsefilter" style="display:none;"></div>');
                html.push('<div class="tablewrapper">');
                html.push('<table>');
                html.push('<thead>');
                // header row
                html.push('<tr class="fieldnames">');
                if (($control.attr('data-type') === 'Grid') || (($control.attr('data-type') === 'Browse') && ($control.attr('data-hasmultirowselect') === 'true'))) {
                    if ($control.attr('data-manualsorting') === 'true') {
                        html.push(`<td class="column manual-sort" style="display:none;"></td>`);
                    }
                    let cbuniqueId = FwApplication.prototype.uniqueId(10);
                    if ($control.attr('data-hasmultirowselect') !== 'false') {
                        html.push(`<td class="column tdselectrow" style="width:20px;"><div class="divselectrow"><input id="${cbuniqueId}" type="checkbox" tabindex="-1" class="cbselectrow"/><label for="${cbuniqueId}" class="lblselectrow"></label></div></td>`);
                    }
                }
                for (let colno = 0; colno < $columns.length; colno++) {
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
                        } else if (sort === 'desc') {
                            html.push('<div class="sort"><i class="material-icons">keyboard_arrow_down</i></div>');
                        } else if (sort === 'off') {
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
                        if ($theadfield.attr('data-browsedatatype') === 'date') {
                            html.push(`<input class="value" type="search" name="${'a' + Math.random()}"/>`);
                            html.push('<i class="material-icons btndate" style="position:absolute; right:0px; top:5px;">&#xE8DF;</i>');
                            html.push('<span class="searchclear" title="clear" style="right:20px;"><i class="material-icons">clear</i></span>');
                        } else {
                            if (applicationConfig.allCaps && $control.attr('data-allcaps') !== 'false') {
                                html.push(`<input type="text" style="text-transform:uppercase" name="${'a' + Math.random()}" />`);
                            } else {
                                html.push(`<input type="text" name="${'a' + Math.random()}" />`);
                            }
                            html.push('<span class="searchclear" title="clear"><i class="material-icons">clear</i></span>');
                        }
                        html.push('</div>');
                        html.push('</div>');
                    };
                    html.push('</td>');
                }
                if (($control.attr('data-type') === 'Grid') && ($control.attr('data-flexgrid') === "true")) {
                    html.push('<td class="column flexgridspacer" style="display:none;"></td>'); // 10/12/18 Jason H - add invisible div for flexgrid
                }
                if ($control.attr('data-type') === 'Grid') {
                    html.push('<td class="column browsecontextmenucell" style="width:32px;"></td>');
                }
                html.push('</tr>');
                html.push('</thead>');
                html.push('<tbody>');
                // empty body row
                html.push('<tr class="empty">');
                for (var colno = 0; colno < $columns.length + 2; colno++) {
                    var $column = $columns.eq(colno);
                    //var width = $column.attr('data-width');
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
                    const moduleName = $control.attr('data-name');
                    const controller = `${moduleName}Controller`;
                    const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, (<any>window)[controller].id);
                    const nodeBrowse = FwApplicationTree.getChildByType(nodeModule, 'Browse');
                    const nodeBrowseNewButton = FwApplicationTree.getChildrenByType(nodeBrowse, 'NewMenuBarButton');
                    const hasBrowseNew = (nodeBrowseNewButton.length > 0) ? (nodeBrowseNewButton[0].properties.visible === 'T') : false;

                    html.push('<div class="validationbuttons">');
                    html.push('<div class="fwbrowsebutton btnSelect">Select</div>');
                    html.push('<div class="fwbrowsebutton btnSelectAll" title="The report will run faster if you Select All from this button vs selecting individual rows." style="display:none;">Select All</div>');
                    html.push('<div class="fwbrowsebutton btnClear">Clear</div>');
                    html.push('<div class="fwbrowsebutton btnViewSelection" style="display:none;">View Selection</div>');
                    html.push('<div class="fwbrowsebutton btnCancel">Cancel</div>');
                    if (hasBrowseNew) {
                        html.push('<div class="fwbrowsebutton btnNew">New</div>');
                    }
                    html.push(`<div class="multiSelectDisplay" style="font-size:.9em; font-weight:bold; margin:0px 10px; display:none;">
                                    <div class="fwformfield-caption">Display Field</div>
                                    <div class="fwformfield-control">
                                    <select class="fwformfield-value"></select>
                                    </div>
                            </div>`);
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

                // build pager
                let controlType = $control.attr('data-type');
                let htmlPager = [];
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
                        //htmlPager.push('      <option value="0">All</option>');
                        htmlPager.push('    </select>');
                        htmlPager.push('    <span class="caption">rows per page</span>');
                        htmlPager.push('  </div>');
                        htmlPager.push('</div>');
                        break;
                    case 'Grid':
                        if ($control.attr('data-manualsorting') === 'true') {
                            htmlPager.push('  <div class="btn-manualsort" title="Sort" tabindex="0"><i class="material-icons">sort</i></div>');
                        }
                        if ($control.attr('data-paging') == 'true') {
                            htmlPager.push('<div class="col1" style="width:33%;overflow:hidden;float:left;">');
                            htmlPager.push('  <div class="btnRefresh" title="Refresh" tabindex="0"><i class="material-icons">&#xE5D5;</i></div>');
                            htmlPager.push('  <div class="count" style="float:left;"></div>');
                            htmlPager.push('</div>');
                            htmlPager.push('<div class="col2" style="width:34%;overflow:hidden;float:left;height:32px;text-align:center;">');
                            htmlPager.push('  <div class="buttons">');
                            htmlPager.push('    <div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><i class="material-icons">&#xE5DC;</i></div>');
                            htmlPager.push('    <div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><i class="material-icons">&#xE5CB;</i></div>');
                            htmlPager.push('    <div class="page" style="float:left;">');
                            htmlPager.push('      <input class="txtPageNo" type="text" value="0"/>');
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
                            //htmlPager.push('      <option value="0">All</option>');
                            htmlPager.push('    </select>');
                            htmlPager.push('    <span class="caption">rows per page</span>');
                            htmlPager.push('  </div>');
                            htmlPager.push('</div>');
                            if ((controlType === 'Grid') && (typeof $control.attr('data-activeinactiveview') === 'string') && (FwSecurity.isUser())) {
                                htmlPager.push('<div class="activeinactiveview" style="float:right;">');
                                htmlPager.push('  <select class="activeinactiveview">');
                                htmlPager.push('    <option value="active">Show Active</option>');
                                htmlPager.push('    <option value="inactive">Show Inactive</option>');
                                htmlPager.push('    <option value="all">Show All</option>');
                                htmlPager.push('  </select>')
                                htmlPager.push('</div>');
                            }
                            break;
                        } else {
                            htmlPager.push('  <div class="btnRefresh" title="Refresh" tabindex="0"><i class="material-icons">&#xE5D5;</i></div>');
                        }
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
                        if (controlType === 'Validation') {
                            htmlPager.push(`<div class="show-all">Show All</div>`);
                        }
                        if ((controlType === 'Grid') && (typeof $control.attr('data-activeinactiveview') === 'string') && (FwSecurity.isUser())) {
                            htmlPager.push('<div class="activeinactiveview" style="float:right;">');
                            htmlPager.push('  <select class="activeinactiveview">');
                            htmlPager.push('    <option value="active">Show Active</option>');
                            htmlPager.push('    <option value="inactive">Show Inactive</option>');
                            htmlPager.push('    <option value="all">Show All</option>');
                            htmlPager.push('  </select>')
                            htmlPager.push('</div>');
                        }
                        break;
                }
                let htmlPagerStr = htmlPager.join('');
                let $pager = $control.find('.runtime .pager');
                $pager.html(htmlPagerStr);
                $pager.find('select.pagesize').val($control.attr('data-pagesize'));
                $pager.find('select.activeinactiveview').val($control.attr('data-activeinactiveview'));
                $pager.show();

                // mv 2018-07-08 this is really the wrong place for this.  This needs to be in one of the column files.  Need a way to edit the header html from the column files
                (<any>$control.find('.value')).datepicker({
                    endDate: (($control.attr('data-nofuture') == 'true') ? '+0d' : Infinity),
                    autoclose: true,
                    format: "mm/dd/yyyy",
                    todayHighlight: true,
                    todayBtn: 'linked',
                    weekStart: FwFunc.getWeekStartInt(),
                }).off('focus');

                $control.on('click', '.btndate', e => {
                    (<any>jQuery(e.currentTarget).siblings('.value')).datepicker('show');
                });

                $control.on('click', 'thead .cbselectrow', function () {
                    try {
                        var $this = jQuery(this);
                        $control.find('tbody .cbselectrow').prop('checked', $this.prop('checked'));
                    } catch (ex) {
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
                            } catch (ex) {
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
                            } catch (ex) {
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
                        $clearbtn.on('click', function () {
                            try {
                                $theadfield.find('.search input').val('');
                                me.databind($control);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $columnoptions.append($clearbtn);

                        $clearallbtn = getcolumnoptionbutton('Clear All Filters', '');
                        $clearallbtn.on('click', function () {
                            try {
                                $theadfields.find('.search input').val('');
                                me.databind($control);
                            } catch (ex) {
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
                                            } else if ($field.hasClass('active')) {
                                                jQuery(document).one('click', closeOptions);
                                            }
                                        });
                                    } else {
                                        $field.removeClass('active');
                                        $field.find('.columnoptions').css('z-index', '0');
                                    }
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                });
                //if ($control.attr('data-type') === 'Grid') {
                //    var $menu = FwGridMenu.getMenuControl('grid');
                //    $control.find('.gridmenu').append($menu);
                //}
                break;
        }
        if (($control.attr('data-type') === 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller, $browse;
            $browse = $control.closest('.fwbrowse');
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;

            var $menu = FwGridMenu.getMenuControl('grid');
            $browse.find('.gridmenu').append($menu);
            const $subMenu = FwMenu.addSubMenu($menu);
            const $colActions = FwMenu.addSubMenuColumn($subMenu);
            const $groupActions = FwMenu.addSubMenuGroup($colActions, 'Actions');
            //const $colExport = FwMenu.addSubMenuColumn($subMenu);    -- J. Pace commented to place Export under the Actions column to make menu more narrow #1446
            const $groupExport = FwMenu.addSubMenuGroup($colActions, 'Export');

            //const $submenucolumn = FwGridMenu.addSubMenuColumn($submenubtn);
            //const $rowactions = FwGridMenu.addSubMenuGroup($submenucolumn, 'Actions', '');
            const options: IAddGridMenuOptions = {
                $browse: $browse,
                $menu: $menu,
                $subMenu: $subMenu,
                $colActions: $colActions,
                $groupActions: $groupActions,
                //$colExport: $colExport,
                $groupExport: $groupExport,
                hasNew: true,
                hasEdit: true,
                hasDelete: true,
                gridSecurityId: $browse.data('secid'),
                hasDownloadExcel: true
            };
            if ((typeof window[controller]['addGridMenu'] === 'function')) {
                window[controller]['addGridMenu'](options);
                FwBrowse.addGridMenuButtons(options);
            }
            if (typeof $control.data('addGridMenu') === 'function') {
                $control.data('addGridMenu')(options);
                FwBrowse.addGridMenuButtons(options);
            }

            // cleanup unused menus
            const $submenubuttons = $browse.find('.gridmenu .submenubutton');
            for (let submenubuttonno = 0; submenubuttonno < $submenubuttons.length; submenubuttonno++) {
                const $submenubutton = $submenubuttons.eq(submenubuttonno);
                const $submenugroups = $submenubutton.find('.submenu-group');
                for (let submenugroupno = 0; submenugroupno < $submenugroups.length; submenugroupno++) {
                    const $submenugroup = $submenugroups.eq(submenugroupno);
                    if ($submenugroup.children('.body').eq(0).children().length === 0) {
                        $submenugroup.remove();
                    }
                }
                const $submenucolumns = $submenubutton.find('.submenu-column');
                for (let submenucolno = 0; submenucolno < $submenucolumns.length; submenucolno++) {
                    const $submenucol = $submenucolumns.eq(submenucolno);
                    if ($submenucol.children().eq(0).length === 0) {
                        $submenucol.remove();
                    }
                }
                if ($submenubutton.children('.submenu').children().length === 0) {
                    //$submenubutton.remove();
                    $submenubutton.off('click').css({ color: '#cccccc' });
                }
            }

            if (typeof window[controller]['setDefaultOptions'] === 'function') {
                window[controller]['setDefaultOptions']($control);
            }
            if (typeof window[controller]['addLegend'] === 'function') {
                window[controller]['addLegend']($control);
            }
        }
        //(<any>$control.find('.runtime > .tablewrapper > table')).colResizable();
        me.setGridBrowseMode($control);
    }
    //---------------------------------------------------------------------------------
    addGridMenuButtons(options: IAddGridMenuOptions) {
        if (typeof options.gridSecurityId !== 'string' || options.gridSecurityId.length == 0) {
            throw 'options.gridSecurityId is required';
        }
        const nodeGrid = FwApplicationTree.getNodeById(FwApplicationTree.tree, options.gridSecurityId);
        let nodeGridActions = null, nodeGridEdit = null, nodeGridDelete = null;
        if (nodeGrid !== null) {
            nodeGridActions = FwApplicationTree.getNodeByFuncRecursive(nodeGrid, {}, (node: any, args2: any) => {
                return (node.nodetype === 'ControlActions') || (node.nodetype === 'ModuleActions');
            });
            if (nodeGridActions !== null) {
                nodeGridEdit = FwApplicationTree.getNodeByFuncRecursive(nodeGridActions, {}, (node: any, args2: any) => {
                    return (node.nodetype === 'ControlAction' && (node.properties.action === 'ControlEdit' || node.properties.action === 'ControlSave')) || (node.nodetype === 'ModuleAction' && (node.properties.action === 'Edit' || node.properties.action === 'Save'));
                });
                nodeGridDelete = FwApplicationTree.getNodeByFuncRecursive(nodeGridActions, {}, (node: any, args2: any) => {
                    return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlDelete') || (node.nodetype === 'ModuleAction' && node.properties.action === 'Delete');
                });
            }
        }

        if (typeof options.hasDelete === 'boolean' && options.hasDelete && nodeGridDelete !== null && nodeGridDelete.properties.visible === 'T') {
            const $submenuitem = FwGridMenu.addSubMenuBtn(options.$groupActions, 'Delete Selected', nodeGridDelete.id);
            $submenuitem.on('click', (e: JQuery.Event) => {
                try {
                    if (options.$browse.attr('data-enabled') !== 'false') {
                        try {
                            e.stopPropagation();
                            var $selectedCheckBoxes = options.$browse.find('tbody .cbselectrow:checked');
                            if ($selectedCheckBoxes.length === 0) {
                                FwFunc.showMessage('Select one or more rows to delete!');
                            } else {
                                var $confirmation = FwConfirmation.yesNo('Delete Record' + ($selectedCheckBoxes.length > 1 ? 's' : ''), 'Delete ' + $selectedCheckBoxes.length + ' record' + ($selectedCheckBoxes.length > 1 ? 's' : '') + '?',
                                    //on yes
                                    async () => {
                                        const recordCount = $selectedCheckBoxes.length;
                                        const $confirmation = FwConfirmation.renderConfirmation('Deleting...', '');
                                        FwConfirmation.addControls($confirmation, `<div style="text-align:center;"><progress class="progress" max="${recordCount}" value="0"></progress></div><div style="margin:10px 0 0 0;text-align:center;">Deleting Record <span class="recordno">1</span> of ${recordCount}<div>`);
                                        try {
                                            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                                                $confirmation.find('.recordno').html((i + 1).toString());
                                                $confirmation.find('.progress').attr('value', (i + 1).toString());
                                                const $tr = $selectedCheckBoxes.eq(i).closest('tr');
                                                await this.deleteRecord(options.$browse, $tr);
                                            }
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                        finally {
                                            FwConfirmation.destroyConfirmation($confirmation);
                                            await this.databind(options.$browse);
                                        }
                                    },
                                    // on no
                                    () => {
                                        // do nothing
                                    });

                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    } else {
                        FwFunc.showMessage('This grid is disabled.');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }


        if (typeof options.hasEdit === 'boolean' && options.hasEdit && nodeGridEdit !== null && nodeGridEdit.properties.visible === 'T') {
            options.$browse.data('hasedit', true);
        }
        FwGridMenu.addCaption(options.$menu, options.$browse.attr('data-caption'));
        if (typeof options.hasNew === 'boolean' && options.hasNew && nodeGridEdit !== null && nodeGridEdit.properties.visible === 'T') {
            var $new = FwGridMenu.addStandardBtn(options.$menu, FwLanguages.translate('New'), nodeGridEdit.id);
            $new.attr('data-type', 'NewButton');
            $new.on('click', (e: JQuery.Event) => {
                try {
                    e.stopPropagation();
                    let $form = options.$browse.closest('.fwform');
                    let mode = $form.attr('data-mode');
                    if (options.$browse.attr('data-enabled') !== 'false') {
                        if ((mode === 'EDIT') || ($new.closest('.fwconfirmation').length > 0)) {
                            if (options.$browse.hasClass('sort-mode')) {
                                FwNotification.renderNotification('WARNING', 'Please exit sort mode before adding new records.');
                            } else {
                                if (typeof $new.data('onclick') === 'function') {
                                    $new.data('onclick')(options.$browse);
                                } else {
                                    this.addRowNewMode(options.$browse);
                                }
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

        if (options.hasDownloadExcel) {
            const gridSecurityId = options.$browse.data('secid');
            const gridName = options.$browse.data('name');
            FwMenu.addSubMenuItem(options.$groupExport, 'Download Excel Workbook (*.xlsx)', gridSecurityId, (e: JQuery.ClickEvent) => {
                try {
                    FwBrowse.downloadExcelWorkbook(options.$browse, gridName + 'Controller');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        FwMenu.applyGridSecurity(options, options.gridSecurityId);
        FwMenu.cleanupMenu(options.$menu);
    }
    //---------------------------------------------------------------------------------
    addFilterPanel($control, $filterpanel) {
        $control.find('.fwbrowsefilter').empty().append($filterpanel).show();
        var fwcontrols = $filterpanel.find('.fwcontrol');
        FwControl.renderRuntimeControls(fwcontrols);
    }
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control) {
        var html, data_rendermode, $ths;
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
        switch (data_rendermode) {
            case 'designer':
                html = [];
                $ths = $control.find('.designer > thead td.column')
                $ths.each(function (index, th) {
                    var $th, caption, cssclass, browsedatafield, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    $th = jQuery(th);
                    //width   = $th.attr('data-width');
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
                $ths = $control.find('.runtime thead td.column')
                $ths.each(function (index, th) {
                    var $th, caption, cssclass, browsedatafield, browsedatatype, formdatafield, formdatatype, width, visible, $fields;
                    $th = jQuery(th);
                    //width   = $th.attr('data-width');
                    width = 'auto';
                    visible = $th.attr('data-visible');
                    html.push('<div class="column" ' + /*'data-width="' + width*/ + '" data-visible="' + visible + '">');
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
    }
    //---------------------------------------------------------------------------------
    screenload($control) {
        if (typeof $control.data('onscreenload') === 'function') {
            $control.data('onscreenload')();
        }
    }
    //---------------------------------------------------------------------------------
    screenunload($control) {
        if (typeof $control.data('onscreenunload') === 'function') {
            $control.data('onscreenunload')();
        }
    }
    //---------------------------------------------------------------------------------
    getRequest($control: JQuery): BrowseRequest {
        var $fields, orderby, $field, $txtSearch, browsedatafield, value, sort, module, controller, fieldtype, searchSeparator, sortSequence;
        orderby = [];
        let request = new BrowseRequest();
        request.module = '';
        request.searchfields = [];
        request.searchfieldtypes = [];
        request.searchseparators = [];
        request.searchfieldoperators = [];
        request.searchfieldvalues = [];
        request.searchcondition = [];
        request.searchconjunctions = [];
        request.miscfields = !$control.closest('.fwform').length ? jQuery([]) : FwModule.getFormUniqueIds($control.closest('.fwform'));
        request.orderby = '';
        request.pageno = parseInt($control.attr('data-pageno'));
        request.pagesize = parseInt($control.attr('data-pagesize'));
        request.options = this.getOptions($control);

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
            throw request.module + 'Controller is not defined.'
        }
        $fields = $control.find('.runtime thead > tr.fieldnames > td.column > div.field');
        $fields.each(function (index, element) {
            $field = jQuery(element);
            $txtSearch = $field.find('> div.search > input');
            value = $txtSearch.val();
            sort = $field.attr('data-sort');
            sortSequence = $field.attr('data-sortsequence');
            fieldtype = $field.attr('data-browsedatatype') || $field.attr('data-datatype');

            if (typeof $field.attr('data-searchfield') !== 'undefined') {
                browsedatafield = $field.attr('data-searchfield');
            } else {
                if (fieldtype === "validation") {
                    browsedatafield = $field.attr('data-browsedisplayfield') || $field.attr('data-displayfield');
                } else {
                    browsedatafield = $field.attr('data-browsedatafield') || $field.attr('data-datafield');
                }
            }
            searchSeparator = $field.attr('data-multiwordseparator') || ",";

            if (value.length > 0) {
                request.searchfields.push(browsedatafield);
                request.searchfieldtypes.push(fieldtype);
                request.searchseparators.push(searchSeparator);
                if (typeof $field.attr('data-searchcondition') !== 'undefined') {
                    request.searchcondition.push($field.attr('data-searchcondition'));
                } else {
                    request.searchcondition.push("and");
                }
                if ($field.attr('data-searchfieldoperators') === 'startswith') {
                    request.searchfieldoperators.push('startswith');
                } else {
                    request.searchfieldoperators.push('like');
                }
                request.searchfieldvalues.push(value);
            }

            if (sort === 'asc' || sort === 'desc') {
                orderby.push({
                    Field: `${browsedatafield} ${sort}`
                    , SortSequence: sortSequence
                });
            } else if (typeof sortSequence != 'undefined' && sort != 'off') {
                orderby.push({
                    Field: `${browsedatafield}`
                    , SortSequence: sortSequence
                });
            };
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
                        request.searchfieldtypes.push('Text');
                        request.searchfieldoperators.push('<>');
                        request.searchfieldvalues.push('T');
                    } else {
                        request.searchfields.push('inactive');
                        request.searchfieldoperators.push('<>');
                        request.searchfieldvalues.push('T');
                    }
                    break;
                case 'inactive':
                    if ((typeof controller !== 'undefined') && (typeof controller.apiurl === 'string')) {
                        request.searchfields.push('Inactive');
                        request.searchfieldtypes.push('Text');
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
        if ($control.data('advancedsearchrequest') !== undefined) {
            let advancedSearch = $control.data('advancedsearchrequest');
            request.searchfieldoperators = request.searchfieldoperators.concat(advancedSearch.searchfieldoperators);
            request.searchfields = request.searchfields.concat(advancedSearch.searchfields);
            request.searchfieldtypes = request.searchfieldtypes.concat(advancedSearch.searchfieldtypes);
            request.searchfieldvalues = request.searchfieldvalues.concat(advancedSearch.searchfieldvalues);
            request.searchseparators = request.searchseparators.concat(advancedSearch.searchseparators);
            request.searchconjunctions = request.searchconjunctions.concat(advancedSearch.searchconjunctions);
        }
        //sort orderby list by sequence, map to return only the field, and join into a string
        request.orderby = orderby
            .sort((a, b) => (a.SortSequence > b.SortSequence) ? 1 : ((b.SortSequence > a.SortSequence) ? -1 : 0))
            .map(a => a.Field)
            .join();

        if (typeof $control.data('ondatabind') === 'function') {
            $control.data('ondatabind')(request);  // you can attach an ondatabind function to the browse control if you need to add custom parameters to the request
        }
        return request;
    }
    //---------------------------------------------------------------------------------
    getManyRequest($control: JQuery): GetManyRequest {
        let request = new GetManyRequest();
        request.pageno = parseInt($control.attr('data-pageno'));
        request.pagesize = parseInt($control.attr('data-pagesize'));
        //request.options = this.getOptions($control);
        let orderby: any = [];
        let module = '';
        if ($control.attr('data-type') === 'Grid') {
            module = $control.attr('data-name');
        } else if ($control.attr('data-type') === 'Validation') {
            module = $control.attr('data-name');
        } else if ($control.attr('data-type') === 'Browse') {
            if (typeof $control.attr('data-name') !== 'undefined') {
                module = $control.attr('data-name');
            } else {
                module = window[$control.attr('data-controller')].Module;
            }
        }
        let controller = window[module + 'Controller'];
        if (typeof controller === 'undefined' && ($control.attr('data-type') === 'Grid' || $control.attr('data-type') === 'Browse')) {
            throw module + 'Controller is not defined.'
        }
        let $fields = $control.find('.runtime thead > tr.fieldnames > td.column > div.field');
        $fields.each(function (index, element) {
            let $field = jQuery(element);
            let $txtSearch = $field.find('> div.search > input');
            let value = $txtSearch.val().toString();
            let sort = $field.attr('data-sort');
            let fieldtype = $field.attr('data-browsedatatype');
            let browsedatafield: string = '';
            let searchSeparator = ',';
            if (typeof $field.attr('data-datafield') !== 'undefined') {
                browsedatafield = $field.attr('data-datafield');
            }
            else if (typeof $field.attr('data-browsedatafield') !== 'undefined') {
                browsedatafield = $field.attr('data-browsedatafield');
            }
            if (typeof $field.attr('data-multiwordseparator') !== 'undefined') {
                searchSeparator = $field.attr('data-multiwordseparator');
            } else {
                searchSeparator = ",";
            }
            if (typeof $field.attr('data-browsedatatype') !== 'undefined') {
                fieldtype = $field.attr('data-browsedatatype');
            } else if (typeof $field.attr('data-datatype') !== 'undefined') {
                fieldtype = $field.attr('data-datatype');
            }
            if (value.length > 0) {
                let filter = new GetManyFilter();
                filter.fieldName = browsedatafield;
                filter.comparisonOperator = (typeof $field.attr('data-comparisonoperator') !== 'undefined') ? $field.attr('data-comparisonoperator') : 'co';
                filter.fieldValue = value;
                filter.searchSeparator = searchSeparator;
                filter.fieldType = fieldtype;
                request.filters.push(filter);
            }
            if (sort === 'asc') {
                orderby.push(browsedatafield + ':asc');
            }
            if (sort === 'desc') {
                orderby.push(browsedatafield + ':desc');
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
                case 'active': {
                    let filter = new GetManyFilter();
                    filter.fieldName = 'Inactive';
                    filter.comparisonOperator = 'ne';
                    filter.fieldValue = 'T';
                    request.filters.push(filter);
                    break;
                }
                case 'inactive': {
                    let filter = new GetManyFilter();
                    filter.fieldName = 'Inactive';
                    filter.comparisonOperator = 'eq';
                    filter.fieldValue = 'T';
                    request.filters.push(filter);
                    break;
                }
            }
        }
        orderby = orderby.join(',');
        request.sort = orderby;
        if (typeof $control.data('beforegetmany') === 'function') {
            $control.data('beforegetmany')(request);  // you can attach an ondatabind function to the browse control if you need to add custom parameters to the request
        }
        return request;
    }
    //---------------------------------------------------------------------------------
    async databind($control): Promise<any> {
        let me = this;
        return new Promise<any>(async (resolve, reject) => {
            try {
                jQuery(window).off('click.FwBrowse'); // remove the auto-save click event from window

                if ($control.length > 0) {
                    let request = this.getRequest($control);
                    if (typeof $control.data('calldatabind') === 'function') {
                        $control.data('calldatabind')(request, function (response) {
                            resolve();
                        });
                    } else {
                        if ($control.attr('data-type') === 'Grid') {
                            FwServices.grid.method(request, request.module, 'Browse', $control, function (response) {
                                try {
                                    me.beforeDataBindCallBack($control, request, response);
                                    resolve();
                                } catch (ex) {
                                    //FwFunc.showError(ex);
                                    reject(ex);
                                }
                            });
                        } else if ($control.attr('data-type') === 'Validation') {
                            let validationmode = $control.data('validationmode');
                            if (validationmode !== undefined && validationmode === 2) {
                                try {
                                    try {
                                        let getManyRequest = this.getManyRequest($control);
                                        let url = Array<string>();
                                        url.push(`${applicationConfig.apiurl}${$control.attr('data-apiurl')}?`);
                                        url.push(`&pageno=${getManyRequest.pageno}`);
                                        url.push(`&pagesize=${getManyRequest.pagesize}`);
                                        url.push(`&sort=${getManyRequest.sort}`);
                                        for (let filterno = 0; filterno < getManyRequest.filters.length; filterno++) {
                                            let filter = getManyRequest.filters[filterno];
                                            url.push(`&${filter.fieldName}=${filter.comparisonOperator}:${filter.fieldValue}`);
                                        }
                                        let request = new FwAjaxRequest();
                                        request.httpMethod = 'GET';
                                        request.url = encodeURI(url.join(''));
                                        request.timeout = 15000;
                                        request.$elementToBlock = $control.data('$control');
                                        request.addAuthorizationHeader = true;
                                        let getManyResponse = await FwAjax.callWebApi<any, GetManyModel<any>>(request);
                                        let dt = DataTable.objectListToDataTable(getManyResponse);
                                        me.beforeDataBindCallBack($control, request, dt);
                                        resolve();
                                    } catch (ex) {
                                        reject(ex);
                                    }
                                } catch (ex) {
                                    //FwFunc.showError(ex);
                                    reject(ex);
                                }
                            } else {
                                FwServices.validation.method(request, request.module, 'Browse', $control, function (response) {
                                    // replace spinner with search again
                                    $control.data('$control').find('.validation-loader').hide();
                                    $control.data('$btnvalidate').show();
                                    try {
                                        me.beforeDataBindCallBack($control, request, response);
                                        resolve();
                                    } catch (ex) {
                                        //FwFunc.showError(ex);
                                        reject(ex);
                                    }
                                });
                            }
                        } else if ($control.attr('data-type') === 'Browse') {
                            FwServices.module.method(request, request.module, 'Browse', $control, function (response) {
                                try {
                                    me.beforeDataBindCallBack($control, request, response);
                                    resolve();
                                } catch (ex) {
                                    //FwFunc.showError(ex);
                                    reject(ex);
                                }
                            });
                        } else {
                            reject('Unknown Browse Control type.');
                        }
                    }
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //---------------------------------------------------------------------------------
    beforeDataBindCallBack($control: JQuery, request: any, response: any) {
        //var controller = window[request.module + 'Controller'];
        //if (typeof controller === 'undefined') {
        //    throw request.module + 'Controller is not defined.'
        //}
        this.databindcallback($control, response);
    }
    //---------------------------------------------------------------------------------
    databindcallback($control, dt: FwJsonDataTable) {
        let me = this;
        try {
            this.setGridBrowseMode($control);
            let pageSize = this.getPageSize($control);
            this.setTotalPages($control, dt.TotalPages);
            $control.data('totalRowCount', dt.TotalRows);

            //clear select all box
            $control.find('thead .cbselectrow').prop('checked', false);

            var controller = $control.attr('data-controller');
            if (typeof controller === 'undefined') {
                if (typeof $control.data('name') !== 'undefined') {
                    controller = $control.data('name') + 'Controller';
                } else {
                    return;
                }
            }
            let nodeModule = null, nodeActions = null, nodeView = null, nodeEdit = null, nodeSave = null;
            if ($control.data('type') === 'Browse') {
                if (window[controller] === null) {
                    throw `Controller: ${controller} is not defined.`;
                }
                nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, (<any>window)[controller].id);
            } else if ($control.data('type') === 'Grid') {
                const gridSecurityId = $control.data('secid');
                if (typeof gridSecurityId === 'string' && gridSecurityId.length > 0) {
                    nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, gridSecurityId);
                }
            }
            if (nodeModule !== null) {
                nodeActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, function (node, args) {
                    if ($control.data('type') === 'Browse') {
                        return (node.nodetype === 'ModuleActions');
                    }
                    else if ($control.data('type') === 'Grid') {
                        return (node.nodetype === 'ControlActions') || (node.nodetype === 'ModuleActions');
                    }
                    return false;
                });
            }
            if (nodeActions !== null) {
                if ($control.attr('data-type') === 'Browse') {
                    nodeView = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, function (node, args) {
                        return ((node.nodetype === 'ModuleAction' && node.properties.action === 'View'));
                    });
                }
                nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, function (node, args) {
                    return ((node.nodetype === 'ModuleAction' && node.properties.action === 'Edit') || (node.nodetype === 'ControlAction' && node.properties.action === 'ControlEdit'));
                });
                nodeSave = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, function (node, args) {
                    return ((node.nodetype === 'ModuleAction' && node.properties.action === 'Save') || (node.nodetype === 'ControlAction' && node.properties.action === 'ControlSave'));
                });
            }

            let onrowdblclick = $control.data('onrowdblclick');
            dt.ColumnIndex = {};
            for (let i = 0; i < dt.Columns.length; i++) {
                let dtCol = dt.Columns[i];
                dt.ColumnIndex[dtCol.DataField] = i;
            }
            let $tbody = $control.find('.runtime tbody').remove();
            $tbody.empty();
            for (let rowIndex = 0; rowIndex < dt.Rows.length; rowIndex++) {
                var $tr;
                $tr = this.generateRow($control);
                if ($control.attr('data-type') === 'Browse' || $control.attr('data-type') === 'Validation') {
                    $tr.attr('tabindex', '0')
                }
                $tr.addClass('viewmode');

                let $fields = $tr.find('.field');
                for (var j = 0; j < $fields.length; j++) {
                    var $field, dtColIndex, dtRow, dtCellValue, $td;
                    $field = jQuery($fields[j]);
                    $td = $field.parent('td');
                    dtColIndex = dt.ColumnIndex[$field.attr('data-browsedatafield')];
                    dtRow = dt.Rows[rowIndex];
                    dtCellValue = dtRow[dtColIndex];
                    if ($field.attr('data-formreadonly') !== 'true') {
                        if (typeof $control.data('isfieldeditable') === 'function' && $control.data('isfieldeditable')($field, dt, rowIndex)) {
                            //do nothing
                        } else if ((nodeEdit !== null && nodeEdit.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T')) {
                            $field.addClass('editablefield');
                        }
                    }
                    if (typeof dtCellValue !== 'undefined' && dtCellValue != null) {
                        $field.attr('data-originalvalue', dtCellValue.toString());
                    } else {
                        $field.attr('data-originalvalue', '');
                    }

                    var cellcolor = $field.attr('data-cellcolor');
                    if (typeof cellcolor !== 'undefined') {
                        $td.children().css('padding-left', '10px');
                        if ((cellcolor.length > 0) && ((dtRow[dt.ColumnIndex[cellcolor]]) !== null) && ((dtRow[dt.ColumnIndex[cellcolor]]) != "")) {
                            if (typeof dt.ColumnIndex[cellcolor] !== 'number') {
                                throw 'FwBrowse.databindcallback: cellcolor: "column ' + cellcolor + '" was not returned by the web service.';
                            }
                            let css = {
                                'position': 'relative',
                                'border-top-color': dtRow[dt.ColumnIndex[cellcolor]],
                                'border-top-style': 'none',
                                'z-index': 0
                            };
                            $td.addClass('cellColor').css(css);
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
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[halfcellcolor]] + ', rgba(245, 245, 245, 1)50%)'
                                };
                            } else {
                                var css = {
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
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[fullcellcolor]] + ', rgba(245, 245, 245, 1))'
                                }
                            } else {
                                var css = {
                                    'background': 'linear-gradient(to bottom, ' + dtRow[dt.ColumnIndex[fullcellcolor]] + ', rgba(255, 255, 255, 0))'
                                }
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

                    if ($field.attr('data-browsedatatype') === 'number') {
                        if (typeof window['FwBrowseColumn_' + $field.attr('data-browsedatatype')] !== 'undefined') {
                            if (typeof window['FwBrowseColumn_' + $field.attr('data-browsedatatype')].renderRuntimeHtml === 'function') {
                                window['FwBrowseColumn_' + $field.attr('data-browsedatatype')].renderRuntimeHtml($control, $tr, $field);
                            }
                        }
                    } else {
                        this.setFieldViewMode($control, $tr, $field);
                    }
                    //this.setFieldViewMode($control, $tr, $field);

                    // if you want to dynamically change something on a .field or td:
                    const AFTER_RENDER_FIELD = 'afterrenderfield';
                    if (typeof $control.data(AFTER_RENDER_FIELD) === 'function') {
                        let funcAfterRenderField: ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => void = ($control.data(AFTER_RENDER_FIELD));
                        funcAfterRenderField($tr, $td, $field, dt, rowIndex, dtColIndex);
                    }
                }

                if (((typeof dt.ColumnIndex['inactive'] === 'number') && (dt.Rows[rowIndex][dt.ColumnIndex['inactive']] === 'T')) ||
                    ((typeof dt.ColumnIndex['Inactive'] === 'number') && (dt.Rows[rowIndex][dt.ColumnIndex['Inactive']] === true))) {
                    $tr.addClass('inactive');
                }
                $tbody.append($tr);

                // if you want to dynamically change something on a tr:
                const AFTER_RENDER_ROW = 'afterrenderrow';
                if (typeof $control.data(AFTER_RENDER_ROW) === 'function') {
                    let funcAfterRenderRow: ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => void = $control.data(AFTER_RENDER_ROW);
                    funcAfterRenderRow($tr, dt, rowIndex);
                }
            }

            $control.find('.runtime table').append($tbody);

            if ($control.attr('data-type') === 'Grid') {
                if ($control.attr('data-manualsorting') === 'true') {
                    this.addManualSorting($control);
                }
                var $trs = $control.find('tbody tr');
                //$trs.on('blur', '.value', function (event) {
                //    try {
                //        var $tr = jQuery(event.delegateTarget);
                //        $control.data('blurredFrom', event.delegateTarget);
                //        $control.data('delayedFn', setTimeout(function () {
                //            try {
                //                //console.log('Blurred');
                //                me.saveRow($control, $tr);
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
                // ----------
                $control.find('tbody tr').on('click', function (e: JQuery.Event) {
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
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                // ----------
                $control.find('tbody .browsecontextmenu').on('click', function (e: JQuery.Event) {
                    try {
                        //e.stopPropagation();
                        const $browse = jQuery(this).closest('.fwbrowse');
                        //const $fwgrid = $browse.closest('div[data-control="FwGrid"]');
                        const $fwcontextmenus = $browse.find('tbody .fwcontextmenu');
                        for (let i = 0; i < $fwcontextmenus.length; i++) {
                            FwContextMenu.destroy($fwcontextmenus.eq(i));
                        }
                        let menuItemCount = 0;
                        //me.unselectAllRows($control);
                        //me.selectRow($control, $tr, true);
                        const $browsecontextmenu = jQuery(this);
                        const $contextmenu = FwContextMenu.render('Options', 'bottomleft', $browsecontextmenu, e);
                        //$contextmenu.data('beforedestroy', function () {
                        //    me.unselectRow($control, $tr);
                        //});
                        const controller = $control.attr('data-controller');
                        if (typeof controller === 'undefined') {
                            throw 'Attribute data-controller is not defined on Browse control.'
                        }
                        // Delete menu option
                        if ($browse.attr('data-enabled') !== 'false' && $browse.attr('data-deleteoption') !== 'false' &&
                            ((nodeEdit !== null && nodeEdit.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'))) {
                            const nodeGrid = FwApplicationTree.getNodeById(nodeModule, $browse.data('secid'));
                            if (nodeGrid !== null) {
                                const nodeGridActions = FwApplicationTree.getNodeByFuncRecursive(nodeGrid, {}, (node: any, args: any) => {
                                    return (node.nodetype === 'ControlActions' || node.nodetype === 'ModuleActions');
                                });
                                if (nodeGridActions !== null) {
                                    const nodeGridDelete = FwApplicationTree.getNodeByFuncRecursive(nodeGrid, {}, (node: any, args: any) => {
                                        return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlDelete') || (node.nodetype === 'ModuleAction' && node.properties.action === 'Delete');
                                    });
                                    if (nodeGridDelete !== null && nodeGridDelete.properties.visible === 'T') {
                                        FwContextMenu.addMenuItem($contextmenu, 'Delete', function () {
                                            try {
                                                const $tr = jQuery(this).closest('tr');
                                                me.deleteRow($control, $tr);
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                        menuItemCount++;
                                    }
                                }
                            }
                        }

                        const ADD_CONTEXT_MENU_OPTIONS = 'contextmenuoptions';
                        if (typeof $browsecontextmenu.data(ADD_CONTEXT_MENU_OPTIONS) === 'function') {
                            let funcAddContextMenuOptions: ($tr: JQuery) => void = ($browsecontextmenu.data(ADD_CONTEXT_MENU_OPTIONS));
                            const $tr = jQuery(this).closest('tr');
                            funcAddContextMenuOptions($tr);
                            menuItemCount++;
                        }

                        // Audit history menu option
                        if ($browse.attr('data-hasaudithistory') !== 'false') {
                            var nodeAuditGrid = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'xepjGBf0rdL');
                            if (nodeAuditGrid !== null && nodeAuditGrid.properties.visible === 'T') {
                                FwContextMenu.addMenuItem($contextmenu, 'Audit History', () => {
                                    try {
                                        const $tr = jQuery(this).closest('tr');
                                        me.renderAuditHistoryPopup($tr);
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
                });
                // ----------
                $control.find('tbody tr .btnpeek').on('click', function (e: JQuery.Event) {
                    try {
                        let $this = jQuery(this);
                        let $td = $this.parent();
                        $this.css('visibility', 'hidden');
                        $td.find('.validation-loader').show();
                        setTimeout(function () {
                            const validationName = $td.attr('data-peekForm') || $td.data('validationname').slice(0, -10);
                            FwValidation.validationPeek($control, validationName, $td.data('originalvalue'), $td.data('browsedatafield'), null, $td.data('originaltext'));
                            $this.css('visibility', 'visible');
                            $td.find('.validation-loader').hide();
                        })

                    } catch (ex) {
                        FwFunc.showError(ex)
                    }
                    e.stopPropagation();
                });
                // ----------
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
                        if ($control.attr('data-type') === 'Grid' && $control.attr('data-enabled') !== 'false' && !$tr.hasClass('editmode') && !$tr.find('.manual-sort').is(':visible')) {
                            me.setRowEditMode($control, $tr);
                            $field.find('.value').focus();
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                //$trs.on('focusout', function (e) {
                //    try {

                //        var $tr = jQuery(this);
                //        me.saveRow($control, $tr);
                //    } catch (ex) {
                //        FwFunc.showError(ex);
                //    }
                //});
            }

            // set the spacer row height;
            let spacerHeight = 0;
            //let rowHeight = $control.find('tbody .tr').eq(1).height();
            //if (rowHeight === 0) {
            //    rowHeight = 33;
            //}
            if (pageSize <= 15) {
                spacerHeight = 25 * (pageSize - dt.Rows.length);
            } else {
                spacerHeight = 25 * (15 - dt.Rows.length);
            }
            if (spacerHeight > 0) {
                $control.find('.runtime tfoot tr.spacerrow > td > div').show().height(spacerHeight);
            } else {
                $control.find('.runtime tfoot tr.spacerrow > td > div').hide();
            }

            // update pager
            let rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
            let rownoend = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
            if ((pageSize > 0) && (dt.PageNo > 1)) {
                $control.find('.pager .btnFirstPage')
                    .attr('data-enabled', 'true')
                    .prop('disabled', false);
                $control.find('.pager .btnPreviousPage')
                    .attr('data-enabled', 'true')
                    .prop('disabled', false);
            } else {
                $control.find('.pager .btnFirstPage')
                    .attr('data-enabled', 'false')
                    .prop('disabled', true);
                $control.find('.pager .btnPreviousPage')
                    .attr('data-enabled', 'false')
                    .prop('disabled', true);
            }
            if (dt.TotalPages > 0) {
                $control.find('.txtPageNo').val(dt.PageNo);
            } else {
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
            } else {
                $control.find('.pager .btnNextPage')
                    .attr('data-enabled', 'false')
                    .prop('disabled', true);
                $control.find('.pager .btnLastPage')
                    .attr('data-enabled', 'false')
                    .prop('disabled', true);
            }
            let controlType = $control.attr('data-type');
            switch (controlType) {
                case 'Browse':
                    if ((rownoend === 0) && (dt.TotalRows === 0)) {
                        $control.find('.pager .count').text(dt.TotalRows + ' rows');
                    } else {
                        if (dt.TotalPages == 1) {
                            $control.find('.pager .count').text(dt.TotalRows + ' rows');
                        } else {
                            $control.find('.pager .count').text(rownostart + ' to ' + rownoend + ' of ' + dt.TotalRows + ' rows');
                        }
                    }
                    break;
                case 'Grid':
                    if ($control.attr('data-paging') == 'true') {
                        if ((rownoend === 0) && (dt.TotalRows === 0)) {
                            $control.find('.pager .count').text(dt.TotalRows + ' rows');
                        } else {
                            if (dt.TotalPages == 1) {
                                $control.find('.pager .count').text(dt.TotalRows + ' rows');
                            } else {
                                $control.find('.pager .count').text(rownostart + ' to ' + rownoend + ' of ' + dt.TotalRows + ' rows');
                            }
                        }
                    } else {
                        $control.find('.pager .count').text(dt.TotalRows + ' row(s)');
                    }
                    break;
                case 'Validation':
                    $control.find('.pager .count').hide();
                    $control.find('.pager .show-all').text(`Show All ${dt.TotalRows} rows`);

                    const isMultiSelect = $control.attr('data-multiselectvalidation');
                    if (isMultiSelect) {
                        if (dt.TotalPages <= 1) {
                            $control.find('.pager .show-all').hide();
                        } else {
                            $control.find('.pager .show-all').show();
                        }
                    } else {
                        $control.find('.pager .show-all').hide();
                    }
                    break;
            }

            if (typeof onrowdblclick !== 'undefined') {
                $control.find('.runtime tbody').on('dblclick', '> tr', (event: JQuery.DoubleClickEvent) => {
                    let $tr = jQuery(event.target);

                    $tr.addClass('selected');
                    if ((nodeView !== null && nodeView.properties.visible === 'T') ||
                        (nodeEdit !== null && nodeEdit.properties.visible === 'T') ||
                        (nodeSave !== null && nodeSave.properties.visible === 'T')) {
                        onrowdblclick.apply(event.currentTarget, [event]);
                    }

                    if ($control.attr('data-type') === 'Validation') {
                        if ($tr.data('onrowdblclick')) {  //prevents event from being applied multiple times to the same control, fixes issue with .data('onchange') events triggering multiple times
                            return false;
                        } else {
                            $tr.data('onrowdblclick', true);
                            onrowdblclick.apply(event.currentTarget, [event]);
                        }
                    }
                });
            }

            if ((typeof $control.attr('data-type') === 'string') && ($control.attr('data-type') === 'Validation')) {
                FwValidation.validateSearchCallback($control);
            }

            setTimeout(function () {
                var selectedindex = me.getSelectedIndex($control);
                var rowcount = me.getRowCount($control);
                if (rowcount > me.getSelectedIndex($control) && selectedindex !== -1) {
                    let $tr = me.selectRowByIndex($control, selectedindex);
                    let selectedRowMode = me.getSelectedRowMode($control);
                    switch (selectedRowMode) {
                        case 'view':
                            me.setRowViewMode($control, $tr);
                            break;
                        case 'new':
                            me.setRowNewMode($control, $tr);
                            break;
                        case 'edit':
                            me.setRowEditMode($control, $tr);
                            break;
                    }
                } else if (rowcount < selectedindex) {
                    var lastrowindex = rowcount - 1;
                    me.selectRowByIndex($control, lastrowindex);
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
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //---------------------------------------------------------------------------------
    generateRow($control) {
        const $table = $control.find('table');
        const hasMultiRowSelect = $control.attr('data-hasmultirowselect') === 'true';
        let $tr;

        if ($control.data('trtemplate') != undefined) {
            $tr = $control.data('trtemplate').clone();
        } else {
            $tr = jQuery('<tr>');
            const $theadtds = $table.find('> thead > tr.fieldnames > td.column');
            for (let i = 0; i < $theadtds.length; i++) {
                let $theadtd = $theadtds.eq(i);
                let $td = $theadtd.clone().empty();
                //$td.css({ 'min-width': width });
                $tr.append($td);
                var $theadfields = $theadtd.children('.field');
                $theadfields.each(function (index, element) {
                    var $theadfield, $field, $field_newmode, formdatatype;
                    $theadfield = jQuery(element);
                    $field = $theadfield.clone().empty();
                    $td.append($field);
                });
            }

            if ($control.attr('data-type') === 'Grid') {
                if ($control.attr('data-manualsorting') === 'true') {
                    $tr.find('.manual-sort').append(`<i style="vertical-align:-webkit-baseline-middle; cursor:grab;" class="material-icons drag-handle">drag_handle</i>`);
                }
                $tr.find('.browsecontextmenucell').append('<div class="browsecontextmenu"><i class="material-icons">more_vert</i><div>');
            }

            $control.data('trtemplate', $tr.clone());
        }

        if ((($control.attr('data-type') === 'Browse') && hasMultiRowSelect) || (($control.attr('data-type') === 'Grid') && !hasMultiRowSelect)) {
            const cbuniqueId = FwApplication.prototype.uniqueId(10);
            $tr.find('.tdselectrow').append(`<div class="divselectrow"><input id="${cbuniqueId}" type="checkbox" tabindex="-1" class="cbselectrow" /><label for="${cbuniqueId}" class="lblselect"></label><div>`);
        }
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            let controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['generateRow'] === 'function') {
                window[controller]['generateRow']($control, $tr);
            }
        }

        return $tr;
    }
    //---------------------------------------------------------------------------------
    setGridBrowseMode($control) {
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
            this.setRowViewMode($control, $trEditMode);
        }
    }
    //---------------------------------------------------------------------------------
    addRowNewMode($control) {
        var $table, $tr, $tbody;
        $table = $control.find('.runtime table');
        if ($table.find('> tbody > tr.editrow.newmode').length === 0) {
            $tr = this.generateRow($control);
            $tr.addClass('editrow newmode');
            $tbody = $table.find('> tbody');
            $tbody.prepend($tr);
            this.setRowNewMode($control, $tr);
            this.addSaveAndCancelButtonToRow($control, $tr);

            // auto focus the first input element
            let $inputs = $tr.find('.field input[type!="hidden"]:visible:enabled,select:visible:enabled,textarea:visible:enabled');
            if ($inputs.length > 0) {
                $inputs.eq(0).focus();
            }
        }
    }
    //---------------------------------------------------------------------------------
    setRowNewMode($control, $tr) {
        let me = this;
        this.beforeNewOrEditRow($control, $tr);
        var $fields, $inputs;
        $control.attr('data-mode', 'EDIT');
        $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
        //$control.find('.gridmenu .buttonbar div[data-type="EditButton"]').hide();
        //$control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').hide();

        $fields = $tr.find('.field');
        $fields.each(function (index, element) {
            var $field = jQuery(element);
            $field.attr('data-originalvalue', '');
            if ($field.attr('data-formreadonly') === 'true') {
                me.setFieldViewMode($control, $tr, $field);
            } else {
                me.setFieldEditMode($control, $tr, $field);
            }

            if ($control.attr('data-type') === 'Grid' && $control.attr('data-flexgrid') === 'true') {
                if ($field.attr('data-browsedatatype') !== 'key') {
                    let header = $field.attr('data-caption');
                    $field.parents('td').attr('data-th', `${header}:`);
                }
            }
        });

        // Applies custom defaults to a grid control
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['onRowNewMode'] === 'function') {
                window[controller]['onRowNewMode']($control, $tr);
            }
        }
    }
    //---------------------------------------------------------------------------------
    setRowViewMode($control, $tr) {
        let me = this;
        jQuery(window).off('click.FwBrowse');
        $tr.find('.divsaverow').remove();
        $tr.find('.divcancelsaverow').remove();
        $tr.find('.divselectrow').show();
        $tr.find('.browsecontextmenu').show();
        $tr.removeClass('editmode').removeClass('editrow').addClass('viewmode');
        //$control.find('.gridmenu .buttonbar div[data-type="EditButton"]').show();
        //$control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').show();
        //$control.find('.gridmenu .buttonbar div[data-type="SaveButton"]').hide();
        //$control.find('.gridmenu .buttonbar div[data-type="CancelButton"]').hide();
        $tr.find('> td > .field').each(function (index, field) {
            var $field, html;
            $field = jQuery(field);
            me.setFieldViewMode($control, $tr, $field);
        });

        let $trEditModeRows = $control.find('tbody tr.editmode');
        if ($trEditModeRows.length === 0) {
            $control.find('thead .tdselectrow .divselectrow').show();
            $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').show();
            $control.find('tbody tr .divselectrow').show();
            $control.find('tbody tr .browsecontextmenu').show();
        }
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($control: JQuery, $tr: JQuery, $field: JQuery) {
        let browsedatatype = (typeof $field.attr('data-browsedatatype') === 'string') ? $field.attr('data-browsedatatype') : '';
        if (typeof window['FwBrowseColumn_' + browsedatatype] !== 'undefined') {
            if (typeof window['FwBrowseColumn_' + browsedatatype].setFieldViewMode === 'function') {
                window['FwBrowseColumn_' + browsedatatype].setFieldViewMode($control, $tr, $field);
            }
        }

        if ($control.attr('data-type') === 'Grid' && $control.attr('data-flexgrid') === 'true') {
            if ($field.attr('data-browsedatatype') !== 'key') {
                let header = $field.attr('data-caption');
                $field.parents('td').attr('data-th', `${header}:`);
            }
        }
    }
    //---------------------------------------------------------------------------------
    cancelEditMode($control: JQuery, $tr: JQuery) {
        var $inputFile;
        $inputFile = $tr.find('input[type="file"]');
        if (($inputFile.length > 0) && ($inputFile.val().length > 0)) {
            this.search($control);
        } else {
            if ($tr.hasClass('newmode')) {
                $tr.remove();
            }

            //hide save button
            var $tdselectrow = $tr.find('.tdselectrow');
            $tdselectrow.find('.divsaverow').remove();
            $tdselectrow.find('.divselectrow').show();

            //hide cancel button
            var $browsecontextmenucell = $tr.find('.browsecontextmenucell');
            $browsecontextmenucell.find('.divcancelsaverow').remove();
            $browsecontextmenucell.find('.browsecontextmenu').show();

            this.setRowViewMode($control, $tr);
        }
        if ($control.attr('data-refreshaftercancel') !== undefined && $control.attr('data-refreshaftercancel') === 'true') {
            this.databind($control);
        }
    }
    //---------------------------------------------------------------------------------
    // trigger an auto-save on any rows in new or edit mode
    autoSave($control: JQuery, $trToExclude: JQuery): Promise<any> {
        let $trsNewMode = $control.find('tr.newmode').not($trToExclude);
        let promises = [];
        for (let i = 0; i < $trsNewMode.length; i++) {
            let $trNewMode = $trsNewMode.eq(i);
            //$trNewMode.removeClass('newmode');
            let saveRowPromise = this.saveRow($control, $trNewMode);
            promises.push(saveRowPromise);
        }
        let $trsEditMode = $control.find('tr.editmode').not($trToExclude);
        for (let i = 0; i < $trsEditMode.length; i++) {
            let $trEditMode = $trsEditMode.eq(i);
            let saveRowPromise = this.saveRow($control, $trEditMode);
            promises.push(saveRowPromise);
        }
        return Promise.all(promises);
    };
    //---------------------------------------------------------------------------------
    beforeNewOrEditRow($control: JQuery, $tr: JQuery): Promise<any> {
        return new Promise((resolve, reject) => {
            this.autoSave($control, $tr) // this is actually saving any other rows that were open in new/edit mode and ignoring the row passed in
                .then(() => {
                    if (typeof $control.attr('data-autosave') === 'undefined' || $control.attr('data-autosave') === 'true') {
                        $control.find('thead .tdselectrow .divselectrow').hide();
                        jQuery(window)
                            .off('click.FwBrowse')
                            .on('click.FwBrowse', (e: JQuery.ClickEvent) => {
                                if (typeof $control.attr('data-autosave') === 'undefined' || $control.attr('data-autosave') === 'true') {
                                    try {
                                        let triggerAutoSave = true;
                                        const clockPicker = jQuery(document.body).find('.clockpicker-popover');
                                        if (jQuery(e.target).closest('.fwconfirmation').length > 0 || jQuery(e.target).closest('body').length === 0) {
                                            triggerAutoSave = false;
                                        } else if ((jQuery(e.target).closest('body').length === 0 && jQuery(e.target).find('body').length > 0) || (jQuery(e.target).closest('body').length > 0 && jQuery(e.target).find('body').length === 0)) {
                                            triggerAutoSave = true;
                                        }

                                        if ($control.find('.tablewrapper tbody').get(0).contains(<Node>e.target)) {
                                            triggerAutoSave = false;
                                        }
                                        if (clockPicker.length > 0) {
                                            for (let i = 0; i < clockPicker.length; i++) {
                                                if (clockPicker.css('display') === 'none' && !clockPicker.get(i).contains(<Node>e.target)) {
                                                    triggerAutoSave = true;
                                                } else if (clockPicker.get(i).contains(<Node>e.target)) {
                                                    triggerAutoSave = false;
                                                }
                                            }
                                        }
                                        const datePicker = jQuery(document.body).find('.datepicker');
                                        if (datePicker.length > 0) {
                                            triggerAutoSave = false;
                                        }
                                        if (triggerAutoSave) {
                                            this.saveRow($control, $tr);
                                        }
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                }
                            });
                    }
                    resolve();
                })
                .catch((reason) => {
                    reject(reason);
                });
        });
    }
    //---------------------------------------------------------------------------------
    setRowEditMode($control: JQuery, $tr: JQuery): void {
        const rowIndex = $tr.index();

        if ($control.attr('data-multisave') == 'true') {
            $tr = $control.find('tbody tr').eq(rowIndex);
            $control.attr('data-mode', 'EDIT');
            if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                $tr.removeClass('viewmode').addClass('editmode').addClass('editrow');
                $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
                //$control.find('.gridmenu .buttonbar div[data-type="EditButton"]').hide();
                //$control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').hide();

                const controller = $control.attr('data-controller');
                if (typeof <any>window[controller] === 'undefined') throw `Missing javascript module: ${controller}`;
                if (typeof <any>window[controller]['beforeRowEditMode'] === 'function') {
                    <any>window[controller]['beforeRowEditMode']($control, $tr);
                }
            }

            $tr.find('> td > .field').each((index, element) => {
                const $field = jQuery(element);
                if ($field.attr('data-formreadonly') === 'true') {
                    this.setFieldViewMode($control, $tr, $field);
                } else {
                    this.setFieldEditMode($control, $tr, $field);
                }
            });

            this.addMultiSaveAndCancelButtonToRow($control, $tr);

            if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                const controller = $control.attr('data-controller');
                if (typeof <any>window[controller] === 'undefined') throw `Missing javascript module: ${controller}`;
                if (typeof <any>window[controller]['afterRowEditMode'] === 'function') {
                    <any>window[controller]['afterRowEditMode']($control, $tr);
                }
            }

            if (typeof $control.data('selectedfield') === 'string') {
                const fieldName = $control.data('selectedfield');
                $tr.find(`[data-browsedatafield="${fieldName}"] input`).select();
                $control.data('selectedfield', []);
            }
            else {
                $tr.find('td.column:visible div.editablefield input.text').select();
            }
        } else {
            this.beforeNewOrEditRow($control, $tr)
                .then(() => {
                    $tr = $control.find('tbody tr').eq(rowIndex);
                    $control.attr('data-mode', 'EDIT');
                    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                        $tr.removeClass('viewmode').addClass('editmode').addClass('editrow');
                        $control.find('.gridmenu .buttonbar div[data-type="NewButton"]').hide();
                        //$control.find('.gridmenu .buttonbar div[data-type="EditButton"]').hide();
                        //$control.find('.gridmenu .buttonbar div[data-type="DeleteButton"]').hide();

                        const controller = $control.attr('data-controller');
                        if (typeof <any>window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                        if (typeof <any>window[controller]['beforeRowEditMode'] === 'function') {
                            <any>window[controller]['beforeRowEditMode']($control, $tr);
                        }
                    }

                    $tr.find('> td > .field').each((index, element) => {
                        const $field = jQuery(element);
                        if ($field.attr('data-formreadonly') === 'true') {
                            this.setFieldViewMode($control, $tr, $field);
                        } else {
                            this.setFieldEditMode($control, $tr, $field);
                        }
                    });

                    //$control.attr('data-multisave') == 'true' ? me.addMultiSaveAndCancelButtonToRow($control, $tr) : me.addSaveAndCancelButtonToRow($control, $tr);
                    this.addSaveAndCancelButtonToRow($control, $tr);

                    if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                        const controller = $control.attr('data-controller');
                        if (typeof <any>window[controller] === 'undefined') throw `Missing javascript module: ${controller}`;
                        if (typeof <any>window[controller]['afterRowEditMode'] === 'function') {
                            <any>window[controller]['afterRowEditMode']($control, $tr);
                        }
                    }

                    if (typeof $control.data('selectedfield') === 'string') {
                        const fieldName = $control.data('selectedfield');
                        $tr.find(`[data-browsedatafield="${fieldName}"] input`).select();
                        $control.data('selectedfield', []);
                    }
                });
        }
    }
    //---------------------------------------------------------------------------------
    addSaveAndCancelButtonToRow($control: JQuery, $tr: JQuery): void {
        let me = this;
        // add the save button
        var $browsecontextmenucell = $tr.find('.browsecontextmenucell');
        $tr.closest('tbody').find('.divselectrow').hide();
        var $divsaverow = jQuery('<div class="divsaverow"><i class="material-icons">&#xE161;</i></div>'); //save
        $divsaverow.on('click', function () {
            try {
                var $this = jQuery(this);
                var $tr = $this.closest('tr');
                //let saveRowPromise = me.saveRow($control, $tr);
                me.saveRow($control, $tr); //justin 10/31/2019 RWW#1240 - prevent blank error pop-up when saving a row with missing required fields.
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
                me.cancelEditMode($control, $tr);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $tdselectrow.append($divcancelsaverow);
    }
    //---------------------------------------------------------------------------------
    addMultiSaveAndCancelButtonToRow($control: JQuery, $tr: JQuery): void {
        let me = this;
        // add the multi-save button
        const $gridmenu = $control.find('[data-control="FwMenu"]');
        $tr.closest('tbody').find('.divselectrow').hide();
        $tr.find('.browsecontextmenucell').hide();
        const $multisave = jQuery('<div data-type="button" class="fwformcontrol grid-multi-save"><i class="material-icons" style="position:relative; top:5px;">&#xE161;</i> Save All</div>'); //save
        $multisave.on('click', function () {
            try {
                const $trs = $control.find('tr.editmode.editrow');
                me.multiSaveRow($control, $trs);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        if ($gridmenu.find('.grid-multi-save').length < 1) {
            $gridmenu.append($multisave);
        } else {
            $gridmenu.find('.grid-multi-save').show();
        }

        // add the cancel button
        const $tdselectrow = $tr.find('.tdselectrow');
        $tr.closest('tbody').find('.browsecontextmenu').hide();
        if ($tr.find('.divcancelsaverow').length === 0) {
            const $divcancelsaverow = jQuery('<div class="divcancelsaverow"><i class="material-icons">&#xE5C9;</i></div>'); //cancel
            $divcancelsaverow.on('click', function () {
                try {
                    const $this = jQuery(this);
                    const $tr = $this.closest('tr');
                    me.cancelEditMode($control, $tr);
                    $tr.find('.browsecontextmenucell').show();
                    const rowsInEditMode = $control.find('.editmode').length;
                    if (rowsInEditMode == 0) $gridmenu.find('.grid-multi-save').hide();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $tdselectrow.append($divcancelsaverow);
        }
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($control: JQuery, $tr: JQuery, $field: JQuery): void {
        let formdatatype = (typeof $field.attr('data-formdatatype') === 'string') ? $field.attr('data-formdatatype') : '';
        if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
            if (typeof window['FwBrowseColumn_' + formdatatype].setFieldEditMode === 'function') {
                window['FwBrowseColumn_' + formdatatype].setFieldEditMode($control, $tr, $field);
            }
        }
    }
    //---------------------------------------------------------------------------------
    appdocumentimageLoadFile($control: JQuery, $field: JQuery, file: File) {
        try {
            let reader = new FileReader();
            reader.onloadend = function () {
                $field.data('filedataurl', reader.result);
                $field.attr('data-filepath', file.name);
                if ((<string>reader.result).indexOf('data:application/pdf;') == 0) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-pdf.png');
                } else if ((<string>reader.result).indexOf('data:image/') == 0) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-image.png');
                } else if (((<string>reader.result).indexOf('data:application/vnd.ms-excel;') == 0) || ((<string>reader.result).indexOf('data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;') == 0)) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-spreadsheet.png');
                } else if ((((<string>reader.result).indexOf('data:application/msword;') == 0)) || ((<string>reader.result).indexOf('data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;') == 0)) {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-document.png');
                } else {
                    $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-generic.png');
                }
            }
            // this actually happens first, then when the file is done loading, reader.onloadend fires
            if (file) {
                reader.readAsDataURL(file);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //---------------------------------------------------------------------------------
    addManualSorting($control) {
        //adds button to apply changes in sorting
        const $applyChangesBtn = jQuery('<div data-type="button" class="fwformcontrol sorting"><i class="material-icons" style="position:relative; top:5px;">&#xE161;</i>Apply</div>');
        const $gridMenu = $control.find('[data-control="FwMenu"]');

        $applyChangesBtn.on('click', e => {
            try {
                const controller = $control.attr('data-controller');
                const $trs = $control.find('tbody  tr');
                const isFirstPage = $control.attr('data-pageno') === "1";
                let startAtIndex = '';

                let ids: any = [];
                for (let i = 0; i < $trs.length; i++) {
                    const $tr = jQuery($trs[i]);
                    let id = FwBrowse.getRowBrowseUniqueIds($control, $tr);
                    //get index of first row if not on first page of the grid
                    if (i === 0 && !isFirstPage) {
                        startAtIndex = $tr.find('[data-browsedatafield="RowNumber"]').attr('data-originalvalue');
                    }
                    ids.push(id[Object.keys(id)[0]]);
                }

                const request: any = {};
                const gridUniqueIdField = $control.find('thead [data-isuniqueid="true"]').attr('data-browsedatafield');
                const pageNo = $control.attr('data-pageno');
                request[`${gridUniqueIdField}s`] = ids;
                request.pageno = parseInt(pageNo);
                if (startAtIndex != '') request.StartAtIndex = startAtIndex;
                let apiurl = (<any>window[controller]).apiurl;
                FwAppData.apiMethod(true, 'POST', `${apiurl}/sort`, request, FwServices.defaultTimeout,
                    response => {
                        if (response.success) {
                            const onDataBind = $control.data('ondatabind');
                            if (typeof onDataBind == 'function') {
                                $control.data('ondatabind', function (request) {
                                    onDataBind(request);
                                    request.pageno = pageNo;
                                });
                            }
                            FwBrowse.search($control);
                            $control.removeClass('sort-mode');
                            $control.find('td.manual-sort').hide();
                            $gridMenu.find('.sorting').hide();
                            $gridMenu.find('.buttonbar').show();
                            $control.find('.btn-manualsort').show();
                            const $form = $control.closest('.fwform');
                            $form.data('ismanualsort', true);

                            if (typeof $control.data('onafterrowsort') === 'function') {
                                const $tr = jQuery($trs[0]);
                                $control.data('onafterrowsort')($control, $tr);
                            }
                            $control.attr('data-pageno', pageNo);
                            $control.data('ondatabind', onDataBind);
                        } else {
                            FwNotification.renderNotification('ERROR', response.msg);
                        };
                    },
                    ex => FwFunc.showError(ex), $control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //cancel sorting button
        const $cancelBtn = jQuery('<div data-type="button" class="fwformcontrol sorting" style="margin-left:10px;">Cancel</div>');
        $cancelBtn.on('click', e => {
            const onDataBind = $control.data('ondatabind');
            const pageNumber = $control.attr('data-pageno');
            if (typeof onDataBind == 'function') {
                $control.data('ondatabind', function (request) {
                    onDataBind(request);
                    request.pageno = parseInt(pageNumber);
                });
            }
            FwBrowse.search($control); //refresh grid to reset to original sorting order
            $control.find('td.manual-sort').hide();
            $gridMenu.find('.sorting').hide();
            $gridMenu.find('.buttonbar').show();
            $control.find('.btn-manualsort').show();
            $control.removeClass('sort-mode');
            $control.attr('data-pageno', pageNumber);
            $control.data('ondatabind', onDataBind);
        });

        //initialize Sortable
        Sortable.create($control.find('tbody').get(0), {
            handle: 'i.drag-handle',
            onEnd: function (evt) {
                //toggle displayed buttons
                $gridMenu.find('.buttonbar').hide();
                if ($gridMenu.find('.sorting').length < 1) {
                    $gridMenu.append($applyChangesBtn, $cancelBtn);
                } else {
                    $gridMenu.find('.sorting').show();
                }
                $control.find('.btn-manualsort').hide();
            }
        });
    }
    //---------------------------------------------------------------------------------
    getRowBrowseUniqueIds($control: JQuery, $tr: JQuery) {
        let uniqueids: any = {};
        let $uniqueidfields = $tr.find('> td.column > div.field[data-isuniqueid="true"]');
        $uniqueidfields.each(function (index, element) {
            var $field, browsedatafield, originalvalue;
            $field = jQuery(element);
            browsedatafield = $field.attr('data-browsedatafield');
            originalvalue = $field.attr('data-originalvalue');
            uniqueids[browsedatafield] = originalvalue;
        });
        return uniqueids;
    }
    //---------------------------------------------------------------------------------
    getRowFormUniqueIds($control, $tr) {
        let uniqueids: any = {};
        let $uniqueidfields = $tr.find('> td.column > div.field[data-isuniqueid="true"]');
        $uniqueidfields.each(function (index, element) {
            var $field, formdatafield, formdatatype, value, originalvalue;
            $field = jQuery(element);
            formdatafield = $field.attr('data-formdatafield');
            formdatatype = $field.attr('data-formdatatype');
            originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
            let uniqueid = {
                datafield: formdatafield,
                value: originalvalue
            };
            // provides an opportunity to override the originalvalue for the uniqueid
            if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
                if (typeof window['FwBrowseColumn_' + formdatatype].getFieldUniqueId === 'function') {
                    window['FwBrowseColumn_' + formdatatype].getFieldUniqueId($control, $tr, $field, uniqueid, originalvalue);
                }
            }
            uniqueids[formdatafield] = uniqueid;
        });
        return uniqueids;
    }
    //---------------------------------------------------------------------------------
    getRowFormDataFields($control: JQuery, $tr: JQuery, getmiscfields?: boolean) {
        var $fields: JQuery;
        var fields: any = {};
        if (getmiscfields === false) {
            $fields = $tr.find('> td.column > div.field[data-formreadonly!="true"][data-formdatafield][data-formdatafield!=""]');
        } else {
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
            } else {
                fields[miscfield] = field;
            }
        });
        return fields;
    }
    //----------------------------------------------------------------------------------------------
    getWebApiRowFields($control: JQuery, $tr: JQuery) {
        let fields: any = {};
        let $fields = $tr.find('> td.column > div.field[data-formdatafield][data-formdatafield!=""]');
        $fields.each(function (index, element) {
            let $field = jQuery(element);
            let formdatafield = (typeof $field.attr('data-formdatafield') === 'string') ? $field.attr('data-formdatafield') : '';
            let formdatatype = (typeof $field.attr('data-formdatatype') === 'string') ? $field.attr('data-formdatatype') : '';
            let originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';


            let field: any = {};
            if ($field.data('customfield') !== undefined && $field.data('customfield') === true) {
                if (typeof fields._Custom === 'undefined') {
                    fields._Custom = [];
                }
            } else {
                if (formdatatype === 'appdocumentimage') {
                    field = {};
                } else {
                    field = {
                        datafield: formdatafield,
                        value: originalvalue
                    };
                }
            }

            if (typeof window['FwBrowseColumn_' + formdatatype] !== 'undefined') {
                if (typeof window['FwBrowseColumn_' + formdatatype].getFieldValue === 'function') {
                    window['FwBrowseColumn_' + formdatatype].getFieldValue($control, $tr, $field, field, originalvalue);
                }
            }

            if ($field.data('customfield') !== undefined && $field.data('customfield') === true) {
                switch (formdatatype) {
                    case "text":
                        formdatatype = "Text";
                        break;
                    case "date":
                        formdatatype = "Date";
                        break;
                    case "checkbox":
                        formdatatype = "True/False";
                        break;
                    case "number":
                        formdatatype = "Integer";
                        break;
                    case "decimal":
                        formdatatype = "Float";
                        break;
                }
                field = {
                    FieldName: formdatafield,
                    FieldType: formdatatype,
                    FieldValue: field.value
                }
                fields._Custom.push(field);
            }
            else if (formdatatype === 'appdocumentimage') {
                const uniqueId1Field = (typeof $field.attr('data-uniqueid1field') === 'string') ? $field.attr('data-uniqueid1field') : '';
                const uniqueId2Field = (typeof $field.attr('data-uniqueid2field') === 'string') ? $field.attr('data-uniqueid2field') : '';
                if (uniqueId1Field.length > 0) {
                    fields[uniqueId1Field] = field[uniqueId1Field];
                }
                if (uniqueId2Field.length > 0) {
                    fields[uniqueId2Field] = field[uniqueId2Field];
                }
                fields.FileIsModified = field.FileIsModified;
                fields.FileDataUrl = field.FileDataUrl;
                fields.FilePath = field.FilePath;
            }
            else {
                fields[formdatafield] = field.value;
            }

            if (formdatatype === 'validation') {
                const validationDisplayField = (typeof $field.attr('data-browsedisplayfield') === 'string') ? $field.attr('data-browsedisplayfield') : '';
                const validationDisplayValue = $tr.find(`.field[data-browsedatafield="${formdatafield}"] input.text`).val();
                if (validationDisplayField != formdatafield) {
                    fields[validationDisplayField] = validationDisplayValue; // 11/09/2018 CAS-24077-PDIB adding display field here for audit history
                }
            }
        });
        for (const key in fields) {
            if (fields[key] === undefined) {
                delete fields[key];
            }
        }
        return fields;
    }
    //----------------------------------------------------------------------------------------------
    saveRow($control: JQuery, $tr: JQuery): Promise<boolean> {
        let me = this;
        let isNewMode = $tr.hasClass('newmode');
        return new Promise<boolean>((resolve, reject) => {
            let isvalid = true;
            if (this.isRowModified($control, $tr)) {
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
                if (mode === '') throw 'FwBrowse.saveRow: Invalid mode';
                isvalid = this.validateRow($control, $tr);
                if (isvalid) {
                    var isUsingWebApi = this.isUsingWebApi($control);
                    var request;
                    $form = $control.closest('.fwform');
                    if (isUsingWebApi) {
                        // set request for web api
                        var allparentformfields = FwModule.getWebApiFields($form, true);
                        var parentformfields = {};
                        var whitelistedFields = (typeof $control.attr('data-parentformdatafields') !== 'undefined') ? $control.attr('data-parentformdatafields') : '';
                        if (whitelistedFields.length > 0) {
                            let whitelistedFieldsArray = whitelistedFields.split(',');
                            for (var fieldname in allparentformfields) {
                                for (var i = 0; i < whitelistedFieldsArray.length; i++) {
                                    var whitelistedField = whitelistedFieldsArray[i];
                                    var indexOfEquals = whitelistedField.indexOf('=');
                                    if (indexOfEquals === -1) {
                                        if (fieldname === whitelistedFieldsArray[i]) {
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

                        var gridfields = this.getWebApiRowFields($control, $tr);
                        request = jQuery.extend({}, parentformfields, gridfields);
                        if (typeof $control.data('beforesave') === 'function') {
                            $control.data('beforesave')(request, $control, $tr);
                        }
                    }
                    else {
                        // set request for old api
                        rowuniqueids = this.getRowFormUniqueIds($control, $tr);
                        rowfields = this.getRowFormDataFields($control, $tr, false);
                        miscfields = this.getRowFormDataFields($control, $tr, true);
                        if ($form.length > 0) {
                            formuniqueids = FwModule.getFormUniqueIds($form);
                            formfields = FwModule.getFormFields($form, true);
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
                        $control.data('beforesave')(request, $control, $tr);
                    }
                    if (typeof controller.apiurl === 'undefined') {
                        mode = 'Save';
                    }
                    FwServices.grid.method(request, name, mode, $control, function (response) {
                        try {
                            // if the server reloaded the fields, then databind them into the row
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
                                if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                                if (typeof window[controller]['afterSave'] === 'function') {
                                    window[controller]['afterSave']($control, $tr);
                                }
                            }

                            if ($control.attr('data-refreshaftersave') === 'true' && (typeof $control.attr('data-autosave') === 'undefined' || $control.attr('data-autosave') === 'true') ||
                                (isNewMode && ($control.attr('data-refreshafterinsert') === 'true') || typeof $control.attr('data-refreshafterinsert') === 'undefined')) {
                                // mv 2018-07-09 returns a promise so you can do things after the grid reloads from a save
                                me.databind($control)
                                    .then(() => {
                                        resolve();
                                    })
                                    .catch((reason) => {
                                        reject(reason);
                                    });
                            }
                            else {
                                $tr.removeClass('newmode');
                                resolve();
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                            reject();
                        }
                    });
                } else {
                    //reject();
                    resolve();  //justin 10/31/2019 RWW#1240 - prevent blank error pop-up when saving a row with missing required fields.
                }
            } else {
                this.cancelEditMode($control, $tr);
                resolve();
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    multiSaveRow($control: JQuery, $trs: JQuery): Promise<boolean> {
        return new Promise<boolean>((resolve, reject) => {
            const name = $control.attr('data-name');
            if (typeof name === 'undefined') {
                throw 'Attrtibute data-name is missing on the Browser controller with html: ' + $control[0].outerHTML;
            }
            const controller = window[name + 'Controller'];
            if (typeof controller === 'undefined') {
                throw `Controller: ${name} is not defined`;
            }
            let manyRequest = [];
            let ids = [];
            for (let i = 0; i < $trs.length; i++) {
                const $tr = jQuery($trs[i]);
                if (this.isRowModified($control, $tr)) {
                    var $form;
                    let isvalid = true;
                    isvalid = this.validateRow($control, $tr);
                    if (isvalid) {
                        $form = $control.closest('.fwform');
                        const gridfields = this.getWebApiRowFields($control, $tr);
                        if (typeof $control.data('beforesave') === 'function') {
                            $control.data('beforesave')(gridfields, $control, $tr);
                        }
                        manyRequest.push(gridfields);
                        const uniqueIds = this.getRowFormUniqueIds($control, $tr);
                        ids.push(uniqueIds);
                    }
                }
            }
            FwAppData.apiMethod(true, 'POST', `${controller.apiurl}/many`, manyRequest, FwServices.defaultTimeout, function (response) {
                const pageNumber = $control.attr('data-pageno');
                const onDataBind = $control.data('ondatabind');
                if (typeof onDataBind == 'function') { //adds current page number to request
                    $control.data('ondatabind', function (request) {
                        onDataBind(request);
                        request.pageno = parseInt(pageNumber);
                    });
                }
                FwBrowse.search($control)
                    .then(() => {
                        $control.find('.grid-multi-save').hide();
                        $control.attr('data-pageno', pageNumber);
                        $control.data('ondatabind', onDataBind); //re-binds original request
                        for (let i = 0; i < response.length; i++) {
                            const item = response[i];
                            const key = Object.keys(ids[i])[0];
                            const uniqueIdField = ids[i][key].datafield;
                            const uniqueIdValue = ids[i][key].value;
                            const $tr = $control.find(`div.field[data-isuniqueid="true"][data-formdatafield="${uniqueIdField}"][data-originalvalue="${uniqueIdValue}"]`).parents('tr');
                            const $contextMenu = $tr.find('td.browsecontextmenucell');
                            if (item["Result"]["StatusCode"] != 200) {
                                FwNotification.renderNotification('ERROR', item["Result"]["Value"]["Message"]);
                                $contextMenu.addClass('menuError');
                                $contextMenu.unbind('click.error');
                                $contextMenu.bind('click.error', () => {
                                    FwNotification.renderNotification('ERROR', item["Result"]["Value"]["Message"]);
                                });
                            } else {
                                $contextMenu.removeClass('menuError');
                                $contextMenu.unbind('click.error');
                            }
                        }
                        resolve();
                    })
                    .catch((reason) => {
                        reject(reason);
                    });
            }, null, $control);
        })
    }
    //----------------------------------------------------------------------------------------------
    deleteRow($control: JQuery, $tr: JQuery) {
        let me = this;
        var rowuniqueids, formuniqueids, name, $form, $confirmation, $ok, $cancel, candelete, miscfields;
        candelete = true;
        miscfields = {};
        if (($control.attr('data-type') === 'Grid') && (typeof $control.data('beforedelete') === 'function')) {
            $control.data('beforedelete')($control, $tr);
        }
        else if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['beforeDelete'] === 'function') {
                candelete = window[controller]['beforeDelete']($control, $tr);
            }
        }
        if (($tr.length == 1) && (candelete)) {
            $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Delete Record?');
            $ok = FwConfirmation.addButton($confirmation, 'OK');
            $cancel = FwConfirmation.addButton($confirmation, 'Cancel');

            $ok.on('click', async () => {
                try {
                    await me.deleteRecord($control, $tr);
                    await me.databind($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    async deleteRecord($control: JQuery, $tr: JQuery): Promise<void> {
        return new Promise<void>(async (resolve, reject) => {
            try {
                let me = this;
                this.autoSave($control, $tr);
                let miscfields = {};
                let name = $control.attr('data-name');
                let $form = $control.closest('.fwform');
                let rowuniqueids = this.getRowFormUniqueIds($control, $tr);
                const request = new FwAjaxRequest<any>();
                request.data = {
                    module: name,
                    ids: rowuniqueids,
                    miscfields: miscfields
                };
                if ($form.length > 0) {
                    let formuniqueids = ($form.length > 0) ? FwModule.getFormUniqueIds($form) : [];
                    request.data.miscfields = jQuery.extend({}, miscfields, formuniqueids);
                }

                var controller: any = window[name + 'Controller'];
                if (typeof controller === 'undefined') {
                    throw name + 'Controller is not defined.'
                }
                let url = '';
                if (typeof $control.data('getapiurl') === 'function') {
                    url = $control.data('getapiurl')('DELETE');
                }
                if (url.length === 0 && typeof controller.apiurl !== 'undefined' || typeof $control.data('getbaseapiurl') === 'function') {
                    if (typeof $control.data('getbaseapiurl') !== 'undefined') {
                        url = $control.data('getbaseapiurl')();
                    }
                    else if (typeof controller.apiurl !== 'undefined' && controller.apiurl.length > 0) {
                        url = controller.apiurl;
                    }
                    else {
                        throw `No apiurl defined for Grid: ${name}`;
                    }
                    var ids: any = [];
                    for (var key in request.data.ids) {
                        ids.push(request.data.ids[key].value);
                    }
                    ids = ids.join('~');
                    if (ids.length === 0) {
                        throw 'primary key id(s) cannot be blank';
                    }
                    url += '/' + ids;
                }
                request.url = applicationConfig.apiurl + url;
                request.httpMethod = 'DELETE';

                const response = await FwAjax.callWebApi<any, any>(request)
                if (request.xmlHttpRequest.status === 200 || request.xmlHttpRequest.status === 404) {
                    //perform after delete
                    if (($control.attr('data-type') === 'Grid') && (typeof $control.data('afterdelete') === 'function')) {
                        $control.data('afterdelete')($control, $tr);
                    }
                    else if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '')) {
                        if (controller.afterDelete === 'function') {
                            controller.afterDelete($control, $tr);
                        }
                    }
                    //if (refreshAfterDelete) {
                    //    me.search($control);
                    //}
                    resolve();
                }
                else {
                    reject(response);
                }
            }
            catch (ex) {
                reject(ex);
            }
        });
    }
    //---------------------------------------------------------------------------------
    addLegend($control: JQuery, caption: string, color: string) {
        let html = [];
        html.push('<div class="legenditem">');
        html.push('  <div class="color" style="background-color:' + color + '"></div>');
        html.push('  <div class="caption">' + caption + '</div>');
        html.push('</div>');
        let htmlString = html.join('\n');
        let $legenditem = jQuery(htmlString);
        let $legend = $control.find('.legend');
        $legend.append($legenditem);
        $legend.show();
    }
    //----------------------------------------------------------------------------------------------
    getGridData($object: JQuery, request: any, responseFunc: Function) {
        var webserviceurl, controller, module;
        controller = $object.attr('data-controller');
        module = (<any>window)[controller].Module;
        request.module = module;
        webserviceurl = 'services.ashx?path=/grid/' + module + '/GetData';
        FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $object);
    }
    //----------------------------------------------------------------------------------------------
    downloadExcelWorkbook($control: JQuery, controller: any): void {
        const totalNumberofRows = FwBrowse.getTotalRowCount($control);
        const totalNumberofRowsStr = totalNumberofRows.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        if (totalNumberofRows >= 1) {
            const $confirmation = FwConfirmation.renderConfirmation('Download Excel Workbook', '');
            $confirmation.find('.fwconfirmationbox').css('width', '564px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="flexrow">');
            html.push('  <div class="flexcolumn">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield all-records" data-caption="Download all ${totalNumberofRowsStr} Records" data-datafield="" style="float:left;width:100px;"></div>`);
            html.push('  </div>');
            html.push(' <div class="formrow" style="width:100%;display:flex;align-content:flex-start;align-items:center;padding-bottom:13px;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield user-defined-records" data-caption="" data-datafield="" style="float:left;width:30px;"></div>`);
            html.push('  </div>');
            html.push('  <span style="margin:18px 0px 0px 0px;">First</span>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin:0px 0px 0px 0px;">');
            html.push('    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield user-defined-records-input" data-caption="" data-datafield="" style="width:80px;float:left;margin:0px 0px 0px 0px;"></div>');
            html.push('  </div>');
            html.push('  <span style="margin:18px 0px 0px 0px;">Records</span>');
            html.push(' </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield all-col" data-caption="Include All fields" data-datafield="" style="float:left;width:100px;"></div>`);
            html.push('  </div>');
            html.push('  <div class="flexrow fieldsrow" style="display:none;">');
            html.push('  <div class="flexcolumn">');
            html.push('  <div class="flexrow" style="padding:0 20px 0 7px;">');
            html.push('  <div><div class="check-uncheck" style="color:#2626f3;cursor:pointer;float:left;">Uncheck All</div><div class="sort-list" style="color:#2626f3;cursor:pointer; float:right;">Sort List By Name</div></div>');
            html.push('  </div>');
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" class="fwcontrol fwformfield" data-checkboxlist="persist" data-type="checkboxlist" data-sortable="true" data-orderby="false" data-caption="Include these fields" data-datafield="FieldList" style="flex:1 1 550px;"></div>`);
            html.push('  </div>');
            html.push('  </div>');
            html.push('  </div>');
            html.push('  </div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Download', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');
            const request: any = FwBrowse.getRequest($control);

            if (request.pagesize > totalNumberofRows) {
                $confirmation.find('.user-defined-records-input input').val(totalNumberofRows);
            } else {
                $confirmation.find('.user-defined-records-input input').val(request.pagesize);
            }

            $confirmation.find('.all-records input').prop('checked', true);
            $confirmation.find('.all-col input').prop('checked', true);

            $confirmation.find('.all-records input').on('change', e => {
                const $this = jQuery(e.currentTarget);
                if ($this.prop('checked') === true) {
                    $confirmation.find('.user-defined-records input').prop('checked', false);
                }
                else {
                    $confirmation.find('.user-defined-records input').prop('checked', true);
                }
            });

            $confirmation.find('.user-defined-records input').on('change', e => {
                const $this = jQuery(e.currentTarget);
                if ($this.prop('checked') === true) {
                    $confirmation.find('.all-records input').prop('checked', false);
                }
                else {
                    $confirmation.find('.all-records input').prop('checked', true);
                }
            });

            $confirmation.find('.all-col input').on('change', e => {
                const $this = jQuery(e.currentTarget);
                if ($this.prop('checked') === true) {
                    $confirmation.find('.fieldsrow').hide();
                }
                else {
                    $confirmation.find('.fieldsrow').show();
                    if ($confirmation.find('div[data-datafield="FieldList"]').attr('api-req') !== 'true') {
                        renderColumnPopup($confirmation, controller);
                    }
                }
            });

            $confirmation.find('.check-uncheck').on('click', e => {
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
            });
            $confirmation.find('.sort-list').on('click', e => {
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
            });

            $confirmation.find('.user-defined-records-input input').keypress(() => {
                $confirmation.find('.user-defined-records input').prop('checked', true);
                $confirmation.find('.all-records input').prop('checked', false);
            });

            $yes.on('click', e => {
                const $existingNotification = jQuery('body').find(".fwnotification.advisory.info .message:contains('Downloading Excel Workbook...')");
                if ($existingNotification.length > 0) {
                    $existingNotification.parent().remove();
                }
                const $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Downloading Excel Workbook...');
                let userDefinedNumberofRows = +$confirmation.find('.user-defined-records input').val();
                $confirmation.find('.all-records input').prop('checked') === true ? userDefinedNumberofRows = totalNumberofRows : userDefinedNumberofRows = +$confirmation.find('.user-defined-records-input input').val();
                request.pagesize = userDefinedNumberofRows;
                let includeallcolumns: boolean;
                let excelfields: any;
                if ($confirmation.find('.all-col input').prop('checked') === true) {
                    request.includeallcolumns = true;
                } else {
                    request.includeallcolumns = false;
                    request.excelfields = FwFormField.getValueByDataField($confirmation, 'FieldList');
                }

                const apiurl = (<any>window)[controller].apiurl;
                const timeout = 7200; // 2 hour timeout for the ajax request

                FwAppData.apiMethod(true, 'POST', `${apiurl}/exportexcelxlsx`, request, timeout, function (response) {
                    try {
                        const $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${response.downloadUrl}" style="display:none;"></iframe>`);
                        jQuery('#application').append($iframe);
                        setTimeout(function () {
                            $iframe.remove();
                            FwNotification.closeNotification($notification);
                        }, 500);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, null);
                FwConfirmation.destroyConfirmation($confirmation);
            });
            // ----------
            function renderColumnPopup($confirmation, controller) {
                FwAppData.apiMethod(true, 'GET', `${(<any>window[controller]).apiurl}/emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
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
        } else {
            FwNotification.renderNotification('WARNING', 'There are no records to export.');
        }
    }
    //----------------------------------------------------------------------------------------------
    customizeColumns($control: JQuery, name: any, type: any) {
        let $form;
        const isCustomBrowse = $control.data('iscustombrowse');
        const fullName = sessionStorage.getItem('fullname');
        if (isCustomBrowse) {
            if (typeof $control.data('customformid')) {
                const uniqueids = {
                    CustomFormId: $control.data('customformid')
                }
                $form = CustomFormController.loadForm(uniqueids);
                FwModule.openModuleTab($form, `${name} ${type} - ${fullName}`, true, 'FORM', true);
                $form.attr('data-mode', 'EDIT');
                $form.data('selfassign', true);
                CustomFormController.enableSave($form);
            }
        } else {
            try {
                $form = CustomFormController.openForm('NEW');
                FwModule.openModuleTab($form, 'New Custom Form', true, 'FORM', true);
                const value = name + type.charAt(0).toUpperCase() + type.slice(1);
                FwFormField.setValueByDataField($form, 'BaseForm', value, null, true);
                FwFormField.setValueByDataField($form, 'Description', `${name} ${type} - ${fullName}`);
                FwFormField.setValueByDataField($form, 'AssignTo', 'USERS');
                $form.attr('data-mode', 'NEW');
                $form.data('selfassign', true);
                CustomFormController.enableSave($form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    disableGrid($control: JQuery) {
        //$control.attr('data-enabled', 'false');
        $control.find('.buttonbar').hide();
        const $columns = $control.find('.column');
        jQuery.each($columns, (i, el) => {
            const $field = jQuery(el).find('.field');
            if ($field) {
                if ($field.attr('data-preventformreadonly') != 'true') {
                    $field.attr('data-formreadonly', 'true');
                    if ($field.attr('data-datatype') === 'checkbox' || $field.attr('data-browsedatatype') === 'checkbox' || $field.attr('data-formdatatype') === 'checkbox') {
                        $field.css('pointer-events', 'none');
                    }
                }
            }
        })
    }
    //----------------------------------------------------------------------------------------------
    enableGrid($control: JQuery) {
        $control.attr('data-enabled', 'true');
    }
    //---------------------------------------------------------------------------------
    validateRow($control: JQuery, $tr: JQuery) {
        var isvalid, $fields;

        isvalid = true;

        $fields = $tr.find('.field');
        //$fields = $tr.find('.field[data-isuniqueid!="true"]');
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

        //validate grid values before saves
        if (($control.attr('data-type') == 'Grid') && (typeof $control.attr('data-controller') !== 'undefined') && ($control.attr('data-controller') !== '') && (isvalid)) {
            var controller;
            controller = $control.attr('data-controller');
            if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['validateRow'] === 'function') {
                isvalid = window[controller]['validateRow']($control, $tr);
            }
        }

        if (!isvalid) {
            FwNotification.renderNotification('ERROR', 'Please correct the error(s) before saving this row.');
        }

        return isvalid;
    }
    //----------------------------------------------------------------------------------------------
    getOptions($control: JQuery) {
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
    }
    //---------------------------------------------------------------------------------
    endsWith(str: string, suffix: string): boolean {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }
    //---------------------------------------------------------------------------------
    auditHistoryPopupContent() {
        return jQuery(
            `<div>
              <div class="menu"></div>
              <div class="flexrow body" style="background-color:white;max-width:1275px;">
                <div class="formcolumn" style="margin:20px 5px 0 px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div class="flexrow"></div>
                    <div data-control="FwGrid" class="container">
                      <div class="formrow"><div data-control="FwGrid" data-grid="AuditHistoryGrid" data-securitycaption=""></div></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`);
    }
    //---------------------------------------------------------------------------------
    renderAuditHistoryPopup($tr: JQuery): void {
        const $popup = FwPopup.renderPopup(this.auditHistoryPopupContent(), { ismodal: true }, 'Audit History', 'placeholder');
        $popup.find('.popout-modal').removeClass('popout-modal').addClass('pop-out').off();
        FwPopup.showPopup($popup);

        let auditKeyFields;
        const controller = $tr.parents('[data-type="Grid"]').attr('data-controller');

        if (typeof window[controller].AuditKeyFields != 'undefined') {
            auditKeyFields = window[controller].AuditKeyFields;
        }

        let module: string = $tr.parents('[data-type="Grid"]').attr('data-auditmodule') || window[controller].Module;
        if (this.endsWith(module, 'Grid')) {
            module = module.substring(0, module.length - 4);
        }

        const $auditHistoryGrid = FwBrowse.renderGrid({
            nameGrid: 'AuditHistoryGrid',
            gridSecurityId: 'xepjGBf0rdL',
            moduleSecurityId: '',
            $form: $popup,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ModuleName: module
                };
                //const keys = $tr.find('[data-browsedatatype="key"]');
                let keys: any = [];
                if (typeof auditKeyFields != 'undefined') {
                    for (let i = 0; i < auditKeyFields.length; i++) {
                        keys[i] = $tr.find(`[data-browsedatafield="${auditKeyFields[i]}"]`);
                    }
                } else {
                    keys = $tr.find('[data-browsedatatype="key"]');
                }
                if (keys.length > 0) {
                    request.uniqueids.UniqueId1 = jQuery(keys[0]).attr('data-originalvalue');
                    if (keys.length > 1) {
                        request.uniqueids.UniqueId2 = jQuery(keys[1]).attr('data-originalvalue');
                        if (keys.length > 2) {
                            request.uniqueids.UniqueId3 = jQuery(keys[2]).attr('data-originalvalue');
                        }
                    }
                }
            }
        });
        FwBrowse.search($auditHistoryGrid);
        // Close modal
        $popup.find('.close-modal').one('click', e => {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });
        //pop-out button
        $popup.on('click', '.pop-out', e => {
            const $auditHistoryGridControl = $popup.find('div[data-name="AuditHistoryGrid"]');
            const $gridClone = $auditHistoryGridControl.clone(true);
            const griddatabind = $auditHistoryGridControl.data('ondatabind');
            setTimeout(() => {
                const $popoutContent = this.auditHistoryPopupContent();
                FwControl.renderRuntimeControls($popoutContent.find('.fwcontrol'));
                const $form = $tr.closest('.fwform');
                FwModule.openSubModuleTab($form, $popoutContent);
                $popoutContent.css({ 'border': 'none', 'max-width': 'none', 'padding': '0px' });

                const $menu = FwMenu.getMenuControl('default');
                $popoutContent.find('.menu').append($menu);
                FwMenu.addSubMenu($menu);
                const tabid = $popoutContent.closest('.tabpage').attr('data-tabid');
                jQuery(`#${tabid} .caption`).text('Audit History');
                const $popOutGrid = $popoutContent.find('[data-grid="AuditHistoryGrid"]');
                $popOutGrid.empty().append($gridClone);
                $gridClone.data('ondatabind', griddatabind);
                FwBrowse.search($gridClone);
                FwPopup.detachPopup($popup);
            }, 0, [$tr]);
        });
        // Close modal if click outside
        jQuery(document).on('click', e => {
            if (!jQuery(e.target).closest('.popup').length) {
                FwPopup.destroyPopup($popup);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getValidationData($object: JQuery, request: any, responseFunc: Function) {
        const controller = $object.attr('data-controller');
        const module = (<any>window)[controller].Module;
        request.module = module;
        const webserviceurl = `services.ashx?path=/validation/${module}/GetData`;
        FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $object);
    }
    //---------------------------------------------------------------------------------
    getController($control: JQuery) {
        let controllername;
        let controller; // default value of controller will be undefined if not found
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
    }
    //--------------------------------------------------------------------------------- 
    isUsingWebApi($control: JQuery) {
        let useWebApi = false;
        const controller = this.getController($control);
        if (typeof controller.apiurl !== 'undefined') {
            useWebApi = true;
        }
        return useWebApi;
    }
    //---------------------------------------------------------------------------------
    loadBrowseFromTemplate(modulename: string) {
        const $control = jQuery(jQuery('#tmpl-modules-' + modulename + 'Browse').html());

        //FwBrowse.loadCustomBrowseFields($control, modulename)

        return $control;
    }
    //---------------------------------------------------------------------------------
    //loadCustomBrowseFields($control: JQuery, modulename: string) {
    //    if (sessionStorage.getItem('customFieldsBrowse') !== null) {
    //        var customBrowse = JSON.parse(sessionStorage.getItem('customFieldsBrowse'));
    //        var customBrowseHtml = [];

    //        if (customBrowse !== 'undefined' && customBrowse.length > 0) {
    //            for (var i = 0; i < customBrowse.length; i++) {
    //                if (modulename === customBrowse[i].moduleName) {
    //                    customBrowseHtml.push(`<div class="column" data-width="${customBrowse[i].browsewidth}px" data-visible="true"><div class="field" data-caption="${customBrowse[i].fieldName}" data-datafield="${customBrowse[i].fieldName}" data-digits="${customBrowse[i].digits}" data-datatype="${customBrowse[i].datatype}" data-browsedatatype="${customBrowse[i].datatype}" data-sort="off"></div></div>`);
    //                }
    //            }
    //        }
    //        if ($control.find('.spacer').length > 0) {
    //            jQuery(customBrowseHtml.join('')).insertBefore($control.find('.spacer'));
    //        } else {
    //            $control.append(customBrowseHtml.join(''));
    //        }
    //    }
    //}
    //---------------------------------------------------------------------------------
    loadGridFromTemplate(modulename: string) {
        let $control = null;
        if (sessionStorage.getItem('customForms') !== null) {
            let customGrids = JSON.parse(sessionStorage.getItem('customForms'));
            customGrids = customGrids.filter(a => a.BaseForm == `${modulename}Browse`);
            if (customGrids.length > 0) {
                $control = jQuery(jQuery(`#tmpl-custom-${modulename}Browse`)[0].innerHTML);
            } else {
                if (typeof window[modulename + 'Controller'] != 'undefined' && typeof window[modulename + 'Controller'].getBrowseTemplate === 'function') {
                    $control = window[modulename + 'Controller'].getBrowseTemplate();
                } else {
                    $control = jQuery(jQuery(`#tmpl-grids-${modulename}Browse`).html());
                }

            }
        } else {
            if (typeof window[modulename + 'Controller'] != 'undefined' && typeof window[modulename + 'Controller'].getBrowseTemplate === 'function') {
                $control = window[modulename + 'Controller'].getBrowseTemplate();
            } else {
                $control = jQuery(jQuery(`#tmpl-grids-${modulename}Browse`).html());
            }
        }
        return $control;
    }
    //---------------------------------------------------------------------------------
    setBeforeSaveCallback($control: JQuery, callback: ($browse: JQuery, $tr: JQuery) => void) {
        $control.data('beforesave', callback);
    }
    //---------------------------------------------------------------------------------
    setAfterSaveCallback($control: JQuery, callback: ($browse: JQuery, $tr: JQuery) => void) {
        $control.data('aftersave', callback);
    }
    //---------------------------------------------------------------------------------
    setBeforeDeleteCallback($control: JQuery, callback: ($browse: JQuery, $tr: JQuery) => void) {
        $control.data('beforedelete', callback);
    }
    //---------------------------------------------------------------------------------
    setAfterDeleteCallback($control: JQuery, callback: ($browse: JQuery, $tr: JQuery) => void) {
        $control.data('afterdelete', callback);
    }
    //---------------------------------------------------------------------------------
    setAfterRenderRowCallback = function ($control: JQuery, callback: ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => void) {
        $control.data('afterrenderrow', callback);
    }
    //---------------------------------------------------------------------------------
    setAfterRenderFieldCallback = function ($control: JQuery, callback: ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => void) {
        $control.data('afterrenderfield', callback);
    }
    //---------------------------------------------------------------------------------
    setFieldValue($control: JQuery, $tr: JQuery, datafield: string, data: FwBrowse_SetFieldValueData) {
        let $field = $tr.find(`.field[data-browsedatafield="${datafield}"]`);
        let datatype = $field.attr('data-browsedatatype');
        if ($tr.hasClass('newmode')) {
            $field.attr('data-originalvalue', data.value);
        }
        (<IFwBrowseColumn>(<any>window)[`FwBrowseColumn_${datatype}`]).setFieldValue($control, $tr, $field, data);
    }
    //---------------------------------------------------------------------------------
    renderGrid(options: {
        moduleSecurityId: string,
        $form: JQuery,
        gridSelector?: string,
        nameGrid: string,
        gridSecurityId: string,
        pageSize?: number,
        getBaseApiUrl?: () => string,
        onDataBind?: (request: any) => void,
        onOverrideNotesTemplate?: ($browse: JQuery, $tr: JQuery, $field: JQuery, controlhtml, $confirmation: any, $ok: any) => void,
        afterDataBindCallback?: ($browse: JQuery, dt: FwJsonDataTable) => void,
        beforeSave?: (request: any, $browse?: JQuery, $tr?: JQuery) => void,
        addGridMenu?: (options: IAddGridMenuOptions) => void,
        beforeInit?: ($fwgrid: JQuery, $browse: JQuery) => void
        getTemplate?: () => string
    }): JQuery {
        if (typeof options.gridSelector !== 'string' || options.gridSelector.length === 0) {
            options.gridSelector = `div[data-grid="${options.nameGrid}"]`;
        }
        //justin hoffman 01/25/2020 RWW#1659 (commented)
        //if (typeof options.pageSize !== 'number') {
        //    options.pageSize = 15;
        //}
        const $fwgrid: JQuery = options.$form.find(options.gridSelector);
        let $browse: JQuery;
        if (typeof options.getTemplate !== 'function') {
            $browse = FwBrowse.loadGridFromTemplate(options.nameGrid);
        } else {
            $browse = jQuery(options.getTemplate());
        }
        $fwgrid.empty().append($browse);
        $browse.data('secid', options.gridSecurityId);
        $browse.attr('data-pagesize', options.pageSize);
        if (typeof options.getBaseApiUrl === 'function') {
            $browse.data('getbaseapiurl', options.getBaseApiUrl)
        }
        if (typeof options.onDataBind === 'function') {
            $browse.data('ondatabind', options.onDataBind);
        }
        if (typeof options.onOverrideNotesTemplate === 'function') {
            $browse.data('onOverrideNotesTemplate', options.onOverrideNotesTemplate);
        }
        if (typeof options.afterDataBindCallback === 'function') {
            FwBrowse.addEventHandler($browse, 'afterdatabindcallback', ($browse: JQuery, dt: FwJsonDataTable) => {
                options.afterDataBindCallback($browse, dt);
            });
        }
        if (typeof options.beforeSave === 'function') {
            $browse.data('beforesave', options.beforeSave);
        }
        if (typeof options.addGridMenu === 'function') {
            $browse.data('addGridMenu', options.addGridMenu);
        } else {
            $browse.data('addGridMenu', (options: IAddGridMenuOptions) => void {});
        }
        if (typeof options.beforeInit === 'function') {
            options.beforeInit($fwgrid, $browse);
        }
        FwBrowse.init($browse);
        FwBrowse.renderRuntimeHtml($browse);
        return $browse;
    }
    //---------------------------------------------------------------------------------
    getValueByDataField($control: JQuery, $tr: JQuery, datafield: string) {
        let $field = $tr.find(`.field[data-browsedatafield="${datafield}"]`);
        let datatype = $field.attr('data-browsedatatype');
        let originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let field: any = {
            datafield: datafield,
            value: originalvalue
        };
        (<IFwBrowseColumn>(<any>window)[`FwBrowseColumn_${datatype}`]).getFieldValue($control, $tr, $field, field, originalvalue);
        return field.value;
    }
    //---------------------------------------------------------------------------------
}

var FwBrowse = new FwBrowseClass();

interface IRenderGridOptions {

}

interface IFwBrowseColumn {
    databindfield($browse: JQuery, $field: JQuery, dt: DataTable, dtRow: any, $tr: JQuery): void;
    getFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, field: any, originalvalue: string): void;
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void;
    isModified($browse: JQuery, $tr: JQuery, $field: JQuery): boolean;
    setFieldViewMode($browse: JQuery, $tr: JQuery, $field: JQuery): void;
    setFieldEditMode($browse: JQuery, $tr: JQuery, $field: JQuery): void;
}

class FwBrowse_SetFieldValueData {
    value: any;
    text?: string;
}

class DataTable {
    ColumnIndex: any = {};
    Columns: Array<DataTableColumn> = [];
    Rows: Array<Array<any>> = [];
    PageNo: number = 0;
    PageSize: number = 15;
    TotalPages: number = 0;
    TotalRows: number = 0;
    ColumnNameByIndex: any = {};

    static toObjectList<T>(dt: DataTable): Array<T> {
        let objects = [];
        for (let rowno = 0; rowno < dt.Rows.length; rowno++) {
            let row = dt.Rows[rowno];
            let object: any = {};
            for (let colno = 0; colno < dt.Columns.length; colno++) {
                let column = dt.Columns[colno];
                object[dt.Columns[colno].DataField] = row[colno];
            }
            objects.push(object);
        }
        return objects;
    }

    static objectListToDataTable<T>(getManyModel: GetManyModel<T>): DataTable {
        let dt = new DataTable();
        dt.PageNo = getManyModel.PageNo;
        dt.PageSize = getManyModel.PageSize;
        dt.TotalRows = getManyModel.TotalRows;
        if (getManyModel.Items.length > 0) {
            let record = getManyModel.Items[0];
            let colno = 0;
            for (let key in record) {
                // build the ColumnIndexs
                dt.ColumnIndex[key] = colno;
                dt.ColumnNameByIndex[colno] = key;

                // add the Column
                let column = new DataTableColumn();
                column.Name = key;
                column.DataField = key;
                dt.Columns.push(column);
            }
        }
        for (let recno = 0; recno < getManyModel.Items.length; recno++) {
            let record = getManyModel.Items[recno];
            let row = new Array<any>();
            for (let key in record) {
                let cellValue = record[key];
                row.push(cellValue);
            }
            dt.Rows.push(row);
        }
        return dt;
    }
}

class DataTableColumn {
    Name: string = '';
    DataField: string = '';
    DataType: number = 0;
    IsUniqueId: boolean = false;
    IsVisible: boolean = true;
}

class BrowseRequest {
    miscfields?: any = {};
    module?: string = '';
    options?: any = {};
    orderby?: string = '';
    orderbydirection?: string = '';
    top?: number = 0;
    pageno?: number = 0;
    pagesize?: number = 0;
    searchfieldoperators?: string[] = [];
    searchfields?: string[] = [];
    searchfieldvalues?: string[] = [];
    searchfieldtypes?: string[] = [];
    searchseparators?: string[] = [];
    searchcondition?: string[] = [];
    searchconjunctions?: string[] = [];
    uniqueids?: any = {};
    boundids?: any = {};
    filterfields?: any = {};
    activeview?: string = '';
}

class GetManyRequest {
    pageno: number = 0;
    pagesize: number = 0;
    sort: string = '';
    //options: this.getOptions($control);
    filters: Array<GetManyFilter> = [];
}

class GetManyFilter {
    fieldName: string;
    comparisonOperator: string;
    fieldValue: string;
    searchSeparator: string;
    fieldType: string;
}
