using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Services
{
    public class ScopedFlowManagerService : IScopedFlowManagerService
    {
        private readonly IServiceScope scope;

        public ScopedFlowManagerService(IServiceScopeFactory serviceScopeFactory, IAppLogger appLogger)
        {
            this.appLogger = appLogger;
            scope = serviceScopeFactory.CreateScope();
            FlowManager = scope.ServiceProvider.GetRequiredService<IFlowManager>();
        }

        private readonly IAppLogger appLogger;

        public IFlowManager FlowManager { get; }

        public async Task RunFlow(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(UiNotificationService), $"RunFlow (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        public void Dispose()
        {
            scope?.Dispose();
        }

        public void SetFlowDataCacheItem(string key, object value)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return;

            this.FlowManager.FlowDataCache.Items[key] = value;
        }

        public object GetFlowDataCacheItem(string key)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return null;

            return this.FlowManager.FlowDataCache.Items.TryGetValue(key, out object value) ? value : null;
        }
    }
}
