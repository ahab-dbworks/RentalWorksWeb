using FwStandard.AppManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Api
{
    // This can be configured as the IContractResolver for Json.NET in Startup.  This can control stuff at runtime, like whether a property gets serialized
    public class FwContractResolver : DefaultContractResolver
    {
        public static readonly FwContractResolver Instance = new FwContractResolver();

    //    mv 2018-09-15 - need to look at the security tree and control property serialization using a strategy like this:
    //    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    //{
    //    JsonProperty property = base.CreateProperty(member, memberSerialization);

    //    if (property.DeclaringType == typeof(Employee) && property.PropertyName == "Manager")
    //    {
    //        property.ShouldSerialize =
    //            instance =>
    //            {
    //                Employee e = (Employee)instance;
    //                return e.Manager != e;
    //            };
    //    }

    //    return property;
    //}
    }
}
