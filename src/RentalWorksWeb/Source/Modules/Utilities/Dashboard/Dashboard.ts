class Dashboard {

    constructor() {
    }

    loadDashboard() {
        program.navigate('home');
    }
 
}

(<any>window).DashboardController = new Dashboard();