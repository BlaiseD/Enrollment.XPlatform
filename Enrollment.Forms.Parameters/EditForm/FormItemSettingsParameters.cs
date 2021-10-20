using LogicBuilder.Attributes;

namespace Enrollment.Forms.Parameters.EditForm
{
    abstract public class FormItemSettingsParameters
    {
		public FormItemSettingsParameters
		(
			string field
		)
		{
			Field = field;
		}

		public string Field { get; set; }
    }
}