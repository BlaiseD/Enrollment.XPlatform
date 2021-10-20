namespace Enrollment.Forms.Configuration.DetailForm
{
    public class DetailControlSettingsDescriptor : DetailItemSettingsDescriptor
    {
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public string StringFormat { get; set; }
        public string Type { get; set; }
        public TextFieldTemplateDescriptor TextTemplate { get; set; }
        public DropDownTemplateDescriptor DropDownTemplate { get; set; }
    }
}
