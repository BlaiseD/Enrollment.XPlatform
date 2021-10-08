using System.Collections.Generic;

namespace Enrollment.Bsl.Business.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
    }
}
