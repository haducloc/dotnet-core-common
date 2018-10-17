using System.Collections.Generic;

namespace NetCore.Common.Base
{
    public class DictionaryImpl<K, V> : Dictionary<K, V> where V : class
    {
        public DictionaryImpl() : base() { }

        public DictionaryImpl(IDictionary<K, V> dictionary) : base(dictionary) { }

        public DictionaryImpl(IEqualityComparer<K> comparer) : base(comparer) { }

        public DictionaryImpl(int capacity) : base(capacity) { }

        public DictionaryImpl(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer) : base(dictionary, comparer) { }

        public DictionaryImpl(int capacity, IEqualityComparer<K> comparer) : base(capacity, comparer) { }

        public new V this[K key]
        {
            get
            {
                TryGetValue(key, out V value);
                return value;
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
