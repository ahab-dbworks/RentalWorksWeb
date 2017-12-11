using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Unit
{
    public class UnitLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        UnitRecord unit = new UnitRecord();
        public UnitLogic()
        {
            dataRecords.Add(unit);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UnitId { get { return unit.UnitId; } set { unit.UnitId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Unit { get { return unit.Unit; } set { unit.Unit = value; } }
        public string Description { get { return unit.Description; } set { unit.Description = value; } }
        public string UnitType { get { return unit.UnitType; } set { unit.UnitType = value; } }
        public string PluralDescription { get { return unit.PluralDescription; } set { unit.PluralDescription = value; } }
        public bool? Inactive { get { return unit.Inactive; } set { unit.Inactive = value; } }
        public string DateStamp { get { return unit.DateStamp; } set { unit.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
