using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Administrator.Plugin
{
    public class PluginLogic: AppBusinessLogic
    {
        PluginRecord pluginRecord = new PluginRecord();
        PluginLoader pluginLoader = new PluginLoader();
        //------------------------------------------------------------------------------------ 
        public PluginLogic()
        {
            this.dataRecords.Add(pluginRecord);
            this.dataLoader = pluginLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "wTk9p5dqbDwq", IsPrimaryKey: true)]
        public int PluginId { get { return pluginRecord.PluginId; } set { pluginRecord.PluginId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "yL9Q4Awc0ezN")]
        public string Description { get { return pluginRecord.Description; } set { pluginRecord.Description = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1eHeWdjznus0")]
        public string Settings { get { return pluginRecord.Settings; } set { pluginRecord.Settings = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "3FCSNPuRs2uD")]
        public bool Inactive { get { return pluginRecord.Inactive; } set { pluginRecord.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
