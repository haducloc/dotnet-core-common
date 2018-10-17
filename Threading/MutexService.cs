using NetCore.Common.Base;
using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Threading
{
    public class MutexService<K> : InitializeObject
    {
        protected readonly DictionaryImpl<K, object> mutexMap = new DictionaryImpl<K, object>();
        private readonly object sharedMutex = new Object();

        protected override void Init()
        {
        }

        public object GetMutex(K key)
        {
            AssertUtils.AssertNotNull(key);

            object mutex = this.mutexMap[key];
            if (mutex == null)
            {
                lock (this.sharedMutex)
                {
                    if ((mutex = this.mutexMap[key]) == null)
                    {
                        mutex = new object();
                        this.mutexMap[key] = mutex;
                    }
                }
            }
            return mutex;
        }
    }
}
