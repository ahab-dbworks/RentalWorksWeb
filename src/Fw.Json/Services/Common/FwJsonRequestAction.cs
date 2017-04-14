using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using Fw.Json.Utilities;

namespace Fw.Json.Services
{
    public class FwJsonRequestAction
    {
        //---------------------------------------------------------------------------------------------
        public delegate bool IsMatchDelegate(string requestPath, string applicationPath);
        public delegate void ActionDelegate(dynamic request, dynamic response, dynamic session);
        //---------------------------------------------------------------------------------------------
        public string[] Roles {get;set;}
        public IsMatchDelegate IsMatch {get;set;}
        public ActionDelegate OnMatch {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwJsonRequestAction(string[] roles, IsMatchDelegate isMatch, ActionDelegate onMatch)
        {
            Roles   = roles;
            IsMatch = isMatch;
            OnMatch = onMatch;
        }
        //---------------------------------------------------------------------------------------------
    }
}