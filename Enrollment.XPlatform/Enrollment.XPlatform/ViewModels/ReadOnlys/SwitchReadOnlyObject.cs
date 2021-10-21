using Enrollment.Forms.Configuration.DetailForm;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class SwitchReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public SwitchReadOnlyObject(string name, DetailControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
            DetailControlSettingsDescriptor = setting;
            SwitchLabel = setting.Title;
        }

        public DetailControlSettingsDescriptor DetailControlSettingsDescriptor { get; }

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
