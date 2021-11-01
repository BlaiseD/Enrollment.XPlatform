using AutoMapper;

namespace Enrollment.XPlatform.Services
{
    public interface IContextProvider
    {
        IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        IHideIfConditionalDirectiveBuilder HideIfConditionalDirectiveBuilder { get; }
        IEntityStateUpdater EntityStateUpdater { get; }
        IEntityUpdater EntityUpdater { get; }
        IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        IUpdateOnlyFieldsCollectionBuilder UpdateOnlyFieldsCollectionBuilder { get; }
        IReadOnlyFieldsCollectionBuilder ReadOnlyFieldsCollectionBuilder { get; }
        IGetItemFilterBuilder GetItemFilterBuilder { get; }
        IHttpService HttpService { get; }
        IMapper Mapper { get; }
        ISearchSelectorBuilder SearchSelectorBuilder { get; }
        IPropertiesUpdater PropertiesUpdater { get; }
        IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        UiNotificationService UiNotificationService { get; }
    }
}
