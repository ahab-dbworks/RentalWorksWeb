import { HomeModule } from "../../../shared/HomeModule";

export class Asset extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Asset';
        this.moduleId = '1C45299E-F8DB-4AE4-966F-BE142295E3D6';
        this.moduleCaption = 'Asset';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}