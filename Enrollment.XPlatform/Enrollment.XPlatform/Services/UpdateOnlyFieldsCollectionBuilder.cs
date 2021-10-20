using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public class UpdateOnlyFieldsCollectionBuilder : IUpdateOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public UpdateOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ObservableCollection<IValidatable> CreateFieldsCollection(IFormGroupSettings formSettings)
            => new UpdateOnlyFieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
