using Enrollment.Forms.Configuration.EditForm;
using Enrollment.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Converters
{
    public class DetailStringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetFormattedString(GetFormControlSettingsDescriptor());

            object GetFormattedString(FormControlSettingsDescriptor formControlSettings)
            {
                if (value == null)
                    return null;

                if (string.IsNullOrEmpty(formControlSettings.StringFormat))
                    return value;

                return string.Format(CultureInfo.CurrentCulture, formControlSettings.StringFormat, value);
            }

            FormControlSettingsDescriptor GetFormControlSettingsDescriptor()
                => ((VisualElement)parameter).BindingContext.GetPropertyValue<FormControlSettingsDescriptor>
                (
                    nameof(TextFieldReadOnlyObject<string>.FormControlSettingsDescriptor)
                );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().TryParse(targetType, out object result))
                return result;

            return value;
        }
    }
}
