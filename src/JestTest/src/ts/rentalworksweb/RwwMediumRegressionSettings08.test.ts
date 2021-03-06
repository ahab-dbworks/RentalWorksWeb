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

        this.MediumRegressionOnModule(new PropsCondition());
        this.MediumRegressionOnModule(new Region());
        this.MediumRegressionOnModule(new RepairItemStatus());
        this.MediumRegressionOnModule(new SetCondition());
        this.MediumRegressionOnModule(new SetSurface());
        this.MediumRegressionOnModule(new SetOpening());
        this.MediumRegressionOnModule(new WallDescription());
        this.MediumRegressionOnModule(new WallType());
        this.MediumRegressionOnModule(new ShipVia());
        this.MediumRegressionOnModule(new Source());
        this.MediumRegressionOnModule(new AvailabilitySettings());
        this.MediumRegressionOnModule(new DefaultSettings());
        this.MediumRegressionOnModule(new EmailSettings());
        this.MediumRegressionOnModule(new InventorySettings());
        this.MediumRegressionOnModule(new LogoSettings());
        this.MediumRegressionOnModule(new DocumentBarCodeSettings());
        this.MediumRegressionOnModule(new SystemSettings());

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
