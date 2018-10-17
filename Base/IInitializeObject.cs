using System;

namespace NetCore.Common.Base
{
    public interface IInitializeObject : IDisposable
    {
        void Initialize();
    }
}
