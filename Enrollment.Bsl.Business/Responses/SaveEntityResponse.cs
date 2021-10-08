﻿using Enrollment.Domain;
using LogicBuilder.Attributes;

namespace Enrollment.Bsl.Business.Responses
{
    public class SaveEntityResponse : BaseResponse
    {
        [AlsoKnownAs("SaveEntityResponse_Entity")]
        public EntityModelBase Entity { get; set; }
    }
}
