routes.push({ pattern: /^module\/dashboard$/, action: function (match: RegExpExecArray) { return DashboardController.loadDashboard(); } });

class Dashboard {
    Module: string = 'Dashboard';
    caption: string = 'Dashboard';
    nav: string = 'module/dashboard';
    id: string = 'E01F0032-CFAA-4556-9F24-E4C28C5B50A1';

    loadDashboard() {
        program.navigate('home');
    }
}

var DashboardController = new Dashboard();