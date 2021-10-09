﻿using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using System.Collections.Generic;

namespace Enrollment.Bsl.Flow.Cache
{
    public class FlowDataCache
    {
        public BaseRequest Request { get; set; }
        public BaseResponse Response { get; set; }
        public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    }
}
