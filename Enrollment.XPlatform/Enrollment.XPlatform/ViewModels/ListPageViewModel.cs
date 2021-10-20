using Enrollment.Forms.Configuration.ListForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.ListPage;
using System;
using System.Reflection;

namespace Enrollment.XPlatform.ViewModels
{
    public class ListPageViewModel : FlyoutDetailViewModelBase
    {
        private readonly IHttpService httpService;

        public ListPageViewModel(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            ListPageCollectionViewModel = CreateSearchPageListViewModel((ScreenSettings<ListFormSettingsDescriptor>)screenSettings);
        }

        public ListPageCollectionViewModelBase ListPageCollectionViewModel { get; set; }

        private ListPageCollectionViewModelBase CreateSearchPageListViewModel(ScreenSettings<ListFormSettingsDescriptor> screenSettings)
        {
            return (ListPageCollectionViewModelBase)Activator.CreateInstance
            (
                typeof(ListPageCollectionViewModel<>).MakeGenericType
                (
                    Type.GetType
                    (
                        screenSettings.Settings.ModelType,
                        AssemblyResolver,
                        TypeResolver
                    )
                ),
                new object[]
                {
                    screenSettings,
                    this.httpService,
                }
            );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
