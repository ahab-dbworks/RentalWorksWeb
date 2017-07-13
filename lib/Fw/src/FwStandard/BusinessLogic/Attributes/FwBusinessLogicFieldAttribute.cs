using System;

namespace FwStandard.BusinessLogic.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwBusinessLogicFieldAttribute : Attribute
    {
        public readonly bool IsPrimaryKey;
        public readonly bool IsTitle;
        public readonly bool IsReadOnly;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwBusinessLogicFieldAttribute(bool isPrimaryKey = false, bool isTitle = false,  bool isReadOnly = false)
        {
            IsPrimaryKey = isPrimaryKey;
            IsTitle = isTitle;
            IsReadOnly = isReadOnly;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
