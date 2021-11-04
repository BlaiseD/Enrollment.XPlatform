using AutoMapper;
using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.EditForm
{
    public class EditFormEntityViewModel<TModel> : EditFormEntityViewModelBase where TModel : Domain.EntityModelBase
    {
        public EditFormEntityViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
            : base(screenSettings, contextProvider)
        {
            this.entityStateUpdater = contextProvider.EntityStateUpdater;
            this.httpService = contextProvider.HttpService;
            this.propertiesUpdater = contextProvider.PropertiesUpdater;
            this.mapper = contextProvider.Mapper;
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                FormLayout.Properties,
                contextProvider.ConditionalValidationConditionsBuilder.GetConditions<TModel>
                (
                    FormSettings,
                    FormLayout.Properties
                ),
                contextProvider.Mapper,
                this.UiNotificationService
            );

            this.hideIfManager = new HideIfManager<TModel>
            (
                FormLayout.Properties,
                contextProvider.HideIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    FormSettings,
                    FormLayout.Properties
                ),
                contextProvider.Mapper,
                this.UiNotificationService
            );

            propertyChangedSubscription = this.UiNotificationService.ValueChanged.Subscribe(FieldChanged);

            if (this.FormSettings.EditType == EditType.Update)
                GetEntity();
        }

        private readonly IEntityStateUpdater entityStateUpdater;
        private readonly IHttpService httpService;
        private readonly IPropertiesUpdater propertiesUpdater;
        private readonly IMapper mapper;
        private readonly ValidateIfManager<TModel> validateIfManager;
        private readonly HideIfManager<TModel> hideIfManager;
        private TModel entity;
        private Dictionary<string, object> originalEntityDictionary = new Dictionary<string, object>();
        private readonly IDisposable propertyChangedSubscription;

        public override void Dispose()
        {
            base.Dispose();
            Dispose(this.validateIfManager);
            Dispose(this.hideIfManager);
            Dispose(this.propertyChangedSubscription);
            foreach (var property in FormLayout.Properties)
            {
                if (property is IDisposable disposable)
                    Dispose(disposable);
            }
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }

        private void FieldChanged(string fieldName)
        {
            (SubmitCommand as Command).ChangeCanExecute();
        }

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 883E834F-98A6-4DF7-9D07-F1BB0D6639E1");

            BaseResponse baseResponse = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.httpService.GetEntity
                (
                    new GetEntityRequest
                    {
                        Filter = this.FormSettings.RequestDetails.Filter,
                        SelectExpandDefinition = this.FormSettings.RequestDetails.SelectExpandDefinition,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType
                    }
                )
            );

            if (baseResponse.Success == false)
            {
                await App.Current.MainPage.DisplayAlert
                (
                    "Errors",
                    string.Join(Environment.NewLine, baseResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            GetEntityResponse getEntityResponse = (GetEntityResponse)baseResponse;
            this.entity = (TModel)getEntityResponse.Entity;
            this.originalEntityDictionary = this.entity.EntityToObjectDictionary
            (
               mapper,
               this.FormSettings.FieldSettings
            );

            this.propertiesUpdater.UpdateProperties
            (
                FormLayout.Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??= new Command<CommandButtonDescriptor>
        (
            execute: async (button) =>
            {
                BaseResponse response = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
                (
                    () => this.httpService.SaveEntity
                    (
                        new SaveEntityRequest
                        {
                            Entity = this.entityStateUpdater.GetUpdatedModel
                            (
                                entity,
                                this.originalEntityDictionary,
                                FormLayout.Properties,
                                FormSettings.FieldSettings
                            )
                        },
                        this.FormSettings.EditType == EditType.Add
                            ? this.FormSettings.RequestDetails.AddUrl
                            : this.FormSettings.RequestDetails.UpdateUrl
                    )
                );

                if (response.Success == false)
                {
                    await App.Current.MainPage.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
                }

                Next(button);
            },
            canExecute: (button) => AreFieldsValid()
        );
    }
}
