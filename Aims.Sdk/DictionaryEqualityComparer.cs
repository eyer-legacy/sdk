using System.Collections.Generic;
using System.Linq;

namespace Aims.Sdk
{
    public class DictionaryEqualityComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
    {
        public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            if (x.Count != y.Count) return false;
            return x.All(kvp => y.ContainsKey(kvp.Key) && y[kvp.Key].Equals(kvp.Value));
        }

        public int GetHashCode(Dictionary<TKey, TValue> obj)
        {
            return obj.Keys
                .OrderBy(x => x)
                .Aggregate(17, (a, k) => a * 23 + k.GetHashCode());
        }
    }
}