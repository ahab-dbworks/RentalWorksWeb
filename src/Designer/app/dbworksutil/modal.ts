namespace dbworksutil {
    // we use bootstrap's modal
    export class modal {

        targetID: string;
        $target: JQuery;
        config: modal_config;
        // width
        // height
          
        constructor(targetID: string);
        constructor($target: JQuery);
        constructor(target: string | JQuery, config?: modal_config) {
            if (typeof target === 'string') {
                this.targetID = target;
                this.$target = jQuery('#' + target);
            }

            if (typeof target === 'object') {
                this.$target = target;
            }                        
            
            this.config = new modal_config;
            this._configure();
        }

        private _configure(): void {
            //var config: Materialize.ModalOptions = { complete: function () { alert('MODAL CLOSING'); } };
            var config: any;
            config
            this.$target.modal(config);
        }

        open(): modal {
            this.$target.modal('show');            
            return this;
        }

        close(): modal {
            this.$target.modal('hide');
            return this;
        }

        toggle(): modal {
            this.$target.modal('toggle');
            return this;
        }

        toggle_loading(): modal {
            this.$target.find('.loader').slideToggle('fast');
            return this;
        }

    }

    class modal_config {
        title?: string;
        width?: number;
        height?: number;
        on_open?: () => any;
        on_close?: () => any;
    }
}