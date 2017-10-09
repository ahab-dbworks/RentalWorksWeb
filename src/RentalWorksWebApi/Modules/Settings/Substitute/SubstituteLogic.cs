using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.Substitute
{
    public class SubstituteLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SubstituteRecord substitute = new SubstituteRecord();
        SubstituteLoader substituteLoader = new SubstituteLoader();
        public SubstituteLogic()
        {
            dataRecords.Add(substitute);
            dataLoader = substituteLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemId { get { return substitute.ItemId; } set { substitute.ItemId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SubstituteId { get { return substitute.SubstituteId; } set { substitute.SubstituteId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ManufacturerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Manufacturer { get; set; }
        public string DateStamp { get { return substitute.DateStamp; } set { substitute.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}