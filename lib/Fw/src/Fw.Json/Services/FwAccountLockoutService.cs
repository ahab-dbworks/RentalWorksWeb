using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using Fw.Json.ValueTypes;

namespace Fw.Json.Services
{
    public class FwAccountLockoutService : IDictionary<string, FwAccountLockoutService.User>
    {
        //---------------------------------------------------------------------------------------------
        private Dictionary<string, FwAccountLockoutService.User> users = new Dictionary<string,User>();
        private FwControl controlrec;
        public bool LockAccount { get { return FwSession.Current.SQLCache.GetTable("webcontrol").GetRow("webcontrolid", "1").GetColumn("lockaccount").Value.Equals("T"); } } 
        public static FwAccountLockoutService Current{ get {return (FwAccountLockoutService)HttpContext.Current.Application["AccountLockoutService"];} }
        //---------------------------------------------------------------------------------------------        
        public class User
        {
            //---------------------------------------------------------------------------------------------
            private string webUsersId = string.Empty;
            private int failedLoginAttempts = 0;
            private FwDateTime lastFailedLoginAttempt = null;
            public bool LockAccount 
            { 
                get
                {
                    bool result;
                    result = false;
                    if (!string.IsNullOrEmpty(webUsersId))
                    {
                        using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.AppConnection))
                        {
                            qry.Add("select lockaccount");
                            qry.Add("from webusers with (nolock)");
                            qry.Add("where webusersid = @webusersid");
                            result = qry.GetField("lockaccount").ToBoolean();
                        }
                    }
                    return result;
                } 
            }            
            
            //---------------------------------------------------------------------------------------------
            public bool IsLockedOut
            {
                get
                {
                    bool isLockedOut = false;
                    if (this.LockAccount)
                    {
                        isLockedOut = true;
                    }
                    else if ((FwAccountLockoutService.Current.LockAccount) && (this.failedLoginAttempts > FwConfiguration.AccountLockoutAttempts))
                    {
                        if (this.lastFailedLoginAttempt != null)
                        {
                            if (LockoutExpirationTime <= DateTime.Now)
                            {
                                ResetAccountLockOut();
                                isLockedOut = false;
                            }
                            else
                            {
                                isLockedOut = true;
                            }
                        }
                        else
                        {
                            isLockedOut = true;
                        }
                    }
                    else
                    {
                        
                        isLockedOut = false;
                    }
                    if ((isLockedOut) && (FwConfiguration.AccountLockoutManualUnlock) && (!this.webUsersId.Equals(string.Empty)))
                    {
                        FwAppData.WebUsersLockedOut(this.webUsersId);
                    }

                    return isLockedOut;
                }
            }
            //---------------------------------------------------------------------------------------------
            public FwDateTime LockoutExpirationTime
            {
                get
                {
                    FwDateTime lockoutExpirationTime = null;
                    if (lastFailedLoginAttempt != null)
                    {
                        lockoutExpirationTime = this.lastFailedLoginAttempt.ToDateTime().AddMinutes(FwConfiguration.AccountLockoutMinutes);
                    }
                    else
                    {
                        lockoutExpirationTime = DateTime.Now;
                    }
                    return lockoutExpirationTime;
                }
            }
            //---------------------------------------------------------------------------------------------
            public User(string webusersid)
            {
                this.webUsersId = webusersid;
            }
            //---------------------------------------------------------------------------------------------
            public void ResetAccountLockOut()
            {
                this.failedLoginAttempts = 0;
                this.lastFailedLoginAttempt = null;
            }
            //---------------------------------------------------------------------------------------------
            public void IncrementFailedLoginAttempts()
            {
                this.failedLoginAttempts++;
                this.lastFailedLoginAttempt = DateTime.Now;
            }
            //---------------------------------------------------------------------------------------------
        }
        //---------------------------------------------------------------------------------------------

        #region IDictionary<string,User> Members
        //---------------------------------------------------------------------------------------------
        public void Add(string key, FwAccountLockoutService.User value)
        {
            users.Add(key, value);
        }
        //---------------------------------------------------------------------------------------------
        public bool ContainsKey(string key)
        {
            return users.ContainsKey(key);
        }
        //---------------------------------------------------------------------------------------------
        public ICollection<string> Keys
        {
            get { return users.Keys; }
        }
        //---------------------------------------------------------------------------------------------
        public bool Remove(string key)
        {
            return users.Remove(key);
        }
        //---------------------------------------------------------------------------------------------
        public bool TryGetValue(string key, out FwAccountLockoutService.User value)
        {
            return users.TryGetValue(key, out value);
        }
        //---------------------------------------------------------------------------------------------
        public ICollection<FwAccountLockoutService.User> Values
        {
            get { return users.Values; }
        }
        //---------------------------------------------------------------------------------------------
        public FwAccountLockoutService.User this[string webusersid]
        {
            get
            {
                if (!users.ContainsKey(webusersid))
                {
                    users[webusersid] = new User(webusersid);
                }
                return users[webusersid];
            }
            set
            {
                users[webusersid] = value;
            }
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
        #region ICollection<KeyValuePair<string,User>> Members
        //---------------------------------------------------------------------------------------------
        public void Add(KeyValuePair<string, FwAccountLockoutService.User> item)
        {
            users.Add(item.Key, item.Value);
        }
        //---------------------------------------------------------------------------------------------
        public void Clear()
        {
            users.Clear();
        }
        //---------------------------------------------------------------------------------------------
        public bool Contains(KeyValuePair<string, FwAccountLockoutService.User> item)
        {
            return users.Contains(item);
        }
        //---------------------------------------------------------------------------------------------
        public void CopyTo(KeyValuePair<string, FwAccountLockoutService.User>[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach(KeyValuePair<string, FwAccountLockoutService.User> item in users)
            {
                if (i < array.Length)
                {
                    array[i] = item;
                    i++;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public int Count
        {
            get { return users.Count; }
        }
        //---------------------------------------------------------------------------------------------
        public bool IsReadOnly
        {
            get { return false; }
        }
        //---------------------------------------------------------------------------------------------
        public bool Remove(KeyValuePair<string, FwAccountLockoutService.User> item)
        {
            return users.Remove(item.Key);
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
        #region IEnumerable<KeyValuePair<string,User>> Members
        //---------------------------------------------------------------------------------------------
        public IEnumerator<KeyValuePair<string, FwAccountLockoutService.User>> GetEnumerator()
        {
            return users.GetEnumerator();
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
        #region IEnumerable Members
        //---------------------------------------------------------------------------------------------
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return users.GetEnumerator();
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
    }
}
