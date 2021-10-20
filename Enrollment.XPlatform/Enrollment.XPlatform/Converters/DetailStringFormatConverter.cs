using Enrollment.Forms.Configuration.DetailForm;
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

            object GetFormattedString(DetailControlSettingsDescriptor formControlSettings)
            {
                if (value == null)
                    return null;

                if (string.IsNullOrEmpty(formControlSettings.StringFormat))
                    return value;

                return string.Format(CultureInfo.CurrentCulture, formControlSettings.StringFormat, value);
            }

            DetailControlSettingsDescriptor GetFormControlSettingsDescriptor()
                => ((VisualElement)parameter).BindingContext.GetPropertyValue<DetailControlSettingsDescriptor>
                (
                    nameof(TextFieldReadOnlyObject<string>.DetailControlSettingsDescriptor)
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
