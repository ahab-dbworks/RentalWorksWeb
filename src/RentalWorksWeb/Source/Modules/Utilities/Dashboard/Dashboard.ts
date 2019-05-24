class Dashboard {
    Module: string = 'Dashboard';
    caption: string = Constants.Modules.Utilities.Dashboard.caption;
    nav: string = Constants.Modules.Utilities.Dashboard.nav;
    id: string = Constants.Modules.Utilities.Dashboard.id;

    loadDashboard() {
        program.navigate('home');
    }
}

var DashboardController = new Dashboard();