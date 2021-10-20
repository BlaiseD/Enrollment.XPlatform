using Enrollment.Forms.Configuration.DetailForm;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(string name, DetailControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
        }
    }
}
