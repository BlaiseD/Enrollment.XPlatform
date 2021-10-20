using AutoMapper;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.Services
{
    public class PropertiesUpdater : IPropertiesUpdater
    {
        private readonly IMapper mapper;

        public PropertiesUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void UpdateProperties(IEnumerable<IValidatable> properties, object entity, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null) 
            => UpdateValidatables(properties, entity, fieldSettings, parentField);

        private void UpdateValidatables(IEnumerable<IValidatable> properties, object source, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null)
        {
            IDictionary<string, object> existingValues = mapper.Map<Dictionary<string, object>>(source) ?? new Dictionary<string, object>();
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                {
                    if (existingValues.TryGetValue(multiSelectFormControlSetting.Field, out object @value) && @value != null)
                    {
                        propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                Type.GetType(multiSelectFormControlSetting.MultiSelectTemplate.ModelType)
                            ),
                            new object[] { @value }
                        );
                    }
                }
                else if (setting is FormControlSettingsDescriptor controlSetting)
                {//must stay after MultiSelect because MultiSelect extends FormControl
                    if (existingValues.TryGetValue(controlSetting.Field, out object @value) && @value != null)
                        propertiesDictionary[GetFieldName(controlSetting.Field)].Value = @value;
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (existingValues.TryGetValue(formGroupSetting.Field, out object @value) && @value != null)
                    {
                        if (formGroupSetting.FormGroupTemplate == null)
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: 74E0697E-B5EF-4939-B0B4-8B7E4AE5544B");

                        if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                            UpdateValidatables(properties, @value, formGroupSetting.FieldSettings, GetFieldName(formGroupSetting.Field));
                        else if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                            propertiesDictionary[GetFieldName(formGroupSetting.Field)].Value = @value;
                        else
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate.TemplateName)}: 5504FE49-2766-4D7C-916D-8FC633477DB1");
                    }
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    if (existingValues.TryGetValue(formGroupArraySetting.Field, out object @value) && @value != null)
                    {
                        propertiesDictionary[GetFieldName(formGroupArraySetting.Field)].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                Type.GetType(formGroupArraySetting.ModelType)
                            ),
                            new object[] { @value }
                        );
                    }
                }
            }

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";
        }
    }
}
