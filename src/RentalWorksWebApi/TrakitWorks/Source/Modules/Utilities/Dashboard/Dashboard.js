routes.push({ pattern: /^module\/dashboard$/, action: function (match) { return DashboardController.loadDashboard(); } });
class Dashboard {
    constructor() {
        this.Module = 'Dashboard';
        this.caption = Constants.Modules.Utilities.children.Dashboard.caption;
        this.nav = Constants.Modules.Utilities.children.Dashboard.nav;
        this.id = Constants.Modules.Utilities.children.Dashboard.id;
    }
    loadDashboard() {
        program.navigate('home');
    }
}
var DashboardController = new Dashboard();
//# sourceMappingURL=Dashboard.js.map