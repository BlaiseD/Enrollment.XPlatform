using Enrollment.Forms.Configuration.Directives;
using Enrollment.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.EditForm
{
    public class EditFormSettingsDescriptor : IFormGroupSettings
    {
        public string Title { get; set; }
        public FormRequestDetailsDescriptor RequestDetails { get; set; }
        public Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public EditType EditType { get; set; }
        public string ModelType { get; set; }
        public Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
    }
}
