import { HomeModule } from "../../../shared/HomeModule";

export class Vendor extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Vendor';
        this.moduleId = 'AE4884F4-CB21-4D10-A0B5-306BD0883F19';
        this.moduleCaption = 'Vendor';

        this.defaultNewRecordToExpect = {
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Vendor: "GlobalScope.TestToken~1.TestToken",
                    VendorNumber: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    VendorDisplayName: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

    }
    //---------------------------------------------------------------------------------------
}