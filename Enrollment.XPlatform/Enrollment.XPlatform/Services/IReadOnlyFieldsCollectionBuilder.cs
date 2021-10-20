using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public interface IReadOnlyFieldsCollectionBuilder
    {
        ObservableCollection<IReadOnly> CreateFieldsCollection(IDetailGroupSettings formSettings);
    }
}
