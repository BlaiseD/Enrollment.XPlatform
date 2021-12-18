using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Requests;
using System;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Services
{
    public interface IScopedFlowManagerService : IDisposable
    {
        IFlowManager FlowManager { get; }
        Task RunFlow(NewFlowRequest request);
        void SetFlowDataCacheItem(string key, object value);
        object GetFlowDataCacheItem(string key);
    }
}
