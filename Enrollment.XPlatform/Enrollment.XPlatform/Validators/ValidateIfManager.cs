using AutoMapper;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Enrollment.XPlatform.Validators
{
    internal class ValidateIfManager<TModel> : IDisposable
    {
        public ValidateIfManager(ICollection<IValidatable> currentProperties, List<ValidateIf<TModel>> conditions, IMapper mapper, UiNotificationService uiNotificationService)
        {
            CurrentProperties = currentProperties;
            this.conditions = conditions;
            this.mapper = mapper;
            this.uiNotificationService = uiNotificationService;
            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(PropertyChanged);
        }

        private void PropertyChanged(string fieldName)
        {
            Type thisClassType = this.GetType();
            conditions.ForEach
            (
                condition => thisClassType.GetMethod(condition.DirectiveDefinition.FunctionName)
                    .Invoke(this, GetArguments(condition))
            );

            object[] GetArguments(ValidateIf<TModel> condition)
            {
                if (condition.DirectiveDefinition.Arguments?.Any() != true)
                    return new object[] { condition };

                return condition.DirectiveDefinition.Arguments.Values.Aggregate
                (
                    new List<object> { condition }, 
                    (list, next) =>
                    {
                        list.Add(next);
                        return list;
                    }
                ).ToArray();
            }
        }

        private readonly IMapper mapper;
        private readonly List<ValidateIf<TModel>> conditions;
        private readonly UiNotificationService uiNotificationService;
        private readonly IDisposable propertyChangedSubscription;

        public ICollection<IValidatable> CurrentProperties { get; }
        private IDictionary<string, IValidatable> CurrentPropertiesDictionary
            => CurrentProperties.ToDictionary(p => p.Name);

        public void Check(ValidateIf<TModel> condition)
        {
            DoCheck(CurrentPropertiesDictionary[condition.Field]);

            void DoCheck(IValidatable currentValidatable)
            {
                HashSet<IValidationRule> existingRules = currentValidatable.Validations.ToHashSet();
                TModel entity = mapper.Map<TModel>(CurrentProperties.ToDictionary(p => p.Name, p => p.Value));
                if (CanValidate(entity, condition.Evaluator))
                {
                    if (!existingRules.Contains(condition.Validator))
                    {
                        currentValidatable.Validations.Add(condition.Validator);
                        currentValidatable.Validate();
                    }
                }
                else
                {
                    if (existingRules.Contains(condition.Validator))
                    {
                        currentValidatable.Validations.Remove(condition.Validator);
                        currentValidatable.Validate();
                    }
                }
            }
        }

        bool CanValidate(TModel entity, Expression<Func<TModel, bool>> evaluator) 
            => new List<TModel> { entity }.AsQueryable().All(evaluator);

        public void Dispose()
        {
            DisposeSubscription(propertyChangedSubscription);
        }

        private void DisposeSubscription(IDisposable subscription)
        {
            if (subscription != null)
            {
                subscription.Dispose();
            }
        }
    }
}
