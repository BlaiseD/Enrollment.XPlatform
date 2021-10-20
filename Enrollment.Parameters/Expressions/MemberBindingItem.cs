﻿using LogicBuilder.Attributes;

namespace Enrollment.Parameters.Expressions
{
    public class MemberBindingItem
    {
        public MemberBindingItem
        (
            [NameValue(AttributeNames.USEFOREQUALITY, "true")]
            [NameValue(AttributeNames.USEFORHASHCODE, "true")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
            [Comments("Update fieldTypeSource first. Property to bind the selector to.")]
            string property,

            [Comments("Selector.")]
            IExpressionParameter selector,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string fieldTypeSource = null
        )
        {
            Property = property;
            Selector = selector;
        }

        public string Property { get; set; }
        public IExpressionParameter Selector { get; set; }
    }
}
