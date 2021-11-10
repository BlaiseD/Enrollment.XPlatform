using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Services
{
    public interface IFieldsCollectionBuilder
    {
        EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType);
    }
}
