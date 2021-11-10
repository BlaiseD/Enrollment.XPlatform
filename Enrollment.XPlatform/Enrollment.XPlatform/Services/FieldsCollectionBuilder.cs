using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public FieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType) 
            => new FieldsCollectionHelper(formSettings, this.contextProvider, modelType).CreateFields();
    }
}
