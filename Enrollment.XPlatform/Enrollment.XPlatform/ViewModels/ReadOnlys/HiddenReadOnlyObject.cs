using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(string name, FormControlSettingsDescriptor setting, IContextProvider contextProvider) : base(name, setting.TextTemplate.TemplateName, contextProvider.UiNotificationService)
        {
        }
    }
}
