﻿using Enrollment.Bsl.Business.Responses.Json;
using Enrollment.Common.Configuration.Json;
using Enrollment.Domain.Json;
using Enrollment.Utils;
using System.Text.Json;

namespace Enrollment.XPlatform.Utils
{
    public static class SerializationOptions
    {
        private static JsonSerializerOptions _default;
        public static JsonSerializerOptions Default
        {
            get
            {
                if (_default != null)
                    return _default;

                _default = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new DescriptorConverter(),
                        new ModelConverter(),
                        new ObjectConverter(),
                        new ResponseConverter()
                    }
                };

                return _default;
            }
        }
    }
}
