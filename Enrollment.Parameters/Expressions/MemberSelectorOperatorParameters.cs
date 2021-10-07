namespace Enrollment.Parameters.Expressions
{
    public class MemberSelectorOperatorParameters : IExpressionParameter
    {
		public MemberSelectorOperatorParameters()
		{
		}

		public MemberSelectorOperatorParameters(string memberFullName, IExpressionParameter sourceOperand)
		{
			MemberFullName = memberFullName;
			SourceOperand = sourceOperand;
		}

		public string MemberFullName { get; set; }
		public IExpressionParameter SourceOperand { get; set; }
    }
}