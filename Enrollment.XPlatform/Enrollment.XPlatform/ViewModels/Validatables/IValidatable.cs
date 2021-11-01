using Enrollment.XPlatform.Validators;
using System.Collections.Generic;
using System.ComponentModel;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public interface IValidatable : INotifyPropertyChanged
    {
        string Name { get; set; }
        string TemplateName { get; set; }
        bool IsValid { get; set; }
        bool IsDirty { get; set; }
        bool IsVisible { get; set; }
        bool IsEnabled { get; set; }
        object Value { get; set; }

        List<IValidationRule> Validations { get; }
        Dictionary<string, string> Errors { get; set; }

        bool Validate();
    }
}
