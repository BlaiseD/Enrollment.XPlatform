﻿using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class CheckboxReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public CheckboxReadOnlyObject(string name, string templateName, string checkboxLabel, IContextProvider contextProvider) : base(name, templateName, contextProvider.UiNotificationService)
        {
            CheckboxLabel = checkboxLabel;
        }

        private string _checkboxLabel;
        public string CheckboxLabel
        {
            get => _checkboxLabel;
            set
            {
                if (_checkboxLabel == value)
                    return;

                _checkboxLabel = value;
                OnPropertyChanged();
            }
        }
    }
}
