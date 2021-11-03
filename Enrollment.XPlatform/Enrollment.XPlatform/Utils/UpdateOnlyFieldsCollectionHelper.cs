using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Utils
{
    internal class UpdateOnlyFieldsCollectionHelper : FieldsCollectionHelper
    {
        public UpdateOnlyFieldsCollectionHelper(IFormGroupSettings formSettings, IContextProvider contextProvider, EditFormLayout formLayout = null, string parentName = null) : base(formSettings, contextProvider, formLayout, parentName)
        {
        }

        protected override void AddFormControl(FormControlSettingsDescriptor setting)
        {
            if (setting.UpdateOnlyTextTemplate != null)
            {
                AddTextControl(setting, setting.UpdateOnlyTextTemplate);
            }
            else if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 32652CB4-2574-4E5B-9B3F-7E47B37425AD");
        }

        protected override void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            new FieldsCollectionHelper
            (
                setting,
                this.contextProvider,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }
    }
}
