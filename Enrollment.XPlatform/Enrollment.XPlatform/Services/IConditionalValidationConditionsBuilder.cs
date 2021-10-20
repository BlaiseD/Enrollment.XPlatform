using Enrollment.Forms.Configuration.Directives;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IConditionalValidationConditionsBuilder
    {
        List<ValidateIf<TModel>> GetConditions<TModel>(Dictionary<string, List<DirectiveDescriptor>> conditionalDirectives, IEnumerable<IValidatable> properties);
    }
}
