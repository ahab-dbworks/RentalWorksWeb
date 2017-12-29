var geval = eval;
var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var process_fwgrids = /** @class */ (function () {
                function process_fwgrids(editor_controller) {
                    this._editor = editor_controller;
                    this.grids = [];
                }
                process_fwgrids.prototype.process = function () {
                    var $container = jQuery('.fwcontrol.fwtabs');
                    this.find_grids($container);
                    this.render_grid($container);
                };
                process_fwgrids.prototype.find_grids = function ($body) {
                    var _this = this;
                    var $grids = $body.find('[data-control="FwGrid"]');
                    $grids.each(function (_i, _e) {
                        var newGrid = new fw_grid;
                        newGrid.$grid = jQuery(_e);
                        newGrid.name = _this.clean_browse_names(jQuery(_e).data('grid'));
                        newGrid.controllerName = newGrid.name + 'Controller';
                        newGrid.$browseHTML = jQuery(jQuery('#tmpl-grids-' + newGrid.name + 'Browse').html());
                        _this.grids.push(newGrid);
                    });
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
                    var $promise = jQuery.Deferred(), i = 0;
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
                process_fwgrids.prototype.render_grid = function ($container) {
                    this.grids.forEach(function (_v, _i) {
                        var gridName = _v.name;
                        var $grid = $container.find('div[data-grid="' + gridName + '"]');
                        var $control = FwBrowse.loadGridFromTemplate(gridName);
                        $grid.empty().append($control);
                        FwBrowse.init($control);
                        FwBrowse.renderRuntimeHtml($control);
                        var $finalGrid = $container.find('[data-name="' + gridName + '"]');
                        FwBrowse.search($finalGrid);
                    });
                };
                process_fwgrids.prototype.clean_browse_names = function (browseName) {
                    //browseName = browseName.replace('Grid', '');
                    browseName = browseName.replace('Browse', '');
                    return browseName;
                };
                return process_fwgrids;
            }());
            controllers.process_fwgrids = process_fwgrids;
            var fw_grid = /** @class */ (function () {
                function fw_grid() {
                }
                return fw_grid;
            }());
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.process_fwgrids.js.map