routes.push({ pattern: /^module\/group$/, action: function (match: RegExpExecArray) { return GroupController.getModuleScreen(); } });

namespace RentalWorks.Modules.Administrator {
    export class Group extends Fw.Modules.FwGroup {
        constructor() {                             
            super();
            this.id = '849D2706-72EC-48C0-B41C-0890297BF24B';
        }
    }
}

var GroupController = new RentalWorks.Modules.Administrator.Group();