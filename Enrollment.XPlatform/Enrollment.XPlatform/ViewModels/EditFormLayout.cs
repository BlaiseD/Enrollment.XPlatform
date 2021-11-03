using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.ViewModels
{
    public class EditFormLayout
    {
        public EditFormLayout()
        {
            ControlGroupBoxList = new List<ControlGroupBox>();
            Properties = new ObservableCollection<IValidatable>();
        }

        public void Add(IValidatable validatable)
        {
            Properties.Add(validatable);

            if (!ControlGroupBoxList.Any())
                throw new InvalidOperationException("{AA443C6C-7007-498B-9404-54D87CE1278C}");

            ControlGroupBoxList.Last().Add(validatable);
        }

        public void AddControlGroupBox(IFormGroupSettings formSettings)
        {
            ControlGroupBoxList.Add
            (
                new ControlGroupBox
                (
                    formSettings.Title,
                    formSettings.HeaderBindings,
                    new List<IValidatable>()
                )
            );
        }

        public void AddControlGroupBox(FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
        {
            ControlGroupBoxList.Add
            (
                new ControlGroupBox
                (
                    groupBoxSettingsDescriptor.GroupHeader,
                    groupBoxSettingsDescriptor.HeaderBindings,
                    new List<IValidatable>()
                )
            );
        }

        public List<ControlGroupBox> ControlGroupBoxList { get; }

        public ObservableCollection<IValidatable> Properties { get; }
    }
}
