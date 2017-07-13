using FwStandard.ConfigSection;
using FwStandard.DataLayer;
using System.Collections.Generic;

namespace FwStandard.BusinessLogic
{
    public class FwBusinessLogicCustomValues : List<FwBusinessLogicCustomValue>
    {
        protected DatabaseConfig _dbConfig { get; set; }
        //------------------------------------------------------------------------------------
        protected string _moduleName { get; set; }
        //------------------------------------------------------------------------------------
        public FwBusinessLogicCustomValues(string moduleName)
        {
            this._moduleName = moduleName;

        }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public void Load(string[] primaryKeyValues)
        {
            FwCustomData customData = new FwCustomData(_moduleName, primaryKeyValues);
            customData.SetDbConfig(_dbConfig);
            customData.Load();
            this.Clear();
            foreach (FwCustomValue value in customData.customValues) {
                this.Add(new FwBusinessLogicCustomValue(value.FieldName, value.FieldValue));
            }
        }
        //------------------------------------------------------------------------------------
        public void Save(string[] primaryKeyValues)
        {
            FwCustomData customData = new FwCustomData(_moduleName, primaryKeyValues);
            customData.SetDbConfig(_dbConfig);
            foreach (FwBusinessLogicCustomValue value in this)
            {
                customData.customValues.Add(new FwCustomValue(value.Name, value.Value));
            }
            customData.Save();
        }
        //------------------------------------------------------------------------------------
    }

}
