using Enrollment.Forms.Configuration.Navigation;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Flow.Settings.Screen;

namespace Enrollment.XPlatform.Flow.Settings
{
    public class FlowSettings
    {
        public FlowSettings(ScreenSettingsBase screenSettings)
        {
            ScreenSettings = screenSettings;
        }

        public FlowSettings(FlowDataCache flowDataCache, ScreenSettingsBase screenSettings)
        {
            FlowDataCache = flowDataCache;
            ScreenSettings = screenSettings;
        }

        public FlowDataCache FlowDataCache { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
    }
}
