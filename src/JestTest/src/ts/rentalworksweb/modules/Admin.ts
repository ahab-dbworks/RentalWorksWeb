import { AdminModule } from "../../shared/AdminModule";
import { TestUtils } from "../../shared/TestUtils";
import { GridBase } from "../../shared/GridBase";

//---------------------------------------------------------------------------------------
export class Alert extends AdminModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Alert';
        this.moduleId = 'gFfpaR5mDAzX';
        this.moduleCaption = 'Alert';

        let recipientGrid: GridBase = new GridBase("Recipients Grid", "AlertWebUsersGrid");

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
                    ModuleName: TestUtils.randomIntegerBetween(5,10), 
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

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: recipientGrid,
                recordToCreate: {
                    record: {
                        UserId: 1,
                    },
                },
            },
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
        this.moduleId = 'cZHPJQyBxolS';
        this.moduleCaption = 'Custom Field';

        this.defaultNewRecordToExpect = {
            ModuleName: "",
            FieldName: "",
            CustomTableName: "customvaluesstring",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ModuleName: TestUtils.randomIntegerBetween(5,10), 
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
        this.moduleId = '11txpzVKVGi2';
        this.moduleCaption = 'Custom Form';

        this.defaultNewRecordToExpect = {
            BaseForm: "",
            Description: "",
            Active: true,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    BaseForm: TestUtils.randomIntegerBetween(5, 10),
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
        this.moduleId = 'v7oBspDLjli8';
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
                    ModuleName: TestUtils.randomIntegerBetween(5,10),  // must be last
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
        this.moduleId = '3XHEm3Q8WSD8z';
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
        this.moduleId = '0vP4rXxgGL1M';
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
        this.moduleId = 'yeqaGIUYfYNX';
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
        this.moduleId = 'r1fKvn1KaFd0u';
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
