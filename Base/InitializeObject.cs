using System;

namespace NetCore.Common.Base
{
    public abstract class InitializeObject : IInitializeObject
    {
        private volatile bool initialized;
        readonly object mutex = new object();
        public void Initialize()
        {
            if (this.initialized) return;
            lock (this.mutex)
            {
                if (this.initialized) return;
                try
                {
                    this.Init();
                    this.initialized = true;
                }
                catch (Exception ex)
                {
                    throw new InitializeException(ex);
                }
            }
        }

        protected abstract void Init();

        public virtual void Dispose()
        {
        }

        protected void AssertNotInitialized()
        {
            if (this.initialized)
            {
                throw new Exception("initialized.");
            }
        }
    }
}
