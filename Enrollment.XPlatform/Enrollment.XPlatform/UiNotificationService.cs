using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Enrollment.XPlatform
{
    public class UiNotificationService
    {
        public UiNotificationService(IFlowManager flowManager, IAppLogger appLogger)
        {
            this.FlowManager = flowManager;
            this.appLogger = appLogger;
        }

        #region Fields
        private readonly IAppLogger appLogger;
        #endregion Fields

        #region Properties
        public IFlowManager FlowManager { get; }
        public Subject<FlowSettings> FlowSettingsSubject { get; set; } = new Subject<FlowSettings>();
        public Subject<string> ValueChanged { get; set; } = new Subject<string>();
        public FlowSettings FlowSettings { get; private set; }
        #endregion Properties

        public async Task Start()
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.Start("home");
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(UiNotificationService), $"Start (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
            this.FlowSettings = flowSettings;
            this.FlowSettingsSubject.OnNext(flowSettings);
        }

        public async Task NavStart(NavBarRequest navBarRequest)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.NavStart(navBarRequest);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(UiNotificationService), $"NavStart (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
            this.FlowSettings = flowSettings;
            this.FlowSettingsSubject.OnNext(flowSettings);
        }

        public async Task Next(CommandButtonRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.Next(request);
            DateTime dt2 = DateTime.Now;
            appLogger.LogMessage(nameof(UiNotificationService), $"Next (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            this.FlowSettings = flowSettings;
            this.FlowSettingsSubject.OnNext(flowSettings);
        }

        public void NotifyPropertyChanged(string fieldName)
        {
            this.ValueChanged.OnNext(fieldName);
        }

        public void SetFlowDataCacheItem(string key, object value)
        {
            if (this.FlowSettings?.FlowDataCache?.Items == null)
                return;

            this.FlowSettings.FlowDataCache.Items[key] = value;
        }
    }
}
