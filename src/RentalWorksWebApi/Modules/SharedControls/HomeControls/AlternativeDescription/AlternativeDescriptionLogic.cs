using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.AlternativeDescription
{
    [FwLogic(Id: "2HTGoYck1Gql")]
    public class AlternativeDescriptionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AlternativeDescriptionRecord alternativeDescription = new AlternativeDescriptionRecord();
        AlternativeDescriptionLoader alternativeDescriptionLoader = new AlternativeDescriptionLoader();
        public AlternativeDescriptionLogic()
        {
            dataRecords.Add(alternativeDescription);
            dataLoader = alternativeDescriptionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2HxeyNYhWaoQ", IsPrimaryKey: true)]
        public int? AlternativeDescriptionId { get { return alternativeDescription.AlternativeDescriptionId; } set { alternativeDescription.AlternativeDescriptionId = value; } }
        [FwLogicProperty(Id: "2IEJ55FTCNxDL")]
        public bool? InternalChar { get { return alternativeDescription.InternalChar; } set { alternativeDescription.InternalChar = value; } }
        [FwLogicProperty(Id: "2lNj3vno7XkfJ")]
        public string InventoryId { get { return alternativeDescription.InventoryId; } set { alternativeDescription.InventoryId = value; } }
        [FwLogicProperty(Id: "2OK4zjZkdlHSD", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "2qTt0mJx05Jq", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "2RceW8kvhz19")]
        public string AKA { get { return alternativeDescription.AKA; } set { alternativeDescription.AKA = value; } }
        [FwLogicProperty(Id: "2rKsWzvpUgor")]
        public bool? AllowedOnInvoice { get { return alternativeDescription.AllowedOnInvoice; } set { alternativeDescription.AllowedOnInvoice = value; } }
        [FwLogicProperty(Id: "2TOIuXdc7sOe", IsReadOnly: true)]
        public bool? IsPrimary { get; set; }
        [FwLogicProperty(Id: "2tQKNimp6zuq5")]
        public bool? Inactive { get { return alternativeDescription.Inactive; } set { alternativeDescription.Inactive = value; } }
    }
}
