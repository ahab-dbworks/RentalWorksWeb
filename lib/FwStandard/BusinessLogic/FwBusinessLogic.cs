//using AutoMapper;
using FwStandard.ConfigSection;
using FwStandard.Models;
using FwStandard.SqlServer;
//using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
//using System;
using System.Collections.Generic;
//using System.Reflection;
//using System.Text;
using FwStandard.DataLayer;

namespace FwStandard.BusinessLogic
{
    public class FwBusinessLogic
    {
        [JsonIgnore]
        public List<FwDataRecord> dataRecords = new List<FwDataRecord>();
        //------------------------------------------------------------------------------------
        public void SetDbConfig(DatabaseConfig dbConfig)
        {
            foreach (FwDataRecord dal in dataRecords)
            {
                dal.SetDbConfig(dbConfig);
            }
        }
        //------------------------------------------------------------------------------------
        public FwJsonDataTable Browse(BrowseRequestDto request)
        {
            //jh 06/28/2017 this is the default Browser.  works fine for simple BusinessLogic objects where there is an identical class properties match between the Business Logic layer and the Data layer
            FwJsonDataTable browse = null;
            if (dataRecords.Count > 0)
            {
                browse = dataRecords[0].Browse(request);
            }
            return browse;

        }
        //------------------------------------------------------------------------------------
        public virtual IEnumerable<T> Select<T>(BrowseRequestDto request)
        {
            //jh 06/28/2017 this is the default Selector.  works fine for simple BusinessLogic objects where there is an identical class properties match between the Business Logic layer and the Data layer
            IEnumerable<T> records = null;
            if (dataRecords.Count > 0)
            {
                records = dataRecords[0].Select<T>(request);  
            }
            return records;
        }
        //------------------------------------------------------------------------------------
        //public virtual void Load<T>(List<string> primaryKeyValues) 
        public virtual void Load<T>(string[] primaryKeyValues) 
        {
            foreach (FwDataRecord dal in dataRecords)
            {
                dal.Load<T>(primaryKeyValues);
            }
        }
        //------------------------------------------------------------------------------------
        public virtual int Save()
        {
            int rowsAffected = 0;
            foreach (FwDataRecord dal in dataRecords)
            {
                rowsAffected = rowsAffected + dal.Save();
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual bool Delete()
        {
            bool success = true;
            foreach (FwDataRecord dal in dataRecords)
            {
                success = ((success) && dal.Delete());
            }
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}
