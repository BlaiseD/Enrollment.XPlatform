using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Utils;
using Enrollment.XPlatform.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class PickerReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public PickerReadOnlyObject(string name, FormControlSettingsDescriptor setting, IContextProvider contextProvider) : base(name, setting.DropDownTemplate.TemplateName)
        {
            this._dropDownTemplate = setting.DropDownTemplate;
            this.httpService = contextProvider.HttpService;
            FormControlSettingsDescriptor = setting;
            this.Title = setting.Title;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private List<object> _items;

        public FormControlSettingsDescriptor FormControlSettingsDescriptor { get; }
        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

        public string DisplayText
        {
            get
            {
                if (SelectedItem == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(FormControlSettingsDescriptor.StringFormat))
                    return SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField);

                return string.Format
                (
                    CultureInfo.CurrentCulture, 
                    FormControlSettingsDescriptor.StringFormat,
                    SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField)
                );
            }
        }

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

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public object SelectedItem
        {
            get
            {
                if (_items?.Any() != true)
                    return null;

                return _items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );
            }
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
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

                _items = null;
                _items = ((GetListResponse)response).List.Cast<object>().ToList();
                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(DisplayText));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }
    }
}
