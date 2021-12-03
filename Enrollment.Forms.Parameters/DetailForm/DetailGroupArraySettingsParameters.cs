using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.DetailForm
{
    public class DetailGroupArraySettingsParameters : DetailItemSettingsParameters
    {
		public DetailGroupArraySettingsParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Source property name from the target object.")]
			string field,

			[Comments("Update listElementTypeSource first. Usually just a list of one item - the primary key. Additional fields apply when the primary key is a composite key.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "listElementTypeSource")]
			List<string> keyFields,

			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Title for the form group.")]
			string title,

			[Comments("Place holder text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Courses)")]
			string placeholder,

			[Comments("e.g. T. The element type for the list being dispalyed. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("e.g. ICollection<T>. The type for the list being dispalyed. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type type,

			[Comments("Template and property bindings to be displayed for each item in the list.")]
			FormsCollectionDisplayTemplateParameters formsCollectionDisplayTemplate,

			[Comments("XAML template for the form group.")]
			FormGroupTemplateParameters formGroupTemplate,

			[Comments("Configuration for each field in one of the array's form groups.")]
			List<DetailItemSettingsParameters> fieldSettings,

			[Comments("Multibindings list for the form header field.")]
			MultiBindingParameters headerBindings = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Enrollment.Domain.Entities",

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string listElementTypeSource = "Enrollment.Domain.Entities"
		)
		{
			Field = field;
			Title = title;
			Placeholder = placeholder;
			KeyFields = keyFields;
			ModelType = modelType;
			Type = type;
			FormsCollectionDisplayTemplate = formsCollectionDisplayTemplate;
			FormGroupTemplate = formGroupTemplate;
			FieldSettings = fieldSettings;
			HeaderBindings = headerBindings;
		}

		public string Field { get; set; }
		public string Title { get; set; }
		public string Placeholder { get; set; }
		public List<string> KeyFields { get; set; }
		public Type ModelType { get; set; }
		public Type Type { get; set; }
		public FormsCollectionDisplayTemplateParameters FormsCollectionDisplayTemplate { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
	}
}