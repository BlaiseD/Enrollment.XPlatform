using AutoMapper;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public FieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ObservableCollection<IValidatable> CreateFieldsCollection(IFormGroupSettings formSettings) 
            => new FieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
