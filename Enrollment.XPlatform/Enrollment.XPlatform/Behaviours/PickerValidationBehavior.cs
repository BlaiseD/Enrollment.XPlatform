﻿using Xamarin.Forms;

namespace Enrollment.XPlatform.Behaviours
{
    public class PickerValidationBehavior : BehaviorBase<Picker>
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create
        (
            nameof(IsValid),
            typeof(bool),
            typeof(PickerValidationBehavior),
            true,
            BindingMode.Default,
            null,
            (bindable, oldValue, newValue) => OnIsValidChanged(bindable, newValue)
        );

        public static readonly BindableProperty IsDirtyProperty = BindableProperty.Create
        (
            nameof(IsDirty),
            typeof(bool),
            typeof(PickerValidationBehavior),
            true,
            BindingMode.Default,
            null,
            (bindable, oldValue, newValue) => OnIsDirtyChanged(bindable, newValue)
        );

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public bool IsDirty
        {
            get => (bool)GetValue(IsDirtyProperty);
            set => SetValue(IsDirtyProperty, value);
        }

        private static void OnIsValidChanged(BindableObject bindable, object newValue)
        {
            if (bindable is PickerValidationBehavior isValidBehavior &&
                 newValue is bool isValid)
            {
                UpdatePlaceholderColor(isValidBehavior.IsDirty, isValid, isValidBehavior);
            }
        }

        private static void OnIsDirtyChanged(BindableObject bindable, object newValue)
        {
            if (bindable is PickerValidationBehavior isValidBehavior &&
                 newValue is bool isDirty)
            {
                UpdatePlaceholderColor(isDirty, isValidBehavior.IsValid, isValidBehavior);
            }
        }

        private static void UpdatePlaceholderColor(bool isDirty, bool isValid, PickerValidationBehavior isValidBehavior)
        {
            if (!isDirty || isValid)
                isValidBehavior.AssociatedObject.SetDynamicResource(Picker.TitleColorProperty, "TertiaryTextColor");
            else
                isValidBehavior.AssociatedObject.SetDynamicResource(Picker.TitleColorProperty, "ErrorTextColor");
        }
    }
}
