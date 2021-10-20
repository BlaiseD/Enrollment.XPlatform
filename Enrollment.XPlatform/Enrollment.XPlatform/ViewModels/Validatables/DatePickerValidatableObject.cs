﻿using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public class DatePickerValidatableObject : ValidatableObjectBase<DateTime>
    {
        public DatePickerValidatableObject(string name, string templateName, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(name, templateName, validations, uiNotificationService)
        {
        }

        public ICommand DateChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
