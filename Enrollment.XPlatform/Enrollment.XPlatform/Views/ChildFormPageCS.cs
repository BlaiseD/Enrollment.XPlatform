using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;

using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class ChildFormPageCS : ContentPage
    {
        public ChildFormPageCS(IValidatable formValidatable)
        {
            this.formValidatable = formValidatable;
            
            Content = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children = 
                {
                    new ContentView 
                    { 
                        Content = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource("ChildFormPopupViewStyle"),
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupHeaderStyle"),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupHeaderLabelStyle"),
                                        }.AddBinding(Label.TextProperty, new Binding("Title"))
                                    }
                                },
                                new ScrollView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("ChildFormPopupScrollViewStyle"),
                                    Content = new StackLayout()
                                    .AddBinding(BindableLayout.ItemsSourceProperty, new Binding(nameof(FormValidatableObject<object>.Properties)))
                                    .SetDataTemplateSelector(EditFormViewHelpers.QuestionTemplateSelector)
                                },
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") },
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupFooterStyle"),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("CancelCommand"))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupAcceptButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("SubmitCommand"))
                                        .SetGridColumn(2)
                                    }
                                }
                            }
                        }
                    }
                    .AssignDynamicResource(VisualElement.BackgroundColorProperty, "PopupViewBackgroundColor")
                    .SetAbsoluteLayoutBounds(new Rectangle(0, 0, 1, 1))
                    .SetAbsoluteLayoutFlags(AbsoluteLayoutFlags.All)
                }
            };

            this.BackgroundColor = Color.Transparent;
            Visual = VisualMarker.Material;
            this.BindingContext = this.formValidatable;
        }

        private IValidatable formValidatable;
    }
}