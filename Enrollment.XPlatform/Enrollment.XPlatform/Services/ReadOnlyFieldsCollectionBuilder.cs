using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;

namespace Enrollment.XPlatform.Services
{
    public class ReadOnlyFieldsCollectionBuilder : IReadOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public ReadOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public DetailFormLayout CreateFieldsCollection(IFormGroupSettings formSettings)
            => new ReadOnlyFieldsCollectionHelper(formSettings.FieldSettings, formSettings, this.contextProvider).CreateFields();
    }
}
