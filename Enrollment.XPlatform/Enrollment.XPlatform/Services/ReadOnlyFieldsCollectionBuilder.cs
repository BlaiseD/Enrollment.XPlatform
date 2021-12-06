using Enrollment.Forms.Configuration.DetailForm;
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

        public DetailFormLayout CreateFieldsCollection(IDetailGroupSettings formSettings)
            => new ReadOnlyFieldsCollectionHelper(formSettings.FieldSettings, formSettings, this.contextProvider).CreateFields();
    }
}
