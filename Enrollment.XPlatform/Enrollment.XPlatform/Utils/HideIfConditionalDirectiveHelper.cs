using AutoMapper;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Enrollment.XPlatform.Utils
{
    public class HideIfConditionalDirectiveHelper<TModel>
    {
        private readonly IFormGroupSettings formGroupSettings;
        private readonly IEnumerable<IValidatable> properties;
        private readonly IMapper mapper;
        private readonly List<HideIf<TModel>> parentList;
        private readonly string parentName;
        const string PARAMETERS_KEY = "parameters";

        public HideIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties, IMapper mapper, List<HideIf<TModel>> parentList = null, string parentName = null)
        {
            this.formGroupSettings = formGroupSettings;
            this.properties = properties;
            this.mapper = mapper;
            this.parentList = parentList;
            this.parentName = parentName;
        }

        public List<HideIf<TModel>> GetConditions()
        {
            if (formGroupSettings.ConditionalDirectives == null)
                return this.parentList ?? new List<HideIf<TModel>>();

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);

            List<HideIf<TModel>> conditions = formGroupSettings.ConditionalDirectives.Aggregate(parentList ?? new List<HideIf<TModel>>(), (list, kvp) =>
            {
                kvp.Value.ForEach
                (
                    descriptor =>
                    {
                        if (descriptor.Definition.ClassName != nameof(HideIf<TModel>))
                            return;

                        var validatable = propertiesDictionary[GetFieldName(kvp.Key)];

                        list.Add
                        (
                            new HideIf<TModel>
                            {
                                Field = GetFieldName(kvp.Key),
                                ParentField = this.parentName,
                                Evaluator = (Expression<Func<TModel, bool>>)mapper.Map<FilterLambdaOperator>
                                (
                                    descriptor.Condition,
                                    opts => opts.Items[PARAMETERS_KEY] = new Dictionary<string, ParameterExpression>()
                                ).Build(),
                                DirectiveDefinition = descriptor.Definition
                            }
                        );
                    }
                );

                return list;
            });

            formGroupSettings.FieldSettings.ForEach(descriptor =>
            {
                if (!(descriptor is FormGroupSettingsDescriptor childForm))
                    return;

                if ((childForm.FormGroupTemplate?.TemplateName) != FromGroupTemplateNames.InlineFormGroupTemplate)
                    return;

                conditions = new HideIfConditionalDirectiveHelper<TModel>
                (
                    childForm,
                    properties,
                    mapper,
                    conditions,
                    GetFieldName(childForm.Field)
                ).GetConditions();
            });

            return conditions;
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
