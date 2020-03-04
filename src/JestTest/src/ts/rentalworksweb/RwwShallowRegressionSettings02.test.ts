import {
    InventoryAdjustmentReason, Attribute, InventoryCondition, InventoryGroup, InventoryRank, InventoryStatus, InventoryType,
    PartsCategory, RentalCategory, RetiredReason, SalesCategory, Unit, UnretiredReason, WarehouseCatalog, Crew, LaborRate, LaborPosition, LaborType, LaborCategory,
    CrewScheduleStatus, CrewStatus, MiscRate, MiscType, MiscCategory, OfficeLocation, OrderType, DiscountReason, MarketSegment, MarketType, OrderSetNo,
    OrderLocation, PaymentTerms, PaymentType, POApprovalStatus, POApproverRole, POClassification, POImportance, PORejectReason, POType, POApprover, VendorInvoiceApprover,
    FormDesign, PresentationLayer, ProjectAsBuild, ProjectCommissioning, ProjectDeposit, ProjectDrawings, ProjectDropShipItems, ProjectItemsOrdered,
} from './modules/AllModules';
import { ShallowRegressionBaseTest } from './RwwShallowRegressionBase';

export class ShallowRegressionSettingsTest extends ShallowRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        this.ShallowRegressionOnModule(new InventoryAdjustmentReason());
        this.ShallowRegressionOnModule(new Attribute());
        this.ShallowRegressionOnModule(new InventoryCondition());
        this.ShallowRegressionOnModule(new InventoryGroup());
        this.ShallowRegressionOnModule(new InventoryRank());
        this.ShallowRegressionOnModule(new InventoryStatus());
        this.ShallowRegressionOnModule(new InventoryType());
        this.ShallowRegressionOnModule(new PartsCategory());
        this.ShallowRegressionOnModule(new RentalCategory());
        this.ShallowRegressionOnModule(new RetiredReason());
        this.ShallowRegressionOnModule(new SalesCategory());
        this.ShallowRegressionOnModule(new Unit());
        this.ShallowRegressionOnModule(new UnretiredReason());
        this.ShallowRegressionOnModule(new WarehouseCatalog());
        this.ShallowRegressionOnModule(new Crew());
        this.ShallowRegressionOnModule(new LaborRate());
        this.ShallowRegressionOnModule(new LaborPosition());
        this.ShallowRegressionOnModule(new LaborType());
        this.ShallowRegressionOnModule(new LaborCategory());
        this.ShallowRegressionOnModule(new CrewScheduleStatus());
        this.ShallowRegressionOnModule(new CrewStatus());
        this.ShallowRegressionOnModule(new MiscRate());
        this.ShallowRegressionOnModule(new MiscType());
        this.ShallowRegressionOnModule(new MiscCategory());
        this.ShallowRegressionOnModule(new OfficeLocation());
        this.ShallowRegressionOnModule(new OrderType());
        this.ShallowRegressionOnModule(new DiscountReason());
        this.ShallowRegressionOnModule(new MarketSegment());
        this.ShallowRegressionOnModule(new MarketType());
        this.ShallowRegressionOnModule(new OrderSetNo());
        this.ShallowRegressionOnModule(new OrderLocation());
        this.ShallowRegressionOnModule(new PaymentTerms());
        this.ShallowRegressionOnModule(new PaymentType());
        this.ShallowRegressionOnModule(new POApprovalStatus());
        this.ShallowRegressionOnModule(new POApproverRole());
        this.ShallowRegressionOnModule(new POClassification());
        this.ShallowRegressionOnModule(new POImportance());
        this.ShallowRegressionOnModule(new PORejectReason());
        this.ShallowRegressionOnModule(new POType());
        this.ShallowRegressionOnModule(new POApprover());
        this.ShallowRegressionOnModule(new VendorInvoiceApprover());
        this.ShallowRegressionOnModule(new FormDesign());
        this.ShallowRegressionOnModule(new PresentationLayer());
        this.ShallowRegressionOnModule(new ProjectAsBuild());
        this.ShallowRegressionOnModule(new ProjectCommissioning());
        this.ShallowRegressionOnModule(new ProjectDeposit());
        this.ShallowRegressionOnModule(new ProjectDrawings());
        this.ShallowRegressionOnModule(new ProjectDropShipItems());
        this.ShallowRegressionOnModule(new ProjectItemsOrdered());
    }
    //---------------------------------------------------------------------------------------
}

describe('ShallowRegressionSettingsTest', () => {
    try {
        new ShallowRegressionSettingsTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
