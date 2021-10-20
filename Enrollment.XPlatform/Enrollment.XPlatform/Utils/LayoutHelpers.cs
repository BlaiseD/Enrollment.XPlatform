﻿using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Utils
{
    internal static class LayoutHelpers
    {
        public static T AddBinding<T>(this T bindable, BindableProperty targetProperty, BindingBase binding) where T : BindableObject
        {
            bindable.SetBinding
            (
                targetProperty,
                binding
            );

            return bindable;
        }

        public static T SetAutomationPropertiesName<T>(this T bindable, string propertyName) where T : BindableObject
        {
            AutomationProperties.SetName(bindable, propertyName);

            return bindable;
        }

        public static T AssignDynamicResource<T>(this T bindable, BindableProperty property, string key) where T : Element
        {
            bindable.SetDynamicResource
            (
                property, key
            );

            return bindable;
        }

        public static T SetGridColumn<T>(this T bindable, int column) where T : BindableObject
        {
            Grid.SetColumn(bindable, column);
            return bindable;
        }

        public static T SetAbsoluteLayoutBounds<T>(this T bindable, Rectangle rectangle) where T : BindableObject
        {
            AbsoluteLayout.SetLayoutBounds(bindable, rectangle);
            return bindable;
        }

        public static T SetAbsoluteLayoutFlags<T>(this T bindable, AbsoluteLayoutFlags absoluteLayoutFlags) where T : BindableObject
        {
            AbsoluteLayout.SetLayoutFlags(bindable, absoluteLayoutFlags);
            return bindable;
        }

        public static Style GetStaticStyleResource(string styleName)
        {
            if (Application.Current.Resources.TryGetValue(styleName, out object resource)
                && resource is Style style)
                return style;

            throw new ArgumentException($"{nameof(styleName)}: DF65BD5C-E8A5-409C-A736-F6DF1B29D5E7");
        }

        internal static Page CreatePage(this ScreenSettingsBase screenSettings)
        {
            return CreatePage(Enum.GetName(typeof(ViewType), screenSettings.ViewType));

            Page CreatePage(string viewName)
            {
                FlyoutDetailViewModelBase viewModel = (FlyoutDetailViewModelBase)App.ServiceProvider.GetRequiredService
                (
                    typeof(FlyoutDetailViewModelBase).Assembly.GetType
                    (
                        $"Enrollment.XPlatform.ViewModels.{viewName}ViewModel"
                    )
                );

                viewModel.Initialize(screenSettings);

                return (Page)Activator.CreateInstance
                (
                    typeof(MainPageView).Assembly.GetType
                    (
                        $"Enrollment.XPlatform.Views.{viewName}ViewCS"
                    ),
                    viewModel
                );
            }
        }

        internal static DataTemplate GetCollectionViewItemTemplate(string templateName, Dictionary<string, ItemBindingDescriptor> bindings)
        {
            switch (templateName)
            {
                case TemplateNames.HeaderTextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Style = GetStaticStyleResource(ItemStyleNames.HeaderTextDetailListItemStyle),
                            Children =
                            {
                                new StackLayout
                                {
                                    Margin = new Thickness(2),
                                    Padding = new Thickness(7),
                                    Children =
                                    {
                                        new Label
                                        {
                                            FontAttributes = FontAttributes.Bold
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                bindings[BindingNames.Header].Property,
                                                stringFormat : bindings[BindingNames.Header].StringFormat
                                            )
                                        ),
                                        new Label
                                        {
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                bindings[BindingNames.Text].Property,
                                                stringFormat : bindings[BindingNames.Text].StringFormat
                                            )
                                        ),
                                        new Label
                                        {
                                            FontAttributes = FontAttributes.Italic
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                bindings[BindingNames.Detail].Property,
                                                stringFormat : bindings[BindingNames.Detail].StringFormat
                                            )
                                        )
                                    }
                                }
                                .AssignDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor")
                            }
                        }
                    );
                case TemplateNames.TextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Style = GetStaticStyleResource(ItemStyleNames.TextDetailListItemStyle),
                            Children =
                            {
                                new StackLayout
                                {
                                    Margin = new Thickness(2),
                                    Padding = new Thickness(7),
                                    Children =
                                    {
                                        new Label
                                        {
                                            FontAttributes = FontAttributes.Bold
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                bindings[BindingNames.Text].Property,
                                                stringFormat : bindings[BindingNames.Text].StringFormat
                                            )
                                        ),
                                        new Label
                                        {
                                            FontAttributes = FontAttributes.Italic,
                                            VerticalOptions = LayoutOptions.Center
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                bindings[BindingNames.Detail].Property,
                                                stringFormat : bindings[BindingNames.Detail].StringFormat
                                            )
                                        )
                                    }
                                }
                                .AssignDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor")
                            }
                        }
                    );
                default:
                    throw new ArgumentException
                    (
                        $"{nameof(templateName)}: 83C55FEE-9A93-45D3-A972-2335BA0F16AE"
                    );
            }
        }

        private struct TemplateNames
        {
            public const string TextDetailTemplate = "TextDetailTemplate";
            public const string HeaderTextDetailTemplate = "HeaderTextDetailTemplate";
        }

        private struct BindingNames
        {
            public const string Header = "Header";
            public const string Text = "Text";
            public const string Detail = "Detail";
        }

        private struct ItemStyleNames
        {
            public const string TextDetailListItemStyle = "TextDetailListItemStyle";
            public const string HeaderTextDetailListItemStyle = "HeaderTextDetailListItemStyle";
        }

        internal static void AddToolBarItems(IList<ToolbarItem> toolbarItems, ICollection<CommandButtonDescriptor> buttons)
        {
            foreach (var button in buttons)
                toolbarItems.Add(BuildToolbarItem(button));
        }

        static ToolbarItem BuildToolbarItem(CommandButtonDescriptor button)
            => new ToolbarItem
            {
                AutomationId = button.ShortString,
                    //Text = button.LongString,
                    IconImageSource = new FontImageSource
                {
                    FontFamily = EditFormViewHelpers.GetFontAwesomeFontFamily(),
                    Glyph = FontAwesomeIcons.Solid[button.ButtonIcon],
                    Size = 20
                },
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                CommandParameter = button
            }
            .AddBinding(MenuItem.CommandProperty, new Binding(button.Command))
            .SetAutomationPropertiesName(button.ShortString);
    }
}