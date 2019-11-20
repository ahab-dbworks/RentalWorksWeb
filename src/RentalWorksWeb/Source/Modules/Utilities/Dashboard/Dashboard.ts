class Dashboard {
    Module: string = 'Dashboard';
    caption: string = Constants.Modules.Utilities.children.Dashboard.caption;
    nav: string = Constants.Modules.Utilities.children.Dashboard.nav;
    id: string = Constants.Modules.Utilities.children.Dashboard.id;

    loadDashboard() {
        program.navigate('home');
    }
}

var DashboardController = new Dashboard();