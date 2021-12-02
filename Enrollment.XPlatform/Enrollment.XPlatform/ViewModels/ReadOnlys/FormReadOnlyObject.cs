﻿using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class FormReadOnlyObject<T> : ReadOnlyObjectBase<T> where T : class
    {
        public FormReadOnlyObject(string name, IChildDetailGroupSettings setting, IContextProvider contextProvider) 
            : base(name, setting.FormGroupTemplate.TemplateName)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.propertiesUpdater = contextProvider.ReadOnlyPropertiesUpdater;
            this.Placeholder = this.FormSettings.Placeholder;
            FormLayout = contextProvider.ReadOnlyFieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings);
        }

        public IChildDetailGroupSettings FormSettings { get; set; }
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;

        public DetailFormLayout FormLayout { get; set; }

        public string DisplayText => string.Empty;

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

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                this.propertiesUpdater.UpdateProperties
                (
                    FormLayout.Properties,
                    base.Value,
                    FormSettings.FieldSettings
                );
            }
        }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand != null)
                    return _openCommand;

                _openCommand = new Command
                (
                    () =>
                    {
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PushModalAsync
                            (
                                new Views.ReadOnlyChildFormPageCS(this)
                            )
                        );
                    }
                );

                return _openCommand;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand != null)
                    return _cancelCommand;

                _cancelCommand = new Command
                (
                    Cancel
                );

                return _cancelCommand;
            }
        }

        protected virtual void Cancel()
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PopModalAsync()
            );
        }
    }
}
