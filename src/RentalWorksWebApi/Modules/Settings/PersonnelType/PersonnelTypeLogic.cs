﻿using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PersonnelType
{
    public class PersonnelTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PersonnelTypeRecord personnelType = new PersonnelTypeRecord();
        public PersonnelTypeLogic()
        {
            dataRecords.Add(personnelType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PersonnelTypeId { get { return personnelType.PersonnelTypeId; } set { personnelType.PersonnelTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PersonnelType { get { return personnelType.PersonnelType; } set { personnelType.PersonnelType = value; } }
        public bool? Inactive { get { return personnelType.Inactive; } set { personnelType.Inactive = value; } }
        public string DateStamp { get { return personnelType.DateStamp; } set { personnelType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
