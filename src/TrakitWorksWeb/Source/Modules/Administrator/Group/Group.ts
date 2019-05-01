class Group extends Fw.Modules.FwGroup {
    constructor() {
        super();
        this.id = Constants.Modules.Administrator.Group.id;
        this.caption = Constants.Modules.Administrator.Group.caption;
        this.nav = Constants.Modules.Administrator.Group.nav;
    }
}

var GroupController = new Group();