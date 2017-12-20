namespace dbworksutil.ui {

    export class side_nav {

        private _targetID: string;
        private _container: HTMLElement;
        private _config: side_nav_config;

        constructor(targetID?: string) {
            this._config = new side_nav_config;
            this._container = document.getElementById(targetID);            
        }

        side_nav(action: string);
        side_nav(config: side_nav_config);
        side_nav(action?: string, config?: side_nav_config): void {
            if (typeof config === 'string') {
                // do action
                this._side_nav_action(action);
            } else {
                // do object configuration
                this._side_nav_configuration();
            }
        }   

        open(): void {
            this._side_nav_configuration();
            this._setup();
            this._side_nav_action('open');
            window.addEventListener('click', clickEvent);

            function clickEvent() {
                alert('test!');
            }
        }

        close(): void {
            this._side_nav_action('close');
        }

        private _setup(): void {
            //if (!this._is_hidden()) this._container.style.visibility = 'hidden';            
        }    

        private _side_nav_action(action: string): void {

            //window.requestAnimationFrame((step) => { this._toggle_slide(step); });
            var _self = this;
            this._toggle_slide();
            
        }

        private _click_event(): void {
            alert('test');
            //this._container.removeEventListener("click", () => {
            //    document.addEventListener('click', () => { alert('test'); });
            //});
        }

        private _side_nav_configuration(): void {

            this._config.hasOverlay = false;
            this._config.width = 315 ;
            this._config.slideSpeed = 1000;
            this._config.edge = 'left';

        }

        private _toggle_slide(onComplete?: () => void): void {
            //console.log(frameInfo);
            //var prevLeft = 0;
            ////var timer = setInterval(() => {
            //    if (prevLeft !== this._config.width) {
            //        prevLeft = (prevLeft + 1);
            //      this._container.style.marginLeft = prevLeft + 'px';
            //    }
            ////}, this._config.slideSpeed);
            //    window.requestAnimationFrame((step) => { this._toggle_slide(step); });
            var i = 0;
            var prevLeft = 0;
            while (i < this._config.width) {

                setTimeout(() => {

                    if (prevLeft !== this._config.width) {
                        prevLeft = (prevLeft + 1);
                        this._container.style.marginLeft = prevLeft + 'px';
                    }

                }, i * 1);
                
                i++;

            }
            
        }

        // helpers
        private _is_hidden(): boolean {
            return (this._container.offsetParent === null)
        }

        

    }

    class side_nav_config {
        hasOverlay?: boolean;
        width?: number;
        slideSpeed?: number;  
        edge?: string;
    }

}