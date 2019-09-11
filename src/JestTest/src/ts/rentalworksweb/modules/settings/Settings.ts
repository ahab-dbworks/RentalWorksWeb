import { SettingsModule } from "../../../shared/SettingsModule";

//---------------------------------------------------------------------------------------
export class AccountingSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'AccountingSettings';
        this.moduleId = '6EB6300F-1416-42DE-B776-3E418656021D';
        this.moduleCaption = 'Accounting Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GlAccount extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GlAccount';
        this.moduleId = 'F03CA227-99EE-42EF-B615-94540DCB21B3';
        this.moduleCaption = 'Chart of Accounts';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GlDistribution extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GlDistribution';
        this.moduleId = '7C249F59-B5E3-4DAE-933D-38D30858CF7C';
        this.moduleCaption = 'G/L Distribution';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Country extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Country';
        this.moduleId = 'D6E787E6-502B-4D36-B0A6-FA691E6D10CF';
        this.moduleCaption = 'Country';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class State extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'State';
        this.moduleId = 'B70B4B88-51EB-4635-971B-1F676243B810';
        this.moduleCaption = 'State/Province';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class BillingCycle extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'BillingCycle';
        this.moduleId = '5736D549-CEA7-4FCF-86DA-0BCD4C87FA04';
        this.moduleCaption = 'Billing Cycle';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Department extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Department';
        this.moduleId = 'A6CC5F50-F9DE-4158-B627-6FDC1044C22A';
        this.moduleCaption = 'Company Department';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ContactEvent extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ContactEvent';
        this.moduleId = '25ad258e-db9d-4e94-a500-0382e7a2024a';
        this.moduleCaption = 'Contact Event';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ContactTitle extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ContactTitle';
        this.moduleId = '1b9183b2-add9-416d-a5e1-59fe68104e4a';
        this.moduleCaption = 'Contact Title';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MailList extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MailList';
        this.moduleId = '255ceb68-fb87-4248-ab99-37c18a192300';
        this.moduleCaption = 'Mail List';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Currency extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Currency';
        this.moduleId = '672145d0-9b37-4f6f-a216-9ae1e7728168';
        this.moduleCaption = 'Currency';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CreditStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CreditStatus';
        this.moduleId = 'A28D0CC9-B922-4259-BA4A-A5DE474ADFA4';
        this.moduleCaption = 'Credit Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomerCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomerCategory';
        this.moduleId = '8FB6C746-AB6E-4CA5-9BD4-4E9AD88A3BC5';
        this.moduleCaption = 'Customer Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomerStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomerStatus';
        this.moduleId = 'B689A0AA-9FCC-450B-AF0F-AD85483531FA';
        this.moduleCaption = 'Customer Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomerType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomerType';
        this.moduleId = '314EDC6F-478A-40E2-B17E-349886A85EA0';
        this.moduleCaption = 'Customer Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DealClassification extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DealClassification';
        this.moduleId = 'D1035FCC-D92B-4A3A-B985-C7E02CBE3DFD';
        this.moduleCaption = 'Deal Classification';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DealType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DealType';
        this.moduleId = 'A021AE67-0F33-4C97-9149-4CD5560EE10A';
        this.moduleCaption = 'Deal Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DealStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DealStatus';
        this.moduleId = '543F8F83-20AB-4001-8283-1E73A9D795DF';
        this.moduleCaption = 'Deal Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProductionType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProductionType';
        this.moduleId = '993EBF0C-EDF0-47A2-8507-51492502088B';
        this.moduleCaption = 'Production Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ScheduleType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ScheduleType';
        this.moduleId = '8646d7bb-9676-4fdd-b9ea-db98045390f4';
        this.moduleCaption = 'Schedule Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DiscountTemplate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DiscountTemplate';
        this.moduleId = '258D920E-7024-4F68-BF1F-F07F3613829C';
        this.moduleCaption = 'Discount Template';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DocumentType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DocumentType';
        this.moduleId = '358fbe63-83a7-4ab4-973b-1a5520573674';
        this.moduleCaption = 'Document Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CoverLetter extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CoverLetter';
        this.moduleId = 'BE13DA09-E3AA-4520-A16C-F43F1A207EA5';
        this.moduleCaption = 'Cover Letter';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TermsConditions extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TermsConditions';
        this.moduleId = '5C09A4C3-4272-458A-80DA-A5DF6B098D02';
        this.moduleCaption = 'Terms & Conditions';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EventCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EventCategory';
        this.moduleId = '3912b3cc-b35f-434d-aeeb-c45fed537e29';
        this.moduleCaption = 'Event Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EventType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EventType';
        this.moduleId = 'FE501F99-95D4-444C-A7B6-EA20ACE88879';
        this.moduleCaption = 'Event Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PersonnelType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PersonnelType';
        this.moduleId = '46339c9c-c663-4041-aeb4-a7f85783996f';
        this.moduleCaption = 'Personnel Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PhotographyType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PhotographyType';
        this.moduleId = '66bff7f0-8bca-4d32-bd94-6b5f13623bec';
        this.moduleCaption = 'Photography Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Building extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Building';
        this.moduleId = '2D344845-7E77-40C9-BB9D-04A930D352EB';
        this.moduleCaption = 'Building';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityType';
        this.moduleId = '197BBE51-28A8-4D00-BD0C-098C0F88DD0E';
        this.moduleCaption = 'Facility Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityRate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityRate';
        this.moduleId = '5D49FC0B-F1BA-4FE4-889D-3C52B6202ACD';
        this.moduleCaption = 'Facility Rate';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityScheduleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityScheduleStatus';
        this.moduleId = 'A693C2F7-DF16-4492-9DE5-FC672375C44E';
        this.moduleCaption = 'Facility Schedule Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityStatus';
        this.moduleId = 'DB2C8448-9287-4885-952F-BE3D0E4BFEF1';
        this.moduleCaption = 'Facility Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityCategory';
        this.moduleId = '67A9BEC5-4865-409C-9327-B2B8714DDAA8';
        this.moduleCaption = 'Facility Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SpaceType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SpaceType';
        this.moduleId = 'EDF05CFB-9F6B-4771-88EB-6FD254CFE6C6';
        this.moduleCaption = 'Space Type / Activity';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FiscalYear extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FiscalYear';
        this.moduleId = '6F87E62B-F17A-48CB-B673-16D12B6DFFB9';
        this.moduleCaption = 'Fiscal Year';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorFuelType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorFuelType';
        this.moduleId = '8A331FE0-B92A-4DD2-8A59-29E4E6D6EA4F';
        this.moduleCaption = 'Generator Fuel Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorMake extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorMake';
        this.moduleId = 'D7C38A54-A198-4304-8EC2-CE8038D3BE9C';
        this.moduleCaption = 'Generator Make';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorRating extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorRating';
        this.moduleId = '140E6997-1BA9-49B7-AA79-CD5EF6444C72';
        this.moduleCaption = 'Generator Rating';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorWatts extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorWatts';
        this.moduleId = '503349D6-711A-4F45-8891-4B3203008441';
        this.moduleCaption = 'Generator Watts';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorType';
        this.moduleId = '95D9D422-DCEB-4150-BCC2-79573B87AC4D';
        this.moduleCaption = 'Generator Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Holiday extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Holiday';
        this.moduleId = 'CFFEFF09-A083-478E-913C-945184B5DE94';
        this.moduleCaption = 'Holiday';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class BlackoutStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'BlackoutStatus';
        this.moduleId = '43D7C88D-8D8C-424E-94D3-A2C537F0C76E';
        this.moduleCaption = 'Blackout Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class BarCodeRange extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'BarCodeRange';
        this.moduleId = '9A52C5B8-98AB-49A0-A392-69DB0873F943';
        this.moduleCaption = 'Bar Code Range';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryAdjustmentReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryAdjustmentReason';
        this.moduleId = 'B3156707-4D41-481C-A66E-8951E5233CDA';
        this.moduleCaption = 'Inventory Adjustment Reason';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Attribute extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Attribute';
        this.moduleId = '2777dd37-daca-47ff-aa44-29677b302745';
        this.moduleCaption = 'Inventory Attribute';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryCondition';
        this.moduleId = 'BF711CAC-1E69-4C92-B509-4CBFA29FAED3';
        this.moduleCaption = 'Inventory Condition';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryGroup extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryGroup';
        this.moduleId = '43AF2FBA-69FB-46A8-8E5A-2712486B66F3';
        this.moduleCaption = 'Inventory Group';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryRank extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryRank';
        this.moduleId = '963F5133-29CA-4675-9BE6-E5C47D38789A';
        this.moduleCaption = 'Inventory Rank';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryStatus';
        this.moduleId = 'E8E24D94-A07D-4388-9F2F-58FE028F24BB';
        this.moduleCaption = 'Inventory Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryType';
        this.moduleId = 'D62E0D20-AFF4-46A7-A767-FF32F6EC4617';
        this.moduleCaption = 'Inventory Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PartsCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PartsCategory';
        this.moduleId = '4750DFBD-6C60-41EF-83FE-49C8340D6062';
        this.moduleCaption = 'Parts Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RentalCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RentalCategory';
        this.moduleId = '91079439-A188-4637-B733-A7EF9A9DFC22';
        this.moduleCaption = 'Rental Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RetiredReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RetiredReason';
        this.moduleId = '1DE1DD87-47FD-4079-B7D8-B5DE61FCB280';
        this.moduleCaption = 'Retired Reason';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SalesCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SalesCategory';
        this.moduleId = '428619B5-ABDE-48C4-9B2F-CF6D2A3AC574';
        this.moduleCaption = 'Sales Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Unit extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Unit';
        this.moduleId = 'EE9F1081-BD9F-4004-A0CA-3813E2360642';
        this.moduleCaption = 'Unit of Measure';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class UnretiredReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'UnretiredReason';
        this.moduleId = 'C8E7F77B-52BC-435C-9971-331CF99284A0';
        this.moduleCaption = 'Unretired Reason';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WarehouseCatalog extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WarehouseCatalog';
        this.moduleId = '9045B118-A790-44FB-9867-3E8035EFEE69';
        this.moduleCaption = 'Warehouse Catalog';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Crew extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Crew';
        this.moduleId = 'FF4C0AF2-0984-48FD-A108-68D93CB8FFE6';
        this.moduleCaption = 'Crew';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborRate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborRate';
        this.moduleId = '650305EC-0A53-490B-A8FB-E1AF636DA89B';
        this.moduleCaption = 'Labor Rate';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborPosition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborPosition';
        this.moduleId = '6D3B3D4F-2DD8-436F-8942-8FF68B73F3B6';
        this.moduleCaption = 'Position';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborType';
        this.moduleId = '6757DFC2-360A-450A-B2E8-0B8232E87D6A';
        this.moduleCaption = 'Labor Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborCategory';
        this.moduleId = '2A5190B9-B0E8-4B93-897B-C91FC4807FA6';
        this.moduleCaption = 'Labor Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CrewScheduleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CrewScheduleStatus';
        this.moduleId = 'E4E11656-0783-4327-A374-161BCFDF0F24';
        this.moduleCaption = 'Crew Schedule Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CrewStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CrewStatus';
        this.moduleId = '73A6D9E3-E3BE-4B7A-BB3B-0AFE571C944E';
        this.moduleCaption = 'Crew Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MiscRate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MiscRate';
        this.moduleId = '15B5AA83-4C3A-4136-B74B-574BDC0141B2';
        this.moduleCaption = 'Misc Rate';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MiscType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MiscType';
        this.moduleId = 'EAFEE5C7-84BB-419E-905A-3AE86E18DFAB';
        this.moduleCaption = 'Misc Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MiscCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MiscCategory';
        this.moduleId = 'D5318A2F-ECB8-498A-9D9A-0846F4B9E4DF';
        this.moduleCaption = 'Misc Category';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OfficeLocation extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OfficeLocation';
        this.moduleId = '8A8EE5CC-458E-4E4B-BA09-9C514588D3BD';
        this.moduleCaption = 'Office Location';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrderType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrderType';
        this.moduleId = 'CF3E22CB-F836-4277-9589-998B0BEC3500';
        this.moduleCaption = 'Order Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DiscountReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DiscountReason';
        this.moduleId = 'CBBBFA51-DE2D-4A24-A50E-F7F4774016F6';
        this.moduleCaption = 'Discount Reason';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MarketSegment extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MarketSegment';
        this.moduleId = '53B627BE-6AC8-4C1F-BEF4-E8B0A5422E14';
        this.moduleCaption = 'Market Segment';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MarketType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MarketType';
        this.moduleId = '77D7FD11-EBD2-40A2-A40D-C82D32528A01';
        this.moduleCaption = 'Market Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrderSetNo extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrderSetNo';
        this.moduleId = '4960D9A7-D1E0-4558-B571-DF1CE1BB8245';
        this.moduleCaption = 'Order Set No.';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrderLocation extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrderLocation';
        this.moduleId = 'CF58D8C9-95EE-4617-97B9-CAFE200719CC';
        this.moduleCaption = 'Order Location';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PaymentTerms extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PaymentTerms';
        this.moduleId = '44FD799A-1572-4B34-9943-D94FFBEF89D4';
        this.moduleCaption = 'Payment Terms';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PaymentType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PaymentType';
        this.moduleId = 'E88C4957-3A3E-4258-8677-EB6FB61F9BA3';
        this.moduleCaption = 'Payment Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POApprovalStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POApprovalStatus';
        this.moduleId = '22EF1328-FBB1-44D0-A965-4E96675B96CD';
        this.moduleCaption = 'PO Approval Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POApproverRole extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POApproverRole';
        this.moduleId = '992314B6-A24F-468C-A8B6-5EAC8F14BE16';
        this.moduleCaption = 'PO Approver Role';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POClassification extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POClassification';
        this.moduleId = '58ef51c5-a97b-43c6-9298-08b064a84a48';
        this.moduleCaption = 'PO Classification';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POImportance extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POImportance';
        this.moduleId = '82BF3B3E-0EF8-4A6E-8577-33F23EA9C4FB';
        this.moduleCaption = 'PO Importance';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PORejectReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PORejectReason';
        this.moduleId = '2C6910A8-51BC-421E-898F-C23938B624B4';
        this.moduleCaption = 'PO Reject Reason';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POType';
        this.moduleId = 'BB8D68B3-012A-4B05-BE7F-844EB5C96896';
        this.moduleCaption = 'PO Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POApprover extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POApprover';
        this.moduleId = '237B99DC-252D-4197-AB4A-01E795076447';
        this.moduleCaption = 'PO Approver';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorInvoiceApprover extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorInvoiceApprover';
        this.moduleId = '4E34DB8F-84C0-4810-B49E-AE6640DD8E4B';
        this.moduleCaption = 'Vendor Invoice Approver';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FormDesign extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FormDesign';
        this.moduleId = '4DFEC75D-C33A-4358-9EF1-4D1F5F9C5D73';
        this.moduleCaption = 'Form Design';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PresentationLayer extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PresentationLayer';
        this.moduleId = 'BBEF0AFD-B46A-46B0-8046-113834736060';
        this.moduleCaption = 'Presentation Layer';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectAsBuild extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectAsBuild';
        this.moduleId = 'A3BFF1F7-0951-4F3A-A6DE-1A62BEDF45E6';
        this.moduleCaption = 'As Build';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectCommissioning extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectCommissioning';
        this.moduleId = '0EFE9BBA-0685-4046-A7D6-EC3D34AD01AA';
        this.moduleCaption = 'Commissioning';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectDeposit extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectDeposit';
        this.moduleId = '24E6F284-7457-4E75-B77D-25B3A6BE6A4D';
        this.moduleCaption = 'Deposit';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectDrawings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectDrawings';
        this.moduleId = '7486859D-243F-4817-8177-6DCB81392C36';
        this.moduleCaption = 'Drawings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectDropShipItems extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectDropShipItems';
        this.moduleId = '20CD34E6-7E35-4EAF-B4D3-587870412C85';
        this.moduleCaption = 'Drop Ship Items';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectItemsOrdered extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectItemsOrdered';
        this.moduleId = '25507FAD-E140-4A19-8FED-2C381DA653D9';
        this.moduleCaption = 'Items Ordered';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PropsCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PropsCondition';
        this.moduleId = '86C769E8-F8E6-4C59-BC0B-8F2D563C698F';
        this.moduleCaption = 'Props Condition';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Region extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Region';
        this.moduleId = 'A50C7F59-AF91-44D5-8253-5C4A4D5DFB8B';
        this.moduleCaption = 'Region';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RepairItemStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RepairItemStatus';
        this.moduleId = 'D952672A-DCF6-47C8-9B99-47561C79B3F8';
        this.moduleCaption = 'Repair Item Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SetCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SetCondition';
        this.moduleId = '0FFC8940-C060-49E4-BC24-688E25250C5F';
        this.moduleCaption = 'Set Condition';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SetSurface extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SetSurface';
        this.moduleId = 'EC55E743-0CB1-4A74-9D10-6C4C6045AAAB';
        this.moduleCaption = 'Set Surface';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SetOpening extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SetOpening';
        this.moduleId = '15E52CA3-475D-4BDA-B940-525E5EEAF8CD';
        this.moduleCaption = 'Set Opening';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WallDescription extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WallDescription';
        this.moduleId = 'F34F1A9B-53C6-447C-B52B-7FF5BAE38AB5';
        this.moduleCaption = 'Wall Description';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WallType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WallType';
        this.moduleId = '4C9D2D20-D129-461D-9589-ABC896DD9BC6';
        this.moduleCaption = 'Wall Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ShipVia extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ShipVia';
        this.moduleId = 'F9E01296-D240-4E16-B267-898787B29509';
        this.moduleCaption = 'Ship Via';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Source extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Source';
        this.moduleId = '6D6165D1-51F2-4616-A67C-DCC803B549AF';
        this.moduleCaption = 'Source';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class AvailabilitySettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'AvailabilitySettings';
        this.moduleId = 'E1C62A69-05B0-4657-AAF0-703F8BDEBC5C';
        this.moduleCaption = 'Availability Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DefaultSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DefaultSettings';
        this.moduleId = '3F551271-C157-44F6-B06A-8F835F7A2084';
        this.moduleCaption = 'Default Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EmailSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EmailSettings';
        this.moduleId = '8C9613E0-E7E5-4242-9DF6-4F57F59CE2B9';
        this.moduleCaption = 'Email Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventorySettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventorySettings';
        this.moduleId = '5C7D5BFA-3EA3-42C5-B90A-27A9EA5EA9FC';
        this.moduleCaption = 'Inventory Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LogoSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LogoSettings';
        this.moduleId = 'B3ADDF49-64EB-4740-AB41-4327E6E56242';
        this.moduleCaption = 'Logo Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SystemSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SystemSettings';
        this.moduleId = '6EFC3A8C-E83E-4FE3-BAC8-8E04EBD7F204';
        this.moduleCaption = 'System Settings';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TaxOption extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TaxOption';
        this.moduleId = '5895CA39-5EF8-405B-9E97-2FEB83939EE5';
        this.moduleCaption = 'Tax Option';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Template extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Template';
        this.moduleId = 'BDDB1439-F128-4AB7-9657-B1CDFFA12721';
        this.moduleCaption = 'Template';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class UserStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'UserStatus';
        this.moduleId = 'E19916C6-A844-4BD1-A338-FAB0F278122C';
        this.moduleCaption = 'User Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Sound extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Sound';
        this.moduleId = '29C327DD-7734-4039-9CE2-B25D7CD6F9CB';
        this.moduleCaption = 'Sounds';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LicenseClass extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LicenseClass';
        this.moduleId = '422F777F-B57F-43DF-8485-F12F3F7BF662';
        this.moduleCaption = 'License Class';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleColor extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleColor';
        this.moduleId = 'F7A34B70-509A-422F-BFD1-5F30BE2C8186';
        this.moduleCaption = 'Vehicle Color';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleFuelType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleFuelType';
        this.moduleId = 'D9140FB3-084D-4615-8E7A-95731670E682';
        this.moduleCaption = 'Vehicle Fuel Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleMake extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleMake';
        this.moduleId = '299DECA3-B427-49ED-B6AC-2E11F6AA1C4D';
        this.moduleCaption = 'Vehicle Make';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleScheduleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleScheduleStatus';
        this.moduleId = 'A001473B-1FB4-4E85-8093-37A92057CD93';
        this.moduleCaption = 'Vehicle Schedule Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleStatus';
        this.moduleId = 'FB12061D-E6AF-4C09-95A0-8647930C289A';
        this.moduleCaption = 'Vehicle Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleType';
        this.moduleId = '60187072-8990-40BA-8D80-43B451E5BC8B';
        this.moduleCaption = 'Vehicle Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrganizationType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrganizationType';
        this.moduleId = 'fe3a764c-ab55-4ce5-8d7f-bfc86f174c11';
        this.moduleCaption = 'Organization Type';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorCatalog extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorCatalog';
        this.moduleId = 'BDA5E2DC-0FD2-4227-B80F-8414F3F912B8';
        this.moduleCaption = 'Vendor Catalog';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorClass extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorClass';
        this.moduleId = '8B2C9EE3-AE87-483F-A651-8BA633E6C439';
        this.moduleCaption = 'Vendor Class';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SapVendorInvoiceStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SapVendorInvoiceStatus';
        this.moduleId = '1C8E14A3-73A8-4BB6-9B33-65D827B3ED0C';
        this.moduleCaption = 'SAP Vendor Invoice Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeCare extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeCare';
        this.moduleId = 'BE6E4F7C-5D81-4437-A343-8F4933DD6545';
        this.moduleCaption = 'Wardrobe Care';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeColor extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeColor';
        this.moduleId = '32238B26-3635-4637-AFE0-0D5B12CAAEE4';
        this.moduleCaption = 'Wardrobe Color';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeCondition';
        this.moduleId = '4EEBE71C-139A-4D09-B589-59DA576C83FD';
        this.moduleCaption = 'Wardrobe Condition';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeGender extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeGender';
        this.moduleId = '28574D17-D2FF-41A0-8117-5F252013E7B1';
        this.moduleCaption = 'Wardrobe Gender';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeLabel extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeLabel';
        this.moduleId = '9C1B5157-C983-44EE-817F-171B4448401A';
        this.moduleCaption = 'Wardrobe Label';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeMaterial extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeMaterial';
        this.moduleId = '25895901-C700-4618-9ADA-00A7CB4B83B9';
        this.moduleCaption = 'Wardrobe Material';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobePattern extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobePattern';
        this.moduleId = '2BE7072A-5588-4205-8DCD-0FFE6F0C48F7';
        this.moduleCaption = 'Wardrobe Pattern';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobePeriod extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobePeriod';
        this.moduleId = 'BF51623D-ABA6-471A-BC00-4729067C64CF';
        this.moduleCaption = 'Wardrobe Period';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeSource extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeSource';
        this.moduleId = '6709D1A1-3319-435C-BF0E-15D2602575B0';
        this.moduleCaption = 'Wardrobe Source';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Warehouse extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Warehouse';
        this.moduleId = '931D3E75-68CB-4280-B12F-9A955444AA0C';
        this.moduleCaption = 'Warehouse';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Widget extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Widget';
        this.moduleId = '0CAF7264-D1FB-46EC-96B9-68D242985812';
        this.moduleCaption = 'Widget';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WorkWeek extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WorkWeek';
        this.moduleId = 'AF91AE34-ADED-4A5A-BD03-113ED817F575';
        this.moduleCaption = 'Work Week';
    }
    //---------------------------------------------------------------------------------------
}

