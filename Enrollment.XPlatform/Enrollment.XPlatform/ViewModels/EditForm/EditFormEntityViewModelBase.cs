﻿using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.EditForm
{
    public abstract class EditFormEntityViewModelBase : ViewModelBase, IDisposable
    {
        protected EditFormEntityViewModelBase(ScreenSettings<EditFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
        {
            this.UiNotificationService = contextProvider.UiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
        }

        public Dictionary<string, IValidatable> BindingPropertiesDictionary
            => FormLayout.Properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());

        public EditFormSettingsDescriptor FormSettings { get; set; }
        public EditFormLayout FormLayout { get; set; }
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        protected IDictionary<string, object> values;

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand != null)
                    return _nextCommand;

                _nextCommand = new Command<CommandButtonDescriptor>
                (
                     Next
                );

                return _nextCommand;
            }
        }

        protected void Next(CommandButtonDescriptor button)
        {
            NavigateNext(button);
        }

        private Task NavigateNext(CommandButtonDescriptor button)
            => this.UiNotificationService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );

        public bool AreFieldsValid()
            => FormLayout.Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        public virtual void Dispose()
        {
        }
    }
}
