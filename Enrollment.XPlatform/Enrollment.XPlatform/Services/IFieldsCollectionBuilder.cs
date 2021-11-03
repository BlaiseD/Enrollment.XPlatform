using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.ViewModels;

namespace Enrollment.XPlatform.Services
{
    public interface IFieldsCollectionBuilder
    {
        EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings);
    }
}
