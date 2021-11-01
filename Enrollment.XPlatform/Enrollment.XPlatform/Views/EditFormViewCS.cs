using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.EditForm;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Linq;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModel editFormViewModel)
        {
            this.editFormEntityViewModel = editFormViewModel.EditFormEntityViewModel;
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.editFormEntityViewModel;
        }

        private EditFormEntityViewModelBase editFormEntityViewModel;
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.editFormEntityViewModel.Buttons);
            Title = editFormEntityViewModel.FormSettings.Title;

            BindingBase GetHeaderBinding(MultiBindingDescriptor multiBindingDescriptor)
            {
                if (editFormEntityViewModel.FormSettings.EditType == EditType.Add 
                    || multiBindingDescriptor == null)
                    return new Binding($"{nameof(EditFormEntityViewModelBase.FormSettings)}.{nameof(EditFormSettingsDescriptor.Title)}");

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(EditFormEntityViewModelBase.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IValidatable.Value)}")
                    )
                    .Cast<BindingBase>()
                    .ToList()
                };
            }
            Content = new Grid
            {
                Children =
                {
                    (
                        page = new StackLayout
                        {
                            Padding = new Thickness(30),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                                }
                                .AddBinding
                                (
                                    Label.TextProperty, 
                                    GetHeaderBinding(editFormEntityViewModel.FormSettings.HeaderBindings)
                                ),
                                new ScrollView
                                {
                                    Content = new StackLayout()
                                    .AddBinding(BindableLayout.ItemsSourceProperty, new Binding(nameof(EditFormEntityViewModelBase.Properties)))
                                    .SetDataTemplateSelector(EditFormViewHelpers.QuestionTemplateSelector)
                                }
                            }
                        }
                    ),
                    (
                        transitionGrid = new Grid().AssignDynamicResource
                        (
                            VisualElement.BackgroundColorProperty, 
                            "PageBackgroundColor"
                        )
                    )
                }
            };
        }
    }
}