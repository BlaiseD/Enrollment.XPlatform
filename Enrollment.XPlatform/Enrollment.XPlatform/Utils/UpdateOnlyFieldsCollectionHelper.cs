using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Services;
using System;

namespace Enrollment.XPlatform.Utils
{
    internal class UpdateOnlyFieldsCollectionHelper : FieldsCollectionHelper
    {
        public UpdateOnlyFieldsCollectionHelper(IFormGroupSettings formSettings, IContextProvider contextProvider) : base(formSettings, contextProvider)
        {
        }

        protected override void AddFormControl(FormControlSettingsDescriptor setting, string name)
        {
            if (setting.UpdateOnlyTextTemplate != null)
            {
                AddTextControl(setting, setting.UpdateOnlyTextTemplate, name);
            }
            else if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate, name);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting, name);
            else
                throw new ArgumentException($"{nameof(setting)}: 32652CB4-2574-4E5B-9B3F-7E47B37425AD");
        }
    }
}
