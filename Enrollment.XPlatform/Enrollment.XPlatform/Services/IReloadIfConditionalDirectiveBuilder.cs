using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IReloadIfConditionalDirectiveBuilder
    {
        List<ReloadIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties);
    }
}
