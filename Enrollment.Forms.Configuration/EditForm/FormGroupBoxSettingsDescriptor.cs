using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.EditForm
{
    public class FormGroupBoxSettingsDescriptor : FormItemSettingsDescriptor
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.GroupBox;

        public string GroupHeader { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public bool IsHidden { get; set; }
    }
}
