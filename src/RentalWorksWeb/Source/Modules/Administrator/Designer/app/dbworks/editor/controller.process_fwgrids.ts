declare var FwBrowse: any;
declare var FwFormField: any;
var geval = eval;

namespace dbworks.editor.controllers {

    export class process_fwgrids {

        _editor: editor.controllers.main;
        grids: fw_grid[];

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
            this.grids = [];
        }        

        process(file: editor.models.file): void {
            var $body = jQuery(file.fileContents);
            this.find_grids($body);            
            this.evaluate_grid_javascript().done(() => {
                this.render_grid($body);
            });
            
        }

        process_2($container: JQuery): void {

            var $grids = $container.find('[data-control="FwGrid"]');

            $grids.each((_i, _e) => {

                var $grid = jQuery(_e), gridName: string = $grid.data('grid');

                for (var i = 0; i < this._editor.files.length; i++) {
                    if (this._editor.files[i].fileName.indexOf(gridName) != -1) {
                        this.render_grid_2($grid, this._editor.files[i]);
                        break;
                    }
                }                                

            });

        }



        find_grids($body: JQuery): void {            

            var $grids = $body.find('[data-control="FwGrid"]');
            
            $grids.each((_i, _e) => {
                var grid = new fw_grid();
                grid.$grid = jQuery(_e);
                grid.name = jQuery(_e).data('grid');
                var modGridName: string = jQuery(_e).data('grid');
                modGridName = modGridName.replace('Grid', '');
                this._editor.files.forEach((_f) => {                    
                    if (_f.fileName.indexOf(modGridName) != -1 && _f.ext == 'js') {
                        grid.javascript = _f.fileContents;
                    }

                });
                this.grids.push(grid);
            });

            this.update_grids_with_content();            
        }

        update_grids_with_content(): void {
            this.grids.forEach((_v, _i) => {

                for (var i = 0; i < this._editor.files.length; i++) {

                    if (this._editor.files[i].fileName.indexOf(_v.name) != -1) {

                        _v.body = this._editor.files[i].fileContents;
                        break;

                    }

                }

            });
        }        

        evaluate_grid_javascript(): JQueryDeferred<boolean> {
            var $promise = $.Deferred(), i = 0;

            while(i < this.grids.length) {
                var x = this.grids[i].javascript;
                //eval(x);

                geval(x);

                i++;
            }

            if (i == this.grids.length) {
                $promise.resolveWith(true);
            }
            
            return $promise;
        }

        render_grid($control: JQuery): void {

            this.grids.forEach((_v) => {                
                var $form = jQuery('#preview_view').next(),
                    $grid = $form.find('div[data-grid="' + _v.name + '"]'),
                    $gridControl: JQuery = jQuery(_v.body);

                $grid.empty().append($gridControl);
                $gridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderTypeId: FwFormField.getValueByDataField($control, 'OrderTypeId')
                    };
                });
                $gridControl.data('beforesave', function (request) {
                    request.OrderTypeId = FwFormField.getValueByDataField($control, 'OrderTypeId')
                });
                FwBrowse.init($gridControl);
                FwBrowse.renderRuntimeHtml($gridControl);

            });
            
        }

        render_grid_2($container: JQuery, file: editor.models.file): void {
            var content = file.fileContents, $root = jQuery('<div>' + content + '</div>'), $fwControls = $root.find('.fwcontrol');
            if (file.ext === 'htm') {
                $container.empty();
                $container.html(file.fileContents);
                var $controls = $container.find('.fwcontrol');
                FwBrowse.init($container);
                FwBrowse.renderRuntimeHtml($container);
                var $grid = $container.find('[data-name="' + $container.data('grid') + '"]');
                FwBrowse.search($grid);

            } else {
                $container.html('<strong>File type ' + file.ext.toUpperCase() + ' not supported in preview.</strong>');
            }

        }

    }

    class fw_grid {        

        $grid?: JQuery;
        name?: string;
        body?: string;
        javascript: string;
        
    }

}