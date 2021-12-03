using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.ViewModels
{
    public class ControlGroupBox : ObservableCollection<IValidatable>
    {
        public ControlGroupBox(string groupHeader, MultiBindingDescriptor headerBindings, IEnumerable<IValidatable> collection, bool isVisible) : base(collection)
        {
            GroupHeader = groupHeader;
            HeaderBindings = headerBindings;
            this.IsVisible = isVisible;
        }

        public string GroupHeader { get; set; }
        public bool IsVisible { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public Dictionary<string, IValidatable> BindingPropertiesDictionary
            => this.ToDictionary(p => p.Name.ToBindingDictionaryKey());
    }
}
