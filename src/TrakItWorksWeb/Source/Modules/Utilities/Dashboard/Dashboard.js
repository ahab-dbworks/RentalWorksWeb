routes.push({ pattern: /^module\/dashboard$/, action: function (match) { return DashboardController.loadDashboard(); } });
class Dashboard {
    constructor() {
        this.Module = 'Dashboard';
        this.caption = 'Dashboard';
        this.nav = 'module/dashboard';
        this.id = 'E01F0032-CFAA-4556-9F24-E4C28C5B50A1';
    }
    loadDashboard() {
        program.navigate('home');
    }
}
var DashboardController = new Dashboard();
//# sourceMappingURL=Dashboard.js.map