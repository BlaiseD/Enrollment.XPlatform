namespace Enrollment.Forms.Configuration.EditForm
{
    public interface IChildFormGroupSettings : IFormGroupSettings
    {
        string ValidFormControlText { get; }
        string InvalidFormControlText { get; }
        string Placeholder { get; set; }
        FormGroupTemplateDescriptor FormGroupTemplate { get; }
    }
}
