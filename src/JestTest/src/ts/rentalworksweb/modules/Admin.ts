import { AdminModule } from "../../shared/AdminModule";

//---------------------------------------------------------------------------------------
export class Alert extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Alert';
        this.moduleId = '6E5F47FB-1F18-443E-B464-9D2351857361';
        this.moduleCaption = 'Alert';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomField extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomField';
        this.moduleId = 'C98C4CB4-2036-4D70-BC29-8F5A2874B178';
        this.moduleCaption = 'Custom Field';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomForm extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomForm';
        this.moduleId = 'CB2EF8FF-2E8D-4AD0-B880-07037B839C5E';
        this.moduleCaption = 'Custom Form';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DuplicateRule extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DuplicateRule';
        this.moduleId = '2E0EA479-AC02-43B1-87FA-CCE2ABA6E934';
        this.moduleCaption = 'Duplicate Rule';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EmailHistory extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EmailHistory';
        this.moduleId = '3F44AC27-CE34-46BA-B4FB-A8AEBB214167';
        this.moduleCaption = 'Email History';
        this.canNew = false;
        this.canDelete = false;
        this.canEdit = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Group extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Group';
        this.moduleId = '9BE101B6-B406-4253-B2C6-D0571C7E5916';
        this.moduleCaption = 'Group';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Hotfix extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Hotfix';
        this.moduleId = '9D29A5D9-744F-40CE-AE3B-09219611A680';
        this.moduleCaption = 'Hotfix';
        this.canNew = false;
        this.canDelete = false;
        this.canEdit = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class User extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'User';
        this.moduleId = '79E93B21-8638-483C-B377-3F4D561F1243';
        this.moduleCaption = 'User';
    }
    //---------------------------------------------------------------------------------------
}
