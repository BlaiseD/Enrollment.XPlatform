using Enrollment.Forms.Configuration.EditForm;
using System.Globalization;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class CheckboxReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public CheckboxReadOnlyObject(string name, FormControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
            FormControlSettingsDescriptor = setting;
        }

        public FormControlSettingsDescriptor FormControlSettingsDescriptor { get; }

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(FormControlSettingsDescriptor.StringFormat))
                    return Value ? "\u2713" : "";

                return string.Format(CultureInfo.CurrentCulture, FormControlSettingsDescriptor.StringFormat, Value ? "\u2713" : "");
            }
        }
    }
}
