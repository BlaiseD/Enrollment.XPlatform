using Enrollment.Forms.Configuration.EditForm;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class SwitchReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public SwitchReadOnlyObject(string name, FormControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
            FormControlSettingsDescriptor = setting;
            SwitchLabel = setting.Title;
        }

        public FormControlSettingsDescriptor FormControlSettingsDescriptor { get; }

        private string _switchLabel;
        public string SwitchLabel
        {
            get => _switchLabel;
            set
            {
                if (_switchLabel == value)
                    return;

                _switchLabel = value;
                OnPropertyChanged();
            }
        }
    }
}
