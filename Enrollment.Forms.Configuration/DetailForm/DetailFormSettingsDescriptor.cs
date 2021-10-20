using Enrollment.Common.Configuration.ItemFilter;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public class DetailFormSettingsDescriptor : IDetailGroupSettings
    {
        public string Title { get; set; }
        public FormRequestDetailsDescriptor RequestDetails { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
        public DetailType DetailType { get; set; }
        public string ModelType { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public MultiBindingDescriptor SubtitleBindings { get; set; }
        public ItemFilterGroupDescriptor ItemFilterGroup { get; set; }
    }
}
