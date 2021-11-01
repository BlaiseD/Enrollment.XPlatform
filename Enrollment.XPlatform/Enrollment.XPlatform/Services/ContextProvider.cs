﻿using AutoMapper;

namespace Enrollment.XPlatform.Services
{
    public class ContextProvider : IContextProvider
    {
        public ContextProvider(UiNotificationService uiNotificationService, IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder, IEntityStateUpdater entityStateUpdater, IEntityUpdater entityUpdater, IGetItemFilterBuilder getItemFilterBuilder, IHttpService httpService, IMapper mapper, ISearchSelectorBuilder searchSelectorBuilder, IPropertiesUpdater propertiesUpdater, IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater, IHideIfConditionalDirectiveBuilder hideIfConditionalDirectiveBuilder)
        {
            UiNotificationService = uiNotificationService;
            ConditionalValidationConditionsBuilder = conditionalValidationConditionsBuilder;
            EntityStateUpdater = entityStateUpdater;
            EntityUpdater = entityUpdater;
            GetItemFilterBuilder = getItemFilterBuilder;
            HttpService = httpService;
            Mapper = mapper;
            SearchSelectorBuilder = searchSelectorBuilder;
            PropertiesUpdater = propertiesUpdater;
            ReadOnlyPropertiesUpdater = readOnlyPropertiesUpdater;
            HideIfConditionalDirectiveBuilder = hideIfConditionalDirectiveBuilder;

            //passing IContextProvider to FieldsCollectionBuilder will create a circular dependency
            //so creating the instance here instead of using DI.
            FieldsCollectionBuilder = new FieldsCollectionBuilder(this);
            UpdateOnlyFieldsCollectionBuilder = new UpdateOnlyFieldsCollectionBuilder(this);
            ReadOnlyFieldsCollectionBuilder = new ReadOnlyFieldsCollectionBuilder(this);
        }

        public IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        public IHideIfConditionalDirectiveBuilder HideIfConditionalDirectiveBuilder { get; }
        public IEntityStateUpdater EntityStateUpdater { get; }
        public IEntityUpdater EntityUpdater { get; }
        public IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        public IUpdateOnlyFieldsCollectionBuilder UpdateOnlyFieldsCollectionBuilder { get; }
        public IReadOnlyFieldsCollectionBuilder ReadOnlyFieldsCollectionBuilder { get; }
        public IGetItemFilterBuilder GetItemFilterBuilder { get; }
        public IHttpService HttpService { get; }
        public IMapper Mapper { get; }
        public ISearchSelectorBuilder SearchSelectorBuilder { get; }
        public IPropertiesUpdater PropertiesUpdater { get; }
        public IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        public UiNotificationService UiNotificationService { get; }
    }
}
