using Enrollment.Forms.Configuration.Directives;
using System;
using System.Linq.Expressions;

namespace Enrollment.XPlatform.Validators
{
    //From DirectiveDescriptor where DirectiveDescriptor.DirectiveDefinitionDescriptor.Classname == ValidateIf
    //Evaluator is DirectiveDescriptor.FilterLambdaOperatorDescriptor
    //Field is the field to evaluate
    //ValidateIfManager listens for PropertyChanged and calls ValidateIfManager.Check()
    // or the specified method from DirectiveDefinition.FunctionName
    public class ValidateIf<T>
    {
        public Expression<Func<T, bool>> Evaluator { get; set; }
        public DirectiveDefinitionDescriptor DirectiveDefinition { get; set; }
        public IValidationRule Validator { get; set; }
        public string Field { get; set; }
        public string ParentField { get; set; }
    }
}
