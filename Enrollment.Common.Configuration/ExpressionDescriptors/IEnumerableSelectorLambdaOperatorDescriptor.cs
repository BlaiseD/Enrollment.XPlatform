﻿using System.Collections.Generic;
using System;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class IEnumerableSelectorLambdaOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Selector { get; set; }
		public string SourceElementType { get; set; }
		public string ParameterName { get; set; }
    }
}