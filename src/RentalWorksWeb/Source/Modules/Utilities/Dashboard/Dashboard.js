var Dashboard = (function () {
    function Dashboard() {
    }
    Dashboard.prototype.loadDashboard = function () {
        program.navigate('home');
    };
    return Dashboard;
}());
window.DashboardController = new Dashboard();
//# sourceMappingURL=Dashboard.js.map