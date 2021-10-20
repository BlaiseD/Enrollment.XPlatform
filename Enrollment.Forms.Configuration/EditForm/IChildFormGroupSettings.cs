namespace Enrollment.Forms.Configuration.EditForm
{
    public interface IChildFormGroupSettings : IFormGroupSettings
    {
        string ValidFormControlText { get; }
        string InvalidFormControlText { get; }
        FormGroupTemplateDescriptor FormGroupTemplate { get; }
    }
}
