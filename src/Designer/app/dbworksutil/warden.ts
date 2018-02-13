namespace dbworksutil {


    // TODO
    // - kill events that take longer than the _timer property

    export class warden {

        private _timer: number;
        private _interval: any;
        private _hooked_events: ((...arg) => void)[];

        constructor() {
            this._timer = 500;
            this._interval = null;
            this._hooked_events = [];
        }

        watch(): void {
            this._runner();
        }

        hook(singleton: (...arg) => void): void {
            this._hooked_events.push(singleton);
        }

        stop_watch(): void {
            clearInterval(this._interval);
        }

        private _runner(): void {

            this._interval = setInterval(() => {

                console.log('warden running');

                this._hooked_events.forEach((event) => {
                
                    event();

                });

            }, this._timer);

        }

    }

}