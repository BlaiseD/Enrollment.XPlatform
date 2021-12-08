using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.DetailForm
{
    public abstract class DetailFormEntityViewModelBase : ViewModelBase
    {
        protected DetailFormEntityViewModelBase(ScreenSettings<EditFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
        {
            this.UiNotificationService = contextProvider.UiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            FormLayout = contextProvider.ReadOnlyFieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings);
        }

        public Dictionary<string, IReadOnly> BindingPropertiesDictionary
            => FormLayout.Properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());

        public EditFormSettingsDescriptor FormSettings { get; set; }
        public DetailFormLayout FormLayout { get; set; }
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

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

        protected Task NavigateNext(CommandButtonDescriptor button)
            => this.UiNotificationService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
    }
}
