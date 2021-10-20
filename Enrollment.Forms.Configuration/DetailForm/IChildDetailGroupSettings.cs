using Enrollment.Forms.Configuration.EditForm;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public interface IChildDetailGroupSettings : IDetailGroupSettings
    {
        string Placeholder { get; }
        FormGroupTemplateDescriptor FormGroupTemplate { get; }
    }
}
