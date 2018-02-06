declare var FwModule: any;
declare var FwBrowse: any;

class Dashboard {

    constructor() {
    }

    loadDashboard() {
        window.location.href = window.location.origin + '/rentalworksweb/'
    }
 
}

(<any>window).DashboardController = new Dashboard();