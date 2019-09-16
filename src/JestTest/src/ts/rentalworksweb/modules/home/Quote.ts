import { HomeModule } from "../../../shared/HomeModule";

export class Quote extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Quote';
        this.moduleId = '4D785844-BE8A-4C00-B1FA-2AA5B05183E5';
        this.moduleCaption = 'Quote';
        this.canDelete = false
    }
    //---------------------------------------------------------------------------------------
}