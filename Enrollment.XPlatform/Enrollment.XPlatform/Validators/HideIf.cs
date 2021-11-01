using Enrollment.Forms.Configuration.Directives;
using System;
using System.Linq.Expressions;

namespace Enrollment.XPlatform.Validators
{
    //From DirectiveDescriptor where DirectiveDescriptor.DirectiveDefinitionDescriptor.Classname == HideIf
    //Evaluator is DirectiveDescriptor.FilterLambdaOperatorDescriptor
    //Field is the field to evaluate
    //HideIfManager listens for PropertyChanged and calls HideIfManager.Check()
    // or the specified method from DirectiveDefinition.FunctionName
    public class HideIf<T>
    {
        public Expression<Func<T, bool>> Evaluator { get; set; }
        public DirectiveDefinitionDescriptor DirectiveDefinition { get; set; }
        public string Field { get; set; }
        public string ParentField { get; set; }
    }
}
