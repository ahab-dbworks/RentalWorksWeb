import { AdminModule } from "../../shared/AdminModule";
import { TestUtils } from "../../shared/TestUtils";

//---------------------------------------------------------------------------------------
export class Alert extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Alert';
        this.moduleId = '6E5F47FB-1F18-443E-B464-9D2351857361';
        this.moduleCaption = 'Alert';

        this.defaultNewRecordToExpect = {
            ModuleName: "",
            AlertName: "",
            ActionNew: false,
            ActionEdit: false,
            ActionDelete: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ModuleName: TestUtils.randomIntegerBetween(2,20), 
                    AlertName: "GlobalScope.TestToken~1.TestToken",
                    ActionNew: true,
                    ActionEdit: true,
                    ActionDelete: false,
                },
                seekObject: {
                    AlertName: "GlobalScope.TestToken~1.TestToken",
                }
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ModuleName: "|NOTEMPTY|",
            AlertName: this.newRecordsToCreate[0].record.AlertName,
            ActionNew: this.newRecordsToCreate[0].record.ActionNew,
            ActionEdit: this.newRecordsToCreate[0].record.ActionEdit,
            ActionDelete: this.newRecordsToCreate[0].record.ActionDelete,
            Inactive: false
        }

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

        this.defaultNewRecordToExpect = {
            ModuleName: "",
            FieldName: "",
            CustomTableName: "customvaluesstring",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ModuleName: TestUtils.randomIntegerBetween(2, 20), 
                    FieldName: "GlobalScope.TestToken~1.TestToken",
                    CustomTableName: "customvaluesdatetime",
                },
                seekObject: {
                    FieldName: "GlobalScope.TestToken~1.TestToken",
                }
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ModuleName: "|NOTEMPTY|",
            FieldName: this.newRecordsToCreate[0].record.FieldName,
            CustomTableName: this.newRecordsToCreate[0].record.CustomTableName,
        }

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

        this.defaultNewRecordToExpect = {
            BaseForm: "",
            Description: "",
            Active: true,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    BaseForm: TestUtils.randomIntegerBetween(2, 20),
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                }
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            BaseForm: "|NOTEMPTY|",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Active: true,
        }


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

        this.defaultNewRecordToExpect = {
            ModuleName: "",
            RuleName: "",
            ConsiderBlanks: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    RuleName: "GlobalScope.TestToken~1.TestToken",
                    ConsiderBlanks: true,
                    ModuleName: TestUtils.randomIntegerBetween(2, 20),  // must be last
                },
                seekObject: {
                    RuleName: "GlobalScope.TestToken~1.TestToken",
                }
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ModuleName: "|NOTEMPTY|",
            RuleName: this.newRecordsToCreate[0].record.RuleName,
            ConsiderBlanks: this.newRecordsToCreate[0].record.ConsiderBlanks,
        }


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

        this.defaultNewRecordToExpect = {
            Name: "",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Name: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Name: "GlobalScope.TestToken~1.TestToken",
                }
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Name: this.newRecordsToCreate[0].record.Name.toUpperCase(),
        }


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

        this.defaultNewRecordToExpect = {
            FirstName: "",
            LastName: "",
            GroupName: "",
            Email: "",
            OfficeLocation: "",
            Warehouse: "",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    FirstName: TestUtils.randomFirstName(),
                    LastName: TestUtils.randomLastName(),
                    LoginName: "GlobalScope.TestToken~1.TestToken"+"_X",  // must be unique
                    Password: TestUtils.randomAlphanumeric(20),
                    Email: TestUtils.randomEmail(),
                    GroupId: 1,
                    OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
                    Warehouse: "GlobalScope.User~ME.Warehouse",
                    DefaultDepartmentType: "GlobalScope.User~ME.DefaultDepartmentType",
                    RentalDepartment: "GlobalScope.User~ME.RentalDepartment",
                    SalesDepartment: "GlobalScope.User~ME.SalesDepartment",
                    MiscDepartment: "GlobalScope.User~ME.MiscDepartment",
                    LaborDepartment: "GlobalScope.User~ME.LaborDepartment",
                },
                seekObject: {
                    LoginName: "GlobalScope.TestToken~1.TestToken"+"_X",  // must be unique
                }
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            FirstName: this.newRecordsToCreate[0].record.FirstName.toUpperCase(),
            LastName: this.newRecordsToCreate[0].record.LastName.toUpperCase(),
            LoginName: this.newRecordsToCreate[0].record.LoginName.toUpperCase(),
            GroupName: "|NOTEMPTY|",
            Email: this.newRecordsToCreate[0].record.Email,
            OfficeLocation: this.newRecordsToCreate[0].record.OfficeLocation.toUpperCase(),
            Warehouse: this.newRecordsToCreate[0].record.Warehouse.toUpperCase(),
        }


    }
    //---------------------------------------------------------------------------------------
}
