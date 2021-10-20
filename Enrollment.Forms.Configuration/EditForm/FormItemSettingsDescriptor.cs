namespace Enrollment.Forms.Configuration.EditForm
{
    public abstract class FormItemSettingsDescriptor
    {
        abstract public AbstractControlEnumDescriptor AbstractControlType { get; }
        public string Field { get; set; }
    }
}
