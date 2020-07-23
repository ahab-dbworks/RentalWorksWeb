using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.BusinessLogic
{

    public class FwForeignKeyField
    {
        public string IdFieldName = "";
        public string ForeignIdFieldName = "";
        public FwForeignKeyField(string idFieldName, string foreignIdFieldName)
        {
            this.IdFieldName = idFieldName;
            this.ForeignIdFieldName = foreignIdFieldName;
        }
    }

    public class FwForeignKeyDisplayField
    {
        public string DisplayFieldName = "";
        public string ForeignDisplayFieldName = "";
        public FwForeignKeyDisplayField(string displayFieldName, string foreignDisplayFieldName)
        {
            this.DisplayFieldName = displayFieldName;
            this.ForeignDisplayFieldName = foreignDisplayFieldName;
        }
    }

    public class FwForeignKey
    {
        public Type ForeignObjectType = null;
        public List<FwForeignKeyField> KeyFields = new List<FwForeignKeyField>();
        public List<FwForeignKeyDisplayField> KeyDisplayFields = new List<FwForeignKeyDisplayField>();

        public FwForeignKey(Type foreignObjectType)
        {
            string idFieldName = foreignObjectType.Name.Replace("Logic", "") + "Id";
            string displayFieldName = idFieldName.Substring(0, idFieldName.Length - 2);
            this.ForeignObjectType = foreignObjectType;
            this.KeyFields.Add(new FwForeignKeyField(idFieldName, idFieldName));
            this.KeyDisplayFields.Add(new FwForeignKeyDisplayField(displayFieldName, displayFieldName));
        }
        public FwForeignKey(Type foreignObjectType, string idFieldName)
        {
            if (idFieldName.EndsWith("Id"))
            {
                string displayFieldName = idFieldName.Substring(0, idFieldName.Length - 2);
                this.ForeignObjectType = foreignObjectType;
                this.KeyFields.Add(new FwForeignKeyField(idFieldName, idFieldName));
                this.KeyDisplayFields.Add(new FwForeignKeyDisplayField(displayFieldName, displayFieldName));
            }
            else
            {
                throw new Exception("Cannot determine displayFieldName for Foreign Key defined for " + idFieldName);
            }
        }
        public FwForeignKey(Type foreignObjectType, string idFieldName, string displayFieldName)
        {
            this.ForeignObjectType = foreignObjectType;
            this.KeyFields.Add(new FwForeignKeyField(idFieldName, idFieldName));
            this.KeyDisplayFields.Add(new FwForeignKeyDisplayField(displayFieldName, displayFieldName));
        }
        public FwForeignKey(Type foreignObjectType, string idFieldName, string foreignIdFieldName, string displayFieldName, string foreignDisplayFieldName)
        {
            this.ForeignObjectType = foreignObjectType;
            this.KeyFields.Add(new FwForeignKeyField(idFieldName, foreignIdFieldName));
            this.KeyDisplayFields.Add(new FwForeignKeyDisplayField(displayFieldName, foreignDisplayFieldName));
        }
    }
}
