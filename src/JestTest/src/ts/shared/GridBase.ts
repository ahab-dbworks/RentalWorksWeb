import { FwGridBase } from '../fwjest/FwGridBase';

//---------------------------------------------------------------------------------------
export class GridBase extends FwGridBase {
    //---------------------------------------------------------------------------------------
    constructor(gridDisplayName: string, gridName: string, gridClass?: string[]) {
        super(gridDisplayName, gridName, gridClass);
    }
    //---------------------------------------------------------------------------------------
}