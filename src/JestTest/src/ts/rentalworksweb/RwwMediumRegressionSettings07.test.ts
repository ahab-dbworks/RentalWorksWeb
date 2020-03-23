import {
    OrderSetNo,
    OrderLocation, PaymentTerms, PaymentType, POApprovalStatus, POApproverRole, POClassification, POImportance, PORejectReason, 
    FormDesign, PresentationLayer, ProjectAsBuild, ProjectCommissioning, ProjectDeposit, ProjectDrawings, ProjectDropShipItems, ProjectItemsOrdered, PropsCondition,
    Region, RepairItemStatus, SetCondition, SetSurface, SetOpening, WallDescription, WallType, ShipVia, Source, AvailabilitySettings, DefaultSettings, EmailSettings,
    InventorySettings, LogoSettings, DocumentBarCodeSettings, SystemSettings, Warehouse, User,
} from './modules/AllModules';
import { MediumRegressionBaseTest } from './RwwMediumRegressionBase';

export class MediumRegressionSettingsTest extends MediumRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        this.LoadMyUserGlobal(new User());
        //this.OpenSpecificRecord(new DefaultSettings(), null, true);
        //this.OpenSpecificRecord(new InventorySettings(), null, true);
        //
        //let warehouseToSeek: any = {
        //    Warehouse: "GlobalScope.User~ME.Warehouse",
        //}
        //this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        this.MediumRegressionOnModule(new OrderSetNo());
        this.MediumRegressionOnModule(new OrderLocation());
        this.MediumRegressionOnModule(new PaymentTerms());
        this.MediumRegressionOnModule(new PaymentType());
        this.MediumRegressionOnModule(new POApprovalStatus());
        this.MediumRegressionOnModule(new POApproverRole());
        this.MediumRegressionOnModule(new POClassification());
        this.MediumRegressionOnModule(new POImportance());
        this.MediumRegressionOnModule(new PORejectReason());
        //this.MediumRegressionOnModule(new POApprover());                // module cannot be tested because there is no unique field that can be searched to validate or delete the record
        //this.MediumRegressionOnModule(new VendorInvoiceApprover());     // module cannot be tested because there is no unique field that can be searched to validate or delete the record
        this.MediumRegressionOnModule(new FormDesign());
        this.MediumRegressionOnModule(new PresentationLayer());
        this.MediumRegressionOnModule(new ProjectAsBuild());
        this.MediumRegressionOnModule(new ProjectCommissioning());
        this.MediumRegressionOnModule(new ProjectDeposit());
        this.MediumRegressionOnModule(new ProjectDrawings());
        this.MediumRegressionOnModule(new ProjectDropShipItems());
        this.MediumRegressionOnModule(new ProjectItemsOrdered());

    }
    //---------------------------------------------------------------------------------------
}

describe('MediumRegressionSettingsTest', () => {
    try {
        new MediumRegressionSettingsTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
