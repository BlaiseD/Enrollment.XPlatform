using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;
using System.Collections.Generic;
using System.Globalization;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class TextFieldReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public TextFieldReadOnlyObject(string name, FormControlSettingsDescriptor setting, IContextProvider contextProvider) : base(name, setting.TextTemplate.TemplateName, contextProvider.UiNotificationService)
        {
            FormControlSettingsDescriptor = setting;
            this.Title = setting.Title;
        }

        public FormControlSettingsDescriptor FormControlSettingsDescriptor { get; }

        public string DisplayText
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(Value, default(T)))
                    return string.Empty;

                if (string.IsNullOrEmpty(FormControlSettingsDescriptor.StringFormat))
                    return Value.ToString();

                return string.Format(CultureInfo.CurrentCulture, FormControlSettingsDescriptor.StringFormat, Value);
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }
    }
}
