using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Unit
{
    [FwLogic(Id:"vgk2OGUGHsbeL")]
    public class UnitLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        UnitRecord unit = new UnitRecord();
        public UnitLogic()
        {
            dataRecords.Add(unit);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"oDTJc0DrHR5o3", IsPrimaryKey:true)]
        public string UnitId { get { return unit.UnitId; } set { unit.UnitId = value; } }

        [FwLogicProperty(Id:"oDTJc0DrHR5o3", IsRecordTitle:true)]
        public string Unit { get { return unit.Unit; } set { unit.Unit = value; } }

        [FwLogicProperty(Id:"4wODVpLGrAgt")]
        public string Description { get { return unit.Description; } set { unit.Description = value; } }

        [FwLogicProperty(Id:"tjwkTfZUELNn")]
        public string UnitType { get { return unit.UnitType; } set { unit.UnitType = value; } }

        [FwLogicProperty(Id:"Kqg0epu1sEJm")]
        public string PluralDescription { get { return unit.PluralDescription; } set { unit.PluralDescription = value; } }

        [FwLogicProperty(Id:"DXNcyjsf7KqL")]
        public bool? Inactive { get { return unit.Inactive; } set { unit.Inactive = value; } }

        [FwLogicProperty(Id:"lfDAfzcRi6vk")]
        public string DateStamp { get { return unit.DateStamp; } set { unit.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
