using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.ContractHistory
{
    [FwLogic(Id: "FYeRKYFEWE9Nd")]
    public class ContractHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractHistoryLoader contractHistoryLoader = new ContractHistoryLoader();
        public ContractHistoryLogic()
        {
            dataLoader = contractHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "fYv0w8ntBYkaO", IsReadOnly: true)]
        public int? OrderTranId { get; set; }
        [FwLogicProperty(Id: "FyvceGvPCTffz", IsReadOnly: true)]
        public bool? InternalChar { get; set; }
        [FwLogicProperty(Id: "FZGPyOvmTMK1o", IsReadOnly: true)]
        public string ItemId { get; set; }
        [FwLogicProperty(Id: "FzSnhymPOGZtB", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "G0kAyeiUUkCOH", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "G0VFVhxXdISez", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "g1epQ3jsjPaXq", IsReadOnly: true)]
        public string BarCode { get; set; }
        [FwLogicProperty(Id: "g1PgqpWiPAoyO", IsReadOnly: true)]
        public string CategoryId { get; set; }
        [FwLogicProperty(Id: "g2osziD9Wabcz", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "G2qi4rbmQMbOQ", IsReadOnly: true)]
        public string AvailableFrom { get; set; }
        [FwLogicProperty(Id: "G2SVxR2gMfZV6", IsReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwLogicProperty(Id: "G3hn0egSpcK4q", IsReadOnly: true)]
        public bool? IsMetered { get; set; }
        [FwLogicProperty(Id: "g3on7DNUmxFEq", IsReadOnly: true)]
        public bool? IsTrackedByWeight { get; set; }
        [FwLogicProperty(Id: "G3qlOylqM1ART", IsReadOnly: true)]
        public string IsTrackedByLength { get; set; }
        [FwLogicProperty(Id: "G3RtRjGBSvgk0", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "G3ZGaBbG0nQAv", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "G4JGaG5AIzT5e", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "G4YTjmawu66jo", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "g4zcTZ6MWYrQQ", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "g5tt0MLKNcP1k", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "g6k8GAPIzIpl5", IsReadOnly: true)]
        public string Department { get; set; }
        [FwLogicProperty(Id: "g6Sej76d53DDd", IsReadOnly: true)]
        public string OrderType { get; set; }
        [FwLogicProperty(Id: "g6tpIW0ineGjQ", IsReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwLogicProperty(Id: "g75JpNeybOTEQ", IsReadOnly: true)]
        public string ParentId { get; set; }
        [FwLogicProperty(Id: "G7MjNS6zt4h3l", IsReadOnly: true)]
        public decimal? QuantityOrdered { get; set; }
        [FwLogicProperty(Id: "g7RsqJC9o9COT", IsReadOnly: true)]
        public decimal? SubQuantity { get; set; }
        [FwLogicProperty(Id: "G8ImNyfkpThGI", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "G8vK0ZWeqr3rK", IsReadOnly: true)]
        public string ItemClass { get; set; }
        [FwLogicProperty(Id: "G9Db7dULTtB8a", IsReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwLogicProperty(Id: "G9QUCt5VHiTYx", IsReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        [FwLogicProperty(Id: "gaIP1dcnblqWs", IsReadOnly: true)]
        public string OutContractId { get; set; }
        [FwLogicProperty(Id: "GajIGsNCXCBWA", IsReadOnly: true)]
        public string OutContractNumber { get; set; }
        [FwLogicProperty(Id: "GaqcdS6Oihcnf", IsReadOnly: true)]
        public bool? IsOutSuspend { get; set; }
        [FwLogicProperty(Id: "gaUAaclLDWqcV", IsReadOnly: true)]
        public string OutDateTime { get; set; }
        [FwLogicProperty(Id: "gaWUElqRIuvtE", IsReadOnly: true)]
        public string OutUserId { get; set; }
        [FwLogicProperty(Id: "9UON7DpPdLKq", IsReadOnly: true)]
        public string OutUserName { get; set; }
        [FwLogicProperty(Id: "gBcF3XlLW21KW", IsReadOnly: true)]
        public string OutWarehouseId { get; set; }
        [FwLogicProperty(Id: "gcb2GH9Rz1sRY", IsReadOnly: true)]
        public string OutWarehouseCode { get; set; }
        [FwLogicProperty(Id: "gCcmMI7d8n556", IsReadOnly: true)]
        public string OutWarehouse { get; set; }
        [FwLogicProperty(Id: "GcnRMJSOIOUsn", IsReadOnly: true)]
        public string InContractId { get; set; }
        [FwLogicProperty(Id: "GdGra1scn0tFn", IsReadOnly: true)]
        public string InContractNumber { get; set; }
        [FwLogicProperty(Id: "gdSNpqOyvUBTv", IsReadOnly: true)]
        public bool? IsInSuspend { get; set; }
        [FwLogicProperty(Id: "gecrukLwCPBP8", IsReadOnly: true)]
        public string InDateTime { get; set; }
        [FwLogicProperty(Id: "geJsvH0f0sRUT", IsReadOnly: true)]
        public string InUserId { get; set; }
        [FwLogicProperty(Id: "GF9TpkJ9Mn91v", IsReadOnly: true)]
        public string InUserName { get; set; }
        [FwLogicProperty(Id: "GFftEdEMSIn1V", IsReadOnly: true)]
        public string InWarehouseId { get; set; }
        [FwLogicProperty(Id: "gFmGuMvQqbBRU", IsReadOnly: true)]
        public string InWarehouseCode { get; set; }
        [FwLogicProperty(Id: "GgePIKjZy9Ud7", IsReadOnly: true)]
        public string InWarehouse { get; set; }
        [FwLogicProperty(Id: "GgIVpf6eFqq5I", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "ggJ4bUNjsmsPp", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "GGXjNd8N87GgY", IsReadOnly: true)]
        public bool? ItemStatus { get; set; }
        [FwLogicProperty(Id: "Gh4wjt3QU6aFN", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        [FwLogicProperty(Id: "Gh7nZTqZv0B01", IsReadOnly: true)]
        public bool? IsToRepair { get; set; }
        [FwLogicProperty(Id: "ghbtE4tPw6d0f", IsReadOnly: true)]
        public string ContainerItemId { get; set; }
        [FwLogicProperty(Id: "ghoutM5uZonEB", IsReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwLogicProperty(Id: "Gi7HVA0BTGMfj", IsReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
        [FwLogicProperty(Id: "gIIYdBCsM0nnT", IsReadOnly: true)]
        public int? AssetHoursOut { get; set; }
        [FwLogicProperty(Id: "gIWFMx0aOcSn1", IsReadOnly: true)]
        public int? AssetHoursIn { get; set; }
        [FwLogicProperty(Id: "GJb0B5jUB8vck", IsReadOnly: true)]
        public int? StrikesOut { get; set; }
        [FwLogicProperty(Id: "GJEqgkiGJlVXA", IsReadOnly: true)]
        public int? StrikesIn { get; set; }
        [FwLogicProperty(Id: "GjKIN7McMY6tM", IsReadOnly: true)]
        public int? CandlesOut { get; set; }
        [FwLogicProperty(Id: "gkxuSDZrvtuoR", IsReadOnly: true)]
        public int? CandlesIn { get; set; }
        [FwLogicProperty(Id: "gl9ukDHtvWucp", IsReadOnly: true)]
        public decimal? MeterOut { get; set; }
        [FwLogicProperty(Id: "glaOPj1VVMtSF", IsReadOnly: true)]
        public decimal? MeterIn { get; set; }
        [FwLogicProperty(Id: "gll9aS67VGpo9", IsReadOnly: true)]
        public int? LampHours1Out { get; set; }
        [FwLogicProperty(Id: "Gllem6AKjOLZf", IsReadOnly: true)]
        public int? LampHours1In { get; set; }
        [FwLogicProperty(Id: "glrKoOQJt4LGe", IsReadOnly: true)]
        public int? LampHours2Out { get; set; }
        [FwLogicProperty(Id: "GlT5D3jSm8n8x", IsReadOnly: true)]
        public int? LampHours2In { get; set; }
        [FwLogicProperty(Id: "glYG2oI19QRdR", IsReadOnly: true)]
        public int? LampHours3Out { get; set; }
        [FwLogicProperty(Id: "gmtMcKKfUGDw3", IsReadOnly: true)]
        public int? LampHours3In { get; set; }
        [FwLogicProperty(Id: "gnTZ1fXqyNTWL", IsReadOnly: true)]
        public int? LampHours4Out { get; set; }
        [FwLogicProperty(Id: "go3CwasoaJcD4", IsReadOnly: true)]
        public int? LampHours4In { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
