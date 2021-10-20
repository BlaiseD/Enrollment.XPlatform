using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public interface IFieldsCollectionBuilder
    {
        ObservableCollection<IValidatable> CreateFieldsCollection(IFormGroupSettings formSettings);
    }
}
