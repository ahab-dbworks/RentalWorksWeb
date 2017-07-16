using FwStandard.ConfigSection;
using FwStandard.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task LoadAsync(string[] primaryKeyValues)
        {
            FwCustomData customData = new FwCustomData(_moduleName, primaryKeyValues);
            customData.SetDbConfig(_dbConfig);
            await customData.LoadAsync();
            this.Clear();
            foreach (FwCustomValue value in customData.customValues) {
                this.Add(new FwBusinessLogicCustomValue(value.FieldName, value.FieldValue));
            }
        }
        //------------------------------------------------------------------------------------
        public async Task SaveAsync(string[] primaryKeyValues)
        {
            FwCustomData customData = new FwCustomData(_moduleName, primaryKeyValues);
            customData.SetDbConfig(_dbConfig);
            foreach (FwBusinessLogicCustomValue value in this)
            {
                customData.customValues.Add(new FwCustomValue(value.Name, value.Value));
            }
            await customData.SaveAsync();
        }
        //------------------------------------------------------------------------------------
    }

}
