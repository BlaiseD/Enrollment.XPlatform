using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public interface IDetailGroupBoxSettings
    {
        string GroupHeader { get; }
        List<DetailItemSettingsDescriptor> FieldSettings { get; }
        MultiBindingDescriptor HeaderBindings { get; }
        bool IsHidden { get; }
    }
}
