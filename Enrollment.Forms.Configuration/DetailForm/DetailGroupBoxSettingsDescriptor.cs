using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public class DetailGroupBoxSettingsDescriptor : DetailItemSettingsDescriptor
    {
        public string GroupHeader { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public bool IsHidden { get; set; }
    }
}
