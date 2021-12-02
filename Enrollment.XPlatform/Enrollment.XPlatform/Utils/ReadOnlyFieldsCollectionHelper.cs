﻿using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.Utils
{
    internal class ReadOnlyFieldsCollectionHelper
    {
        private IDetailGroupSettings formSettings;
        private DetailFormLayout formLayout;
        private readonly IContextProvider contextProvider;
        private readonly string parentName;

        public ReadOnlyFieldsCollectionHelper(IDetailGroupSettings formSettings, IContextProvider contextProvider, DetailFormLayout formLayout = null, string parentName = null)
        {
            this.formSettings = formSettings;
            this.contextProvider = contextProvider;

            if (formLayout == null)
            {
                this.formLayout = new DetailFormLayout();
                if (this.formSettings.FieldSettings.ShouldCreateDefaultControlGroupBox())
                    this.formLayout.AddControlGroupBox(this.formSettings);
            }
            else
            {
                this.formLayout = formLayout;
            }

            this.parentName = parentName;
        }

        public DetailFormLayout CreateFields()
        {
            this.CreateFieldsCollection();
            return this.formLayout;
        }

        private void CreateFieldsCollection()
        {
            CreateFieldsCollection(this.formSettings.FieldSettings);
        }

        private void CreateFieldsCollection(List<DetailItemSettingsDescriptor> fieldSettings)
        {
            fieldSettings.ForEach
            (
                setting =>
                {
                    switch (setting)
                    {
                        case MultiSelectDetailControlSettingsDescriptor multiSelectDetailControlSettings:
                            AddMultiSelectControl(multiSelectDetailControlSettings);
                            break;
                        case DetailControlSettingsDescriptor formControlSettings:
                            AddFormControl(formControlSettings);
                            break;
                        case DetailGroupSettingsDescriptor formGroupSettings:
                            AddFormGroupSettings(formGroupSettings);
                            break;
                        case DetailGroupArraySettingsDescriptor formGroupArraySettings:
                            AddFormGroupArray(formGroupArraySettings);
                            break;
                        case DetailGroupBoxSettingsDescriptor groupBoxSettingsDescriptor:
                            AddGroupBoxSettings(groupBoxSettingsDescriptor);
                            break;
                        default:
                            throw new ArgumentException($"{nameof(setting)}: B024F65A-50DC-4D45-B8F0-9EC0BE0E2FE2");
                    }
                }
            );
        }

        private void AddGroupBoxSettings(DetailGroupBoxSettingsDescriptor setting)
        {
            this.formLayout.AddControlGroupBox(setting);

            if (setting.FieldSettings.Any(s => s is DetailGroupBoxSettingsDescriptor))
                throw new ArgumentException($"{nameof(setting.FieldSettings)}: E5DC0442-F3B9-4C50-B641-304358DF8EBA");

            CreateFieldsCollection(setting.FieldSettings);
        }

        private void AddMultiSelectControl(MultiSelectDetailControlSettingsDescriptor setting)
        {
            if (setting.MultiSelectTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.MultiSelectTemplate))
            {
                formLayout.Add(CreateMultiSelectReadOnlyObject(setting));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: E63A881F-3B4D-47A1-A13C-835EA8A86C61");
            }
        }

        private void AddFormControl(DetailControlSettingsDescriptor setting)
        {
            if (setting.TextTemplate != null)
                AddTextControl(setting);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 88225DC7-F14A-4CB5-AC97-3FE63BFCC298");
        }

        private void AddTextControl(DetailControlSettingsDescriptor setting)
        {
            if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.TextTemplate)
                || setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.PasswordTemplate)
                || setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.DateTemplate))
            {
                formLayout.Add(CreateTextFieldReadOnlyObject(setting));
            }
            else if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.HiddenTemplate))
            {
                formLayout.Add(CreateHiddenReadOnlyObject(setting));
            }
            else if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.CheckboxTemplate))
            {
                formLayout.Add(CreateCheckboxReadOnlyObject(setting));
            }
            else if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.SwitchTemplate))
            {
                formLayout.Add(CreateSwitchReadOnlyObject(setting));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.TextTemplate.TemplateName)}: 537C774B-91B8-490D-8F47-38834614C383");
            }
        }

        private void AddDropdownControl(DetailControlSettingsDescriptor setting)
        {
            if (setting.DropDownTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.PickerTemplate))
            {
                formLayout.Add(CreatePickerReadOnlyObject(setting));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: E3D30EE3-4AFF-4706-A1D2-BB5FA852258E");
            }
        }

        private void AddFormGroupSettings(DetailGroupSettingsDescriptor setting)
        {
            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 2A49FA6B-F0F0-4AE7-8CA4-A47D39C712C9");

            switch (setting.FormGroupTemplate.TemplateName)
            {
                case FromGroupTemplateNames.InlineFormGroupTemplate:
                    AddFormGroupInline(setting);
                    break;
                case FromGroupTemplateNames.PopupFormGroupTemplate:
                    AddFormGroupPopup(setting);
                    break;
                default:
                    throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: D6A2C8DF-8581-46C3-A4CA-810C9A14E635");
            }
        }

        private void AddFormGroupInline(DetailGroupSettingsDescriptor setting)
        {
            new ReadOnlyFieldsCollectionHelper
            (
                setting,
                this.contextProvider,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }

        private void AddFormGroupPopup(DetailGroupSettingsDescriptor setting)
            => formLayout.Add(CreateFormReadOnlyObject(setting));

        private void AddFormGroupArray(DetailGroupArraySettingsDescriptor setting)
        {
            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 0D7B0F95-8230-4ABD-B5B1-479558B4C4F1");

            if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
            {
                formLayout.Add(CreateFormArrayReadOnlyObject(setting));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 3B2D01FD-4405-484C-A325-E3C9E8E56A3A");
            }
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";

        private IReadOnly CreateFormReadOnlyObject(DetailGroupSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(FormReadOnlyObject<>).MakeGenericType(Type.GetType(setting.ModelType)),
                GetFieldName(setting.Field),
                setting,
                this.contextProvider
            );

        private IReadOnly CreateHiddenReadOnlyObject(DetailControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(HiddenReadOnlyObject<>).MakeGenericType(Type.GetType(setting.Type)),
                GetFieldName(setting.Field),
                setting
            );

        private IReadOnly CreateCheckboxReadOnlyObject(DetailControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(CheckboxReadOnlyObject),
                GetFieldName(setting.Field),
                setting
            );

        private IReadOnly CreateSwitchReadOnlyObject(DetailControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(SwitchReadOnlyObject),
                GetFieldName(setting.Field),
                setting
            );

        private IReadOnly CreateTextFieldReadOnlyObject(DetailControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(TextFieldReadOnlyObject<>).MakeGenericType(Type.GetType(setting.Type)),
                GetFieldName(setting.Field),
                setting
            );

        private IReadOnly CreatePickerReadOnlyObject(DetailControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(PickerReadOnlyObject<>).MakeGenericType(Type.GetType(setting.Type)),
                GetFieldName(setting.Field),
                setting,
                this.contextProvider
            );

        private IReadOnly CreateMultiSelectReadOnlyObject(MultiSelectDetailControlSettingsDescriptor setting)
        {
            return GetValidatable(Type.GetType(setting.MultiSelectTemplate.ModelType));
            IReadOnly GetValidatable(Type elementType)
                => (IReadOnly)Activator.CreateInstance
                (
                    typeof(MultiSelectReadOnlyObject<,>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType(elementType),
                        elementType
                    ),
                    GetFieldName(setting.Field),
                    setting,
                    this.contextProvider
                );
        }

        private IReadOnly CreateFormArrayReadOnlyObject(DetailGroupArraySettingsDescriptor setting)
        {
            return GetValidatable(Type.GetType(setting.ModelType));
            IReadOnly GetValidatable(Type elementType)
                => (IReadOnly)Activator.CreateInstance
                (
                    typeof(FormArrayReadOnlyObject<,>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType(elementType),
                        elementType
                    ),
                    GetFieldName(setting.Field),
                    setting,
                    this.contextProvider
                );
        }
    }
}
