using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.Forms.Parameters.EditForm
{
    public class GroupBoxSettingsParameters : FormItemSettingsParameters
    {
		public GroupBoxSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Header")]
			[Comments("Title for the group box.")]
			string groupHeader,

			[Comments("Configuration for each field in the group box.")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("Multibindings list for the group header field - typically used in edit mode.")]
			MultiBindingParameters headerBindings = null
		)
		{
			if (fieldSettings.Any(s => s is GroupBoxSettingsParameters))
				throw new ArgumentException($"{nameof(fieldSettings)}: D8590E1F-D029-405F-8E6C-EA98803004B8");

			GroupHeader = groupHeader;
			FieldSettings = fieldSettings;
			HeaderBindings = headerBindings;
		}

		public string GroupHeader { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
    }
}