class Dashboard {
    Module: string = 'Dashboard';
    caption: string = 'Dashboard';
    nav: string = 'module/dashboard';
    id: string = 'DF8111F5-F022-40B4-BAE6-23B2C6CF3705';

    loadDashboard() {
        program.navigate('home');
    }
}

var DashboardController = new Dashboard();