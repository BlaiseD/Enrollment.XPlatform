﻿using Enrollment.Forms.Configuration;
using Enrollment.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Converters
{
    public class PickerItemDisplayPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            object bindingContext = ((VisualElement)parameter).BindingContext;
            var dropDownTemplate = bindingContext.GetPropertyValue<DropDownTemplateDescriptor>
            (
                nameof(PickerValidatableObject<int>.DropDownTemplate)
            );

            return value.GetPropertyValue<string>(dropDownTemplate.TextField);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
