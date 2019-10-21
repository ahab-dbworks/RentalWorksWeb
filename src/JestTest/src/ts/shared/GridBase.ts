import { GlobalScope } from '../shared/GlobalScope';
import { NewRecordToCreate } from './ModuleBase';

//---------------------------------------------------------------------------------------
export class GridBase {
    gridName: string;
    gridClass: string[];
    //moduleName: string;
    //moduleId: string;
    //moduleCaption: string;
    deleteTimeout: number;
    saveTimeout: number;

    canNew: boolean;
    canDelete: boolean;

    defaultNewRecordToExpect?: any;
    newRecordsToCreate?: NewRecordToCreate[];

    globalScopeRef?= GlobalScope;

    //---------------------------------------------------------------------------------------
    constructor(gridName: string, gridClass?: string[]) {
        //this.moduleName = 'UnknownModule';
        //this.moduleId = '99999999-9999-9999-9999-999999999999';
        //this.moduleCaption = 'UnknownModule';

        if ((gridClass == undefined) || (gridClass == null)) {
            gridClass = new Array();
        }
        this.gridName = gridName;
        this.gridClass = gridClass;
        this.canNew = true;
        this.canDelete = true;
        this.deleteTimeout = 120000; // 120 seconds
        this.saveTimeout = 120000; // 120 seconds
        this.globalScopeRef = GlobalScope;
    }
    //---------------------------------------------------------------------------------------
}