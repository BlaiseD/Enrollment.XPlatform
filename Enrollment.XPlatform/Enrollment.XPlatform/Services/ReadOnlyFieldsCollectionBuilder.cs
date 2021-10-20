using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public class ReadOnlyFieldsCollectionBuilder : IReadOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public ReadOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ObservableCollection<IReadOnly> CreateFieldsCollection(IDetailGroupSettings formSettings)
            => new ReadOnlyFieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
