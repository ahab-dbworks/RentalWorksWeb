import { HomeModule } from "../../../shared/HomeModule";

export class Customer extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Customer';
        this.moduleId = '214C6242-AA91-4498-A4CC-E0F3DCCCE71E';
        this.moduleCaption = 'Customer';

        this.defaultNewObject = {
            Customer: "GlobalScope.TestToken~1.TestToken",
            CustomerNumber: "GlobalScope.TestToken~1.ShortTestToken",
            CustomerTypeId: 1,
            CreditStatusId: 1,
        };

    }
    //---------------------------------------------------------------------------------------
}
