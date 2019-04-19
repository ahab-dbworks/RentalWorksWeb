using System;
using System.Collections.Generic;

namespace QuartzScheduler.Utilities
{
    [Serializable]
    public class FwFields : IDictionary<string, object>
    {
        //------------------------------------------------------------------------------------------------
        private Dictionary<string, object> fFields = new Dictionary<string,object>();
        //------------------------------------------------------------------------------------------------
        public FwFields()
        {
        }
        //------------------------------------------------------------------------------------------
        public Dictionary<string, object> Items
        {
            get
            {
                return fFields;
            }
            set
            {
                fFields = value;
            }
        }
        //------------------------------------------------------------------------------------------------
        public void SetField(string fieldName, object fieldValue)
        {
            fFields[fieldName] = fieldValue;
        }
        //---------------------------------------------------------------------------------------------------
        public object GetField(string fieldName)
        {           
            if (!fFields.ContainsKey(fieldName))
            {
                throw new Exception("Invalid field name '" + fieldName + "'.");    
            }
            return fFields[fieldName];
        }
        //---------------------------------------------------------------------------------------------------
        public Dictionary<string, FwDatabaseField> GetFwDatabaseFields()
        {
            Dictionary<string, FwDatabaseField> dbfields = new Dictionary<string, FwDatabaseField>();
            foreach (KeyValuePair<string,object> item in this.Items)
            {
                FwDatabaseField dbfield = new FwDatabaseField();
                dbfield.FieldValue = item.Value;
                dbfields[item.Key] = dbfield;
            }
            return dbfields;
        }
        //---------------------------------------------------------------------------------------------------

        #region IDictionary<string,object> Members

        public void Add(string key, object value)
        {
            fFields[key] = value;
        }

        public bool ContainsKey(string key)
        {
            return fFields.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return fFields.Keys; }
        }

        public bool Remove(string key)
        {
            return fFields.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return fFields.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return fFields.Values; }
        }

        public object this[string key]
        {
            get
            {
                return fFields[key];
            }
            set
            {
                fFields[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> Members

        public void Add(KeyValuePair<string, object> item)
        {
            fFields.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            fFields.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return fFields.ContainsKey(item.Key) && (fFields[item.Key] == item.Value);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                fFields[array[i].Key] = array[i].Value;
            }
        }

        public int Count
        {
            get { return fFields.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return fFields.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return fFields.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return fFields.GetEnumerator();
        }

        #endregion
    }
}
