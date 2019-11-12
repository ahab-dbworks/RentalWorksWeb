import { FrontEndModule } from "../../shared/FrontEndModule";
import { TestUtils } from "../../shared/TestUtils";
import { GridBase } from "../../shared/GridBase";

//---------------------------------------------------------------------------------------
export class Staging extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Staging';
        this.moduleId = 'C3B5EEC9-3654-4660-AD28-20DE8FF9044D';
        this.moduleCaption = 'Staging / Check-Out';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ReceiveFromVendor extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ReceiveFromVendor';
        this.moduleId = '00539824-6489-4377-A291-EBFE26325FAD';
        this.moduleCaption = 'Receive From Vendor';

        let itemsGrid: GridBase = new GridBase("Receive Items Grid", "POReceiveItemGrid");
        this.grids.push(itemsGrid);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
