﻿using Enrollment.Forms.Configuration.Directives;
using Enrollment.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.EditForm
{
    public class FormGroupSettingsDescriptor : FormItemSettingsDescriptor, IChildFormGroupSettings
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroup;
        public string Title { get; set; }
        public string ValidFormControlText { get; set; }
        public string InvalidFormControlText { get; set; }
        public string ModelType { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; set; }
    }
}