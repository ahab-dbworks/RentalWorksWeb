namespace dbworks.editor.controllers {

    export class process_fwgrids {

        _editor: editor.controllers.main;
        grids: fw_grid[];

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
            this.grids = [];
        }                

        process(): void {
            var $container = jQuery('.fwcontrol.fwtabs');            
            this.find_grids($container);
            this.render_grid($container);
        }        

        find_grids($body: JQuery): void {
            var $grids = $body.find('[data-control="FwGrid"]');

            $grids.each((_i, _e) => {

                var newGrid = new fw_grid;
                newGrid.$grid = jQuery(_e);
                newGrid.name = this.clean_browse_names(jQuery(_e).data('grid'));
                newGrid.controllerName = newGrid.name + 'Controller';
                newGrid.$browseHTML = jQuery(jQuery('#tmpl-grids-' + newGrid.name + 'Browse').html());
                this.grids.push(newGrid);
            });
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
            var $promise = jQuery.Deferred<any>(), i = 0;

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

        render_grid($container: JQuery): void {            
            this.grids.forEach((_v, _i) => {                
                var gridName: string = _v.name;
                var $grid: any = $container.find('div[data-grid="' + gridName + '"]');
                var $control: any = FwBrowse.loadGridFromTemplate(gridName);               
                $grid.empty().append($control);                
                FwBrowse.init($control);
                FwBrowse.renderRuntimeHtml($control);                
                var $finalGrid: any = $container.find('[data-name="' + gridName + '"]');
                FwBrowse.search($finalGrid);

            });
        }

        clean_browse_names(browseName: string): string {
            //browseName = browseName.replace('Grid', '');
            browseName = browseName.replace('Browse', '');
            return browseName;
        }

    }

    class fw_grid {        

        $grid?: JQuery;
        name?: string;
        controllerName?: string;
        $browseHTML?: JQuery;
        body?: string;
        javascript: string;
        
    }

}