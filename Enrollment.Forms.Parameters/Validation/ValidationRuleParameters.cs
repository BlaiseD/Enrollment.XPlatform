using LogicBuilder.Attributes;

namespace Enrollment.Forms.Parameters.Validation
{
    public class ValidationRuleParameters
    {
		public ValidationRuleParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "RequiredRule")]
			[Comments("The validation class")]
			[Domain("IsLengthValidRule,IsMatchRule,IsValidEmailRule,IsValidPasswordRule,IsValueTrueRule,MustBeIntegerRule,MustBeNumberRule,MustBePositiveNumberRule,RangeRule,RequiredRule")]
			string className,

			[Comments("The validtion message")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Property) is required.")]
			string message
		)
		{
			ClassName = className;
			Message = message;
		}

		public string ClassName { get; set; }
		public string Message { get; set; }
    }
}