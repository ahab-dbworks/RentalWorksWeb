class BillingCycleEventsGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'BillingCycleEventsGrid';
        this.apiurl = 'api/v1/billingcycleevent';
    }
}

(<any>window).BillingCycleEventsGridController = new BillingCycleEventsGrid();
//----------------------------------------------------------------------------------------------