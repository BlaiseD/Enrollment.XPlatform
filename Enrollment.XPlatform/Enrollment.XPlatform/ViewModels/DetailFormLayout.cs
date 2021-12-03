using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.ViewModels
{
    public class DetailFormLayout
    {
        public DetailFormLayout()
        {
            ControlGroupBoxList = new List<ReadOnlyControlGroupBox>();
            Properties = new ObservableCollection<IReadOnly>();
        }

        public void Add(IReadOnly readOnly)
        {
            Properties.Add(readOnly);

            if (!ControlGroupBoxList.Any())
                throw new InvalidOperationException("{40FA092B-D705-44B4-A1B8-151BD2FCD2CD}");

            ControlGroupBoxList.Last().Add(readOnly);
        }

        public void AddControlGroupBox(IDetailGroupSettings formSettings)
        {
            ControlGroupBoxList.Add
            (
                new ReadOnlyControlGroupBox
                (
                    formSettings.Title,
                    formSettings.HeaderBindings,
                    new List<IReadOnly>(),
                    true
                )
            );
        }

        public void AddControlGroupBox(DetailGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
        {
            ControlGroupBoxList.Add
            (
                new ReadOnlyControlGroupBox
                (
                    groupBoxSettingsDescriptor.GroupHeader,
                    groupBoxSettingsDescriptor.HeaderBindings,
                    new List<IReadOnly>(),
                    groupBoxSettingsDescriptor.IsHidden == false
                )
            );
        }

        public List<ReadOnlyControlGroupBox> ControlGroupBoxList { get; }

        public ObservableCollection<IReadOnly> Properties { get; }
    }
}
