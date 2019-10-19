import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';
import { GlobalScope } from '../shared/GlobalScope';
import { NewRecordToCreate } from './ModuleBase';

//---------------------------------------------------------------------------------------
export class GridBase {
    gridName: string;
    //moduleName: string;
    //moduleId: string;
    //moduleCaption: string;
    //browseOpenTimeout: number = 120000; // 120 seconds
    //browseSeekTimeout: number = 120000; // 120 seconds
    //deleteTimeout: number = 120000; // 120 seconds
    //formOpenTimeout: number = 120000; // 120 seconds
    //formSaveTimeout: number = 120000; // 120 seconds

    canNew?: boolean = true;
    canView?: boolean = true;
    canEdit?: boolean = true;
    canDelete?: boolean = true;

    defaultNewRecordToExpect?: any;
    newRecordsToCreate?: NewRecordToCreate[];

    globalScopeRef? = GlobalScope;

    //---------------------------------------------------------------------------------------
    constructor() {
        //this.moduleName = 'UnknownModule';
        //this.moduleId = '99999999-9999-9999-9999-999999999999';
        //this.moduleCaption = 'UnknownModule';
    }
    //---------------------------------------------------------------------------------------
}