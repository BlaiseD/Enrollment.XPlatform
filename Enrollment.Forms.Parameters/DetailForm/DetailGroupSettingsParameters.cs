using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.DetailForm
{
    public class DetailGroupSettingsParameters : DetailItemSettingsParameters
    {
		public DetailGroupSettingsParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Source property name from the target object.")]
			string field,

			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Title for the detail group.")]
			string title,

			[Comments("Place holder text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Office Assignment)")]
			string placeholder,

			[Comments("The entity type for the object being displayed. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("XAML template for the detail group.")]
			FormGroupTemplateParameters formGroupTemplate,

			[Comments("Configuration for each field in the form group.")]
			List<DetailItemSettingsParameters> fieldSettings,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Enrollment.Domain.Entities"
		) : base(field)
		{
			Title = title;
			ModelType = modelType;
			Placeholder = placeholder;
			FormGroupTemplate = formGroupTemplate;
			FieldSettings = fieldSettings;
		}

		public string Title { get; set; }
		public Type ModelType { get; set; }
		public string Placeholder { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
    }
}