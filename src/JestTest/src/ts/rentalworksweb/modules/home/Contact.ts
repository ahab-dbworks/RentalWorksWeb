import { HomeModule } from "../../../shared/HomeModule";

export class Contact extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Contact';
        this.moduleId = '3F803517-618A-41C0-9F0B-2C96B8BDAFC4';
        this.moduleCaption = 'Contact';

        this.newRecordsToCreate = [
            {
                record: {
                    FirstName: "GlobalScope.TestToken~1.TestToken",
                    LastName: "GlobalScope.TestToken~1.TestToken",
                    Email: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    LastName: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];


    }
    //---------------------------------------------------------------------------------------
}