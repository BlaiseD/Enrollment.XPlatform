using AutoMapper;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Utils
{
    public class HideIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<HideIf<TModel>, TModel>
    {
        public HideIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IValidatable> properties,
                                                 IMapper mapper,
                                                 List<HideIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
