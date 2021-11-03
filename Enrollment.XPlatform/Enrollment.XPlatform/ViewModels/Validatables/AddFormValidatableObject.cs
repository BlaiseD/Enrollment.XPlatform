﻿using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Validators;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public class AddFormValidatableObject<T> : FormValidatableObject<T> where T : class
    {
        public AddFormValidatableObject(string name, IChildFormGroupSettings setting, IEnumerable<IValidationRule> validations, IContextProvider contextProvider) : base(name, setting, validations, contextProvider)
        {
        }

        protected override void CreateFieldsCollection()
        {
            FormLayout = this.fieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings);
        }

        public event EventHandler AddCancelled;

        protected override void Cancel()
        {
            AddCancelled?.Invoke(this, new EventArgs());
            base.Cancel();
        }
    }
}
