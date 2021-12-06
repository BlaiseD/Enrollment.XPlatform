using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DetailForm
{
    public interface IDetailGroupSettings : IDetailGroupBoxSettings
    {
        string ModelType { get; }
        string Title { get; }
    }
}
