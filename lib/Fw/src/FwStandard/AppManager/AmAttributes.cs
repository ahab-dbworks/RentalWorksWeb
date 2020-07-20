using FwStandard.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.AppManager
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class FwControllerAttribute : Attribute
    {
        public readonly string Id;
        public readonly string Editions;
        public readonly string ParentId;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwControllerAttribute(string Id, string Editions = null, string ParentId = null)
        {
            this.Id = Id;
            this.Editions = Editions;
            this.ParentId = ParentId;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    public enum FwControllerActionTypes {
        Browse, View, New, Edit, Save, Delete, Option, ApiMethod, ControlBrowse, ControlNew, ControlEdit, ControlSave, ControlDelete, ControlOption
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class FwControllerMethodAttribute : Attribute
    {
        public readonly string Id;
        public readonly string Editions;
        public readonly bool AllowAnonymous;
        public readonly bool ValidateSecurityGroup;
        public readonly FwControllerActionTypes ActionType;
        public readonly string ParentId;
        public readonly string Caption;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwControllerMethodAttribute(string Id, FwControllerActionTypes ActionType = FwControllerActionTypes.Browse, string Caption = null, string ParentId = null, string Editions = null, bool AllowAnonymous = false, bool ValidateSecurityGroup = true)
        {
            this.Id = Id;
            this.Editions = Editions;
            this.AllowAnonymous = AllowAnonymous;
            this.ValidateSecurityGroup = ValidateSecurityGroup;
            this.ActionType = ActionType;
            this.ParentId = ParentId;
            this.Caption = Caption;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class FwLogicAttribute : Attribute
    {
        public readonly string Id;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwLogicAttribute(string Id)
        {
            this.Id = Id;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class FwLogicPropertyAttribute : Attribute
    {
        public readonly string Id;
        public readonly bool IsPrimaryKey;
        public readonly bool IsRecordTitle;
        public readonly bool IsReadOnly;
        public readonly bool IsPrimaryKeyOptional;
        public readonly bool IsNotAudited;
        public readonly bool IsAuditMasked;
        public readonly bool AuditNoSave;
        public readonly bool DisableDirectAssign;  // for New 
        public readonly bool DisableDirectModify;  // for Editing 
        public readonly bool IsForeignKey;
        public readonly string RelatedIdField;
        public readonly Type RelatedObject;
        public readonly string RelatedObjectFieldName;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwLogicPropertyAttribute(string Id, bool IsPrimaryKey = false, bool IsRecordTitle = false, bool IsReadOnly = false, bool IsPrimaryKeyOptional = false, bool IsNotAudited = false, bool IsAuditMasked = false, bool AuditNoSave = false, bool DisableDirectAssign = false, bool DisableDirectModify = false, bool IsForeignKey = false, string RelatedIdField = null, Type RelatedObject = null, string RelatedObjectFieldName = "")
        {
            this.Id = Id;
            this.IsPrimaryKey         = IsPrimaryKey;
            this.IsRecordTitle        = IsRecordTitle;
            this.IsReadOnly           = IsReadOnly;
            this.IsPrimaryKeyOptional = IsPrimaryKeyOptional;
            this.IsNotAudited         = IsNotAudited;
            this.IsAuditMasked        = IsAuditMasked;
            this.AuditNoSave          = AuditNoSave;
            this.DisableDirectAssign  = DisableDirectAssign;
            this.DisableDirectModify  = DisableDirectModify;

            this.IsForeignKey           = IsForeignKey;
            this.RelatedIdField         = RelatedIdField;
            this.RelatedObject          = RelatedObject;
            this.RelatedObjectFieldName = RelatedObjectFieldName;
        }
        //---------------------------------------------------------------------------------------------------------------------------

    }

    public enum FwControlTypes { Grid, AppImage }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FwControlAttribute : Attribute
    {
        public readonly string Caption;
        public readonly string SecurityId;
        public readonly FwControlTypes ControlType;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwControlAttribute(string Caption, string SecurityId, FwControlTypes ControlType)
        {
            this.Caption = Caption;
            this.SecurityId = SecurityId;
            this.ControlType = ControlType;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FwOptionsGroupAttribute : Attribute
    {
        public readonly string Caption;
        public readonly string SecurityId;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwOptionsGroupAttribute(string Caption, string SecurityId)
        {
            this.Caption = Caption;
            this.SecurityId = SecurityId;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
