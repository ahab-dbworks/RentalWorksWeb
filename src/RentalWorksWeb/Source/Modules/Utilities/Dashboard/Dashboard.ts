class Dashboard {

    constructor() {
    }

    loadDashboard() {
        window.location.href = window.location.origin + '/rentalworksweb/'
    }
 
}

(<any>window).DashboardController = new Dashboard();