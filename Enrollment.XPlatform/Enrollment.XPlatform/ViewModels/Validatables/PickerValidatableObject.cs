using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.Utils;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public class PickerValidatableObject<T> : ValidatableObjectBase<T>
    {
        public PickerValidatableObject(string name, DropDownTemplateDescriptor dropDownTemplate, IEnumerable<IValidationRule> validations, IContextProvider contextProvider) 
            : base(name, dropDownTemplate.TemplateName, validations, contextProvider.UiNotificationService)
        {       
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = contextProvider.HttpService;
            this.Title = this._dropDownTemplate.LoadingIndicatorText;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get
            {
                if (Items?.Any() != true)
                    return null;

                return Items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );
            }

            set
            {
                if (_selectedItem == null && value == null)
                    return;

                if (_selectedItem != null && _selectedItem.Equals(value))
                    return;

                _selectedItem = value;
                Value = _selectedItem == null 
                    ? default 
                    : _selectedItem.GetPropertyValue<T>(_dropDownTemplate.ValueField);

                OnPropertyChanged();
            }
        }

        private List<object> _items;
        public List<object> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private async void GetItemSource()
        {
            try
            {
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._dropDownTemplate.RequestDetails.DataType,
                        ModelType = this._dropDownTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._dropDownTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._dropDownTemplate.RequestDetails.DataReturnType,
                        Selector = this.DropDownTemplate.TextAndValueSelector
                    },
                    this._dropDownTemplate.RequestDetails.DataSourceUrl
                );

                if (response?.Success != true)
                {
#if DEBUG
                    await App.Current.MainPage.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = null;
                await System.Threading.Tasks.Task.Delay(400);
                Items = ((GetListResponse)response).List.Cast<object>().ToList();
                OnPropertyChanged(nameof(SelectedItem));

                this.Title = this._dropDownTemplate.TitleText;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public ICommand SelectedIndexChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
