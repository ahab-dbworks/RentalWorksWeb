import { HomeModule } from "../../../shared/HomeModule";

export class Order extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Order';
        this.moduleId = '64C46F51-5E00-48FA-94B6-FC4EF53FEA20';
        this.moduleCaption = 'Order';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------

}