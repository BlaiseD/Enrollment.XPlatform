using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public interface IDetailGroupSettings
    {
        string ModelType { get; }
        string Title { get; }
        List<DetailItemSettingsDescriptor> FieldSettings { get; }
        MultiBindingDescriptor HeaderBindings { get; }
    }
}
