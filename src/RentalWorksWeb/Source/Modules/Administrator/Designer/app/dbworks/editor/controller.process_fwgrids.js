var geval = eval;
var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var process_fwgrids = (function () {
                function process_fwgrids(editor_controller) {
                    this._editor = editor_controller;
                    this.grids = [];
                }
                process_fwgrids.prototype.process = function (file) {
                    var _this = this;
                    var $body = jQuery(file.fileContents);
                    this.find_grids($body);
                    this.evaluate_grid_javascript().done(function () {
                        _this.render_grid($body);
                    });
                };
                process_fwgrids.prototype.process_2 = function ($container) {
                    var _this = this;
                    var $grids = $container.find('[data-control="FwGrid"]');
                    $grids.each(function (_i, _e) {
                        var $grid = jQuery(_e), gridName = $grid.data('grid');
                        for (var i = 0; i < _this._editor.files.length; i++) {
                            if (_this._editor.files[i].fileName.indexOf(gridName) != -1) {
                                _this.render_grid_2($grid, _this._editor.files[i]);
                                break;
                            }
                        }
                    });
                };
                process_fwgrids.prototype.find_grids = function ($body) {
                    var _this = this;
                    var $grids = $body.find('[data-control="FwGrid"]');
                    $grids.each(function (_i, _e) {
                        var grid = new fw_grid();
                        grid.$grid = jQuery(_e);
                        grid.name = jQuery(_e).data('grid');
                        var modGridName = jQuery(_e).data('grid');
                        modGridName = modGridName.replace('Grid', '');
                        _this._editor.files.forEach(function (_f) {
                            if (_f.fileName.indexOf(modGridName) != -1 && _f.ext == 'js') {
                                grid.javascript = _f.fileContents;
                            }
                        });
                        _this.grids.push(grid);
                    });
                    this.update_grids_with_content();
                };
                process_fwgrids.prototype.update_grids_with_content = function () {
                    var _this = this;
                    this.grids.forEach(function (_v, _i) {
                        for (var i = 0; i < _this._editor.files.length; i++) {
                            if (_this._editor.files[i].fileName.indexOf(_v.name) != -1) {
                                _v.body = _this._editor.files[i].fileContents;
                                break;
                            }
                        }
                    });
                };
                process_fwgrids.prototype.evaluate_grid_javascript = function () {
                    var $promise = $.Deferred(), i = 0;
                    while (i < this.grids.length) {
                        var x = this.grids[i].javascript;
                        //eval(x);
                        geval(x);
                        i++;
                    }
                    if (i == this.grids.length) {
                        $promise.resolveWith(true);
                    }
                    return $promise;
                };
                process_fwgrids.prototype.render_grid = function ($control) {
                    this.grids.forEach(function (_v) {
                        var $form = jQuery('#preview_view').next(), $grid = $form.find('div[data-grid="' + _v.name + '"]'), $gridControl = jQuery(_v.body);
                        $grid.empty().append($gridControl);
                        $gridControl.data('ondatabind', function (request) {
                            request.uniqueids = {
                                OrderTypeId: FwFormField.getValueByDataField($control, 'OrderTypeId')
                            };
                        });
                        $gridControl.data('beforesave', function (request) {
                            request.OrderTypeId = FwFormField.getValueByDataField($control, 'OrderTypeId');
                        });
                        FwBrowse.init($gridControl);
                        FwBrowse.renderRuntimeHtml($gridControl);
                    });
                };
                process_fwgrids.prototype.render_grid_2 = function ($container, file) {
                    var content = file.fileContents, $root = jQuery('<div>' + content + '</div>'), $fwControls = $root.find('.fwcontrol');
                    if (file.ext === 'htm') {
                        $container.empty();
                        $container.html(file.fileContents);
                        var $controls = $container.find('.fwcontrol');
                        FwBrowse.init($container);
                        FwBrowse.renderRuntimeHtml($container);
                        var $grid = $container.find('[data-name="' + $container.data('grid') + '"]');
                        FwBrowse.search($grid);
                    }
                    else {
                        $container.html('<strong>File type ' + file.ext.toUpperCase() + ' not supported in preview.</strong>');
                    }
                };
                return process_fwgrids;
            }());
            controllers.process_fwgrids = process_fwgrids;
            var fw_grid = (function () {
                function fw_grid() {
                }
                return fw_grid;
            }());
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.process_fwgrids.js.map