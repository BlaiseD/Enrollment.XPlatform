﻿using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class ChildFormArrayPageCS : ContentPage
    {
        public ChildFormArrayPageCS(IValidatable formArrayValidatable)
        {
            this.formArrayValidatable = formArrayValidatable;
            this.formsCollectionDisplayTemplateDescriptor = (FormsCollectionDisplayTemplateDescriptor)this.formArrayValidatable.GetType()
                .GetProperty(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate))
                .GetValue(this.formArrayValidatable);

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
                            Style = LayoutHelpers.GetStaticStyleResource("FormArrayPopupViewStyle"),
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
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("FormArrayPopupCollectionViewStyle"),
                                    ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                    (
                                        this.formsCollectionDisplayTemplateDescriptor.TemplateName,
                                        this.formsCollectionDisplayTemplateDescriptor.Bindings
                                    )
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Value"))
                                .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding("SelectionChangedCommand"))
                                .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding("SelectedItem")),
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") },
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupFooterStyle"),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupAddButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("AddCommand")),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupEditButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("EditCommand"))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupDeleteButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("DeleteCommand"))
                                        .SetGridColumn(2),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("CancelCommand"))
                                        .SetGridColumn(3),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupAcceptButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("SubmitCommand"))
                                        .SetGridColumn(4)
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
            this.BindingContext = this.formArrayValidatable;
        }

        private IValidatable formArrayValidatable;
        private FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
    }
}