routes.push({ pattern: /^module\/group/, action: function (match) { return GroupController.getModuleScreen(); } });
var RentalWorks;
(function (RentalWorks) {
    var Modules;
    (function (Modules) {
        var Administrator;
        (function (Administrator) {
            class Group extends Fw.Modules.FwGroup {
                constructor() {
                    super();
                    this.id = '849D2706-72EC-48C0-B41C-0890297BF24B';
                }
            }
            Administrator.Group = Group;
        })(Administrator = Modules.Administrator || (Modules.Administrator = {}));
    })(Modules = RentalWorks.Modules || (RentalWorks.Modules = {}));
})(RentalWorks || (RentalWorks = {}));
var GroupController = new RentalWorks.Modules.Administrator.Group();
//# sourceMappingURL=Group.js.map