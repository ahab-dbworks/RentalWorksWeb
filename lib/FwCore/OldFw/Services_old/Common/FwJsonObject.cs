using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace FwCore.Services
{
    public class FwJsonObject : DynamicObject
    {
        private IDictionary<string, object> _dictionary { get; set; }
 
        public FwJsonObject(IDictionary<string, object> dictionary)
        {
            this._dictionary = dictionary;
        }
 
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this._dictionary[binder.Name];
            if (result is IDictionary<string, object>)
            {
                result = new FwJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
            {
                result = new List<FwJsonObject>((result as ArrayList).ToArray().Select(x => new FwJsonObject(x as IDictionary<string, object>)));
            }
            else if (result is ArrayList)
            {
                result = new List<object>((result as ArrayList).ToArray());
            }
            return this._dictionary.ContainsKey(binder.Name);
        }
    }
}