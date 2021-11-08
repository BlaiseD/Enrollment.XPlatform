using AutoMapper;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Utils
{
    public class ReloadIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<ReloadIf<TModel>, TModel>
    {
        public ReloadIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IValidatable> properties,
                                                 IMapper mapper,
                                                 List<ReloadIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
