using AutoMapper;
using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enrollment.XPlatform.Utils
{
    public static class EntityMapper
    {
        public static Dictionary<string, object> EntityToObjectDictionary(this object entity, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings)
            => mapper.Map<Dictionary<string, object>>(entity).ToObjectDictionaryFromEntity(mapper, fieldSettings);

        public static Dictionary<string, object> ValidatableListToObjectDictionary(this IEnumerable<IValidatable> properties, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings)
            => properties.ToDictionary(p => p.Name, p => p.Value).ToObjectDictionaryFromValidatableObjects(mapper, fieldSettings);

        public static object ToModelObject(this IEnumerable<IValidatable> properties, Type entityType, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings, object destination = null)
        {
            MethodInfo methodInfo = typeof(EntityMapper).GetMethod
            (
                "ToModelObject",
                1,
                new Type[]
                {
                    typeof(IEnumerable<IValidatable>),
                    typeof(IMapper),
                    typeof(List<FormItemSettingsDescriptor>),
                    entityType
                }
            ).MakeGenericMethod(entityType);

            return methodInfo.Invoke(null, new object[] { properties, mapper, fieldSettings, destination });
        }

        public static T ToModelObject<T>(this IEnumerable<IValidatable> properties, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings, T destination = null) where T : class
        {
            if (destination == null)
            {
                return mapper.Map<T>
                (
                    properties.ValidatableListToObjectDictionary(mapper, fieldSettings)
                );
            }

            return (T)mapper.Map
            (
                properties.ValidatableListToObjectDictionary(mapper, fieldSettings),
                destination,
                typeof(Dictionary<string, object>),
                typeof(T)
            );
        }

        /// <summary>
        /// Ensures all child objects and collections are dictionaries
        /// </summary>
        /// <param name="propertiesDictionary"></param>
        /// <param name="mapper"></param>
        /// <param name="fieldSettings"></param>
        /// <param name="parentField"></param>
        /// <returns></returns>
        private static Dictionary<string, object> ToObjectDictionaryFromValidatableObjects(this IDictionary<string, object> propertiesDictionary, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null)
        {
            return fieldSettings.Aggregate(new Dictionary<string, object>(), DoAggregation);

            Dictionary<string, object> DoAggregation(Dictionary<string, object> objectDictionary, FormItemSettingsDescriptor setting)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                    AddMultiSelects();
                else if (setting is FormControlSettingsDescriptor controlSetting)
                    AddSingleValueField();//must stay after MultiSelect because MultiSelect extends FormControl
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (formGroupSetting.FormGroupTemplate == null)
                        throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: E29413BD-6DC7-47D1-9986-B613E0568AFE");

                    if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                        AddFormGroupInline(formGroupSetting);
                    else if (formGroupSetting.FormGroupTemplate?.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                        AddFormGroupPopup(formGroupSetting);
                    else
                        throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: CE249F5F-5645-4B74-BA17-5E1A9A5E73C8");
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                    AddFormGroupArray(formGroupArraySetting);
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    groupBoxSettingsDescriptor.FieldSettings.Aggregate(objectDictionary, DoAggregation);
                }
                else
                    throw new ArgumentException($"{nameof(setting)}: 82FDF6AC-C2DB-4655-AA03-673E5C6B4E0A");

                return objectDictionary;

                void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting) => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                    (
                        (IEnumerable<object>)propertiesDictionary[GetFieldName(setting.Field)]
                    )
                    .Select
                    (
                        dictionary => dictionary.ToObjectDictionaryFromValidatableObjects(mapper, formGroupArraySetting.FieldSettings)
                    ).ToList()//Need an ICollection<Dictionary<string, object>>
                );

                void AddFormGroupPopup(FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (propertiesDictionary[GetFieldName(formGroupSetting.Field)] == null)
                    {
                        objectDictionary.Add(setting.Field, null);
                        return;
                    }

                    objectDictionary.Add
                    (
                        setting.Field,
                        mapper.Map<Dictionary<string, object>>
                        (
                            propertiesDictionary[GetFieldName(formGroupSetting.Field)]
                        ).ToObjectDictionaryFromValidatableObjects(mapper, formGroupSetting.FieldSettings)
                    );
                }

                void AddFormGroupInline(FormGroupSettingsDescriptor formGroupSetting) => objectDictionary.Add
                (
                    setting.Field,
                    ToObjectDictionaryFromValidatableObjects
                    (
                        propertiesDictionary,
                        mapper,
                        formGroupSetting.FieldSettings,
                        GetFieldName(formGroupSetting.Field)
                    )
                );

                void AddMultiSelects()
                {
                    if (propertiesDictionary[GetFieldName(setting.Field)] == null)
                    {//The mapper.Map<IEnumerable, IEnumerable>(null) behavior in the Xamarin runtime is different
                     //form the local tests.  Use the null check to ensure the same behavior.
                        objectDictionary.Add(setting.Field, null);
                        return;
                    }

                    objectDictionary.Add
                    (
                        setting.Field,
                        mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                        (
                            (IEnumerable<object>)propertiesDictionary[GetFieldName(setting.Field)]
                        )
                    );
                }

                void AddSingleValueField() => objectDictionary.Add
                (
                    setting.Field,
                    propertiesDictionary[GetFieldName(setting.Field)]
                );
            }

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";
        }

        private static Dictionary<string, object> ToObjectDictionaryFromEntity(this Dictionary<string, object> propertiesDictionary, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings)
        {
            if (propertiesDictionary.IsEmpty())//object must be null - no fields to update
                return new Dictionary<string, object>();

            return fieldSettings.Aggregate(new Dictionary<string, object>(), DoAggregation);

            Dictionary<string, object> DoAggregation(Dictionary<string, object>  objectDictionary, FormItemSettingsDescriptor setting)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                    AddMultiSelects();
                else if (setting is FormControlSettingsDescriptor controlSetting)
                    AddSingleValueField();//must stay after MultiSelect because MultiSelect extends FormControl
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    AddFormGroup(formGroupSetting);
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                    AddFormGroupArray(formGroupArraySetting);
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    groupBoxSettingsDescriptor.FieldSettings.Aggregate(objectDictionary, DoAggregation);
                }
                else
                    throw new ArgumentException($"{nameof(setting)}: CC7AD9E6-1CA5-4B9D-B1DF-D28AF8D6D757");

                return objectDictionary;

                void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting) => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                    (
                        (IEnumerable<object>)propertiesDictionary[setting.Field]
                    )
                    .Select
                    (
                        dictionary => dictionary.ToObjectDictionaryFromEntity(mapper, formGroupArraySetting.FieldSettings)
                    ).ToList()//Need an ICollection<Dictionary<string, object>>
                );

                void AddFormGroup(FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (propertiesDictionary[setting.Field] == null)
                    {
                        objectDictionary.Add(setting.Field, null);
                        return;
                    }

                    objectDictionary.Add
                    (
                        setting.Field,
                        mapper.Map<Dictionary<string, object>>
                        (
                            propertiesDictionary[setting.Field]
                        ).ToObjectDictionaryFromEntity(mapper, formGroupSetting.FieldSettings)
                    );
                }

                void AddMultiSelects()
                {
                    if (propertiesDictionary[setting.Field] == null)
                    {//The mapper.Map<IEnumerable, IEnumerable>(null) behavior in the Xamarin runtime is different
                     //form the local tests.  Use the null check to ensure the same behavior.
                        objectDictionary.Add(setting.Field, null);
                        return;
                    }

                    objectDictionary.Add
                    (
                        setting.Field,
                        mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                        (
                            (IEnumerable<object>)propertiesDictionary[setting.Field]
                        )
                    );
                }

                void AddSingleValueField() => objectDictionary.Add
                (
                    setting.Field,
                    propertiesDictionary[setting.Field]
                );
            }
        }

        public static void UpdateReadOnlys(this IEnumerable<IReadOnly> properties, object source, List<DetailItemSettingsDescriptor> fieldSettings, IMapper mapper, string parentField = null)
        {
            throw new NotImplementedException("{C00D7396-30A9-47F1-8761-F6AC81D9767E}");
        }

        const string EntityState = "EntityState";
        public static void UpdateEntityStates(Dictionary<string, object> existing, Dictionary<string, object> current, List<FormItemSettingsDescriptor> fieldSettings)
        {
            if (current.IsEmpty() && existing.IsEmpty() == false)
            {
                current[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                return;
            }

            if (current.IsEmpty() && existing.IsEmpty())
            {
                return;
            }

            if (existing.IsEmpty() && current.IsEmpty() == false)
            {
                current[EntityState] = LogicBuilder.Domain.EntityStateType.Added;
            }
            else
            {
                current[EntityState] = new DictionaryComparer().Equals(current, existing)
                    ? LogicBuilder.Domain.EntityStateType.Unchanged
                    : LogicBuilder.Domain.EntityStateType.Modified;
            }
            

            foreach (var setting in fieldSettings)
            {
                if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    existing.TryGetValue(setting.Field, out object existingObject);
                    UpdateEntityStates((Dictionary<string, object>)existingObject ?? new Dictionary<string, object>(), (Dictionary<string, object>)current[setting.Field], formGroupSetting.FieldSettings);
                }
                else if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettingsDescriptor)
                {
                    existing.TryGetValue(setting.Field, out object existingCollection);
                    ICollection<Dictionary<string, object>> existingList = (ICollection<Dictionary<string, object>>)existingCollection ?? new List<Dictionary<string, object>>();
                    ICollection<Dictionary<string, object>> currentList = (ICollection<Dictionary<string, object>>)current[setting.Field] ?? new List<Dictionary<string, object>>();

                    if (currentList.Any() == true)
                    {
                        foreach (var entry in currentList)
                        {
                            if (entry.ExistsInList(existingList, multiSelectFormControlSettingsDescriptor.KeyFields))
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Unchanged;
                            else
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Added;
                        }
                    }

                    if (existingList.Any() == true)
                    {
                        foreach (var entry in existingList)
                        {
                            if (!entry.ExistsInList(currentList, multiSelectFormControlSettingsDescriptor.KeyFields))
                            {
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                                currentList.Add(entry);
                            }
                        }
                    }
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    existing.TryGetValue(setting.Field, out object existingCollection);
                    ICollection<Dictionary<string, object>> existingList = (ICollection<Dictionary<string, object>>)existingCollection ?? new List<Dictionary<string, object>>();
                    ICollection<Dictionary<string, object>> currentList = (ICollection<Dictionary<string, object>>)current[setting.Field];

                    if (currentList.Any() == true)
                    {
                        foreach (var entry in currentList)
                        {
                            Dictionary<string, object> existingEntry = entry.GetExistingEntry(existingList, formGroupArraySetting.KeyFields);
                            UpdateEntityStates
                            (
                                existingEntry ?? new Dictionary<string, object>(),
                                entry,
                                formGroupArraySetting.FieldSettings
                            );
                        }
                    }

                    if (existingList.Any() == true)
                    {
                        foreach (var entry in existingList)
                        {
                            if (!entry.ExistsInList(currentList, formGroupArraySetting.KeyFields))
                            {
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                                currentList.Add(entry);
                            }
                        }
                    }
                }
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    UpdateEntityStates(existing, current, groupBoxSettingsDescriptor.FieldSettings);
                }
            }
        }

        private static bool IsEmpty(this IDictionary<string, object> dictionary)
            => dictionary?.Any() != true;
    }
}
