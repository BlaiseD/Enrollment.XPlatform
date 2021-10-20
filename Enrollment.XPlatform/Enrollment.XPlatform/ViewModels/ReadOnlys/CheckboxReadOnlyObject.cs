using Enrollment.Forms.Configuration.DetailForm;
using System.Globalization;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class CheckboxReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public CheckboxReadOnlyObject(string name, DetailControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
            DetailControlSettingsDescriptor = setting;
        }

        public DetailControlSettingsDescriptor DetailControlSettingsDescriptor { get; }

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(DetailControlSettingsDescriptor.StringFormat))
                    return Value ? "\u2713" : "";

                return string.Format(CultureInfo.CurrentCulture, DetailControlSettingsDescriptor.StringFormat, Value ? "\u2713" : "");
            }
        }
    }
}
