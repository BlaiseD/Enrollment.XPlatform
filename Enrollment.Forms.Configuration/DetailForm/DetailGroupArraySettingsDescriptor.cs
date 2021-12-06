using Enrollment.Forms.Configuration.EditForm;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public class DetailGroupArraySettingsDescriptor : DetailItemSettingsDescriptor, IChildDetailGroupSettings
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public List<string> KeyFields { get; set; }
        public string ModelType { get; set; }//e.g. T
        public string Type { get; set; }//e.g. ICollection<T>
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public string GroupHeader => Title;
        public bool IsHidden => false;
    }
}
