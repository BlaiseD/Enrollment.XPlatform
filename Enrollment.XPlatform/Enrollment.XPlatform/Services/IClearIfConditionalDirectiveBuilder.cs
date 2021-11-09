﻿using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IClearIfConditionalDirectiveBuilder
    {
        List<ClearIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties);
    }
}