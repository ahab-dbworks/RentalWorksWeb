class BillingCycleEventsGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'BillingCycleEventsGrid';
        this.apiurl = 'api/v1/billingcycleevent';
    }
}

var BillingCycleEventsGridController = new BillingCycleEventsGrid();
//----------------------------------------------------------------------------------------------