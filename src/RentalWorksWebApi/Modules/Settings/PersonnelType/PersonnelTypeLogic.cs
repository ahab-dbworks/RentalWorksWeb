using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PersonnelType
{
    [FwLogic(Id:"KvwHeLp6Ylnp")]
    public class PersonnelTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PersonnelTypeRecord personnelType = new PersonnelTypeRecord();
        public PersonnelTypeLogic()
        {
            dataRecords.Add(personnelType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"F4QmiDirqZbn", IsPrimaryKey:true)]
        public string PersonnelTypeId { get { return personnelType.PersonnelTypeId; } set { personnelType.PersonnelTypeId = value; } }

        [FwLogicProperty(Id:"F4QmiDirqZbn", IsRecordTitle:true)]
        public string PersonnelType { get { return personnelType.PersonnelType; } set { personnelType.PersonnelType = value; } }

        [FwLogicProperty(Id:"sQmacGcVYjM8")]
        public bool? Inactive { get { return personnelType.Inactive; } set { personnelType.Inactive = value; } }

        [FwLogicProperty(Id:"cb3NyGuFfTOm")]
        public string DateStamp { get { return personnelType.DateStamp; } set { personnelType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
