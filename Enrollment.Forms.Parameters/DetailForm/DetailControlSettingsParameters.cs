using LogicBuilder.Attributes;
using System;

namespace Enrollment.Forms.Parameters.DetailForm
{
    public class DetailControlSettingsParameters : DetailItemSettingsParameters
    {
		public DetailControlSettingsParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. This property being displayed.")]
			string field,

			[Comments("Label for the field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			string title,

			[Comments("String format - useful for binding dates and decimals e.g. {0:F2}.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[Comments("Place holder text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Title)")]
			string placeholder,

			[Comments("The type for the field being displayed. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.  Use the full name (e.g. System.Int32) for literals or core platform types.")]
			Type type,

			[Comments("Holds the XAML template name for the field.")]
			TextFieldTemplateParameters textTemplate = null,

			[Comments("Holds the XAML template name for the field plus additional drop-down related properties (textField, valueField, request details etc.). Useful for displaying the text field given the saved value vield.")]
			DropDownTemplateParameters dropDownTemplate = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Enrollment.Domain.Entities"
		) : base(field)
		{
			Title = title;
			Placeholder = placeholder;
			StringFormat = stringFormat;
			Type = type;
			TextTemplate = textTemplate;
			DropDownTemplate = dropDownTemplate;
		}

		public string Title { get; set; }
		public string Placeholder { get; set; }
		public string StringFormat { get; set; }
		public Type Type { get; set; }
		public TextFieldTemplateParameters TextTemplate { get; set; }
		public DropDownTemplateParameters DropDownTemplate { get; set; }
    }
}