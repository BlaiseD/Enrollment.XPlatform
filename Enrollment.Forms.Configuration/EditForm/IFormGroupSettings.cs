using Enrollment.Forms.Configuration.Directives;
using Enrollment.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.EditForm
{
    public interface IFormGroupSettings
    {
        string ModelType { get; }
        string Title { get; }
        Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; }
        List<FormItemSettingsDescriptor> FieldSettings { get; }
        Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; }
    }
}
