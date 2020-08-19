class Group extends Fw.Modules.FwGroup {
    constructor() {
        super();
        this.id = Constants.Modules.Administrator.children.Group.id;
        this.caption = Constants.Modules.Administrator.children.Group.caption;
        this.nav = Constants.Modules.Administrator.children.Group.nav;
    }
}
var GroupController = new Group();
//# sourceMappingURL=Group.js.map