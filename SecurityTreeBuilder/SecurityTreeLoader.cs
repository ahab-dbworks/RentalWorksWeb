using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityTreeBuilder
{
    public class SecurityTreeLoader
    {
        public static List<Type> LoadTypes(string rootNamespace, Type subClassOf)
        {
            List<Type> types = typeof(WebApi.Program).Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(rootNamespace) && t.IsSubclassOf(subClassOf))
                .OrderBy(o => o.Name)
                .ToList<Type>();

            return types;
        }
    }
}
