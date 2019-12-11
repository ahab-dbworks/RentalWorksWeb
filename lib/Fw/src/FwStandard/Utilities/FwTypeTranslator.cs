using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwStandard.Utilities
{
    // This was created to override the Model names for classes in Swashbuckle/Swagger, because reflection returns an ugly value for Generic Type names instead of a readable form like GetRequest<SomeType>
    public static class FwTypeTranslator
    {
        private static Dictionary<Type, string> _defaultDictionary = new Dictionary<System.Type, string>
        {
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(bool), "bool"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(decimal), "decimal"},
            {typeof(char), "char"},
            {typeof(string), "string"},
            {typeof(object), "object"},
            {typeof(void), "void"}
        };

        public static string GetFriendlyName(this Type type, Dictionary<Type, string> translations)
        {
            if (translations.ContainsKey(type))
                return translations[type];
            else if (type.IsArray)
                return GetFriendlyName(type.GetElementType(), translations) + "[]";
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments()[0].GetFriendlyName() + "?";
            else if (type.IsGenericType)
                return type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(x => GetFriendlyName(x)).ToArray()) + ">";
            else
                return type.Name;
        }

        public static string GetFriendlyName(this Type type)
        {
            return type.GetFriendlyName(_defaultDictionary);
        }
    }
}
