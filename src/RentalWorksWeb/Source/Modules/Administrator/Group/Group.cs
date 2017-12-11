﻿using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using FwStandard.Security;
using System;
using System.Threading.Tasks;

namespace Web.Source.Modules
{
    class Group : FwModule
    {
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return form.Tables["groups"].GetField("name").Value;
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            form.Tables["groups"].GetField("groupsid").UniqueIdentifier = true;
            form.Tables["groups"].GetField("groupsid").Value = FwSqlData.GetNextId(this.form.DatabaseConnection);
        }
        //---------------------------------------------------------------------------------------------
        protected override string getFormUniqueId()
        {
            return form.Tables["groups"].GetUniqueId("groupsid").Value;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            base.GetData();
            switch((string)request.method)
            {
                case "getapplicationtree":
                    GetApplicationTree();
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected void GetApplicationTree()
        {
            const string METHOD_NAME = "FwGroup.getApplicationTree";
            FwSecurityTreeNode groupTree;
            string groupsid;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "groupsid");
            groupsid = FwCryptography.AjaxDecrypt(request.groupsid);
            groupTree = Task.Run<FwSecurityTreeNode>(async () => await FwSecurityTree.Tree.GetGroupsTreeAsync(groupsid, false)).Result;
            
            response.applicationtree = groupTree;
        }
        //---------------------------------------------------------------------------------------------
    }
}
