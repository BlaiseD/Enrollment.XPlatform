using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.ViewModels;

namespace Enrollment.XPlatform.Services
{
    public interface IReadOnlyFieldsCollectionBuilder
    {
        DetailFormLayout CreateFieldsCollection(IDetailGroupSettings formSettings);
    }
}
