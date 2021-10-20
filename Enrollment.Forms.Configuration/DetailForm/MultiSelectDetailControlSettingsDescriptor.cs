using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public class MultiSelectDetailControlSettingsDescriptor : DetailControlSettingsDescriptor
    {
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateDescriptor MultiSelectTemplate { get; set; }
    }
}
