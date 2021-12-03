using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enrollment.Forms.Parameters.DetailForm
{
    public class DetailGroupBoxSettingsParameters : DetailItemSettingsParameters
    {
		public DetailGroupBoxSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Header")]
			[Comments("Title for the group box.")]
			string groupHeader,

			[Comments("Configuration for each field in the group box.")]
			List<DetailItemSettingsParameters> fieldSettings,

			[Comments("Multibindings list for the group header field.")]
			MultiBindingParameters headerBindings = null,

			[Comments("Hide this group box.")]
			bool isHidden = false
		)
		{
			if (fieldSettings.Any(s => s is DetailGroupBoxSettingsParameters))
				throw new ArgumentException($"{nameof(fieldSettings)}: 1C646CA3-0132-42EA-9F0C-C7E9A4C35FB0");

			GroupHeader = groupHeader;
			FieldSettings = fieldSettings;
			HeaderBindings = headerBindings;
			IsHidden = isHidden;
		}

		public string GroupHeader { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
		public bool IsHidden { get; set; }
	}
}
